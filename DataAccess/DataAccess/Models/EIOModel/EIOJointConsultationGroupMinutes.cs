using DataAccess.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.EIOModel
{
    public class EIOJointConsultationGroupMinutes : IEntity
    {
        public EIOJointConsultationGroupMinutes()
        {
            this.EIOJointConsultationGroupMinutesDatas = new HashSet<EIOJointConsultationGroupMinutesData>();
            this.EIOJointConsultationGroupMinutesMembers = new HashSet<EIOJointConsultationGroupMinutesMember>();
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
        public Guid? VisitId { get; set; }
        public string VisitTypeGroupCode { get; set; }
        public Guid? SpecialtyId { get; set; }
        [ForeignKey("SpecialtyId")]
        public virtual Specialty Specialty { get; set; }
        public Guid? DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public virtual User Doctor { get; set; }
        public bool SecretaryConfirm { get; set; }
        public Guid? SecretaryId { get; set; }
        [ForeignKey("SecretaryId")]
        public virtual User Secretary { get; set; }
        public bool ChairmanConfirm { get; set; }
        public Guid? ChairmanId { get; set; }
        [ForeignKey("ChairmanId")]
        public virtual User Chairman { get; set; }
        public bool MemberConfirm { get; set; }
        public virtual ICollection<EIOJointConsultationGroupMinutesData> EIOJointConsultationGroupMinutesDatas { get; set; }
        public virtual ICollection<EIOJointConsultationGroupMinutesMember> EIOJointConsultationGroupMinutesMembers { get; set; }
    }
}
