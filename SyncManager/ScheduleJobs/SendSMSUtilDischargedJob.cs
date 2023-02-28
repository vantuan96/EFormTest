using Common;
using DataAccess.Models.GeneralModel;
using DataAccess.Repository;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace SyncManager.ScheduleJobs
{
    public class SendSMSUtilDischargedJob : IJob
    {
        protected IUnitOfWork unitOfWork = new EfUnitOfWork();
        private readonly string smsType = "SMSDISCHANGED";
        public void Execute(IJobExecutionContext context)
        {
            DoJob();
        }

        public void Execute()
        {
            DoJob();
        }

        private void DoJob()
        {
            CustomLogs.intervaljoblog.Info($"<SendSMSUtilDischargedJob> Start!");
            if (ConfigHelper.CF_SendNotiToMyVinmec_CS_is_off)
            {
                CustomLogs.intervaljoblog.Info($"<SendSMSUtilDischargedJob> is Off!");
                return;
            }


            string statusCode = ConfigurationManager.AppSettings["myvinmec-sms-StatusCode"].ToString();
            var curent_date = DateTime.Now.AddMinutes(Convert.ToDouble(ConfigurationManager.AppSettings["myvinmec-sms-AboutMinute"].ToString()));
            var vi_Nationality = string.IsNullOrEmpty(ConfigurationManager.AppSettings["myvinmec-sms-ViNationality"].ToString()) ? new List<string>() {"VNM"} : ConfigurationManager.AppSettings["myvinmec-sms-ViNationality"].ToString().ToUpper().Split(',').ToList();
            if (string.IsNullOrWhiteSpace(statusCode))
            {
                CustomLogs.intervaljoblog.Info($"<SendSMSUtilDischargedJob> END with status config is null");
                return;
            }
            var om = DateTime.Now.AddDays(-7);
            var opds = unitOfWork.OPDRepository.Find(x => statusCode.Contains(x.EDStatus.Code) && !x.IsDeleted
                                                                                            && x.OPDOutpatientExaminationNote != null
                                                                                            && x.OPDOutpatientExaminationNote.UpdatedAt >= curent_date
                                                                                            && x.OPDOutpatientExaminationNote.UpdatedAt <= DateTime.Now
                                                                                            && x.CreatedAt > om
                                                                                            ).ToList();
            
            foreach (var opd in opds)
            {
                // CustomLog.intervaljoblog.Info($"<SendSMSUtilDischargedJob> OPD!" + opd.Id.ToString());
                var mailSend = unitOfWork.SendMailNotificationRepository.FirstOrDefault(x => x.FormId == opd.Id);
                if (mailSend == null)
                {
                    string lang = string.IsNullOrWhiteSpace(opd.Customer.Nationality) || !vi_Nationality.Contains(opd.Customer.Nationality.ToUpper()) ? "en" : "vi";
                    BodyAPI body = new BodyAPI()
                    {
                        pid = opd.Customer.PID,
                        customer_name = opd?.Customer?.Fullname,
                        phone_number = opd?.Customer?.Phone,
                        doctor_name = opd?.PrimaryDoctor?.Fullname,
                        visit_code = opd?.VisitCode,
                        visit_type = "OPD",
                        lang = lang,
                        message_type = "OPD1",
                        speciality = opd?.Specialty?.ViName,
                        hospital_code = opd?.Site?.ApiCode,
                        send_time = DateTime.Now.ToString(),
                        completed_time = opd.OPDOutpatientExaminationNote.UpdatedAt.ToString(),
                        examination_date = opd.OPDOutpatientExaminationNote?.ExaminationTime?.ToString(),
                        emr_visit_type = "OPD"
                    };
                    SendSMS(opd.Id, body);
                }
            }

            var edcs = unitOfWork.EOCRepository.Find(x => statusCode.Contains(x.Status.Code) && !x.IsDeleted
                                                                                            && x.UpdatedAt >= curent_date
                                                                                            && x.UpdatedAt <= DateTime.Now
                                                                                            && x.CreatedAt > om
                                                                                            ).ToList();
            foreach (var edc in edcs)
            {
                var mailSend = unitOfWork.SendMailNotificationRepository.FirstOrDefault(x => x.FormId == edc.Id);
                if (mailSend == null)
                {
                    string lang = string.IsNullOrWhiteSpace(edc.Customer.Nationality) || !vi_Nationality.Contains(edc.Customer.Nationality.ToUpper()) ? "en" : "vi";
                    var examinationNote = unitOfWork.EOCOutpatientExaminationNoteRepository.Find(e => e.VisitId == edc.Id).FirstOrDefault();
                    if (examinationNote != null)
                    {
                        BodyAPI body = new BodyAPI()
                        {
                            pid = edc.Customer.PID,
                            customer_name = edc?.Customer?.Fullname,
                            phone_number = edc?.Customer?.Phone,
                            doctor_name = edc?.PrimaryDoctor?.Fullname,
                            visit_code = edc?.VisitCode,
                            visit_type = "EOD",
                            lang = lang,
                            message_type = "EOD1",
                            speciality = edc?.Specialty?.ViName,
                            hospital_code = edc?.Site?.ApiCode,
                            send_time = DateTime.Now.ToString(),
                            completed_time = examinationNote.UpdatedAt.ToString(),
                            examination_date = examinationNote.ExaminationTime?.ToString(),
                            emr_visit_type = "EDC"
                        };
                        SendSMS(edc.Id, body, "EDC");
                    }
                }
            }

            var eds = unitOfWork.EDRepository.Find(x => statusCode.Contains(x.EDStatus.Code) && !x.IsDeleted
                                                                                            && x.UpdatedAt >= curent_date
                                                                                            && x.UpdatedAt <= DateTime.Now
                                                                                            && x.CreatedAt > om
                                                                                            ).ToList();

            foreach (var edvisit in eds)
            {
                var mailSend = unitOfWork.SendMailNotificationRepository.FirstOrDefault(x => x.FormId == edvisit.Id);
                //  || (mailSend.Status != "SENDED" && mailSend.Status != "SENDING" && mailSend.ErrorCount < 3)
                if (mailSend == null)
                {
                    string lang = string.IsNullOrWhiteSpace(edvisit.Customer.Nationality) || !vi_Nationality.Contains(edvisit.Customer.Nationality.ToUpper()) ? "en" : "vi";
                    var examinationNote = unitOfWork.DischargeInformationRepository.Find(e => e.Id == edvisit.DischargeInformationId).FirstOrDefault();
                    if (examinationNote != null)
                    {
                        BodyAPI body = new BodyAPI()
                        {
                            pid = edvisit.Customer.PID,
                            customer_name = edvisit?.Customer?.Fullname,
                            phone_number = edvisit?.Customer?.Phone,
                            doctor_name = edvisit?.PrimaryDoctor?.Fullname,
                            visit_code = edvisit?.VisitCode,
                            visit_type = "ED",
                            lang = lang,
                            message_type = "ED1",
                            speciality = edvisit?.Specialty?.ViName,
                            hospital_code = edvisit?.Site?.ApiCode,
                            send_time = DateTime.Now.ToString(),
                            completed_time = examinationNote.UpdatedAt.ToString(),
                            examination_date = edvisit.AdmittedDate.ToString(),
                            emr_visit_type = "ED"
                        };
                        SendSMS(edvisit.Id, body, "ED");
                    }
                }
            }

            var ipds = unitOfWork.IPDRepository.Find(x => statusCode.Contains(x.EDStatus.Code) && !x.IsDeleted
                                                                                            && x.UpdatedAt >= curent_date
                                                                                            && x.UpdatedAt <= DateTime.Now
                                                                                            && x.CreatedAt > om
                                                                                            ).ToList();
            foreach (var ipdvisit in ipds)
            {
                var mailSend = unitOfWork.SendMailNotificationRepository.FirstOrDefault(x => x.FormId == ipdvisit.Id);
                if (mailSend == null)
                {
                    string lang = string.IsNullOrWhiteSpace(ipdvisit.Customer.Nationality) || !vi_Nationality.Contains(ipdvisit.Customer.Nationality.ToUpper()) ? "en" : "vi";
                    var examinationNote = unitOfWork.IPDMedicalRecordRepository.Find(e => e.Id == ipdvisit.IPDMedicalRecordId).FirstOrDefault();
                    if (examinationNote != null)
                    {
                        BodyAPI body = new BodyAPI()
                        {
                            pid = ipdvisit.Customer.PID,
                            customer_name = ipdvisit?.Customer?.Fullname,
                            phone_number = ipdvisit?.Customer?.Phone,
                            doctor_name = ipdvisit?.PrimaryDoctor?.Fullname,
                            visit_code = ipdvisit?.VisitCode,
                            visit_type = "IPD",
                            lang = lang,
                            message_type = "IPD1",
                            speciality = ipdvisit?.Specialty?.ViName,
                            hospital_code = ipdvisit?.Site?.ApiCode,
                            send_time = DateTime.Now.ToString(),
                            completed_time = examinationNote.UpdatedAt.ToString(),
                            examination_date = ipdvisit.AdmittedDate.ToString(),
                            emr_visit_type = "IPD"
                        };
                        SendSMS(ipdvisit.Id, body, "IPD");
                    }
                }
            }
            unitOfWork.Dispose();
            CustomLogs.intervaljoblog.Info($"<SendSMSUtilDischargedJob> END!");
        }
        private SendMailNotification getOrCreate(Guid formId, string bodyCovert, string emr_visit_type = "OPD")
        {
            var lastError = unitOfWork.SendMailNotificationRepository.FirstOrDefault(x => x.FormId == formId && x.Type == smsType && x.ReceiverId == formId && x.Status == "ERROR");
            if (lastError == null)
            {
                SendMailNotification noti = new SendMailNotification
                {
                    ReceiverId = formId,
                    FormId = formId,
                    Type = smsType,
                    Subject = emr_visit_type + ", Gửi sms khi Hoàn thành khám",
                    Body = bodyCovert,
                    To = "MYVINMEC",
                    ErrorCount = 0,
                    Status = "SENDING"
                };
                unitOfWork.SendMailNotificationRepository.Add(noti);
                unitOfWork.Commit();
                return noti;
            }
            return lastError;
        }
        private void SendSMS(Guid formId, BodyAPI body, string emr_visit_type = "OPD")
        {
            var bodyCovert = JsonConvert.SerializeObject(body);
            // CustomLog.intervaljoblog.Info($"<SendSMSUtilDischargedJob> SENDING!" + bodyCovert);
            var lastError = getOrCreate(formId, bodyCovert, emr_visit_type);
            try
            {
                //var client_id = ConfigurationManager.AppSettings["myvinmec-sms-client-id"].ToString();
                //var client_secret = ConfigurationManager.AppSettings["myvinmec-sms-client-secret"].ToString();
                //var requestUri = ConfigurationManager.AppSettings["myvinmec-sms-url"].ToString();
                //var request = (HttpWebRequest)WebRequest.Create(requestUri);
                //CustomLog.intervaljoblog.Info($"<SendSMSUtilDischargedJob> requestUri!" + requestUri);
                ////request.Method = "POST";
                ////// request.PreAuthenticate = true;
                //request.Headers["Client-Id"] = client_id;
                //request.Headers["Client-Secret"] = client_secret;
                //request.Headers.Add("User-Agent : Mozilla / 5.0(Windows NT 10.0; Win64; x64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 94.0.4606.81 Safari / 537.36");
                ////request.Accept = "application/json";
                //CustomLog.intervaljoblog.Info($"<SendSMSUtilDischargedJob> Client-Secret!" + client_secret);
                //CustomLog.intervaljoblog.Info($"<SendSMSUtilDischargedJob> Client-Id!" + client_id);
                //var proxy_addr = ConfigurationManager.AppSettings["ProxySever"];
                //string raw_data = string.Empty;

                //if (!string.IsNullOrEmpty(proxy_addr))
                //{
                //    CustomLog.intervaljoblog.Info($"<SendSMSUtilDischargedJob> WebProxy!" + proxy_addr);
                //    WebProxy wp = new WebProxy(proxy_addr);
                //    request.Proxy = wp;
                //}
                //using (WebResponse response = request.GetResponse())
                //{
                //    CustomLog.intervaljoblog.Info($"<SendSMSUtilDischargedJob> GetResponse!");
                //    using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                //    {
                //        CustomLog.intervaljoblog.Info($"<SendSMSUtilDischargedJob> ReadToEnd!");
                //        raw_data = stream.ReadToEnd();
                //        lastError.Status = "SENDED";
                //        lastError.ErrorMessenge = raw_data;
                //        unitOfWork.SendMailNotificationRepository.Update(lastError);
                //        unitOfWork.Commit();
                //    }
                //}
                var proxy_url = ConfigurationManager.AppSettings["ProxySever"];
                HttpClientHandler handler1 = new HttpClientHandler()
                {
                    UseProxy = true,
                    PreAuthenticate = true,
                    UseDefaultCredentials = true,
                };
                if (proxy_url != null)
                {
                    handler1.Proxy = new WebProxy
                    {
                        Address = new Uri(ConfigurationManager.AppSettings["ProxySever"]),
                    };
                }

                handler1.ClientCertificateOptions = ClientCertificateOption.Manual;
                handler1.DefaultProxyCredentials = CredentialCache.DefaultCredentials;
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

                var url = ConfigurationManager.AppSettings["myvinmec-sms-url"].ToString();
                var client_id = ConfigurationManager.AppSettings["myvinmec-sms-client-id"].ToString();
                var client_secret = ConfigurationManager.AppSettings["myvinmec-sms-client-secret"].ToString();


                var client = new HttpClient(handler1);

                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla / 5.0");
                client.Timeout = TimeSpan.FromSeconds(20);
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(url),
                    Headers = {
                                { HttpRequestHeader.ContentType.ToString(), "application/json" },
                                { "Client-Id", client_id},
                                { "Client-Secret", client_secret},
                                { HttpRequestHeader.UserAgent.ToString(), "Mozilla / 5.0(Windows NT 10.0; Win64; x64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 94.0.4606.81 Safari / 537.36"}
                              },
                    Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json")
                };


                var response = client.SendAsync(request).Result;


                // CustomLog.intervaljoblog.Info($"<SendSMSUtilDischargedJob> SENDED!" + response.Content.ReadAsStringAsync().Result);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    lastError.Status = "SENDED";
                    unitOfWork.SendMailNotificationRepository.Update(lastError);
                }
                else
                {
                    lastError.ErrorCount++;
                    lastError.Status = "ERROR";
                    lastError.ErrorMessenge += "{" + DateTime.Now.ToString() + " - StatusCode:" + (int)response.StatusCode + " - " + response.Content.ReadAsStringAsync().Result + "} ";
                    unitOfWork.SendMailNotificationRepository.Update(lastError);
                }
                unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                lastError.ErrorCount++;
                lastError.Status = "ERROR";
                lastError.ErrorMessenge += "{" + DateTime.Now.ToString() + " - Exception:" + ex.ToString();
                unitOfWork.SendMailNotificationRepository.Update(lastError);
                unitOfWork.Commit();
            }
        }
    }
    public class BodyAPI
    {
        public string pid { get; set; }
        public string customer_name { get; set; }
        public string phone_number { get; set; }
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
        public string visit_type { get; set; }
        public string lang { get; set; }
        public string emr_visit_type { get; set; }
    }
}