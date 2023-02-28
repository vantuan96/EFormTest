using DataAccess.Models;
using DataAccess.Models.GeneralModel;
using DataAccess.Models.IPDModel;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Common;
using EForm.Models;
using EForm.Utils;
using Newtonsoft.Json;
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
    public class IPDPainRecordController : BaseApiController
    {
        private readonly string visit_type = "IPD";

        [HttpGet]
        [Route("api/IPD/PainRecord/Info/{type}/{visitId}/{tab}")]
        [Permission(Code = "IPDPAINRECORDGET")]
        public IHttpActionResult GetInfo(Guid visitId, string tab, string type = "A01_042_050919_VE")
        {
            var visit = GetVisit(visitId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var site_code = GetSiteCode();
            if (string.IsNullOrEmpty(site_code))
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);
            VisitHistory visit_history = VisitHistoryFactory.GetVisit("IPD", visit, site_code);
            var visit_history_list = visit_history.GetHistory();
            return Content(HttpStatusCode.OK, new
            {
                HistoryOfAllergies = visit_history_list.Where(e => !string.IsNullOrEmpty(e.HistoryOfPresentIllness)).Select(e => new {
                    ExaminationTime = e.ExaminationTime.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                    e.ViName,
                    e.EnName,
                    e.Fullname,
                    e.Username,
                    e.HistoryOfAllergies,
                    e.VisitCode,
                    e.Type
                }),
                PastMedicalHistory = visit_history_list.Where(e => !string.IsNullOrEmpty(e.PastMedicalHistory)).Select(e => formatPastMedicalHistory(e)),
            });
        }

        [HttpGet]
        [Route("api/IPD/PainRecord/{type}/{visitId}/{tab}")]
        [Permission(Code = "IPDPAINRECORDGET")]
        public IHttpActionResult GetPainRecord(Guid visitId, string tab, string type = "A01_042_050919_VE")
        {
            var visit = GetVisit(visitId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var form = GetForm(tab, visitId);
            if (form == null)
                return Content(HttpStatusCode.NotFound, NotFoundData(visit,type));
            return Content(HttpStatusCode.OK, FormatOutput(type, tab, visit, form));
        }

        [HttpPost]
        [Route("api/IPD/PainRecord/Create/{type}/{visitId}/{tab}")]
        [Permission(Code = "IPDPAINRECORDPOST")]
        public IHttpActionResult CreatePainRecord(Guid visitId, string tab, string type = "A01_042_050919_VE")
        {
            var visit = GetVisit(visitId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);
            var form = GetForm(tab, visitId);
            if (form != null)
                return Content(HttpStatusCode.NotFound, new
                {
                    ViMessage = "Form đã tồn tại",
                    EnMessage = "Form is not found",
                    IsLocked = IPDIsBlock(visit, type)
                });
            var form_data = new IPDPainRecord()
            {
                DataType = tab,
                VisitId = visitId
            };
            unitOfWork.IPDPainRecordRepository.Add(form_data);
            UpdateVisit(visit, visit_type);
            var idForm = form_data.Id;
            CreateOrUpdateIPDIPDInitialAssessmentToByFormType(visit, tab, idForm);
            return Content(HttpStatusCode.OK, new
            {
                form_data.Id,
                form_data.VisitId,
                form_data.CreatedBy,
                form_data.CreatedAt
            });
        }

        [HttpPost]
        [Route("api/IPD/PainRecord/Update/{type}/{visitId}/{tab}")]
        [Permission(Code = "IPDPAINRECORDPUT")]
        public IHttpActionResult UpdateAPI(Guid visitId, string tab, [FromBody] JObject request, string type= "A01_042_050919_VE")
        {
            string formCodeInFormDatas = type + '_' + tab;
            var visit = GetVisit(visitId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);
            var form = GetForm(tab, visitId);
            if (form == null)
                return Content(HttpStatusCode.NotFound, NotFoundData(visit,type));
            HandleUpdateOrCreateTableFormData((Guid)form.VisitId, form.Id, formCodeInFormDatas, request["Datas"]);
            unitOfWork.IPDPainRecordRepository.Update(form);
            UpdateVisit(visit, visit_type);
            var formId = form.Id;
            CreateOrUpdateIPDIPDInitialAssessmentToByFormType(visit, tab, formId);
            return Content(HttpStatusCode.OK, new
            {
                form.Id,
                form.VisitId,
                form.UpdatedAt
            });
        }
        private IPDPainRecord GetForm(string datatype, Guid visit_id)
        {
            return unitOfWork.IPDPainRecordRepository.Find(e => !e.IsDeleted && e.VisitId == visit_id && e.DataType == datatype).FirstOrDefault();
        }
        private dynamic FormatOutput(string formCode, string tab, IPD ipd, IPDPainRecord fprm)
        {
            string formCodeinFormDatas = formCode + '_' + tab;
            var FullNameCreate = unitOfWork.UserRepository.FirstOrDefault(u => !u.IsDeleted && u.Username == fprm.CreatedBy)?.Fullname;
            var FullNameUpdate = unitOfWork.UserRepository.FirstOrDefault(u => !u.IsDeleted && u.Username == fprm.UpdatedBy)?.Fullname;
            return new
            {
                ID = fprm.Id,
                VisitId = fprm.VisitId,
                Datas = GetFormData((Guid)fprm.VisitId, fprm.Id, formCodeinFormDatas),
                CreatedBy = fprm.CreatedBy,
                FullNameCreate = FullNameCreate,
                CreatedAt = fprm.CreatedAt,
                UpdateBy = fprm.UpdatedBy,
                FullNameUpdate = FullNameUpdate,
                UpdatedAt = fprm.UpdatedAt,
                IsLocked = IPDIsBlock(ipd, formCode),
            };
        }
        protected void HandleUpdateOrCreateTableFormData(Guid VisitId, Guid FormId, string formCode, JToken request)
        {
            List<FormDatas> listInsert = new List<FormDatas>();
            List<FormDatas> listUpdate = new List<FormDatas>();
            var allergy_dct = new Dictionary<string, string>();

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
        protected void CreateOrUpdateTableFormData(Guid visitId, Guid formId, string formCode, string code, string value, string visit_type, ref List<FormDatas> listInsert, ref List<FormDatas> listUpdate)
        {
            var finded = unitOfWork.FormDatasRepository.FirstOrDefault(e =>
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

    }
}