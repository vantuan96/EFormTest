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
    public class OPDJointConsultationGroupMinutesController : EIOJointConsultationGroupMinutesController
    {
        [HttpGet]
        [Route("api/OPD/JointConsultationGroupMinutes/List/{id}")]
        [Permission(Code = "OJCGM2")]
        public IHttpActionResult GetListProcedureSummaryAPI(Guid id)
        {
            var visit = GetOPD(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);
            var procedures = GetListJointConsultationGroupMinutes(id);
            return Content(HttpStatusCode.OK, procedures.Select(e => new {
                e.CreatedBy,
                e.CreatedAt,
                e.Id
            }).ToList());
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/JointConsultationGroupMinutes/Create/{id}")]
        [Permission(Code = "OJCGM1")]
        public IHttpActionResult CreateJointConsultationGroupMinutesAPI(Guid id)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);
            CreateEIOJointConsultationGroupMinutes(opd.Id, "OPD", opd.SpecialtyId);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpGet]
        [Route("api/OPD/JointConsultationGroupMinutes/{id}")]
        [Permission(Code = "OJCGM2")]
        public IHttpActionResult GetJointConsultationGroupMinutesAPI(Guid id)
        {
            var jscm = GetEIOJointConsultationGroupMinutes(id);
            if (jscm == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_JSCM_NOT_FOUND);           
            var visit = GetVisit((Guid)jscm.VisitId, "OPD");
            return Content(HttpStatusCode.OK, GetOrUpdateNewestEIOJointConsultationGroupMinutesData(jscm, visit));
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/JointConsultationGroupMinutes/{id}")]
        [Permission(Code = "OJCGM3")]
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
        [Route("api/OPD/JointConsultationGroupMinutes/Confirm/{id}")]
        [Permission(Code = "OJCGM4")]
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
        [HttpGet]
        [Route("api/OPD/JointConsultationGroupMinutes/CheckFormLocked/{id}")]
        [Permission(Code = "OJCGM2")]
        public IHttpActionResult CheckFormLockedAPI(Guid id)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND); 
            var user = GetUser();
            return Content(HttpStatusCode.OK, new
            {
                IsLocked24h = Is24hLocked(opd.CreatedAt, opd.Id, "OPDJCFM", user.Username)
            });
        }
    }
}
