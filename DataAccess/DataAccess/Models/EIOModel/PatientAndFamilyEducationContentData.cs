using DataAccess.Model.BaseModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    public class PatientAndFamilyEducationContentData: IEntity
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
        public string Code { get; set; }
        public string Value { get; set; }
        public string EnValue { get; set; }
        public Guid? PatientAndFamilyEducationContentId { get; set; }
        [ForeignKey("PatientAndFamilyEducationContentId")]
        public virtual PatientAndFamilyEducationContent PatientAndFamilyEducationContent { get; set; }
    }
}
