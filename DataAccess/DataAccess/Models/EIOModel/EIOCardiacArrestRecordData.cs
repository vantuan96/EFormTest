using DataAccess.Model.BaseModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.EIOModel
{
    public class EIOCardiacArrestRecordData : IEntity
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
        public int Order { get; set; }
        public string Type { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }
        public Guid? FormId { get; set; }
        public Guid? EIOCardiacArrestRecordId { get; set; }
        public virtual EIOCardiacArrestRecord EIOCardiacArrestRecord { get; set; }
    }
}
