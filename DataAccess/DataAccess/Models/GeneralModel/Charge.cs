using DataAccess.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.GeneralModel
{
    public class ChargeVisit : VBaseModel
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
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
        public ChargeVisit()
        {
            this.Charges = new HashSet<Charge>();
        }
        public virtual ICollection<Charge> Charges { get; set; }

    }
    public class Charge: VBaseModel
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
        [ForeignKey("ChargeVisitId")]
        public virtual ChargeVisit ChargeVisit { get; set; }

        public int? Status { get; set; }
    }
}
