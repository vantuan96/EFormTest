using DataAccess.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.IPDModel
{
    public class IPDVitalSignForPediatrics : VBaseModel
    {
        public string BloodVesselPews { get; set; }
        public string BreathingPews { get; set; }
        public string HypothermiaPews { get; set; }
        public string TotalPews { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string FormType { get; set; }
        public Guid VisitId { get; set; }
    }
}
