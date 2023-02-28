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
using System.Collections.Generic;
using DataAccess.Models.IPDModel;
using MasterDataForm = DataAccess.Models.MasterData;
using Admin.Common.Extentions;

namespace Admin.Controllers
{
    [Authorize(Roles = Constant.AdminRoles.SuperAdmin + "," + Constant.AdminRoles.ManageData)]
    public class SpecialtyController : Controller
    {
        protected IUnitOfWork unitOfWork = new EfUnitOfWork();

        // GET: Specialty
        public ActionResult Index()
        {
            var filter = new FiltersViewModel();
            return View(filter);
        }
        private bool CheckBd(List<IPDSetupMedicalRecord> medicalRecord, string formcode)
        {
            foreach (var item in medicalRecord)
            {
                if (item.Formcode == formcode)
                {
                    return true;
                }
            }
            return false;
        }

        private List<MasterDataForm> GetFormMedicalRecordFromMasterData(string visitType)
        {
            var fromMasterData = unitOfWork.MasterDataRepository.AsQueryable()
                                .Where(m => !m.IsDeleted && (m.Group == "MedicalRecords" && m.Note == visitType) || (m.Group == "PromissoryNote" && m.Note == visitType))
                                .OrderBy(m => m.Group)
                                .ToList();
            if (fromMasterData == null || fromMasterData.Count == 0) return new List<MasterDataForm>() { };

            return fromMasterData;
        }

        [HttpGet]
        public JsonResult GetListIPDMedicalRecords(Guid? visitTypeId)
        {
            try
            {
                if (visitTypeId == null) return Json(false);
                var checkVisitType = unitOfWork.VisitTypeGroupRepository.AsQueryable()
                                    .Where(v => v.Id == visitTypeId)
                                    .FirstOrDefault();
                if (checkVisitType == null) return Json(false);
                if (checkVisitType.Code == "IPD" || checkVisitType.Code == "OPD")
                {
                    var _listMedicalRecords = GetFormMedicalRecordFromMasterData(checkVisitType.Code);
                    var list = _listMedicalRecords
                           .Select(s => new
                           {
                               Formcode = s.Form,
                               ViName = s.ViName,
                               IsDeploy = false                             
                           })
                           .Distinct().ToList();
                    return Json(new { data = list, VisitTypeCode = checkVisitType.Code, msg = "Lấy dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(new { data = "" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }
        [HttpGet]
        public JsonResult GetListIPDMedicalRecord(Guid? id)
        {
            try
            {
                if (id != null)
                {
                    var visitType = (from spec in unitOfWork.SpecialtyRepository.AsQueryable()
                                     join vt in unitOfWork.VisitTypeGroupRepository.AsQueryable()
                                     on spec.VisitTypeGroupId equals vt.Id
                                     where spec.Id == id
                                     select vt).FirstOrDefault();
                    if (visitType.Code == "IPD" || visitType.Code == "OPD")
                    {
                        var medicalRecords = (from me in unitOfWork.IPDSetupMedicalRecordRepository.AsQueryable()
                                              where me.SpecialityId == id
                                              select me).ToList();
                        var _listMedicalRecord = GetFormMedicalRecordFromMasterData(visitType.Code);
                        if (medicalRecords.Count < _listMedicalRecord.Count)
                        {
                            foreach (var item in _listMedicalRecord)
                            {
                                bool check = CheckBd(medicalRecords, item.Form);
                                if (check) continue;
                                else
                                {
                                    var addMedical = new IPDSetupMedicalRecord()
                                    {
                                        SpecialityId = (Guid)id,
                                        Formcode = item.Form,
                                        ViName = item.ViName,
                                        EnName = item.EnName,
                                        IsDeploy = false,
                                        Type = item.Code,
                                        FormType = item.Group
                                    };
                                    unitOfWork.IPDSetupMedicalRecordRepository.Add(addMedical);
                                    medicalRecords.Add(addMedical);
                                }
                            }
                            unitOfWork.Commit();
                            medicalRecords.OrderBy(m => m.FormType);
                            return Json(new { data = medicalRecords }, JsonRequestBehavior.AllowGet);
                        }
                        return Json(new { data = medicalRecords }, JsonRequestBehavior.AllowGet);
                    }
                    return Json(new { data = "" }, JsonRequestBehavior.AllowGet);
                }
                return Json(false);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        public class StatusIPDMedicalRecord
        {
            public bool Status { get; set; }
            public string Code { get; set; }
        }

        [HttpPost]
        public ActionResult SetupIPDMedicalRecord(Guid model, List<StatusIPDMedicalRecord> setStatus)
        {
            try
            {
                if(model == null)
                {
                    return Json(false);
                }
                if(setStatus == null || setStatus.Count == 0)
                {
                    return Json(false);
                }

                Guid specialtyId = model;
                var specialty = unitOfWork.SpecialtyRepository.FirstOrDefault(s => !s.IsDeleted && s.Id == specialtyId);
                if (specialty == null)
                    return Json(false);

                string visitType = specialty.VisitTypeGroup?.Code;
                if (string.IsNullOrEmpty(visitType))
                    return Json(false);

                foreach (var item in setStatus)
                {
                    var listMedical = unitOfWork.IPDSetupMedicalRecordRepository.AsQueryable()
                                      .Where(x => x.SpecialityId == specialtyId && x.Formcode == item.Code)
                                      .FirstOrDefault();
                    if(listMedical == null)
                    {
                        var _listMedicalRecord = GetFormMedicalRecordFromMasterData(visitType);
                        var medical = (from m in _listMedicalRecord
                                       where m.Form == item.Code
                                       select m).FirstOrDefault();

                        IPDSetupMedicalRecord newSetup = new IPDSetupMedicalRecord()
                        {
                            SpecialityId = specialtyId,
                            Formcode = item.Code,
                            IsDeploy = item.Status,
                            ViName = medical.ViName,
                            EnName = medical.EnName,
                            Type = medical.Code,
                            FormType = medical.Group
                        };
                        unitOfWork.IPDSetupMedicalRecordRepository.Add(newSetup);
                    }
                    else
                        listMedical.IsDeploy = item.Status;
                        unitOfWork.IPDSetupMedicalRecordRepository.Update(listMedical);
                }
                unitOfWork.Commit();
                return Json(true);
            }
            catch (Exception)
            {
                return Json(false);
            }
        }
        public ActionResult GetListSpecialties(DataTablesQueryModel queryModel)
        {
            Guid filterSite;
            Guid filterVisitType;
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

            var list = unitOfWork.SpecialtyRepository.AsQueryable();

            #region Filtering
            if (Guid.TryParse(queryModel.columns.First(s => s.name == "Site").search.value, out filterSite))
            {
                if (filterSite != Guid.Empty)
                    list = list.Where(s => s.SiteId == filterSite);
            }

            if (Guid.TryParse(queryModel.columns.First(s => s.name == "VisitType").search.value, out filterVisitType))
            {
                if (filterVisitType != Guid.Empty)
                    list = list.Where(s => s.VisitTypeGroupId == filterVisitType);
            }
            if (queryModel.columns.First(s => s.name == "Publish").search.value != null &&
                queryModel.columns.First(s => s.name == "Publish").search.value != "0")
            {
                bool ispublic = queryModel.columns.First(s => s.name == "Publish").search.value == "1";
                list = list.Where(s => s.IsPublish == ispublic);
            }

            var filterName = queryModel.columns.First(s => s.name == "ViName").search.value?.Trim();
            if (!string.IsNullOrEmpty(filterName))
                list = list.Where(s => s.ViName.Trim().ToLower().Contains(filterName.Trim().ToLower()));
            #endregion

            totalResultsCount = list.Count();

            var result = list.OrderBy(sortBy + (sortDir == "desc" ? " descending" : "")).Skip(skip).Take(take).ToList()
                .Select(x => new SpecialtyViewModel
                 {
                     Id = x.Id,
                     ViName = x.ViName,
                     EnName = x.EnName,
                     Code = x.Code,
                     Site = x.Site.Name,
                     VisitType = x.VisitTypeGroup.ViName,
                     Publish = x.IsPublish ? "Đã" : "Chưa",
                     IsDeleted = x.IsDeleted,
                     LocationCode = x.LocationCode
                 });

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
        public ActionResult DeactivateSpecialty(Guid id)
        {
            try
            {
                var specialty = unitOfWork.SpecialtyRepository.GetById(id);
                if (specialty == null)
                    return Json(false);
                else
                {
                    unitOfWork.SpecialtyRepository.Delete(specialty);
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
        public ActionResult ActivateSpecialty(Guid id)
        {
            try
            {
                var specialty = unitOfWork.SpecialtyRepository.GetById(id);
                if (specialty == null)
                    return Json(false);
                else
                {
                    specialty.IsDeleted = false;
                    unitOfWork.Commit();
                }
                return Json(true);
            }
            catch (Exception)
            {
                return Json(false);
            }
        }

        public ActionResult GetSpecialtyDetail(Guid? id)
        {
            if (id == null)
                return PartialView("_PartialViewModalSpecialty", new SpecialtyViewModel { IsEdit = false });
            else
            {
                var specialty = unitOfWork.SpecialtyRepository.GetById(id.Value);
                if (specialty == null) return PartialView("_PartialViewModalSpecialty", new SpecialtyViewModel { IsEdit = false });
                return PartialView("_PartialViewModalSpecialty", new SpecialtyViewModel
                {
                    Id = specialty.Id,
                    Code = specialty.Code,
                    ViName = specialty.ViName,
                    EnName = specialty.EnName,
                    SiteId = specialty.SiteId,
                    VisitTypeId = specialty.VisitTypeGroupId,
                    IsPublish = specialty.IsPublish,
                    IsEdit = true,
                    LocationCode = specialty.LocationCode,
                    VisitType = specialty.VisitTypeGroup?.Code,
                    IsAnesthesia = specialty.IsAnesthesia
                });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveSpecialty(SpecialtyViewModel model)
        {
            try
            {
                var specialty = unitOfWork.SpecialtyRepository.FirstOrDefault(s => s.Id == model.Id);
                if (specialty != null)
                {
                    specialty.ViName = model.ViName;
                    specialty.EnName = model.EnName;
                    specialty.Code = model.Code;
                    specialty.SiteId = model.SiteId;
                    specialty.VisitTypeGroupId = model.VisitTypeId;
                    specialty.IsPublish = model.IsPublish;
                    specialty.LocationCode = model.LocationCode;
                    unitOfWork.SpecialtyRepository.Update(specialty);
                    specialty.IsAnesthesia = model.IsAnesthesia;
                    unitOfWork.Commit();
                    var obj = specialty.Id;
                    return Json(new {data = obj },JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var newSpecialty = new Specialty
                    {
                        ViName = model.ViName,
                        EnName = model.EnName,
                        Code = model.Code,
                        SiteId = model.SiteId,
                        VisitTypeGroupId = model.VisitTypeId,
                        IsPublish = model.IsPublish,
                        LocationCode = model.LocationCode,
                        IsAnesthesia = model.IsAnesthesia
                    };
                    unitOfWork.SpecialtyRepository.Add(newSpecialty);
                    unitOfWork.Commit();
                    var obj = newSpecialty.Id;
                    return Json(new { data = obj},JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return Json(false);
            }
        }
    }
}