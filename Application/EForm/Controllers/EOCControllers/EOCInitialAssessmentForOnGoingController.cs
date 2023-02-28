using DataAccess.Models.EOCModel;
using DataAccess.Models.OPDModel;
using DataAccess.Repository;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;
namespace EForm.Controllers.EOCControllers
{
    [SessionAuthorize]
    public class EOCInitialAssessmentForOnGoingController : BaseEOCApiController
    {
        private readonly string formCode = "OPDIAFOGOP";

        [HttpGet]
        [Route("api/eoc/InitialAssessment/ForOnGoing/{id}")]
        [Permission(Code = "EOC021")]
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
        [Route("api/eoc/InitialAssessment/ForOnGoing/Create/{id}")]
        [Permission(Code = "EOC023")]
        public IHttpActionResult Post(Guid id)
        {
            var visit = GetEOC(id);
            if (visit == null) return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);
            var form = GetForm(id);
            if (form != null) return Content(HttpStatusCode.BadRequest, Message.FORM_EXIST);

            var form_data = new EOCInitialAssessmentForOnGoing
            {
                VisitId = id
            };
            unitOfWork.EOCInitialAssessmentForOnGoingRepository.Add(form_data);
            UpdateVisit(visit);
            unitOfWork.Commit();
            return Content(HttpStatusCode.OK, new { form_data.Id });
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/eoc/InitialAssessment/ForOnGoing/Update/{id}")]
        [Permission(Code = "EOC023")]
        public IHttpActionResult Update(Guid id, [FromBody]JObject request)
        {
            var visit = GetEOC(id);
            if (visit == null) return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);
            var form = GetForm(id);
            if (form == null) return Content(HttpStatusCode.BadRequest, Message.FORM_NOT_FOUND);

            var user = GetUser();
            if (user.Username != form.CreatedBy) return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);
            
            HandleUpdateOrCreateFormDatas(id, form.Id, formCode, request["Datas"], visit);
            unitOfWork.EOCInitialAssessmentForOnGoingRepository.Update(form);
            SetPrimaryDoctorAndAdmittedDate(visit, request);
            UpdateVisit(visit);
            unitOfWork.Commit();

            return Content(HttpStatusCode.OK, new { form.Id });
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/eoc/InitialAssessment/ForOnGoing/Sync/{id}")]
        [Permission(Code = "EOC021")]
        public IHttpActionResult SyncInitialAssessmentsForOnGoingAPI(Guid id)
        {
            var opd = GetEOC(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);
            if (opd.AdmittedDate == null)
                return Content(HttpStatusCode.NotFound, Message.SYNC_24H_NOT_FOUND);

            var lastest_opd = GetLastestOPDIn24H(opd.CustomerId, opd.AdmittedDate);
            if (lastest_opd == null)
                return Content(HttpStatusCode.NotFound, Message.SYNC_24H_NOT_FOUND);

            var iafog = lastest_opd.OPDInitialAssessmentForOnGoing;
            if (iafog == null || iafog.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.SYNC_24H_NOT_FOUND);

            var clinic = lastest_opd.Clinic;
            return Content(HttpStatusCode.OK, new
            {
                iafog.Id,
                Version = opd.Version >= 7 ? opd.Version : iafog.Version,
                ClinicId = clinic?.Id,
                Clinic = new { clinic?.ViName, clinic?.EnName, clinic?.Code },
                AdmittedDate = iafog.AdmittedDate?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                OPDId = lastest_opd.Id,
                Datas = iafog.OPDInitialAssessmentForOnGoingDatas.Where(i => !i.IsDeleted).Select(e => new { e.Id, e.Code, e.Value }).ToList(),
                Orders = "",
                IsNew = IsNew(iafog.CreatedAt, iafog.UpdatedAt),
            });
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
        private EOCInitialAssessmentForOnGoing GetForm(Guid VisitId)
        {
            var form = unitOfWork.EOCInitialAssessmentForOnGoingRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == VisitId);
            return form;
        }
        private dynamic FormatOutput(EOC visit, EOCInitialAssessmentForOnGoing fprm, Guid VisitId)
        {
            var IsNew = fprm.CreatedAt == fprm.UpdatedAt;
            return new
            {
                fprm.Id,
                IsNew = IsNew,
                Datas = GetFormData(VisitId, fprm.Id, formCode),
                fprm.CreatedBy,
                VisitId,
                AdmittedDate = visit.AdmittedDate.ToString(Constant.TIME_DATE_FORMAT),
                PrimaryDoctor = new
                {
                    visit.PrimaryDoctorId,
                    visit.PrimaryDoctor?.Username,
                    Id = visit.PrimaryDoctorId
                },
                IsShowSyncButton = IsNew && IsShowSyncButton(visit.CustomerId, visit.CreatedAt),
                hasOEN = GetOutpatientExaminationNote(VisitId) != null,
                visit.Version
            };
        }
        private bool IsShowSyncButton(Guid? customer_id, DateTime? opd_admitted_date)
        {
            var opd = GetLastestOPDIn24H(customer_id, opd_admitted_date);
            return opd != null;
        }
    }
}