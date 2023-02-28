using Admin.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Models
{
    public class RoleViewModel
    {
        private MasterData md = new MasterData();

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Hospital { get; set; }
        public string Role { get; set; }
        public bool IsChecked { get; set; }

        public string ViName { get; set; }
        public string EnName { get; set; }
        public Guid? VisitTypeGroupId { get; set; }
        public List<string> Actions { get; set; }
        public List<string> Specialties { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsEdit { get; set; }

        public List<SelectListItem> ListVisitTypes => md.GetListVisitTypes();
        public List<SelectListItem> ListActions => VisitTypeGroupId.HasValue ? md.GetListActions(VisitTypeGroupId.Value.ToString()) : md.GetListActions(string.Empty);
        public List<SelectListItem> ListSpecialties => VisitTypeGroupId.HasValue ? md.GetListSpecialties(VisitTypeGroupId.Value.ToString()) : md.GetListSpecialties(string.Empty);
    }
}