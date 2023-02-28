using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models
{
    public class ActionViewModel
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }
    }
}