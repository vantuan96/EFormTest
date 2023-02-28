using DataAccess.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class UserDto : IEntity
    {
        public Guid Id { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<Guid> CurrentSiteId { get; set; }
        public Nullable<Guid> CurrentRoleId { get; set; }
        public Nullable<Guid> CurrentSpecialtyId { get; set; }
        public Nullable<Guid> UserPositionId { get; set; }
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
        public string EHOSAccount { get; set; }
        public string SessionId { get; set; }
        public string Session { get; set; }
        public bool IsLocked { get; set; } = false;
    }
}
