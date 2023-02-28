using DataAccess.Model.BaseModel;
using DataAccess.Models.BaseModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.IPDModel
{
    public class IPDGlamorganData : VBaseModel
    {
        public string Code { get; set; }
        public string Value { get; set; }
        public string FormCode { get; set; }
        public Guid? IPDGlamorganId { get; set; }
        [ForeignKey("IPDGlamorganId")]
        public virtual IPDGlamorgan IPDGlamorgan { get; set; }
    }
}
