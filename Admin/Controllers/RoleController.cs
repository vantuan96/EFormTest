using Admin.Common;
using Admin.Common.Model;
using Admin.CustomAuthen;
using Admin.Models;
using DataAccess.Models;
using DataAccess.Repository;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Linq.Dynamic;

namespace Admin.Controllers
{
    [Authorize(Roles = Constant.AdminRoles.SuperAdmin + "," + Constant.AdminRoles.ManageRole)]
    public class RoleController : Controller
    {
        protected IUnitOfWork unitOfWork = new EfUnitOfWork();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetListRoles(DataTablesQueryModel queryModel)
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

            var list = unitOfWork.RoleRepository.AsQueryable();

            totalResultsCount = list.Count();

            var result = list.OrderBy(sortBy + (sortDir == "desc" ? " descending" : "")).Skip(skip).Take(take).ToList()
                .Select(x => new RoleViewModel
                {
                    Id = x.Id,
                    ViName = x.ViName,
                    EnName = x.EnName,
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeactivateRole(Guid id)
        {
            try
            {
                var role = unitOfWork.RoleRepository.GetById(id);
                if (role == null)
                    return Json(false);
                else
                {
                    var users = unitOfWork.UserRoleRepository.Find(
                        e => !e.IsDeleted &&
                        e.UserId != null &&
                        e.RoleId != null &&
                        e.RoleId == id
                    ).Select(e=> e.User);
                    foreach (var user in users)
                    {
                        user.SessionId = null;
                        user.Session = null;
                    }
                    unitOfWork.RoleRepository.Delete(role);
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
        public ActionResult ActivateRole(Guid id)
        {
            try
            {
                var role = unitOfWork.RoleRepository.GetById(id);
                if (role == null)
                    return Json(false);
                else
                {
                    role.IsDeleted = false;
                    unitOfWork.Commit();
                }
                return Json(true);
            }
            catch (Exception)
            {
                return Json(false);
            }
        }

        public ActionResult GetRoleDetail(Guid? id)
        {
            if (id == null)
                return PartialView("_PartialViewModalRole", new RoleViewModel { IsEdit = false });
            else
            {
                var specialty = unitOfWork.RoleRepository.GetById(id.Value);
                if (specialty == null) return PartialView("_PartialViewModalRole", new RoleViewModel { IsEdit = false });
                return PartialView("_PartialViewModalRole", new RoleViewModel
                {
                    Id = specialty.Id,
                    ViName = specialty.ViName,
                    EnName = specialty.EnName,
                    VisitTypeGroupId = specialty.VisitTypeGroupId,
                    Actions = specialty.RoleActions.Where(s => !s.IsDeleted).Select(s => s.Action.Id).ToList().Select(s => s.ToString()).ToList(),
                    IsEdit = true
                });
            }
        }

        public ActionResult GetActionSpecialtyByVisit(Guid? id, Guid visitId)
        {
            if (id == null)
                return PartialView("_PartialViewSelectActionSpecialty", new RoleViewModel { VisitTypeGroupId = visitId });
            else
            {
                var specialty = unitOfWork.RoleRepository.GetById(id.Value);
                if (specialty == null)
                    return PartialView("_PartialViewSelectActionSpecialty", new RoleViewModel { VisitTypeGroupId = visitId });
                var model = new RoleViewModel
                {
                    VisitTypeGroupId = visitId,
                    Actions = specialty.RoleActions.Where(s => !s.IsDeleted).Select(s => s.Action.Id).ToList().Select(s => s.ToString()).ToList(),
                };
                return PartialView("_PartialViewSelectActionSpecialty", model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveRole(RoleViewModel model)
        {
            try
            {
                var role = unitOfWork.RoleRepository.FirstOrDefault(s => s.Id == model.Id);
                if (role != null)
                {
                    role.ViName = model.ViName;
                    role.EnName = model.EnName;

                    #region Edit Actions
                    if (model.Actions == null)
                    {
                        foreach (var item in role.RoleActions.Where(s => !s.IsDeleted))
                        {
                            unitOfWork.RoleActionRepository.Delete(item);
                        }
                    }
                    else
                    {
                        var listActionIds = model.Actions.Select(s => Guid.Parse(s)).ToList();
                        foreach (var item in role.RoleActions.Where(s => !s.IsDeleted))
                        {
                            if (!listActionIds.Contains(item.Id))
                                unitOfWork.RoleActionRepository.Delete(item);
                        }

                        foreach (var newItem in listActionIds)
                        {
                            if (!role.RoleActions.Where(s => !s.IsDeleted).Select(s => s.ActionId).ToList().Contains(newItem))
                            {
                                var newRoleAction = new RoleAction
                                {
                                    RoleId = model.Id,
                                    ActionId = newItem
                                };
                                unitOfWork.RoleActionRepository.Add(newRoleAction);
                            }
                        }
                    }
                    #endregion

                    #region remove user session
                    //var users = unitOfWork.UserRoleRepository.Find(
                    //    e => !e.IsDeleted &&
                    //    e.UserId != null &&
                    //    e.RoleId != null &&
                    //    e.RoleId == model.Id
                    //).Select(e => e.User);
                    
                    //foreach (var user in users)
                    //{
                    //    user.SessionId = null;
                    //    user.Session = null;
                    //}
                    #endregion

                    unitOfWork.Commit();
                    return Json(true);
                }
                else
                {
                    var newRoleId = Guid.NewGuid();
                    var newRole = new Role
                    {
                        Id = newRoleId,
                        ViName = model.ViName,
                        EnName = model.ViName,
                        VisitTypeGroupId = model.VisitTypeGroupId
                    };
                    unitOfWork.RoleRepository.Add(newRole);

                    #region Add new Actions
                    if (model.Actions != null)
                    {
                        var listActionIds = model.Actions.Select(s => Guid.Parse(s)).ToList();
                        foreach (var item in listActionIds)
                        {
                            var newRoleAction = new RoleAction
                            {
                                RoleId = newRoleId,
                                ActionId = item
                            };
                            unitOfWork.RoleActionRepository.Add(newRoleAction);
                        }
                    }
                    #endregion

                    unitOfWork.Commit();
                    return Json(true);
                }
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }
    }
}