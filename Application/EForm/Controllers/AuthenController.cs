using DataAccess.Models;
using DataAccess.Repository;
using EForm.Helper;
using EForm.Models;
using EMRModels;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EForm.Controllers
{

    public class AuthenController : Controller
    {
        private static string AzureAuthority { get => "https://login.microsoftonline.com/"; }

        private static string Tenantid { get => ConfigurationManager.AppSettings.Get("ida:Tenant"); }

        private static string ClientId { get => ConfigurationManager.AppSettings.Get("ClientId"); }

        private static string Redirect1 { get => ConfigurationManager.AppSettings.Get("RedirectUri"); }
        protected IUnitOfWork unitOfWork = new EfUnitOfWork();
        // GET: Authen
        public ActionResult ReLogin()
        {
            HttpContext.GetOwinContext().Authentication.Challenge(
              new AuthenticationProperties { RedirectUri = "/" },
              OpenIdConnectAuthenticationDefaults.AuthenticationType);
            return null;
        }
        [HttpGet]
        public async Task<ActionResult> Login()
        {
            var email = Session["Email"];
            if (System.Web.HttpContext.Current.User.Identity.Name == "")
            {
                //HttpContext.GetOwinContext().Request.User.Identity.
                HttpContext.GetOwinContext().Authentication.Challenge(
              new AuthenticationProperties { RedirectUri = "/Authen/Dologin" },
              OpenIdConnectAuthenticationDefaults.AuthenticationType);
                return null;
            }
            else
            {
                //var deviceId = Startup.GenUniqueIdOnDevice();
                var response = "";
                var ok = Session["Uid"];
                if (Session["Uid"] != null)
                {
                    response = await GetListUserAsync(ok.ToString());
                }


                ViewBag.ListUser = unitOfWork.UserRepository.AsQueryable().ToList();
                List<UserInfo> json = JsonConvert.DeserializeObject<List<UserInfo>>(response);
                if (json == null)
                {
                    return Redirect(Startup.PostLogoutRedirectUri);
                }
                else
                {
                    ViewBag.User = json.Select(n => n.userName).ToList();
                }


                return View();

            }


        }


        public async Task<ActionResult> DoLogin()
        {

            var userClaims = User.Identity as System.Security.Claims.ClaimsIdentity;
            var result = userClaims.Name.Split('@');
            var st = result[0].ToString();
            var userData = unitOfWork.UserRepository.FirstOrDefault(s => !s.IsDeleted && s.Username.Trim().ToLower().Equals(st));

            if (userData == null)
            {
                //string host = ConfigurationManager.AppSettings["redirecttoken"];
                //string url = host + "#/404";
                //System.Uri uri = new System.Uri(url);
                return RedirectToAction("Login");
            }
            else
            {

                await SetAccessToken(userClaims.Name); 
                ClaimsIdentity identity = CreateIdentity(userData);
                Request.GetOwinContext().Authentication.SignIn(identity);
                HttpCookie cookie = new HttpCookie("_CookieCheckLogin", Guid.NewGuid().ToString());
                cookie.Expires = DateTime.Now.AddMinutes(1440);
                Response.Cookies.Add(cookie);
                return RedirectToAction("RedirectLogin", "Home", new { Token = "23442434" });

            }
        }

        public async Task<string> DoLoginLocal(string username)
        {

         
            var userData = unitOfWork.UserRepository.FirstOrDefault(s => !s.IsDeleted && s.Username.Trim().ToLower().Equals(username));
            var accesstoken = await GetAccessTokenFromBase(username);
            var strs = JsonConvert.DeserializeObject<dynamic>(accesstoken);
            //Validate acesstoken user
            var valid = ApiHelper.ValidateAccesstoken(strs);
            if (valid)
            {
                if (userData == null)
                {
                    //string host = ConfigurationManager.AppSettings["redirecttoken"];
                    //string url = host + "#/404";
                    //System.Uri uri = new System.Uri(url);
                    return "Login";
                }
                else
                {

                    await SetAccessToken(username);
                    ClaimsIdentity identity = CreateIdentity(userData);
                    Request.GetOwinContext().Authentication.SignIn(identity);
                    HttpCookie cookie = new HttpCookie("_CookieCheckLogin", Guid.NewGuid().ToString());
                    cookie.Expires = DateTime.Now.AddMinutes(1440);
                    Response.Cookies.Add(cookie);
                    return "/Home/RedirectLogin?Token=23442434";
                    //return RedirectToAction("RedirectLogin", "Home", new { Token = "23442434" });

                }
            }
            return null;
        }

        private async Task<string> GetListUserAsync(string deviceID)
        {
            var uri = "api/ManageAppBase/GetListUser?deviceID=" + deviceID;
            var value = await ApiHelper.HttpGet(uri, "2krojMdNQkSpZzwybnoR6g==");
            var response = value.Content.ReadAsStringAsync().Result;
            return response;
        }
        private async Task<string> GetAccessTokenFromBase(string username)
        {
            var uri = "api/ManageAppBase/GetAzureToken?email=" + username;
            var value = await ApiHelper.HttpGet(uri, "2krojMdNQkSpZzwybnoR6g==");
            var response = value.Content.ReadAsStringAsync().Result;
            
            return response;
        }

        [HttpGet]
        public ActionResult RedirectLogin(string Token)
        {
            ViewBag.token = Token;
            return View();
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

        [Authorize]
        public JsonResult SessionChanged()
        {
            // If the javascript made the reuest, issue a challenge so the OIDC request will be constructed.
            if (HttpContext.GetOwinContext().Request.QueryString.Value == "")
            {
                HttpContext.GetOwinContext().Authentication.Challenge(
                    new AuthenticationProperties { RedirectUri = "/Authen/SessionChanged" },
                    OpenIdConnectAuthenticationDefaults.AuthenticationType);

                return Json(new { }, "application/json", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(HttpContext.GetOwinContext().Request.QueryString.Value, "application/json", JsonRequestBehavior.AllowGet);
            }
        }
        // Sign in has been triggered from Sign In Button or From Single Sign Out Page.
        public void SignIn(string redirectUri)
        {
            //var result = Startup.email.Split('@');
            //var st = result[0].ToString();
            //var isLogon = ApiHelper.ChecloginEMR(st, Startup.sessionId.ToString());
            //if (isLogon == "0")
            //{


            //}
            //else if (isLogon == "1")
            //{

            //}
            HttpContext.GetOwinContext().Authentication.Challenge(
          new AuthenticationProperties { RedirectUri = "/Authen/DoLogin" },
          OpenIdConnectAuthenticationDefaults.AuthenticationType);

        }

        public async Task<string> SetAccessToken(string email)
        {
            var Uid = Session["Uid"];
            var userClaims = User.Identity as System.Security.Claims.ClaimsIdentity;
            string accessToken = await ApiHelper.GetToken();
            if (Session["AccessToken"] != null)
            {
                accessToken = Session["AccessToken"].ToString();
            }
            else
            {
                Session["AccessToken"] = accessToken;
            }
      
            //var m1 = Startup.Unique;

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
        public async Task<string> ChecloginAzure(string email, string sessionId)
        {
            var uri = "api/ManageAppBase/CheckLogin?email=" + email + "&sessionId=" + sessionId;
            var value = await ApiHelper.HttpGet(uri, "2krojMdNQkSpZzwybnoR6g==");
            var response = value.Content.ReadAsStringAsync().Result;
            return response;
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<JsonResult> CheckLogin(string placeCheck = "AnyWhere")
        {
            var user = User.Identity as System.Security.Claims.ClaimsIdentity;
            //string curRequestUrl = HttpUtility.UrlEncode(Request.Url.ToString());
            string url = new UrlHelper(HttpContext.Request.RequestContext).Action("Login", "Authen");
            string urlAzure = new UrlHelper(HttpContext.Request.RequestContext).Action("SignOut", "Authen");
            string email = user.Name;
            string sessionId = Startup.sessionId;
            var isLogon = await ChecloginAzure(email, sessionId);
            switch (int.Parse(isLogon))
            {
                case 0:
                    Startup.IsLogin = 1;
                    return Json(new { IsSuccess = -1, Message = url }, JsonRequestBehavior.AllowGet);
                case 1:
                    return Json(new { IsSuccess = 1, Message = "Login" }, JsonRequestBehavior.AllowGet);
                //case 2:
                //    return Json(new { IsSuccess = -1, Message = urlAzure }, JsonRequestBehavior.AllowGet);
                default:
                    Startup.IsLogin = 1;
                    return Json(new { IsSuccess = -1, Message = url }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SignInDiff(string redirectUri)
        {
            // var m = "https://login.microsoftonline.com/1efa3961-fa37-4153-9253-38b8c63be196/oauth2/authorize?client_id=dff6933d-f731-402f-957c-c88e1a0ee373&redirect_uri=https%3a%2f%2flocalhost%3a44320%2fsignin-oidc&response_mode=form_post&response_type=code+id_token&scope=openid+profile&state=OpenIdConnect.AuthenticationProperties%3d0sDshgVx-gZmo_0C8ftzIM7NfG03rsuNE4ix1cB5YbNQ4eg7KVT7O898d1zdpgxkQmkxcU32g1IiPdCYAFsLuMQ7ahwpgv1nXhKKzQcfoYJSXLqN3_78jBtpPHHzBbPzRKfZ2KMCyBSCbptKJsx9hvLo7TlefeiTnmm1xMu7o2iXEZl5rmC9sME2ldlze8cT&nonce=638073570286880976.ZDRhNDFiNmMtNGI1ZC00ZjUyLThmYmMtYzdjOTZjODAwYjVkYTNiNmNkYTMtM2Q4Yi00NDg5LTlkNTMtZTE3ZmQ2YTc4MjZm";
            //var uri = $"{AzureAuthority}/{Tenantid}/oauth2/authorize?client_id={ClientId}&redirect_uri={Redirect1}&response_mode=form_post&response_type=code+id_token&scope=openid+profile&state=12345&prompt=select_account";
            //  return Redirect("https://login.microsoftonline.com/1efa3961-fa37-4153-9253-38b8c63be196/oauth2/authorize?client_id=dff6933d-f731-402f-957c-c88e1a0ee373&redirect_uri=https%3a%2f%2flocalhost%3a44320%2fsignin-oidc&response_mode=form_post&response_type=code+id_token&scope=openid+profile&state=OpenIdConnect.AuthenticationProperties%3d4XL856gBwtr6VSl7KTEWJt1Ufh6P7YF1IdnO5e8MwwD_tA5bDAh7BKWEWL8tdQIGEsN6cWGiEDwuWRKutLI_CWjFlBvnBzvJkNN9BPJPE-H3t8Kuj7oQlS5vW3v5kKWheWV7EMX_w0x_gh0nZmb5_zMbXwsrEUuP08uoijqLZHeK5vet8TvfMgXszDj2UuQH&nonce=638072944621214416.Yzc2MDgzN2UtMTVjNi00OGZhLTk1MTMtMzgxNTU3MTg0ZTExNDcxOThkODctM2Q0YS00NjAxLTg1YWUtYTE3YTgzNjQ5N2I2&prompt=select_account");
            return RedirectToAction("SignIn1");
        }
        public void SignIn1(string redirectUri)
        {
            HttpContext.GetOwinContext().Authentication.Challenge(
           new AuthenticationProperties { RedirectUri = "/Authen/DoLogin" },
           OpenIdConnectAuthenticationDefaults.AuthenticationType);

        }
        // Sign a user out of both AAD and the Application
        public void SignOut()
        {
            Session.Clear();
            Session.Abandon();
            var cookie1 = new HttpCookie("EFormED");
            DateTime nowDateTime = DateTime.Now;
            cookie1.Expires = nowDateTime.AddDays(-1);
            HttpContext.Request.Cookies.Add(cookie1);
            var value = HttpContext.Response.Cookies["EFormED"];
            HttpContext.GetOwinContext().Authentication.SignOut(
                new AuthenticationProperties { RedirectUri = Startup.PostLogoutRedirectUri },
                OpenIdConnectAuthenticationDefaults.AuthenticationType,
                CookieAuthenticationDefaults.AuthenticationType);
            //return RedirectToAction("Login", "Authen");
        }

        public ActionResult LogOutAz()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            HttpContext.GetOwinContext().Authentication.User =
                new GenericPrincipal(new GenericIdentity(string.Empty), null);
            HttpContext.GetOwinContext().Authentication.Challenge(
                      new AuthenticationProperties { RedirectUri = "/" },
                      OpenIdConnectAuthenticationDefaults.AuthenticationType);
            return null;

        }
        public async Task<string> GetListAppLogin()
        {
            var appid = ConfigurationManager.AppSettings["AppId"].ToString();
            var uri = "api/ManageAppBase/GetListAppForm?appid=" + appid;
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
        public ActionResult ErrorHandler(int id)
        {
            return View("Error", id);

        }
        [HttpPost]
        public async Task<ActionResult> PushUid(string id)
        {
            Startup.Unique = id;
            Session["Uid"] = id;
            var userClaims = User.Identity as System.Security.Claims.ClaimsIdentity;
            if (userClaims != null)
            {
                await SetAccessToken(userClaims.Name);
            }
           
            return Json(null);

        }

    }
}