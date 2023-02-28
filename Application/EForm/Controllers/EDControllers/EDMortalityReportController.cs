using DataAccess.Models.EDModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers.BaseEIOControllers;
using EForm.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web.Http;

namespace EForm.Controllers.EDControllers
{
    [SessionAuthorize]
    public class EDMortalityReportController : EIOMortalityReportController
    {
        [HttpGet]
        [Route("api/ED/MortalityReport/{id}")]
        [Permission(Code = "EMORE1")]
        public IHttpActionResult GetMortalityReportAPI(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var mortality = GetEIOMortalityReport(ed.Id, "ED");
            if (mortality == null)
                return Content(HttpStatusCode.NotFound, Message.ED_MORE_NOT_FOUND);

            return Content(HttpStatusCode.OK, GetMortalityReportResult(mortality, ed));
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/ED/MortalityReport/Create/{id}")]
        [Permission(Code = "EMORE2")]
        public IHttpActionResult CreateMortalityReportAPI(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var mortality = GetEIOMortalityReport(ed.Id, "ED");
            if (mortality != null)
                return Content(HttpStatusCode.NotFound, Message.ED_MORE_EXIST);

            CreateEIOMortalityReport(ed.Id,"ED");
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/ED/MortalityReport/{id}")]
        [Permission(Code = "EMORE3")]
        public IHttpActionResult UpdateMortalityReportAPI(Guid id, [FromBody]JObject request)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var mortality = GetEIOMortalityReport(ed.Id, "ED");
            if (mortality == null)
                return Content(HttpStatusCode.NotFound, Message.ED_MORE_NOT_FOUND);

            var members = mortality.EDMortalityReportMembers.Where(e => !e.IsDeleted);

            if (mortality.ChairmanTime != null || mortality.SecretaryTime != null || members.FirstOrDefault( e=> e.ConfirmTime != null) != null)
                return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);

            UpdateMortalityReport(mortality, members, request, app_version: ed.Version);
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/ED/MortalityReport/Sync/{id}")]
        [Permission(Code = "EMORE4")]
        public IHttpActionResult SyncMortalityReportAPI(Guid id, [FromBody]JObject request)
        {
            ED ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var req_method = request["Method"]?.ToString();
            if (string.IsNullOrEmpty(req_method))
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            Type thisType = this.GetType();
            MethodInfo method_info = thisType.GetMethod(req_method, BindingFlags.Instance | BindingFlags.NonPublic);
            if (method_info == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);
            object[] parameters = new object[] { ed };
            var result = method_info.Invoke(this, parameters);
            return Content(HttpStatusCode.OK, result);
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/ED/MortalityReport/Confirm/{id}")]
        [Permission(Code = "EMORE5")]
        public IHttpActionResult ConfirmMortalityReportAPI(Guid id, [FromBody]JObject request)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var mortality = GetEIOMortalityReport(ed.Id, "ED");
            if (mortality == null)
                return Content(HttpStatusCode.NotFound, Message.ED_MORE_NOT_FOUND);

            var error = ConfirmMortalityReport(mortality, request);
            if (error != null)
                return Content(HttpStatusCode.NotFound, error);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        
        private dynamic SyncPastMedicalHistory(ED ed)
        {
            var result = new List<string>();
            var emer_record = ed.EmergencyRecord;
            var emer_record_datas = emer_record.EmergencyRecordDatas;
            var past_health_history = emer_record_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "ER0PHHANS")?.Value;
            if (!string.IsNullOrEmpty(past_health_history))
                result.Add($"Tiền sử: {past_health_history}");
            var history = emer_record_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "ER0HISANS")?.Value;
            if (!string.IsNullOrEmpty(history))
                result.Add($"Bệnh sử: {history}");

            return result;
        }
        private dynamic SyncStatus(ED ed)
        {
            var result = new List<string>();
            var emer_record = ed.EmergencyRecord;
            var emer_record_datas = emer_record.EmergencyRecordDatas;
            var history = emer_record_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "ER0HISANS")?.Value;
            if (!string.IsNullOrEmpty(history))
                result.Add($"Bệnh sử: {history}");
            var assessment = new EmergencyRecordAssessment(emer_record.Id).GetString(); ;
            if (!string.IsNullOrEmpty(assessment))
                result.Add($"Thăm khám: \n{assessment}");

            var discharge_info = ed.DischargeInformation;
            var discharge_info_datas = discharge_info.DischargeInformationDatas;
            var rop_tests = discharge_info_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0RPTANS2")?.Value;
            if (!string.IsNullOrEmpty(rop_tests))
                result.Add($"Kết quả cận lâm sàng: {rop_tests}");

            return result;
        }
        private dynamic SyncDiagnosis(ED ed)
        {
            var result = new List<string>();
            var discharge_info = ed.DischargeInformation;
            var discharge_info_datas = discharge_info.DischargeInformationDatas;
            var diagnosis = discharge_info_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0DIAANS")?.Value;
            if (!string.IsNullOrEmpty(diagnosis))
                result.Add(diagnosis);

            return result;
        }
        private dynamic SyncProgress(ED ed)
        {
            var patient_progress_note = ed.PatientProgressNote;
            return patient_progress_note.PatientProgressNoteDatas.Where(ppnd => !ppnd.IsDeleted)
                .OrderBy(ppnd => ppnd.NoteAt)
                .Select(ppnd => new
                {
                    ppnd.Id,
                    NoteAt = ppnd.NoteAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                    ppnd.Note,
                    ppnd.Interventions,
                    Username = ppnd.CreatedBy
                });
        }
        private dynamic SyncAssessment(ED ed)
        {
            var result = new List<string>();
            var emer_record = ed.EmergencyRecord;
            var emer_record_datas = emer_record.EmergencyRecordDatas;
            var assessment = new EmergencyRecordAssessment(emer_record.Id).GetString();
            if (!string.IsNullOrEmpty(assessment))
                result.Add($"Thăm khám: \n{assessment}");
            var initial_diagnosis = emer_record_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "ER0ID0ANS")?.Value;
            if (!string.IsNullOrEmpty(initial_diagnosis))
                result.Add($"Chẩn đoán sơ bộ: {initial_diagnosis}");

            return result;
        }
        private dynamic SyncTreatmentAndProcedures(ED ed)
        {
            var result = new List<string>();
            var discharge_info = ed.DischargeInformation;
            var discharge_info_datas = discharge_info.DischargeInformationDatas;
            var treatment_procedures = discharge_info_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0TAPANS")?.Value;
            if (!string.IsNullOrEmpty(treatment_procedures))
                result.Add($"Phương pháp điều trị: {treatment_procedures}");

            return result;
        }
    }
}
