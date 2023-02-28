using DataAccess.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.IPDModel
{
    public class IPDThrombosisRiskFactorAssessmentForGeneralSurgeryPatient : VBaseModel
    {
        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public string CapriniScoreCalculator { get; set; }
        public string IndividualBleedingRiskScore { get; set; }
        public string BaselineSurgicalBleedingRisk { get; set; }
        public string AntiFreeze { get; set; }
        public string MechanicalProphylaxis { get; set; }
        public string ThromboembolismProphylaxis { get; set; }
        public Guid VisitId { get; set; }
    }
}
