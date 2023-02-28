using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EForm.Models.IPDModels
{
    public class MEWItemModel
    {
        public double? MEWValue { get; set; }
        public string MEWRate { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string CreatedBy { get; set; }

    }
}
