using DataAccess.Model.BaseModel;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    public class ComplexOutpatientCaseSummary: IEntity
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
        public string MainDiseases { get; set; }
        public string Orders { get; set; }
        public Guid? CustomerId { get; set; }
        [ForeignKey("CustomerId")]

        [JsonIgnore]
        public virtual Customer Customer { get; set; }
        public Guid? PrimaryDoctorId { get; set; }
        [ForeignKey("PrimaryDoctorId")]

        [JsonIgnore]
        public virtual User PrimaryDoctor { get; set; }
        public Guid? VisitId { get; set; }
        public string VisitTypeGroupCode { get; set; }
    }
}
