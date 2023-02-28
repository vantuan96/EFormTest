using DataAccess.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class ChargeVisitDto : VBaseModel
    {
        public string PatientLocationCode { get; set; }
        public string VisitGroupType { get; set; }
        public string VisitCode { get; set; }
        public Guid? VisitId { get; set; }
        public string AreaName { get; set; }
        public string VisitType { get; set; }
        public string HospitalCode { get; set; }
        public string DoctorAD { get; set; }
        public Guid? PatientLocationId { get; set; }
        public Guid? PatientVisitId { get; set; }
        public DateTime? ActualVisitDate { get; set; }

        public Guid? CustomerId { get; set; }


    }
    public class ChargeDto : VBaseModel
    {
        public Guid? VisitId { get; set; }
        public string Reason { get; set; }
        public string Priority { get; set; }
        public string Diagnosis { get; set; }
        public string DoctorAD { get; set; }
        public Guid? PatientVisitId { get; set; }
        public Guid? PatientLocationId { get; set; }
        public string VisitCode { get; set; }
        public string VisitType { get; set; }
        public string HospitalCode { get; set; }
        public string Room { get; set; }
        public string Bed { get; set; }

        public Guid? ChargeVisitId { get; set; }
    }
}
