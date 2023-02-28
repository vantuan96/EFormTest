using System;
using System.Collections.Generic;

namespace DataAccess.Models.IPDModel
{
    public class IPDDischargePreparationChecklist : IPDBase
    {
        public IPDDischargePreparationChecklist()
        {
            this.IPDDischargePreparationChecklistDatas = new HashSet<IPDDischargePreparationChecklistData>();
        }
        public string Type { get; set; }

        public DateTime? Time { get; set; }
        public virtual ICollection<IPDDischargePreparationChecklistData> IPDDischargePreparationChecklistDatas { get; set; }
    }
}
