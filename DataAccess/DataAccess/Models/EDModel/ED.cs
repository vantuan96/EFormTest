using DataAccess.Model.BaseModel;
using DataAccess.Models.BaseModel;
using DataAccess.Models.EIOModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.EDModel
{
    public class ED: VisitInfo
    {

        public bool MetDoctor { get; set; }
        [Column(TypeName = "NVARCHAR")]
        [StringLength(50)]
        [Index]
        public string ATSScale { get; set; }
        public DateTime? DischargeDate { get; set; }
        public string HealthInsuranceNumber { get; set; }
        public DateTime? StartHealthInsuranceDate { get; set; }
        public DateTime? ExpireHealthInsuranceDate { get; set; }
        public string Reason { get; set; }
        public string Bed { get; set; }

        public Guid? DischargeInformationId { get; set; }
        [ForeignKey("DischargeInformationId")]
        public virtual EDDischargeInformation DischargeInformation { get; set; }

        public Guid? EDStatusId { get; set; }
        [ForeignKey("EDStatusId")]
        public virtual EDStatus EDStatus { get; set; }

        public Guid? EmergencyRecordId { get; set; }
        [ForeignKey("EmergencyRecordId")]
        public virtual EDEmergencyRecord EmergencyRecord { get; set; }

        public Guid? EmergencyTriageRecordId { get; set; }
        [ForeignKey("EmergencyTriageRecordId")]
        public virtual EDEmergencyTriageRecord EmergencyTriageRecord { get; set; }

        public Guid? HandOverCheckListId { get; set; }
        [ForeignKey("HandOverCheckListId")]
        public virtual EDHandOverCheckList HandOverCheckList { get; set; }

        public Guid? MonitoringChartAndHandoverFormId { get; set; }
        [ForeignKey("MonitoringChartAndHandoverFormId")]
        public virtual EDMonitoringChartAndHandoverForm MonitoringChartAndHandoverForm { get; set; }

        public Guid? ObservationChartId { get; set; }
        [ForeignKey("ObservationChartId")]
        public virtual EDObservationChart ObservationChart { get; set; }

        public Guid? PatientProgressNoteId { get; set; }
        [ForeignKey("PatientProgressNoteId")]
        public virtual EDPatientProgressNote PatientProgressNote { get; set; }

        public Guid? PreOperativeProcedureHandoverChecklistId { get; set; }
        [ForeignKey("PreOperativeProcedureHandoverChecklistId")]
        public virtual EIOPreOperativeProcedureHandoverChecklist PreOperativeProcedureHandoverChecklist { get; set; }

        public Guid? SpongeSharpsAndInstrumentsCountsSheetId { get; set; }
        [ForeignKey("SpongeSharpsAndInstrumentsCountsSheetId")]
        public virtual EIOSpongeSharpsAndInstrumentsCountsSheet SpongeSharpsAndInstrumentsCountsSheet { get; set; }

        public Guid? EDArterialBloodGasTestId { get; set; }
        [ForeignKey("EDArterialBloodGasTestId")]
        public virtual EDArterialBloodGasTest EDArterialBloodGasTest { get; set; }

        public Guid? EDChemicalBiologyTestId { get; set; }
        [ForeignKey("EDChemicalBiologyTestId")]
        public virtual EDChemicalBiologyTest EDChemicalBiologyTest { get; set; }

        public bool IsRetailService { get; set; }
        public Guid? EDAssessmentForRetailServicePatientId { get; set; }
        [ForeignKey("EDAssessmentForRetailServicePatientId")]
        public virtual EIOAssessmentForRetailServicePatient EDAssessmentForRetailServicePatient { get; set; }
        public Guid? EDStandingOrderForRetailServiceId { get; set; }
        [ForeignKey("EDStandingOrderForRetailServiceId")]
        public virtual EIOStandingOrderForRetailService EDStandingOrderForRetailService { get; set; }
        public Guid? EDSkinTestResultId { get; set; }
        [ForeignKey("EDSkinTestResultId")]
        public virtual EIOSkinTestResult EDSkinTestResult { get; set; }
        public Guid? EDConsultationDrugWithAnAsteriskMarkId { get; set; }

        //[ForeignKey("EDConsultationDrugWithAnAsteriskMarkId")]
        //public virtual EDConsultationDrugWithAnAsteriskMark EDConsultationDrugWithAnAsteriskMark { get; set; }
        public virtual ICollection<EDConsultationDrugWithAnAsteriskMark> EDConsultationDrugWithAnAsteriskMarks { get; set; }
        public Guid? EDJointConsultationForApprovalOfSurgeryId { get; set; }
        [ForeignKey("EDJointConsultationForApprovalOfSurgeryId")]
        public virtual EIOJointConsultationForApprovalOfSurgery EDJointConsultationForApprovalOfSurgery { get; set; }
        public Guid? EDAmbulanceRunReportId { get; set; }
        [ForeignKey("EDAmbulanceRunReportId")]
        public virtual EDAmbulanceRunReport EDAmbulanceRunReport { get; set; }

        public Guid? CurrentDoctorId { get; set; }
        [ForeignKey("CurrentDoctorId")]
        public virtual User CurrentDoctor { get; set; }

        public Guid? CurrentNurseId { get; set; }
        [ForeignKey("CurrentNurseId")]
        public virtual User CurrentNurse { get; set; }

        public Guid? EFormVisitId { get; set; }

        public Guid? EIOTestCovid2ConfirmationId { get; set; }
        [ForeignKey("EIOTestCovid2ConfirmationId")]
        public virtual EIOTestCovid2Confirmation EIOTestCovid2Confirmation { get; set; }

        public Guid? HighlyRestrictedAntimicrobialConsultId { get; set; }

        public bool? IsHasFallRiskScreening { get; set; }

        public int? CovidRiskGroup { get; set; }

        public int? SelfHarmRiskScreeningResults { get; set; }

        public string Room { get; set; }
        // [ForeignKey("HighlyRestrictedAntimicrobialConsultId")]
        // public virtual HighlyRestrictedAntimicrobialConsult HighlyRestrictedAntimicrobialConsult { get; set; }

        public int Version { get; set; }
    }
}
