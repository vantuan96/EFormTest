using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EForm.Models
{
    public class DietCodeParameterModel: PagingParameterModel
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
