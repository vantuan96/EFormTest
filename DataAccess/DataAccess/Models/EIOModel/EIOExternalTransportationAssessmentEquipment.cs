using DataAccess.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.EIOModel
{
    public class EIOExternalTransportationAssessmentEquipment : IEntity
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
        public string Name { get; set; }
        public bool? IsNeeded { get; set; }
        public string Note { get; set; }
        public Guid? EIOExternalTransportationAssessmentId { get; set; }
        [ForeignKey("EIOExternalTransportationAssessmentId")]
        public virtual EIOExternalTransportationAssessment EIOExternalTransportationAssessment { get; set; }
    }
}
