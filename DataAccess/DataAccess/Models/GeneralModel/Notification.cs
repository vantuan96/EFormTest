using DataAccess.Model.BaseModel;
using System;

namespace DataAccess.Models
{
    public class Notification: IEntity
    {
        public Guid Id { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }
        public string DeletedBy { get; set; }
        public bool Seen { get; set; }
        public DateTime? TimeSeen { get; set; }
        public string FromUser { get; set; }
        public string ToUser { get; set; }
        public int Priority { get; set; }
        public string ViMessage { get; set; }
        public string EnMessage { get; set; }
        public Guid? VisitId { get; set; }
        public string VisitTypeGroupCode { get; set; }
        public string Form { get; set; }
        public Guid? SpecialtyId { get; set; }
    }
}
