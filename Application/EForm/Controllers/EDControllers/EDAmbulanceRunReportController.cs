using DataAccess.Models.EDModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.EDControllers
{
    [SessionAuthorize]
    public class EDAmbulanceRunReportController : BaseEDApiController
    {
        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/AmbulanceRunReport/Create/{id}")]
        [Permission(Code = "EAMRR1")]
        public IHttpActionResult CreateEDAmbulanceRunReportAPI(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var ambulance = ed.EDAmbulanceRunReport;
            if (ambulance != null)
                return Content(HttpStatusCode.NotFound, Message.ED_ARR_EXIST);

            var user = GetUser();
            var positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName).ToList();
            if (positions.Contains("Nurse"))
                ambulance = new EDAmbulanceRunReport()
                {
                    AssessmentNurseId = user.Id,
                    AssessmentNurseTime = DateTime.Now,
                };
            else if (positions.Contains("Doctor"))
                ambulance = new EDAmbulanceRunReport()
                {
                    AssessmentPhysicianId = user.Id,
                    AssessmentPhysicianTime = DateTime.Now,
                };
            else
                return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);

            //Tab 1
            CreateIdForForm(ed.Id, "A03_006_050919_VE_TAB1", "ED", ed.Version, formid: ambulance.Id);

            unitOfWork.EDAmbulanceRunReportRepository.Add(ambulance);
            ed.EDAmbulanceRunReportId = ambulance.Id;
            unitOfWork.EDRepository.Update(ed);
            unitOfWork.Commit();
            return Content(HttpStatusCode.OK, new { ambulance.Id });
        }

        [HttpGet]
        [Route("api/ED/AmbulanceRunReport/PatientManagement/{id}")]
        [Permission(Code = "EAMRR2")]
        public IHttpActionResult GetEDPatientManagementAPI(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var ambulance = ed.EDAmbulanceRunReport;
            if (ambulance == null)
                return Content(HttpStatusCode.NotFound, Message.ED_ARR_NOT_FOUND);

            var assessment_nurse = ambulance.AssessmentNurse;
            var assessment_physician = ambulance.AssessmentPhysician;

            var datas = ambulance.EDAmbulanceRunReportDatas.Where(
                e => !e.IsDeleted &&
                !string.IsNullOrEmpty(e.Form) &&
                e.Form == Constant.ED_ARR_PAMA
            ).Select(e => new { e.Id, e.Code, e.Value, e.Form });

            var tab1 = CreateIdForForm(ed.Id, "A03_006_050919_VE_TAB1", "ED", ed.Version, formid: ambulance.Id);

            return Content(HttpStatusCode.OK, new
            {
                ambulance.Id,
                IsNew = IsNew(ambulance.CreatedAt, ambulance.UpdatedAt),
                AssessmentNurseTime = ambulance.AssessmentNurseTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                AssessmentNurse = new
                {
                    assessment_nurse?.Username,
                    assessment_nurse?.FirstName,
                    assessment_nurse?.DisplayName,
                    assessment_nurse?.Title
                },
                AssessmentPhysicianTime = ambulance.AssessmentPhysicianTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                AssessmentPhysician = new
                {
                    assessment_physician?.Username,
                    assessment_physician?.FirstName,
                    assessment_physician?.DisplayName,
                    assessment_physician?.Title
                },
                Datas = datas,
                ed.Version,
                ambulance.CreatedAt,
                ambulance.CreatedBy,
                ambulance.UpdatedAt,
                ambulance.UpdatedBy,
                TabId = tab1.Id,
                TabUpdatedAt = tab1.UpdatedAt,
                TabUpdatedBy = tab1.UpdatedBy
            });
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/ED/AmbulanceRunReport/PatientManagement/{id}")]
        [Permission(Code = "EAMRR3")]
        public IHttpActionResult UpdateEDPatientManagementAPI(Guid id, [FromBody] JObject request)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var ambulance = ed.EDAmbulanceRunReport;
            if (ambulance == null)
                return Content(HttpStatusCode.NotFound, Message.ED_ARR_NOT_FOUND);

            HandleUpdateOrCreatePatientManagement(ambulance, request["Datas"]);
            var updateTab1 = CreateIdForForm(ed.Id, "A03_006_050919_VE_TAB1", "ED", ed.Version, formid: ambulance.Id, isTimeChage: true, isAnonymus: false);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpGet]
        [Route("api/ED/AmbulanceRunReport/TransferPatient/{id}")]
        [Permission(Code = "EAMRR4")]
        public IHttpActionResult GetEDTransferPatientAPI(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var ambulance = ed.EDAmbulanceRunReport;
            if (ambulance == null)
                return Content(HttpStatusCode.NotFound, Message.ED_ARR_NOT_FOUND);

            var datas = ambulance.EDAmbulanceRunReportDatas.Where(
                e => !e.IsDeleted &&
                !string.IsNullOrEmpty(e.Form) &&
                e.Form == Constant.ED_ARR_TRPA
            ).Select(e => new { e.Id, e.Code, e.Value, e.Form });

            var records = ambulance.EDAmbulanceTransferPatientsRecords
                          .Where(e => !e.IsDeleted);
            var res = (from r in records
                       join c in unitOfWork.EIOFormConfirmRepository.AsQueryable()
                       .Where(e => !e.IsDeleted)
                       on r.Id equals c.FormId into temp
                       from result in temp.DefaultIfEmpty()
                       orderby r.Time
                       select new
                       {
                           r.Id,
                           Time = r.Time?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                           r.BP,
                           r.Pulse,
                           r.RespRate,
                           r.PatternOfRespiration,
                           r.SpO2,
                           r.HR,
                           r.Procedure,
                           r.Drug,
                           r.Dose,
                           r.Route,
                           r.CreatedBy,
                           ConfirmCreated = new
                           {
                               result?.ConfirmAt,
                               result?.ConfirmBy,
                               result?.ConfirmType,
                               result?.Note
                           },
                           r.IsDeleted
                       });

            var tab2 = CreateIdForForm(ed.Id, "A03_006_050919_VE_TAB2", "ED", ed.Version, formid: ambulance.Id);

            return Content(HttpStatusCode.OK, new
            {
                ambulance.Id,
                Records = res,
                Datas = datas,
                ed.Version,
                ambulance.CreatedAt,
                ambulance.CreatedBy,
                ambulance.UpdatedAt,
                ambulance.UpdatedBy,
                TabId = tab2.Id,
                TabUpdatedBy = tab2.UpdatedBy,
                TabUpdatedAt = tab2.UpdatedAt
            });
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/ED/AmbulanceRunReport/TransferPatient/{id}")]
        [Permission(Code = "EAMRR5")]
        public IHttpActionResult UpdateEDTransferPatientAPI(Guid id, [FromBody] JObject request)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var ambulance = ed.EDAmbulanceRunReport;
            if (ambulance == null)
                return Content(HttpStatusCode.NotFound, Message.ED_ARR_NOT_FOUND);

            HandleUpdateOrCreateTransferPatient(ambulance, request);
            var updateTab2 = CreateIdForForm(ed.Id, "A03_006_050919_VE_TAB2", "ED", ed.Version, formid: ambulance.Id, isTimeChage: true, isAnonymus: false);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpGet]
        [Route("api/ED/AmbulanceRunReport/PatientHandover/{id}")]
        [Permission(Code = "EAMRR6")]
        public IHttpActionResult GetEDPatientHandoverAPI(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var ambulance = ed.EDAmbulanceRunReport;
            if (ambulance == null)
                return Content(HttpStatusCode.NotFound, Message.ED_ARR_NOT_FOUND);

            var datas = ambulance.EDAmbulanceRunReportDatas.Where(
                e => !e.IsDeleted &&
                !string.IsNullOrEmpty(e.Form) &&
                e.Form == Constant.ED_ARR_PAHA
            ).Select(e => new { e.Id, e.Code, e.Value, e.Form });

            var tab3 = CreateIdForForm(ed.Id, "A03_006_050919_VE_TAB3", "ED", ed.Version, formid: ambulance.Id);

            var transfer = ambulance.Transfer;
            var admit = ambulance.Admit;
            return Content(HttpStatusCode.OK, new
            {
                ambulance.Id,
                TransferTime = ambulance.TransferTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                Transfer = new
                {
                    transfer?.Username,
                    transfer?.FirstName,
                    transfer?.DisplayName,
                    transfer?.Title
                },
                AdmitTime = ambulance.AdmitTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                Admit = new
                {
                    admit?.Username,
                    admit?.FirstName,
                    admit?.DisplayName,
                    admit?.Title
                },
                Datas = datas,
                ed.Version,
                ambulance.CreatedAt,
                ambulance.CreatedBy,
                ambulance.UpdatedAt,
                ambulance.UpdatedBy,
                TabId = tab3.Id,
                TabUpdatedBy = tab3.UpdatedBy,
                TabUpdatedAt = tab3.UpdatedAt
            });
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/ED/AmbulanceRunReport/PatientHandover/{id}")]
        [Permission(Code = "EAMRR7")]
        public IHttpActionResult UpdateEDPatientHandoverAPI(Guid id, [FromBody] JObject request)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var ambulance = ed.EDAmbulanceRunReport;
            if (ambulance == null)
                return Content(HttpStatusCode.NotFound, Message.ED_ARR_NOT_FOUND);

            if (ambulance.TransferId != null || ambulance.AdmitId != null)
                return Content(HttpStatusCode.BadRequest, Message.OWNER_FORBIDDEN);

            HandleUpdateOrCreatePatientHandover(ambulance, request["Datas"]);
            var updateTab3 = CreateIdForForm(ed.Id, "A03_006_050919_VE_TAB3", "ED", ed.Version, formid: ambulance.Id, isTimeChage: true, isAnonymus: false);
            unitOfWork.EDAmbulanceRunReportRepository.Update(ambulance);
            unitOfWork.Commit();

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/ED/AmbulanceRunReport/PatientHandover/Confirm/{id}")]
        [Permission(Code = "EAMRR8")]
        public IHttpActionResult ConfirmEDPatientHandoverAPI(Guid id, [FromBody] JObject request)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var ambulance = ed.EDAmbulanceRunReport;
            if (ambulance == null)
                return Content(HttpStatusCode.NotFound, Message.ED_ARR_NOT_FOUND);

            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var kind = request["kind"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null || string.IsNullOrEmpty(kind))
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);

            if (kind == "Transfer" && ambulance.TransferId == null)
            {
                ambulance.TransferTime = DateTime.Now;
                ambulance.TransferId = user.Id;
            }
            else if (kind == "Admit" && ambulance.AdmitId == null)
            {
                ambulance.AdmitTime = DateTime.Now;
                ambulance.AdmitId = user.Id;
            }
            else
                return Content(HttpStatusCode.BadRequest, Message.OWNER_FORBIDDEN);

            unitOfWork.EDAmbulanceRunReportRepository.Update(ambulance);
            unitOfWork.Commit();
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        private void HandleUpdateOrCreatePatientManagement(EDAmbulanceRunReport ambulance, JToken request)
        {
            var datas = ambulance.EDAmbulanceRunReportDatas.Where(
                e => !e.IsDeleted &&
                !string.IsNullOrEmpty(e.Form) &&
                e.Form == Constant.ED_ARR_PAMA
            ).ToList();


            foreach (var item in request)
            {
                var code = item["Code"]?.ToString();
                if (string.IsNullOrEmpty(code)) continue;

                var data = GetOrCreateAmbulanceRunReportData(code, ambulance.Id, Constant.ED_ARR_PAMA, datas);

                if (data == null) continue;
                var value = item["Value"]?.ToString();
                UpdateAmbulanceRunReportData(data, value);
            }

            var user = GetUser();
            var positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName).ToList();
            if (positions.Contains("Nurse"))
            {
                ambulance.AssessmentNurseId = user.Id;
                ambulance.AssessmentNurseTime = DateTime.Now;
            }
            else if (positions.Contains("Doctor"))
            {
                ambulance.AssessmentPhysicianId = user.Id;
                ambulance.AssessmentPhysicianTime = DateTime.Now;
            }
            unitOfWork.EDAmbulanceRunReportRepository.Update(ambulance);
            unitOfWork.Commit();
        }
        private EDAmbulanceRunReportData GetOrCreateAmbulanceRunReportData(string code, Guid form_id, string form, List<EDAmbulanceRunReportData> datas)
        {
            var data = datas.FirstOrDefault(e => e.Code == code);
            if (data != null) return data;

            data = new EDAmbulanceRunReportData()
            {
                EDAmbulanceRunReportId = form_id,
                Code = code,
                Form = form,
            };
            unitOfWork.EDAmbulanceRunReportDataRepository.Add(data);
            return data;
        }
        private void UpdateAmbulanceRunReportData(EDAmbulanceRunReportData data, string value)
        {
            data.Value = value;
            unitOfWork.EDAmbulanceRunReportDataRepository.Update(data);
        }

        private void HandleUpdateOrCreateTransferPatient(EDAmbulanceRunReport ambulance, JObject request)
        {
            var datas = ambulance.EDAmbulanceRunReportDatas.Where(
                e => !e.IsDeleted &&
                !string.IsNullOrEmpty(e.Form) &&
                e.Form == Constant.ED_ARR_TRPA
            ).ToList();
            foreach (var item in request["Datas"])
            {
                var code = item["Code"]?.ToString();
                if (string.IsNullOrEmpty(code)) continue;

                var data = GetOrCreateAmbulanceRunReportData(code, ambulance.Id, Constant.ED_ARR_TRPA, datas);
                var value = item["Value"]?.ToString();
                if (data == null || data.Value == value) continue;

                UpdateAmbulanceRunReportData(data, value);
            }

            var user = GetUser();
            var records = ambulance.EDAmbulanceTransferPatientsRecords.Where(e => !e.IsDeleted).ToList();
            foreach (var item in request["Records"])
            {
                var id = item["Id"]?.ToString();
                var data = GetOrCreateAmbulanceTransferPatientsRecord(id, ambulance.Id, records);
                var value = item["Value"]?.ToString();
                if (data == null || data.CreatedBy != user.Username) continue;
               
                UpdateAmbulanceTransferPatientsRecord(data, item);
            }
            unitOfWork.EDAmbulanceRunReportRepository.Update(ambulance);
            unitOfWork.Commit();
        }
        private EDAmbulanceTransferPatientsRecord GetOrCreateAmbulanceTransferPatientsRecord(string id, Guid form_id, List<EDAmbulanceTransferPatientsRecord> datas)
        {
            if (string.IsNullOrEmpty(id))
            {
                var new_data = new EDAmbulanceTransferPatientsRecord()
                {
                    EDAmbulanceRunReportId = form_id,
                };
                unitOfWork.EDAmbulanceTransferPatientsRecordRepository.Add(new_data);
                return new_data;
            }

            Guid data_id = new Guid(id);
            return datas.FirstOrDefault(e => e.Id == data_id);
        }
        private void UpdateAmbulanceTransferPatientsRecord(EDAmbulanceTransferPatientsRecord data, JToken item)
        {
            var time = item["Time"]?.ToString();
            if (string.IsNullOrEmpty(time))
                data.Time = null;
            else
                data.Time = DateTime.ParseExact(time, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            data.BP = item["BP"]?.ToString();
            data.Pulse = item["Pulse"]?.ToString();
            data.RespRate = item["RespRate"]?.ToString();
            data.PatternOfRespiration = item["PatternOfRespiration"]?.ToString();
            data.SpO2 = item["SpO2"]?.ToString();
            data.HR = item["HR"]?.ToString();
            data.Procedure = item["Procedure"]?.ToString();
            data.Drug = item["Drug"]?.ToString();
            data.Dose = item["Dose"]?.ToString();
            data.Route = item["Route"]?.ToString();
            data.IsDeleted = item["IsDeleted"].ToObject<bool>();
            unitOfWork.EDAmbulanceTransferPatientsRecordRepository.Update(data);
        }

        private void HandleUpdateOrCreatePatientHandover(EDAmbulanceRunReport ambulance, JToken request)
        {
            var datas = ambulance.EDAmbulanceRunReportDatas.Where(
                e => !e.IsDeleted &&
                !string.IsNullOrEmpty(e.Form) &&
                e.Form == Constant.ED_ARR_PAHA
            ).ToList();


            foreach (var item in request)
            {
                var code = item["Code"]?.ToString();
                if (string.IsNullOrEmpty(code)) continue;

                var data = GetOrCreateAmbulanceRunReportData(code, ambulance.Id, Constant.ED_ARR_PAHA, datas);

                if (data == null) continue;
                var value = item["Value"]?.ToString();
                UpdateAmbulanceRunReportData(data, value);
            }
            unitOfWork.EDAmbulanceRunReportRepository.Update(ambulance);
            unitOfWork.Commit();
        }
    }
}
