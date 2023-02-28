using DataAccess.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.GeneralModel
{
    public class HumanResourceAssessment : IEntity
    {
        public HumanResourceAssessment()
        {
            this.HumanResourceAssessmentStaffs = new HashSet<HumanResourceAssessmentStaff>();
        }
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public DateTime? Date { get; set; }
        public string ViName { get; set; }
        public string EnName { get; set; }
        public Guid? SpecialtyId { get; set; }
        [ForeignKey("SpecialtyId")]
        public Specialty Specialty { get; set; }
        public virtual ICollection<HumanResourceAssessmentStaff> HumanResourceAssessmentStaffs { get; set; }
    }
}
