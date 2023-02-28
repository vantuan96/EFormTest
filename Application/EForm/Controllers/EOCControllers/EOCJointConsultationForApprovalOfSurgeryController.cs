using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers.BaseEIOControllers;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.EOCControllers
{
    [SessionAuthorize]
    public class EOCJointConsultationForApprovalOfSurgeryController : EIOJointConsultationForApprovalOfSurgeryController
    {
        [HttpGet]
        [Route("api/EOC/JointConsultationForApprovalOfSurgery/{id}")]
        [Permission(Code = "EOC029")]
        public IHttpActionResult GetJointConsultationForApprovalOfSurgeryAPI(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var consultation = GetJointConsultationForApprovalOfSurgery(id, "EOC");
            if (consultation == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_JCFAOS_NOT_FOUND);

            dynamic datas;
            if (consultation.CMOId == null && consultation.HeadOfDept == null && consultation.AnesthetistId == null && consultation.SurgeonId == null)
                datas = GetOrUpdateNewestDataJointConsultation(consultation, ed, "EOC");
            else
                datas = consultation.EIOJointConsultationForApprovalOfSurgeryDatas.Where(e => !e.IsDeleted).Select(e => new
                {
                    e.Id,
                    e.Code,
                    e.Value,
                    e.EnValue,
                });

            var cmo = consultation.CMO;
            var head_of_deft = consultation.HeadOfDept;
            var anesthetist = consultation.Anesthetist;
            var surgeon = consultation.Surgeon;
            var response = new
            {
                consultation.Id,
                CMO = new { cmo?.Username, cmo?.Fullname, cmo?.DisplayName },
                HeadOfDept = new { head_of_deft?.Username, head_of_deft?.Fullname, head_of_deft?.DisplayName },
                Anesthetist = new { anesthetist?.Username, anesthetist?.Fullname, anesthetist?.DisplayName },
                Surgeon = new { surgeon?.Username, surgeon?.Fullname, surgeon?.DisplayName },
                Datas = datas,
                IsNew = consultation.CreatedAt?.ToString(Constant.TIME_DATE_FORMAT) == consultation.UpdatedAt?.ToString(Constant.TIME_DATE_FORMAT)
            };
            return Content(HttpStatusCode.OK, response);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/EOC/JointConsultationForApprovalOfSurgery/Create/{id}")]
        [Permission(Code = "EOC030")]
        public IHttpActionResult CreateEDJointConsultationForApprovalOfSurgeryAPI(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var consultation = GetJointConsultationForApprovalOfSurgery(id, "EOC");
            if (consultation != null)
                return Content(HttpStatusCode.NotFound, Message.EIO_JCFAOS_EXIST);

            CreateJointConsultationForApprovalOfSurgery(id, "EOC");
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        
        [HttpPost]
        [CSRFCheck]
        [Route("api/EOC/JointConsultationForApprovalOfSurgery/{id}")]
        [Permission(Code = "EOC031")]
        public IHttpActionResult UpdateJointConsultationForApprovalOfSurgery(Guid id, [FromBody]JObject request)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var consultation = GetJointConsultationForApprovalOfSurgery(id, "EOC");
            if (consultation == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_JCFAOS_NOT_FOUND);

            HandleUpdateOrCreateJointConsultationData(consultation, request["Datas"]);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/EOC/JointConsultationForApprovalOfSurgery/Sync")]
        [Permission(Code = "EOC029")]
        public IHttpActionResult SyncJointConsultationReadOnlyResultOfParaclinicalTestsAPI([FromBody]JObject request)
        {
            var id = new Guid(request["Id"]?.ToString());
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);
            else if (ed.VisitCode == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_CODE_IS_MISSING);

            var customer = ed.Customer;
            if (customer == null || customer.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.CUSTOMER_NOT_FOUND);
            else if (customer.PID == null)
                return Content(HttpStatusCode.NotFound, Message.PID_IS_MISSING);

            var site_code = GetSiteCode();
            if (site_code == null)
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);

            return Content(HttpStatusCode.OK, SyncReadOnlyResultOfParaclinicalTests(site_code, customer.PID, ed.VisitCode));
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/EOC/JointConsultationForApprovalOfSurgery/Accept/{id}")]
        [Permission(Code = "EOC032")]
        public IHttpActionResult AcceptJointConsultationForApprovalOfSurgeryAPI(Guid id, [FromBody]JObject request)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var consultation = GetJointConsultationForApprovalOfSurgery(id, "EOC");
            if (consultation == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_JCFAOS_NOT_FOUND);

            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var kind = request["kind"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null || string.IsNullOrEmpty(kind))
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);

            var success = AcceptJointConsultation(user, consultation, kind);
            if(success)
                return Content(HttpStatusCode.OK, Message.SUCCESS);
            return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);
        }
    }
}
