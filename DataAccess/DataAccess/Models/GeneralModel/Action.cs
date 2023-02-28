using DataAccess.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    public class Action: IEntity
    {
        public Action()
        {
            this.RoleActions = new HashSet<RoleAction>();
        }
        [Key]
        public Guid Id { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }
        public string DeletedBy { get; set; }
        public string Url { get; set; }
        public string Method { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Nullable<Guid> VisitTypeGroupId { get; set; }
        [ForeignKey("VisitTypeGroupId")]
        public virtual VisitTypeGroup VisitTypeGroup { get; set; }
        public virtual ICollection<RoleAction> RoleActions { get; set; }
    }
}
