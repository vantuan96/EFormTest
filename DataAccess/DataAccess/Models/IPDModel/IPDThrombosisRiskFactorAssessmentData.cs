using DataAccess.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.IPDModel
{
    public class IPDThrombosisRiskFactorAssessmentData : IEntity
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public string Code { get; set; }
        public string Value { get; set; }

        public Guid? IPDThrombosisRiskFactorAssessmentId { get; set; }
        [ForeignKey("IPDThrombosisRiskFactorAssessmentId")]
        public virtual IPDThrombosisRiskFactorAssessment IPDThrombosisRiskFactorAssessment { get; set; }
    }
}
