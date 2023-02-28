
using AutoMapper;
using DataAccess.Repository;
using EForm.AutoMapper;
using EForm.Common;
using EForm.ScheduleJobs;
using System;
using System.IdentityModel.Claims;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace EForm
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            JobScheduler.Start();
            Mapper.Initialize(cfg => cfg.AddProfile<AutoMapperProfile>());

        }

        protected void Application_Error(Object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            CustomLog.errorlog.Error(ex);
        }

        protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
        {
            try
            {
                var resp_header = HttpContext.Current.Response.Headers;
                if (resp_header == null) return;

                resp_header.Remove("Server");
                resp_header.Remove("X-AspNetWebPages-Version");
                resp_header.Remove("X-AspNet-Version");
                resp_header.Remove("X-Powered-By");
                resp_header.Remove("X-AspNetMvc-Version");

                var new_cookie = resp_header.GetValues("Set-Cookie");
                if (new_cookie != null)
                {
                    var path = HttpContext.Current.Request.Path.ToLower();
                    if (Constant.IGNORE_EXTEND_SESSION_PATH.Contains(path))
                        resp_header.Remove("Set-Cookie");

                    else if (!Constant.IGNORE_UPDATE_SESSION_PATH.Contains(path))
                        UpdateSession(new_cookie);
                }
            }
            catch (Exception ex ) {
           
            
            }
        }

        private void UpdateSession(string[] raw_cookie)
        {
            var cookie = Regex.Match(raw_cookie[0], "^EFormED=(.*);")?.Groups[1].Value;
            if (cookie.ToString().Contains("expires"))
            {
                cookie = "";
            }
            if (!string.IsNullOrEmpty(cookie))
            {
                using (IUnitOfWork unitOfWork = new EfUnitOfWork())
                {
                    var username = HttpContext.Current.User.Identity.Name;
                    var user = unitOfWork.UserRepository.FirstOrDefault(m => !m.IsDeleted && m.Username == username);
                    if (user != null)
                    {
                        user.SessionId = cookie.Substring(0, 20);
                        user.Session = cookie;
                        unitOfWork.UserRepository.Update(user);
                        unitOfWork.Commit();
                    }
                }
            }
        }
    }
}
