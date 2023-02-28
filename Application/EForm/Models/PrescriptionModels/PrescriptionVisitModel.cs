using DataAccess.Models;
using DataAccess.Models.EDModel;
using DataAccess.Models.IPDModel;
using DataAccess.Models.OPDModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EForm.Models.PrescriptionModels
{
    public class PrescriptionVisitModel
    {
        public Guid VisitId { get; set; }
        public string VisitType { get; set; }
        public string VisitCode { get; set; }
        public Specialty Specialty { get; set; }
        public Customer Customer { get; set; }
        public bool? IsAllergy { get; set; }
        public string Allergy { get; set; }
        public string KindOfAllergy { get; set; }
        public string HealthInsuranceNumber { get; set; }
        public DateTime? ExpireHealthInsuranceDate { get; set; }
        public virtual IPDInitialAssessmentForAdult IPDInitialAssessmentForAdult { get; set; }
        public virtual EDEmergencyTriageRecord EmergencyTriageRecord { get; set; }
        public virtual OPDInitialAssessmentForShortTerm OPDInitialAssessmentForShortTerm { get; set; }
        //public DateTime ExaminationTime { get; set; } // Bổ sung cái này nếu cần để sort, tìm hồ sơ mới nhất
    }
}
