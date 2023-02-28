using DataAccess.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class PatientAndFamilyEducation: IEntity
    {
        public PatientAndFamilyEducation()
        {
            this.PatientAndFamilyEducationContents = new HashSet<PatientAndFamilyEducationContent>();
            this.PatientAndFamilyEducationDatas = new HashSet<PatientAndFamilyEducationData>();
        }
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
        public virtual ICollection<PatientAndFamilyEducationContent> PatientAndFamilyEducationContents { get; set; }
        public virtual ICollection<PatientAndFamilyEducationData> PatientAndFamilyEducationDatas { get; set; }
        public int Version { get; set; }
    }
}
