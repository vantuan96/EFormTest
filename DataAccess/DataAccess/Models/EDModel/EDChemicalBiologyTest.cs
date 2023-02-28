using DataAccess.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.EDModel
{
    public class EDChemicalBiologyTest: IEntity
    {
        public EDChemicalBiologyTest()
        {
            this.EDChemicalBiologyTestDatas = new HashSet<EDChemicalBiologyTestData>();
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
        public virtual ICollection<EDChemicalBiologyTestData> EDChemicalBiologyTestDatas { get; set; }
        public int Version { get; set; }
    }
}
