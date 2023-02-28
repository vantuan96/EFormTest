using DataAccess;
using DataAccess.Models;
using DataAccess.Models.GeneralModel;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Client;
using EForm.Common;
using EForm.Helper;
using EForm.Models;
using EForm.Models.DiagnosticReporting;
using EForm.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
namespace EForm.Controllers
{
    [SessionAuthorize]
    public class DiagnosticReportingController : BaseApiController
    {
        [HttpGet]
        [Route("api/DiagnosticReporting/ByPID")]
        [Permission(Code = "XEMCLSBYPID")]
        public IHttpActionResult GetListCLS([FromUri] DiagnosticReportingParameterModel param)
        {
            var pid = param.Search;
            if (string.IsNullOrEmpty(pid))
                return Content(HttpStatusCode.BadRequest, new
                {
                    ViName = "Không có PID",
                    EnName = "No PID"
                });
            var customer = GetCustomerByPid(pid);
            if (customer == null)
            {
                return Content(HttpStatusCode.BadRequest, new
                {
                    ViName = "Không có PID",
                    EnName = "No PID"
                });
            }
            
            var dt = GetAppConfig("DRS_START_TIME");
            if (string.IsNullOrEmpty(dt)) dt = "17:30 22/04/2022";
            var fn = customer.Fullname;
            var DateOfBirth = customer.DateOfBirth;
            DateTime from_date = DateTime.ParseExact(dt, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);

            var query = from charge_item_sql in unitOfWork.ChargeItemRepository.AsQueryable().Where(
                          e => !e.IsDeleted &&
                          e.Status == Constant.ChargeItemStatus.Placed &&
                          e.CustomerId == customer.Id &&
                          e.CreatedAt > from_date
                     )
                    join charge_sql in unitOfWork.ChargeRepository.AsQueryable()
                        on charge_item_sql.ChargeId equals charge_sql.Id
                    
                       join ser_sql in unitOfWork.ServiceRepository.AsQueryable()
                               on charge_item_sql.ServiceId equals ser_sql.Id
                       join charge_visit_sql in unitOfWork.ChargeVistRepository.AsQueryable()
                    on charge_sql.ChargeVisitId equals charge_visit_sql.Id
                        join dr_sql in unitOfWork.DiagnosticReportingRepository.AsQueryable()
                                  on charge_item_sql.Id equals dr_sql.ChargeItemId
                            // join uss_sql in unitOfWork.UserRepository.AsQueryable()
                            //   on charge_item_sql.CreatedBy equals uss_sql.Username
                        where ser_sql.IsDiagnosticReporting
                       select new DiagnosticReportingCharge
                       {
                           Id = dr_sql.Id,
                           PID = charge_item_sql.PatientId,
                           CreatedAt = charge_item_sql.CreatedAt,
                           CreatedBy = charge_item_sql.CreatedBy,
                           Fullname = fn,
                           ChargeItemId = charge_item_sql.Id,
                           AreaName = charge_visit_sql.AreaName,
                           ServiceCode = ser_sql.Code,
                           ServiceName = ser_sql.ViName,
                           ExamCompleted = dr_sql.ExamCompleted,
                           Status = dr_sql.Status,
                           Technique = dr_sql.Technique,
                           Dob = DateOfBirth,
                           Findings = dr_sql.Findings,
                           VisitCode = charge_item_sql.VisitCode,
                           Impression = dr_sql.Impression,
                           PickupAt = dr_sql.CreatedAt,
                           UpdatedBy = dr_sql.UpdatedBy,
                           HospitalCode = charge_sql.HospitalCode,
                           //IsReadony = (dia_sql.UpdatedBy != username || (dia_sql.ExamCompleted != null && dia_sql.ExamCompleted >= current_date))
                       }
                    //select new
                    //{
                    //    a = charge_item_sql,
                    //    b = charge_sql,
                    //    c = dr_sql
                    //}
                    ;

            

            if (!string.IsNullOrEmpty(param.StartAt))
                query = query.Where(e => e.CreatedAt >= param.ConvertedStartAt);
            if (!string.IsNullOrEmpty(param.EndAt))
            {
                DateTime? endAt = param.ConvertedEndAt;
                endAt = endAt.Value.AddSeconds(59);
                query = query.Where(e => e.CreatedAt <= endAt);
            }
            if (!string.IsNullOrEmpty(param.VisitCode))
                query = query.Where(e => e.VisitCode == param.VisitCode);

            var items = query.OrderByDescending(m => m.CreatedAt)
                   .ToListNoLock();

            return Content(HttpStatusCode.OK, new
            {
                count = items.Count(),
                items
            });
        }

        [HttpGet]
        [Route("api/DiagnosticReporting/List")]
        [Permission(Code = "DR000001")]
        public IHttpActionResult List([FromUri] DiagnosticReportingParameterModel request)
        {
            // if (string.IsNullOrWhiteSpace(request.Search)) return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);
            var search = request.Search;
            Guid? search_by_pid = new Guid();
            if (string.IsNullOrWhiteSpace(search))
            {
                search = null;
                search_by_pid = null;
            } else
            {
                var customer = GetCustomerByPid(search);
                if (customer == null)
                {
                    return Content(HttpStatusCode.OK, new
                    {
                        count = 0,
                        items = new List<DiagnosticReportingCharge>()
                    });
                }
                search_by_pid = customer.Id;
            }

            var dt = GetAppConfig("DRS_START_TIME");
            if (string.IsNullOrEmpty(dt)) dt = "17:30 22/04/2022";

            DateTime from_date = DateTime.ParseExact(dt, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            var current_site = GetSiteAPICode();





            //var search = request.Search;
            //if (string.IsNullOrWhiteSpace(search)) search = null;

            //var dt = GetAppConfig("DRS_START_TIME");
            //if (string.IsNullOrEmpty(dt)) dt = "17:30 22/04/2022";

            //DateTime from_date = DateTime.ParseExact(dt, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            //var current_site = GetSiteAPICode();
            var query = (from charge_item_sql in unitOfWork.ChargeItemRepository.AsQueryable().Where(
                            e => !e.IsDeleted &&
                            e.Status == Constant.ChargeItemStatus.Placed &&
                            e.HospitalCode == current_site &&
                            e.CreatedAt > from_date &&
                            (search_by_pid == null || e.CustomerId == search_by_pid)
                         )
                         join cus_sql in unitOfWork.CustomerRepository.AsQueryable()
                            on charge_item_sql.CustomerId equals cus_sql.Id
                         join charge_sql in unitOfWork.ChargeRepository.AsQueryable()
                         on charge_item_sql.ChargeId equals charge_sql.Id
                         join charge_visit_sql in unitOfWork.ChargeVistRepository.AsQueryable()
                            on charge_sql.ChargeVisitId equals charge_visit_sql.Id
                         join ser_sql in unitOfWork.ServiceRepository.AsQueryable()
                            on charge_item_sql.ServiceId equals ser_sql.Id
                         //join ser_gro_sql in unitOfWork.ServiceGroupRepository.AsQueryable()
                         //   on ser_sql.ServiceGroupId equals ser_gro_sql.Id
                         join dr_sql in unitOfWork.DiagnosticReportingRepository.AsQueryable()
                              on charge_item_sql.Id equals dr_sql.ChargeItemId into dr_list
                         from dr_sql in dr_list.DefaultIfEmpty()
                             // join uss_sql in unitOfWork.UserRepository.AsQueryable()
                             //   on charge_item_sql.CreatedBy equals uss_sql.Username
                         where ser_sql.IsDiagnosticReporting
                         select new DiagnosticReportingCharge
                         {
                             Id = charge_item_sql.Id,
                             SiteCode = charge_item_sql.HospitalCode,
                             PID = charge_item_sql.PatientId,
                             CreatedAt = charge_item_sql.CreatedAt,
                             CreatedBy = charge_item_sql.CreatedBy,
                             Fullname = cus_sql.Fullname,
                             ChargeItemId = charge_item_sql.Id,
                             AreaName = charge_visit_sql.AreaName,
                             ServiceCode = ser_sql.Code,
                             ServiceName = ser_sql.ViName,
                             // ServiceGroupCode = ser_gro_sql.Code,
                             HospitalCode = charge_item_sql.HospitalCode,
                             VisitCode = charge_item_sql.VisitCode,
                             Dob = cus_sql.DateOfBirth,
                             DiagnosticReportingId = dr_sql.Id,
                             Status = dr_sql.Status,
                             IsDiagnosticReporting = ser_sql.IsDiagnosticReporting,
                             Nurse = dr_sql.Nurse,
                             Area = dr_sql.Area,
                             // ChargeBy = uss_sql.Fullname
                         });

            query = query.Where(e => e.DiagnosticReportingId == null || e.Status == 0 || e.Status == null);
            //query = query.Where(e => e.IsDiagnosticReporting);

            //if (!string.IsNullOrWhiteSpace(request.Search))
            //{
            //    query = query.Where(e => e.PID == request.Search);
            //}

            if (request.StartAt != null && request.EndAt != null)
                query = query.Where(e => e.CreatedAt != null && e.CreatedAt >= request.ConvertedStartAt && e.CreatedAt <= request.ConvertedEndAt);
            else if (request.StartAt != null)
                query = query.Where(e => e.CreatedAt != null && e.CreatedAt >= request.ConvertedStartAt);
            else if (request.EndAt != null)
                query = query.Where(e => e.CreatedAt != null && e.CreatedAt <= request.ConvertedEndAt);

            if (!string.IsNullOrWhiteSpace(request.AreaName))
            {
                query = query.Where(e => e.AreaName.Contains(request.AreaName));
            }

            if (!string.IsNullOrWhiteSpace(request.User))
            {
                query = query.Where(e => ("," + request.User + ",").Contains("," + e.CreatedBy + ","));
            }
            //var service_group = GetAppConfig("SERVICE_GROUP_DIAGNOSTICREPORTING");
            //var service_code = GetAppConfig("SERVICE_CODE_DIAGNOSTICREPORTING");

            //List<string> service_groups = service_group.Split(',').ToList();
            //List<string> service_codes = service_code.Split(',').ToList();

            //query = query.Where(e => service_groups.Contains(e.ServiceGroupCode) || service_codes.Contains(e.ServiceCode) || e.ServiceGroupCode.StartsWith("FE"));
            var items = new List<DiagnosticReportingCharge>();
            
            if (string.IsNullOrEmpty(request.Type) || request.Type == "1")
            {
                items = query.OrderByDescending(m => m.CreatedAt).Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListNoLock();
                int count = query.CountNoLock();
                return Content(HttpStatusCode.OK, new
                {
                    count,
                    items
                });
            }
            return Content(HttpStatusCode.OK, new
            {
                count = 0
            });




            //var temp = (from charge_item_sql in unitOfWork.ChargeItemRepository.AsQueryable().Where(
            //                e => !e.IsDeleted &&
            //                e.Status == Constant.ChargeItemStatus.Placed &&
            //                e.HospitalCode == current_site &&
            //                e.CreatedAt > from_date
            //             )
            //             join charge_sql in unitOfWork.ChargeRepository.AsQueryable()
            //             on charge_item_sql.ChargeId equals charge_sql.Id
            //             join dr_sql in unitOfWork.DiagnosticReportingRepository.AsQueryable()
            //                  on charge_item_sql.Id equals dr_sql.ChargeItemId into dr_list
            //             where charge_item_sql.Service.IsDiagnosticReporting && (search_by_pid == null || charge_item_sql.CustomerId == search_by_pid)
            //             from dr_sql in dr_list.DefaultIfEmpty()
            //             select new
            //             {
            //                 a = charge_item_sql,
            //                 b = charge_sql,
            //                 c = dr_sql
            //             });

            //var query =(from kq in temp
            //            select new DiagnosticReportingCharge
            //            {
            //                Id = kq.a.Id,
            //                SiteCode = kq.a.HospitalCode,
            //                PID = kq.a.PatientId,
            //                CreatedAt = kq.a.CreatedAt,
            //                CreatedBy = kq.a.CreatedBy,
            //                Fullname = kq.b.ChargeVisit == null ? null : kq.b.ChargeVisit.Customer == null ? null : kq.b.ChargeVisit.Customer.Fullname,
            //                ChargeItemId = kq.a.Id,
            //                AreaName = kq.b.ChargeVisit == null ? null : kq.b.ChargeVisit.AreaName,
            //                ServiceCode = kq.a.Service == null ? null : kq.a.Service.Code,
            //                ServiceName = kq.a.Service == null ? null : kq.a.Service.ViName,
            //                // ServiceGroupCode = ser_gro_sql.Code,
            //                HospitalCode = kq.a.HospitalCode,
            //                VisitCode = kq.a.VisitCode,
            //                Dob = kq.b.ChargeVisit == null ? null : kq.b.ChargeVisit.Customer == null ? null : kq.b.ChargeVisit.Customer.DateOfBirth,
            //                DiagnosticReportingId = kq.c == null ? new Nullable<Guid>() : kq.c.Id,
            //                Status = kq.c == null ? new Nullable<int>() : kq.c.Status,
            //                IsDiagnosticReporting = kq.a.Service == null ? false : kq.a.Service.IsDiagnosticReporting,
            //                Nurse = kq.c == null ? null : kq.c.Nurse,
            //                Area = kq.c == null ? null : kq.c.Area,
            //                // ChargeBy = uss_sql.Fullname
            //            });

            //query = query.Where(e => e.DiagnosticReportingId == null || e.Status == 0 || e.Status == null);
            ////query = query.Where(e => e.IsDiagnosticReporting);

            ////if (!string.IsNullOrWhiteSpace(request.Search))
            ////{
            ////    query = query.Where(e => e.PID == request.Search);
            ////}

            //if (request.StartAt != null && request.EndAt != null)
            //    query = query.Where(e => e.CreatedAt != null && e.CreatedAt >= request.ConvertedStartAt && e.CreatedAt <= request.ConvertedEndAt);
            //else if (request.StartAt != null)
            //    query = query.Where(e => e.CreatedAt != null && e.CreatedAt >= request.ConvertedStartAt);
            //else if (request.EndAt != null)
            //    query = query.Where(e => e.CreatedAt != null && e.CreatedAt <= request.ConvertedEndAt);

            //if (!string.IsNullOrWhiteSpace(request.AreaName))
            //{
            //    query = query.Where(e => e.AreaName.Contains(request.AreaName));
            //}

            //if (!string.IsNullOrWhiteSpace(request.User))
            //{
            //    query = query.Where(e => ("," + request.User + ",").Contains("," + e.CreatedBy + ","));
            //}
            ////var service_group = GetAppConfig("SERVICE_GROUP_DIAGNOSTICREPORTING");
            ////var service_code = GetAppConfig("SERVICE_CODE_DIAGNOSTICREPORTING");

            ////List<string> service_groups = service_group.Split(',').ToList();
            ////List<string> service_codes = service_code.Split(',').ToList();

            ////query = query.Where(e => service_groups.Contains(e.ServiceGroupCode) || service_codes.Contains(e.ServiceCode) || e.ServiceGroupCode.StartsWith("FE"));
            //var items = new List<DiagnosticReportingCharge>();
            //if (string.IsNullOrEmpty(request.Type) || request.Type == "1")
            //{
            //    items = query.OrderByDescending(m => m.CreatedAt).Skip((request.PageNumber - 1) * request.PageSize)
            //        .Take(request.PageSize)
            //        .ToList();
            //}
            //int count = query.Count();
            //return Content(HttpStatusCode.OK, new
            //{
            //    count,
            //    items
            //});
        }
        [HttpPost]
        [Route("api/DiagnosticReporting/Pickup/{id}")]
        [Permission(Code = "DR000002")]
        public IHttpActionResult Pickup(Guid id)
        {
            var charge = unitOfWork.ChargeItemRepository.Find(e => !e.IsDeleted && e.Id == id).FirstOrDefault();
            if (charge == null) return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);

            var exit = unitOfWork.DiagnosticReportingRepository.Find(e => !e.IsDeleted && e.ChargeItemId == charge.Id).FirstOrDefault();

            if (exit == null)
            {
                var new_item = new DiagnosticReporting() { Status = 1, ChargeItemId = charge.Id };
                unitOfWork.DiagnosticReportingRepository.Add(new_item);
                unitOfWork.Commit();
                return Content(HttpStatusCode.OK, new { new_item.Id });
            };
            if (exit != null && exit.Status == 0)
            {
                exit.Status = 1;
                unitOfWork.Commit();
                return Content(HttpStatusCode.OK, new { exit.Id });
            }
            return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);
        }
        [HttpPost]
        [Route("api/DiagnosticReporting/Cancel/{id}")]
        [Permission(Code = "DR000011")]
        public IHttpActionResult Cancel(Guid id)
        {
            var exit = unitOfWork.DiagnosticReportingRepository.Find(e => !e.IsDeleted && e.Id == id).FirstOrDefault();
            if (exit == null) return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);
            exit.Status = 0;
            unitOfWork.Commit();
            return Content(HttpStatusCode.OK, new { });
        }
        [HttpGet]
        [Route("api/DiagnosticReporting/ByDoctor")]
        [Permission(Code = "DR000003")]
        public IHttpActionResult ByDoctor([FromUri] DiagnosticReportingParameterModel request)
        {
            var username = getUsername();
            var current_site = GetSiteAPICode();
            var current_date = DateTime.Now.AddDays(1);
            var query = (from dia_sql in unitOfWork.DiagnosticReportingRepository.AsQueryable().Where(
                            e => !e.IsDeleted && e.Status != 0
                         )
                         join charge_item_sql in unitOfWork.ChargeItemRepository.AsQueryable()
                            on dia_sql.ChargeItemId equals charge_item_sql.Id
                         join cus_sql in unitOfWork.CustomerRepository.AsQueryable()
                            on charge_item_sql.CustomerId equals cus_sql.Id
                         join charge_sql in unitOfWork.ChargeRepository.AsQueryable()
                            on charge_item_sql.ChargeId equals charge_sql.Id
                         join charge_visit_sql in unitOfWork.ChargeVistRepository.AsQueryable()
                            on charge_sql.ChargeVisitId equals charge_visit_sql.Id
                         join ser_sql in unitOfWork.ServiceRepository.AsQueryable()
                            on charge_item_sql.ServiceId equals ser_sql.Id
                         select new DiagnosticReportingCharge
                         {
                             Id = dia_sql.Id,
                             PID = charge_item_sql.PatientId,
                             CreatedAt = charge_item_sql.CreatedAt,
                             CreatedBy = charge_item_sql.CreatedBy,
                             Fullname = cus_sql.Fullname,
                             ChargeItemId = charge_item_sql.Id,
                             AreaName = charge_visit_sql.AreaName,
                             ServiceCode = ser_sql.Code,
                             ServiceName = ser_sql.ViName,
                             ExamCompleted = dia_sql.ExamCompleted,
                             Status = dia_sql.Status,
                             Technique = dia_sql.Technique,
                             Dob = cus_sql.DateOfBirth,
                             Findings = dia_sql.Findings,
                             VisitCode = charge_item_sql.VisitCode,
                             Impression = dia_sql.Impression,
                             PickupAt = dia_sql.CreatedAt,
                             UpdatedBy = dia_sql.Status == 2 ? dia_sql.UpdatedBy : "",
                             HospitalCode = charge_item_sql.HospitalCode,
                             Nurse = dia_sql.Nurse,
                             Area = dia_sql.Area,
                             IsReadony = dia_sql.Status == 2 && (dia_sql.UpdatedBy != username || (dia_sql.ExamCompleted != null && dia_sql.ExamCompleted >= current_date))
                         });
            query = query.Where(e => e.HospitalCode == current_site);

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                query = query.Where(e => e.PID == request.Search);
            }
            if (!string.IsNullOrWhiteSpace(request.Status))
            {
                List<int> status = request.Status.Split(',').Select(int.Parse).ToList();
                query = query.Where(e => status.Contains((int)e.Status));
            }
            var items = query.OrderByDescending(m => m.CreatedAt).Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListNoLock();
            int count = query.CountNoLock();
            return Content(HttpStatusCode.OK, new
            {
                count,
                items
            });
        }
        [HttpPost]
        [Route("api/DiagnosticReporting/Update/{id}")]
        [Permission(Code = "DR000004")]
        public IHttpActionResult Update(Guid id, [FromBody] DiagnosticReportingCharge request)
        {
            var exit = unitOfWork.DiagnosticReportingRepository.Find(e => !e.IsDeleted && e.Id == id).FirstOrDefault();
            if (exit == null) return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);

            exit.Status = 2;
            exit.Technique = request.Technique;
            exit.Findings = request.Findings;
            exit.Impression = request.Impression;
            exit.ExamCompleted = DateTime.ParseExact(request.ExamCompletedStr, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);

            unitOfWork.DiagnosticReportingRepository.Update(exit);
            unitOfWork.Commit();
            return Content(HttpStatusCode.OK, new { });
        }
        [HttpPost]
        [Route("api/DiagnosticReporting/AddNurse/{id}")]
        [Permission(Code = "DR000008")]
        public IHttpActionResult AddNurse(Guid id, [FromBody] DiagnosticReportingCharge request)
        {
            var exit = unitOfWork.DiagnosticReportingRepository.Find(e => !e.IsDeleted && e.ChargeItemId == id).FirstOrDefault();
            if (exit == null)
            {
                exit = new DiagnosticReporting() { Status = 0, ChargeItemId = id };
                unitOfWork.DiagnosticReportingRepository.Add(exit);
                unitOfWork.Commit();
            }
            exit.UpdatedAt = DateTime.Now;
            exit.UpdatedBy = exit.UpdatedBy;
            exit.Nurse = request.Nurse;
            exit.UpdatedAt = DateTime.Now;
            exit.Area = request.Area;
            // unitOfWork.DiagnosticReportingRepository.Update(exit);
            unitOfWork.Commit();
            return Content(HttpStatusCode.OK, new { });
        }
        [HttpGet]
        [Route("api/DiagnosticReporting/{id}")]
        [Permission(Code = "DR000005")]
        public IHttpActionResult Detail(Guid id)
        {
            var username = getUsername();
            var current_date = DateTime.Now.AddDays(-1);
            var query = (from dia_sql in unitOfWork.DiagnosticReportingRepository.AsQueryable().Where(
                            e => !e.IsDeleted && e.Id == id
                         )
                         join charge_item_sql in unitOfWork.ChargeItemRepository.AsQueryable()
                            on dia_sql.ChargeItemId equals charge_item_sql.Id
                         join cus_sql in unitOfWork.CustomerRepository.AsQueryable()
                            on charge_item_sql.CustomerId equals cus_sql.Id
                         join charge_sql in unitOfWork.ChargeRepository.AsQueryable()
                            on charge_item_sql.ChargeId equals charge_sql.Id
                         join charge_visit_sql in unitOfWork.ChargeVistRepository.AsQueryable()
                            on charge_sql.ChargeVisitId equals charge_visit_sql.Id
                         join ser_sql in unitOfWork.ServiceRepository.AsQueryable()
                            on charge_item_sql.ServiceId equals ser_sql.Id
                         join us_sql in unitOfWork.UserRepository.AsQueryable()
                             on dia_sql.UpdatedBy equals us_sql.Username
                         join uss_sql in unitOfWork.UserRepository.AsQueryable()
                            on charge_item_sql.CreatedBy equals uss_sql.Username into nlist
                         from uss_sql in nlist.DefaultIfEmpty()
                         select new DiagnosticReportingCharge
                         {
                             Id = dia_sql.Id,
                             PID = charge_item_sql.PatientId,
                             CreatedAt = charge_item_sql.CreatedAt,
                             CreatedBy = charge_item_sql.CreatedBy,
                             Fullname = cus_sql.Fullname,
                             ChargeItemId = charge_item_sql.Id,
                             AreaName = charge_visit_sql.AreaName,
                             ServiceCode = ser_sql.Code,
                             ServiceName = ser_sql.ViName,
                             ExamCompleted = dia_sql.ExamCompleted,
                             Status = dia_sql.Status,
                             Technique = dia_sql.Technique,
                             Findings = dia_sql.Findings,
                             Impression = dia_sql.Impression,
                             Dob = cus_sql.DateOfBirth,
                             Gender = cus_sql.Gender,
                             VisitGroupType = charge_item_sql.VisitGroupType,
                             VisitType = charge_item_sql.VisitType.Trim(),
                             VisitCode = charge_item_sql.VisitCode,
                             PickupAt = dia_sql.CreatedAt,
                             PickupBy = dia_sql.CreatedBy,
                             InitialDiagnosis = charge_item_sql.InitialDiagnosis,
                             UpdatedBy = dia_sql.Status == 2 ? dia_sql.UpdatedBy : "",
                             CompletedBy = us_sql.Fullname,
                             ChargeBy = uss_sql.Fullname,
                             IsReadony = (dia_sql.Status == 2 && (dia_sql.UpdatedBy != username || (dia_sql.ExamCompleted != null && dia_sql.ExamCompleted <= current_date))),
                             SiteCode = charge_item_sql.HospitalCode
                         });
            var item = query.FirstOrDefaultWithNoLock();
            if (item == null) return Content(HttpStatusCode.NotFound, Message.FORMAT_INVALID);
            item.IsReadony = item.IsReadony && !IsUnlockForm(item.Id);
            return Content(HttpStatusCode.OK, new { item });
        }
        [HttpPost]
        [Route("api/DiagnosticReporting/Unlock/{id}")]
        [Permission(Code = "DR000006")]
        public IHttpActionResult Unlock(Guid id)
        {
            var exit = unitOfWork.DiagnosticReportingRepository.Find(e => !e.IsDeleted && e.Id == id).FirstOrDefault();
            if (exit == null) return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);
            var current_date = DateTime.Now.AddDays(1);
            unitOfWork.UnlockFormToUpdateRepository.Add(new UnlockFormToUpdate()
            {
                FormCode = "DR000006",
                VisitId = exit.Id,
                Username = exit.UpdatedBy,
                ExpiredAt = current_date
            });
            unitOfWork.Commit();
            return Content(HttpStatusCode.OK, new { });
        }

        [HttpGet]
        [Route("api/DiagnosticReporting/UpdateService")]
        [Permission(Code = "DR000007")]
        public IHttpActionResult UpdateService(Guid id)
        {
            var service_group = GetAppConfig("SERVICE_GROUP_DIAGNOSTICREPORTING");
            var service_code = GetAppConfig("SERVICE_CODE_DIAGNOSTICREPORTING");

            List<string> service_groups = service_group.Split(',').ToList();
            List<string> service_codes = service_code.Split(',').ToList();

            var list_services = unitOfWork.ServiceRepository.Find(e => !e.IsDeleted && service_groups.Contains(e.ServiceGroup.Code) || service_codes.Contains(e.Code) || e.ServiceGroup.Code.StartsWith("FE")).ToList();

            foreach (var service in list_services)
            {
                service.IsDiagnosticReporting = true;
            }

            unitOfWork.Commit();
            return Content(HttpStatusCode.OK, new { count = list_services.Count() });
        }
        private bool IsUnlockForm(Guid id)
        {
            var current_date = DateTime.Now;
            return unitOfWork.UnlockFormToUpdateRepository.Find(e => !e.IsDeleted && e.VisitId == id && e.ExpiredAt >= current_date).Count() > 0;
        }
    }
}