using DataAccess.Models.BaseModel;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.EDModel
{
    public class EDBase : VBaseModel
    {
        public int Version { get; set; } = 1;
        public Guid? VisitId { get; set; }
        [ForeignKey("VisitId")]
        public virtual ED Visit { get; set; }
    }
}
