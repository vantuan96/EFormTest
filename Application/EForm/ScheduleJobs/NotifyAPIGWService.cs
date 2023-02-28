using DataAccess.Repository;
using EForm.Common;
using EForm.Utils;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EForm.ScheduleJobs
{
    public class NotifyAPIGWService: IJob
    {
        private IUnitOfWork unitOfWork = new EfUnitOfWork();

        public void Execute(IJobExecutionContext context)
        {
            CustomLog.intervaljoblog.Info("<Notify APIGW> Start job!");
            var noti = unitOfWork.SystemNotificationRepository.Find(
                e => !e.IsDeleted &&
                e.Service == Constant.SERVICE_APIGW
            ).OrderBy(e => e.Scope).ToList();
            var noti_cnt = noti.Count();
            if (noti_cnt < 1)
                return;

            var down = noti.Where(e => e.Status == 0).ToList();
            if (down.Count() > 0)
            {
                var group = GroupError(down);
                var message = GenerateMessage(group);
                var is_success = SendEmail("Lỗi apigw", message);
                if (is_success)
                    ChangeNotificationStatus(down);
                return;
            }

            var sent = noti.Where(e => e.Status == 1).ToList();
            var up = noti.Where(e => e.Status == 2).ToList();
            if (up.Count() < 1 || sent.Count() > 0)
                return;

            var is_suc = SendEmail("Thông báo apigw đã hoạt động bình thường", "API đã hoạt động bình thường");
            if (is_suc)
                RemoveNotification(up);
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
        private void RemoveNotification(dynamic up)
        {
            //foreach (var u in up)
            //    unitOfWork.SystemNotificationRepository.HardDelete(u);
            //unitOfWork.Commit();
        }
        private bool SendEmail(string subject, string message)
        {
            var email = new Email();
            var receiver = unitOfWork.SystemConfigRepository.FirstOrDefault(e => !e.IsDeleted)?.NotificationEmail;
            if (string.IsNullOrEmpty(receiver))
                return false;

            email.ToMany(receiver.Split(';'));
            email.ChangeSubject(subject);
            email.ChangeBody(message);
            try
            {
                email.Send();
                CustomLog.intervaljoblog.Info("<Notify APIGW> Send email Success");
            }
            catch(Exception ex)
            {
                CustomLog.intervaljoblog.Info($"<Notify APIGW> Send email error: {ex.ToString()}");
                return false;
            }
            return true;
        }
    }
}