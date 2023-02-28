using DataAccess.Repository;
using EForm.Common;
using EForm.Utils;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using EForm.Client;
using DataAccess.Models.GeneralModel;

namespace SyncManager.ScheduleJobs
{
    public class NotifyAPIGWService : IJob
    {
        private IUnitOfWork unitOfWork = new EfUnitOfWork();

        public void Execute(IJobExecutionContext context)
        {

            CustomLog.intervaljoblog.Info($"<NotifyAPIGWService> Start!");
            if (Common.ConfigHelper.CF_NotifyAPIGW_is_off)
            {
                CustomLog.intervaljoblog.Info($"<NotifyAPIGWService> is Off!");
                return;
            }

            string msg_content = string.Empty;
            bool has_alrert = false;
            try
            {
                HISClient.RequestPostAsyncAPI(ConfigurationManager.AppSettings["API_PUBLIC_PING"].ToString(), ConfigurationManager.AppSettings["API_PUBLIC_PING_AUTHOR"].ToString());
                var ig = ConfigurationManager.AppSettings["SERVICE_APIGW_IG"].ToString();
                var noti = unitOfWork.SystemNotificationRepository.Find(e => !e.IsDeleted && e.Service == Constant.SERVICE_APIGW && !ig.Contains(e.Scope)).OrderBy(e => e.Scope).ToList();
                
                var down = noti.Where(e => e.Status == 0).ToList();
                if (down.Count() > 0)
                {
                    var group = GroupError(down);
                    msg_content += GenerateMessage(group);
                    has_alrert = true;
                }

                List<string> nodeOthers = new List<string>();
                var apiNodes = unitOfWork.AppConfigRepository.FirstOrDefault(x => x.Key == "API_NODES" && !x.IsDeleted);
                if (apiNodes != null && !string.IsNullOrEmpty(apiNodes?.Value))
                {
                    string[] nodes = apiNodes.Value.Split(',');
                    var is_app_work = CheckAppWork(nodes, ref nodeOthers);
                    if (!is_app_work) has_alrert = true;
                }
                
                msg_content += GenerateMessageApp(nodeOthers);

                if (has_alrert)
                {
                    var is_success = false;
                    var curre = DateTime.Now.AddMinutes(-5);

                    var notis = unitOfWork.SendMailNotificationRepository.Find(x => x.Type == "EMRALERT" && x.CreatedAt >= curre).ToList();

                    var sended_email = notis.Where(x => x.Status == "SENDED").Count();

                    if (sended_email == 0) {
                        is_success = SendEmail("[EMR ALERT]", msg_content);
                        var notification = notis.Where(x => x.Status == "PENDING").FirstOrDefault();
                        if (notification == null)
                        {
                            SendMailNotification sendMailNotification = new SendMailNotification()
                            {
                                Id = Guid.NewGuid(),
                                ReceiverId = Guid.Empty,
                                Subject = "[EMR ALERT]",
                                Body = msg_content,
                                FormId = Guid.Empty,
                                Type = "EMRALERT",
                                Status = is_success ? "SENDED" : "PENDING"
                            };
                            unitOfWork.SendMailNotificationRepository.Add(sendMailNotification);
                        } else
                        {
                            notification.Status = is_success ? "SENDED" : "PENDING";
                        }
                    }
                    if (is_success)
                    {
                        ChangeNotificationStatus(down);
                    }
                    unitOfWork.Commit();

                }
            }
            catch (Exception ex)
            {
                CustomLog.intervaljoblog.Error(string.Format("ALERT job error {0}", ex.ToString()));
            }


        }
        private Dictionary<string, string> GroupError(dynamic down)
        {
            var group = new Dictionary<string, string>();
            foreach (var d in down)
            {
                if (!group.ContainsKey(d.Content))
                    group[d.Content] = d.Subject;
                else
                    group[d.Content] = group[d.Content] + "<br/>" + d.Subject;
            }
            return group;
        }
        private string GenerateMessage(Dictionary<string, string> group)
        {
            var message = "<style>table,th,td{border:1.5px solid black;border-collapse:collapse;padding:5px;text-align:left}</style><table><tr><th width='66%'>API</th><th>Lỗi</th></tr>";
            foreach (var k in group.Keys)
                message += $"<tr><td>{group[k]}</td><td>{k}</td></tr>";
            message += "</table>";
            return message;
        }
        private void ChangeNotificationStatus(dynamic down)
        {
            foreach (var d in down)
            {
                d.Status = 1;
                unitOfWork.SystemNotificationRepository.Update(d);
            }
            unitOfWork.Commit();
        }
        private bool SendEmail(string subject, string message)
        {
            var email = new Email();
            var receiver = unitOfWork.AppConfigRepository.FirstOrDefault(e => !e.IsDeleted && e.Key == "RECEIVER_ALERT_EMAIL")?.Value;
            if (string.IsNullOrEmpty(receiver))
                return true;

            email.ToMany(receiver.Split(';'));
            email.ChangeSubject(subject);
            email.ChangeBody(message);
            try
            {
                email.Send();
                CustomLog.intervaljoblog.Info("<Notify APIGW> Send email Success");
            }
            catch (Exception ex)
            {
                CustomLog.intervaljoblog.Info($"<Notify APIGW> Send email error: {ex.ToString()}");
                return false;
            }
            return true;
        }
        private string GenerateMessageApp(List<string> nodeOthers)
        {
            var message = "";
            if (nodeOthers.Count > 0)
            {
                message = "<style>table,th,td{border:1.5px solid black;border-collapse:collapse;padding:5px;text-align:left}</style><table><tr><th width='66%'>Application</th><th>Error</th></tr>";
                string node = "";
                foreach (var item in nodeOthers)
                {
                    node += item + ",";
                }
                node = node.Substring(0, node.Length - 1);
                message += $"<tr><td>ERROR</td><td>{node} is offline</td></tr>";
                message += "</table>";
            }
            return message;
        }
        private bool CheckAppWork(string[] nodes, ref List<string> nodeOther)
        {
            bool isWork = false;
            DateTime currentDatetime = DateTime.Now.AddMinutes(-30);
            var logTmps = unitOfWork.LogTmpRepository.Find(x => x.CreatedAt > currentDatetime).Select(x => x.Ip).ToArray();
            var result = nodes.Except(logTmps);
            if (result.Count() == 0)
            {
                isWork = true;
            }
            else
            {
                foreach (var item in result)
                {
                    nodeOther.Add(item);
                }
            }
            return isWork;
        }
    }
}