using DataAccess.Models;
using EForm.Common;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.Http.Filters;

namespace EForm.Filters
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        private readonly static string[] IGNORE_WRITE_LOG =
        {
            "System.Threading.Tasks.TaskCanceledException: A task was canceled",
            "System.OperationCanceledException: The operation was canceled"
        };
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var noww = DateTime.Now;
            var t = string.Format("{0}{1}{2}{3}", noww.Day.ToString(), noww.Hour.ToString(), noww.Minute.ToString(), noww.Millisecond.ToString());
            var error_id = string.Format("{0}-{1}", ConfigurationManager.AppSettings["APP_ID"] != null ? ConfigurationManager.AppSettings["APP_ID"].ToString() : "NONE", t);
            try
            {
                
                var log = new {
                    Ip = error_id,
                    URI = actionExecutedContext.Request.RequestUri.ToString(),
                    Response = actionExecutedContext.Exception.ToString(),
                    Action = "Error"
                };
                if (!(InIgnoreWriteLog(log.Response)))
                    CustomLog.errorlog.Error(log);
            }
            catch (Exception) { }

            var message = string.Format("Có lỗi xảy ra! Mã lỗi: {0}", error_id);
            var status = HttpStatusCode.InternalServerError;
            actionExecutedContext.Response = new HttpResponseMessage()
            {
                Content = new StringContent(message, System.Text.Encoding.UTF8, "text/plain"),
                StatusCode = status
            };
            base.OnException(actionExecutedContext);
        }
        private bool InIgnoreWriteLog(string err)
        {
            var lowercase_err = err.ToLower();
            foreach (var i in IGNORE_WRITE_LOG)
                if (lowercase_err.Contains(i.ToLower()))
                    return true;
            return false;
        }
        private string GetUsername()
        {
            try {
                var identity = (ClaimsIdentity)User.Identity;
                var username = identity?.Name;
                return username;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}