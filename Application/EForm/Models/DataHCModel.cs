using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace EForm.Models
{
    public class DataHCModel
    {
        public Guid? ChargeDetaiId { get; set; }
        public string Status { get; set; }
        public string VisitGroupType { get; set; }        
        public string DoctorAD { get; set; }
        public Guid? PatientLocationId { get; set; }
        public string VisitCode { get; set; }
        public string AreaName { get; set; }
        public string HospitalCode { get; set; }
        public DateTime? ChargeDateTime { get; set; }
        public DateTime? CreationDateTime { get; set; }
        public DateTime? DeleteDateTime { get; set; }
        public string ServiceCode { get; set; }
        public Guid? PatientVisitId { get; set; }
        public string PatientId { get; set; }
        public string VisitType { get; set; }
        public Guid? CostCentreId { get; set; }
        public string PatientLocationCode { get; set; }
    }
}