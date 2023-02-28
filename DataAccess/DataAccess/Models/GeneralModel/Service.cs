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
    public class Service : VBaseModel
    {
        public Guid HISId { get; set; }
        public string ViName { get; set; }
        public string EnName { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        [Index]
        public string Code { get; set; }
        public int HISCode { get; set; }
        public DateTime HISLastUpdated { get; set; }
        // 0: OH
        // 1: EHOS
        public Guid? ServiceGroupId { get; set; }
        [ForeignKey("ServiceGroupId")]
        public virtual ServiceGroup ServiceGroup { get; set; }
        

        public bool IsActive { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Type { get; set; }
        public string CombinedName { get; set; }
        public string RootServiceGroupCode { get; set; }
        public Guid? RootServiceGroupId { get; set; }
        public string ServiceType { get; set; } // Lab, Rad, Allies
        public bool IsDiagnosticReporting { get; set; }
    }
}
