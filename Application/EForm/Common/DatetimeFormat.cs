using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EForm.Common
{
    public class DatetimeFormat
    {
        public DateTime stringToDatetime (string str_date, string format)
        {
            return DateTime.ParseExact(str_date, format, null);
        }
    }
}