using DataAccess.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.EIOModel
{
    public class SurgeryAndProcedureSummaryV3 : VBaseModel
    {
        public Guid? VisitId { get; set; }
        public string VisitType { get; set; }
        public Guid? ProcedureDoctorId { get; set; }
        public Nullable<DateTime> ProcedureTime { get; set; }
        public string Note { get; set; }
        public string Version { get; set; }
    }
}
