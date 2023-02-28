using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMRModels
{
    public class HISChargeDetailModel
    {
        public Guid? ChargeDetailId { get; set; }
        public Guid? NewChargeId { get; set; }
        public string FillerOrderNumber { get; set; }
        public string PlacerOrderNumber { get; set; }
        public string PaymentStatus { get; set; }
        public string RadiologyScheduledStatus { get; set; }
        public string PlacerOrderStatus { get; set; }
        public string SpecimenStatus { get; set; }
        public DateTime? ChargeDeletedDate { get; set; }
    }
    public class OpenVisitInfo
    {
        public string PatientId { get; set; }
        public string PatientVisitId { get; set; }
        public string VisitCode { get; set; }
        public string VisitType { get; set; }
        public string ActualVisitDate { get; set; }
        public string ClosureDate { get; set; }

    }
}
