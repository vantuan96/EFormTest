using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EForm.Models.PrescriptionModels
{
    public class VisitMedicalInfo
    {
        public string Code { get; set; }
        public string Value { get; set; }

        public VisitMedicalInfo(string code, string value)
        {
            Code = code;
            Value = value;
        }
    }
}
