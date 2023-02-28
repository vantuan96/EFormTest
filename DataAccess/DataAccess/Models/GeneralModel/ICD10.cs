using DataAccess.Model.BaseModel;
using DataAccess.Models.BaseModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    public class ICD10:IEntity
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
        public string EnName { get; set; }
        public string ViName { get; set; }
        public string ViNameWithoutSign { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        [Index]
        public string Code { get; set; }
        public string GroupCode { get; set; }
        public bool IsChronic { get; set; }
        public Guid? ICDSpecialtyId { get; set; }
        [ForeignKey("ICDSpecialtyId")]
        public virtual ICDSpecialty ICDSpecialty { get; set; }
    }
    public class ICD10Visit : VBaseModel
    {
        public Guid VisitId { get; set; }
        public Guid? FormId { get; set; }
        public string Type { get; set; }
        public string MasterDataCode { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string EnName { get; set; }
        public string Diagnosis { get; set; }
    }
}
