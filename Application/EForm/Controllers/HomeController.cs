
using Bussiness.IPD;
using Common;
using DataAccess.Models.GeneralModel;
using DataAccess.Repository;
using EForm.Cached;
using EForm.Common;
using EForm.Helper;
using EForm.Utils;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EForm.Controllers
{

    public class HomeController : BaseController
    {


        protected IUnitOfWork unitOfWork = new EfUnitOfWork();
        private string _tentantId = ConfigurationManager.AppSettings["AzureTenantId"].ToString();
        private string _clientId = ConfigurationManager.AppSettings["AzureClientId"].ToString();
        private string _Authority = ConfigurationManager.AppSettings["AzureAuthority"].ToString();
        private string _ClientSecret = ConfigurationManager.AppSettings["ClientSecret"].ToString();
        private string _redirectUrl = ConfigurationManager.AppSettings["AzureRedirect"];
        //[Authorize]
        public async Task<ActionResult> Index()
        {

            try
            {

                var form_token = Request.Headers.GetValues("RequestVerificationToken").First();
                var cookie_token = Request.Cookies["__RequestVerificationToken"].ToString();
                var csrf_token = string.Format("{0}{1}", form_token, cookie_token);

            }
            catch { }
            ViewBag.Title = "Home Page";
            var token = CSRFToken.Generate();
            ViewBag.CSRFToken = token.Substring(0, 60);
            HttpCookie cookie = new HttpCookie("__RequestVerificationToken", token.Substring(60));
            cookie.Expires = DateTime.Now.AddMinutes(1440);
            Response.Cookies.Add(cookie);
            var userClaims = User.Identity as System.Security.Claims.ClaimsIdentity;
            await SetAccessToken(userClaims.Name);
            return View();
        }

        private async Task<string> SetAccessToken(string email)
        {
            var Uid = Session["Uid"];

            var userClaims = User.Identity as System.Security.Claims.ClaimsIdentity;
            string accessToken = Session["AccessToken"] == null ? "" : Session["AccessToken"].ToString();
            //var m1 = Startup.Unique;
            Session["Email"] = userClaims.Name;
            Session["SessionId"] = Startup.sessionId;
            string sessionId = Startup.sessionId;
            var identity = new Dictionary<string, string>();
            identity.Add("Email", email);
            identity.Add("AccessToken", accessToken);
            identity.Add("SessionId", sessionId);
            identity.Add("CurrentDeviceId", Uid == null ? Startup.Unique : Uid.ToString());
            var uri = "api/ManageAppBase/SetAccessToken";
            var value = await ApiHelper.HttpPost(uri, identity, "2krojMdNQkSpZzwybnoR6g==");
            var response = value.Content.ReadAsStringAsync().Result;
            return response;
        }

        public ActionResult RedirectLogin(string Token)
        {

            ViewBag.token = Token;
            return View();
        }

        public ActionResult Logout()
        {
            HttpContext.GetOwinContext().Authentication.Challenge(
                   new AuthenticationProperties { RedirectUri = "/" },
                   OpenIdConnectAuthenticationDefaults.AuthenticationType);
            return null;
        }
        public ActionResult CheckLogin()
        {

            return View();
        }

        public ActionResult DoLogin()
        {

            var userClaims = User.Identity as System.Security.Claims.ClaimsIdentity;
            var result = userClaims.Name.Split('@');
            var st = result[0].ToString();
            var userData = unitOfWork.UserRepository.FirstOrDefault(s => !s.IsDeleted && s.Username.Trim().ToLower().Equals(st));
            if (userData == null)
            {
                string host = ConfigurationManager.AppSettings["redirecttoken"];
                string url = host + "#/404";
                System.Uri uri = new System.Uri(url);
                return Redirect(uri.ToString());
            }
            else
            {

                ClaimsIdentity identity = CreateIdentity(userData);
                Request.GetOwinContext().Authentication.SignIn(identity);
                HttpCookie cookie = new HttpCookie("_CookieCheckLogin", Guid.NewGuid().ToString());
                cookie.Expires = DateTime.Now.AddMinutes(1440);
                Response.Cookies.Add(cookie);
                return RedirectToAction("RedirectLogin", new { Token = "23442434" });

            }
        }

        public async Task<string> GetCurrentApp()
        {
            var appid = ConfigurationManager.AppSettings["AppId"].ToString();
            var uri = "api/ManageAppBase/CurrentApp?appid=" + appid;
            var value = await ApiHelper.HttpGet(uri, "2krojMdNQkSpZzwybnoR6g==");
            var response = value.Content.ReadAsStringAsync().Result;
            return response;
        }
        public async Task<string> GetListApp()
        {
            var appid = ConfigurationManager.AppSettings["AppId"].ToString();
            var uri = "api/ManageAppBase/GetListApp?appid=" + appid;
            var value = await ApiHelper.HttpGet(uri, "2krojMdNQkSpZzwybnoR6g==");
            var response = value.Content.ReadAsStringAsync().Result;
            return response;
        }
        public async Task<string> GetListAppForm()
        {
            var uri = "api/ManageAppBase/GetListManageApp";
            var value = await ApiHelper.HttpGet(uri, "2krojMdNQkSpZzwybnoR6g==");
            var response = value.Content.ReadAsStringAsync().Result;
            return response;
        }
        public async Task<string> GetListAppLogin()
        {
            var appid = ConfigurationManager.AppSettings["AppId"].ToString();
            var uri = "api/ManageAppBase/GetListAppForm?appid=" + appid;
            var value = await ApiHelper.HttpGet(uri, "2krojMdNQkSpZzwybnoR6g==");
            var response = value.Content.ReadAsStringAsync().Result;
            return response;
        }
        public string Login()
        {
            string urlAzureLogin = string.Concat(_Authority, _tentantId, "/oauth2/v2.0/authorize?client_id=", _clientId, "&scope=profile+email+openid&redirect_uri=", _redirectUrl, "&response_type=code&response_mode=form_post");

            return urlAzureLogin;
        }
        private ClaimsIdentity CreateIdentity(DataAccess.Models.User user)
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
      
        private List<string> GetListObjAction(Guid user_id)
        {
            var actions = (from user_role in unitOfWork.UserRoleRepository.AsQueryable()
                            .Where(
                                e => !e.IsDeleted &&
                                e.UserId != null &&
                                e.UserId == user_id &&
                                e.RoleId != null
                            )
                           join role_action in unitOfWork.RoleActionRepository.AsQueryable() on user_role.RoleId equals role_action.RoleId
                           where !role_action.IsDeleted
                           select role_action.Action.Code
                            ).Distinct().ToList();
            return actions;
        }
    }
}
