using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EForm.Common
{
    public class IsServiceDiagnosticReporting
    {
        public static bool IsDiagnosticReporting(string code)
        {
            var service_group = GetAppConfig("SERVICE_GROUP_DIAGNOSTICREPORTING");
            var service_code = GetAppConfig("SERVICE_CODE_DIAGNOSTICREPORTING");
            return service_groups.Contains(group?.Code) || service_codes.Contains(item.ServiceCode) || rootServiceGroupCode.StartsWith("F\\FE");
        }
    }
}