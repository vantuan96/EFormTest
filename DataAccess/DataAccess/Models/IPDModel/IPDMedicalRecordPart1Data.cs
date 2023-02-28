using DataAccess.Model.BaseModel;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.IPDModel
{
    public class IPDMedicalRecordPart1Data : IEntity
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
        public Guid? IPDMedicalRecordPart1Id { get; set; }
        [ForeignKey("IPDMedicalRecordPart1Id")]
        public virtual IPDMedicalRecordPart1 IPDMedicalRecordPart1 { get; set; }
    }
}
