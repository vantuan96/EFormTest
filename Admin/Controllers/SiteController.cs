using Admin.Common;
using Admin.Common.Model;
using Admin.CustomAuthen;
using Admin.Models;
using DataAccess.Models;
using DataAccess.Repository;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Linq.Dynamic;

namespace Admin.Controllers
{
    [Authorize(Roles = Constant.AdminRoles.SuperAdmin + "," + Constant.AdminRoles.ManageData)]
    public class SiteController : Controller
    {
        protected IUnitOfWork unitOfWork = new EfUnitOfWork();

        // GET: Site
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetListSites(DataTablesQueryModel queryModel)
        {
            int totalResultsCount;
            var take = queryModel.length;
            var skip = queryModel.start;

            string sortBy = "";
            string sortDir = "";
            if (queryModel.order != null)
            {
                sortBy = queryModel.columns[queryModel.order[0].column].data;
                sortDir = queryModel.order[0].dir.ToLower();
            }

            var list = unitOfWork.SiteRepository.AsQueryable();
            totalResultsCount = list.Count();

            var result = list.OrderBy(sortBy + (sortDir == "desc" ? " descending" : "")).Skip(skip).Take(take).ToList()
                .Select(x => new SiteViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Code = x.Code,
                    ApiCode = x.ApiCode,
                    Location = x.Location,
                    Province = $"{x.LocationUnit} {x.Province}",  
                    Level = x.Level,
                    IsDeleted = x.IsDeleted
                });

            return Json(new
            {
                queryModel.draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = totalResultsCount,
                data = result
            });
        }

        public ActionResult GetSiteDetail(Guid? id)
        {
            if (id == null)
                return PartialView("_PartialViewModalSite", new SiteViewModel { IsEdit = false });
            else
            {
                var site = unitOfWork.SiteRepository.GetById(id.Value);
                if (site == null) return PartialView("_PartialViewModalSite", new SiteViewModel { IsEdit = false });
                return PartialView("_PartialViewModalSite", new SiteViewModel
                {
                    Id = site.Id,
                    Code = site.Code,
                    ApiCode = site.ApiCode,
                    Name = site.Name,
                    Location = site.Location,
                    LocationUnit = site.LocationUnit,
                    Province = site.Province,
                    Level = site.Level,
                    IsEdit = true
                });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveSite(SiteViewModel model)
        {
            try
            {
                var site = unitOfWork.SiteRepository.FirstOrDefault(s => s.Id == model.Id);
                if (site != null)
                {
                    site.Name = model.Name;
                    site.Code = model.Code;
                    site.ApiCode = model.ApiCode;
                    site.Location = model.Location;
                    site.LocationUnit = model.LocationUnit;
                    site.Province = model.Province;
                    site.Level = model.Level;
                    unitOfWork.Commit();
                    return Json(true);
                }
                else
                {
                    var newSite = new Site {
                        Name = model.Name,
                        Code = model.Code,
                        ApiCode = model.ApiCode,
                        Location = model.Location,
                        LocationUnit = model.LocationUnit,
                        Province = model.Province,
                        Level = model.Level,
                    };
                    unitOfWork.SiteRepository.Add(newSite);
                    unitOfWork.Commit();
                    return Json(true);
                }
            }
            catch (Exception)
            {
                return Json(false);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeactivateSite(Guid id)
        {
            try
            {
                var site = unitOfWork.SiteRepository.GetById(id);
                if (site == null)
                    return Json(false);
                else
                {
                    unitOfWork.SiteRepository.Delete(site);
                    unitOfWork.Commit();
                }
                return Json(true);
            }
            catch (Exception)
            {
                return Json(false);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActivateSite(Guid id)
        {
            try
            {
                var site = unitOfWork.SiteRepository.GetById(id);
                if (site == null)
                    return Json(false);
                else
                {
                    site.IsDeleted = false;
                    unitOfWork.Commit();
                }
                return Json(true);
            }
            catch (Exception)
            {
                return Json(false);
            }
        }
    }
}