using Common;
using DataAccess.Models.GeneralModel;
using DataAccess.Repository;
using EForm.Client;
using EForm.Models;
using Quartz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncManager.ScheduleJobs
{
    public class ClearOldTmpLogAndNoti : IJob
    {
        protected IUnitOfWork unitOfWork = new EfUnitOfWork();
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
            CustomLogs.intervaljoblog.Info($"<ClearOldTmpLogAndNoti> Start!");
            
            if (ConfigHelper.CF_ClearOldNotifications_CS_is_off)
            {
                CustomLogs.intervaljoblog.Info($"<ClearOldTmpLogAndNoti> is Off!");
                return;
            }
            try
            {
                var d = DateTime.Now.AddDays(-30);
                unitOfWork.LogTmpRepository.HardDeleteRange(
                    unitOfWork.LogTmpRepository.AsQueryable().Where(e => e.CreatedAt < d).Take(1000)
                );

                unitOfWork.NotificationRepository.HardDeleteRange(
                    unitOfWork.NotificationRepository.AsQueryable().Where(e => e.CreatedAt < d).Take(1000)
                );
                //unitOfWork.SendMailNotificationRepository.HardDeleteRange(
                //    unitOfWork.SendMailNotificationRepository.AsQueryable().Where(e => e.CreatedAt < d).Take(1000)
                //);
                unitOfWork.Commit();
                CustomLogs.intervaljoblog.Info($"<ClearOldTmpLogAndNoti> Success!");
            }
            catch (Exception ex)
            {
                CustomLogs.intervaljoblog.Info(string.Format("<ClearOldTmpLogAndNoti> Error: {0}", ex));
            }
        }
    }
}