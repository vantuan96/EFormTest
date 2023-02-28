using DataAccess.Models;
using DataAccess.Models.GeneralModel;
using DataAccess.Models.IPDModel;
using DataAccess.Models.OPDModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers;
using EMRModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.OPDControllers
{
    [SessionAuthorize]
    public class OPDRiskAssessmentForCancerController : BaseOPDApiController
    {
        private readonly string visit_type = "OPD";
        private const string formCode = "A03_115_200520_V";
        #region Extentions
        [HttpGet]
        [Route("api/OPD/RiskAssessmentForCancer/Info/{type}/{visitId}")]
        [Permission(Code = "RISKASSFORCANCERGET")]
        public IHttpActionResult GetInfo(Guid visitId, string type = "A01_201_201119_V")
        {
            OPD visit = GetVisit(visitId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);
            var etrMasterDataCode = new string[] { "OPDIAFSTOPPULANS", "OPDIAFSTOPBP0ANS", "OPDIAFSTOPTEMANS", "OPDIAFSTOPSPOANS", "OPDIAFSTOPRR0ANS", "OPDIAFSTOPHEIANS", "OPDIAFSTOPWEIANS" };

            var dataEtr = unitOfWork.OPDInitialAssessmentForShortTermDataRepository
               .Find(e => etrMasterDataCode.Contains(e.Code) && e.OPDInitialAssessmentForShortTermId == visit.OPDInitialAssessmentForShortTerm.Id)
               .Select(e => new MasterDataValue
               {
                   Code = e.Code,
                   Value = e.Value,
               }).ToList();
            var user = GetUser();
            return Content(HttpStatusCode.OK, new
            {
                VitalSigns = new
                {
                    Pulse = getValueFromMasterDatas("OPDIAFSTOPPULANS", dataEtr),
                    BP = getValueFromMasterDatas("OPDIAFSTOPBP0ANS", dataEtr),
                    Temp = getValueFromMasterDatas("OPDIAFSTOPTEMANS", dataEtr),
                    Spo2 = getValueFromMasterDatas("OPDIAFSTOPSPOANS", dataEtr),
                    RR = getValueFromMasterDatas("OPDIAFSTOPRR0ANS", dataEtr),
                    Weight = getValueFromMasterDatas("OPDIAFSTOPWEIANS", dataEtr),
                    Height = getValueFromMasterDatas("OPDIAFSTOPHEIANS", dataEtr)
                },
                SpecialtyName = visit?.Clinic?.ViName,
                Is24hLocked = Is24hLocked(visit.CreatedAt, visit.Id, type, user.Username)
        });
        }

        [HttpGet]
        [Route("api/OPD/RiskAssessmentForCancer/{type}/{visitId}")]
        [Permission(Code = "RISKASSFORCANCERGET")]
        public IHttpActionResult GetRiskAssessmentForCancerByVisitId(Guid visitId, string type = "A03_115_200520_V")
        {
            var visit = GetVisit(visitId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var form = GetForm(visitId);
            if (form == null)
                return Content(HttpStatusCode.NotFound, new
                {
                    ViMessage = "Form thông tin khách hàng đánh giá nguy cơ ung thư không tồn tại",
                    EnMessage = "Form patient information the risk assessment for cancer does not exist"
                });            
            return Content(HttpStatusCode.OK, FormatOutput(type, visit, form));
        }
        [HttpPost]
        [Route("api/OPD/RiskAssessmentForCancer/Create/{type}/{visitId}")]
        [Permission(Code = "RISKASSFORCANCERPOST")]
        public IHttpActionResult CreateMedicalRecordExtenstion(Guid visitId, string type = "A03_115_200520_V")
        {
            var visit = GetVisit(visitId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.OPD_NOT_FOUND);
            var user = GetUser();
            if (Is24hLocked(visit.CreatedAt, visitId, type, user.Username))
                return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);

            var form = GetForm(visitId);
            if (form != null)
                return Content(HttpStatusCode.BadRequest,
                    new
                    {
                        ViMessage = "Thông tin khách hàng - Đánh giá nguy cơ ung thư đã tồn tại",
                        EnMessage = "Patient information- The risk assessment for cancer already exists"
                    });

            var form_data = new OPDRiskAssessmentForCancer()
            {
                VisitId = visitId
            };
            unitOfWork.OPDRiskAssessmentForCancerRepository.Add(form_data);
            CreateOrUpdateFormForSetupOfAdmin(visitId, form_data?.Id, formCode);
            UpdateVisit(visit, visit_type);
            //var idForm = form_data.Id;
            //CreateOrUpdateIPDIPDInitialAssessmentToByFormType(visit, type, idForm);
            return Content(HttpStatusCode.OK, new
            {
                form_data.Id,
                form_data.VisitId,
                form_data.CreatedBy,
                form_data.CreatedAt
            });
        }
        [HttpPost]
        [Route("api/OPD/RiskAssessmentForCancer/Update/{type}/{visitId}")]
        [Permission(Code = "RISKASSFORCANCERPOST")]
        public IHttpActionResult UpdateRiskAssessmentForCancer(Guid visitId, [FromBody] JObject request, string type = "A03_115_200520_V")
        {
            var visit = GetVisit(visitId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.OPD_NOT_FOUND);

            var form = GetForm(visitId);
            if (form == null)
                return Content(HttpStatusCode.NotFound, NotFoundData(visit, type));
            var user = GetUser();
            
            if (Is24hLocked(visit.CreatedAt, visitId, type, user.Username, form.Id))
                return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);
            if (form.DoctorConfirmId != null)
                return Content(HttpStatusCode.NotFound, Message.OWNER_FORBIDDEN);
            HandleUpdateOrCreateTableFormData((Guid)form.VisitId, form.Id, type, request["Datas"]);
            unitOfWork.OPDRiskAssessmentForCancerRepository.Update(form);
            CreateOrUpdateFormForSetupOfAdmin(visitId, form.Id, formCode);
            UpdateVisit(visit, visit_type);
            //var formId = form.Id;
            //CreateOrUpdateIPDIPDInitialAssessmentToByFormType(visit, type, formId);
            return Content(HttpStatusCode.OK, new
            {
                form.Id,
                form.VisitId,
                form.UpdatedAt
            });
        }

        [HttpPost]
        [Route("api/OPD/RiskAssessmentForCancer/Confirm/{type}/{visitId}")]
        public IHttpActionResult ConfirmAPI(Guid visitId, [FromBody] JObject request, string type = "A03_115_200520_V")
        {
            var visit = GetVisit(visitId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.OPD_NOT_FOUND);

            var form = GetForm(visitId);
            if (form == null)
                return Content(HttpStatusCode.NotFound, new
                {
                    ViMessage = "Form thông tin khách hàng đánh giá nguy cơ ung thư không tồn tại",
                    EnMessage = "Form patient information the risk assessment for cancer does not exist"
                });            
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
        private OPDRiskAssessmentForCancer GetForm(Guid visitId)
        {
            return unitOfWork.OPDRiskAssessmentForCancerRepository.Find(e => !e.IsDeleted && e.VisitId == visitId).FirstOrDefault();
        }
        private bool ConfirmUser(OPDRiskAssessmentForCancer opdRiskAssessmentForCancer, User user, string kind)
        {
            var positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName.ToUpper());
            if (kind.ToUpper() == "DOCTOR" && positions.Contains("DOCTOR") && opdRiskAssessmentForCancer.DoctorConfirmId == null)
            {
                opdRiskAssessmentForCancer.DoctorConfirmAt = DateTime.Now;
                opdRiskAssessmentForCancer.DoctorConfirmId = user?.Id;
            }
            else
            {
                return false;
            }
            unitOfWork.OPDRiskAssessmentForCancerRepository.Update(opdRiskAssessmentForCancer);
            unitOfWork.Commit();
            return true;
        }
        private dynamic FormatOutput(string formCode, OPD opd, OPDRiskAssessmentForCancer fprm)
        {
            var DoctorConfirm = unitOfWork.UserRepository.FirstOrDefault(u => !u.IsDeleted && u.Id == fprm.DoctorConfirmId);
            var FullNameCreate = unitOfWork.UserRepository.FirstOrDefault(u => !u.IsDeleted && u.Username == fprm.CreatedBy)?.Fullname;
            var FullNameUpdate = unitOfWork.UserRepository.FirstOrDefault(u => !u.IsDeleted && u.Username == fprm.UpdatedBy)?.Fullname;
            var user = GetUser();
            return new
            {
                ID = fprm.Id,
                VisitId = fprm.VisitId,
                Datas = GetFormData((Guid)fprm.VisitId, fprm.Id, formCode),
                CreatedBy = fprm.CreatedBy,
                FullNameCreate = FullNameCreate,
                CreatedAt = fprm.CreatedAt,
                UpdateBy = fprm.UpdatedBy,
                FullNameUpdate = FullNameUpdate,
                UpdatedAt = fprm.UpdatedAt,
                Is24hLocked = Is24hLocked(opd.CreatedAt, opd.Id, formCode, user.Username),
                Confirm = new
                {
                    Doctor = new
                    {
                        UserName = DoctorConfirm?.Username,
                        FullName = DoctorConfirm?.Fullname,
                        ConfirmAt = fprm.DoctorConfirmAt,
                    }
                }
            };
        }
        protected void HandleUpdateOrCreateTableFormData(Guid VisitId, Guid FormId, string formCode, JToken request)
        {
            List<FormDatas> listInsert = new List<FormDatas>();
            List<FormDatas> listUpdate = new List<FormDatas>();
            var allergy_dct = new Dictionary<string, string>();

            var visit_type = GetCurrentVisitType();
            if (request != null)
            {
                foreach (var item in request)
                {
                    var code = item["Code"]?.ToString();
                    if (string.IsNullOrEmpty(code)) continue;
                    var value = item["Value"]?.ToString();
                    CreateOrUpdateTableFormData(VisitId, FormId, formCode, code, value, visit_type, ref listInsert, ref listUpdate);
                }
                if (listInsert.Count > 0)
                {
                    unitOfWorkDapper.FormDatasRepository.Adds(listInsert);
                }
                if (listUpdate.Count > 0)
                {
                    unitOfWorkDapper.FormDatasRepository.Updates(listUpdate);
                }
            }
        }
        protected void CreateOrUpdateTableFormData(Guid visitId, Guid formId, string formCode, string code, string value, string visit_type, ref List<FormDatas> listInsert, ref List<FormDatas> listUpdate)
        {
            var finded = unitOfWorkDapper.FormDatasRepository.FirstOrDefault(e =>
            e.IsDeleted == false &&
            e.VisitId == visitId &&
            e.FormCode == formCode &&
            e.FormId == formId &&
            e.Code == code);
            if (finded == null)
            {
                listInsert.Add(new FormDatas
                {
                    Code = code,
                    Value = value,
                    FormId = formId,
                    VisitId = visitId,
                    FormCode = formCode,
                    VisitType = visit_type,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                });
            }
            else
            {
                finded.Value = value;
                listUpdate.Add(finded);
            }
        }

        #endregion
    }
}
