using DataAccess.Model.BaseModel;
using DataAccess.Models.EDModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    public class Order: IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string DeletedBy { get; set; }
        public string Drug { get; set; }
        public string Quantity { get; set; }
        public string Speed { get; set; }
        public string Dosage { get; set; }
        public string Route { get; set; }
        public DateTime? UsedAt { get; set; }
        public string MedicalStaffName { get; set; }
        public string DoctorConfirm { get; set; }
        public bool IsConfirm { get; set; }
        public bool Status { get; set; }
        public DateTime? LastDoseDate { get; set; }
        public string OrderType { get; set; }
        public string Note { get; set; }
        public string Reason { get; set; }
        public string Concentration { get; set; }
        public Guid? VisitId { get; set; }
        public DateTime? DoctorTime { get; set; }
        public Guid? DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public virtual User Doctor { get; set; }
        public DateTime? NurseTime { get; set; }
        public Guid? NurseId { get; set; }
        [ForeignKey("NurseId")]
        public virtual User Nurse { get; set; }
        public Guid? EmergencyTriageRecordId { get; set; }
        [ForeignKey("EmergencyTriageRecordId")]
        public virtual EDEmergencyTriageRecord EmergencyTriageRecord { get; set; }
        public Guid? StandingOrderMasterDataId { get; set; }
        [ForeignKey("StandingOrderMasterDataId")]
        public virtual StandingOrderMasterData StandingOrderMasterData { get; set; }
        public Guid? MedicationMasterdataId { get; set; }
        [ForeignKey("MedicationMasterdataId")]
        public virtual MedicationMasterdata MedicationMasterdata { get; set; }
        public string MedicationPlan { get; set; }
        public Guid? FormId { get; set; }
    }
}
