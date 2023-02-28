using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EForm.Models
{
    public class MedicationOrdersResponse
    {
        public List<MedicationOrdersResult> MedicationOrdersResults { get; set; }
    }
    public class MedicationOrdersResult
    {
        public List<OrdersOutput> OrdersOutputs { get; set; }
    }
    public class OrdersOutput
    {
        public string PID { get; set; }
        public string Medication { get; set; }
        public string Status { get; set; }
        public string Dose { get; set; }
        public string Frequency { get; set; }
        public string RxQuantity { get; set; }
        public string Route { get; set; }
        public string PRN { get; set; }
        public string StartDateTime { get; set; }
        public string StopDateTime { get; set; }
        public string Duration { get; set; }
        public string Supply { get; set; }
        public string Facility { get; set; }
        public string WardOrClinic { get; set; }
        public string VisitTypeGroup { get; set; }
        public string VisitType { get; set; }
        public string VisitCode { get; set; }
        public string VisitStart { get; set; }
        public string VisitClosure { get; set; }
        public string PrescriberName { get; set; }
        public string PrescriberAD { get; set; }
        public string Rx { get; set; }
        public string RxDate { get; set; }
        public string RxItem { get; set; }
    }
}