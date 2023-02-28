using DataAccess.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.EIOModel
{
    public class EIOJointConsultationForApprovalOfSurgery: IEntity
    {
        public EIOJointConsultationForApprovalOfSurgery()
        {
            this.EIOJointConsultationForApprovalOfSurgeryDatas = new HashSet<EIOJointConsultationForApprovalOfSurgeryData>();
        }
        [Key]
        public Guid Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string DeletedBy { get; set; }
        public Guid? VisitId { get; set; }
        public string VisitTypeGroupCode { get; set; }
        public DateTime? TimeOfAdmission { get; set; }
        public DateTime? TimeOfJointConsultation { get; set; }
        public Guid? CMOId { get; set; }
        [ForeignKey("CMOId")]
        public virtual User CMO { get; set; }
        public Guid? HeadOfDeptId { get; set; }
        [ForeignKey("HeadOfDeptId")]
        public virtual User HeadOfDept { get; set; }
        public Guid? AnesthetistId { get; set; }
        [ForeignKey("AnesthetistId")]
        public virtual User Anesthetist { get; set; }
        public Guid? SurgeonId { get; set; }
        [ForeignKey("SurgeonId")]
        public virtual User Surgeon { get; set; }
        public virtual ICollection<EIOJointConsultationForApprovalOfSurgeryData> EIOJointConsultationForApprovalOfSurgeryDatas { get; set; }
        public int Version { get; set; }
    }
}
