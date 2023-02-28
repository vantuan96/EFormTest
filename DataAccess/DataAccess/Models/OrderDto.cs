using DataAccess.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.DTOs
{
    public class OrderDto : IEntity
    {
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
        public DateTime? NurseTime { get; set; }
        public Guid? NurseId { get; set; }
        public Guid? EmergencyTriageRecordId { get; set; }
        public Guid? StandingOrderMasterDataId { get; set; }
        public Guid? MedicationMasterdataId { get; set; }
        public string MedicationPlan { get; set; }
    }
}
