using Admin.Common;
using Admin.Common.Model;
using Admin.CustomAuthen;
using Admin.MemCached;
using Admin.Models;
using DataAccess.Models;
using DataAccess.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace Admin.Controllers
{
    public class AccountController : Controller
    {
        protected IUnitOfWork unitOfWork = new EfUnitOfWork();
        // GET: Account
        public ActionResult Login()
        {
            string ip = Request.UserHostAddress;
            ViewBag.ShowCaptcha = IsShowCaptcha(ip);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string ReturnUrl = "")
        {
            string ip = Request.UserHostAddress;
            if (!ModelState.IsValid)
            {
                IncreaseLoginFail(ip);
                ViewBag.ShowCaptcha = IsShowCaptcha(ip);
                return View(model);
            }

            if (ModelState.IsValid)
            {
                if (IsShowCaptcha(ip))
                {
                    string EncodedResponse = Request.Form["g-Recaptcha-Response"];
                    if (!RecaptchaAuthen.IsReCaptchValid(EncodedResponse))
                    {
                        ViewBag.Message = "Input informations is incorrect";
                        IncreaseLoginFail(ip);
                        ViewBag.ShowCaptcha = IsShowCaptcha(ip);
                        return View(model);
                    }
                }
                bool isValidADAccount = LoginAdAccount(model.UserName, model.Password);
                if (isValidADAccount)
                {
                    try
                    {
                        var user = unitOfWork.UserRepository.FirstOrDefault(s => s.Username.Equals(model.UserName) && s.IsAdminUser);
                        if (user == null)
                        {
                            ViewBag.Message = "User is not registered to application";
                            IncreaseLoginFail(ip);
                            ViewBag.ShowCaptcha = IsShowCaptcha(ip);
                        }
                        else
                        {
                            var userData = StoreUserData(user);
                            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, model.UserName, DateTime.Now, DateTime.Now.AddMinutes(1500), false, JsonConvert.SerializeObject(userData), FormsAuthentication.FormsCookiePath);
                            string hash = FormsAuthentication.Encrypt(ticket);
                            user.SessionId = hash.Substring(0, 20);
                            user.Session = hash;
                            unitOfWork.UserRepository.Update(user);
                            unitOfWork.Commit();
                            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);
                            Response.Cookies.Add(cookie);
                            SetZeroLoginFail(ip);
                            //string sKey = string.Format("{0}.{1}", "VMUser", model.UserName.ToString());
                            //CacheHelper.AddObject(userData, sKey, DateTime.Now.AddMinutes(15));
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "Have error when login. Please check with our Administrator";
                    }

                }
                else
                {
                    ViewBag.Message = "Input informations is incorrect";
                    IncreaseLoginFail(ip);
                    ViewBag.ShowCaptcha = IsShowCaptcha(ip);
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                //FormsIdentity id = (FormsIdentity)System.Web.HttpContext.Current.User.Identity;
                //string sKey = string.Format("{0}.{1}", "VMUser", id.Name);
                //CacheHelper.RemoveBy(sKey);
                try
                {
                    string session = Request.Cookies[FormsAuthentication.FormsCookieName].Value;
                    RemoveSession(session);
                }
                catch (Exception) { }
            }

            FormsAuthentication.SignOut();
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie);

            return RedirectToAction("Login", "Account");
        }

        #region Methods
        private bool IsShowCaptcha(string ip)
        {
            try
            {
                var login_failed = unitOfWork.LogInFailRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    !string.IsNullOrEmpty(e.IPAddress) &&
                    e.IPAddress == ip
                );
                return login_failed.Time >= Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["NumberShowCaptCha"]);
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        private bool LoginAdAccount(string userName, string password)
        {
            bool isValidAdAccount = false;
            using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
            {
                isValidAdAccount = context.ValidateCredentials(userName, password);
            }
            return isValidAdAccount;
        }
        
        private UserData StoreUserData(User user)
        {
            var userData = new UserData
            {
                DisplayName = user.DisplayName,
                FullName = user.Fullname,
                Roles = user.UserAdminRoles?.Where(s => !s.IsDeleted).Select(s => s.AdminRole.RoleName).ToList(),
            };
            return userData;
        }
        private void IncreaseLoginFail(string ip)
        {
            var login_fail = unitOfWork.LogInFailRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                !string.IsNullOrEmpty(e.IPAddress) &&
                e.IPAddress == ip
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
        #endregion
    }
}