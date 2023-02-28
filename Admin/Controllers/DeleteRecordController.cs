using Admin.Common;
using Admin.Common.HandlerLogs;
using Admin.Common.Model;
using Admin.Models;
using DataAccess.Models;
using DataAccess.Models.OPDModel;
using DataAccess.Repository;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Admin.Controllers
{
    [Authorize(Roles = Constant.AdminRoles.SuperAdmin + "," + Constant.AdminRoles.ManageUnlock)]
    public class DeleteRecordController : Controller
    {
        protected IUnitOfWork unitOfWork = new EfUnitOfWork();
        public ActionResult Index()
        {
            var model = new DeleteRecordViewModels();
            return View(model);
        }

        public ActionResult GetListDeleteRecord(DataTablesQueryModel queryModel)
        {
            int totalResultsCount = 0;
            var list_result = new List<DeleteRecordViewModels>();

            var filterName = queryModel.columns.First(s => s.name == "RecordCode").search.value;
            if (string.IsNullOrEmpty(filterName))
                return Json(new
                {
                    queryModel.draw,
                    recordsTotal = totalResultsCount,
                    recordsFiltered = totalResultsCount,
                    data = list_result
                });

            var list_ed = unitOfWork.EDRepository.Find(e => !e.IsDeleted && filterName.ToLower().Contains(e.RecordCode.ToLower()));
            var list_opd = unitOfWork.OPDRepository.Find(e => !e.IsDeleted && filterName.ToLower().Contains(e.RecordCode.ToLower()));
            var list_ipd = unitOfWork.IPDRepository.Find(e => !e.IsDeleted && filterName.ToLower().Contains(e.RecordCode.ToLower()));
            var list_eoc = unitOfWork.EOCRepository.Find(e => !e.IsDeleted && filterName.ToLower().Contains(e.RecordCode.ToLower()));
            totalResultsCount = list_ed.Count() + list_opd.Count() + list_ipd.Count() + list_eoc.Count();

            list_result.AddRange(list_ed.Select(x => new DeleteRecordViewModels
            {
                Id = x.Id,
                RecordCode = x.RecordCode,
                CustomerInfo = $"<p>PID:{x.Customer?.PID} ({x.VisitCode}) - {x.Customer?.Fullname}</p><p>{x.Customer?.DateOfBirth?.ToString("dd/MM/yyy")} - {x.Customer?.Phone}</p>",
                Doctor = x.DischargeInformation?.UpdatedBy,
                Nurse = x.EmergencyTriageRecord?.UpdatedBy
            }).ToList());

            list_result.AddRange(list_opd.Select(x => new DeleteRecordViewModels
            {
                Id = x.Id,
                RecordCode = x.RecordCode,
                CustomerInfo = $"<p>PID:{x.Customer?.PID} ({x.VisitCode}) - {x.Customer?.Fullname}</p><p>{x.Customer?.DateOfBirth?.ToString("dd/MM/yyy")} - {x.Customer?.Phone}</p>",
                Doctor = x.PrimaryDoctor?.Username,
                Nurse = x.PrimaryNurse?.Username
            }).ToList());

            list_result.AddRange(list_ipd.Select(x => new DeleteRecordViewModels
            {
                Id = x.Id,
                RecordCode = x.RecordCode,
                CustomerInfo = $"<p>PID:{x.Customer?.PID} ({x.VisitCode}) - {x.Customer?.Fullname}</p><p>{x.Customer?.DateOfBirth?.ToString("dd/MM/yyy")} - {x.Customer?.Phone}</p>",
                Doctor = x.PrimaryDoctor?.Username,
                Nurse = x.PrimaryNurse?.Username
            }).ToList());

            list_result.AddRange(list_eoc.Select(x => new DeleteRecordViewModels
            {
                Id = x.Id,
                RecordCode = x.RecordCode,
                CustomerInfo = $"<p>PID:{x.Customer?.PID} ({x.VisitCode}) - {x.Customer?.Fullname}</p><p>{x.Customer?.DateOfBirth?.ToString("dd/MM/yyy")} - {x.Customer?.Phone}</p>",
                Doctor = x.PrimaryDoctor?.Username,
                Nurse = x.PrimaryNurse?.Username
            }).ToList());

            return Json(new
            {
                queryModel.draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = totalResultsCount,
                data = list_result
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRecordItem(Guid id, string content)
        {
            dynamic data = null;
            string message = string.Empty;
            try
            {
                dynamic visit = null;
                var ed = unitOfWork.EDRepository.GetById(id);
                if (ed != null)
                {
                    visit = ed;
                    unitOfWork.EDRepository.Delete(ed);
                    unitOfWork.Commit();
                }

                var opd = unitOfWork.OPDRepository.GetById(id);
                if (opd != null)
                {
                    if (opd.TransferFromId != null)
                    {
                        DeleteHandOverCheckList(opd.TransferFromId.Value);
                        DeleteEDHandOverCheckList(opd.TransferFromId.Value);
                    }
                    visit = opd;
                    unitOfWork.OPDRepository.Delete(opd);
                    unitOfWork.Commit();
                }

                var ipd = unitOfWork.IPDRepository.GetById(id);
                if (ipd != null)
                {
                    visit = ipd;
                    unitOfWork.IPDRepository.Delete(ipd);
                    unitOfWork.Commit();
                }

                var eoc = unitOfWork.EOCRepository.GetById(id);
                if (eoc != null)
                {
                    visit = eoc;
                    unitOfWork.EOCRepository.Delete(eoc);
                    unitOfWork.Commit();
                }

                if (visit != null)
                {
                    Customer customer = visit.Customer;
                    ExecuteLog(visit, customer, content);
                    RemoveComplexSummary(customer.Id, id);
                    HandleUpdateChronic(customer);
                    unitOfWork.Commit();
                    data = new
                    {
                        IsDeleted = true,
                        Message = $"Hồ sơ: {visit.RecordCode} xóa thành công"
                    };
                    return Json(data);
                }
                data = new
                {
                    IsDeleted = false,
                    Message = "Không tìm thấy hồ sơ muốn xóa"
                };
                return Json(data);
            }
            catch (Exception)
            {
                data = new
                {
                    IsDeleted = false,
                    Message = "Có lỗi trong quá trình xử lý"
                };
                return Json(data);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRecordAll(string[] ids, string content)
        {
            dynamic data = null;
            string message = string.Empty;
            try
            {
                foreach (var str_id in ids)
                {
                    Guid _id = new Guid(str_id);
                    dynamic visit = null;
                    var ed = unitOfWork.EDRepository.GetById(_id);
                    if (ed != null)
                    {
                        message = message + $"Hồ sơ: {ed.RecordCode} xóa thành công \n ";
                        visit = ed;
                        unitOfWork.EDRepository.Delete(ed);
                        unitOfWork.Commit();
                    }

                    var opd = unitOfWork.OPDRepository.GetById(_id);
                    if (opd != null)
                    {
                        message = message + $"Hồ sơ: {opd.RecordCode} xóa thành công \n ";
                        visit = opd;
                        unitOfWork.OPDRepository.Delete(opd);
                        unitOfWork.Commit();
                    }

                    var ipd = unitOfWork.IPDRepository.GetById(_id);
                    if (ipd != null)
                    {
                        message = message + $"Hồ sơ: {ipd.RecordCode} xóa thành công \n ";
                        visit = ipd;
                        unitOfWork.IPDRepository.Delete(ipd);
                        unitOfWork.Commit();
                    }

                    var eoc = unitOfWork.EOCRepository.GetById(_id);
                    if (eoc != null)
                    {
                        message = message + $"Hồ sơ: {eoc.RecordCode} xóa thành công \n ";
                        visit = eoc;
                        unitOfWork.EOCRepository.Delete(eoc);
                        unitOfWork.Commit();
                    }

                    if (visit != null)
                    {
                        Customer customer = visit.Customer;
                        ExecuteLog(visit, customer, content);
                        RemoveComplexSummary(customer.Id, _id);
                        HandleUpdateChronic(customer);
                        unitOfWork.Commit();
                    }
                }
                data = new
                {
                    IsDeleted = true,
                    Message = message
                };
                return Json(data);
            }
            catch (Exception)
            {
                data = new
                {
                    IsDeleted = false,
                    Message = "Có lỗi trong quá trình xử lý"
                };
                return Json(data);
            }
        }
        private void ExecuteLog(dynamic visit, Customer customer, string content)
        {
            string action = $"Delete HS: {visit.RecordCode.ToString().ToUpper()}";
            string customerInfo = $"PID :{customer?.PID} ({visit.VisitCode}), Name : {customer?.Fullname}, Birthday : {customer?.DateOfBirth?.ToString("dd/MM/yyyy")}, Phone : {customer?.Phone}";
            var fullUrl = this.Url.Action("GetListDeleteRecord", "DeleteRecord", new { id = visit.Id, content = content }, this.Request.Url.Scheme);
            LogHelper.WriteLogStatus(visit.Id, action, content, fullUrl, customerInfo);
        }
        private void RemoveComplexSummary(Guid customer_id, Guid visit_id)
        {
            var comp = unitOfWork.ComplexOutpatientCaseSummaryRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.CustomerId != null &&
                e.CustomerId == customer_id &&
                e.VisitId != null &&
                e.VisitId == visit_id
            );
            if (comp != null)
                unitOfWork.ComplexOutpatientCaseSummaryRepository.Delete(comp);
        }
        private void HandleUpdateChronic(Customer customer)
        {
            var list_icd = GetICD(customer.Id);
            var is_chronic = IsChronic(list_icd);
            if (customer.IsChronic != is_chronic)
            {
                customer.IsChronic = is_chronic;
                unitOfWork.CustomerRepository.Update(customer);
                unitOfWork.Commit();
            }
        }
        private bool IsChronic(List<string> list_icd)
        {
            if (list_icd.Count() < 2)
                return false;
            var icd_chronic = unitOfWork.ICD10Repository.Find(
                e => !e.IsDeleted &&
                e.IsChronic &&
                list_icd.Contains(e.Code)
                && e.ICDSpecialtyId != null
            ).Select(e => e.ICDSpecialtyId).Distinct();
            if (icd_chronic.Count() > 1)
                return true;
            return false;
        }
        private List<string> GetICD(Guid customer_id)
        {
            var list_icd = new List<string>();
            var dies = unitOfWork.EDRepository.Find(
                e => !e.IsDeleted &&
                e.CustomerId != null &&
                e.CustomerId == customer_id
            ).Select(e => e.DischargeInformation).ToList();
            foreach (var di in dies)
            {
                var icd_raw = di.DischargeInformationDatas.Where(
                    e => !e.IsDeleted &&
                    !string.IsNullOrEmpty(e.Code) &&
                    "DI0DIAICD,DI0DIAOPT".Contains(e.Code)
                ).Select(e => e.Value);
                foreach (var i in icd_raw)
                {
                    var icd = ICDConvert(i).Select(e => e.Code).ToList();
                    list_icd.AddRange(icd);
                }
            }


            var oens = unitOfWork.OPDRepository.Find(
                e => !e.IsDeleted &&
                e.CustomerId != null &&
                e.CustomerId == customer_id
            ).Select(e => e.OPDOutpatientExaminationNote).ToList();
            foreach (var oen in oens)
            {
                var icd_raw = oen.OPDOutpatientExaminationNoteDatas.Where(
                    e => !e.IsDeleted &&
                    !string.IsNullOrEmpty(e.Code) &&
                    "OPDOENICDANS,OPDOENICDOPT".Contains(e.Code)
                ).Select(e => e.Value);
                foreach (var i in icd_raw)
                {
                    var icd = ICDConvert(i).Select(e => e.Code).ToList();
                    list_icd.AddRange(icd);
                }
            }

            return list_icd.Distinct().ToList();
        }
        private List<ICDModel> ICDConvert(string raw)
        {
            try
            {
                var json = JArray.Parse(raw);
                return json.Select(e => new ICDModel
                {
                    Code = e.Value<string>("code"),
                    Label = e.Value<string>("label"),
                }).ToList();
            }
            catch (Exception)
            {
                return new List<ICDModel>();
            }
        }
        private void DeleteHandOverCheckList(Guid id)
        {
            var hovcl = unitOfWork.OPDPreAnesthesiaHandOverCheckListRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == id);
            if (hovcl != null)
            {
                hovcl.IsAcceptNurse = false;
                hovcl.IsAcceptPhysician = false;
                hovcl.ReceivingNurseId = null;
                hovcl.ReceivingPhysicianId = null;
                unitOfWork.OPDPreAnesthesiaHandOverCheckListRepository.Update(hovcl);
                unitOfWork.Commit();
            }
        }
        private void DeleteEDHandOverCheckList(Guid id)
        {
            var checkEDHandOverCheckList = unitOfWork.HandOverCheckListRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == id);
            if (checkEDHandOverCheckList != null)
            {
                checkEDHandOverCheckList.IsAcceptNurse = false;
                checkEDHandOverCheckList.IsAcceptPhysician = false;
                checkEDHandOverCheckList.ReceivingNurseId = null;
                checkEDHandOverCheckList.ReceivingPhysicianId = null;
                unitOfWork.HandOverCheckListRepository.Update(checkEDHandOverCheckList);
                unitOfWork.Commit();
            }
        }
    }
}