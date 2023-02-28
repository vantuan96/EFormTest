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
    public class ClearOldNotificationsJob : IJob
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
            CustomLog.intervaljoblog.Info($"<ClearOldNotificationsJob> Start!");
            try
            {
                var d = DateTime.Now.AddDays(-30);
                unitOfWork.NotificationRepository.HardDeleteRange(
                    unitOfWork.NotificationRepository.AsQueryable().Where(e => e.CreatedAt < d).Take(1000)
                );
                unitOfWork.Commit();
                CustomLog.intervaljoblog.Info($"<ClearOldNotificationsJob> Success!");
            }
            catch (Exception ex)
            {
                CustomLog.intervaljoblog.Info(string.Format("<ClearOldNotificationsJob> Error: {0}", ex));
            }
        }
    }
}