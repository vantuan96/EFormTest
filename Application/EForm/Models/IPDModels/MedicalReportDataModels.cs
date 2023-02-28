using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EForm.Models.IPDModels
{
    public class MedicalReportDataModels
    {
        public string Code { get; set; }
        public string ViName { get; set; }
        public string EnName { get; set; }
        public string Value { get; set; }
        public string Clinic { get; set; }
        public int? Order { get; set; }
    }
}