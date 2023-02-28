using DataAccess.Models.EIOModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EForm.Models.IPDModels
{
    public class IPDCareNoteQueryModel
    {
        public DateTime? NoteTime { get; set; }
        public string ProgressNote { get; set; }
        public string Fullname { get; set; }
        public string CareNote { get; set; }
        public Guid? VisitId { get; set; }
        public string VisitTypeGroupCode { get; set; }
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? FormId { get; set; }
        public string Room { get; set; }
        public string Beb { get; set; }
    }
    public class IPDQueryModel
    {
        public Guid Id { get; set; }
        public string VisitCode { get; set; }
        public DateTime AdmittedDate { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string AdmittedDateDate { get; set; }
        public bool PermissionForVisitor { get; set; }
        public string CustomerPID { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerFullname { get; set; }
        public DateTime? CustomerDateOfBirth { get; set; }
        public bool? CustomerIsAllergy { get; set; }
        public string CustomerAllergy { get; set; }
        public string CustomerKindOfAllergy { get; set; }
        public Guid EDStatusId { get; set; }
        public DateTime? EDStatusCreatedAt { get; set; }
        public string EDStatusEnName { get; set; }
        public string EDStatusViName { get; set; }
        public Guid? PrimaryDoctorId { get; set; }
        public Guid? MedicalRecordId { get; set; }
        public string PrimaryDoctorUsername { get; set; }
        public Guid? PrimaryNurseId { get; set; }
        public string PrimaryNurseUsername { get; set; }
        public string UnlockFor { get; set; }
        public bool IsVip { get; set; }
        public bool IsDraft { get; set; }
        public int Version { get; set; }
        public string UserDoNormalInpatiens { get; set; }
        public string UserReceiver { get; set; }
    }
}