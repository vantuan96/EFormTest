using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers.BaseEIOControllers;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.EDControllers
{
    [SessionAuthorize]
    public class EDJointConsultationForApprovalOfSurgeryController : EIOJointConsultationForApprovalOfSurgeryController
    {
        [HttpGet]
        [Route("api/ED/JointConsultationForApprovalOfSurgery/{type}/{visitId}")]
        [Permission(Code = "EJCFA2")]
        public IHttpActionResult GetJointConsultationForApprovalOfSurgeryAPI(string type, Guid visitId)
        {
            var ed = GetED(visitId);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);
            var consultationList = unitOfWork.EIOJointConsultationForApprovalOfSurgeryRepository.Find(e => !e.IsDeleted && e.VisitId == visitId && e.VisitTypeGroupCode == "ED").OrderBy(o => o.CreatedAt).ToList().Select(consultation => new
            {
                consultation.Id,
                consultation.VisitId,
                consultation.CreatedBy,
                CreatedAt = consultation.CreatedAt,
                UpdatedAt = consultation.UpdatedAt,
                consultation.Version
            }).ToList();
            return Content(HttpStatusCode.OK, new
            {
                Datas = consultationList,
            });
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/JointConsultationForApprovalOfSurgery/Create/{type}/{visitId}")]
        [Permission(Code = "EJCFA1")]
        public IHttpActionResult CreateEDJointConsultationForApprovalOfSurgeryAPI(string type,Guid visitId)
        {
            var ed = GetED(visitId);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            //var consultation = GetJointConsultationForApprovalOfSurgery(id, "ED");
            //if (consultation != null)
            //    return Content(HttpStatusCode.NotFound, Message.EIO_JCFAOS_EXIST);

            var consultation = CreateJointConsultationForApprovalOfSurgery(visitId, "ED");
            return Content(HttpStatusCode.OK, new
            {
                consultation.Id,
                consultation.VisitId,
                consultation.CreatedBy,
                consultation.CreatedAt,
                consultation.Version
            });
        }

        [HttpGet]
        [Route("api/ED/JointConsultationForApprovalOfSurgery/{type}/{visitId}/{id}")]
        [Permission(Code = "EJCFA2")]
        public IHttpActionResult GetJointConsultationForApprovalOfSurgeryAPI(string type, Guid visitId, Guid id)
        {
            var ed = GetED(visitId);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var consultation = GetJointConsultationForApprovalOfSurgery(id, "ED");
            if(consultation == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_JCFAOS_NOT_FOUND);

            dynamic datas;
            if (consultation.CMOId == null && consultation.HeadOfDept == null && consultation.AnesthetistId == null && consultation.SurgeonId == null)
                datas = GetOrUpdateNewestDataJointConsultation(consultation, ed, "ED");
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
            var discharge_info = ed.DischargeInformation;
            var discharge_info_datas = discharge_info.DischargeInformationDatas.ToList();
            var emer_record = ed.EmergencyRecord;
            var emer_record_datas = emer_record.EmergencyRecordDatas.ToList();
            var response = new
            {
                consultation.Id,
                CMO = new { cmo?.Username, cmo?.Fullname, cmo?.DisplayName },
                HeadOfDept = new { head_of_deft?.Username, head_of_deft?.Fullname, head_of_deft?.DisplayName },
                Anesthetist = new { anesthetist?.Username, anesthetist?.Fullname, anesthetist?.DisplayName },
                Surgeon = new { surgeon?.Username, surgeon?.Fullname, surgeon?.DisplayName },
                Datas = datas,
                IsNew = consultation.CreatedAt?.ToString(Constant.TIME_DATE_FORMAT) == consultation.UpdatedAt?.ToString(Constant.TIME_DATE_FORMAT),
                consultation.Version,
                consultation.CreatedBy,
                CreatedAt = consultation.CreatedAt,
                consultation.UpdatedBy,
                UpdatedAt = consultation.UpdatedAt,
                Diagnosis = GetDiagnosisED(discharge_info_datas, emer_record_datas, ed.Version,"JOIN CONSULTATION FOR APPROVAL OF SURGERY")
            };
            return Content(HttpStatusCode.OK, response);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/JointConsultationForApprovalOfSurgery/Update/{type}/{visitId}/{id}")]
        [Permission(Code = "EJCFA3")]
        public IHttpActionResult UpdateJointConsultationForApprovalOfSurgery(string type, Guid visitId, Guid id, [FromBody]JObject request)
        {
            var ed = GetED(visitId);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var consultation = GetJointConsultationForApprovalOfSurgery(id, "ED");
            if (consultation == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_JCFAOS_NOT_FOUND);

            HandleUpdateOrCreateJointConsultationData(consultation, request["Datas"]);

            return Content(HttpStatusCode.OK, new
            {
                consultation.Id,
                consultation.VisitId,
                consultation.UpdatedAt,
                consultation.UpdatedBy,
                consultation.Version
            });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/JointConsultationForApprovalOfSurgery/Sync")]
        [Permission(Code = "EJCFA4")]
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

            return Content(HttpStatusCode.OK, SyncReadOnlyResultOfParaclinicalTests(site_code, customer.PID, ed.VisitCode, customer.Id));
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/JointConsultationForApprovalOfSurgery/Accept/{type}/{visitId}/{id}")]
        [Permission(Code = "EJCFA5")]
        public IHttpActionResult AcceptJointConsultationForApprovalOfSurgeryAPI(string type, Guid visitId, Guid id, [FromBody]JObject request)
        {
            var ed = GetED(visitId);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var consultation = GetJointConsultationForApprovalOfSurgery(id, "ED");
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
