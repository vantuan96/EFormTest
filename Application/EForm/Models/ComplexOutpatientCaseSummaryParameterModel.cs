using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EForm.Models
{
    public class ComplexOutpatientCaseSummaryParameterModel
    {
        public Guid? Id { get; set; }
        public string MainDiseases { get; set; }
        public string Orders { get; set; }
        public string VisitTypeGroupCode { get; set; }
        public Guid? VisitId { get; set; }
    }

    public class SyncComplexOutpatientCaseSummaryParameterModel
    {
        public string VisitTypeGroupCode { get; set; }
        public Guid? VisitId { get; set; }
    }

    public class ICDComplexOutpatientCaseSummary
    {
        public string Diagnosis { get; set; }
        public DateTime? ExaminationDate { get; set; }
        public dynamic ICDs { get; set; }
        public string PrimaryDoctor { get; set; }
        public dynamic Specialty { get; set; }
    }

    public class ICDReturnComplexOutpatientCaseSummary
    {
        public string Diagnosis { get; set; }
        public string ExaminationDate { get; set; }
        public dynamic ICDs { get; set; }
        public string PrimaryDoctor { get; set; }
        public dynamic Specialty { get; set; }
    }
}