using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class AdminRole
    {
        [Key]
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public virtual ICollection<UserAdminRole> UserAdminRoles { get; set; }
    }
}
