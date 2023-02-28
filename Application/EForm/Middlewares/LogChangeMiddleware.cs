using DataAccess.Dapper.Repository;
using DataAccess.Models;
using DataAccess.Repository;
using EForm.Common;
using Microsoft.Owin;
using System;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EForm.Middlewares
{
    public class LogChangeMiddleware: OwinMiddleware
    {
        public LogChangeMiddleware(OwinMiddleware next)
            : base(next)
        {
        }

        public async override Task Invoke(IOwinContext context)
        {
            var cr_at = DateTime.Now;
            var request_path = context.Request.Path.Value;
            var request_method = context.Request.Method;
            if (!request_path.Contains("api") || InIgnoreWriteLog(request_path))
            {
                await Next.Invoke(context);
                return;
            }

            var log_created_by = context.Request.User.Identity.Name;
            var log_ip = context.Request.RemoteIpAddress;
            var log_uri = context.Request.Uri.ToString();
            var log_action = request_method;
            var log_request = string.Empty;
            if (context.Request.Body.CanRead)
            {
                using (StreamReader reader = new StreamReader(context.Request.Body))
                {
                    log_request = reader.ReadToEndAsync().Result;
                    if ((log_request != null && log_request.ToLower().Contains("\"password\"")) || InSensitive(request_path))
                    {
                        log_request = "*****";
                    }
                }
            }

            await Next.Invoke(context);

            var log_response = context.Response.StatusCode.ToString();
            if ((InWriteToLogFile(request_path) || log_action.ToUpper() == "GET") && !log_uri.Contains("api/PublicApi/Ping"))
                WriteLogToFile(log_created_by, log_ip, log_uri, log_action, log_request, log_response);
            else
                WriteLogToDB(log_created_by, log_ip, log_uri, log_action, log_request, log_response, cr_at);
        }

        private bool InSensitive(string path)
        {
            var lowercase_path = path.ToLower();
            foreach(var i in Constant.LOG_CHANGE_SENSITIVE)
                if (lowercase_path.Contains(i))
                    return true;
            return false;
        }
        private bool InIgnoreWriteLog(string path)
        {
            var lowercase_path = path.ToLower();
            foreach (var i in Constant.IGNORE_WRITE_LOG)
                if (lowercase_path.Contains(i))
                    return true;
            return false;
        }
        private bool InWriteToLogFile(string path)
        {
            var lowercase_path = path.ToLower();
            foreach (var i in Constant.LOG_CHANGE_FILE)
                if (lowercase_path.Contains(i))
                    return true;
            return false;
        }

        private void WriteLogToFile(string log_created_by, string log_ip, string log_uri, string log_action, string log_request, string log_response)
        {
            if (log_uri.ToLower().Contains(Constant.API_PUBLIC_LOG[0].ToLower()))
                CustomLog.apipubliblog.Info(new
                {
                    CreatedBy = log_created_by,
                    Ip = log_ip,
                    URI = log_uri,
                    Action = log_action,
                    Request = log_request,
                    Response = log_response,
                });
            else
                CustomLog.accesslog.Info(new
                {
                    CreatedBy = log_created_by,
                    Ip = log_ip,
                    URI = log_uri,
                    Action = log_action,
                    Request = log_request,
                    Response = log_response,
                });
        }
        private void WriteLogToDB(string log_created_by, string log_ip, string log_uri, string log_action, string log_request, string log_response, DateTime CreatedAt)
        {
            using (DataAccess.Repository.IUnitOfWork unitOfWork = new EfUnitOfWork())
            {
                if ((log_uri.Contains("api/DoctorPlaceDiagnosticsOrder/Charge") || log_uri.Contains("api/PublicApi/Ping")) && log_action == "POST")
                {
                    LogTmp logTmp = new LogTmp
                    {
                        Id = Guid.NewGuid(),
                        UpdatedAt = DateTime.Now,
                        CreatedAt = CreatedAt,
                        CreatedBy = log_created_by,
                        Ip = ConfigurationManager.AppSettings["APP_ID"] != null ? ConfigurationManager.AppSettings["APP_ID"].ToString() : "NONE",
                        URI = log_uri,
                        Action = log_uri.Contains("api/DoctorPlaceDiagnosticsOrder/Charge") ? "CHARGE_SUBMIT" : "INFO",
                        Request = log_request,
                        Response = log_response,
                    };
                    unitOfWork.LogTmpRepository.Add(logTmp);
                    unitOfWork.Commit();
                    //using (DataAccess.Repository.IUnitOfWork unitOfWork = new EfUnitOfWork())
                    //{
                    //    unitOfWork.LogTmpRepository.Add(logTmp);
                    //    
                    //}
                }
                else
                {
                    //LogTmp logTmp = new LogTmp
                    //{
                    //    Id = Guid.NewGuid(),
                    //    UpdatedAt = DateTime.Now,
                    //    CreatedAt = CreatedAt,
                    //    CreatedBy = log_created_by,
                    //    Ip = log_ip,
                    //    URI = log_uri,
                    //    Action = log_action,
                    //    Request = log_request,
                    //    Response = log_response,
                    //};
                    //ExecStoProcedure.NoResultForLog("SaveLog", logTmp);
                    Log log = new Log
                    {
                        Id = Guid.NewGuid(),
                        CreatedAt = CreatedAt,
                        CreatedBy = log_created_by,
                        Ip = ConfigurationManager.AppSettings["APP_ID"] != null ? ConfigurationManager.AppSettings["APP_ID"].ToString() : "NONE",
                        URI = log_uri,
                        Action = log_action,
                        Request = log_request,
                        Response = log_response,
                        Reason = DateTime.Now.ToString()
                    };
                    unitOfWork.LogRepository.Add(log);
                    unitOfWork.Commit();
                }
                
            }
        }
    }
}