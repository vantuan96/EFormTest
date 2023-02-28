using DataAccess.Models;
using DataAccess.Models.EIOModel;
using DataAccess.Models.GeneralModel;
using DataAccess.Models.IPDModel;
using DataAccess.Models.OPDModel;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Common;
using EForm.Models;
using EForm.Models.OPDModels;
using EForm.Utils;
using EMRModels;
using Helper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web.Http;

namespace EForm.Controllers.OPDControllers
{
    [SessionAuthorize]
    public class OPDPregnancyFollowUpRecordControllerController : BaseApiController
    {
        private readonly string visit_type = "OPD";
        [HttpGet]
        [Route("api/OPD/PregnancyFollowUpRecord/Info/{type}/{visitId}")]
        //[Permission(Code = "NCCNBROV1GET")]
        public IHttpActionResult GetInfo(Guid visitId, string type = "A01_067_050919_VE")
        {
            var visit = GetOPD(visitId);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            OPDHistory opdHistory = new OPDHistory(visit, visit.Site.Code);
            List<VisitModel> lstPersonalHistory = opdHistory.GetPersonalHistory(visitId);
            var user = GetUser();

            var etrMasterDataCode = new string[] { "OPDIAFSTOPHEIANS" };
            var dataEtr = unitOfWork.OPDInitialAssessmentForShortTermDataRepository
              .Find(e => etrMasterDataCode.Contains(e.Code) && e.OPDInitialAssessmentForShortTermId == visit.OPDInitialAssessmentForShortTermId)
              .Select(e => new MasterDataValue
              {
                  Code = e.Code,
                  Value = e.Value,
              }).ToList();
            var weightBeforePregnancy = unitOfWork.FormDatasRepository?.FirstOrDefault(x => x.VisitId == visitId && x.FormCode == "A01_067_050919_VE_TAB1" && x.Code == "PRFOURE27" && !x.IsDeleted)?.Value;
            var isHasDataFromTab2 = unitOfWork.EIOFormRepository.AsQueryable().Any(x => x.FormCode == "A01_067_050919_VE_TAB2" && x.VisitId == visitId && x.VisitTypeGroupCode == "OPD" && !x.IsDeleted);
            var masterdataforms = new MasterdataForms();
            if (!isHasDataFromTab2)
            {
                var opds = unitOfWork.OPDRepository.Find(x => x.CustomerId == visit.CustomerId && !x.IsDeleted && x.Id != visit.Id).OrderByDescending(x => x.AdmittedDate).ToList();
                if (opds.Count > 0)
                {
                    var opd = opds.FirstOrDefault();
                    var forms = GetForms(opd.Id, "A01_067_050919_VE_TAB2");
                    if (forms.Count > 0)
                    {
                        masterdataforms.VisitId = opd.Id;
                        masterdataforms.Forms = forms.Select(form => FormatMasterOutput(form, "A01_067_050919_VE_TAB2")).ToList();
                        masterdataforms.FormCode = "A01_067_050919_VE_TAB2";
                    }
                    else
                    {
                        masterdataforms = null;
                    }
                }
                else
                {
                    masterdataforms = null;
                }
            }
            var allergy = EMRVisitAllergy.GetOPDVisitAllergy(visit);
            // dannv6 lưu form theo setup admin
            var form_obj = unitOfWork.EIOFormRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == visit.Id && e.FormCode == type && e.VisitTypeGroupCode == visit_type);
            if(form_obj != null)
                CreateOrUpdateFormForSetupOfAdmin(visit.Id, form_obj.Id, "A01_067_050919_VE");
           
            return Content(HttpStatusCode.OK, new
            {
                Height = getValueFromMasterDatas("OPDIAFSTOPHEIANS", dataEtr),
                WeightBeforePregnancy = weightBeforePregnancy,
                PersonalHistory = lstPersonalHistory.Select(x => new
                {
                    x.Username,
                    x.Fullname,
                    x.ViName,
                    x.EnName,
                    x.FamilyMedicalHistory,
                    x.VisitTypeGroup,
                    x.ExaminationTime
                }),
                IsLocked24h = Is24hLocked(visit.CreatedAt, visitId, ConvertFormCode(type), user.Username),
                IsNewTab2 = !isHasDataFromTab2,
                DataOld = masterdataforms,
                Allergy = allergy
            });
        }
        private List<EIOForm> GetForms(Guid visit_id, string formcode)
        {
            return unitOfWork.EIOFormRepository.Find(e => !e.IsDeleted && e.VisitId == visit_id && e.FormCode == formcode).OrderBy(e => e.CreatedAt).ToList();
        }
        private MasterdataForm FormatMasterOutput(EIOForm fprm, string formCode)
        {
            return new MasterdataForm
            {
                Id = fprm.Id,
                Datas = GetFormDatas((Guid)fprm.VisitId, fprm.Id, formCode),
                IsDeleted = false
            };
        }
        private List<FormDataValue> GetFormDatas(Guid visitId, Guid formId, string formCode)
        {
            return unitOfWork.FormDatasRepository.Find(e =>
                    !e.IsDeleted &&
                    e.VisitId == visitId &&
                    e.FormCode == formCode &&
                    e.FormId == formId
            ).Select(f => new FormDataValue { Code = f.Code, Value = f.Value, FormId = f.FormId, FormCode = f.FormCode }).ToList();
        }
    }
}