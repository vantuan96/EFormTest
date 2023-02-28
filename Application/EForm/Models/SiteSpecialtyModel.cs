using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EForm.Models
{
    public class SiteSpecialtyModel
    {
        public Guid Id { get; set; }        
        public string Name { get; set; }
        public dynamic Specialities { get; set; }
    }
    public class SiteInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ViName { get; set; }
        public string EnName { get; set; }
        public string Code { get; set; }
        public string ApiCode { get; set; }
    }
    
}