using DataAccess.Models.EIOModel;
using DataAccess.Models.IPDModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers.BaseEIOControllers;
using EForm.Models.EIOModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Http;

namespace EForm.Controllers.EDControllers
{
    [SessionAuthorize]
    public class IPDJointConsultationForApprovalOfSurgeryController : EIOJointConsultationForApprovalOfSurgeryController
    {
        [HttpGet]
        [Route("api/IPD/JointConsultationForApprovalOfSurgery/{type}/{visitId}")]
        [Permission(Code = "IJCFA2")]
        public IHttpActionResult GetJointConsultationForApprovalOfSurgeryAPI(string type, Guid visitId)
        {
            var ipd = GetIPD(visitId);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            var consultationList = unitOfWork.EIOJointConsultationForApprovalOfSurgeryRepository.Find(e => !e.IsDeleted && e.VisitId == visitId && e.VisitTypeGroupCode == "IPD").OrderBy(o => o.CreatedAt).ToList().Select(consultation => new
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
                IsLocked = IPDIsBlock(ipd, Constant.IPDFormCode.BienBanHoiChanThongQuaMo)
            });
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/JointConsultationForApprovalOfSurgery/Create/{type}/{visitId}")]
        [Permission(Code = "IJCFA1")]
        public IHttpActionResult CreateEDJointConsultationForApprovalOfSurgeryAPI(string type, Guid visitId)
        {
            var ipd = GetIPD(visitId);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            //var consultation = GetJointConsultationForApprovalOfSurgery(visitId, "IPD");
            //if (consultation != null)
            //    return Content(HttpStatusCode.NotFound, Message.EIO_JCFAOS_EXIST);

            var consultation = CreateJointConsultationForApprovalOfSurgery(visitId, "IPD");
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
        [Route("api/IPD/JointConsultationForApprovalOfSurgery/CheckFormLocked/{type}/{visitId}/{id}")]
        [Permission(Code = "IJCFA2")]
        public IHttpActionResult CheckFormLockedAPI(string type,Guid visitId, Guid id)
        {
            var ipd = GetIPD(visitId);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);
            return Content(HttpStatusCode.OK, new
            {
                IsLocked = IPDIsBlock(ipd, Constant.IPDFormCode.BienBanHoiChanThongQuaMo)
            });
        }
        [HttpGet]
        [Route("api/IPD/JointConsultationForApprovalOfSurgery/{type}/{visitId}/{id}")]
        [Permission(Code = "IJCFA2")]
        public IHttpActionResult GetJointConsultationForApprovalOfSurgeryAPI(string type,Guid visitId, Guid id)
        {
            var ipd = GetIPD(visitId);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var consultation = GetJointConsultationForApprovalOfSurgery(id, "IPD");
            if (consultation == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_JCFAOS_NOT_FOUND);           
            List<EIOJointConsultationForApprovalOfSurgeryResponse> datas;
            if (consultation.CMOId == null && consultation.HeadOfDept == null && consultation.AnesthetistId == null && consultation.SurgeonId == null)
                datas = GetOrUpdateNewestDataJointConsultation(consultation, ipd, "IPD");
            else
                datas = consultation.EIOJointConsultationForApprovalOfSurgeryDatas.Where(e => !e.IsDeleted).Select(e => new EIOJointConsultationForApprovalOfSurgeryResponse
                {
                    Id = e.Id,
                    Code = e.Code,
                    Value = StringContent(e, ipd),
                    EnValue = e.EnValue,
                }).ToList();
            for(int i = 0; i < datas.Count(); i++)
            {
                if (datas[i].Code == "EDJCFAOSPHHANS")
                {
                    datas[i].Value = GetPersonalHistory(ipd);
                    break;
                }
            }
            var cmo = consultation.CMO;
            var head_of_deft = consultation.HeadOfDept;
            var anesthetist = consultation.Anesthetist;
            var surgeon = consultation.Surgeon;
            var IsLocked = IPDIsBlock(ipd, Constant.IPDFormCode.BienBanHoiChanThongQuaMo, id);           

            //var MedicalCode = (from ipd_sql in unitOfWork.IPDSetupMedicalRecordRepository.AsQueryable().Where(
            //            e => e.FormType == "MedicalRecords")
            //                    select ipd_sql.Formcode).ToList();

            //var IPDMedicalRecordOfPatient = unitOfWork.IPDMedicalRecordOfPatientRepository.AsQueryable()
            //                 .Where(r => r.VisitId == id && MedicalCode.Contains(r.FormCode) && !r.IsDeleted)
            //                 .OrderByDescending(e => e.UpdatedAt)
            //                 .FirstOrDefault();
            var response = new
            {
                consultation.Id,
                CMO = new { cmo?.Username, cmo?.Fullname, cmo?.DisplayName },
                HeadOfDept = new { head_of_deft?.Username, head_of_deft?.Fullname, head_of_deft?.DisplayName },
                Anesthetist = new { anesthetist?.Username, anesthetist?.Fullname, anesthetist?.DisplayName },
                Surgeon = new { surgeon?.Username, surgeon?.Fullname, surgeon?.DisplayName },
                IsLocked,
                Datas = datas,
                VisitType = "IPD",
                IPDMedicalRecordOfPatient = GetLastIPDMedicalRecordOfPatients(visitId),
                IsNew = consultation.CreatedAt?.ToString(Constant.TIME_DATE_FORMAT) == consultation.UpdatedAt?.ToString(Constant.TIME_DATE_FORMAT),
                consultation.Version,
                consultation.CreatedBy,
                CreatedAt = consultation.CreatedAt,
                consultation.UpdatedBy,
                UpdatedAt = consultation.UpdatedAt
            };
            return Content(HttpStatusCode.OK, response);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/JointConsultationForApprovalOfSurgery/Update/{type}/{visitId}/{id}")]
        [Permission(Code = "IJCFA3")]
        public IHttpActionResult UpdateJointConsultationForApprovalOfSurgery(string type, Guid visitId, Guid id, [FromBody] JObject request)
        {
            var ipd = GetIPD(visitId);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            var consultation = GetJointConsultationForApprovalOfSurgery(id, "IPD");
            if (consultation == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_JCFAOS_NOT_FOUND);
            if (IPDIsBlock(ipd, Constant.IPDFormCode.BienBanHoiChanThongQuaMo, id))
                return Content(HttpStatusCode.Forbidden, Message.FORM_IS_LOCKED);
            if (consultation.CMOId != null || consultation.HeadOfDeptId != null || consultation.AnesthetistId != null || consultation.SurgeonId != null)
                return Content(HttpStatusCode.NotFound, Message.OWNER_FORBIDDEN);
            HandleUpdateOrCreateJointConsultationData(consultation, request["Datas"]);
            return Content(HttpStatusCode.OK, new
            {
                consultation.Id,
                consultation.VisitId,
                consultation.UpdatedAt,
                consultation.Version
            });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/JointConsultationForApprovalOfSurgery/Sync")]
        [Permission(Code = "IJCFA4")]
        public IHttpActionResult SyncJointConsultationReadOnlyResultOfParaclinicalTestsAPI([FromBody] JObject request)
        {
            var id = new Guid(request["Id"]?.ToString());
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            else if (ipd.VisitCode == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_CODE_IS_MISSING);

            var customer = ipd.Customer;
            if (customer == null || customer.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.CUSTOMER_NOT_FOUND);
            else if (customer.PID == null)
                return Content(HttpStatusCode.NotFound, Message.PID_IS_MISSING);

            var site_code = GetSiteCode();
            if (site_code == null)
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);

            return Content(HttpStatusCode.OK, SyncReadOnlyResultOfParaclinicalTests(site_code, customer.PID, ipd.VisitCode, customer.Id));
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/JointConsultationForApprovalOfSurgery/Accept/{type}/{visitId}/{id}")]
        [Permission(Code = "IJCFA5")]
        public IHttpActionResult AcceptJointConsultationForApprovalOfSurgeryAPI(string type, Guid visitId, Guid id, [FromBody] JObject request)
        {
            var ipd = GetIPD(visitId);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            if (IPDIsBlock(ipd, Constant.IPDFormCode.BienBanHoiChanThongQuaMo, id))
                return Content(HttpStatusCode.Forbidden, Message.FORM_IS_LOCKED);

            var consultation = GetJointConsultationForApprovalOfSurgery(id, "IPD");
            if (consultation == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_JCFAOS_NOT_FOUND);  
            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var kind = request["kind"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null || string.IsNullOrEmpty(kind))
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);
            var success = AcceptJointConsultation(user, consultation, kind);
            if (success)
                return Content(HttpStatusCode.OK, Message.SUCCESS);
            return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);
        }
    }
}
