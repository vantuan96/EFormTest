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
    public class VisitTypeGroupController : Controller
    {
        protected IUnitOfWork unitOfWork = new EfUnitOfWork();

        // GET: Site
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetListVisitTypes(DataTablesQueryModel queryModel)
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

            var list = unitOfWork.VisitTypeGroupRepository.AsQueryable();
            totalResultsCount = list.Count();

            var result = list.OrderBy(sortBy + (sortDir == "desc" ? " descending" : "")).Skip(skip).Take(take).ToList()
                .Select(x => new VisitTypeViewModel
                {
                    Id = x.Id,
                    EnName = x.EnName,
                    Code = x.Code,
                    ViName = x.ViName,
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

        public ActionResult GetVisitTypeDetail(Guid? id)
        {
            if (id == null)
                return PartialView("_PartialViewModalVisitType", new VisitTypeViewModel { IsEdit = false });
            else
            {
                var visit = unitOfWork.VisitTypeGroupRepository.GetById(id.Value);
                if (visit == null) return PartialView("_PartialViewModalVisitType", new VisitTypeViewModel { IsEdit = false });
                return PartialView("_PartialViewModalVisitType", new VisitTypeViewModel
                {
                    Id = visit.Id,
                    Code = visit.Code,
                    ViName = visit.ViName,
                    EnName = visit.EnName,
                    IsEdit = true
                });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveVisitType(VisitTypeViewModel model)
        {
            try
            {
                var visit = unitOfWork.VisitTypeGroupRepository.FirstOrDefault(s => s.Id == model.Id);
                if (visit != null)
                {
                    visit.EnName = model.EnName;
                    visit.ViName = model.ViName;
                    visit.Code = model.Code;
                    unitOfWork.Commit();
                    return Json(true);
                }
                else
                {
                    var newSite = new VisitTypeGroup
                    {
                        EnName = model.EnName,
                        ViName = model.ViName,
                        Code = model.Code,
                    };
                    unitOfWork.VisitTypeGroupRepository.Add(newSite);
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
        public ActionResult DeactivateVisitType(Guid id)
        {
            try
            {
                var visit = unitOfWork.VisitTypeGroupRepository.GetById(id);
                if (visit == null)
                    return Json(false);
                else
                {
                    unitOfWork.VisitTypeGroupRepository.Delete(visit);
                    unitOfWork.Commit();
                }
                return Json(true);
            }
            catch (Exception e)
            {
                return Json(false);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActivateVisitType(Guid id)
        {
            try
            {
                var visit = unitOfWork.VisitTypeGroupRepository.GetById(id);
                if (visit == null)
                    return Json(false);
                else
                {
                    visit.IsDeleted = false;
                    unitOfWork.Commit();
                }
                return Json(true);
            }
            catch (Exception e)
            {
                return Json(false);
            }
        }
    }
}