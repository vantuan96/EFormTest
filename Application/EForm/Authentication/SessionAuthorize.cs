using EForm.Common;
using Microsoft.Owin;
using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using DataAccess.Repository;
using DataAccess.Models;
using EForm.Helper;

namespace EForm.Authentication
{
    public class SessionAuthorize : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {


            var ip = GetClientIpAddress(actionContext.Request);
            try
            {
                if (ConfigurationManager.AppSettings["DevWriteLists"].Contains(ip))
                    return true;
            }
            catch (Exception) { }

            if (actionContext.Request.RequestUri.AbsoluteUri.ToString().Contains("api/PublicApi"))
            {
                try
                {
                    var token = actionContext.Request.Headers.FirstOrDefault(h => h.Key.Equals("Authorization"));
                    string app_token = ConfigurationManager.AppSettings["PublicApiToken"];
                    string token_value = token.Value.FirstOrDefault();
                    bool is_valieate = token_value.ToString().Contains(ConfigurationManager.AppSettings["PublicApiToken"].ToString());
                    return is_valieate;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            if (!base.IsAuthorized(actionContext))
                return false;

            var result = Startup.email.Split('@');
            var st = result[0].ToString();
            var isLogon = ApiHelper.ChecloginEMR(st, Startup.sessionId.ToString());


            dynamic authen_cookie;
            dynamic authen_cookie1;
            dynamic authen_cookie2;
            var session_id1 = "";
            var session_id2 = "";
            var session_id3 = "";
            try
            {
                authen_cookie = actionContext.Request.Headers.GetCookies("EFormED").FirstOrDefault();
                authen_cookie1 = actionContext.Request.Headers.GetCookies("EFormEDC1").FirstOrDefault();
                authen_cookie2 = actionContext.Request.Headers.GetCookies("EFormEDC2").FirstOrDefault();
                if (authen_cookie != null)
                {
                    session_id1 = authen_cookie["EFormED"].Value;
                    if (session_id1 == "chunks:2")
                    {
                        session_id1 = "";
                    }
                }
                if (authen_cookie1 != null)
                {
                    session_id2 = authen_cookie1["EFormEDC1"].Value;
                }
                if (authen_cookie2 != null)
                {
                    session_id3 = authen_cookie2["EFormEDC2"].Value;
                }
                var session_id_orgin = (session_id1 + session_id2 + session_id3);
                var session_id = session_id_orgin.Substring(0, 20);
                return IsTokenValid(session_id);
                //if (checkValid && isLogon == "0" )
                //{
                //    return true;
                //}
                //else if (isLogon == "0" && !checkValid) {

                //    return false;
                //}else if (checkValid && isLogon == "1")
                //{
                //    return true;
                //}
                //else if (isLogon == "1" && !checkValid)
                //{
                //    return true;
                //}
                //if (isLogon == "1")
                //{
                //    if (checkValid)
                //    {
                //        return true;
                //    }
                //    else
                //    {
                //        return false;
                //    }
                //}
                //else
                //{
                //    if (checkValid)
                //    {
                //        return true;
                //    }
                //    else
                //    {
                //        return false;
                //    }
                //}

            }
            catch (Exception ex)
            {


            }
            return false;
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            base.HandleUnauthorizedRequest(actionContext);
            actionContext.Response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Content = new ObjectContent<dynamic>(Message.UNAUTHORIZED, new JsonMediaTypeFormatter())
            };


        }

        private bool IsTokenValid(string session_id)
        {
            using (IUnitOfWork unitOfWork = new EfUnitOfWork())
            {
                var user = unitOfWork.UserRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    !string.IsNullOrEmpty(e.SessionId) &&
                    e.SessionId == session_id
                );
                if (user != null)
                    return user.Username == HttpContext.Current.User.Identity.Name;

                return false;
            }
        }

        private string GetClientIpAddress(HttpRequestMessage request)
        {
            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                return IPAddress.Parse(((HttpContextBase)request.Properties["MS_HttpContext"]).Request.UserHostAddress).ToString();
            }
            if (request.Properties.ContainsKey("MS_OwinContext"))
            {
                return IPAddress.Parse(((OwinContext)request.Properties["MS_OwinContext"]).Request.RemoteIpAddress).ToString();
            }
            return String.Empty;
        }
    }
}