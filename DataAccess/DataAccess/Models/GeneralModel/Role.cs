using DataAccess.Model.BaseModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class Role: IEntity
    {
        public Role()
        {
            this.RoleActions = new HashSet<RoleAction>();
            this.RoleSpecialties = new HashSet<RoleSpecialty>();
            this.Users = new HashSet<User>();
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
        public string ViName { get; set; }
        public string EnName { get; set; }
        public Nullable<Guid> VisitTypeGroupId { get; set; }
        [JsonIgnore]
        public virtual ICollection<RoleAction> RoleActions { get; set; }
        [JsonIgnore]
        public virtual ICollection<RoleSpecialty> RoleSpecialties { get; set; }
        [JsonIgnore]
        public virtual ICollection<User> Users { get; set; }
    }
}
