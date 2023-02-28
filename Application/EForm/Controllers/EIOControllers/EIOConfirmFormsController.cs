using DataAccess.Model.BaseModel;
using DataAccess.Models;
using DataAccess.Models.EIOModel;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Client;
using EForm.Common;
using EForm.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EForm.Controllers.EIOControllers
{
    [SessionAuthorize]
    public class EIOConfirmFormsController : BaseApiController
    {
        [HttpGet]
        [Route("api/EIO/ConfirmForms/GetListIDConfirm/{visitId}/{formCode}")]
        IHttpActionResult GetIdConfirms(Guid visitId, string formCode)
        {
            var result = (from form in unitOfWork.EIOFormRepository.AsQueryable()
                         .Where(e => !e.IsDeleted && e.VisitId == visitId && e.FormCode == formCode)
                          join confirm in unitOfWork.EIOFormConfirmRepository.AsQueryable()
                          .Where(e => !e.IsDeleted)
                          on form.Id equals confirm.FormId
                          select new
                          {
                              confirm.FormId,
                              confirm.Note,
                              confirm.ConfirmAt,
                              confirm.ConfirmBy,
                              confirm.ConfirmType
                          }).ToArray();

            return Content(HttpStatusCode.OK, new { Confirm = result });
        }

        [HttpGet]
        [Route("api/EIO/ConfirmForms/GetList/{formId}")]
        public IHttpActionResult GetConfirmFormByFormId(Guid formId)
        {
            var confirm = (from c in unitOfWork.EIOFormConfirmRepository.AsQueryable()
                           where !c.IsDeleted && c.FormId == formId
                           join use in unitOfWork.UserRepository.AsQueryable()
                           .Where(m => !m.IsDeleted)
                           on c.ConfirmBy equals use.Username
                           select new EIOFormConfirmModel()
                           {
                               Id = c.Id,
                               ConfirmAt = c.ConfirmAt,
                               ConfirmBy = c.ConfirmBy,
                               Fullname = use.Fullname,
                               Title = use.Title,
                               Department = use.Department,
                               ConfirmType = c.ConfirmType,
                               FormId = c.FormId
                           }).ToList();

            return Content(HttpStatusCode.OK, confirm);
        }
        [HttpGet]
        [Route("api/EIO/ConfirmForms/GetDetail/{formId}/{*kind}")]
        public IHttpActionResult GetConfirmDetailByFormId(Guid formId, string kind)
        {
            return Content(HttpStatusCode.OK, GetEIOFormConfirmByFormId(formId, kind));
        }
        [HttpPost]
        [Route("api/EIO/ConfirmForms/Created/{visitId}/{formId}/{formCode}")]
        public IHttpActionResult CreatedConfirmByFormId(Guid visitId, Guid formId, string formCode, [FromBody] JObject request)
        {
            string visitTypeCode = GetCurrentVisitType();
            var visit = GetVisit(visitId, visitTypeCode);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);

            string req_userName = request["Username"]?.ToString();
            string req_passWord = request["Password"]?.ToString();

            if (string.IsNullOrWhiteSpace(req_userName) || string.IsNullOrWhiteSpace(req_userName)) return Content(HttpStatusCode.BadGateway, Message.FORMAT_INVALID);

            string req_kind = request["Kind"]?.ToString();
            string note = request["Note"]?.ToString();
            string formcodeunlockform = request["formcodeunlockform"]?.ToString();
            var user = GetAcceptUser(req_userName, req_passWord);
            if (user == null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);

            if (req_kind.ToUpper().Contains("USERCREATED"))
                if (!Confirm(visitId, formId, formCode, user.Username, note))
                    return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);

            if (req_kind.ToUpper().Contains("USERRECIVED"))
            {
                switch (formCode)
                {
                    case "OPD_A02_007_080121_VE":
                        if (!ConfirmSuccess(visit, user.Username))
                            return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);
                        break;
                    case "EOC_A02_007_080121_VE":
                        if (!ConfirmSuccess(visit, user.Username))
                            return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);
                        break;
                    default:
                        return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);
                }
            }

            if (!CheckAcctionByFormCode(visitId, formId, user, formCode))
                return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);
            bool success = false;
            if (!string.IsNullOrEmpty(formcodeunlockform))
            {
                success = CreateConfirmUserCreatedFormV2(visit, formId, visitTypeCode, formCode, req_kind, user.Username, note, formcodeunlockform);
            }
            else
            {
                success = CreateConfirmUserCreatedForm(visit, formId, visitTypeCode, formCode, req_kind, user.Username, note);
            }

            if (!success)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);
            //Lưu last update cho các phiếu
            LogLastUpdate(user,formId, formCode);
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        private bool CreateConfirmUserCreatedForm(dynamic visit, Guid formId, string visitTypeCode, string formCode, string kind, string userName, string note)
        {

            var eio_form = unitOfWork.EIOFormRepository.FirstOrDefault(e => e.Id == formId);
            if (eio_form == null)
            {
                eio_form = new EIOForm()
                {
                    Id = formId,
                    VisitTypeGroupCode = visitTypeCode,
                    VisitId = visit?.Id,
                    FormCode = formCode,
                    Version = visit?.Version,
                };
                unitOfWork.EIOFormRepository.Add(eio_form);
                unitOfWork.Commit();
            }
            var confirm = unitOfWork.EIOFormConfirmRepository
                          .FirstOrDefault(e => !e.IsDeleted && e.FormId == eio_form.Id
                                                && e.ConfirmType != null && e.ConfirmType.ToUpper() == kind.ToUpper()
                                                && e.Note == note
                                             );

            if (confirm != null)
                return false;

            EIOFormConfirm new_confirm = new EIOFormConfirm()
            {
                FormId = eio_form.Id,
                ConfirmType = kind.ToUpper(),
                ConfirmAt = DateTime.Now,
                ConfirmBy = userName,
                Note = note,

            };
            unitOfWork.EIOFormConfirmRepository.Add(new_confirm);
            unitOfWork.Commit();
            return true;
        }
        private bool CreateConfirmUserCreatedFormV2(dynamic visit, Guid formId, string visitTypeCode, string formCode, string kind, string userName, string note, string formcodeunlockform = null)
        {

            var eio_form = unitOfWork.EIOFormRepository.FirstOrDefault(e => e.Id == formId);
            if (eio_form == null)
            {
                eio_form = new EIOForm()
                {
                    Id = formId,
                    VisitTypeGroupCode = visitTypeCode,
                    VisitId = visit?.Id,
                    FormCode = formCode,
                    Version = visit?.Version,
                };
                unitOfWork.EIOFormRepository.Add(eio_form);
                unitOfWork.Commit();
            }
            var confirm = unitOfWork.EIOFormConfirmRepository
                          .FirstOrDefault(e => !e.IsDeleted && e.FormId == eio_form.Id
                                                && e.ConfirmType != null && e.ConfirmType.ToUpper() == kind.ToUpper()
                                                && e.Note == note
                                             );

            if (confirm != null)
                return false;

            EIOFormConfirm new_confirm = new EIOFormConfirm()
            {
                FormId = eio_form.Id,
                ConfirmType = kind.ToUpper(),
                ConfirmAt = DateTime.Now,
                ConfirmBy = userName,
                Note = formcodeunlockform,

            };
            unitOfWork.EIOFormConfirmRepository.Add(new_confirm);
            unitOfWork.Commit();
            return true;
        }

        private Guid String2Guid(string input)
        {
            Guid gui;
            bool success = Guid.TryParse(input, out gui);
            return gui;
        }

        private bool ConfirmSuccess(dynamic form, string userName, bool isUpdatedBy = false)
        {
            if (form == null)
                return false;

            if (isUpdatedBy)
                return form.UpdatedBy?.ToLower() == userName.ToLower();

            if (form.CreatedBy?.ToLower() == userName.ToLower())
                return true;
            return false;
        }
        private void LogLastUpdate(User user,Guid id , string formCode)
        {
            if(formCode == "A03_045_290422_VE")//phiếu GDSK
            {
                var form = unitOfWork.PatientAndFamilyEducationContentRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == id);
                form.UpdatedAt = DateTime.Now;
                form.UpdatedBy = user.Username;
                form.UpdatedInfo = user.FirstName + " (" + user.Username + ")";
                unitOfWork.PatientAndFamilyEducationContentRepository.Update(form);
                var form_parent = unitOfWork.PatientAndFamilyEducationRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == form.PatientAndFamilyEducationId);
                form_parent.UpdatedAt = DateTime.Now;
                form_parent.UpdatedBy = user.Username;
                unitOfWork.PatientAndFamilyEducationRepository.Update(form_parent);
                unitOfWork.Commit();
            }
        }

        private bool Confirm(Guid visitId, Guid formId, string formCode, string userName, string note)
        {
            Guid id_object = String2Guid(note);
            dynamic form = null;
            bool success = false;
            switch (formCode)
            {
                case "A03_027_080322_V":
                    form = unitOfWork.EIOBloodProductDataRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == formId);
                    success = ConfirmSuccess(form, userName);
                    if (success)
                    {
                        unitOfWork.EIOBloodProductDataRepository.Update(form);
                        unitOfWork.Commit();
                    }
                    return success;
                case "A03_038_080322_V":
                    form = unitOfWork.EDArterialBloodGasTestRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == formId);
                    success = ConfirmSuccess(form, userName);
                    if (success)
                    {
                        unitOfWork.EDArterialBloodGasTestRepository.Update(form);
                        unitOfWork.Commit();
                    }
                    return success;
                case "A03_039_080322_V":
                    form = unitOfWork.EDChemicalBiologyTestRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == formId);
                    success = ConfirmSuccess(form, userName);
                    if (success)
                    {
                        unitOfWork.EDChemicalBiologyTestRepository.Update(form);
                        unitOfWork.Commit();
                    }
                    return success;
                case "OPDIAFOGOP":
                    form = unitOfWork.OPDInitialAssessmentForOnGoingRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == formId);
                    success = ConfirmSuccess(form, userName, isUpdatedBy: true);
                    if (success)
                    {
                        RemoveUnlock24h(visitId, form.Id);
                        unitOfWork.OPDInitialAssessmentForOnGoingRepository.Update(form);
                        unitOfWork.Commit();
                    }
                    return success;
                case "A03_029_050919_VE":
                    form = unitOfWork.OrderRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == formId);
                    success = ConfirmSuccess(form, userName);
                    if (success)
                    {
                        unitOfWork.OrderRepository.Update(form);
                        unitOfWork.Commit();
                    }
                    return success;
                case "A02_066_050919_VE":
                    form = unitOfWork.IPDGlamorganRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == formId);
                    success = ConfirmSuccess(form, userName);
                    if (success)
                    {
                        unitOfWork.IPDGlamorganRepository.Update(form);
                        unitOfWork.Commit();
                    }
                    return success;
                case "A02_069_080121_VE":
                    form = unitOfWork.IPDMedicalRecordOfPatientRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == formId);
                    success = ConfirmSuccess(form, userName);
                    if (success)
                    {
                        unitOfWork.IPDMedicalRecordOfPatientRepository.Update(form);
                        unitOfWork.Commit();
                    }
                    return success;
                case "A02_014_220321_VE":
                    form = unitOfWork.IPDMedicalRecordOfPatientRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == formId);
                    success = ConfirmSuccess(form, userName);
                    if (success)
                    {
                        unitOfWork.IPDMedicalRecordOfPatientRepository.Update(form);
                        unitOfWork.Commit();
                    }
                    return success;
                case "A02_015_220321_VE":
                    form = unitOfWork.IPDMedicalRecordOfPatientRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == formId);
                    success = ConfirmSuccess(form, userName);
                    if (success)
                    {
                        unitOfWork.IPDMedicalRecordOfPatientRepository.Update(form);
                        unitOfWork.Commit();
                    }
                    return success;
                case "A02_008_080121_VE":
                    form = unitOfWork.EOCInitialAssessmentForOnGoingRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == formId);
                    success = ConfirmSuccess(form, userName);
                    if (success)
                    {
                        RemoveUnlock24h(visitId, form.Id);
                        unitOfWork.EOCInitialAssessmentForOnGoingRepository.Update(form);
                        unitOfWork.Commit();
                    }
                    return success;
                case "OPDIAFTP":
                    form = unitOfWork.OPDInitialAssessmentForTelehealthRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == formId);
                    success = ConfirmSuccess(form, userName);
                    if (success)
                    {
                        RemoveUnlock24h(visitId, form.Id);
                        unitOfWork.OPDInitialAssessmentForTelehealthRepository.Update(form);
                        unitOfWork.Commit();
                    }
                    return success;
                case "A01_049_050919_VE":
                    bool isconfirm = false;
                    form = unitOfWork.IPDThrombosisRiskFactorAssessmentRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == formId);
                    isconfirm = ConfirmSuccess(form, userName);
                    if (isconfirm)
                    {
                        unitOfWork.IPDThrombosisRiskFactorAssessmentRepository.Update(form);
                        unitOfWork.Commit();
                    }
                    return isconfirm;
                case "A01_165_050919_VE":
                    bool isconfirm2 = false;
                    form = unitOfWork.IPDThrombosisRiskFactorAssessmentRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == formId);
                    isconfirm2 = ConfirmSuccess(form, userName);
                    if (isconfirm2)
                    {
                        unitOfWork.IPDThrombosisRiskFactorAssessmentRepository.Update(form);
                        unitOfWork.Commit();
                    }
                    return isconfirm2;
                case "ED_A02_007_220321_VE":
                    form = unitOfWork.EDFallRickScreenningRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == formId);
                    success = ConfirmSuccess(form, userName);
                    if (success)
                    {
                        unitOfWork.EDFallRickScreenningRepository.Update(form);
                        unitOfWork.Commit();
                    }
                    return success;
                case "OPD_A02_007_220321_VE":
                    form = unitOfWork.OPDFallRiskScreeningRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == formId);
                    success = ConfirmSuccess(form, userName);
                    if (success)
                    {
                        unitOfWork.OPDFallRiskScreeningRepository.Update(form);
                        unitOfWork.Commit();
                    }
                    return success;
                case "EOC_A02_007_220321_VE":
                    form = unitOfWork.EOCFallRiskScreeningRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == formId);
                    success = ConfirmSuccess(form, userName);
                    if (success)
                    {
                        unitOfWork.EOCFallRiskScreeningRepository.Update(form);
                        unitOfWork.Commit();
                    }
                    return success;
                case "OPD_A02_007_080121_VE":
                    form = unitOfWork.OPDInitialAssessmentForShortTermRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == formId);
                    success = ConfirmSuccess(form, userName);
                    if (success)
                    {
                        RemoveUnlock24h(visitId, form.Id);
                        unitOfWork.OPDInitialAssessmentForShortTermRepository.Update(form);
                        unitOfWork.Commit();
                    }
                    return success;
                case "EOC_A02_007_080121_VE":
                    form = unitOfWork.EOCInitialAssessmentForShortTermRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == formId);
                    success = ConfirmSuccess(form, userName);
                    if (success)
                    {
                        RemoveUnlock24h(visitId, form.Id);
                        unitOfWork.EOCInitialAssessmentForShortTermRepository.Update(form);
                        unitOfWork.Commit();
                    }
                    return success;
                case "A03_006_050919_VE":
                    form = unitOfWork.EDAmbulanceTransferPatientsRecordRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == formId);
                    success = ConfirmSuccess(form, userName);
                    if (success)
                    {
                        unitOfWork.EDAmbulanceTransferPatientsRecordRepository.Update(form);
                        unitOfWork.Commit();
                    }
                    return success;
                case "A01_066_050919_VE": // phiếu điều trị ED
                    form = unitOfWork.EIOPhysicianNoteRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == formId);
                    success = ConfirmSuccess(form, userName);
                    if (success)
                    {
                        unitOfWork.EIOPhysicianNoteRepository.Update(form);
                        unitOfWork.Commit();
                    }
                    return success;
                case "IPDTT": //phiếu điều trị IPD
                    form = unitOfWork.EIOPhysicianNoteRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == formId);
                    success = ConfirmSuccess(form, userName);
                    if (success)
                    {
                        unitOfWork.EIOPhysicianNoteRepository.Update(form);
                        unitOfWork.Commit();
                    }
                    return success;
                case "A02_062_050919_V": // phiếu chăm sóc ED
                    form = unitOfWork.EIOCareNoteRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == formId);
                    success = ConfirmSuccess(form, userName);
                    if (success)
                    {
                        unitOfWork.EIOCareNoteRepository.Update(form);
                        unitOfWork.Commit();
                    }
                    return success;
                case "IPDCT": // phiếu chăm sóc IPD
                    form = unitOfWork.EIOCareNoteRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == formId);
                    success = ConfirmSuccess(form, userName);
                    if (success)
                    {
                        unitOfWork.EIOCareNoteRepository.Update(form);
                        unitOfWork.Commit();
                    }
                    return success;
                case "A03_045_290422_VE": // phiếu GDSK nhân thân OPD, EOC, ED
                    form = unitOfWork.PatientAndFamilyEducationContentRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == formId);
                    success = ConfirmSuccess(form, userName);
                    if (success)
                    {
                        RemoveUnlock24h(visitId, form.Id);
                        unitOfWork.PatientAndFamilyEducationContentRepository.Update(form);
                        unitOfWork.Commit();
                    }
                    return success;
                case "IPDGDSK": // phiếu GDSK nhân thân IPD
                    form = unitOfWork.PatientAndFamilyEducationContentRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == formId);
                    success = ConfirmSuccess(form, userName);
                    if (success)
                    {
                        RemoveUnlock24h(visitId, form.Id);
                        unitOfWork.PatientAndFamilyEducationContentRepository.Update(form);
                        unitOfWork.Commit();
                    }
                    return success;
                case "A02_013_220321_VE": // phiếu đánh giá ban đầu người bệnh nội trú
                    form = unitOfWork.IPDMedicalRecordOfPatientRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == formId);
                    success = ConfirmSuccess(form, userName);
                    if (success)
                    {
                        unitOfWork.IPDMedicalRecordOfPatientRepository.Update(form);
                        unitOfWork.Commit();
                    }
                    return success;
                case "A03_012_080121_VE": // phiếu đánh giá ban đầu người bệnh cao tuổi
                    form = unitOfWork.IPDInitialAssessmentForFrailElderlyRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == formId);
                    success = ConfirmSuccess(form, userName);
                    if (success)
                    {
                        unitOfWork.IPDInitialAssessmentForFrailElderlyRepository.Update(form);
                        unitOfWork.Commit();
                    }
                    return success;
                case "A02_011_080121_VE": // phiếu đánh giá ban đầu người bệnh hoá trị
                    form = unitOfWork.IPDInitialAssessmentForChemotherapyRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == formId);
                    success = ConfirmSuccess(form, userName);
                    if (success)
                    {
                        unitOfWork.IPDInitialAssessmentForChemotherapyRepository.Update(form);
                        unitOfWork.Commit();
                    }
                    return success;
                case "A02_006_221221_V": // Đánh giá nguy cơ vi khuẩn đa kháng (MDRO)
                    form = unitOfWork.EIOFormRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == formId && e.FormCode == formCode);
                    success = ConfirmSuccess(form, userName);
                    if (success)
                    {
                        unitOfWork.EIOFormRepository.Update(form);
                        unitOfWork.Commit();
                    }
                    return success;
                default:
                    form = unitOfWork.EIOFormRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == formId && e.FormCode == formCode);
                    success = ConfirmSuccess(form, userName);
                    if (success)
                    {
                        unitOfWork.EIOFormRepository.Update(form);
                        unitOfWork.Commit();
                    }
                    return success;
            }
        }

        private bool CheckAcctionByFormCode(Guid visitId, Guid formId, User user, string formCode)
        {
            if (user == null) return false;

            string visitType = GetCurrentVisitType();
            bool success = false;
            switch (formCode)
            {
                case "IPDBKTPT":
                    success = IsCheckPermission(user.Username, "IPDXACNHANBKTPT");
                    if (success)
                        RemoveUnlock24h(visitId, formId);
                    return success;
                case "OPOPH":
                    success = IsCheckPermission(user.Username, "OPDXACNHANBKTPT");
                    if (success)
                        RemoveUnlock24h(visitId, formId);
                    return success;
                case "EOCBKTPT":
                    success = IsCheckPermission(user.Username, "EOCXACNHANBKTPT");
                    if (success)
                        RemoveUnlock24h(visitId, formId);
                    return success;
                case "EDBKTPT":
                    success = IsCheckPermission(user.Username, "EDXACNHANBKTPT");
                    if (success)
                        RemoveUnlock24h(visitId, formId);
                    return success;
                case "A02_066_050919_VE":
                    return IsCheckPermission(user.Username, "XACNHANGLAMORGAN");
                case "EOC_A02_007_080121_VE":
                    return IsCheckPermission(user.Username, "EOCXACNHANDGBDNBNTTT");
                case "OPD_A02_007_080121_VE":
                    return IsCheckPermission(user.Username, "OPDXACNHANDGBDNBNTTT");
                case "EOC_A02_007_220321_VE":
                    return IsCheckPermission(user.Username, "EOCXACNHANPSLN");
                case "ED_A02_007_220321_VE":
                    return IsCheckPermission(user.Username, "EDXACNHANPSLN");
                case "OPD_A02_007_220321_VE":
                    return IsCheckPermission(user.Username, "OPDXACNHANPSLN");
                case "EDER0":
                    return IsCheckPermission(user.Username, "EDXACNHANBACC");
                case "A01_155_050919_V":
                    return IsCheckPermission(user.Username, "EDXACNHANGCNBNCC");
                case "A01_143_290922_VE":
                    return IsCheckPermission(user.Username, "EDXACNHANBCYTRV");
                case "A01_144_050919_VE":
                    return IsCheckPermission(user.Username, "XACNHANBCYT");
                case "A01_198_100120_VE":
                    return IsCheckPermission(user.Username, "XACNHANBCYT");
                case "A01_145_050919_VE": //giấy chuyển viện
                    if(visitType == "IPD")
                        return IsCheckPermission(user.Username, "IRELE2");
                    if(visitType == "OPD")
                        return IsCheckPermission(user.Username, "OPDXACNHANGCV");
                    if(visitType == "ED")
                        return IsCheckPermission(user.Username, "EDXACNHANGCV");
                    if(visitType == "EOC")
                        return IsCheckPermission(user.Username, "EOCXACNHANGCV");
                    return false;
                case "A01_167_180220_VE":// giấy chuyển tuyến
                    if(visitType == "IPD")
                        return IsCheckPermission(user.Username, "ITFLE2");
                    if (visitType == "ED")
                        return IsCheckPermission(user.Username, "EDXACNHANGCT");
                    if (visitType == "OPD")
                        return IsCheckPermission(user.Username, "OPDXACNHANGCT");
                    if (visitType == "EOC")
                        return IsCheckPermission(user.Username, "EOCXACNHANGCT");
                    return false;
                case "A02_069_080121_VE":
                    return IsCheckPermission(user.Username, "IPDXACNHANDGBDSPCD");
                case "A02_014_220321_VE":
                    return IsCheckPermission(user.Username, "XACNHANDGBDNTN");
                case "A02_015_220321_VE":
                    return IsCheckPermission(user.Username, "XACNHANDGBDSSNT");
                case "A02_008_080121_VE":// nội soi dài hạn
                    return IsCheckPermission(user.Username, "EOCXACNHANDGBDNTDH");
                case "OPDIAFOGOP": //ngoại trú dài hạn
                    return IsCheckPermission(user.Username, "OPDXACNHANDGBDNTDH");
                case "OPDIAFTP": //ngoại trú từ xa
                    return IsCheckPermission(user.Username, "OPDXACNHANDGBDNBCSTX");
                case "IPD_A02_053_OR_201022_V": //Bảng kiểm an toàn phẫu thuật, thủ thuật trong phòng mổ và phòng can thiệp tim mạch
                    return IsCheckPermission(user.Username, "APICFIPDA02_053_OR_201022_V");
                case "OPD_A02_053_OR_201022_V": //Bảng kiểm an toàn phẫu thuật, thủ thuật trong phòng mổ và phòng can thiệp tim mạch
                    return IsCheckPermission(user.Username, "APICFOPDA02_053_OR_201022_V");
                case "ED_A02_053_OR_201022_V": //Bảng kiểm an toàn phẫu thuật, thủ thuật trong phòng mổ và phòng can thiệp tim mạch
                    return IsCheckPermission(user.Username, "APICFEDA02_053_OR_201022_V");
                case "EOC_A02_053_OR_201022_V": //Bảng kiểm an toàn phẫu thuật, thủ thuật trong phòng mổ và phòng can thiệp tim mạch
                    return IsCheckPermission(user.Username, "APICFEOCA02_053_OR_201022_V");
                case "A01_165_050919_VE":
                    return IsCheckPermission(user.Username, "CFA01_165_050919_VE");
                case "A01_049_050919_VE":
                    return IsCheckPermission(user.Username, "CFA01_049_050919_VE");
                case "IPDGDSK": //GDSK người bệnh nhân thân IPD
                    return IsCheckPermission(user.Username, "IPDXACNHANGDSK");
                case "A03_045_290422_VE": //GDSK người bệnh nhân thân EOC, ED, OPD
                    if(visitType == "ED")
                    return IsCheckPermission(user.Username, "EDXACNHANGDSK");
                    if (visitType == "OPD")
                        return IsCheckPermission(user.Username, "OPDXACNHANGDSK");
                    if (visitType == "EOC")
                        return IsCheckPermission(user.Username, "EOCXACNHANGDSK");
                    return false;
                case "IPDTT": //Phiếu điều trị IPD
                    return IsCheckPermission(user.Username, "IPDXACNHANPDT");
                case "A01_066_050919_VE": //Phiếu điều trị ED
                    return IsCheckPermission(user.Username, "EDXACNHANPDT");
                case "IPDCT": //Phiếu chăm sóc ED
                    return IsCheckPermission(user.Username, "IPDXACNHANPCS");
                case "A02_062_050919_V": //Phiếu chăm sóc IPD
                    return IsCheckPermission(user.Username, "EDXACNHANPCS");
                case "A02_013_220321_VE": //Xác nhận ĐGBĐ NB nội trú thông thường
                    success = IsCheckPermission(user.Username, "XACNHANDGBDNTTT");
                    if (success)
                        RemoveUnlock24h(visitId, formId);
                    return success;
                case "A03_012_080121_VE": //Xác nhận ĐGBĐ NB cao tuổi/ cuối đời
                    return IsCheckPermission(user.Username, "XACNHANDGBDNBCT");
                case "A03_165_061222_V": //Xét nghiệm tại chỗ - Khí máu và điện giải EG7+
                    if (visitType == "ED")
                        return IsCheckPermission(user.Username, "APICFEDA03_165_061222_V");
                    if (visitType == "IPD")
                        return IsCheckPermission(user.Username, "APICFIPDA03_165_061222_V");
                    return false;
                case "A02_011_080121_VE": //Xác nhận ĐGBĐ NB hoá trị
                    return IsCheckPermission(user.Username, "XACNHANDGBDNBHT");
                case "A01_252_221222_V": //Xác nhận bệnh án ngoại trú
                    return IsCheckPermission(user.Username, "APICFA01_252_221222_V");
                default: return true;
            }
        }

        void RemoveUnlock24h(Guid visitId, Guid formId)
        {
            var unlock = (from un in unitOfWork.UnlockFormToUpdateRepository.AsQueryable()
                          where !un.IsDeleted && un.VisitId == visitId
                          && un.FormId != null
                          && un.FormId == formId
                          select un).ToList();
            unlock.ForEach(e =>
            {
                e.IsDeleted = true;
                unitOfWork.UnlockFormToUpdateRepository.Update(e);
            });

            unitOfWork.Commit();
        }    
    }
}
