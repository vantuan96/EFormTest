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
    public class CpoeOrderable: VBaseModel
    {
        public Guid CpoeOrderableId { get; set; }
        public Nullable<Guid> PhGenericDrugId { get; set; }
        public string ServiceCategoryRcd { get; set; }
        public Nullable<Guid> LabOrderableRid { get; set; }
        public Nullable<Guid> GenericOrderableServiceCodeRid { get; set; }
        public Nullable<Guid> RadiologyProcedurePlanRid { get; set; }
        public Nullable<Guid> PhPharmacyProductId { get; set; }
        public Nullable<Guid> PackageItemId { get; set; }
        public string CpoeOrderableTypeRcd { get; set; }
        public string OverrideNameE { get; set; }
        public string OverrideNameL { get; set; }
        public Nullable<Guid> LuUserId { get; set; }
        public string SeqNum { get; set; }
        public DateTime LuUpdated { get; set; }
        public DateTime EffectiveFromDateTime { get; set; }
        public DateTime? EffectiveToDateTime { get; set; }
        public string Comments { get; set; }
        public string FillerNameE { get; set; }
        public string FillerNameL { get; set; }
       
    }
}
