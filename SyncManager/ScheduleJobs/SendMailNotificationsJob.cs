using Common;
using DataAccess.Models;
using DataAccess.Models.GeneralModel;
using DataAccess.Repository;
using EForm.Client;
using EForm.Models;
using EForm.Utils;
using Quartz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncManager.ScheduleJobs
{
    public class SendMailNotificationsJob : IJob
    {
        protected IUnitOfWork unitOfWork = new EfUnitOfWork();
        private readonly string mailType = "APPDATETORETURNRESULT";
        public void Execute(IJobExecutionContext context)
        {
            DoJob();
        }

        public void Execute()
        {
            DoJob();
        }

        private async void DoJob()
        {

            CustomLogs.intervaljoblog.Info($"<SendMailNotificationsJob> Start!");
            if (ConfigHelper.CF_SendMailNotifications_CS_is_off)
            {
                CustomLogs.intervaljoblog.Info($"<SendMailNotificationsJob> is Off!");
                return;
            }

            var is_send_mail = ConfigurationManager.AppSettings["IS_SEND_MAIL_APPOINTMENT"] != null ? ConfigurationManager.AppSettings["IS_SEND_MAIL_APPOINTMENT"].ToString() : "False";
            if (is_send_mail != "True")
            {
                return;
            }
            try
            {
                var curent_date = DateTime.Now;
                var appointmentDateResult = unitOfWork.OPDOutpatientExaminationNoteRepository.Find(x => !x.IsDeleted && x.AppointmentDateResult != null
                                                                                                                     && EntityFunctions.AddHours(x.AppointmentDateResult.Value, -24).Value.Year == curent_date.Year
                                                                                                                     && EntityFunctions.AddHours(x.AppointmentDateResult.Value, -24).Value.Month == curent_date.Month
                                                                                                                     && EntityFunctions.AddHours(x.AppointmentDateResult.Value, -24).Value.Day == curent_date.Day
                                                                                                                     && EntityFunctions.AddHours(x.AppointmentDateResult.Value, -24).Value.Hour == curent_date.Hour
                                                                                                                     ).ToList();
                List<SendMailNotification> sendMailNotificationDatas = unitOfWork.SendMailNotificationRepository.Find(x => !x.IsDeleted).ToList();
                foreach (var appResult in appointmentDateResult)
                {
                    await Task.Delay(1000);
                    var mailSend = sendMailNotificationDatas.FirstOrDefault(x => x.FormId == appResult.Id && x.Type == "APPDATETORETURNRESULT");
                    if (mailSend == null || (mailSend.Status == "ERROR" && mailSend.ErrorCount < 3))
                    {
                        var opd = unitOfWork.OPDRepository.FirstOrDefault(x => x.OPDOutpatientExaminationNoteId == appResult.Id);
                        //mong muốn: không gửi mail cho BS khi trạng thái NB chờ KQXN("OPDWR")
                        if (opd.EDStatus.Code == "OPDWR")
                        {
                            string PID = opd?.Customer?.PID;
                            string CustomerName = opd?.Customer?.Fullname;
                            string TimeNT = "";
                            if (appResult.ExaminationTime != null)
                            {
                                TimeNT = appResult.ExaminationTime.Value.ToString("HH:mm dd/MM/yyyy");
                            }
                            if (!string.IsNullOrEmpty(opd.PrimaryDoctor?.EmailAddress))
                            {
                                SendEmailNotificationResult(appResult.Id, opd.PrimaryDoctor, CustomerName, PID, TimeNT, "PrimaryDoctor");
                            }
                            if (!string.IsNullOrEmpty(opd.AuthorizedDoctor?.EmailAddress))
                            {
                                SendEmailNotificationResult(appResult.Id, opd.PrimaryDoctor, CustomerName, PID, TimeNT, "PrixmaryDoctor");
                            }
                        }    
                        
                    }
                }
                CustomLogs.intervaljoblog.Info($"<SendMailNotificationsJob> Success!");
            }
            catch (Exception ex)
            {
                CustomLogs.intervaljoblog.Info(string.Format("<SendMailNotificationsJob> Error: {0}", ex));
            }
            unitOfWork.Dispose();
        }
        private void SendEmailNotificationResult(Guid formId, User receiver, string CustomerName, string PID, string TimeNT, string type)
        {
            string subject = unitOfWork.AppConfigRepository.FirstOrDefault(x => x.Key == "SUBJECT_SEND_MAIL_APPOINTMENT")?.Value;
            string body = unitOfWork.AppConfigRepository.FirstOrDefault(x => x.Key == "BODY_SEND_MAIL_APPOINTMENT")?.Value;

            if (!string.IsNullOrEmpty(body))
            {
                body = body.Replace("{FullNameDoctor}", receiver?.Fullname).Replace("{CustomerName}", CustomerName).Replace("{PID}", PID).Replace("{TimeNT}", TimeNT);
            }

            var email_test_1 = ConfigurationManager.AppSettings["EMAIL_TEST_1"] != null ? ConfigurationManager.AppSettings["EMAIL_TEST_1"].ToString() : "";
            var email_test_2 = ConfigurationManager.AppSettings["EMAIL_TEST_2"] != null ? ConfigurationManager.AppSettings["EMAIL_TEST_2"].ToString() : "";
            SendMailNotification mail = new SendMailNotification
            {
                ReceiverId = receiver.Id,
                FormId = formId,
                Type = mailType,
                Subject = subject,
                Body = body,
                To = receiver.EmailAddress
            };
            try
            {
                var email = new Email();
                //email.ToOne("v.thangdc3@vinmec.com");

                if (string.IsNullOrEmpty(email_test_1) && string.IsNullOrEmpty(email_test_2))
                {
                    // CustomLog.intervaljoblog.Info("SENDING TO2 " + receiver.EmailAddress + body);
                    email.ToOne(receiver.EmailAddress);
                }
                else
                {
                    // CustomLog.intervaljoblog.Info("SENDING TO " + (type == "PrimaryDoctor" ? email_test_1 : email_test_2) + body);
                    email.ToOne(type == "PrimaryDoctor" ? email_test_1 : email_test_2);
                }

                ////email.ToMany(receiver.Split(';'));
                email.ChangeSubject(subject);
                email.ChangeBody(body);
                // var is_send_mail = unitOfWork.AppConfigRepository.FirstOrDefault(x => x.Key == "IS_SEND_MAIL_APPOINTMENT");
                email.Send();

                mail.Status = "SENDED";
                unitOfWork.SendMailNotificationRepository.Add(mail);
                unitOfWork.Commit();
                CustomLogs.intervaljoblog.Info("<Notify APIGW> Send email Success");
            }
            catch (Exception ex)
            {

                var lastError = unitOfWork.SendMailNotificationRepository.FirstOrDefault(x => x.FormId == formId && x.Type == mailType && x.ReceiverId == receiver.Id && x.Status == "ERROR");
                if (lastError != null)
                {
                    lastError.ErrorCount++;
                    lastError.ErrorMessenge += "{" + DateTime.Now.ToString() + " - " + ex.ToString() + "} ";
                    unitOfWork.SendMailNotificationRepository.Update(lastError);
                }
                else
                {
                    mail.Status = "ERROR";
                    mail.ErrorCount = 1;
                    mail.ErrorMessenge = "{" + DateTime.Now.ToString() + " - " + ex.ToString() + "} ";
                    unitOfWork.SendMailNotificationRepository.Add(mail);
                }
                unitOfWork.Commit();
                CustomLogs.intervaljoblog.Info($"<Notify APIGW> Send email error: {ex.ToString()}");
            }
        }
    }
}