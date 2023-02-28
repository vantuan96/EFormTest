using DataAccess.Model.BaseModel;
using System;
using System.Collections.Generic;

namespace DataAccess.Models.IPDModel
{
    public class IPDMedicalRecordPart2 : IEntity
    {
        public IPDMedicalRecordPart2()
        {
            this.IPDMedicalRecordPart2Datas = new HashSet<IPDMedicalRecordPart2Data>();
        }
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual ICollection<IPDMedicalRecordPart2Data> IPDMedicalRecordPart2Datas { get; set; }
    }
}
