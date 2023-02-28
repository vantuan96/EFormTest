using Admin.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Models
{
    public class UserViewModel
    {
        private MasterData md = new MasterData();

        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string EHOSAccount { get; set; }
        public string Department{ get; set; }
        public string Title { get; set; }
        public bool IsDeleted { get; set; }

        public bool IsAdminUser { get; set; }
        public Guid? PositionId { get; set; }
        public List<string> Roles { get; set; }
        public List<string> Positions { get; set; }
        public List<string> Specialties { get; set; }
        public List<string> AdminRoles { get; set; }

        public List<SelectListItem> ListPositions => md.GetListPositions();
        public List<SelectListItem> ListRoles => md.GetListRoles();
        public List<SelectListItem> ListSpecialties => md.GetListSpecialties();
        public List<SelectListItem> ListAdminRoles => md.GetListAdminRoles();
    }

    public class UserADCheckModel
    {
        public bool IsExistedAccount { get; set; }
        public bool IsInvalidADAccount { get; set; }
        public bool IsSpamADAccount { get; set; }
        public string DisplayName { get; set; }
        public string Department { get; set; }
        public string Title { get; set; }
    }

    public class UserAdminViewModel
    {
        private MasterData md = new MasterData();

        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Department { get; set; }
        public string Title { get; set; }

        public List<string> AdminRoles { get; set; }
        public List<SelectListItem> ListAdminRoles => md.GetListAdminRoles();
    }
}