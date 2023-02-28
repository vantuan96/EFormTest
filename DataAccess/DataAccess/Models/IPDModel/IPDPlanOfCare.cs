using DataAccess.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.IPDModel
{
    public class IPDPlanOfCare : IEntity
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? IPDId { get; set; }
        [ForeignKey("IPDId")]
        public virtual IPD IPD { get; set; }
        public DateTime? Time { get; set; }
        public string Diagnosis { get; set; }
        public string FollowUpCarePlan { get; set; }
        public string ParaClinicalTestsPlan { get; set; }
        public string SpecialRequests { get; set; }
        public string EducationPlan { get; set; }
        public string ExpectedNumber { get; set; }
        public string Prognosis { get; set; }
        public string ExpectedOutcome { get; set; }
        public string Note { get; set; }
        public DateTime? ConfirmTime { get; set; }
    }
}
