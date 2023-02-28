using DataAccess.Model.BaseModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.EIOModel
{
    public class EIOMortalityReportMember: IEntity
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
        public Guid? MemberId { get; set; }
        [ForeignKey("MemberId")]
        public virtual User Member { get; set; }
        public Guid? EDMortalityReportId { get; set; }
        [ForeignKey("EDMortalityReportId")]
        public virtual EIOMortalityReport EDMortalityReport { get; set; }
        public DateTime? ConfirmTime { get; set; }
        public bool IsNotMember { get; set; }
    }
}
