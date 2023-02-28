using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMRModels
{
    public class LogerModel
    {
        public class LogModel
        {
            public DateTime? StartAt { get; set; }
            public DateTime? EndAt { get; set; }
            public string URI { get; set; }
            public string RequestTime { get; set; }
            public string Request { get; set; }
            public string Response { get; set; }
            public string Username { get; set; }
        }
    }
}
