using Admin.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Models
{
    public class ClinicViewModel
    {
        private MasterData md = new MasterData();

        public Guid Id { get; set; }
        public string ViName { get; set; }
        public string EnName { get; set; }
        public string Code { get; set; }
        public string Specialty { get; set; }
        public string Site { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsEdit { get; set; }
        public Guid? SiteId { get; set; }
        public Guid? SpecialtyId { get; set; }
        public string SetUpClinicDatas { get; set; }

        private List<SetupClinicViewModel> setUpClinic;

        public List<SetupClinicViewModel> SetUpClinic
        {
            get => setUpClinic;
            set
            {
                this.setUpClinic = new List<SetupClinicViewModel>(value);
            }
        }

        public List<SelectListItem> ListSites
        {
            get
            {
                return md.GetListSites();
            }
        }
        public List<SelectListItem> ListSpecialties
        {
            get
            {
                return md.GetListSpecialties(visit_type_code: "OPD");
            }
        }
    }

    public class FiltersClinicViewModel
    {
        private MasterData md = new MasterData();

        public Guid? FilterSiteId { get; set; }
        public List<SelectListItem> ListFilterSites
        {
            get
            {
                var result = md.GetListSites();
                result.Insert(0, new SelectListItem { Value = Guid.Empty.ToString(), Text = "-- Tất cả bệnh viện --", Selected = true });
                return result;
            }
        }

        public Guid? FilterSpecialtyId { get; set; }
        public List<SelectListItem> ListFilterVisitSpecialty
        {
            get
            {
                var result = md.GetListSpecialties(visit_type_code:"OPD");
                result.Insert(0, new SelectListItem { Value = Guid.Empty.ToString(), Text = "-- Tất cả chuyên khoa --", Selected = true });
                return result;
            }
        }
    }

    public class SetupClinicViewModel
    {
        public string ViName { get; set; }
        public string Code { get; set; }
        public bool Status { get; set; } = false;
    }
}