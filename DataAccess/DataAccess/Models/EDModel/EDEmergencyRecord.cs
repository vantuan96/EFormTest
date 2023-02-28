using DataAccess.Model.BaseModel;
using System;
using System.Collections.Generic;

namespace DataAccess.Models.EDModel
{
    public class EDEmergencyRecord: IEntity
    {
        public EDEmergencyRecord()
        {
            this.EmergencyRecordDatas = new HashSet<EDEmergencyRecordData>();
        }

        public Guid Id { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<DateTime> TimeSeen { get; set; }
        public virtual ICollection<EDEmergencyRecordData> EmergencyRecordDatas { get; set; }
    }
}
