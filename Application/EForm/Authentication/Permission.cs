using EForm.Common;
using Microsoft.Ajax.Utilities;
using Microsoft.Owin;
using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace EForm.Authentication
{
    public class Permission: AuthorizeAttribute
    {
        public string Code { get; set; }

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
                var request_path = actionContext.Request.RequestUri.AbsolutePath;                          
                if (Code.Contains("APICR") || Code.Contains("APIGDT") || Code.Contains("APIUD") 
                    || Code.Contains("APICF") || Code.Contains("APIDEL") || Code.Contains("VIEWFILEDT") ||
                    Code.Contains("VIEWFILEDEL") || Code.Contains("UPLOADFILEUPDA"))
                {
                    var url = request_path.Split('/');
                    string type = url[3];
                    string formcode = url[4];
                    if (formcode.Contains("_TAB"))
                    {
                        int index = formcode.IndexOf("_TAB");
                        int lengthSub = formcode.Substring(index).Length;
                        formcode = formcode.Substring(0, formcode.Length - lengthSub);
                    }
                    if(Code == "APICR" || Code == "APIGDT" || Code == "APIUD" || Code == "APICF" || Code == "APIDEL")
                    {
                        Code = Code + type + formcode;
                    }else
                    {
                        if(Code.Contains("APICR"))
                        {
                            Code = "APICR" + type + formcode;
                        }
                        if (Code.Contains("APIGDT"))
                        {
                            Code = "APIGDT" + type + formcode;
                        }
                        if (Code.Contains("APIUD"))
                        {
                            Code = "APIUD" + type + formcode;
                        }
                        if (Code.Contains("APICF"))
                        {
                            Code = "APICF" + type + formcode;
                        }                        
                    }
                    //upload file
                    if (Code.Contains("VIEWFILEDT"))
                    {
                        Code = "VIEWFILEDT" + type + formcode;
                    }
                    if (Code.Contains("VIEWFILEDEL"))
                    {
                        Code = "VIEWFILEDEL" + type + formcode;
                    }
                    if (Code.Contains("UPLOADFILEUPDA"))
                    {
                        Code = "UPLOADFILEUPDA" + type + formcode;
                    }
                }                
                //var url = actionContext.Request.RequestUri.AbsolutePath;
                //var method = actionContext.Request.Method.Method;
                var principal = actionContext.RequestContext.Principal as ClaimsPrincipal;
                var roles = principal.Claims.FirstOrDefault(c => c.Type == "Roles").Value;
                if (string.IsNullOrEmpty(roles))
                    return false;

                string[] array_roles = roles.Split(',');
                return array_roles.Contains(Code);
            }
            catch(Exception)
            {
                return false;
            }
        }
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            base.HandleUnauthorizedRequest(actionContext);
            var msg = new { ViMessage = "Bạn không có quyền truy cập, Mã lỗi " + Code, EnMessage = "You do NOT have permission to access", ActionCode = "" };
            actionContext.Response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.Forbidden,
                Content = new ObjectContent<dynamic>(msg, new JsonMediaTypeFormatter())
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