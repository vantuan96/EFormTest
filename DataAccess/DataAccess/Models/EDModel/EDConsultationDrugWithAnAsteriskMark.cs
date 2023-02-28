using DataAccess.Model.BaseModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.EDModel
{
    public class EDConsultationDrugWithAnAsteriskMark:IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string DeletedBy { get; set; }
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
        public virtual ED Ed { get; set; }
    }
}
