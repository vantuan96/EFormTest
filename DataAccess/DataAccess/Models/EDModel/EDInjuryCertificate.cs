using DataAccess.Model.BaseModel;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.EDModel
{
    public class EDInjuryCertificate : IEntity
    {
        public Guid Id { get ; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DoctorTime { get; set; }
        public Guid? DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public virtual User Doctor { get; set; }
        public DateTime? HeadOfDeptTime { get; set; }
        public Guid? HeadOfDeptId { get; set; }
        [ForeignKey("HeadOfDeptId")]
        public virtual User HeadOfDept { get; set; }
        public DateTime? DirectorTime { get; set; }
        public Guid? DirectorId { get; set; }
        [ForeignKey("DirectorId")]
        public virtual User Director { get; set; }
        public Guid? VisitId { get; set;}
    }
}
