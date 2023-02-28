using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EForm.Models
{
    public class FormDataValue
    {
        // f.Id, f.Code, f.Value, f.FormId, f.FormCode
        public string Code { get; set; }
        public string Value { get; set; }
        public string FormCode { get; set; }
        public Guid? Id { get; set; }
        public Guid? FormId { get; set; }
        public List<FormDataValue> Items { get; set; }
    }

    public class FormConfirms
    {
        // f.Id, f.Code, f.Value, f.FormId, f.FormCode
        public string Code { get; set; }
        public string Value { get; set; }
        public string FormCode { get; set; }
        public Guid? Id { get; set; }
        public Guid? FormId { get; set; }
    }
    public class MasterdataForm
    {
        public Guid? Id { get; set; }
        public Guid? VisitId { get; set; }
        public bool IsDeleted { get; set; }
        public List<FormDataValue> Datas { get; set; }
    }
    public class MasterdataForms
    {
        public Guid? VisitId { get; set; }
        public Guid? FormId { get; set; }
        public string FormCode { get; set; }
        public List<MasterdataForm> Forms { get; set; }
    }

}