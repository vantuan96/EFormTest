using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models
{
    public class VisitTypeViewModel
    {
        public Guid Id { get; set; }
        public string ViName { get; set; }
        public string EnName { get; set; }
        public string Code { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsEdit { get; set; }
    }
}