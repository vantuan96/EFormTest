using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SyncManager
{
    class Program
    {
        static void Main(string[] args)
        {
            //SyncDataService sv = new SyncDataService();
            //sv.OnDebug();
            //System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new SyncDataService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
