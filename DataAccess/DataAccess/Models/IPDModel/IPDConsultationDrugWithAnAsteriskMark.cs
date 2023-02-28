using DataAccess.Models.BaseModel;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.IPDModel
{
    public class IPDConsultationDrugWithAnAsteriskMark : VBaseModel
    {
        public string RoomNumber { get; set; }
        public DateTime? AddmisionDate { get; set; }
        public DateTime? ConsultationDate { get; set; }
        public string Diagnosis { get; set; }
        public string AntibioticsTreatmentBefore { get; set; }
        public string DiagnosisAfterConsultation { get; set; }
        public DateTime? HospitalDirectorOrHeadDepartmentTime { get; set; }
        public Guid? HospitalDirectorOrHeadDepartmentId { get; set; }
        [ForeignKey("HospitalDirectorOrHeadDepartmentId")]
        public virtual User HospitalDirectorOrHeadDepartment { get; set; }
        public DateTime? DoctorTime { get; set; }
        public Guid? DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public virtual User Doctor { get; set; }
        public Guid? VisitId { get; set; }

        [ForeignKey("VisitId")]
        public virtual IPD IpdId { get; set; }
    }
}
