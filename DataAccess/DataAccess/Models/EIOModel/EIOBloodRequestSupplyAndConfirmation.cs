using DataAccess.Model.BaseModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.EIOModel
{
    public class EIOBloodRequestSupplyAndConfirmation: IEntity
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
        public Guid? VisitId { get; set; }
        public string VisitTypeGroupCode { get; set; }
        public int? Number { get; set; }
        public bool IsFrequently { get; set; }
        public string Diagnosis { get; set; }
        public string BloodTypeABO { get; set; }
        public string BloodTypeRH { get; set; }
        public int? TransfusionTime { get; set; }
        public Guid? SpecialtyId { get; set; }
        [ForeignKey("SpecialtyId")]
        public virtual Specialty Specialty { get; set; }
        public DateTime? HeadOfDeptTime { get; set; }
        public Guid? HeadOfDeptId { get; set; }
        [ForeignKey("HeadOfDeptId")]
        public virtual User HeadOfDept { get; set; }
        public Guid? DoctorConfirmId { get; set; }
        [ForeignKey("DoctorConfirmId")]
        public virtual User DoctorConfirm { get; set; }
        public DateTime? DoctorConfirmTime { get; set; }
        public int Version { get; set; } = 1;
    }
}
