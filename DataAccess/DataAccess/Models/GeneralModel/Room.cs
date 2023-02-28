using DataAccess.Model.BaseModel;
using DataAccess.Models.EDModel;
using DataAccess.Models.OPDModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    public class Room : IEntity
    {
        public Room()
        {
            this.EmergencyTriageRecords = new HashSet<EDEmergencyTriageRecord>();
            this.OPDInitialAssessmentForOnGoings = new HashSet<OPDInitialAssessmentForOnGoing>();
            this.OPDInitialAssessmentForShortTerms = new HashSet<OPDInitialAssessmentForShortTerm>();
        }

        [Key]
        public Guid Id { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }
        public string DeletedBy { get; set; }
        public string ViName { get; set; }
        public string EnName { get; set; }
        public string Floor { get; set; }
        public string Value { get; set; }
        public string Service { get; set; }
        public Nullable<Guid> SiteId { get; set; }
        [ForeignKey("SiteId")]
        public virtual Site Site { get; set; }
        public virtual ICollection<EDEmergencyTriageRecord> EmergencyTriageRecords { get; set; }
        public virtual ICollection<OPDInitialAssessmentForOnGoing> OPDInitialAssessmentForOnGoings { get; set; }
        public virtual ICollection<OPDInitialAssessmentForShortTerm> OPDInitialAssessmentForShortTerms { get; set; }

    }
}
