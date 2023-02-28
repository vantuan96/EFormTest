using DataAccess.Model.BaseModel;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.IPDModel
{
    public class IPDDischargeMedicalReport : IEntity
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? PhysicianInChargeId { get; set; }
        [ForeignKey("PhysicianInChargeId")]
        public virtual User PhysicianInCharge { get; set; }
        public DateTime? PhysicianInChargeTime { get; set; }
        public Guid? DeptHeadId { get; set; }
        [ForeignKey("DeptHeadId")]
        public virtual User DeptHead { get; set; }
        public DateTime? DeptHeadTime { get; set; }
        public Guid? DirectorId { get; set; }
        [ForeignKey("DirectorId")]
        public virtual User Director { get; set; }
        public DateTime? DirectorTime { get; set; }
    }
}
