using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMRModels
{
    class ApiResponseModel
    {
        
    }
    public class SyncAreasModel
    {
        public string area_id { get; set; }
        public string Type { get; set; }
        public string SiteCode { get; set; }
        public string areaNameE { get; set; }
        public string areaNameV { get; set; }
        public string area_code { get; set; }
        public string costcentre_code { get; set; }
        public string CostCentreNameV { get; set; }
        public string CostCentreNameE { get; set; }

    }
}
