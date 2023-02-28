using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Common
{
    public class Constant
    {
        public struct AdminRoles
        {
            public const string SuperAdmin = "superadmin";
            public const string ManageUser = "manageuser";
            public const string ManageData = "managedata";
            public const string ManageRole = "managerole";
            public const string ManageUnlock = "manageunlock";
            public const string ViewSysLog = "viewsyslog";
        }

        public const string SetupClinics = "SETUPCLINICS";
    }
}