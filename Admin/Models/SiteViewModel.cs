using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models
{
    public class SiteViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ApiCode { get; set; }
        public string Location { get; set; }
        public string LocationUnit { get; set; }
        public string Province { get; set; }
        public string Level { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsEdit { get; set; }
    }
}