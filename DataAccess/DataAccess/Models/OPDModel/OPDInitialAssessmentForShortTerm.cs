using DataAccess.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.OPDModel
{
    public class OPDInitialAssessmentForShortTerm : IEntity
    {
        public OPDInitialAssessmentForShortTerm()
        {
            this.OPDInitialAssessmentForShortTermDatas = new HashSet<OPDInitialAssessmentForShortTermData>();
        }

        public Guid Id { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? AdmittedDate { get; set; }
        public int Version { get; set; } = 1;
        public virtual ICollection<OPDInitialAssessmentForShortTermData> OPDInitialAssessmentForShortTermDatas { get; set; }
    }
}
