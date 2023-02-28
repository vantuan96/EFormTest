using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EForm.Models.IPDModels
{
    public class IPDThrombosisRiskFactorAssessmentParams: PagingParameterModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public Guid IPDId { get; set; }
        public string Assessor { get; set; } //PID
        public string FormCode { get; set; }
    }
    public class AlliedServiceParamesterModel : PagingParameterModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public Guid IPDId { get; set; }
        public string PID { get; set; } //PID
        public string VisitCode { get; set; }
        public Guid? ServiceGroupId { get; set; }
}
}
