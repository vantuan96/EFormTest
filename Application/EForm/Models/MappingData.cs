using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EForm.Models
{
    public class MappingData
    {
        public string Code { get; set; }
        public string Value { get; set; }
        public string EnValue { get; set; }
        public string ViName { get; set; }
        public string EnName { get; set; }
        public int? Order { get; set; }
        public string Group { get; set; }
        public string DataType { get; set; }
    }
}