using DataAccess.Models.EOCModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Web.Http;
using System.Linq;
using DataAccess.Models.OPDModel;
using EForm.Client;
using DataAccess.Models;
using EMRModels;

namespace EForm.Controllers.EOCControllers
{
    [SessionAuthorize]
    public class EOCInitialAssessmentForShortTermController : BaseEOCApiController
    {
        private readonly string formCode = "OPDIAFSTOP";

        [HttpGet]
        [Route("api/eoc/InitialAssessment/ForShortTerm/{id}")]
        [Permission(Code = "EOC024")]
        public IHttpActionResult Get(Guid id)
        {
            var visit = GetEOC(id);
            if (visit == null) return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);

            var form = GetForm(id);
            if (form == null) return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);
            return Content(HttpStatusCode.OK, FormatOutput(visit, form, id));
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/eoc/InitialAssessment/ForShortTerm/Create/{id}")]
        [Permission(Code = "EOC025")]
        public IHttpActionResult Post(Guid id)
        {
            var visit = GetEOC(id);
            if (visit == null) return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);
            var form = GetForm(id);
            if (form != null) return Content(HttpStatusCode.BadRequest, Message.FORM_EXIST);

            var form_data = new EOCInitialAssessmentForShortTerm
            {
                VisitId = id
            };
            unitOfWork.EOCInitialAssessmentForShortTermRepository.Add(form_data);

            visit.PrimaryNurseId = GetUser().Id;

            UpdateVisit(visit);
            
            unitOfWork.Commit();
            return Content(HttpStatusCode.OK, new { form_data.Id });
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/eoc/InitialAssessment/ForShortTerm/Update/{id}")]
        [Permission(Code = "EOC026")]
        public IHttpActionResult Update(Guid id, [FromBody]JObject request)
        {
            var visit = GetEOC(id);
            if (visit == null) return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);
            var form = GetForm(id);
            if (form == null) return Content(HttpStatusCode.BadRequest, Message.FORM_NOT_FOUND);

            var user = GetUser();
            // if (user.Username != form.CreatedBy) return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);

            bool is_new = IsNew(form.CreatedAt, form.UpdatedAt);
            HandleUpdateOrCreateFormDatas(id, form.Id, formCode, request["Datas"], visit);

            unitOfWork.EOCInitialAssessmentForShortTermRepository.Update(form);

            SetPrimaryDoctorAndAdmittedDate(visit, request);
            if (is_new && visit.TransferFromId != null)
            {
                visit.StatusId = GetStatusIdByCode("EOCWR");
            }
            UpdateVisit(visit);
            unitOfWork.Commit();

            return Content(HttpStatusCode.OK, new { form.Id });
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/eoc/InitialAssessment/ForShortTerm/Sync/{id}")]
        [Permission(Code = "EOC024")]
        public IHttpActionResult SyncInitialAssessmentsForShortTermAPI(Guid id)
        {
            var opd = GetEOC(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);
            if (opd.AdmittedDate == null)
                return Content(HttpStatusCode.NotFound, Message.SYNC_24H_NOT_FOUND);

            var lastest_opd = GetLastestOPDIn24H(opd.CustomerId, opd.AdmittedDate);
            if (lastest_opd == null)
                return Content(HttpStatusCode.NotFound, Message.SYNC_24H_NOT_FOUND);

            var iafst = lastest_opd.OPDInitialAssessmentForShortTerm;
            if (iafst == null || iafst.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.SYNC_24H_NOT_FOUND);

            var clinic = lastest_opd.Clinic;

            return Content(HttpStatusCode.OK, new
            {
                iafst.Id,
                iafst.Version,
                ClinicId = clinic?.Id,
                Clinic = new { clinic?.ViName, clinic?.EnName, clinic?.Code },
                AdmittedDate = iafst.AdmittedDate?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                OPDId = lastest_opd.Id,
                Datas = iafst.OPDInitialAssessmentForShortTermDatas.Where(i => !i.IsDeleted).Select(etrd => new { etrd.Id, etrd.Code, etrd.Value }).ToList(),
                Orders = "",
                IsNew = IsNew(iafst.CreatedAt, iafst.UpdatedAt),
            });
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/eoc/InitialAssessment/ForShortTerm/PrimaryDoctor/{id}")]
        [Permission(Code = "EOC027")]
        public IHttpActionResult ChangePrimaryDoctor(Guid id, [FromBody]JObject request)
        {
            var visit = GetEOC(id);
            if (visit == null) return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);
            // var form = GetForm(id);
            // if (form == null) return Content(HttpStatusCode.BadRequest, Message.FORM_NOT_FOUND);
            var oen = GetOutpatientExaminationNote(id);
            
            if (!string.IsNullOrEmpty(request["Id"]?.ToString()))
            {
                var user_id = new Guid(request["Id"]?.ToString());
                var doctorInfo = GetUserInfo(user_id);
                if (oen != null && oen.CreatedBy != doctorInfo.Username)
                {
                    return Content(HttpStatusCode.BadRequest, Message.HAS_OEN_DOCTOR);
                }

                var form = GetForm(id);
                bool is_new = oen == null && visit.TransferFromId != null;
                if (is_new)
                {
                    visit.StatusId = GetStatusIdByCode("EOCWR");
                }
                visit.PrimaryDoctorId = user_id;
                UpdateVisit(visit);
                unitOfWork.Commit();
            }

            return Content(HttpStatusCode.OK, new { visit.Id });
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/eoc/InitialAssessment/delete/{id}")]
        [Permission(Code = "EOC028")]
        public IHttpActionResult Delete(Guid id, [FromBody]DeleteMedicalRecord request)
        {
            var visit = GetEOC(id);
            if (visit == null) return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);
            
            if (string.IsNullOrWhiteSpace(request.Note)) return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);
            
            var form = GetForm(id);
            if (form == null) return Content(HttpStatusCode.BadRequest, Message.FORM_NOT_FOUND);
            var user = GetUser();
            if (user.Username != form.CreatedBy) return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);

            var oen = GetOutpatientExaminationNote(id);

            if (oen == null || (oen.CreatedAt == oen.UpdatedAt))
            {
                DeleteVisit(visit);
                setLog(new Log
                {
                    Action = "DELETE EOC",
                    URI = id.ToString(),
                    Name = "DELETE EOC",
                    Request = id.ToString(),
                    Reason = request.Note,
                });
                return Content(HttpStatusCode.OK, new { visit.Id });
            }
            
            return Content(HttpStatusCode.BadRequest, Message.HAS_OEN_DOCTOR);

        }
        [HttpGet]
        [Route("api/EOC/InitialAssessment/HISDoctor/{id}")]
        [Permission(Code = "EOC027")]
        public IHttpActionResult GetHISDoctorAPI(Guid id)
        {
            var opd = GetEOC(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);
            if (string.IsNullOrEmpty(opd.VisitCode))
                return Content(HttpStatusCode.NotFound, Message.VISIT_CODE_IS_MISSING);

            var customer = opd.Customer;
            if (customer == null || customer.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.CUSTOMER_NOT_FOUND);

            var site_code = GetSiteCode();
            if (string.IsNullOrEmpty(site_code))
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);
            else if (site_code == "times_city")
                return Content(HttpStatusCode.OK, EHosClient.GetHISDoctor(customer.PID, opd.VisitCode));
            else
                return Content(HttpStatusCode.OK, OHClient.GetHISDoctor(customer.PID, opd.VisitCode));
        }
        private OPD GetLastestOPDIn24H(Guid? customer_id, DateTime? opd_admitted_date)
        {
            var time = DateTime.Now.AddDays(-1);
            
            var opd_lists = unitOfWork.OPDRepository.Find(
                    e => !e.IsDeleted &&
                    e.CustomerId != null &&
                    e.CustomerId == customer_id &&
                    e.AdmittedDate != null &&
                    e.AdmittedDate >= time &&
                    e.AdmittedDate < opd_admitted_date
                ).OrderByDescending(e => e.AdmittedDate).ToList();
            if (opd_lists.Count > 0)
                return opd_lists[0];
            return null;
        }
        private EOCInitialAssessmentForShortTerm GetForm(Guid VisitId)
        {
            var form = unitOfWork.EOCInitialAssessmentForShortTermRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == VisitId);
            return form;
        }
        private dynamic FormatOutput(EOC visit, EOCInitialAssessmentForShortTerm fprm, Guid VisitId)
        {
            var IsNew = fprm.CreatedAt == fprm.UpdatedAt;
            return new
            {
                fprm.Id,
                IsNew = IsNew,
                Datas = GetFormData(VisitId, fprm.Id, formCode),
                fprm.CreatedBy,
                AdmittedDate = visit.AdmittedDate.ToString(Constant.TIME_DATE_FORMAT),
                VisitId,
                PrimaryDoctor = new
                {
                    visit.PrimaryDoctorId,
                    visit.PrimaryDoctor?.Username,
                    Id = visit.PrimaryDoctorId
                },
                IsShowSyncButton = IsNew && IsShowSyncButton(visit.CustomerId, visit.CreatedAt),
                hasOEN = GetOutpatientExaminationNote(VisitId) != null,
                visit.Version,
                UserNameReceiving = visit.CreatedBy
            };
        }
        private bool IsShowSyncButton(Guid? customer_id, DateTime? opd_admitted_date)
        {
            var opd = GetLastestOPDIn24H(customer_id, opd_admitted_date);
            return opd != null;
        }
    }
}