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
    public class EOCJointConsultationGroupMinutesController : EIOJointConsultationGroupMinutesController
    {
        [HttpGet]
        [Route("api/EOC/JointConsultationGroupMinutes/List/{id}")]
        [Permission(Code = "EOC033")]
        public IHttpActionResult GetListProcedureSummaryAPI(Guid id)
        {
            var visit = GetEOC(id);
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
        [Route("api/EOC/JointConsultationGroupMinutes/{id}")]
        [Permission(Code = "EOC033")]
        public IHttpActionResult GetJointConsultationGroupMinutesAPI(Guid id)
        {
            var jscm = GetEIOJointConsultationGroupMinutes(id);
            if (jscm == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_JSCM_NOT_FOUND);
            var visit = GetVisit((Guid)jscm.VisitId, "EOC");
            return Content(HttpStatusCode.OK, GetOrUpdateNewestEIOJointConsultationGroupMinutesData(jscm, visit));
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/EOC/JointConsultationGroupMinutes/Create/{id}")]
        [Permission(Code = "EOC034")]
        public IHttpActionResult CreateJointConsultationGroupMinutesAPI(Guid id)
        {
            var visit = GetEOC(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);

            CreateEIOJointConsultationGroupMinutes(visit.Id, "EOC", visit.SpecialtyId);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/EOC/JointConsultationGroupMinutes/{id}")]
        [Permission(Code = "EOC035")]
        public IHttpActionResult UpdateJointConsultationGroupMinutesAPI(Guid id, [FromBody]JObject request)
        {
            var jscm = GetEIOJointConsultationGroupMinutes(id);

            if (jscm == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_JSCM_NOT_FOUND);

            if (jscm.ChairmanConfirm || jscm.SecretaryConfirm || jscm.MemberConfirm)
                return Content(HttpStatusCode.Forbidden, Message.OWNER_FORBIDDEN);

            var user = GetUser();

            HandleJointConsultationGroupMinutesData(jscm, request["Datas"]);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/EOC/JointConsultationGroupMinutes/Confirm/{id}")]
        [Permission(Code = "EOC036")]
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
    }
}
