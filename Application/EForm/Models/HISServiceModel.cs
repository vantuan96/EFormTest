using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EForm.Models
{
    public class HISServiceGroupModel
    {
        public Guid? ParentServiceGroupId { get; set; }
        public Guid ServiceGroupId { get; set; }
        public string ServiceGroupCode { get; set; }
        public string ServiceGroupViName { get; set; }
        public string ServiceGroupEnName { get; set; }
        public DateTime HISLastUpdated { get; set; }
        public bool IsActive { get; set; }
        public string ServiceType { get; set; }
        public string KeyStruct { get; set; }
    }
    public class HISServiceModel
    {
        public Guid ServiceId { get; set; }
        public Guid? ParentServiceGroupId { get; set; }
        public Guid ServiceGroupId { get; set; }
        public string ServiceGroupCode { get; set; }
        public string ServiceGroupViName { get; set; }
        public string ServiceGroupEnName { get; set; }
        public string ServiceCode { get; set; }
        public string ServiceViName { get; set; }
        public string ServiceEnName { get; set; }
        public string ItemHospital { get; set; }
        public int HISCode { get; set; }
        public DateTime HISLastUpdated { get; set; }
        public bool IsActive { get; set; }
        public string ServiceType { get; set; }
        public decimal? Price { get; set; }
        public string CostCentreId { get; set; }
        public string GLAcc { get; set; }
        public dynamic PriceWarMsg { get; set; }
        public DateTime? EffectFrom { get; set; }
        public DateTime? EffectTo { get; set; }
        public decimal? EffectPrice { get; set; }
        public string Type { get; set; }

    }
}
