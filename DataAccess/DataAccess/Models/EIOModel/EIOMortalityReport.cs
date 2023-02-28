using DataAccess.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.EIOModel
{
    public class EIOMortalityReport : IEntity
    {
        public EIOMortalityReport()
        {
            this.EDMortalityReportMembers = new HashSet<EIOMortalityReportMember>();
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
        public DateTime? Time { get; set; }
        public DateTime? DeathAt { get; set; }
        public string Reason { get; set; }
        public string PastMedicalHistory { get; set; }
        public string Status { get; set; }
        public string Diagnosis { get; set; }
        public string Progress { get; set; }
        public string Welcome { get; set; }
        public string Assessment { get; set; }
        public string TreatmentAndProcedures { get; set; }
        public string Care { get; set; }
        public string RelationShip { get; set; }
        public string Extra { get; set; }
        public string Conclusion { get; set; }
        public DateTime? ChairmanTime { get; set; }
        public Guid? ChairmanId { get; set; }
        [ForeignKey("ChairmanId")]
        public virtual User Chairman { get; set; }
        public DateTime? SecretaryTime { get; set; }
        public Guid? SecretaryId { get; set; }
        [ForeignKey("SecretaryId")]
        public virtual User Secretary { get; set; }
        public virtual ICollection<EIOMortalityReportMember> EDMortalityReportMembers { get; set; }
        public string ICD10 { get; set; }
    }
}
