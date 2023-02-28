using DataAccess.Repository;
using EForm.Common;
using Quartz;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EForm.ScheduleJobs
{
    public class ClearUnuseLog: IJob
    {
        private IUnitOfWork unitOfWork = new EfUnitOfWork();

        public void Execute(IJobExecutionContext context)
        {
            CustomLog.intervaljoblog.Info($"<Clear unuse> Start job");
            try
            {
                unitOfWork.LogRepository.HardDeleteRange(
                    unitOfWork.LogRepository.AsQueryable().Where(e => string.IsNullOrEmpty(e.Action)).Take(1000)
                );
                unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                CustomLog.intervaljoblog.Error($"<Clear unuse> Error clear unuse job!({ex.ToString()})");
            }
            CustomLog.intervaljoblog.Info("<Clear unuse> End clear unuse job!");
        }

    }
}