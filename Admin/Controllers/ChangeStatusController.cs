using Admin.Common.Model;
using Admin.Models;
using DataAccess.Models;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Admin.Controllers
{
    public class ChangeStatusController : Controller
    {
        protected IUnitOfWork unitOfWork = new EfUnitOfWork();

        public ActionResult Index()
        {
            var model = new ChangeStatusViewModel();
            return View(model);
        }

        public ActionResult GetListChangeStatus(DataTablesQueryModel queryModel)
        {
            int totalResultsCount = 0;
            var list_result = new List<ChangeStatusViewModel>();

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
            var ed_status = unitOfWork.EDStatusRepository.Find(
                e => !e.IsDeleted && 
                e.VisitTypeGroup.Code == "ED"
            ).Select(e => new { e.Id, e.ViName });

            var list_opd = unitOfWork.OPDRepository.Find(e => !e.IsDeleted && filterName.ToLower().Contains(e.RecordCode.ToLower()));
            var opd_status = unitOfWork.EDStatusRepository.Find(
                e => !e.IsDeleted &&
                e.VisitTypeGroup.Code == "OPD"
            ).Select(e => new { e.Id, e.ViName });

            var list_ipd = unitOfWork.IPDRepository.Find(e => !e.IsDeleted && filterName.ToLower().Contains(e.RecordCode.ToLower()));
            var ipd_status = unitOfWork.EDStatusRepository.Find(
                e => !e.IsDeleted &&
                e.VisitTypeGroup.Code == "IPD"
            ).Select(e => new { e.Id, e.ViName });

            totalResultsCount = list_ed.Count() + list_opd.Count() + list_ipd.Count();

            list_result.AddRange(list_ed.Select(x => new ChangeStatusViewModel
            {
                Id = x.Id,
                RecordCode = x.RecordCode,
                CustomerInfo = $"<p>PID:{x.Customer?.PID} ({x.VisitCode}) - {x.Customer?.Fullname}</p><p>{x.Customer?.DateOfBirth?.ToString("dd/MM/yyy")} - {x.Customer?.Phone}</p>",
                Doctor = x.DischargeInformation?.UpdatedBy,
                Nurse = x.EmergencyTriageRecord?.UpdatedBy,
                StatusId = x.EDStatusId,
                ListStatus = ed_status,
            }).ToList());

            list_result.AddRange(list_opd.Select(x => new ChangeStatusViewModel
            {
                Id = x.Id,
                RecordCode = x.RecordCode,
                CustomerInfo = $"<p>PID:{x.Customer?.PID} ({x.VisitCode}) - {x.Customer?.Fullname}</p><p>{x.Customer?.DateOfBirth?.ToString("dd/MM/yyy")} - {x.Customer?.Phone}</p>",
                Doctor = x.PrimaryDoctor?.Username,
                Nurse = x.PrimaryNurse?.Username,
                StatusId = x.EDStatusId,
                ListStatus = opd_status,
            }).ToList());

            list_result.AddRange(list_ipd.Select(x => new ChangeStatusViewModel
            {
                Id = x.Id,
                RecordCode = x.RecordCode,
                CustomerInfo = $"<p>PID:{x.Customer?.PID} ({x.VisitCode}) - {x.Customer?.Fullname}</p><p>{x.Customer?.DateOfBirth?.ToString("dd/MM/yyy")} - {x.Customer?.Phone}</p>",
                Doctor = x.PrimaryDoctor?.Username,
                Nurse = x.PrimaryNurse?.Username,
                StatusId = x.EDStatusId,
                ListStatus = ipd_status,
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
        public ActionResult UpdateChangeStatus(Guid visit_id, Guid status_id)
        {
            try
            {
                dynamic visit = unitOfWork.EDRepository.GetById(visit_id);
                if (visit != null)
                {
                    ChangeLogStatus(visit_id, visit.EDStatusId, status_id);
                    visit.EDStatusId = status_id;
                    unitOfWork.EDRepository.Update(visit);
                    unitOfWork.Commit();
                    return Json(true);
                }

                visit = unitOfWork.OPDRepository.GetById(visit_id);
                if (visit != null)
                {
                    ChangeLogStatus(visit_id, visit.EDStatusId, status_id);
                    visit.EDStatusId = status_id;
                    unitOfWork.OPDRepository.Update(visit);
                    unitOfWork.Commit();
                    return Json(true);
                }

                visit = unitOfWork.IPDRepository.GetById(visit_id);
                if (visit != null)
                {
                    ChangeLogStatus(visit_id, visit.EDStatusId, status_id);
                    visit.EDStatusId = status_id;
                    unitOfWork.IPDRepository.Update(visit);
                    unitOfWork.Commit();
                    return Json(true);
                }
                return Json(false);
            }
            catch (Exception)
            {
                return Json(false);
            }
        }
        protected string GetUser()
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                var username = identity?.Name;
                return username;
            }
            catch (Exception)
            {
                return "";
            }
        }
        public void ChangeLogStatus (Guid visit_id, Guid from_status_id, Guid status_id)
        {
            var user = GetUser();
            using (IUnitOfWork unitOfWork = new EfUnitOfWork())
            {
                Log log = new Log
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Now,
                    CreatedBy = user,
                    Action = "CHANGE_STATUS_BY_ADMIN",
                    URI = "CHANGE_STATUS_BY_ADMIN",
                    Name = visit_id.ToString(),
                    Reason = $"CHANGE_STATUS_BY_ADMIN: VisitId {visit_id.ToString()} from (statusID {from_status_id.ToString()}) to (statusID {status_id.ToString()})",
                };
                unitOfWork.LogRepository.Add(log);
                unitOfWork.Commit();
            }
        }
    }
}