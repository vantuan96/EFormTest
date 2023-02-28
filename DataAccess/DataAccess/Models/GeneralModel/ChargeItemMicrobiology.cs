using DataAccess.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.GeneralModel
{
    public class ChargeItemMicrobiology: VBaseModel
    {
        public string STypeGroupID { get; set; }
        public string STypeID { get; set; }
        public string Stratified { get; set; }
        public bool IsNotUse { get; set; }
    }
}
