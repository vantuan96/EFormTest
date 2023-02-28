using DataAccess.Model.BaseModel;
using DataAccess.Models.EDModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class Site : IEntity
    {
        public Site()
        {
            this.UserSites = new HashSet<UserRole>();
            this.Roles = new HashSet<Role>();
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
        public string Name { get; set; }
        public string Code { get; set; }
        public string ApiCode { get; set; }
        public string Location { get; set; }
        public string Province { get; set; }
        public string Level { get; set; }
        public string LocationUnit { get; set; }
        [JsonIgnore]
        public virtual ICollection<UserRole> UserSites { get; set; }
        [JsonIgnore]
        public virtual ICollection<Role> Roles { get; set; }

        //DaoDucDev - Bổ sung Địa chỉ, Số điện thoại, Hotline, Emergency, Tên tiếng Việt, tên tiếng Anh
        //Phục vụ cho đơn thuốc hoặc các mẫu form cần địa chỉ
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Hotline { get; set; }
        public string Emergency { get; set; }
        public string ViName { get; set; }
        public string EnName { get; set; }
    }
}
