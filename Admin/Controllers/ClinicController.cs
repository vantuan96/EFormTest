using Admin.Common;
using Admin.Common.Model;
using Admin.Models;
using DataAccess.Models;
using DataAccess.Repository;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Linq.Dynamic;
using System.Collections.Generic;

namespace Admin.Controllers
{
    [Authorize(Roles = Constant.AdminRoles.SuperAdmin + "," + Constant.AdminRoles.ManageData)]
    public class ClinicController : Controller
    {
        protected IUnitOfWork unitOfWork = new EfUnitOfWork();

        // GET: Specialty
        public ActionResult Index()
        {
            var filter = new FiltersClinicViewModel();
            return View(filter);
        }

        public ActionResult GetListClinics(DataTablesQueryModel queryModel)
        {
            Guid filterSite;
            Guid filterSpecialty;
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

            var list = from cli_sql in unitOfWork.ClinicRepository.AsQueryable()
                        join spe_sql in unitOfWork.SpecialtyRepository.AsQueryable() on cli_sql.SpecialtyId equals spe_sql.Id into slist
                        from spe_sql in slist.DefaultIfEmpty()
                        join sit_sql in unitOfWork.SiteRepository.AsQueryable() on spe_sql.SiteId equals sit_sql.Id into hlist
                        from sit_sql in hlist.DefaultIfEmpty()
                        select new ClinicViewModel
                        {
                            Id = cli_sql.Id,
                            ViName = cli_sql.ViName,
                            EnName = cli_sql.EnName,
                            Code = cli_sql.Code,
                            Specialty = spe_sql.ViName,
                            SpecialtyId = spe_sql.Id,
                            Site = sit_sql.Name,
                            SiteId = sit_sql.Id,
                            IsDeleted = cli_sql.IsDeleted
                        };

            #region Filtering
            if (Guid.TryParse(queryModel.columns.First(s => s.name == "Site").search.value, out filterSite))
            {
                if (filterSite != Guid.Empty)
                    list = list.Where(s => s.SiteId == filterSite);
            }

            if (Guid.TryParse(queryModel.columns.First(s => s.name == "Specialty").search.value, out filterSpecialty))
            {
                if (filterSpecialty != Guid.Empty)
                    list = list.Where(s => s.SpecialtyId == filterSpecialty);
            }

            var filterName = queryModel.columns.First(s => s.name == "ViName").search.value?.Trim();
            if (!string.IsNullOrEmpty(filterName))
                list = list.Where(s => s.ViName.Trim().ToLower().Contains(filterName.Trim().ToLower()));
            #endregion

            totalResultsCount = list.Count();

            var result = list.OrderBy(sortBy + (sortDir == "desc" ? " descending" : "")).Skip(skip).Take(take).ToList();

            return Json(new
            {
                queryModel.draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = totalResultsCount,
                data = result
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeactivateClinic(Guid id)
        {
            try
            {
                var clinic = unitOfWork.ClinicRepository.GetById(id);
                if (clinic == null)
                    return Json(false);
                else
                {
                    unitOfWork.ClinicRepository.Delete(clinic);
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
        public ActionResult ActivateClinic(Guid id)
        {
            try
            {
                var clinic = unitOfWork.ClinicRepository.GetById(id);
                if (clinic == null)
                    return Json(false);
                else
                {
                    clinic.IsDeleted = false;
                    unitOfWork.Commit();
                }
                return Json(true);
            }
            catch (Exception)
            {
                return Json(false);
            }
        }

        public ActionResult GetClinicDetail(Guid? id)
        {
            if (id == null)
                return PartialView("_PartialViewModalClinic", new ClinicViewModel { IsEdit = false, SetUpClinic = GetListSetUpClinic(id) });
            else
            {
                var clinic = unitOfWork.ClinicRepository.GetById(id.Value);
                var specialty = clinic.Specialty;
                var site = specialty?.Site;
                if (clinic == null) return PartialView("_PartialViewModalClinic", new ClinicViewModel { IsEdit = false });
                return PartialView("_PartialViewModalClinic", new ClinicViewModel
                {
                    Id = clinic.Id,
                    Code = clinic.Code,
                    ViName = clinic.ViName,
                    EnName = clinic.EnName,
                    SpecialtyId = specialty?.Id,
                    Specialty = specialty?.ViName,
                    SiteId = site?.Id,
                    Site = site?.Name,
                    IsEdit = true,
                    SetUpClinic = GetListSetUpClinic(clinic.Id)
                });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveClinic(ClinicViewModel model)
        {
            try
            {
                var clinic = unitOfWork.ClinicRepository.FirstOrDefault(s => s.Id == model.Id);
                if (clinic != null)
                {
                    clinic.ViName = model.ViName;
                    clinic.EnName = model.EnName;
                    clinic.Code = model.Code;
                    clinic.SpecialtyId = model.SpecialtyId;
                    clinic.SetUpClinicDatas = model.SetUpClinicDatas;
                    unitOfWork.Commit();
                    return Json(true);
                }
                else
                {
                    var newClinic = new Clinic
                    {
                        ViName = model.ViName,
                        EnName = model.EnName,
                        Code = model.Code,
                        SpecialtyId = model.SpecialtyId,
                        SetUpClinicDatas = model.SetUpClinicDatas
                    };
                    unitOfWork.ClinicRepository.Add(newClinic);
                    unitOfWork.Commit();
                    return Json(true);
                }
            }
            catch (Exception)
            {
                return Json(false);
            }
        }

        private List<SetupClinicViewModel> GetListSetUpClinic(Guid? clinicId)
        {
            List<SetupClinicViewModel> list_master = new List<SetupClinicViewModel>();
            if (clinicId == null)
            {
                list_master = GetSetupClinicFromMasterData();
                return list_master;
            }

            var clinic = unitOfWork.ClinicRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == clinicId);
            if (clinic == null)
            {
                list_master = GetSetupClinicFromMasterData();
                return list_master;
            }

            list_master = GetSetupClinicFromMasterData();

            string code_setup = clinic.SetUpClinicDatas;
            if (string.IsNullOrEmpty(code_setup))
                return list_master;

            var code_array = code_setup.Split(',').ToList();

            foreach (var item in list_master)
            {
                if (code_array.Contains(item.Code))
                    item.Status = true;
            }

            return list_master;
        }

        private List<SetupClinicViewModel> GetSetupClinicFromMasterData()
        {
            var master_list = (from m in unitOfWork.MasterDataRepository.AsQueryable()
                               where !m.IsDeleted && m.Group == Constant.SetupClinics
                               select new SetupClinicViewModel
                               {
                                   ViName = m.ViName,
                                   Code = m.Code,
                               }).ToList();

            return master_list;
        }
    }
}