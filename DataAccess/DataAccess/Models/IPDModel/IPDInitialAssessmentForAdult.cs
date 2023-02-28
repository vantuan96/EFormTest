using DataAccess.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.IPDModel
{
    public class IPDInitialAssessmentForAdult : IEntity
    {
        public IPDInitialAssessmentForAdult()
        {
            this.IPDInitialAssessmentForAdultDatas = new HashSet<IPDInitialAssessmentForAdultData>();
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
        public int Version { get; set; } = 1;
        public virtual ICollection<IPDInitialAssessmentForAdultData> IPDInitialAssessmentForAdultDatas { get; set; }
    }
}
