using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers.BaseEIOControllers;
using EForm.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.EDControllers
{
    [SessionAuthorize]
    public class EDJointConsultationGroupMinutesController : EIOJointConsultationGroupMinutesController
    {
        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/JointConsultationGroupMinutes/Create/{id}")]
        [Permission(Code = "EJCGM1")]
        public IHttpActionResult CreateJointConsultationGroupMinutesAPI(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);
            CreateEIOJointConsultationGroupMinutes(ed.Id, "ED", ed.SpecialtyId);
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }
        [HttpGet]
        [Route("api/ED/JointConsultationGroupMinutes/List/{id}")]
        [Permission(Code = "EJCGM2")]
        public IHttpActionResult GetListProcedureSummaryAPI(Guid id)
        {
            var visit = GetED(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);
            var procedures = GetListJointConsultationGroupMinutes(id);
            return Content(HttpStatusCode.OK, procedures.Select(e => new {
                e.CreatedBy,
                e.CreatedAt,
                e.Id
            }).ToList());
        }
        [HttpGet]
        [Route("api/ED/JointConsultationGroupMinutes/{id}")]
        [Permission(Code = "EJCGM2")]
        public IHttpActionResult GetJointConsultationGroupMinutesAPI(Guid id)
        {
            var jscm = GetEIOJointConsultationGroupMinutes(id);
            if (jscm == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_JSCM_NOT_FOUND);
            var visit = GetVisit((Guid)jscm.VisitId, "ED");
            return Content(HttpStatusCode.OK, GetOrUpdateNewestEIOJointConsultationGroupMinutesData(jscm, visit));
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/JointConsultationGroupMinutes/{id}")]
        [Permission(Code = "EJCGM3")]
        public IHttpActionResult UpdateJointConsultationGroupMinutesAPI(Guid id, [FromBody]JObject request)
        {
            var jscm = GetEIOJointConsultationGroupMinutes(id);
            if (jscm == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_JSCM_NOT_FOUND);
            if (jscm.ChairmanConfirm || jscm.SecretaryConfirm || jscm.MemberConfirm)
                return Content(HttpStatusCode.Forbidden, Message.OWNER_FORBIDDEN);
            HandleJointConsultationGroupMinutesData(jscm, request["Datas"]);
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/JointConsultationGroupMinutes/Confirm/{id}")]
        [Permission(Code = "EJCGM4")]
        public IHttpActionResult ConfirmJointConsultationGroupMinutesAPI(Guid id, [FromBody]JObject request)
        {
            var jscm = GetEIOJointConsultationGroupMinutes(id);
            if (jscm == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_JSCM_NOT_FOUND);

            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var kind = request["kind"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null || string.IsNullOrEmpty(kind))
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);

            var is_success = ConfirmJointConsultationGroupMinutes(jscm, user, kind);

            if (is_success)
                return Content(HttpStatusCode.OK, Message.SUCCESS);

            return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/JointConsultationGroupMinutes/Sync/{id}")]
        [Permission(Code = "EJCGM5")]
        public IHttpActionResult SyncJointConsultationGroupMinutesAPI(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var jscm = GetEIOJointConsultationGroupMinutes(ed.Id, "ED");
            if (jscm == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_JSCM_NOT_FOUND);

            var emer = ed.EmergencyRecord;
            var emer_record = ed.EmergencyRecord;
            var history = emer_record.EmergencyRecordDatas.FirstOrDefault(e => !e.IsDeleted && e.Code == "ER0HISANS")?.Value;
            var assessment = new EmergencyRecordAssessment(emer_record.Id).GetList();

            return Content(HttpStatusCode.OK, new {
                History = history,
                Assessment = assessment,
            });
        }


    }
}
