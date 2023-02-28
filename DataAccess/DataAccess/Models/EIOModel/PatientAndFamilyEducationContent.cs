using DataAccess.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    public class PatientAndFamilyEducationContent : IEntity
    {
        public PatientAndFamilyEducationContent()
        {
            this.PatientAndFamilyEducationContentDatas = new HashSet<PatientAndFamilyEducationContentData>();
        }
        [Key]
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedInfo { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedInfo { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string EducationalNeedCode { get; set; }
        public Guid? PatientAndFamilyEducationId { get; set; }
        [ForeignKey("PatientAndFamilyEducationId")]
        public virtual PatientAndFamilyEducation PatientAndFamilyEducation { get; set; }
        public virtual ICollection<PatientAndFamilyEducationContentData> PatientAndFamilyEducationContentDatas { get; set; }
    }
}
