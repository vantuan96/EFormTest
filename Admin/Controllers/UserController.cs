using Admin.Common;
using Admin.Common.Model;
using Admin.Models;
using DataAccess.Models;
using DataAccess.Repository;
using System;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Linq.Dynamic;
using Admin.Common.Extentions;

namespace Admin.Controllers
{
    [Authorize(Roles = Constant.AdminRoles.SuperAdmin + "," + Constant.AdminRoles.ManageUser)]
    public class UserController : Controller
    {
        protected IUnitOfWork unitOfWork = new EfUnitOfWork();

        // GET: User
        public ActionResult Index()
        {
            var model = new UserViewModel();
            return View(model);
        }

        public ActionResult GetListUsers(DataTablesQueryModel queryModel)
        {
            try
            {
                int totalResultsCount;
                var take = queryModel.length;
                var skip = queryModel.start;

                string sortBy = "";
                string sortDir = "";
                if (queryModel.order != null)
                {
                    sortBy = queryModel.columns[queryModel.order[0].column].data;
                    sortDir = queryModel.order[0].dir.ToLower();
                }

                var list = unitOfWork.UserRepository.AsQueryable();

                var filterName = queryModel.columns.First(s => s.name == "UserName").search.value;
                if (!string.IsNullOrEmpty(filterName))
                {
                    filterName = filterName.Trim().ToLower().GetVnStringOnlyCharactersAndNumbers();
                    list = list.Where(s => s.Username.Trim().ToLower().Contains(filterName));
                }    

                totalResultsCount = list.Count();

                var result = list.OrderBy(sortBy + (sortDir == "desc" ? " descending" : "")).Skip(skip).Take(take).ToList()
                    .Select(x => new UserViewModel
                    {
                        Id = x.Id,
                        UserName = x.Username,
                        DisplayName = x.DisplayName,
                        Department = x.Department,
                        Title = x.Title,
                        IsDeleted = x.IsDeleted
                    });

                return Json(new
                {
                    queryModel.draw,
                    recordsTotal = totalResultsCount,
                    recordsFiltered = totalResultsCount,
                    data = result
                });
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeactivateUser(Guid id)
        {
            try
            {
                var user = unitOfWork.UserRepository.GetById(id);
                if (user == null)
                    return Json(false);
                else
                {
                    user.SessionId = null;
                    user.Session = null;
                    unitOfWork.UserRepository.Delete(user);
                    unitOfWork.Commit();
                }
                return Json(true);
            }
            catch (Exception)
            {
                return Json(false);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActivateUser(Guid id)
        {
            try
            {
                var user = unitOfWork.UserRepository.GetById(id);
                if (user == null)
                    return Json(false);
                else
                {
                    user.IsDeleted = false;
                }
                unitOfWork.Commit();
                return Json(true);
            }
            catch (Exception)
            {
                return Json(false);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUser(UserViewModel model)
        {
            try
            {
                var user = unitOfWork.UserRepository.FirstOrDefault(s => s.Username.ToLower() == model.UserName.ToLower());
                if (user != null) return Json(false);
                var result = GetUserADInfo(model.UserName);
                if (result == null) return Json(false);

                Guid newUserId = Guid.NewGuid();
                var newUser = new User {
                    Id = newUserId,
                    Username = model.UserName.Trim().ToLower(),
                    DisplayName = result.DisplayName,
                    EHOSAccount = model.EHOSAccount,
                    Department = result.Department,
                    Title = result.Title,
                    Fullname = result.FullName,
                    IsAdminUser = model.IsAdminUser,
                };
                unitOfWork.UserRepository.Add(newUser);

                if (model.Positions != null)
                {
                    var listPositionIds = model.Positions.Select(s => Guid.Parse(s)).ToList();
                    foreach (var item in listPositionIds)
                    {
                        var newPosition = new PositionUser
                        {
                            PositionId = item,
                            UserId = newUserId
                        };
                        unitOfWork.PositionUserRepository.Add(newPosition);
                    }
                }

                if (model.Roles != null)
                {
                    var listRoleIds = model.Roles.Select(s => Guid.Parse(s)).ToList();
                    foreach (var item in listRoleIds)
                    {
                        var newRoles = new UserRole
                        {
                            RoleId = item,
                            UserId = newUserId
                        };
                        unitOfWork.UserRoleRepository.Add(newRoles);
                    }
                }

                if (model.Specialties != null)
                {
                    var listSpecialtyIds = model.Specialties.Select(s => Guid.Parse(s)).ToList();
                    foreach (var item in listSpecialtyIds)
                    {
                        var newSpecialty = new UserSpecialty
                        {
                            SpecialtyId = item,
                            UserId = newUserId
                        };
                        unitOfWork.UserSpecialtyRepository.Add(newSpecialty);
                    }
                }

                if (model.AdminRoles != null)
                {
                    var listRoleIds = model.AdminRoles.Select(s => int.Parse(s)).ToList();
                    foreach (var item in listRoleIds)
                    {
                        var newRoles = new UserAdminRole
                        {
                            AdminRoleId = item,
                            UserId = newUserId
                        };
                        unitOfWork.UserAdminRoleRepository.Add(newRoles);
                    }
                }

                unitOfWork.Commit();
                return Json(true);
            }
            catch (Exception)
            {
                return Json(false);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckName(string ad)
        {
            var x = Regex.IsMatch(ad, @"^[a-zA-Z0-9.]+$");
            if (!x)
                return Json(new UserADCheckModel { IsInvalidADAccount = true });

            string ip = Request.UserHostAddress;
            if(IsADSpam(ip))
                return Json(new UserADCheckModel { IsSpamADAccount = true });
            IncreaseADSpam(ip);

            var user = unitOfWork.UserRepository.FirstOrDefault(s => s.Username.ToLower() == ad.ToLower());
            if (user != null) 
                return Json(new UserADCheckModel { IsExistedAccount = true });

            var result = GetUserADInfo(ad);
            if (result == null) 
                return Json(new UserADCheckModel { IsInvalidADAccount = true });

            return Json(new UserADCheckModel {
                Department = result.Department,
                Title = result.Title,
                DisplayName = result.DisplayName
            });
        }

        public ActionResult GetUserDetail(Guid? id)
        {
            if (id == null)
                return PartialView("_PartialViewModalUser", new UserViewModel());
            else
            {
                var user = unitOfWork.UserRepository.GetById(id.Value);
                if (user == null) return PartialView("_PartialViewModalUser", new UserViewModel());
                return PartialView("_PartialViewModalUser", new UserViewModel
                {
                    Id = user.Id,
                    UserName = user.Username,
                    DisplayName = user.DisplayName,
                    EHOSAccount = user.EHOSAccount,
                    Department = user.Department,
                    Title = user.Title,
                    IsAdminUser = user.IsAdminUser,
                    Positions = user.PositionUsers.Where(x => !x.IsDeleted).Select(x => x.Position.Id.ToString()).ToList(),
                    Roles = user.UserRoles.Where(x => !x.IsDeleted).Select(x => x.Role.Id.ToString()).ToList(),
                    Specialties = user.UserSpecialties.Where(x => !x.IsDeleted).Select(x => x.Specialty.Id.ToString()).ToList(),
                    AdminRoles = user.UserAdminRoles.Where(x => !x.IsDeleted).Select(x => x.AdminRole.Id.ToString()).ToList()
                });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(UserViewModel model)
        {
            try
            {
                var user = unitOfWork.UserRepository.FirstOrDefault(s => s.Id == model.Id);
                if (user != null)
                {
                    user.UserPositionId = model.PositionId;
                    user.IsAdminUser = model.IsAdminUser;
                    user.EHOSAccount = model.EHOSAccount;

                    #region Edit Positions
                    if (model.Positions == null)
                        foreach (var item in user.PositionUsers.Where(s => !s.IsDeleted))
                            unitOfWork.PositionUserRepository.Delete(item);
                    else
                    {
                        var listPositionIds = model.Positions.Select(s => Guid.Parse(s)).ToList();
                        foreach (var item in user.PositionUsers.Where(s => !s.IsDeleted))
                            if (!listPositionIds.Contains(item.Id))
                                unitOfWork.PositionUserRepository.Delete(item);

                        foreach (var newItem in listPositionIds)
                        {
                            if (!user.PositionUsers.Where(s => !s.IsDeleted).Select(s => s.PositionId).ToList().Contains(newItem))
                            {
                                var newRole = new PositionUser
                                {
                                    UserId= model.Id,
                                    PositionId = newItem
                                };
                                unitOfWork.PositionUserRepository.Add(newRole);
                            }
                        }
                    }
                    #endregion

                    #region Edit Roles
                    if (model.Roles == null)
                        foreach (var item in user.UserRoles.Where(s => !s.IsDeleted))
                            unitOfWork.UserRoleRepository.Delete(item);
                    else
                    {
                        var listActionIds = model.Roles.Select(s => Guid.Parse(s)).ToList();
                        foreach (var item in user.UserRoles.Where(s => !s.IsDeleted))
                        {
                            if (!listActionIds.Contains(item.Id))
                                unitOfWork.UserRoleRepository.Delete(item);
                        }

                        foreach (var newItem in listActionIds)
                        {
                            if (!user.UserRoles.Where(s => !s.IsDeleted).Select(s => s.RoleId).ToList().Contains(newItem))
                            {
                                var newRole = new UserRole
                                {
                                    UserId = model.Id,
                                    RoleId = newItem
                                };
                                unitOfWork.UserRoleRepository.Add(newRole);
                            }
                        }
                    }
                    #endregion

                    #region Edit Specialty
                    if (model.Specialties == null)
                        foreach (var item in user.UserSpecialties.Where(s => !s.IsDeleted))
                            unitOfWork.UserSpecialtyRepository.Delete(item);
                    else
                    {
                        var listSpecialtyIds = model.Specialties.Select(s => Guid.Parse(s)).ToList();
                        foreach (var item in user.UserSpecialties.Where(s => !s.IsDeleted))
                        {
                            if (!listSpecialtyIds.Contains(item.Id))
                                unitOfWork.UserSpecialtyRepository.Delete(item);
                        }

                        foreach (var newItem in listSpecialtyIds)
                        {
                            if (!user.UserSpecialties.Where(s => !s.IsDeleted).Select(s => s.SpecialtyId).ToList().Contains(newItem))
                            {
                                var newRole = new UserSpecialty
                                {
                                    UserId = model.Id,
                                    SpecialtyId = newItem
                                };
                                unitOfWork.UserSpecialtyRepository.Add(newRole);
                            }
                        }
                    }
                    #endregion
                    
                    #region Edit Admin Roles
                    if (model.AdminRoles == null)
                        foreach (var item in user.UserAdminRoles.Where(s => !s.IsDeleted))
                            unitOfWork.UserAdminRoleRepository.Delete(item);
                    else
                    {
                        var listActionIds = model.AdminRoles.Select(s => int.Parse(s)).ToList();
                        foreach (var item in user.UserAdminRoles.Where(s => !s.IsDeleted))
                        {
                            if (!listActionIds.Contains(item.AdminRoleId))
                                unitOfWork.UserAdminRoleRepository.Delete(item);
                        }

                        foreach (var newItem in listActionIds)
                        {
                            if (!user.UserAdminRoles.Where(s => !s.IsDeleted).Select(s => s.AdminRoleId).ToList().Contains(newItem))
                            {
                                var newRole = new UserAdminRole
                                {
                                    UserId = model.Id,
                                    AdminRoleId = newItem
                                };
                                unitOfWork.UserAdminRoleRepository.Add(newRole);
                            }
                        }
                    }
                    #endregion

                    user.Session = null;
                    user.SessionId = null;
                    unitOfWork.Commit();
                    return Json(true);
                }
                return Json(false);
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }

        private ADUserDetailModel GetUserADInfo(string userName, string domainName = "vingroup.local")
        {
            try
            {
                PrincipalContext domainContext = new PrincipalContext(ContextType.Domain, domainName);
                UserPrincipal userPrincipal = new UserPrincipal(domainContext);
                userPrincipal.SamAccountName = userName;
                PrincipalSearcher principleSearch = new PrincipalSearcher();
                principleSearch.QueryFilter = userPrincipal;
                PrincipalSearchResult<Principal> results = principleSearch.FindAll();
                Principal principle = results.ToList()[0];
                DirectoryEntry directory = (DirectoryEntry)principle.GetUnderlyingObject();
                return ADUserDetailModel.GetUser(directory);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private bool IsADSpam(string ip)
        {
            string ad_ip = $"AD{ip}";
            var time = DateTime.Now.AddMinutes(-2);
            var login_fail = unitOfWork.LogInFailRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                !string.IsNullOrEmpty(e.IPAddress) &&
                e.IPAddress == ad_ip &&
                e.Time >= 10 &&
                e.CreatedAt > time 
            );
            //if (login_fail != null)
            //{
            //    var end = DateTime.Now;
            //    if (login_fail.UpdatedAt?.AddMinutes(2) <= end)
            //    {
            //        login_fail.Time = 0;
            //        unitOfWork.LogInFailRepository.Update(login_fail);
            //        unitOfWork.Commit();
            //        return false;
            //    }
            //    return true;
            //}
            return login_fail != null;
        }

        private void IncreaseADSpam(string ip)
        {
            string ad_ip = $"AD{ip}";
            var time = DateTime.Now.AddMinutes(-2);
            var login_fail = unitOfWork.LogInFailRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                !string.IsNullOrEmpty(e.IPAddress) &&
                e.IPAddress == ad_ip
            );
            if (login_fail != null)
            {
                if(login_fail.CreatedAt < time)
                {
                    login_fail.Time = 1;
                    login_fail.CreatedAt = DateTime.Now;
                }
                else
                {
                    login_fail.Time += 1;
                    unitOfWork.LogInFailRepository.Update(login_fail);
                }
            }
            else
            {
                var new_login_fail = new LogInFail()
                {
                    IPAddress = ad_ip,
                    Time = 1,
                };
                unitOfWork.LogInFailRepository.Add(new_login_fail);
            }
            unitOfWork.Commit();
        }
    }
}