using DataAccess.Model.BaseModel;
using System;

namespace DataAccess.Models.GeneralModel
{
    public class SystemNotification : IEntity
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Service { get; set; }
        public string Subject { get; set; }
        public string Scope { get; set; }
        public string Content { get; set; }
        // 0: error
        // 1: sent
        // 2: done
        public int Status { get; set; }
    }
}
