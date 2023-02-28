using DataAccess.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.EIOModel
{
    public class EIOAssessmentForRetailServicePatient: IEntity
    {
        public EIOAssessmentForRetailServicePatient()
        {
            this.EIOAssessmentForRetailServicePatientDatas = new HashSet<EIOAssessmentForRetailServicePatientData>();
        }
        [Key]
        public Guid Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string DeletedBy { get; set; }
        public DateTime TriageDateTime { get; set; }
        public string Bed { get; set; }
        public int Version { get; set; } = 1;
        public virtual ICollection<EIOAssessmentForRetailServicePatientData> EIOAssessmentForRetailServicePatientDatas { get; set; }
    }
}
