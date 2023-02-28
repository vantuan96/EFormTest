using DataAccess.Model.BaseModel;
using DataAccess.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.IPDModel
{
    public class IPDMedicalRecord : VBaseModel
    {
        public IPDMedicalRecord()
        {
            this.IPDMedicalRecordDatas = new HashSet<IPDMedicalRecordData>();
        } 
        public Guid? IPDMedicalRecordPart1Id { get; set; }
        [ForeignKey("IPDMedicalRecordPart1Id")]
        public virtual IPDMedicalRecordPart1 IPDMedicalRecordPart1 { get; set; }
        public Guid? IPDMedicalRecordPart2Id { get; set; }
        [ForeignKey("IPDMedicalRecordPart2Id")]
        public virtual IPDMedicalRecordPart2 IPDMedicalRecordPart2 { get; set; }
        public Guid? IPDMedicalRecordPart3Id { get; set; }
        [ForeignKey("IPDMedicalRecordPart3Id")]
        public virtual IPDMedicalRecordPart3 IPDMedicalRecordPart3 { get; set; }

        public virtual ICollection<IPDMedicalRecordData> IPDMedicalRecordDatas { get; set; }

        public int? Version { get; set; } = 1;
    }
}
