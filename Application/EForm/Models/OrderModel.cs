using System;

namespace EForm.Models
{
    public class OrderModel
    {
        public Guid Id { get; set; }
        public string Drug { get; set; }
        public string Dosage { get; set; }
        public string Route { get; set; }
        public string UsedAt { get; set; }
    }
    public class CpoeOrderableModel
    {
        public Guid CpoeOrderableId { get; set; }
    }

    public class VisitOrder
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public DateTime? AdmittedDate { get; set; }
    }
}