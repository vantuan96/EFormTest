using DataAccess.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class RoleAction: IEntity
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
        public Nullable<Guid> RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
        public Nullable<Guid> ActionId { get; set; }
        [ForeignKey("ActionId")]
        public virtual Action Action { get; set; }
    }
}
