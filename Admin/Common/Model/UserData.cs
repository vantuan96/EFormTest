using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Common.Model
{
    public class UserData
    {
        public string FullName { get; set; }
        public string DisplayName { get; set; }
        public List<string> Roles { get; set; }
    }
}