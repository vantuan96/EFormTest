using DataAccess.Models;
using DataAccess.Models.EDModel;
using DataAccess.Models.EIOModel;
using DataAccess.Models.EOCModel;
using DataAccess.Models.IPDModel;
using DataAccess.Models.OPDModel;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Common;
using EForm.Models;
using EMRModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.EIOControllers
{
    [SessionAuthorize]
    public class EIOAllFormController : BaseApiController
    {
        
        protected IHttpActionResult GetFormAPI(Guid id, string vist_type, string formCode)
        {
            var visit = GetVisit(id, vist_type);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);

            var form = GetForm(id, formCode);
            if(form == null)
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);
            var data = FormatOutput(visit, form);
            return Content(HttpStatusCode.OK, new { data});
        }
        protected IHttpActionResult CreateAPI(Guid id, string visit_type, string formCode)
        {
            var visit = GetVisit(id, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);
            if(formCode == "A01_001_080721_V" || formCode == "A01_014_050919_VE")// áp dụng cho form làm một lần
            {
                var form = GetForm(id, formCode);
                if (form != null)
                    return Content(HttpStatusCode.BadRequest, Message.FORM_EXIST);
            }           

            var form_data = new EIOForm
            {
                VisitId = id,
                Version = 1,
                VisitTypeGroupCode = visit_type,
                FormCode = formCode
            };
            unitOfWork.EIOFormRepository.Add(form_data);
            unitOfWork.Commit();
            UpdateVisit(visit, visit_type);

            return Content(HttpStatusCode.OK, new { form_data.Id, form_data.CreatedAt, form_data.CreatedBy, form_data.VisitId});
        }
        protected IHttpActionResult UpdateAPI(Guid id, [FromBody] JObject request, string visit_type, string formCode)
        {
            var visit = GetVisit(id, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);

            var form = GetForm(id, formCode);
            if (form == null)
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);
            if(formCode == "A01_001_080721_V" || formCode == "A01_014_050919_VE")// áp dụng cho form ai làm nguời đó đc chỉnh sửa
            {
                var user = GetUser();
                if (user.Username != form.CreatedBy && !IsCheckConfirm(form.Id))
                {
                    return Content(HttpStatusCode.Forbidden, Message.OWNER_FORBIDDEN);
                }
            }
           
            if (form.ConfirmBy != null && formCode == "abc")// áp dungj cho form có xác nhận
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);

            HandleUpdateOrCreateFormDatas((Guid)form.VisitId, form.Id, formCode, request["Datas"]);

            form.Note = request["Note"]?.ToString();
            form.Comment = request["Comment"]?.ToString();          
            unitOfWork.EIOFormRepository.Update(form);

            UpdateVisit(visit, visit_type);

            return Content(HttpStatusCode.OK, new { form.Id });
        }
        protected dynamic FormatOutput(dynamic visit, EIOForm fprm)
        {
            var user = unitOfWork.UserRepository.FirstOrDefault(e => e.Username == fprm.CreatedBy);
            var customer = visit.Customer; return new
            {
                fprm.Id,
                Datas = GetFormData((Guid)fprm.VisitId, fprm.Id, fprm.FormCode),
                fprm.CreatedBy,
                CreatedAt = fprm.CreatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                VisitId = fprm.VisitId,
                fprm.Note,
                fprm.Comment,
                IsNew = fprm.CreatedAt < fprm.UpdatedAt ? false : true,
                UpdatedAt = fprm.UpdatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                ConfirmInfos = GetFormConfirms(fprm.Id),
                CreatedByFullName = user?.Fullname,
                DiagnosisAndICD = GetVisitDiagnosisAndICD((Guid)fprm.VisitId, fprm.VisitTypeGroupCode),
                FullNameNB = customer?.Fullname,
                PID = customer?.PID,
                DateOfBirth = customer?.DateOfBirth,
                Gender = customer?.Gender,
                UpdatedBy = fprm.UpdatedBy
            };
        }       
        protected DiagnosisAndICDModel GetVisitDiagnosisAndICD(Guid visit_id, string visit_type)
        {
            if (visit_type == "ED")
            {
                ED visit = GetED(visit_id);
                if (visit != null)
                {
                   
                  var data_dir = visit.EmergencyRecord.EmergencyRecordDatas;                    
                  return new DiagnosisAndICDModel
                  {
                      ICD = data_dir.FirstOrDefault(e => e.Code == "ER0ICD102")?.Value,
                      Diagnosis = data_dir.FirstOrDefault(e => e.Code == "ER0ID0ANS")?.Value,
                      ICDOption = null
                  };
                }
            }
            if (visit_type == "OPD")
            {
                OPD visit = GetOPD(visit_id);
                if (visit != null)
                {
                    var data_eon = visit.OPDOutpatientExaminationNote.OPDOutpatientExaminationNoteDatas;
                    return new DiagnosisAndICDModel
                    {
                        ICD = data_eon.FirstOrDefault(e => e.Code == "OPDOENICD0101")?.Value,
                        Diagnosis = data_eon.FirstOrDefault(e => e.Code == "OPDOENID0ANS")?.Value,
                        ICDOption = data_eon.FirstOrDefault(e => e.Code == "OPDOENICDOPT")?.Value
                    };
                }
            }
            if (visit_type == "EOC")
            {
                EOC visit = GetEOC(visit_id);
                if (visit != null)
                {
                   var data_dir = unitOfWorkDapper.FormDatasRepository.Find(e =>
                                                                            e.IsDeleted == false &&
                                                                           e.VisitId == visit.Id).ToList();
                   return new DiagnosisAndICDModel
                   {
                       ICD = null,
                       Diagnosis = data_dir.FirstOrDefault(e => e.Code == "OPDOENID0ANS")?.Value,
                       ICDOption = data_dir.FirstOrDefault(e => e.Code == "OPDOENID0ANS")?.Value
                   };
                }
            }

            if (visit_type == "IPD")
            {
                IPD visit = GetIPD(visit_id);
                if (visit != null)
                {
                    var medical_record = visit.IPDMedicalRecord;
                    if (medical_record != null)
                    {
                        var part_2 = visit.IPDMedicalRecord.IPDMedicalRecordPart2;
                        if (part_2 != null)
                        {
                            var data_eon = visit.IPDMedicalRecord.IPDMedicalRecordPart2.IPDMedicalRecordPart2Datas;
                            var returnData = new DiagnosisAndICDModel
                            {
                                ICD = data_eon.FirstOrDefault(e => e.Code == "IPDMRPTICDCANS")?.Value,
                                Diagnosis = data_eon.FirstOrDefault(e => e.Code == "IPDMRPTCDBCANS")?.Value,
                                ICDOption = data_eon.FirstOrDefault(e => e.Code == "IPDMRPTICDPANS")?.Value,
                                DiagnosisOption = data_eon.FirstOrDefault(e => e.Code == "IPDMRPTCDKTANS")?.Value
                            };
                            return returnData;
                        }
                    }
                }
            }
            
            return new DiagnosisAndICDModel { };
        }

    }
}