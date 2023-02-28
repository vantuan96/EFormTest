using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Xml.Linq;
using DataAccess.Models;
using DataAccess.Models.EDModel;
using DataAccess.Models.GeneralModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers;
using EForm.Models;
using EForm.Utils;
using EMRModels;
using Newtonsoft.Json.Linq;
using static EForm.Models.OrdersResponse;

namespace EForm.Controllers.EDControllers
{
    [SessionAuthorize]
    public class EDEmergencyTriageRecordsController : BaseEDApiController
    {
        private const string timeUpdateVersion2 = "UPDATE_VERSION2_A02_007_220321_VE";

        [HttpGet]
        [Route("api/ED/EmergencyTriageRecords/{id}")]
        [Permission(Code = "EEMTR1")]
        public IHttpActionResult GetEmergencyTriageRecordAPI(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var etr = ed.EmergencyTriageRecord;
            if (etr == null || etr.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.ED_ETR_NOT_FOUND);

            var isVisitLastUpdate = IsVisitLastTimeUpdate(ed, timeUpdateVersion2);
            var rooms = GetRooms(id);
            return Content(HttpStatusCode.OK, new {
                ed.RecordCode,
                Rooms = rooms,
                etr.Id,
                etr.Version,
                TriageDateTime = etr.TriageDateTime.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                etr.Bed,
                Room = new { etr.Room?.Id, etr.Room?.ViName, etr.Room?.EnName },
                EDId = ed.Id,
                InitialAssessmentID = ed.EmergencyRecordId,
                IsNew = IsNew(etr.CreatedAt, etr.UpdatedAt),
                Datas = etr.EmergencyTriageRecordDatas
                .Where(e => !e.IsDeleted)
                .Select(etrd => new { etrd.Id, etrd.Code, etrd.Value }).ToList(),
                IsCovidSpecialty = IsCovidSpecialty(),
                ed.CovidRiskGroup,
                PrimaryDoctor = new
                {
                    ed.PrimaryDoctor?.Id,
                    ed.PrimaryDoctor?.Username,
                    ed.PrimaryDoctor?.Fullname
                },
                PrimaryDoctorId = ed.PrimaryDoctor?.Id,
                IsVisitLastUpdate = isVisitLastUpdate,                
            });
        }
 
        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/EmergencyTriageRecords/{id}")]
        [Permission(Code = "EEMTR2")]
        public IHttpActionResult UpdateEmergencyTriageRecordAPI(Guid id, [FromBody]JObject request)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var etr = ed.EmergencyTriageRecord;
            if (etr == null || etr.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.ED_ETR_NOT_FOUND);

            var status_code = ed?.EDStatus?.Code;
            if (!string.IsNullOrEmpty(status_code) && !Constant.InHospital.Contains(status_code))
                return Content(HttpStatusCode.NotFound, Message.ED_ETR_PROHIBIT);

            //var user = GetUser();
            //if (IsBlockAfter24h(ed.CreatedAt) && !HasUnlockPermission(ed.Id, "EDETR", user.Username))
            //    return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);

            //if (!IsUserCreateFormAuto(user.Username, etr.UpdatedBy, etr.CreatedAt, etr.UpdatedAt))
            //    return Content(HttpStatusCode.BadRequest, Message.OWNER_FORBIDDEN);

            UpdateVisit(ed, etr, request);

            HandleUpdateOrCreateEmergencyTriageRecordData(ed, etr, request["Datas"]);
            
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/EmergencyTriageRecords/Sync/{id}")]
        [Permission(Code = "EEMTR3")]
        public IHttpActionResult SyncEmergencyTriageRecordAPI(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);
            if (ed.AdmittedDate == null)
                return Content(HttpStatusCode.NotFound, Message.SYNC_24H_NOT_FOUND);

            var lastest_ed_in_24h = GetLastestEDIn24H(ed.CustomerId, ed.Id, ed.AdmittedDate);
            if (lastest_ed_in_24h == null)
                return Content(HttpStatusCode.NotFound, Message.SYNC_24H_NOT_FOUND);

            var etr = lastest_ed_in_24h.EmergencyTriageRecord;
            if (etr == null || etr.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.SYNC_24H_NOT_FOUND);

            return Content(HttpStatusCode.OK, new
            {
                etr.Version,
                Datas = etr.EmergencyTriageRecordDatas.Where(e => !e.IsDeleted)
                .Select(e => new { e.Id, e.Code, e.Value })
                .ToList(),
            });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/EmergencyTriageRecords/Delete/{id}")]
        [Permission(Code = "EEMTR4")]
        public IHttpActionResult DeleteEmergencyTriageRecordAPI(Guid id, [FromBody]DeleteMedicalRecord request)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            if (string.IsNullOrWhiteSpace(request.Note)) return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);

            var emer_record = ed.EmergencyRecord;
            if (emer_record.CreatedAt != emer_record.UpdatedAt)
                return Content(HttpStatusCode.BadRequest, Message.DELETE_FORBIDDEN);

            var discharge = ed.DischargeInformation;
            if (discharge.CreatedAt != discharge.UpdatedAt)
                return Content(HttpStatusCode.BadRequest, Message.DELETE_FORBIDDEN);

            var user = GetUser();
            //if (IsBlockAfter24h(ed.CreatedAt) && !HasUnlockPermission(ed.Id, "EDETR", user.Username))
            //    return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);

            var etr = ed.EmergencyTriageRecord;
            bool is_delete = ed.CreatedBy == user.Username;
            if (!is_delete)
                is_delete = user.Username == etr.UpdatedBy;
            
            if (is_delete)
            {
                try
                {
                    unitOfWork.EmergencyTriageRecordRepository.Delete(etr);
                    unitOfWork.EmergencyRecordRepository.Delete(emer_record);
                    unitOfWork.DischargeInformationRepository.Delete(discharge);
                    unitOfWork.EDObservationChartRepository.Delete(ed.ObservationChart);
                    unitOfWork.PatientProgressNoteRepository.Delete(ed.PatientProgressNote);
                    unitOfWork.EIOPreOperativeProcedureHandoverChecklistRepository.Delete(ed.PreOperativeProcedureHandoverChecklist);
                    unitOfWork.EIOSpongeSharpsAndInstrumentsCountsSheetRepository.Delete(ed.SpongeSharpsAndInstrumentsCountsSheet);
                    unitOfWork.EDRepository.Delete(ed);
                    unitOfWork.Commit();
                    setLog(new Log
                    {
                        Action = "DELETE ED",
                        URI = id.ToString(),
                        Name = "DELETE ED",
                        Request = id.ToString(),
                        Reason = request.Note,
                    });
                }
                catch (Exception)
                {
                    return Content(HttpStatusCode.InternalServerError, Message.INTERAL_SERVER_ERROR);
                }
                return Content(HttpStatusCode.OK, Message.SUCCESS);
            }
            return Content(HttpStatusCode.BadRequest, Message.OWNER_FORBIDDEN);
        }

        private void HandleUpdateOrCreateEmergencyTriageRecordData(ED ed, EDEmergencyTriageRecord etr, JToken request_etr_data)
        {
            var is_change = false;
            var etr_datas = etr.EmergencyTriageRecordDatas.Where(e => !e.IsDeleted).ToList();
            var ats_scale = unitOfWork.MasterDataRepository.Find(e => !e.IsDeleted && e.Group != null && e.Group == "ETRATS").Select(e => e.Code).ToList();
            var customer_util = new CustomerUtil(unitOfWork, ed.Customer);
            var visit_util = new VisitAllergy(ed);
            var allergy_dct = new Dictionary<string, string>();
            foreach (var item in request_etr_data)
            {
                var code = item.Value<string>("Code");
                if (code == null) continue;

                var value = item.Value<string>("Value");

                if (Constant.ED_ETR_ALLERGIC_CODE.Contains(code))
                    allergy_dct[Constant.CUSTOMER_ALLERGY_SWITCH[code]] = value;

                var etr_data = GetOrCreateEmergencyTriageRecordData(etr_datas, etr.Id, code, value);
                if(etr_data != null)
                {
                    var change = UpdateEmergencyTriageRecordData(ed, customer_util, etr_data, code, value, ats_scale);
                    is_change = is_change || change;
                }
            }
            visit_util.UpdateAllergy(allergy_dct);
            etr.UpdatedBy = GetUser().Username;
            unitOfWork.EmergencyTriageRecordRepository.Update(etr);
            unitOfWork.Commit();
            if (is_change) PushNotification(ed, "đánh giá ban đầu", "EDETR Change");
        }

        private EDEmergencyTriageRecordData GetOrCreateEmergencyTriageRecordData(List<EDEmergencyTriageRecordData> list_data, Guid etr_id, string code, string value)
        {
            EDEmergencyTriageRecordData data = list_data.FirstOrDefault(e => !e.IsDeleted && !string.IsNullOrEmpty(e.Code) && e.Code == code);
            if (data != null)
                return data;

            if (string.IsNullOrEmpty(value)) return null;

            data = new EDEmergencyTriageRecordData
            {
                EmergencyTriageRecordId = etr_id,
                Code = code,
            };
            unitOfWork.EmergencyTriageRecordDataRepository.Add(data);
            return data;
        }
        private bool UpdateEmergencyTriageRecordData(ED ed, CustomerUtil customer_util, EDEmergencyTriageRecordData etr_data, string code, string value, dynamic ats_scale = null)
        {
            if ("ETRTD01,ETRTD02,ETRTD03".Contains(code) && !Validator.ValidateTimeDateWithoutSecond(value))
                return false;

            if (etr_data.Value == value)
                return false;

            etr_data.Value = value;
            unitOfWork.EmergencyTriageRecordDataRepository.Update(etr_data);

            if (ats_scale != null && IsDataATSScale(ats_scale, code, value))
                UpdateATSScale(ed, code);
            else if (code == "ETRHEIANS" && !string.IsNullOrEmpty(value))
                customer_util.UpdateHeight(value);
            else if (code == "ETRWEIANS" && !string.IsNullOrEmpty(value))
                customer_util.UpdateWeight(value);
            else if (code == "ETRCC0ANS")
                UpdateReason(ed, value);

            if (Constant.ED_FRS_CODE.Contains(code))
                return false;

            return true;
        }
        private bool IsDataATSScale(dynamic ats_scale, string code, string value)
        {
            return ats_scale.Contains(code) && value.Equals("True", StringComparison.CurrentCultureIgnoreCase);
        }
        private void UpdateATSScale(ED ed, string code)
        {
            ed.ATSScale = code;
            unitOfWork.EDRepository.Update(ed);
        }

        private void UpdateVisit(ED ed, EDEmergencyTriageRecord etr, JObject request)
        {
            DateTime request_triage_datetime = DateTime.ParseExact(request["TriageDateTime"]?.ToString(), Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            if(etr.TriageDateTime != request_triage_datetime)
            {
                etr.TriageDateTime = request_triage_datetime;
                if (ed.IsRetailService)
                {
                    var retail = ed.EDAssessmentForRetailServicePatient;
                    retail.TriageDateTime = request_triage_datetime;
                    unitOfWork.EIOAssessmentForRetailServicePatientRepository.Update(retail, is_anonymous: true);
                }
                ed.AdmittedDate = request_triage_datetime;
                UpdateRecordCodeOfCustomer((Guid)ed.CustomerId);
            }

            var bed = request["Bed"]?.ToString();
            etr.Bed = bed;
            ed.Bed = bed;
            
            JToken form_data = request["Datas"];
            // ed.IsHasFallRiskScreening = form_data.FirstOrDefault(d => new List<string> { "ETRDPH1", "ETRDPH2", "ETRDPHA3", "ETRDPN1", "ETRDPN2", "ETRDPN3" }.Contains(d.Value<string>("Code")) && d.Value<string>("Value") == "1") != null;
            
            if (!string.IsNullOrEmpty(request["CovidRiskGroup"]?.ToString()))
            {
                ed.CovidRiskGroup = int.Parse(request["CovidRiskGroup"]?.ToString());
            }

            if (!string.IsNullOrEmpty(request["PrimaryDoctorId"]?.ToString()))
            {
                var user_id = new Guid(request["PrimaryDoctorId"]?.ToString());
                ed.PrimaryDoctorId = user_id;
            }

            var user = GetUser();
            if(ed.CurrentNurseId == null)
                ed.CurrentNurseId = user.Id;

            if (ed.PrimaryNurseId == null)
            {
                var userCreateEtr = GetUserByUsername(etr.CreatedBy);
                ed.PrimaryNurseId = userCreateEtr?.Id;  //thêm điều dưỡng chính theo yêu cầu người tiếp nhận là điều dưỡng chính
            }    

            unitOfWork.EDRepository.Update(ed);
            unitOfWork.EmergencyTriageRecordRepository.Update(etr);
            unitOfWork.Commit();
        }

        private void UpdateReason(ED ed, string reason)
        {
            ed.Reason = reason;
            unitOfWork.EDRepository.Update(ed);
        }

        private ED GetLastestEDIn24H(Guid? customer_id, Guid ed_id, DateTime? ed_admitted_date)
        {
            var time = DateTime.Now.AddDays(-1);
            var ed = unitOfWork.EDRepository.Find(
                e => !e.IsDeleted &&
                e.Id != ed_id &&
                e.CustomerId != null &&
                e.CustomerId == customer_id &&
                e.AdmittedDate != null &&
                e.AdmittedDate >= time &&
                e.AdmittedDate < ed_admitted_date
            ).OrderByDescending(e => e.AdmittedDate).ToList();
            if (ed.Count > 0)
                return ed[0];
            return null;
        }

        private void PushNotification(ED ed, string form_name, string form_frontend)
        {
            var di = ed.DischargeInformation;
            if (di.CreatedAt == di.UpdatedAt)
                return;

            var user = GetUser();
            var spec = ed.Specialty;
            var customer = ed.Customer;

            var noti_creator = new NotificationCreator(
                unitOfWork: unitOfWork,
                from_user: user?.Username,
                to_user: di?.UpdatedBy,
                priority: 7,
                vi_message: $"<b>[ED-{spec?.ViName}]</b> Điều dưỡng <b>{user.Fullname}</b> đã chỉnh sửa <b>{form_name}</b> của bệnh nhân <b>{customer.Fullname}</b>",
                en_message: $"<b>[ED-{spec?.ViName}]</b> Điều dưỡng <b>{user.Fullname}</b> đã chỉnh sửa <b>{form_name}</b> của bệnh nhân <b>{customer.Fullname}</b>",
                spec_id: spec?.Id,
                visit_id: ed.Id,
                group_code: "ED",
                form_frontend: form_frontend
            );
            noti_creator.Create();
        }
        private List<RoomModel> GetRooms(Guid visitId)
        {
            var rooms = (from visit in unitOfWork.EDRepository.AsQueryable().Where(e => !e.IsDeleted && e.Id == visitId)
                         join room in unitOfWork.RoomRepository.AsQueryable()
                         on visit.SiteId equals room.SiteId select 
                         new RoomModel { Id = room.Id, EnName = room.EnName, ViName = room.ViName, Value = room.Floor, Service = room.Service}
                         ).ToList();
            return rooms;
        }
        public class RoomModel
        {
            public Guid Id { get; set; }
            public string EnName { get; set; }
            public string ViName { get; set; }
            public string Value { get; set; }
            public string Service { get; set; } 
        }
    }
}