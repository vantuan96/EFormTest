using DataAccess.Models.EIOModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers.BaseEIOControllers;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.OPDControllers
{
    [SessionAuthorize]
    public class OPDAssessmentForRetailServicePatientController : EIOAssessmentForRetailServicePatientController
    {
        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/OPDAssessmentForRetailServicePatient/Confirm/{id}")]
        [Permission(Code = "OAFRS1")]
        public IHttpActionResult CreateOPDAssessmentForRetailServicePatientAPI(Guid id)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var user = GetUser();
            if (CheckIsBlock(opd))
                return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);

            var afrsp = opd.EIOAssessmentForRetailServicePatient;
            if (afrsp != null && !afrsp.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.EIO_AFRS_EXIST);

            CreateAssessmentForRetailServicePatient(opd, "OPD");

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpGet]
        [Route("api/OPD/OPDAssessmentForRetailServicePatient/{id}")]
        [Permission(Code = "OAFRS2")]
        public IHttpActionResult GetOPDAssessmentForRetailServicePatientAPI(Guid id)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);
            var IsLocked = CheckIsBlock(opd);
            var afrsp = opd.EIOAssessmentForRetailServicePatient;
            if (afrsp == null || afrsp.IsDeleted)
                return Content(HttpStatusCode.NotFound, new
                {
                    ViMessage = "Đánh giá NB dịch vụ lẻ không tồn tại",
                    EnMessage = "Assessment for retail service patient is not found",
                    IsLocked
                });

            return Content(HttpStatusCode.OK, GetDetailAssessmentForRetailServicePatient(opd, afrsp, IsLocked));
        }

        [HttpGet]
        [Route("api/OPD/OPDAssessmentForRetailServicePatient/Sync/{id}")]
        [Permission(Code = "OAFRS3")]
        public IHttpActionResult SyncOPDAssessmentForRetailServicePatientAPI(Guid id)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);
            if (opd.AdmittedDate == null)
                return Content(HttpStatusCode.NotFound, Message.SYNC_24H_NOT_FOUND);


            var lastest_visit_in_24h = GetLastestRetailVisit24H("OPD", opd.CustomerId, opd.Id, opd.AdmittedDate);
            if (lastest_visit_in_24h == null)
                return Content(HttpStatusCode.NotFound, Message.SYNC_24H_NOT_FOUND);

            EIOAssessmentForRetailServicePatient afrsp = lastest_visit_in_24h.EIOAssessmentForRetailServicePatient;
            if (afrsp == null || afrsp.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.SYNC_24H_NOT_FOUND);

            return Content(HttpStatusCode.OK, new
            {
                afrsp.Version,
                Datas = afrsp.EIOAssessmentForRetailServicePatientDatas.Where(e => !e.IsDeleted)
                .Select(e => new { e.Id, e.Code, e.Value })
                .ToList(),
                IsLocked = CheckIsBlock(opd)
            });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/OPDAssessmentForRetailServicePatient/{id}")]
        [Permission(Code = "OAFRS4")]
        public IHttpActionResult UpdateOPDAssessmentForRetailServicePatientAPI(Guid id, [FromBody]JObject request)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var afrs = opd.EIOAssessmentForRetailServicePatient;
            if (afrs == null || afrs.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.EIO_AFRS_NOT_FOUND);

            var user = GetUser();
            if (CheckIsBlock(opd))
                return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);

            UpdateVisit("OPD", opd, afrs, request);

            HandleUpdateOrCreateAssessmentForRetailServicePatientData(opd, "OPD", afrs, request["Datas"]);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }
        private bool CheckIsBlock(dynamic visit, Guid? formId = null)
        {
            var user = GetUser();
            var is_block_after_24h = IsBlockAfter24h(visit.CreatedAt, formId);
            var has_unlock_permission = HasUnlockPermission(visit.Id, "OPDAFRS", user.Username, formId);
            if (!has_unlock_permission && is_block_after_24h)
                return true;
            return false;
        }
    }
}