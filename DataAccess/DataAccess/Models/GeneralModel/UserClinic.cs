using DataAccess.Model.BaseModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    public class UserClinic : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<Guid> ClinicId { get; set; }
        [ForeignKey("ClinicId")]
        public virtual Clinic Clinic { get; set; }
        public Nullable<Guid> UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
