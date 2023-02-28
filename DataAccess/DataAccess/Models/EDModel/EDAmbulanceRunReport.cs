using DataAccess.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.EDModel
{
    public class EDAmbulanceRunReport : IEntity
    {
        public EDAmbulanceRunReport()
        {
            this.EDAmbulanceRunReportDatas = new HashSet<EDAmbulanceRunReportData>();
            this.EDAmbulanceTransferPatientsRecords = new HashSet<EDAmbulanceTransferPatientsRecord>();
        }
        [Key]
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? AssessmentNurseTime { get; set; }
        public Guid? AssessmentNurseId { get; set; }
        [ForeignKey("AssessmentNurseId")]
        public virtual User AssessmentNurse { get; set; }
        public DateTime? AssessmentPhysicianTime { get; set; }
        public Guid? AssessmentPhysicianId { get; set; }
        [ForeignKey("AssessmentPhysicianId")]
        public virtual User AssessmentPhysician { get; set; }
        public DateTime? TransferTime { get; set; }
        public Guid? TransferId { get; set; }
        [ForeignKey("TransferId")]
        public virtual User Transfer { get; set; }
        public DateTime? AdmitTime { get; set; }
        public Guid? AdmitId { get; set; }
        [ForeignKey("AdmitId")]
        public virtual User Admit { get; set; }

        public virtual ICollection<EDAmbulanceRunReportData> EDAmbulanceRunReportDatas { get; set; }
        public virtual ICollection<EDAmbulanceTransferPatientsRecord> EDAmbulanceTransferPatientsRecords { get; set; }
    }
}
