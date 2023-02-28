using DataAccess.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.EDModel
{
    public class EDArterialBloodGasTest : IEntity
    {
        public EDArterialBloodGasTest()
        {
            this.EDArterialBloodGasTestDatas = new HashSet<EDArterialBloodGasTestData>();
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
        public bool UseBreathingMachine { get; set; }
        public string BreathingMode { get; set; }
        public string BP { get; set; }
        public string Vt { get; set; }
        public string F { get; set; }
        public string RR { get; set; }
        public string FIO2 { get; set; }
        public string T { get; set; }
        public string Upload { get; set; }
        public Guid? VisitId { get; set; }
        public string VisitTypeGroupCode { get; set; }
        public Guid? ExecutionUserId { get; set; }
        [ForeignKey("ExecutionUserId")]
        public virtual User ExecutionUser { get; set; }
        public DateTime? ExecutionTime { get; set; }
        public Guid? DoctorAcceptId { get; set; }
        [ForeignKey("DoctorAcceptId")]
        public virtual User DoctorAccept { get; set; }
        public DateTime? AcceptTime { get; set; }
        public virtual ICollection<EDArterialBloodGasTestData> EDArterialBloodGasTestDatas { get; set; }
        public string AllenTest { get; set; }
        public string CollectionSite { get; set; }
    }
}
