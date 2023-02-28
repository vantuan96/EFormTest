using DataAccess.Models;
using DataAccess.Models.EDModel;
using DataAccess.Models.EOCModel;
using DataAccess.Models.IPDModel;
using DataAccess.Models.OPDModel;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace EForm.Controllers.GeneralControllers
{
    [SessionAuthorize]
    public class UnlockFormController: BaseApiController
    {
        [CSRFCheck]
        [HttpPost]
        [Route("api/Unlock")]
        [Permission(Code = "GUNLK1")]
        public IHttpActionResult UnlockForm([FromBody]JObject request)
        {
            var id = new Guid(request["Id"]?.ToString());
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var doctor = opd.PrimaryDoctor?.Username;
            if(string.IsNullOrEmpty(doctor))
                return Content(HttpStatusCode.BadRequest, Message.PRIMARY_DOCTOR_NOT_FOUND);

            var unlock = unitOfWork.UnlockFormToUpdateRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.RecordCode == opd.RecordCode &&
                e.Username == doctor
            );
            if(unlock == null)
            {
                unlock = new UnlockFormToUpdate()
                {
                    RecordCode = opd.RecordCode,
                    VisitId = opd.Id,
                    FormName = "Phiếu khám ngoại trú",
                    FormCode = "OPDOEN",
                    Username = doctor,
                };
                unitOfWork.UnlockFormToUpdateRepository.Add(unlock);
            }
            unlock.ExpiredAt = DateTime.Now.AddDays(1);
            unitOfWork.UnlockFormToUpdateRepository.Update(unlock);
            unitOfWork.Commit();
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }
        [CSRFCheck]
        [HttpPost]
        [Route("api/Unlock/Vip")]
        [Permission(Code = "GUNLK2")]
        public IHttpActionResult UnlockVip([FromBody]UnlockVip request)
        {
            if (string.IsNullOrEmpty(request.PID)) return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);
            var cus = GetCustomerByPid(request.PID);
            if (cus == null) return Content(HttpStatusCode.BadRequest, Message.CUSTOMER_NOT_FOUND);
            int check = 0;
            if (string.IsNullOrEmpty(request.RecodeCode))
            {
                unitOfWork.UnlockVipRepository.Add(new UnlockVip
                {
                    CustomerId = cus.Id,
                    Username = request.Username,
                    PID = request.PID,
                    Type = request.Type,
                    ExpiredAt = StringToDatetime(request.StringExpiredAt),
                    Note = request.Note,
                    Files = request.Files
                });
                check++;
                unitOfWork.Commit();
            } else
            {
                var visit_code = request.RecodeCode.Split(',');
                foreach (string code in visit_code)
                {
                    var visit = GetVisitIdByRecodeCode(code);
                    if (visit != null && visit.CustomerId == cus.Id)
                    {
                        var data = new UnlockVip
                        {
                            RecodeCode = code,
                            VisitId = visit.Id,
                            CustomerId = visit.CustomerId,
                            VisitCode = visit.VisitCode,
                            Username = request.Username,
                            PID = request.PID,
                            Type = request.Type,
                            ExpiredAt = StringToDatetime(request.StringExpiredAt),
                            Note = request.Note,
                            Files = request.Files
                        };
                        unitOfWork.UnlockVipRepository.Add(data);
                        check++;
                        unitOfWork.Commit();
                        LockEDVipMedicalRecord(visit.Id);
                        LockEOCVipMedicalRecord(visit.Id);
                        LockOPDVipMedicalRecord(visit.Id);
                        LockIPDVipMedicalRecord(visit.Id);
                        unitOfWork.Commit();
                    }
                }
                
            }
            if (check == 0) return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);
            return Content(HttpStatusCode.OK, request);
        }
        private DateTime? StringToDatetime(string examination_time)
        {
            return DateTime.ParseExact(examination_time, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
        }
        private void LockEDVipMedicalRecord(Guid id)
        {
            var visits = unitOfWork.EDRepository.Find(e => !e.IsDeleted && e.Id == id).ToList();
            foreach (ED visit in visits)
            {
                if (visit.DischargeDate != null && IsBlockAfter24hc(visit.DischargeDate))
                {
                    visit.UnlockFor = GetUnlockFor(visit.Id, visit.Customer.Id, visit.PrimaryDoctor?.Username);
                }
            }
        }
        private void LockOPDVipMedicalRecord(Guid id)
        {
            var visits = unitOfWork.OPDRepository.Find(e => !e.IsDeleted && e.Id == id).ToList();
            foreach (OPD visit in visits)
            {
                if (visit.DischargeDate != null && IsBlockAfter24hc(visit.DischargeDate))
                {
                    var list_doctor = new List<string>();
                    if (!string.IsNullOrEmpty(visit.PrimaryDoctor?.Username)) list_doctor.Add(visit.PrimaryDoctor?.Username);
                    if (!string.IsNullOrEmpty(visit.AuthorizedDoctor?.Username)) list_doctor.Add(visit.AuthorizedDoctor?.Username);
                    visit.UnlockFor = GetUnlockFor(visit.Id, visit.Customer.Id, string.Join(",", list_doctor));
                }
            }
        }
        private void LockIPDVipMedicalRecord(Guid id)
        {
            var visits = unitOfWork.IPDRepository.Find(e => !e.IsDeleted && e.Id == id).ToList();
            foreach (IPD visit in visits)
            {
                if (visit.DischargeDate != null && IsBlockAfter24hc(visit.DischargeDate))
                {
                    visit.UnlockFor = GetUnlockFor(visit.Id, visit.Customer.Id, visit.PrimaryDoctor?.Username);
                }
            }
        }
        private void LockEOCVipMedicalRecord(Guid id)
        {
            var visits = unitOfWork.EOCRepository.Find(e => !e.IsDeleted && e.Id == id).ToList();
            foreach (EOC visit in visits)
            {
                if (visit.DischargeDate != null && IsBlockAfter24hc(visit.DischargeDate))
                {
                    visit.UnlockFor = GetUnlockFor(visit.Id, visit.Customer.Id, visit.PrimaryDoctor?.Username);
                }
            }
        }
        private string GetUnlockFor(Guid visit_id, Guid custome_id, string primary_doctor = null)
        {
            var now = DateTime.Now;
            var unlockfor = unitOfWork.UnlockVipRepository.Find(e => !e.IsDeleted && e.VisitId == visit_id && e.CustomerId == custome_id && e.ExpiredAt >= now).Select(e => e.Username).ToList();
            if (primary_doctor != null)
            {
                unlockfor.Add(primary_doctor);
            }
            if (unlockfor.Count() > 0)
            {
                var usernames = string.Join(",", unlockfor);
                return usernames;
            }
            return null;
        }
        private bool IsBlockAfter24hc(DateTime? created_at)
        {
            var now = DateTime.Now;
            double timeToBlock = 1;
            return created_at?.AddDays(timeToBlock) <= now;
        }
    }
}