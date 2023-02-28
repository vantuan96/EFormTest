using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models
{
    public class DeleteRecordViewModels
    {
        public Guid Id { get; set; }
        public string RecordCode { get; set; }
        public string CustomerInfo { get; set; }
        public string Nurse { get; set; }
        public string Doctor { get; set; }
    }
}