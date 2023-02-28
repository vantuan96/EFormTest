using DataAccess.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.EIOModel
{
    public class PROMForCoronaryDisease : VBaseModel
    {
        public Guid? VisitId { get; set; }
        public string VisitType { get; set; }
        public Guid? ProcedureConfirmId { get; set; }
        public Nullable<DateTime> ProcedureConfirmTime { get; set; }
        public string Note { get; set; }
        public string Version { get; set; }
    }
}
