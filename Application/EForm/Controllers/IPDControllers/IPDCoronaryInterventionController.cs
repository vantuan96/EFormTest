using DataAccess.Models;
using DataAccess.Models.IPDModel;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Common;
using EForm.Models;
using EForm.Models.IPDModels;
using EForm.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web.Http;

namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class IPDCoronaryInterventionController : BaseApiController
    {
        private readonly string visit_type = "IPD";
        private const string timeUpdate_version2 = "UPDATE_VERSION2_A01_076_290422_VE";

        [HttpGet]
        [Route("api/IPD/CoronaryIntervention/Info/{type}/{ipdId}")]
        [Permission(Code = "DTCTDMV1")]
        public IHttpActionResult GetInfoForCoronaryIntervention(string type, Guid ipdId)
        {
            IPD visit = GetVisit(ipdId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);
            var medicalRecordPart2s = visit.IPDMedicalRecord?.IPDMedicalRecordPart2?.IPDMedicalRecordPart2Datas;
            var medicalRecordPart3s = visit.IPDMedicalRecord?.IPDMedicalRecordPart3?.IPDMedicalRecordPart3Datas;
            var ipdInitialAssessmentForAdultDatas = visit.IPDInitialAssessmentForAdult?.IPDInitialAssessmentForAdultDatas;
            return Content(HttpStatusCode.OK, new
            {
                VitalSigns = new
                {
                    BP = ipdInitialAssessmentForAdultDatas?.FirstOrDefault(x=>x.Code== "IPDIAAUBLPRANS")?.Value,
                    Pulse = ipdInitialAssessmentForAdultDatas?.FirstOrDefault(x => x.Code == "IPDIAAUPULSANS")?.Value,
                    RR = ipdInitialAssessmentForAdultDatas?.FirstOrDefault(x => x.Code == "IPDIAAURERAANS")?.Value
                },
                IsLocked = IPDIsBlock(visit, type)
            });;
        }

        [HttpGet]
        [Route("api/IPD/CoronaryIntervention/{type}/{ipdId}/{id}")]
        [Permission(Code = "DTCTDMV1")]
        public IHttpActionResult GetCoronaryIntervention(string type, Guid ipdId, Guid id)
        {
            var visit = GetVisit(ipdId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);
            var form = unitOfWork.IPDCoronaryInterventionRepository.Find(e => !e.IsDeleted && e.Id == id).FirstOrDefault();
            if (form == null)
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND_WITH_LOCKED);            
            return Content(HttpStatusCode.OK, FormatOutput(type, visit, form));
        }

        [HttpGet]
        [Route("api/IPD/CoronaryIntervention/{type}/{ipdId}")]
        [Permission(Code = "DTCTDMV1")]
        public IHttpActionResult GetCoronaryInterventions(string type, Guid ipdId)
        {
            var visit = GetVisit(ipdId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);
            var forms = unitOfWork.IPDCoronaryInterventionRepository.Find(e => !e.IsDeleted && e.VisitId == ipdId).OrderBy(o => o.Order).ToList().Select(form => new
            {
                form.Id,
                form.VisitId,
                form.CreatedBy,
                CreatedAt = form.CreatedAt,
                UpdatedAt = form.UpdatedAt,
                Order = form.Order,
                form.Version,
                form.UpdatedBy
            }).ToList();
            bool IsLocked = IPDIsBlock(visit, type);                      
            return Content(HttpStatusCode.OK, new
            {
                Datas = forms,
                IsLocked,
                Version = IsVisitLastTimeUpdate(visit, timeUpdate_version2) == true ? "2" : "1"
            });
        }

        [HttpPost]
        [Route("api/IPD/CoronaryIntervention/Create/{type}/{ipdId}")]
        [Permission(Code = "DTCTDMV2")]
        public IHttpActionResult CreateCoronaryIntervention(string type, Guid ipdId)
        {
            var visit = GetVisit(ipdId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);
            var form_data = new IPDCoronaryIntervention()
            {
                VisitId = ipdId,
                Version = 3  
            };
            unitOfWork.IPDCoronaryInterventionRepository.Add(form_data);
            UpdateVisit(visit, visit_type);
            var idForm = form_data.Id;
            CreateOrUpdateIPDIPDInitialAssessmentToByFormType(visit, type, idForm);
            return Content(HttpStatusCode.OK, new
            {
                form_data.Id,
                form_data.VisitId,
                form_data.CreatedBy,
                form_data.CreatedAt
            });
        }

        [HttpPost]
        [Route("api/IPD/CoronaryIntervention/Update/{type}/{ipdId}/{id}")]
        [Permission(Code = "DTCTDMV")]
        public IHttpActionResult UpdateCoronaryIntervention(string type, Guid ipdId, Guid id, [FromBody] JObject request)
        {
            var visit = GetVisit(ipdId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);
            bool IsLocked = IPDIsBlock(visit, type, id);
            if (IsLocked)
                return Content(HttpStatusCode.BadRequest, Message.FORM_IS_LOCKED);
            var form = GetForm(id);
            if (form == null)
                return Content(HttpStatusCode.NotFound, NotFoundData(visit, type));            
             var user = GetUser();
             if ((user == null || user.Username != form.CreatedBy) && !IsCheckConfirm(id))
                 return Content(HttpStatusCode.Forbidden, Message.OWNER_FORBIDDEN);             
            
            HandleUpdateOrCreateFormDatas((Guid)form.VisitId, form.Id, type, request["Datas"]);
            unitOfWork.IPDCoronaryInterventionRepository.Update(form);
            UpdateVisit(visit, visit_type);
            var formId = form.Id;
            CreateOrUpdateIPDIPDInitialAssessmentToByFormType(visit, type, formId);
            return Content(HttpStatusCode.OK, new
            {
                form.Id,
                form.VisitId,
                form.UpdatedAt
            });
        }
        [HttpPost]
        [Route("api/IPD/CoronaryIntervention/Confirm/{type}/{ipdId}/{id}")]
        [Permission(Code = "DTCTDMV3")]
        public IHttpActionResult ConfirmAPI(string type, Guid ipdId, Guid id, [FromBody] JObject request)
        {
            var visit = GetVisit(ipdId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);
            bool IsLocked = IPDIsBlock(visit, type, id);
            if (IsLocked)
                return Content(HttpStatusCode.BadRequest, Message.FORM_IS_LOCKED);
            var form = GetForm(id);
            if (form == null)
                return Content(HttpStatusCode.NotFound, NotFoundData(visit, type));           
            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);
            var successConfirm = ConfirmUser(form, user, request["TypeConfirm"].ToString());
            if (successConfirm)
            {
                UpdateVisit(visit, visit_type);
                return Content(HttpStatusCode.OK, Message.SUCCESS);
            }
            else
            {
                return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);
            }
        }
        private IPDCoronaryIntervention GetForm(Guid id)
        {
            return unitOfWork.IPDCoronaryInterventionRepository.Find(e => !e.IsDeleted && e.Id == id).FirstOrDefault();
        }
        private dynamic FormatOutput(string type, IPD ipd, IPDCoronaryIntervention fprm)
        {
            var datas = GetFormData((Guid)fprm.VisitId, fprm.Id, type);
            var doctor = unitOfWork.UserRepository.FirstOrDefault(u => !u.IsDeleted && u.Id == fprm.DoctorConfirmId);
            return new
            {
                Id = fprm.Id,
                VisitId = fprm.VisitId,
                Datas = datas,
                CreatedBy = fprm.CreatedBy,
                CreatedAt = fprm.CreatedAt,
                UpdateBy = fprm.UpdatedBy,
                UpdatedAt = fprm.UpdatedAt,
                IsLocked = IPDIsBlock(ipd, type, fprm.Id),
                DoctorConfirm = new
                {
                    UserName = doctor?.Username,
                    FullName = doctor?.Fullname,
                },
                DoctorConfirmAt = fprm.DoctorConfirmAt,
                fprm.Version
            };
        }
        private bool ConfirmUser(IPDCoronaryIntervention ipdCoronaryIntervention, User user, string kind)
        {
            var positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName.ToUpper());            
            if (kind.ToUpper() == "DOCTOR" && positions.Contains("DOCTOR") && ipdCoronaryIntervention.DoctorConfirmId == null)
            {
                ipdCoronaryIntervention.DoctorConfirmAt = DateTime.Now;
                ipdCoronaryIntervention.DoctorConfirmId = user?.Id;
            }
            else
            {
                return false;
            }
            unitOfWork.IPDCoronaryInterventionRepository.Update(ipdCoronaryIntervention);
            unitOfWork.Commit();
            return true;
        }
    }
}