using DataAccess.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.IPDModel
{
    public class IPDAdultVitalSignMonitorData : VBaseModel
    {
        public string Code { get; set; }
        public string Value { get; set; }
        public Guid? IPDAdultVitalSignMonitorId { get; set; }
        [ForeignKey("IPDAdultVitalSignMonitorId")]
        public virtual IPDAdultVitalSignMonitor IPDAdultVitalSignMonitor { get; set; }
    }
}
