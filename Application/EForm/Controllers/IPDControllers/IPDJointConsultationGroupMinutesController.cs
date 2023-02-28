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
    public class IPDJointConsultationGroupMinutesController : EIOJointConsultationGroupMinutesController
    {
        [HttpGet]
        [Route("api/IPD/JointConsultationGroupMinutes/List/{id}")]
        [Permission(Code = "IPDOB05")]
        public IHttpActionResult GetListProcedureSummaryAPI(Guid id)
        {
            var visit = GetIPD(id);
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
        [Route("api/IPD/JointConsultationGroupMinutes/CheckFormLocked/{id}")]
        [Permission(Code = "IPDOB05")]
        public IHttpActionResult CheckFormLockedAPI(Guid id)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);
            return Content(HttpStatusCode.OK, new
            {
                IsLocked = IPDIsBlock(ipd, Constant.IPDFormCode.BienBanHoiChan)
            });
        }
        [HttpGet]
        [Route("api/IPD/JointConsultationGroupMinutes/{id}")]
        [Permission(Code = "IPDOB05")]
        public IHttpActionResult GetJointConsultationGroupMinutesAPI(Guid id)
        {
            var jscm = GetEIOJointConsultationGroupMinutes(id);
            if (jscm == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_JSCM_NOT_FOUND);            
            var visit = GetVisit((Guid)jscm.VisitId, "IPD");
            return Content(HttpStatusCode.OK, GetOrUpdateNewestEIOJointConsultationGroupMinutesData(jscm, visit));
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/JointConsultationGroupMinutes/Create/{id}")]
        [Permission(Code = "IPDOB06")]
        public IHttpActionResult CreateJointConsultationGroupMinutesAPI(Guid id)
        {
            var visit = GetIPD(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);
            if (IPDIsBlock(visit, Constant.IPDFormCode.BienBanHoiChan))
                return Content(HttpStatusCode.Forbidden, Message.FORM_IS_LOCKED);

            CreateEIOJointConsultationGroupMinutes(visit.Id, "IPD", visit.SpecialtyId);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/JointConsultationGroupMinutes/{id}")]
        [Permission(Code = "IPDOB07")]
        public IHttpActionResult UpdateJointConsultationGroupMinutesAPI(Guid id, [FromBody]JObject request)
        {
            var jscm = GetEIOJointConsultationGroupMinutes(id);
            if (jscm == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_JSCM_NOT_FOUND);
            var ipd = GetIPD(jscm.VisitId.Value);
            var islock24h = IPDIsBlock(ipd, Constant.IPDFormCode.BienBanHoiChan, id);            
            if (islock24h)
                return Content(HttpStatusCode.Forbidden, Message.FORM_IS_LOCKED);
            if (jscm.ChairmanConfirm || jscm.SecretaryConfirm || jscm.MemberConfirm)
                return Content(HttpStatusCode.Forbidden, Message.OWNER_FORBIDDEN);            
            HandleJointConsultationGroupMinutesData(jscm, request["Datas"]);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/JointConsultationGroupMinutes/Confirm/{id}")]
        [Permission(Code = "IPDOB08")]
        public IHttpActionResult ConfirmJointConsultationGroupMinutesAPI(Guid id, [FromBody]JObject request)
        {
            var jscm = GetEIOJointConsultationGroupMinutes(id);
            if (jscm == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_JSCM_NOT_FOUND);
            var ipd = GetIPD(jscm.VisitId.Value);
            var islock24h = IPDIsBlock(ipd, Constant.IPDFormCode.BienBanHoiChan, id);            
            if (islock24h)
                return Content(HttpStatusCode.Forbidden, Message.FORM_IS_LOCKED);           
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
        [Route("api/IPD/JointConsultationGroupMinutes/MedicalHistory/{visit_id}")]
        [Permission(Code = "IPDOB09")]
        public IHttpActionResult GetMedicalHistory(Guid visit_id)
        {
            var ipd = GetIPD(visit_id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);            
            //var result = ipd?.IPDMedicalRecord?.IPDMedicalRecordPart2?.IPDMedicalRecordPart2Datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDMRPTBATHANS");
            return Content(HttpStatusCode.OK, new { MedicalHistory = GetPersonalHistory(ipd) }); 
        }
    }
}
