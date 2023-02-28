using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EForm.Models
{
    public class DetailPharmacyModel
    {
        public string SeqNum { get; set; }
        public string ItemCode { get; set; }
        public string Usage { get; set; }
        public string PrescriberAD { get; set; }
        public string PrescriptionId { get; set; }
        public string Quantity { get; set; }
        public string PID { get; set; }
        public string VisitCode { get; set; }
        public string PrescriberName { get; set; }
        public string PharmacyName { get; set; }
        public string StartDate { get; set; }
        public string NameSX { get; set; }
        public string PrescriptionCode { get; set; }
        public string DType { get; set; }
        public DateTime CreatedDate { get; set; }
        public string StopDate { get; set; }
        public string ItemName { get; set; }
        public string OrderReferenceNumber { get; set; }
        public string LocationCode { get; set; }

        public string HospitalCode { get; set; }
        public string LocationName { get; set; }
        public string VisitType { get; set; }

        public string PrimaryDoctorAD { get; set; }
        public string Note { get; set; }
    }
}
