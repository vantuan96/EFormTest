using DataAccess.Model.BaseModel;
using DataAccess.Models.BaseModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.GeneralModel
{
    public class SystemConfig : IEntity
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
        public string NotificationEmail { get; set; }
        public DateTime? LastUpdatedOHService { get; set; }
        public string TypeConfig { get; set; }
    }
    public class AppConfig : VBaseModel
    {
        public string Key { get; set; }
        public string Label { get; set; }
        public string Value { get; set; }
    }
}
