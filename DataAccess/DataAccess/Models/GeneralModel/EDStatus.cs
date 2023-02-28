using DataAccess.Model.BaseModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    public class EDStatus : IEntity
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
        public string Code { get; set; }
        public Guid? VisitTypeGroupId { get; set; }
        [ForeignKey("VisitTypeGroupId")]
        public virtual VisitTypeGroup VisitTypeGroup { get; set; }
    }
}
