using DataAccess.Model.BaseModel;
using DataAccess.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.GeneralModel
{
    public class FormDatas : VBaseModel
    {
        public string Code { get; set; }
        public string Value { get; set; }
        public string FormCode { get; set; }
        public string VisitType { get; set; }
        public Guid? FormId { get; set; }
        public Guid? VisitId { get; set; }
    }
}
