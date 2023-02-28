using DataAccess.Model.BaseModel;
using DataAccess.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.GeneralModel
{
    public class LabOrderableRef : VBaseModel
    {
        public Guid LabOrderableRid { get; set; }
        public Nullable<Guid> ItemId { get; set; }
        public string LabSpecialProcessingGroupRcd { get; set; }
        public string LabOrderableCode { get; set; }
        public string ServiceCategoryRcd { get; set; }
        public string NameE { get; set; }
        public string NameL { get; set; }
        public string SeqNum { get; set; }
        public string ActiveStatus { get; set; }
        public Nullable<Guid> LuUserId { get; set; }
        public DateTime LuUpdated { get; set; }
    }
}
