using DataAccess.Model.BaseModel;
using DataAccess.Models.BaseModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.GeneralModel
{
    public class ServiceGroup :VBaseModel
    {
        public Guid? HISParentId { get; set; }
        public Guid HISId { get; set; }
        public string ViName { get; set; }
        public string EnName { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        [Index]
        public string Code { get; set; }
        public int HISCode { get; set; }
        public bool? IsActive { get; set; }
        public string Type { get; set; }
        public string KeyStruct { get; set; }
        public DateTime? HISLastUpdated { get; set; }
        public string ServiceType { get; set; } // Lab, Rad, Allies
    }
}