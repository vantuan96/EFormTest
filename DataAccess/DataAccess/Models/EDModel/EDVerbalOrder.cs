using DataAccess.Model.BaseModel;
using DataAccess.Models.BaseModel;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.EDModel
{
    public class EDVerbalOrder : VBaseModel
    {
        public int Version { get; set; }
        public Guid? VisitId { get; set; }
    }
}
