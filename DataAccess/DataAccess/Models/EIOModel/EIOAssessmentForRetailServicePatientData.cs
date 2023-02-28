using DataAccess.Model.BaseModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.EIOModel
{
    public class EIOAssessmentForRetailServicePatientData: IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string DeletedBy { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }
        public string EnValue { get; set; }
        public Guid? EDAssessmentForRetailServicePatientId { get; set; }
        [ForeignKey("EDAssessmentForRetailServicePatientId")]

        public virtual EIOAssessmentForRetailServicePatient EIOAssessmentForRetailServicePatient { get; set; }
    }
}
