using Admin.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Models
{
    public class SpecialtyViewModel
    {
        private MasterData md = new MasterData();

        public Guid Id { get; set; }
        public string ViName { get; set; }
        public string EnName { get; set; }
        public string Code { get; set; }
        public string Site { get; set; }
        public string VisitType { get; set; }
        public string Publish { get; set; }
        public bool IsPublish { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsEdit { get; set; }
        public Guid? SiteId { get; set; }
        public Guid? VisitTypeId { get; set; }

        public List<SelectListItem> ListSites
        {
            get
            {
                return md.GetListSites();
            }
        }
        public List<SelectListItem> ListVisitTypes
        {
            get
            {
                return md.GetListVisitTypes();
            }
        }
        public string LocationCode { get; set; }
        public bool IsAnesthesia { get; set; }
    }
}