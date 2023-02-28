using Admin.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Models
{
    public class UnlockFormViewModel
    {
        public Guid Id { get; set; }
        public Guid VisitId { get; set; }
        public string Username { get; set; }
        public string RecordCode { get; set; }
        public string FormName { get; set; }
        public string ExpiredAt { get; set; }
        public bool IsDeleted { get; set; }
        public string LockType { get; set; }
    }

    public class CheckRecordCodeModel
    {
        public bool IsInvalidRecordCode { get; set; }
        public Guid VisitId { get; set; }
        public dynamic FormList { get; set; }
    }
}