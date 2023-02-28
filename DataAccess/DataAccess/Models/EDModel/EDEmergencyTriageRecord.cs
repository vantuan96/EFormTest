using DataAccess.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.EDModel
{
    public class EDEmergencyTriageRecord : IEntity
    {
        public EDEmergencyTriageRecord()
        {
            this.EmergencyTriageRecordDatas = new HashSet<EDEmergencyTriageRecordData>();
            this.Orders = new HashSet<Order>();
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
        public DateTime TriageDateTime { get;set; }
        public string Bed { get; set; }
        public Nullable<Guid> RoomID { get; set; }
        public virtual Room Room { get; set; }
        public int Version { get; set; } = 1;
        public virtual ICollection<EDEmergencyTriageRecordData> EmergencyTriageRecordDatas { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
