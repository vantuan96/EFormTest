using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class DatetimeHelper
    {
        public static string FormatDateVisitCode(string rawData)
        {
            if (!string.IsNullOrEmpty(rawData))
            {
                DateTime date = DateTime.ParseExact(rawData, "MM/dd/yyyy HH:mm:ss", null);
                return date.ToString(Constants.TIME_DATE_FORMAT_WITHOUT_SECOND);
            }
            return rawData;
        }
    }
}
