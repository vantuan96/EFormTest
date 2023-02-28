using DataAccess.Model.BaseModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.EIOModel
{
    public class EIOBloodTransfusionChecklist : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? VisitId { get; set; }
        public string VisitTypeGroupCode { get; set; }
        public string Diagnosis { get; set; }
        public Guid? SpecialtyId{ get; set; }
        [ForeignKey("SpecialtyId")]
        public virtual Specialty Specialty { get; set; }
        public string BedNo { get; set; }
        public string TypeOfBloodProducts { get; set; }
        public string Quanlity { get; set; }
        public string Code { get; set; }
        public DateTime? DateOfBloodCollection { get; set; }
        public DateTime? Expiry { get; set; }
        public DateTime? ThawedTimeAt { get; set; }
        public DateTime? ThawedTimeTo { get; set; }
        public string PatientBloodTypeABO { get; set; }
        public string PatientBloodTypeRH { get; set; }
        public string DonorBloodTypeABO { get; set; }
        public string DonorBloodTypeRH { get; set; }
        public string OtherCheckTests { get; set; }
        public string OtherTests { get; set; }
        public string MajorCrossMatchSalt { get; set; }
        public string MajorCrossMatchAntiGlobulin { get; set; }
        public string MinorCrossMatchSalt { get; set; }
        public string MinorCrossMatchAntiGlobulin { get; set; }
        public DateTime? HeadOfLabConfirmTime { get; set; }
        public Guid? HeadOfLabId { get; set; }
        [ForeignKey("HeadOfLabId")]
        public virtual User HeadOfLab { get; set; }
        public DateTime? FirstTechnicianConfirmTime { get; set; }
        public Guid? FirstTechnicianId { get; set; }
        [ForeignKey("FirstTechnicianId")]
        public virtual User FirstTechnician { get; set; }
        public DateTime? SecondTechnicianConfirmTime { get; set; }
        public Guid? SecondTechnicianId { get; set; }
        [ForeignKey("SecondTechnicianId")]
        public virtual User SecondTechnician { get; set; }

        public string NumberOfBloodTransfusion { get; set; }
        public string Crossmatch { get; set; }
        public DateTime? StartTransfusionAt { get; set; }
        public DateTime? StopTransfusionAt { get; set; }
        public string ActualAmountOfBloodTransmitted { get; set; }
        public string Remark { get; set; }
        public DateTime? PhysicianConfirmTime { get; set; }
        public Guid? PhysicianId { get; set; }
        [ForeignKey("PhysicianId")]
        public virtual User Physician { get; set; }
        public DateTime? NurseConfirmTime { get; set; }
        public Guid? NurseId { get; set; }
        [ForeignKey("NurseId")]
        public virtual User Nurse { get; set; }
        public int Version { get; set; }
    }
}
