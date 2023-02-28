using DataAccess.Model.BaseModel;
using DataAccess.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.GeneralModel
{
    public class RadiologyProcedurePlanRef : VBaseModel
    {
        public Guid RadiologyProcedurePlanRid { get; set; }
        public string ShortCode { get; set; }
        public string RadiologyProcedureNameE { get; set; }
        public string RadiologyProcedureNameL { get; set; }
        public string ActiveStatus { get; set; }
        public string ServiceCategoryCode { get; set; }
        public string ServiceCategoryNameL { get; set; }
        public string ServiceCategoryNameE { get; set; }
        public string DicomModality { get; set; }
        public DateTime LuUpdated { get; set; }
    }
}
