using System;
using System.Collections.Generic;

namespace EForm.Models
{
    public class TranslateParameterModel : PagingParameterModel
    {
        public string Search { get; set; }

        public string[] ConvertedSearch
        {
            get
            {
                return this.Search.Trim().Split(',');
            }
        }
        public string Status { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string User { get; set; }
    }

    public class QueryTranslateModel
    {
        public Guid Id { get; set; }
        public string PID { get; set; }
        public string VisitCode { get; set; }
        public string VisitTypeGroupCode { get; set; }
        public string CustomerName { get; set; }
        public dynamic TranslationName { get; set; }
        public DateTime? RequestTime { get; set; }
        public int Status { get; set; }
        public string RequestedUsername { get; set; }
        public string TranslatedUsername { get; set; }
        public string SpecialtyName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Phone { get; set; }
    }

    public class TranslateModel
    {
        public Guid Id { get; set; }
        public string PID { get; set; }
        public string VisitCode { get; set; }
        public string VisitTypeGroupCode { get; set; }
        public string CustomerName { get; set; }
        public dynamic TranslationName { get; set; }
        public string RequestTime { get; set; }
        public dynamic Status { get; set; }
        public string RequestedBy { get; set; }
        public string TranslatedBy { get; set; }
        public string SpecialtyName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Phone { get; set; }
    }

    public class TranslateRequest
    {
        public Guid VisitId { get; set; }
        public string VisitTypeGroupCode { get; set; }
        public string ViName { get; set; }
        public string EnName { get; set; }
        public string FromLanguage { get; set; }
        public string ToLanguage { get; set; }
        public Guid FormId { get; set; }
        public string FormCode { get; set; }
    }

}