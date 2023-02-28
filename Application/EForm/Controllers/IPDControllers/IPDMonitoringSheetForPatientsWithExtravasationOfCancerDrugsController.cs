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
    public class IPDMonitoringSheetForPatientsWithExtravasationOfCancerDrugsController : BaseApiController
    {
        private readonly string visit_type = "IPD";

        [HttpGet]
        [Route("api/IPD/MonitoringSheetForPatientsWithExtravasationOfCancerDrugs/Info/{type}/{ipdId}")]
        [Permission(Code = "NBTMDTUT1")]
        public IHttpActionResult GetInfoForMonitoringSheetForPatientsWithExtravasationOfCancerDrug(string type, Guid ipdId)
        {
            IPD visit = GetVisit(ipdId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);
            var medicalRecordPart2s = visit.IPDMedicalRecord?.IPDMedicalRecordPart2?.IPDMedicalRecordPart2Datas;
            var medicalRecordPart3s = visit.IPDMedicalRecord?.IPDMedicalRecordPart3?.IPDMedicalRecordPart3Datas;
            return Content(HttpStatusCode.OK, new
            {
                IsLocked = IPDIsBlock(visit, type)
            });
        }

        [HttpGet]
        [Route("api/IPD/MonitoringSheetForPatientsWithExtravasationOfCancerDrugs/{type}/{ipdId}/{id}")]
        [Permission(Code = "NBTMDTUT1")]
        public IHttpActionResult GetMonitoringSheetForPatientsWithExtravasationOfCancerDrug(string type, Guid ipdId, Guid id)
        {
            var visit = GetVisit(ipdId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);
            var form = unitOfWork.IPDMonitoringSheetForPatientsWithExtravasationOfCancerDrugsRepository.Find(e => !e.IsDeleted && e.Id == id).FirstOrDefault();
            if (form == null)
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND_WITH_LOCKED);
            return Content(HttpStatusCode.OK, FormatOutput(type, visit, form));
        }

        [HttpGet]
        [Route("api/IPD/MonitoringSheetForPatientsWithExtravasationOfCancerDrugs/{type}/{ipdId}")]
        [Permission(Code = "NBTMDTUT1")]
        public IHttpActionResult GetMonitoringSheetForPatientsWithExtravasationOfCancerDrugs(string type, Guid ipdId)
        {
            var visit = GetVisit(ipdId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);
            var forms = unitOfWork.IPDMonitoringSheetForPatientsWithExtravasationOfCancerDrugsRepository.Find(e => !e.IsDeleted && e.VisitId == ipdId).OrderBy(o => o.Order).ToList().Select(form => new
            {
                form.Id,
                form.VisitId,
                form.CreatedBy,
                CreatedAt = form.CreatedAt,
                UpdatedAt = form.UpdatedAt,
                Order = form.Order
            }).ToList();
            bool IsLocked = IPDIsBlock(visit, type);
            return Content(HttpStatusCode.OK, new
            {
                Datas = forms,
                IsLocked
            });
        }

        [HttpPost]
        [Route("api/IPD/MonitoringSheetForPatientsWithExtravasationOfCancerDrugs/Create/{type}/{ipdId}")]
        // [Permission(Code = "SO15DT1")]
        public IHttpActionResult CreateMonitoringSheetForPatientsWithExtravasationOfCancerDrugs(string type, Guid ipdId)
        {
            var visit = GetVisit(ipdId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);
            var form_data = new IPDMonitoringSheetForPatientsWithExtravasationOfCancerDrugs()
            {
                VisitId = ipdId
            };
            unitOfWork.IPDMonitoringSheetForPatientsWithExtravasationOfCancerDrugsRepository.Add(form_data);
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
        [Route("api/IPD/MonitoringSheetForPatientsWithExtravasationOfCancerDrugs/Update/{type}/{ipdId}/{id}")]
        [Permission(Code = "NBTMDTUT")]
        public IHttpActionResult UpdateMonitoringSheetForPatientsWithExtravasationOfCancerDrug(string type, Guid ipdId, Guid id, [FromBody] JObject request)
        {
            //var validates = ValidateTextEmpty(Constant.IPD_MONITOR_FOR_PATIENT, request["Datas"]);
            //if (validates.Count > 0)
            //    return Content(HttpStatusCode.OK, new { Error = validates });
            var visit = GetVisit(ipdId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);
            var form = GetForm(id);
            if (form == null)
                return Content(HttpStatusCode.NotFound, NotFoundData(visit, type));
            //var user = GetUser();
            //if (user == null || user.Username != form.CreatedBy)
            //    return Content(HttpStatusCode.BadRequest, Message.OWNER_FORBIDDEN);
            //HandleUpdateOrCreateFormDatas((Guid)form.VisitId, form.Id, type, request["Datas"]);
            unitOfWork.IPDMonitoringSheetForPatientsWithExtravasationOfCancerDrugsRepository.Update(form);
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
        private IPDMonitoringSheetForPatientsWithExtravasationOfCancerDrugs GetForm(Guid id)
        {
            return unitOfWork.IPDMonitoringSheetForPatientsWithExtravasationOfCancerDrugsRepository.Find(e => !e.IsDeleted && e.Id == id).FirstOrDefault();
        }
        private dynamic FormatOutput(string type, IPD ipd, IPDMonitoringSheetForPatientsWithExtravasationOfCancerDrugs fprm)
        {
            var datas = GetFormData((Guid)fprm.VisitId, fprm.Id, type);
            return new
            {
                Id = fprm.Id,
                VisitId = fprm.VisitId,
                Datas = datas,
                CreatedBy = fprm.CreatedBy,
                CreatedAt = fprm.CreatedAt,
                UpdateBy = fprm.UpdatedBy,
                UpdatedAt = fprm.UpdatedAt,
                IsLocked = IPDIsBlock(ipd, type)
            };
        }
        protected List<MedicalReportDataModels> ValidateTextEmpty(string[] code, JToken request_data)
        {
            List<MedicalReportDataModels> list = new List<MedicalReportDataModels>();
            foreach (var data in request_data)
            {
                if (code.Contains(data["Code"].ToString()) && String.IsNullOrEmpty(data["Value"].ToString()))
                {
                    MedicalReportDataModels medicalReportDataModels = new MedicalReportDataModels();
                    string strCode = data["Code"].ToString();
                    MasterData masterdata = unitOfWork.MasterDataRepository.FirstOrDefault(x => x.Code == strCode);
                    medicalReportDataModels.ViName = masterdata?.ViName;
                    medicalReportDataModels.EnName = masterdata?.EnName;
                    medicalReportDataModels.Code = data["Code"].ToString();
                    list.Add(medicalReportDataModels);
                }
            }
            return list;
        }
    }
}