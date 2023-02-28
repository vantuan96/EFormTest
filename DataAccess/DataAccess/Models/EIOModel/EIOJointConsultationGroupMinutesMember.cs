﻿using DataAccess.Model.BaseModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.EIOModel
{
    public class EIOJointConsultationGroupMinutesMember: IEntity
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
        public Guid? EIOJointConsultationGroupMinutesId { get; set; }
        [ForeignKey("EIOJointConsultationGroupMinutesId")]
        public virtual EIOJointConsultationGroupMinutes EIOJointConsultationGroupMinutes { get; set; }
        public bool IsConfirm { get; set; }
        public Guid? MemberId { get; set; }
        [ForeignKey("MemberId")]
        public virtual User Member { get; set; }
    }
}
