using EForm.Common;
using EForm.Cached;
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

namespace EForm.Authentication
{
    public class CSRFCheck : AuthorizeAttribute
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
            try
            {
                string formToken = actionContext.Request.Headers.GetValues("RequestVerificationToken").First();
                var cookie = actionContext.Request.Headers.GetCookies("__RequestVerificationToken").LastOrDefault();
                var csrf_token = string.Format("{0}{1}", formToken, cookie["__RequestVerificationToken"].Value);
                return CSRFToken.IsValid(csrf_token);
            }
            catch (Exception)
            {
                return false;
            }
        }
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            base.HandleUnauthorizedRequest(actionContext);
            actionContext.Response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new ObjectContent<dynamic>(Message.CSRF_MISSING, new JsonMediaTypeFormatter())
            };
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