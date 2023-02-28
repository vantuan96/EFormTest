using System;
using System.Collections.Generic;

namespace EMRModels
{
    public class EsignRequestModel
    {
        List<ConfirmData> Datas { get; set; }
        public string FormCode { get; set; }
        public Guid FormId { get; set; }

    }
    public class ConfirmData
    {
        public string Code { get; set; }
        public string Value { get; set; }
    }
}