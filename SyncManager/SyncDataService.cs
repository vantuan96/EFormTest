using Common;
using SyncManager.ScheduleJobs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SyncManager
{
    partial class SyncDataService : ServiceBase
    {
        public SyncDataService()
        {
            InitializeComponent();
        }

        public void OnDebug()
        {
            OnStart(null);
        }

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
            try
            {
                //Using Quartz to create job
                JobScheduler.Start();
                CustomLogs.intervaljoblog.Info("SynDataManager was started");

            }
            catch (Exception ex)
            {
                CustomLogs.errorlog.Info(string.Format("SynDataManager start Error: {0}", ex));
            }
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
            try
            {
                CustomLogs.intervaljoblog.Info("SynDataManager was stoped");
            }
            catch (Exception ex)
            {
                CustomLogs.errorlog.Info(string.Format("SynDataManager stop Error: {0}", ex));
            }
        }
    }
}
