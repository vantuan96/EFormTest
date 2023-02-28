using DataAccess.Model.BaseModel;
using DataAccess.Models.EDModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace DataAccess.Models
{
    public class User : IEntity
    {
        public User()
        { 
            this.UserRoles = new HashSet<UserRole>();
            this.UserClinics = new HashSet<UserClinic>();
            this.PositionUsers = new HashSet<PositionUser>();
            this.UserSpecialties = new HashSet<UserSpecialty>();
        }
        public static ClaimsIdentity Identity { get; internal set; }
        [Key]
        public Guid Id { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<Guid> CurrentSiteId { get; set; }
        [ForeignKey("CurrentSiteId")]
        public virtual Site Site { get; set; }
        public Nullable<Guid> CurrentRoleId { get; set; }
        [ForeignKey("CurrentRoleId")]
        public virtual Role Role { get; set; }
        public Nullable<Guid> CurrentSpecialtyId { get; set; }
        [ForeignKey("CurrentSpecialtyId")]
        [JsonIgnore]
        public virtual Specialty Specialty { get; set; }
        public Nullable<Guid> UserPositionId { get; set; }
        [ForeignKey("UserPositionId")]
        [JsonIgnore]
        public virtual Position UserPosition { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        [Index]
        public string Username { get; set; }
        public string Roles { get; set; }
        public string Fullname { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string DisplayName { get; set; }
        public string LoginNameWithDomain { get; set; }
        public string Mobile { get; set; }
        public string EmailAddress { get; set; }
        public string Department { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Company { get; set; }
        public string ManagerName { get; set; }
        public string ManagerId { get; set; }
        [JsonIgnore]
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public bool IsAdminUser { get; set; }
        [JsonIgnore]
        public virtual ICollection<UserAdminRole> UserAdminRoles { get; set; }
        [JsonIgnore]
        public virtual ICollection<UserClinic> UserClinics { get; set; }
        public string EHOSAccount { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(20)]
        [Index]
        public string SessionId { get; set; }
        public string Session { get; set; }
        [JsonIgnore]
        public virtual ICollection<PositionUser> PositionUsers { get; set; }
        [JsonIgnore]
        public virtual ICollection<UserSpecialty> UserSpecialties { get; set; }
        public bool IsLocked { get; set; } = false;
    }
}
