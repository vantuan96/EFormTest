
using DataAccess.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccess.Models.DTOs
{
    public class TableDataDto: VBaseModel
    {
        public string Code { get; set; }
        public string Value { get; set; }
        public Guid Id2 { get; set; }
        public string FormCode { get; set; }
        public Guid IdRow { get; set; }
        public DateTime? CreatedRowAt { get; set; }
    }
}