using Admin.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Models
{
    public class FiltersViewModel
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

        public Guid? FilterVisitTypeId { get; set; }
        public List<SelectListItem> ListFilterVisitTypes
        {
            get
            {
                var result = md.GetListVisitTypes();
                result.Insert(0, new SelectListItem { Value = Guid.Empty.ToString(), Text = "-- Tất cả loại thăm khám --", Selected = true });
                return result;
            }
        }
        public string Publish { get; set; }
        public List<SelectListItem> ListFilterTypes
        {
            get
            {
                var result = new List<SelectListItem>() { };
                result.Insert(0, new SelectListItem { Value = "0", Text = "-- Tất cả --", Selected = true });
                result.Insert(1, new SelectListItem { Value = "1", Text = "Đã triển khai"});
                result.Insert(2, new SelectListItem { Value = "2", Text = "Chưa triển khai"});
                return result;
            }
        }
    }
}