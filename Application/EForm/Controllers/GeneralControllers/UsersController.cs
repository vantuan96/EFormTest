using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using DataAccess.Models;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Common;
using EForm.Models;
using Newtonsoft.Json.Linq;

namespace EForm.Controllers.GeneralControllers
{
    [SessionAuthorize]
    public class UsersController : BaseApiController
    {
        [HttpGet]
        [Route("api/Users")]
        [Permission(Code = "GUSER1")]
        public IHttpActionResult GetUsersAPI([FromUri] UserParameterModel request)
        {
            if (!string.IsNullOrEmpty(request.Usernames))
                return Content(HttpStatusCode.OK, SearchUserByUserNames(request.Usernames.Split(',').ToList()));
            if (!string.IsNullOrEmpty(request.Username))
                return Content(HttpStatusCode.OK, SearchUserByUserName(request.Username));
            if (!string.IsNullOrEmpty(request.Position) && request.SpecialtyId != null)
                return Content(HttpStatusCode.OK, SearchUserByPositionSpecicaltyAndKeyword(request.Position, request.SpecialtyId, request.Search));

            if (!string.IsNullOrEmpty(request.Position))
                return Content(HttpStatusCode.OK, SearchUserByPositionAndKeyword(request.Position, request.Search));

            if (request.SpecialtyId != null)
                return Content(HttpStatusCode.OK, SearchUserBySpecicaltyAndKeyword(request.SpecialtyId, request.Search));

            if (request.Id != null)
                return Content(HttpStatusCode.OK, SearchUserById((Guid)request.Id));

            return Content(HttpStatusCode.OK, SearchUserByKeyword(request.Search));
        }

        [HttpGet]
        [Route("api/User")]
        [Permission(Code = "GUSER2")]
        public IHttpActionResult GetUserAPI()
        {
            var user = GetUser();
            var actions = GetListObjAction();
            var sites = GetListObjSite(user.Id);
            var site = GetSite();
            var specialty = GetSpecialty();
            var user_position = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => new { e.Id, e.Position.ViName, e.Position.EnName });
            var positions = unitOfWork.PositionRepository.Find(e => !e.IsDeleted).Select(e => new { e.Id, e.ViName, e.EnName });
            var app_version = getAppCurrentVersion();
            return Content(HttpStatusCode.OK, new
            {
                user.Username,
                user.Fullname,
                SiteId = user.CurrentSiteId?.ToString(),
                SpecialtyId = user.CurrentSpecialtyId?.ToString(),
                SpecialtyName = string.Format("{0} ({1} - {2})", specialty?.Site.Name, specialty?.VisitTypeGroup.Code, specialty?.ViName),
                specialty?.IsAnesthesia,
                Position = user_position,
                Positions = positions,
                Role = new { Datas = actions },
                Site = new { site?.Id, site?.Code, site?.Location, site?.Name, site?.Province, site?.ApiCode },
                Sites = sites,
                Maintain = ConfigurationManager.AppSettings["Maintain"],
                AppVersion = new
                {
                    app_version.Version,
                    app_version.Lable
                }
            });
        }

        [HttpGet]
        [Route("api/User/Info")]
        [Permission(Code = "GUSER3")]
        public IHttpActionResult GetUserInfoAPI([FromUri] string username, string type = "AD")
        {
            if (!string.IsNullOrEmpty(username))
            {
                if (type.Equals("EHOS"))
                {
                    User user = unitOfWork.UserRepository.FirstOrDefault(
                        e => !e.IsDeleted &&
                        !string.IsNullOrEmpty(e.EHOSAccount) &&
                        e.EHOSAccount.Equals(username)
                    );
                    if (user != null)
                        return Content(HttpStatusCode.OK, GetUserInfo(user));
                }
                else
                {
                    User user = unitOfWork.UserRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    !string.IsNullOrEmpty(e.Username) &&
                    e.Username.Equals(username)
                );
                    if (user != null)
                        return Content(HttpStatusCode.OK, GetUserInfo(user));
                }
            }
            return Content(HttpStatusCode.BadRequest, Message.USER_NOT_FOUND);
        }

        [HttpGet]
        [Route("api/User/InfoFormAD")]
        public IHttpActionResult GetUserInfoFromAD([FromUri] string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                try
                {
                    PrincipalContext domainContext = new PrincipalContext(ContextType.Domain, "vingroup.local");
                    UserPrincipal userPrincipal = new UserPrincipal(domainContext);
                    userPrincipal.SamAccountName = username;
                    PrincipalSearcher principleSearch = new PrincipalSearcher();
                    principleSearch.QueryFilter = userPrincipal;
                    PrincipalSearchResult<Principal> results = principleSearch.FindAll();
                    Principal principle = results.ToList()[0];
                    DirectoryEntry directory = (DirectoryEntry)principle.GetUnderlyingObject();
                    ADUserDetailModel user = ADUserDetailModel.GetUser(directory);

                    if (user != null)
                    {
                        List<UserModel> users = new List<UserModel>();
                        users.Add(new UserModel
                        {
                            Username = user.UserId,
                            Fullname = user.FullName,
                            FullShortName = user.LoginName,
                            Department = user.Department,
                            Title = user.Title,
                            Mobile = user.Mobile
                        });
                        if (unitOfWork.UserRepository.Find(e => e.Username == username).Count() == 0)
                        {
                            var newUser = new User
                            {
                                Id = Guid.NewGuid(),
                                Username = username.Trim().ToLower(),
                                DisplayName = user.DisplayName,
                                Department = user.Department,
                                Title = user.Title,
                                Fullname = user.FullName
                            };
                            unitOfWork.UserRepository.Add(newUser);
                            unitOfWork.Commit();
                        }
                        return Content(HttpStatusCode.OK, users);
                    }
                    else
                    {
                        return Content(HttpStatusCode.BadRequest, Message.USER_NOT_FOUND);
                    }
                }
                catch
                {
                    return Content(HttpStatusCode.BadRequest, Message.USER_NOT_FOUND);
                }
            }

            return Content(HttpStatusCode.BadRequest, Message.USER_NOT_FOUND);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/User/ChooseSite")]
        [Permission(Code = "GUSER4")]
        public IHttpActionResult ChooseSiteAPI([FromBody] JObject request)
        {
            var user = GetUser();
            var specialty_id = new Guid(request["SpecialtyId"]?.ToString());
            var specialties = GetListIdSpecialty(user.Id);

            if (specialty_id != null && specialty_id != user.CurrentSpecialtyId && specialties.Contains(specialty_id))
            {
                var site = unitOfWork.SpecialtyRepository.GetById(specialty_id)?.Site;
                user.Site = site;
                user.CurrentSpecialtyId = specialty_id;
                unitOfWork.UserRepository.Update(user);
                unitOfWork.Commit();

                ClaimsIdentity identity = CreateIdentity(user);
                Request.GetOwinContext().Authentication.SignIn(identity);
            }
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }
        private List<SiteSpecialtyModel> GetListObjSite(Guid user_id)
        {
            //var specialties = unitOfWork.UserSpecialtyRepository.Find(
            //    e => !e.IsDeleted &&
            //    e.UserId != null &&
            //    e.UserId == user_id &&
            //    e.SpecialtyId != null
            //).Select(e => new {
            //    e.Specialty.Id,
            //    e.Specialty.ViName,
            //    e.Specialty.EnName,
            //    e.Specialty.SiteId,
            //    SiteName = e.Specialty.Site.Name,
            //    e.Specialty.VisitTypeGroupId,
            //    VisitTypeGroupCode = e.Specialty.VisitTypeGroup.Code,
            //}).ToList()


            var specialties = (from us in unitOfWork.UserSpecialtyRepository.AsQueryable()
                               join sp in unitOfWork.SpecialtyRepository.AsQueryable() on us.SpecialtyId equals sp.Id
                               join si in unitOfWork.SiteRepository.AsQueryable() on sp.SiteId equals si.Id
                               join vg in unitOfWork.VisitTypeGroupRepository.AsQueryable() on sp.VisitTypeGroupId equals vg.Id
                               where
                                  !us.IsDeleted &&
                                  !sp.IsDeleted &&
                                  us.SpecialtyId != null &&
                                  us.UserId == user_id
                               select new
                               {
                                   sp.Id,
                                   sp.ViName,
                                   sp.EnName,
                                   sp.VisitTypeGroupId,
                                   SiteName = si.Name,
                                   sp.SiteId,
                                   VisitTypeGroupCode = vg.Code
                               }).ToList().Distinct().OrderBy(e => e.SiteName).ThenBy(e => e.VisitTypeGroupCode).ThenBy(e => e.ViName);

            List<SiteSpecialtyModel> site_specs = new List<SiteSpecialtyModel>();
            foreach (var spec in specialties)
            {
                var site = site_specs.FirstOrDefault(e => e.Name == spec.SiteName);
                if (site == null)
                {
                    site = new SiteSpecialtyModel
                    {
                        Id = (Guid)spec.SiteId,
                        Name = spec.SiteName,
                        Specialities = new List<dynamic>(),
                    };
                    site_specs.Add(site);
                }
                site.Specialities.Add(spec);
            }
            return site_specs;
        }


        private dynamic GetListIdSpecialty(Guid user_id)
        {
            return unitOfWork.UserSpecialtyRepository.Find(
                e => !e.IsDeleted &&
                e.UserId != null &&
                e.UserId == user_id &&
                e.SpecialtyId != null
            ).Select(e => e.SpecialtyId).Distinct().ToList();
        }
        private ClaimsIdentity CreateIdentity(User user)
        {
            string username = string.IsNullOrEmpty(user.Username) ? "" : user.Username;

            var user_positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName).ToArray();
            var positions = user.PositionUsers == null ? "" : string.Join(",", user_positions);

            string roles = string.IsNullOrEmpty(user.Roles) ? "" : user.Roles;

            string role = "";
            var current_roles = GetListObjAction(user.Id);
            role = string.Join(",", current_roles);

            string spec_id = user.CurrentSpecialtyId != null ? user.CurrentSpecialtyId.ToString() : "";
            string role_id = user.CurrentRoleId != null ? user.CurrentRoleId.ToString() : "";
            string site_id = user.CurrentSiteId != null ? user.CurrentSiteId.ToString() : "";
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, roles),
                new Claim("Roles", role),
                new Claim("SpecialtyId", spec_id),
                new Claim("RoleId", role_id),
                new Claim("Site", site_id),
                new Claim("Positions", positions),
            };
            var identity = new ClaimsIdentity(claims, "ApplicationCookie");
            return identity;
        }

        private dynamic SearchUserByPositionSpecicaltyAndKeyword(string position, Guid? spec_id, string keyword)
        {
            var position_id = unitOfWork.PositionRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    !string.IsNullOrEmpty(e.EnName) &&
                    e.EnName == position
                ).Id;
            return (from u in unitOfWork.UserRepository.AsQueryable()
                    join pu in unitOfWork.PositionUserRepository.AsQueryable() on u.Id equals pu.UserId
                    join us in unitOfWork.UserSpecialtyRepository.AsQueryable() on u.Id equals us.UserId
                    where
                       !u.IsDeleted &&
                       !us.IsDeleted &&
                       !pu.IsDeleted &&
                       us.SpecialtyId != null &&
                       us.SpecialtyId == spec_id &&
                       pu.PositionId != null &&
                       pu.PositionId == position_id &&
                       ((!string.IsNullOrEmpty(u.Username) && u.Username.ToLower().Contains(keyword.ToLower())) ||
                        (!string.IsNullOrEmpty(u.Fullname) && u.Fullname.ToLower().Contains(keyword.ToLower())))
                    select new
                    {
                        u.Id,
                        Fullname = u.DisplayName,
                        FullShort = u.Fullname,
                        u.Username,
                        u.Title
                    }).Distinct().Take(10).ToList();
        }
        private dynamic SearchUserByPositionAndKeyword(string position, string keyword)
        {
            var position_id = unitOfWork.PositionRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    !string.IsNullOrEmpty(e.EnName) &&
                    e.EnName == position
                ).Id;
            return (from u in unitOfWork.UserRepository.AsQueryable()
                    join pu in unitOfWork.PositionUserRepository.AsQueryable() on u.Id equals pu.UserId
                    join us in unitOfWork.UserSpecialtyRepository.AsQueryable() on u.Id equals us.UserId
                    where
                       !u.IsDeleted &&
                       !us.IsDeleted &&
                       !pu.IsDeleted &&
                       pu.PositionId != null &&
                       pu.PositionId == position_id &&
                       ((!string.IsNullOrEmpty(u.Username) && u.Username.ToLower().Contains(keyword.ToLower())) ||
                        (!string.IsNullOrEmpty(u.Fullname) && u.Fullname.ToLower().Contains(keyword.ToLower())))
                    select new
                    {
                        u.Id,
                        Fullname = u.DisplayName,
                        FullShort = u.Fullname,
                        u.Username,
                        u.Title
                    }).Distinct().Take(10).ToList();
        }
        private dynamic SearchUserBySpecicaltyAndKeyword(Guid? spec_id, string keyword)
        {
            return (from u in unitOfWork.UserRepository.AsQueryable()
                    join pu in unitOfWork.PositionUserRepository.AsQueryable() on u.Id equals pu.UserId
                    join us in unitOfWork.UserSpecialtyRepository.AsQueryable() on u.Id equals us.UserId
                    where
                       !u.IsDeleted &&
                       !us.IsDeleted &&
                       !pu.IsDeleted &&
                       us.SpecialtyId != null &&
                       us.SpecialtyId == spec_id &&
                       ((!string.IsNullOrEmpty(u.Username) && u.Username.ToLower().Contains(keyword.ToLower())) ||
                        (!string.IsNullOrEmpty(u.Fullname) && u.Fullname.ToLower().Contains(keyword.ToLower())))
                    select new
                    {
                        u.Id,
                        Fullname = u.DisplayName,
                        FullShort = u.Fullname,
                        u.Username,
                        u.Title
                    }).Distinct().Take(10).ToList();
        }
        private dynamic SearchUserByKeyword(string keyword)
        {
            return (from u in unitOfWork.UserRepository.AsQueryable()
                    where
                       !u.IsDeleted &&
                       ((!string.IsNullOrEmpty(u.Username) && u.Username.ToLower().Contains(keyword.ToLower())) ||
                        (!string.IsNullOrEmpty(u.Fullname) && u.Fullname.ToLower().Contains(keyword.ToLower())))
                    select new
                    {
                        u.Id,
                        Fullname = u.DisplayName,
                        FullShort = u.Fullname,
                        u.Username,
                        u.Title
                    }).Distinct().Take(10).ToList();
        }
        private dynamic SearchUserByUserName(string username)
        {
            return (from u in unitOfWork.UserRepository.AsQueryable()
                    where
                       !u.IsDeleted && u.Username == username

                    select new
                    {
                        u.Id,
                        Fullname = u.DisplayName,
                        FullShort = u.Fullname,
                        u.Username,
                        u.Title
                    }).Distinct().Take(10).ToList();
        }
        private dynamic SearchUserByUserNames(List<string> usernames)
        {
            return (from u in unitOfWork.UserRepository.AsQueryable()
                    where
                       !u.IsDeleted && usernames.Contains(u.Username)

                    select new
                    {
                        u.Id,
                        Fullname = u.DisplayName,
                        FullShort = u.Fullname,
                        u.Username,
                        u.Title
                    }).Distinct().Take(10).ToList();
        }
        private dynamic SearchUserById(Guid id)
        {
            return (from u in unitOfWork.UserRepository.AsQueryable()
                    where
                       !u.IsDeleted && u.Id == id

                    select new
                    {
                        u.Id,
                        Fullname = u.DisplayName,
                        FullShort = u.Fullname,
                        u.Username,
                        u.Title
                    }).Distinct().Take(10).ToList();
        }
    }
}