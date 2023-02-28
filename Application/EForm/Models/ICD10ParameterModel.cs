using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EForm.Models
{
    public class ICD10ParameterModel: PagingParameterModel
    {
        public string Search { get; set; }
        public string ConvertedSearch
        {
            get
            {
                if (string.IsNullOrEmpty(this.Search))
                {
                    return "";
                }
                return this.Search.Trim().ToLower();
            }
        }
    }
}