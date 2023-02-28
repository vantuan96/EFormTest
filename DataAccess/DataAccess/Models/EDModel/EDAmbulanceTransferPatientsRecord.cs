using DataAccess.Model.BaseModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.EDModel
{
    public class EDAmbulanceTransferPatientsRecord : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? Time { get; set; }
        public string BP { get; set; }
        public string Pulse { get; set; }
        public string RespRate { get; set; }
        public string PatternOfRespiration { get; set; }
        public string SpO2 { get; set; }
        public string HR { get; set; }
        public string Procedure { get; set; }
        public string Drug { get; set; }
        public string Dose { get; set; }
        public string Route { get; set; }
        public Guid? MedicationMasterdataId { get; set; }
        [ForeignKey("MedicationMasterdataId")]
        public virtual MedicationMasterdata MedicationMasterdata { get; set; }
        public Guid? EDAmbulanceRunReportId { get; set; }
        [ForeignKey("EDAmbulanceRunReportId")]
        public virtual EDAmbulanceRunReport EDAmbulanceRunReport { get; set; }
    }
}
