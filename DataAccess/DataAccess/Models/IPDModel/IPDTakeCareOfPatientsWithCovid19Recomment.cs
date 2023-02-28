using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.IPDModel
{
    public class IPDTakeCareOfPatientsWithCovid19Recomment : IPDBase
    {
        public DateTime? HandOverAt { get; set; }
        public string HandOverBy { get; set; }
        public DateTime? ReceivingAt { get; set; }
        public string ReceivingBy { get; set; }
    }
}
