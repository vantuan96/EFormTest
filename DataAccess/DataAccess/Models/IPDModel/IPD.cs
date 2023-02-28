using DataAccess.Model.BaseModel;
using DataAccess.Models.BaseModel;
using DataAccess.Models.EIOModel;
using DataAccess.Models.OPDModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.IPDModel
{
    public class IPD: VisitInfo
    {
        public DateTime? DischargeDate { get; set; }
        public string HealthInsuranceNumber { get; set; }
        public DateTime? StartHealthInsuranceDate { get; set; }
        public DateTime? ExpireHealthInsuranceDate { get; set; }
        public bool PermissionForVisitor { get; set; } = true;
        public string Reason { get; set; }
        public string Bed { get; set; }
        public Guid? EDStatusId { get; set; }
        [ForeignKey("EDStatusId")]
        public virtual EDStatus EDStatus { get; set; }
        public Guid? ClinicId { get; set; }
        [ForeignKey("ClinicId")]
        public virtual Clinic Clinic { get; set; }
        public Guid? HandOverCheckListId { get; set; }
        [ForeignKey("HandOverCheckListId")]
        public virtual IPDHandOverCheckList HandOverCheckList { get; set; }

        public Guid? IPDPatientProgressNoteId { get; set; }
        [ForeignKey("IPDPatientProgressNoteId")]
        public virtual IPDPatientProgressNote IPDPatientProgressNote { get; set; }

        public Guid? IPDInitialAssessmentForAdultId { get; set; }
        [ForeignKey("IPDInitialAssessmentForAdultId")]
        public virtual IPDInitialAssessmentForAdult IPDInitialAssessmentForAdult { get; set; }

        public Guid? IPDInitialAssessmentForChemotherapyId { get; set; }
        [ForeignKey("IPDInitialAssessmentForChemotherapyId")]
        public virtual IPDInitialAssessmentForChemotherapy IPDInitialAssessmentForChemotherapy { get; set; }

        public Guid? IPDInitialAssessmentForFrailElderlyId { get; set; }
        [ForeignKey("IPDInitialAssessmentForFrailElderlyId")]
        public virtual IPDInitialAssessmentForFrailElderly IPDInitialAssessmentForFrailElderly { get; set; }

        public Guid? IPDMedicalRecordId { get; set; }
        [ForeignKey("IPDMedicalRecordId")]
        public virtual IPDMedicalRecord IPDMedicalRecord { get; set; }

        public Guid? IPDDischargeMedicalReportId { get; set; }
        [ForeignKey("IPDDischargeMedicalReportId")]
        public virtual IPDDischargeMedicalReport IPDDischargeMedicalReport { get; set; }
        public Guid? IPDReferralLetterId { get; set; }
        [ForeignKey("IPDReferralLetterId")]
        public virtual IPDReferralLetter IPDReferralLetter { get; set; }
        public Guid? IPDTransferLetterId { get; set; }
        [ForeignKey("IPDTransferLetterId")]
        public virtual IPDTransferLetter IPDTransferLetter { get; set; }

        public Guid? EFormVisitId { get; set; }

        public Guid? HighlyRestrictedAntimicrobialConsultId { get; set; }
        //[ForeignKey("HighlyRestrictedAntimicrobialConsultId")]
        //public virtual HighlyRestrictedAntimicrobialConsult HighlyRestrictedAntimicrobialConsult { get; set; }
        public Guid? OPDObservationChartId { get; set; }
        [ForeignKey("OPDObservationChartId")]
        public virtual OPDObservationChart ObservationChart { get; set; }
        public string Room { get; set; }
        public Guid? IPDConsultationDrugWithAnAsteriskMarkId { get; set; }

        //[ForeignKey("IPDConsultationDrugWithAnAsteriskMarkId")]
        //public virtual IPDConsultationDrugWithAnAsteriskMark IPDConsultationDrugWithAnAsteriskMark { get; set; }
        public bool IsDraft { get; set; } = false;
        public virtual ICollection<IPDConsultationDrugWithAnAsteriskMark> IPDConsultationDrugWithAnAsteriskMarks { get; set; }
        public int Version { get; set; }
    }
}
