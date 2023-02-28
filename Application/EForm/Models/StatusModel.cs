using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EForm.Models
{
    public class StatusViewModel
    {
        public Guid Id { get; set; }
        public string EnName { get; set; }
        public string ViName { get; set; }
        public string Code { get; set; }
        public string StatusCode { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}