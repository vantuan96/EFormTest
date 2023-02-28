using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EForm.Models.EIOModels
{
    public class EIOJointConsultationForApprovalOfSurgeryResponse
    {
        public Guid? Id { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }
        public string EnValue { get; set; }
        public int PkntVersion { get; set; } // Check Version Phiếu khám ngoại trú
    }
}