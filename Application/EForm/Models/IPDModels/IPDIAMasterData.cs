using DataAccess.Models.IPDModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EForm.Models.IPDModels
{
    public class IPDIAMasterForAdultData
    {
        public IPDInitialAssessmentForAdultData Data { get; set; }
        public string Code { get; set; }
        public string Group { get; set; }
        public int? Order { get; set; }
        public string Note { get; set; }
        public string ViName { get; set; }
        public string EnName { get; set; }
        public string Value { get; set; }
        public string DataType { get; set; }
    }

    public class IPDIAMasterForChemotherapyData
    {
        public IPDInitialAssessmentForChemotherapyData Data { get; set; }
        public string Code { get; set; }
        public string Group { get; set; }
        public int? Order { get; set; }
        public string Note { get; set; }
        public string ViName { get; set; }
        public string EnName { get; set; }
        public string Value { get; set; }
        public string DataType { get; set; }
    }

    public class IPDIAMasterForFrailElderlyData
    {
        public IPDInitialAssessmentForFrailElderlyData Data { get; set; }
        public string Code { get; set; }
        public string Group { get; set; }
        public int? Order { get; set; }
        public string Note { get; set; }
        public string ViName { get; set; }
        public string EnName { get; set; }
        public string Value { get; set; }
        public string DataType { get; set; }
    }
}