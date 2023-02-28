using DataAccess.Model.BaseModel;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.GeneralModel
{
    public class HumanResourceAssessmentStaff : IEntity
    {
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
        public int Order { get; set; }
        public string Type { get; set; }
        public Guid? UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public Guid? HumanResourceAssessmentId { get; set; }
        [ForeignKey("HumanResourceAssessmentId")]
        public HumanResourceAssessment HumanResourceAssessment { get; set; }
    }
}
