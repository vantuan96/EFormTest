using DataAccess.Model.BaseModel;
using DataAccess.Models.BaseModel;
using DataAccess.Models.EIOModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.OPDModel
{
    public class OPD : VisitInfo
    {
        public DateTime? DischargeDate { get; set; }
        public string HealthInsuranceNumber { get; set; }
        public DateTime? StartHealthInsuranceDate { get; set; }
        public DateTime? ExpireHealthInsuranceDate { get; set; }
        public bool IsBooked { get; set; }
        public bool IsTelehealth { get; set; }
        public DateTime? BookingTime { get; set; }
        public Guid? EDStatusId { get; set; }
        [ForeignKey("EDStatusId")]
        public virtual EDStatus EDStatus { get; set; }

        public Guid? ClinicId { get; set; }
        [ForeignKey("ClinicId")]
        public virtual Clinic Clinic { get; set; }
        public Guid? OPDFallRiskScreeningId { get; set; }
        [ForeignKey("OPDFallRiskScreeningId")]
        public virtual OPDFallRiskScreening OPDFallRiskScreening { get; set; }

        public Guid? OPDInitialAssessmentForOnGoingId { get; set; }
        [ForeignKey("OPDInitialAssessmentForOnGoingId")]
        public virtual OPDInitialAssessmentForOnGoing OPDInitialAssessmentForOnGoing { get; set; }

        public Guid? OPDInitialAssessmentForShortTermId { get; set; }
        [ForeignKey("OPDInitialAssessmentForShortTermId")]
        public virtual OPDInitialAssessmentForShortTerm OPDInitialAssessmentForShortTerm { get; set; }

        public Guid? OPDInitialAssessmentForTelehealthId { get; set; }
        [ForeignKey("OPDInitialAssessmentForTelehealthId")]
        public virtual OPDInitialAssessmentForTelehealth OPDInitialAssessmentForTelehealth { get; set; }

        public Guid? OPDOutpatientExaminationNoteId { get; set; }
        [ForeignKey("OPDOutpatientExaminationNoteId")]
        public virtual OPDOutpatientExaminationNote OPDOutpatientExaminationNote { get; set; }

        public Guid? OPDHandOverCheckListId { get; set; }
        [ForeignKey("OPDHandOverCheckListId")]
        public virtual OPDHandOverCheckList OPDHandOverCheckList { get; set; }

        public Guid? OPDPatientProgressNoteId { get; set; }
        [ForeignKey("OPDPatientProgressNoteId")]
        public virtual OPDPatientProgressNote OPDPatientProgressNote { get; set; }

        public Guid? OPDObservationChartId { get; set; }
        [ForeignKey("OPDObservationChartId")]
        public virtual OPDObservationChart OPDObservationChart { get; set; }

        public bool IsRetailService { get; set; }
        public Guid? EIOAssessmentForRetailServicePatientId { get; set; }
        [ForeignKey("EIOAssessmentForRetailServicePatientId")]
        public virtual EIOAssessmentForRetailServicePatient EIOAssessmentForRetailServicePatient { get; set; }
        public Guid? EIOStandingOrderForRetailServiceId { get; set; }
        [ForeignKey("EIOStandingOrderForRetailServiceId")]
        public virtual EIOStandingOrderForRetailService EIOStandingOrderForRetailService { get; set; }

        public Guid? GroupId { get; set; }

        public Guid? AuthorizedDoctorId { get; set; }
        [ForeignKey("AuthorizedDoctorId")]
        public virtual User AuthorizedDoctor { get; set; }

        public Guid? EFormVisitId { get; set; }

        public Guid? EIOTestCovid2ConfirmationId { get; set; }
        [ForeignKey("EIOTestCovid2ConfirmationId")]
        public virtual EIOTestCovid2Confirmation EIOTestCovid2Confirmation { get; set; }

        public bool? IsHasFallRiskScreening { get; set; }
        public bool IsAnesthesia { get; set; }
        public int Version { get; set; }
    }
}
