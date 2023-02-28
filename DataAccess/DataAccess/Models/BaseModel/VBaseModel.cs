using DataAccess.Model.BaseModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.BaseModel
{
    public class VBaseModel : IEntity
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
    }
    public class VisitInfo : VBaseModel
    {
        [Index]
        public DateTime AdmittedDate { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(255)]
        [Index]
        public string VisitCode { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        [Index]
        public string RecordCode { get; set; }
        public Guid? SiteId { get; set; }
        [ForeignKey("SiteId")]
        public virtual Site Site { get; set; }
        public Guid? SpecialtyId { get; set; }
        [ForeignKey("SpecialtyId")]
        public virtual Specialty Specialty { get; set; }
        public Guid? CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
        public string PatientLocationCode { get; set; }
        public string VisitGroupType { get; set; }
        public string AreaName { get; set; }
        public string VisitType { get; set; }
        public string HospitalCode { get; set; }
        public string DoctorAD { get; set; }
        public Guid? PatientLocationId { get; set; }
        public Guid? PatientVisitId { get; set; }
        public DateTime? ActualVisitDate { get; set; }
        public Guid? PrimaryDoctorId { get; set; }
        [ForeignKey("PrimaryDoctorId")]
        [JsonIgnore]
        public virtual User PrimaryDoctor { get; set; }
        public Guid? PrimaryNurseId { get; set; }
        [ForeignKey("PrimaryNurseId")]
        public virtual User PrimaryNurse { get; set; }
        public bool? IsAllergy { get; set; }
        public string Allergy { get; set; }
        public string KindOfAllergy { get; set; }
        public bool? IsEhos { get; set; }
        public string UnlockFor { get; set; } = "ALL"; // all or any doctor username

        public bool IsTransfer { get; set; }
        public Guid? TransferFromId { get; set; }
    }
}
