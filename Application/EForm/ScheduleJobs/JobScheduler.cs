using Quartz;
using Quartz.Impl;

namespace EForm.ScheduleJobs
{
    public class JobScheduler
    {
        public static void Start()

        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            IJobDetail notify_job = JobBuilder.Create<NotifyBlock24h>().Build();
            ITrigger notify_trigger = TriggerBuilder.Create()
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(10)
                    .RepeatForever())
            .Build();
            scheduler.ScheduleJob(notify_job, notify_trigger);

            IJobDetail clear_unuse_job = JobBuilder.Create<ClearUnuseLog>().Build();
            ITrigger clear_unuse_trigger = TriggerBuilder.Create()
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(15)
                    .RepeatForever())
            .Build();
            scheduler.ScheduleJob(clear_unuse_job, clear_unuse_trigger);

            //IJobDetail apigw_job = JobBuilder.Create<NotifyAPIGWService>().Build();
            //ITrigger clear_apigw_trigger = TriggerBuilder.Create()
            //    .StartNow()
            //    .WithSimpleSchedule(x => x
            //        .WithIntervalInMinutes(3)
            //        .RepeatForever())
            //.Build();
            //scheduler.ScheduleJob(apigw_job, clear_apigw_trigger);
        }
    }
}