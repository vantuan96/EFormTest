using DataAccess.Model.BaseModel;
using DataAccess.Models.BaseModel;
using DataAccess.Models.EDModel;
using DataAccess.Models.OPDModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    public class TableData: VBaseModel
    {
        public string Code { get; set; }
        public string Value { get; set; }
        public Guid Id2 { get; set; }
        public string FormCode { get; set; }
        public Guid IdRow { get; set; }
        public DateTime? CreatedRowAt { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Order { get; set; }
    }
}
