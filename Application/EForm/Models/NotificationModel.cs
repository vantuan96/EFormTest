using System;

namespace EForm.Models
{
    public class NotificationModel
    {
        public Guid Id { get; set; }
        public bool Seen { get; set; }
        public string Form { get; set; }
        public Guid? SpecialtyId { get; set; }
        public Guid? VisitId { get; set; }
        public string VisitTypeGroupCode { get; set; }
        public string EnMessage { get; set; }
        public string ViMessage { get; set; }
        public string CreatedAt { get; set; }
    }
}