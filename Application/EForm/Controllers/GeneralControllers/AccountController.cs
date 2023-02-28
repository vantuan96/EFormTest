using Common;
using DataAccess.Models;
using DataAccess.Repository;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Common;
using EForm.Helper;
using EForm.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace EForm.Controllers.GeneralControllers
{
    public class AccountController : BaseApiController
    {
        [HttpGet]
        [Route("api/Account/ShowCaptcha")]
        public IHttpActionResult ShowCaptcha()
        {
            var ip = GetIp();
            return Content(HttpStatusCode.OK, IsShowCaptcha(ip));
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/Account/Login")]
        public IHttpActionResult LoginAPI([FromBody] LoginParameterModel request)
        {
            if (!request.Validate())
                return Content(HttpStatusCode.BadRequest, Message.LOGIN_ERROR);

            var ip = GetIp();
            var is_show_captcha = IsShowCaptcha(ip);
            if (is_show_captcha)
            {
                if (string.IsNullOrEmpty(request.captcha) || !Recaptcha.IsReCaptchValid(request.captcha))
                {
                    IncreaseLoginFail(ip);
                    return Content(HttpStatusCode.BadRequest, Message.LOGIN_ERROR);
                }
            }

            var user = ValidateUser(request.username, request.password);
            if (user == null)
            {
                IncreaseLoginFail(ip);
                return Content(HttpStatusCode.BadRequest, Message.LOGIN_ERROR);
            }
            if (user.IsLocked)
            {
                var msg = GetAppConfig("LOCKUSER_MSG");
                return Content(HttpStatusCode.BadRequest, new { ViMessage = msg, EnMessage = msg });
            }
            ClaimsIdentity identity = CreateIdentity(user);
            Request.GetOwinContext().Authentication.SignIn(identity);
            var user1 = Request.GetOwinContext().Authentication.User;
            //var m = Request.GetOwinContext().Response.Cookies["ApplicationCookie"];
            //var m1 = Request.Cookies[".AspNet.ApplicationCookie"]; ;
            var m1 = HttpContext.Current.Request.Cookies["ApplicationCookie"];
            SetZeroLoginFail(ip);
            return Content(HttpStatusCode.OK, new { Token = "a" });
        }

        [HttpGet]
        [Route("api/Account/Logout")]
        public async Task<IHttpActionResult> GetLogout()
        {

            var resp = new HttpResponseMessage();
            try
            {
                CookieHeaderValue cookie = Request.Headers.GetCookies("EFormED").FirstOrDefault();
                if (cookie != null)
                {
                    RemoveSession(cookie["EFormED"].Value);
                    var new_cookie = new CookieHeaderValue("EFormED", "")
                    {
                        Expires = DateTime.Now.AddDays(-1),
                        Domain = cookie.Domain,
                        Path = cookie.Path
                    };
                    resp.Headers.AddCookies(new[] { new_cookie });
                }
            }
            catch (Exception) { }
            try
            {
                var form_token = Request.Headers.GetValues("RequestVerificationToken").First();
                var cookie_token = Request.Headers.GetCookies("__RequestVerificationToken").LastOrDefault();
                var csrf_token = string.Format("{0}{1}", form_token, cookie_token["__RequestVerificationToken"].Value);
                var new_cookie_token = new CookieHeaderValue("__RequestVerificationToken", "")
                {
                    Expires = DateTime.Now.AddDays(-1),
                    Domain = cookie_token.Domain,
                    Path = cookie_token.Path
                };
                resp.Headers.AddCookies(new[] { new_cookie_token });
            }
            catch (Exception) { }
            resp.StatusCode = HttpStatusCode.OK;
            var cookie1 = new HttpCookie("EFormED");
            DateTime nowDateTime = DateTime.Now;
            cookie1.Expires = nowDateTime.AddSeconds(-1);
            HttpContext.Current.Response.Cookies.Add(cookie1);
            var t = HttpContext.Current.Request.Cookies["EFormED"];
            var _CookieCheckLogin = new HttpCookie("_CookieCheckLogin");
            DateTime nowDateTim1 = DateTime.Now;
            _CookieCheckLogin.Expires = nowDateTim1.AddSeconds(-1);
            HttpContext.Current.Response.Cookies.Add(_CookieCheckLogin);
            var value = HttpContext.Current.Request.Cookies["_CookieCheckLogin"];
            var userClaims = User.Identity as System.Security.Claims.ClaimsIdentity;
            SetIsLogin(userClaims.Name);
            //Startup.IsLogin = 1;

            return Content(HttpStatusCode.OK, new { url = "Authen/Login" });

        }

        [HttpGet]
        [Route("api/Account/LogoutBase")]
        public async Task<IHttpActionResult> LogOutApi(string email)
        {

            string username = string.Empty;
            if (!string.IsNullOrEmpty(email))
            {
                username = email.Split('@')[0].Trim();
            };

            using (var unitOfWork = new EfUnitOfWork())
            {
                var user = unitOfWork.UserRepository.FirstOrDefault(m => !m.IsDeleted && m.Username == username);
                user.SessionId = null;
                user.Session = null;
                unitOfWork.UserRepository.Update(user);
                unitOfWork.Commit();
            }
            return null;

        }



        public async Task<string> SetIsLogin(string email)
        {
            var uri = "api/ManageAppBase/SetIsLogin?email=" + email;
            var value = await ApiHelper.HttpGet(uri, "2krojMdNQkSpZzwybnoR6g==");
            var response = value.Content.ReadAsStringAsync().Result;
            return response;
        }
        [HttpGet]
        [Route("api/Account/LogoutAz")]
        public async Task<IHttpActionResult> Logout()
        {

            var resp = new HttpResponseMessage();
            try
            {
                CookieHeaderValue cookie = Request.Headers.GetCookies("EFormED").FirstOrDefault();
                if (cookie != null)
                {
                    RemoveSession(cookie["EFormED"].Value);
                    var new_cookie = new CookieHeaderValue("EFormED", "")
                    {
                        Expires = DateTime.Now.AddDays(-1),
                        Domain = cookie.Domain,
                        Path = cookie.Path
                    };
                    resp.Headers.AddCookies(new[] { new_cookie });
                }
            }
            catch (Exception) { }
            try
            {
                var form_token = Request.Headers.GetValues("RequestVerificationToken").First();
                var cookie_token = Request.Headers.GetCookies("__RequestVerificationToken").LastOrDefault();
                var csrf_token = string.Format("{0}{1}", form_token, cookie_token["__RequestVerificationToken"].Value);
                var new_cookie_token = new CookieHeaderValue("__RequestVerificationToken", "")
                {
                    Expires = DateTime.Now.AddDays(-1),
                    Domain = cookie_token.Domain,
                    Path = cookie_token.Path
                };
                resp.Headers.AddCookies(new[] { new_cookie_token });
            }
            catch (Exception) { }
            //Request.GetOwinContext().Authentication.SignOut();

            resp.StatusCode = HttpStatusCode.OK;

            var cookie1 = new HttpCookie("EFormED");
            DateTime nowDateTime = DateTime.Now;
            cookie1.Expires = nowDateTime.AddSeconds(-1);
            HttpContext.Current.Response.Cookies.Add(cookie1);
            var value = HttpContext.Current.Request.Cookies["EFormED"];


            var _CookieCheckLogin = new HttpCookie("_CookieCheckLogin");
            DateTime nowDateTim1 = DateTime.Now;
            _CookieCheckLogin.Expires = nowDateTim1.AddSeconds(-1);
            HttpContext.Current.Response.Cookies.Add(_CookieCheckLogin);
            var value1 = HttpContext.Current.Request.Cookies["_CookieCheckLogin"];
            var userClaims = User.Identity as System.Security.Claims.ClaimsIdentity;
            await SetIsLogin(userClaims.Name);
            Startup.IsLogin = 1;
            return Content(HttpStatusCode.OK, new { url = "Authen/SignOut" });

        }
        private bool IsShowCaptcha(string ip)
        {
            if (ConfigurationManager.AppSettings["HiddenError"].Equals("false"))
                return false;
            try
            {
                var login_failed = unitOfWork.LogInFailRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    !string.IsNullOrEmpty(e.IPAddress) &&
                    e.IPAddress == ip
                );
                if (login_failed != null)
                    return login_failed.Time > Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["NumberShowCaptCha"]);
                return false;
            }
            catch (Exception)
            {
                return false;
            }

        }

        private ClaimsIdentity CreateIdentity(User user)
        {
            string username = string.IsNullOrEmpty(user.Username) ? "" : user.Username;
            string roles = string.IsNullOrEmpty(user.Roles) ? "" : user.Roles;
            var user_positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName).ToArray();
            var positions = user.PositionUsers == null ? "" : string.Join(",", user_positions);
            string role = "";
            var list_actions = GetListObjAction(user.Id);

            //var current_roles = user.UserRoles.Where(e => !e.IsDeleted).ToList();
            //var actions = new List<string>();
            //if (current_roles.Count > 0)
            //    foreach (var ro in current_roles)
            //        actions.AddRange(ro.Role.RoleActions.Where(e => !e.IsDeleted).Select(r => r.Action.Code).ToList());
            //actions = actions.Distinct().ToList();
            role = string.Join(",", list_actions);

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

        private User ValidateUser(string username, string password)
        {
            string devAccounts = ConfigurationManager.AppSettings.Get("DEV_ACCOUNT");
            if (ConfigurationManager.AppSettings["HiddenError"].Equals("false") && devAccounts.Contains(username))
                return unitOfWork.UserRepository.FirstOrDefault(s => s.Username.Equals(username));

            bool isValidADAccount = LoginAdAccount(username, password);

            if (true)
            {
                var userData = unitOfWork.UserRepository.FirstOrDefault(s => s.Username.Trim().ToLower().Equals(username.Trim().ToLower()));
                if (userData != null)
                {
                    return AdAccountSynchronous(username, userData);
                }
                else
                {
                    var log = new
                    {
                        URI = "api/Account/Login",
                        Request = JsonConvert.SerializeObject(new { Usernam = username, Password = "******" }).ToString(),
                        Response = "Validate AD success, user is not exists in db",
                        Action = "Error"
                    };
                    Common.CustomLog.accesslog.Error(log);
                }
                return userData;
            }
            else
            {
                var log = new
                {
                    URI = "api/Account/Login",
                    Request = JsonConvert.SerializeObject(new { Usernam = username, Password = "******" }).ToString(),
                    Response = "Validate AD fail",
                    Action = "Error"
                };
                Common.CustomLog.accesslog.Error(log);
                return null;
            }
        }
        private User AdAccountSynchronous(string userName, User user)
        {
            var adUser = GetUserADInfo(userName);
            user.FirstName = adUser.FirstName;
            user.LastName = adUser.LastName;
            user.Fullname = adUser.FullName;
            user.DisplayName = adUser.DisplayName;
            user.LoginNameWithDomain = adUser.LoginNameWithDomain;
            user.Mobile = adUser.Mobile;
            user.EmailAddress = adUser.EmailAddress;
            user.Department = adUser.Department;
            user.Title = adUser.Title;
            user.Description = adUser.Description;
            user.Company = adUser.Company;
            user.ManagerName = adUser.ManagerName;
            if (adUser.Manager != null)
            {
                user.ManagerId = adUser.Manager.UserId;
            }
            user.Username = userName.Trim().ToLower();
            user.Roles = "Authorized";
            unitOfWork.UserRepository.Update(user);
            unitOfWork.Commit();
            return user;
        }
        private ADUserDetailModel GetUserADInfo(string userName, string domainName = "vingroup.local")
        {
            PrincipalContext domainContext = new PrincipalContext(ContextType.Domain, domainName);
            UserPrincipal userPrincipal = new UserPrincipal(domainContext);
            userPrincipal.SamAccountName = userName;
            PrincipalSearcher principleSearch = new PrincipalSearcher();
            principleSearch.QueryFilter = userPrincipal;
            PrincipalSearchResult<Principal> results = principleSearch.FindAll();
            Principal principle = results.ToList()[0];
            DirectoryEntry directory = (DirectoryEntry)principle.GetUnderlyingObject();
            principleSearch.Dispose();
            return ADUserDetailModel.GetUser(directory);
        }
        private bool LoginAdAccount(string userName, string password)
        {
            bool isValidAdAccount = false;
            using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
            {
                isValidAdAccount = context.ValidateCredentials(userName, password, ContextOptions.Negotiate);
            }
            return isValidAdAccount;
        }
        private void IncreaseLoginFail(string ip)
        {
            var login_fail = unitOfWork.LogInFailRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                !string.IsNullOrEmpty(e.IPAddress)
            );
            if (login_fail != null)
            {
                login_fail.Time += 1;
                unitOfWork.LogInFailRepository.Update(login_fail);
            }
            else
            {
                var new_login_fail = new LogInFail()
                {
                    IPAddress = ip,
                    Time = 1,
                };
                unitOfWork.LogInFailRepository.Add(new_login_fail);
            }
            unitOfWork.Commit();
        }
        private void SetZeroLoginFail(string ip)
        {
            var login_fail = unitOfWork.LogInFailRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                !string.IsNullOrEmpty(e.IPAddress) &&
                e.IPAddress == ip
            );
            if (login_fail != null)
            {
                login_fail.Time = 0;
                unitOfWork.LogInFailRepository.Update(login_fail);
                unitOfWork.Commit();
            }
        }
        private void RemoveSession(string session)
        {
            var session_id = session.Substring(0, 20);
            var user = unitOfWork.UserRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                !string.IsNullOrEmpty(e.SessionId) &&
                e.SessionId == session_id
            );
            if (user != null)
            {
                user.Session = null;
                user.SessionId = null;
                unitOfWork.UserRepository.Update(user);
                unitOfWork.Commit();
            }
        }
        [HttpGet]
        [Route("api/Account/GetManageApp")]
        public async Task<string> GetManageAppApi()
        {
            var appid = ConfigurationManager.AppSettings["AppId"].ToString();
            var uri = "api/ManageAppBase/GetListManageApp?appid=" + appid;
            var value = await ApiHelper.HttpGet(uri, "2krojMdNQkSpZzwybnoR6g==");
            var response = value.Content.ReadAsStringAsync().Result;
            return response;
        }

    }
}
