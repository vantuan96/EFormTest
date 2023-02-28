using DataAccess.Model.BaseModel;
using DataAccess.Models.BaseModel;
using System;
using System.Collections.Generic;

namespace DataAccess.Models.IPDModel
{
    public class IPDMedicalRecordPart1 : VBaseModel
    {
        public IPDMedicalRecordPart1()
        {
            this.IPDMedicalRecordPart1Datas = new HashSet<IPDMedicalRecordPart1Data>();
        }
        public virtual ICollection<IPDMedicalRecordPart1Data> IPDMedicalRecordPart1Datas { get; set; }
    }
}
