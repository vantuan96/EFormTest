using DataAccess.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.GeneralModel
{
    public class ChargeDraft : VBaseModel
    {
        public string Title { get; set; }
        public string PID { get; set; }
        public string VisitCode { get; set; }
        public string Note { get; set; }
        public string RawData { get; set; }
    }
}
