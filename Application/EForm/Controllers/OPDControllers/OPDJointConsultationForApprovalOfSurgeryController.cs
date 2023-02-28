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
    public class OPDJointConsultationForApprovalOfSurgeryController : EIOJointConsultationForApprovalOfSurgeryController
    {
        [HttpGet]
        [Route("api/OPD/JointConsultationForApprovalOfSurgery/{type}/{visitId}")]
        [Permission(Code = "OPDJCFA2")]
        public IHttpActionResult GetJointConsultationForApprovalOfSurgeryAPI(string type, Guid visitId)
        {
            var opd = GetOPD(visitId);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);
            var consultationList = unitOfWork.EIOJointConsultationForApprovalOfSurgeryRepository.Find(e => !e.IsDeleted && e.VisitId == visitId && e.VisitTypeGroupCode == "OPD").OrderBy(o => o.CreatedAt).ToList().Select(consultation => new
            {
                consultation.Id,
                consultation.VisitId,
                consultation.CreatedBy,
                CreatedAt = consultation.CreatedAt,
                UpdatedAt = consultation.UpdatedAt,
                consultation.Version
            }).ToList();
            var user = GetUser();
            return Content(HttpStatusCode.OK, new
            {
                Datas = consultationList,
                IsLocked24h = Is24hLocked(opd.CreatedAt, visitId, type, user.Username)
            });
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/JointConsultationForApprovalOfSurgery/Create/{type}/{visitId}")]
        [Permission(Code = "OPDJCFA1")]
        public IHttpActionResult CreateEDJointConsultationForApprovalOfSurgeryAPI(string type,Guid visitId)
        {
            var opd = GetOPD(visitId);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            //var consultation = GetJointConsultationForApprovalOfSurgery(id, "ED");
            //if (consultation != null)
            //    return Content(HttpStatusCode.NotFound, Message.EIO_JCFAOS_EXIST);

            var consultation = CreateJointConsultationForApprovalOfSurgery(visitId, "OPD");
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
        [Route("api/OPD/JointConsultationForApprovalOfSurgery/{type}/{visitId}/{id}")]
        [Permission(Code = "OPDJCFA2")]
        public IHttpActionResult GetJointConsultationForApprovalOfSurgeryAPI(string type, Guid visitId, Guid id)
        {
            var opd = GetOPD(visitId);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var consultation = GetJointConsultationForApprovalOfSurgery(id, "OPD");
            if(consultation == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_JCFAOS_NOT_FOUND);

            dynamic datas;
            if (consultation.CMOId == null && consultation.HeadOfDept == null && consultation.AnesthetistId == null && consultation.SurgeonId == null)
                datas = GetOrUpdateNewestDataJointConsultation(consultation, opd, "OPD");
            else
                datas = consultation.EIOJointConsultationForApprovalOfSurgeryDatas.Where(e => !e.IsDeleted).Select(e => new
                {
                    e.Id,
                    e.Code,
                    e.Value,
                    e.EnValue,
                });

            var versionOen = opd.OPDOutpatientExaminationNote?.Version; // version phieu kham ngoai tru
            var cmo = consultation.CMO;
            var head_of_deft = consultation.HeadOfDept;
            var anesthetist = consultation.Anesthetist;
            var surgeon = consultation.Surgeon;
            var user = GetUser();
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
                IsLocked24h = Is24hLocked(opd.CreatedAt, visitId, type, user.Username),
                PkntVersion = versionOen,
                PastMedicalHistory = new
                {
                    TSSK = opd.OPDOutpatientExaminationNote.OPDOutpatientExaminationNoteDatas.FirstOrDefault(e => e.Code == "OPDOENTSSKANS")?.Value,
                    TSKN = opd.OPDOutpatientExaminationNote.OPDOutpatientExaminationNoteDatas.FirstOrDefault(e => e.Code == "OPDOENTSKNANS")?.Value,
                    TSK = opd.OPDOutpatientExaminationNote.OPDOutpatientExaminationNoteDatas.FirstOrDefault(e => e.Code == "OPDOENTSKANS")?.Value,
                }
                
            };
            return Content(HttpStatusCode.OK, response);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/JointConsultationForApprovalOfSurgery/Update/{type}/{visitId}/{id}")]
        [Permission(Code = "OPDJCFA3")]
        public IHttpActionResult UpdateJointConsultationForApprovalOfSurgery(string type, Guid visitId, Guid id, [FromBody]JObject request)
        {
            var opd = GetOPD(visitId);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var consultation = GetJointConsultationForApprovalOfSurgery(id, "OPD");
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
        [Route("api/OPD/JointConsultationForApprovalOfSurgery/Sync")]
        [Permission(Code = "OPDJCFA4")]
        public IHttpActionResult SyncJointConsultationReadOnlyResultOfParaclinicalTestsAPI([FromBody]JObject request)
        {
            var id = new Guid(request["Id"]?.ToString());
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);
            else if (opd.VisitCode == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_CODE_IS_MISSING);

            var customer = opd.Customer;
            if (customer == null || customer.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.CUSTOMER_NOT_FOUND);
            else if (customer.PID == null)
                return Content(HttpStatusCode.NotFound, Message.PID_IS_MISSING);

            var site_code = GetSiteCode();
            if (site_code == null)
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);

            return Content(HttpStatusCode.OK, SyncReadOnlyResultOfParaclinicalTests(site_code, customer.PID, opd.VisitCode, customer.Id));
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/JointConsultationForApprovalOfSurgery/Accept/{type}/{visitId}/{id}")]
        [Permission(Code = "OPDJCFA5")]
        public IHttpActionResult AcceptJointConsultationForApprovalOfSurgeryAPI(string type, Guid visitId, Guid id, [FromBody]JObject request)
        {
            var opd = GetOPD(visitId);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var consultation = GetJointConsultationForApprovalOfSurgery(id, "OPD");
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
