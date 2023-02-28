using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EForm.Models.IPDModels
{
    public class IPDIASpecialRequestModel
    {
        public string Group { get; set; }
        public string ViName { get; set; }
        public string EnName { get; set; }
        public string ViValue { get; set; }
        public string EnValue { get; set; }
        public bool IsShow { get; set; }
    }
}