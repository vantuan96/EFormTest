using DataAccess.Model.BaseModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class Form: IEntity
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
        public string Code { get; set; }
        public string VisitTypeGroupCode { get; set; }
        public int? Version { get; set; }
        public int? Time { get; set; }
        public bool Ispermission { get; set; }
        public int? TimeToLockForm { get; set; }
    }
}
