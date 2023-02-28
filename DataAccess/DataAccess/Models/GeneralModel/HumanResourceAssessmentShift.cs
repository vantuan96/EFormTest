using DataAccess.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.GeneralModel
{
    public class HumanResourceAssessmentShift : IEntity
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string ViName { get; set; }
        public string EnName { get; set; }
        public string StartAt { get; set; }
        public string EndAt { get; set; }
        public Guid? SiteId { get; set; }
        [ForeignKey("SiteId")]
        public Site Site { get; set; }
        public Guid? SpecialtyId { get; set; }
        [ForeignKey("SpecialtyId")]
        public Specialty Specialty { get; set; }
    }
}
