using DataAccess.Models.BaseModel;
using System;
namespace DataAccess.Models.GeneralModel
{
    public class DiagnosticReporting : VBaseModel
    {
        public string Technique { get; set; }
        public string Findings { get; set; }
        public string Impression { get; set; }
        public DateTime? ExamCompleted { get; set; }
        public int Status { get; set; } // 1 Đã Tiếp nhận, 2 Đã hoàn thành
        public Guid ChargeItemId { get; set; } // Charge.Id
        public string Nurse { get; set; }
        public string Area { get; set; }
    }
}
