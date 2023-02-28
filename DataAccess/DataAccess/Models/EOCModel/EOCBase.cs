using DataAccess.Models.BaseModel;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.EOCModel
{
    public class EOCBase : VBaseModel
    {
        public int Version { get; set; } = 1;
        public Guid? VisitId { get; set; }
        [ForeignKey("VisitId")]
        public virtual EOC Visit { get; set; }
    }
}
