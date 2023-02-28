using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web.Http;
using DataAccess.Models.EDModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers;
using Newtonsoft.Json.Linq;

namespace EForm.Controllers.EDControllers
{
    [SessionAuthorize]
    public class EDEmergencyRecordsController : BaseEDApiController
    {
        [HttpGet]
        [Route("api/ED/EmergencyRecords/{id}")]
        [Permission(Code = "EEMRE1")]
        public IHttpActionResult GetEmergencyRecords(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var emer_record = ed.EmergencyRecord;
            if (emer_record == null || emer_record.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.ED_ER0_NOT_FOUND);

            return Ok(new
            {
                ed.RecordCode,
                emer_record.Id,
                EDId = ed.Id,
                TimeSeen = emer_record.TimeSeen?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                Datas = emer_record.EmergencyRecordDatas.Where(e=>!e.IsDeleted)
                .Select(etrd => new { emer_record.Id, etrd.Code, etrd.Value })
                .ToList(),
                IsUseHandOverCheckList = ed.HandOverCheckList?.IsUseHandOverCheckList,
                ed.Version,
                emer_record.CreatedAt,
                emer_record.CreatedBy,
                emer_record.UpdatedAt,
                emer_record.UpdatedBy
            });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/EmergencyRecords/{id}")]
        [Permission(Code = "EEMRE2")]
        public IHttpActionResult UpdateEmergencyRecord(Guid id, JObject request)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var emer_record = ed.EmergencyRecord;
            if (emer_record == null)
                return Content(HttpStatusCode.NotFound, Message.ED_ER0_NOT_FOUND);

            var user = GetUser();
            //if (IsBlockAfter24h(ed.CreatedAt) && !HasUnlockPermission(ed.Id, "EDER0", user.Username))
            //    return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);

            //if (!IsUserCreateFormAuto(user.Username, emer_record.UpdatedBy, emer_record.CreatedAt, emer_record.UpdatedAt))
            //    return Content(HttpStatusCode.BadRequest, Message.OWNER_FORBIDDEN);

            if (IsNew(emer_record.CreatedAt, emer_record.UpdatedAt))
                UpdateOwner(emer_record);

            if (ed.CurrentDoctorId == null)
                UpdateCurrentDoctor(ed, user.Id);

            HandleUpdateTimeSeen(emer_record, request["TimeSeen"]?.ToString());

            HandleUpdateOrCreateEmergencyRecordData(emer_record, request["Datas"]);

            if (user.Username != emer_record.CreatedBy)
                CreatedEDChangingNotification(user, emer_record.CreatedBy, ed, "Bệnh án cấp cứu", "ER0");

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        private void UpdateOwner(EDEmergencyRecord emer_record)
        {
            emer_record.CreatedBy = GetUser()?.Username;
            unitOfWork.EmergencyRecordRepository.Update(emer_record);
            unitOfWork.Commit();
        }

        private void HandleUpdateOrCreateEmergencyRecordData(EDEmergencyRecord emer_record, JToken request_er_data)
        {
            var er_datas = emer_record.EmergencyRecordDatas.Where(e => !e.IsDeleted).ToList();
            foreach (var item in request_er_data)
            {
                if (item.Value<string>("Code") == null)continue;

                var er_data = er_datas.FirstOrDefault(e => e.Code == item.Value<string>("Code"));
                if (er_data == null)
                    CreateEmergencyRecordData(emer_record.Id, item);
                else if (er_data.Value != item.Value<string>("Value"))
                    UpdateEmergencyRecordData(er_data, item);

                emer_record.UpdatedBy = GetUser().Username;
                unitOfWork.EmergencyRecordRepository.Update(emer_record);
            }
            unitOfWork.Commit();
        }

        private void HandleUpdateTimeSeen(EDEmergencyRecord emer_record, string time_seen)
        {
            // String was not recognized as a valid DateTime
            bool isDateTime = DateTime.TryParseExact(time_seen, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null, DateTimeStyles.None, out DateTime request_time_seen);
            // DateTime request_time_seen = DateTime.ParseExact(time_seen, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            if (!isDateTime)
                return;
            if (request_time_seen != null && emer_record.TimeSeen != request_time_seen)
            {
                emer_record.TimeSeen = request_time_seen;
                unitOfWork.EmergencyRecordRepository.Update(emer_record);
                unitOfWork.Commit();
            }
        }

        private void UpdateEmergencyRecordData(EDEmergencyRecordData er_data, dynamic item)
        {
            er_data.Value = item.Value<string>("Value");
            unitOfWork.EmergencyRecordDataRepository.Update(er_data);
        }

        private void CreateEmergencyRecordData(Guid emer_record_id, dynamic item)
        {
            EDEmergencyRecordData new_er_data = new EDEmergencyRecordData
            {
                EmergencyRecordId = emer_record_id,
                Code = item.Value<string>("Code"),
                Value = item.Value<string>("Value")
            };
            unitOfWork.EmergencyRecordDataRepository.Add(new_er_data);
        }

        private void UpdateCurrentDoctor(ED ed, Guid user_id)
        {
            ed.CurrentDoctorId = user_id;
            unitOfWork.EDRepository.Update(ed);
            unitOfWork.Commit();
        }
    }
}