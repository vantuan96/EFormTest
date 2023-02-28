using DataAccess.Dapper.Repository;
using DataAccess.Models;
using DataAccess.Models.GeneralModel;
using DataAccess.Repository;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Common;
using EForm.Models;
using EForm.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace EForm.Controllers.GeneralControllers
{
    [SessionAuthorize]
    public class LogController : BaseApiController
    {
        [HttpGet]
        [Route("api/Logs")]
        [Permission(Code = "GLOGS1")]
        public IHttpActionResult GetLogsAPI([FromUri]LogParameterModel request)
        {
            if (!request.Validate())
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);

            var result = unitOfWork.LogRepository.AsQueryable().Where(
                log => !string.IsNullOrEmpty(log.Action) &&
                log.Action.Equals("POST") &&
                !string.IsNullOrEmpty(log.Response) &&
                log.Response.Equals("200") &&
                !string.IsNullOrEmpty(log.URI) &&
                log.URI.Contains(request.Id) &&
                log.URI.Contains(request.ModelName) &&
                !log.URI.Contains("/Sync") &&
                !log.URI.Contains("/Confirm/") &&
                !log.URI.Contains("/Delete/")
            ).OrderByDescending(log => log.CreatedAt).ToList()
            .Select(log => new LogViewModel
            {
                CreatedAt = log.CreatedAt,
                UpdatedAt = log.CreatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                Request = log.Request,
                Response = log.Response,
                Username = log.CreatedBy
            });
            return Content(HttpStatusCode.OK, result);
        }
        //[HttpGet]
        //[Route("api/api/errorr/log")]
        //[Permission(Code = "ERRORLOGVIEW")]
        //public IHttpActionResult GetApiERRORAPI()
        //{
        //    if (getUsername() != "thangdc3") return Content(HttpStatusCode.OK, new { S = "3" });
        //    var result = unitOfWork.SystemNotificationRepository.Find(e => !e.IsDeleted)
        //    .Take(50).OrderByDescending(log => log.CreatedAt).ToList();
        //    return Content(HttpStatusCode.OK, result);
        //}
        //[HttpGet]
        //[Route("api/api/sendmail/test")]
        //[Permission(Code = "GLOGS1")]
        //public IHttpActionResult SendMailTest()
        //{
        //    if (!IsSuperman()) return Content(HttpStatusCode.OK, new { S = "NoAction" });
        //    SendEmailNotificationResult("x");
        //    return Content(HttpStatusCode.OK, new { });
        //}
        [HttpPost]
        [CSRFCheck]
        [Route("api/Logs")]
        [Permission(Code = "GLOGS2")]
        public IHttpActionResult CreateLogsAPI([FromBody]JObject request)
        {
            var user = GetUser();
            using (DataAccess.Repository.IUnitOfWork unitOfWork = new EfUnitOfWork())
            {
                Log log = new Log
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Now,
                    CreatedBy = user.Username,
                    Username = user.Username,
                    Action = "USER_REQUEST_LOG",
                    URI = request["url"]?.ToString(),
                    Name = request["name"]?.ToString(),
                    Reason = request["reason"]?.ToString(),
                };
                unitOfWork.LogRepository.Add(log);
                unitOfWork.Commit();
            }
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }
        public void MoveDataTableLogToDBOther(int takeRowNumber)
        {
            var param = new
            {
                takeRowNumber = takeRowNumber
            };
            ExecStoProcedure.NoResult("spMoveDataTableLogToDBOther", param);
        }
        protected void SendEmailNotificationResult(string type)
        {
            BodyAPI body = new BodyAPI()
            {
                pid = "82009923",
                customer_name = "BN TEST",
                phone_number = "0987654321",
                doctor_name = "Nguyễn Văn A",
                visit_code = "125863",
                visit_type = "OPD",
                lang = "vi",
                message_type = "OPD1",
                speciality = "Phòng khám Nội",
                hospital_code = "HHN",
                send_time = DateTime.Now.ToString(),
                completed_time = DateTime.Now.ToString(),
                examination_date = DateTime.Now.ToString()
            };
            SendSMS(new Guid(), body);
        }
        private void SendSMS(Guid formId, BodyAPI body)
        {
            var bodyCovert = JsonConvert.SerializeObject(body);
            SendMailNotification mail = new SendMailNotification
            {
                ReceiverId = formId,
                FormId = formId,
                Type = "TEST",
                Subject = "OPD, Gửi sms khi Hoàn thành khám",
                Body = bodyCovert,
                To = "MYVINMEC"
            };
            try
            {
                HttpClientHandler handler1 = new HttpClientHandler();
                handler1.ClientCertificateOptions = ClientCertificateOption.Manual;
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

                var url = ConfigurationManager.AppSettings["urlSMS"].ToString();
                var client_id = ConfigurationManager.AppSettings["client-id"].ToString();
                var client_secret = ConfigurationManager.AppSettings["client-secret"].ToString();

                var webRequest = (HttpWebRequest)WebRequest.Create(url);
                webRequest.Method = HttpMethod.Post.ToString();
                webRequest.Headers.Add(HttpRequestHeader.ContentType.ToString(), "application/json");
                webRequest.Headers.Add("client-id", client_id);
                webRequest.Headers.Add("client-secret", client_secret);

                var proxy_addr = System.Configuration.ConfigurationManager.AppSettings["ProxySever"];
                if (!string.IsNullOrEmpty(proxy_addr))
                {
                    WebProxy wp = new WebProxy(proxy_addr);
                    webRequest.Proxy = wp;
                }

                string raw_data = string.Empty;
                using (WebResponse response = webRequest.GetResponse())
                {
                    using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                    {
                        raw_data = stream.ReadToEnd();
                        CustomLog.intervaljoblog.Info($"<Notify APIGW> Send SMS MSG: {raw_data}");
                    }
                }

            }
            catch (Exception ex)
            {
                CustomLog.intervaljoblog.Info($"<Notify APIGW> Send email error: {ex.ToString()}");
            }


        }
        public class BodyAPI
        {
            public string pid { get; set; }
            public string customer_name { get; set; }
            public string phone_number { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string doctor_name { get; set; }
            public string visit_code { get; set; }
            public string hospital_code { get; set; }
            private string _completedtime { get; set; }
            public string completed_time
            {
                get
                {
                    return _completedtime;
                }
                set
                {
                    _completedtime = DateTime.ParseExact(value, "M/d/yyyy h:mm:ss tt", null).ToString("yyyy-MM-ddTHH:mm:ss.fff+07:00");
                }
            }

            private string _examination;
            public string examination_date
            {
                get { return _examination; }
                set
                {
                    _examination = DateTime.ParseExact(value, "M/d/yyyy h:mm:ss tt", null).ToString("yyyy-MM-ddTHH:mm:ss.fff+07:00");
                }
            }
            public string speciality { get; set; }
            private string _sendtime;
            public string send_time
            {
                get { return _sendtime; }
                set
                {
                    _sendtime = DateTime.ParseExact(value, "M/d/yyyy h:mm:ss tt", null).ToString("yyyy-MM-ddTHH:mm:ss.fff+07:00");
                }
            }
            public string message_type { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string visit_type { get; set; }
            public string lang { get; set; }
        }
    }
}
