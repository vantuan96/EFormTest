using DataAccess.Models;
using DataAccess.Models.IPDModel;
using DataAccess.Models.OPDModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers.BaseEIOControllers;
using EForm.Models;
using EForm.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class IPDHandOverCheckListController : EIOHandOverCheckListController
    {
        [HttpGet]
        [Route("api/IPD/HandOverCheckList")]
        [Permission(Code = "IHOCL1")]
        public IHttpActionResult GetHandOverCheckListsAPI([FromUri] HandOverCheckListParameterModel request)
        {
            if (!request.Validate())
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);

            var specialty_id = GetSpecialtyId();
            //var ed_visit = GetEDHandOverVisit(specialty_id);
            //var opd_visit = GetOPDHandOverVisit(specialty_id);
            //var ipd_visit = GetIPDHandOverVisit(specialty_id);
            //var eoc_visit = GetEOCHandOverVisit(specialty_id);
            var handover_visits = GetHandOverVisit(request);
            //ed_visit.Union(opd_visit).Union(ipd_visit).Union(eoc_visit);

            if (request.Search != null)
                handover_visits = FilterBySearch(handover_visits, request.ConvertedSearch);

            if (request.UserAccept != null)
                handover_visits = FilterByUserAccept(handover_visits, request.ConvertedUserAccept);

            if (request.Status != null)
                handover_visits = FilterByStatus(handover_visits, request.Status);

            if (request.StartDate != null && request.EndDate != null)
                handover_visits = FilterByDate(handover_visits, request.ConvertedStartDate, request.ConvertedEndDate);
            else if (request.StartDate != null)
                handover_visits = FilterByStartDate(handover_visits, request.ConvertedStartDate);
            else if (request.EndDate != null)
                handover_visits = FilterByEndDate(handover_visits, request.ConvertedEndDate);

            return Content(HttpStatusCode.OK, DataFormatted(handover_visits, request.PageNumber, request.PageSize));
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/HandOverCheckList/Accept/{id}")]
        [Permission(Code = "IHOCL2")]
        public IHttpActionResult AcceptHandOverCheckListAPI(Guid id, [FromBody] JObject request)
        {
            var obj = GetVisitHandOverCheckList(id);
            if (obj == null)
                return Content(HttpStatusCode.NotFound, Message.HOCL_NOT_FOUND);

            var hand_over_check_list = obj.hocl;
            Guid hand_over_check_list_id = hand_over_check_list.Id;
            if (hand_over_check_list == null || hand_over_check_list.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.HOCL_NOT_FOUND);

            User user = null;

            if (hand_over_check_list.IsUseHandOverCheckList)
            {
                var username = request["username"]?.ToString();
                var password = request["password"]?.ToString();
                user = GetAcceptUser(username, password);
            }
            else
            {
                user = GetUser();
            }

            if (user == null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);

            var is_create = !hand_over_check_list.IsAcceptPhysician && !hand_over_check_list.IsAcceptNurse;

            var specialties = GetListSpecicalty(user);
            var positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName);
            if (!hand_over_check_list.IsAcceptNurse && positions.Contains("Doctor"))
            {
                return Content(HttpStatusCode.BadRequest, Message.OWNER_FORBIDDEN);
            }
            if (
                positions.Contains("Doctor") &&
                !hand_over_check_list.IsAcceptPhysician
            )
            {
                hand_over_check_list.IsAcceptPhysician = true;
                hand_over_check_list.ReceivingPhysicianId = user.Id;
                hand_over_check_list.PhysicianAcceptTime = DateTime.Now;
            }
            else if (
                positions.Contains("Nurse") &&
                !hand_over_check_list.IsAcceptNurse
            )
            {
                hand_over_check_list.IsAcceptNurse = true;
                hand_over_check_list.ReceivingNurseId = user.Id;
                hand_over_check_list.NurseAcceptTime = DateTime.Now;
            }
            else
                return Content(HttpStatusCode.BadRequest, Message.OWNER_FORBIDDEN);

            var visit = obj.visit;

            Guid? new_visit_id = null;
            var visit_type_group_hand_over_code = unitOfWork.SpecialtyRepository.GetById(
                (Guid)hand_over_check_list.HandOverUnitPhysicianId
            ).VisitTypeGroup.Code;
           
            if (!is_create)
            {
                UpdateHandOverCheckList(visit_type_group_hand_over_code, hand_over_check_list, false);
                unitOfWork.Commit();
                return Content(HttpStatusCode.OK, new { Id = new_visit_id });
            }

            var is_create_error = IsCreateVisitError(visit);
            if (is_create_error != null)
                return Content(HttpStatusCode.BadRequest, is_create_error);

            UpdateHandOverCheckList(visit_type_group_hand_over_code, hand_over_check_list, false);
            if (visit_type_group_hand_over_code.Equals("OPD") && visit.GroupId != null)
                UpdateGroupOPDVisit(visit.Id, visit.GroupId);

            new_visit_id = CreateNewIPDFromHandOver((Guid)visit.CustomerId, hand_over_check_list_id);

            unitOfWork.Commit();
            return Content(HttpStatusCode.OK, new { Id = new_visit_id });
        }

        [HttpGet]
        [Route("api/IPD/HandOverCheckList/{id}")]
        [Permission(Code = "IHOCL3")]
        public IHttpActionResult GetHandOverCheckListAPI(Guid id)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            if (ipd.IPDMedicalRecordId == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_MERE_NOT_FOUND);

            var customer = ipd.Customer;
            var hand_over_check_list = ipd.HandOverCheckList;
            if (hand_over_check_list == null || hand_over_check_list.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.HOCL_NOT_FOUND);

            var medical_record = ipd.IPDMedicalRecord;
            var part_3 = medical_record.IPDMedicalRecordPart3;
            var part_3_datas = part_3?.IPDMedicalRecordPart3Datas.Where(e => !e.IsDeleted).ToList();
            var diagnosis = part_3_datas.FirstOrDefault(e => e.Code == "IPDMRPECDBCANS")?.Value;
            var icd_diagnosis = part_3_datas.FirstOrDefault(e => e.Code == "IPDMRPEICDCANS")?.Value;
            var co_morbidities = part_3_datas.FirstOrDefault(e => e.Code == "IPDMRPECDKTANS")?.Value;
            var icd_co_morbidities = part_3_datas.FirstOrDefault(e => e.Code == "IPDMRPEICDPANS")?.Value;
            var physic_user = ipd.PrimaryDoctor;

            return Content(HttpStatusCode.OK, new
            {
                IsLocked = IPDIsBlock(ipd, Constant.IPDFormCode.BienBanBangKiemNguoiBenhChuyenKhoa),
                ipd.RecordCode,
                hand_over_check_list.Id,
                IPDId = ipd.Id,
                Customer = new { customer.Fullname, customer.DateOfBirth, customer.PID},
                Physician = physic_user?.Fullname,
                Diagnosis = diagnosis,
                ICDDiagnosis = icd_diagnosis,
                CoMorbidities = co_morbidities,
                ICDCoMorbidities = icd_co_morbidities,
                HandOverTimeNurse = hand_over_check_list.HandOverTimeNurse?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                hand_over_check_list.ReasonForTransfer,
                HandOverNurse = hand_over_check_list.HandOverNurse?.Fullname,
                HandOverUnitNurse = hand_over_check_list.HandOverUnitNurse?.ViName,
                ReceivingNurse = hand_over_check_list.ReceivingNurse?.Fullname,
                ReceivingUnitNurse = hand_over_check_list.ReceivingUnitNurse?.ViName,
                Datas = hand_over_check_list.HandOverCheckListDatas.Where(e => !e.IsDeleted).Select(hovcld => new { hovcld.Id, hovcld.Code, hovcld.Value, hovcld.EnValue }).ToList(),
                IsUseHandOverCheckList = ipd.HandOverCheckList.IsUseHandOverCheckList,
                VisitCode = ipd.VisitCode,
                HandOverPhysician = hand_over_check_list.HandOverPhysician,
                HandOverTimePhysician = hand_over_check_list.HandOverTimePhysician,
                HandOverUnitPhysician = hand_over_check_list.HandOverUnitPhysician,
                NurseAcceptTime = hand_over_check_list.NurseAcceptTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                ipd.Version,
                UserNameHandOverNurse = hand_over_check_list.HandOverNurse?.Username,
                UserNameReceivingNurse = hand_over_check_list.ReceivingNurse?.Username
            });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/HandOverCheckList/{id}")]
        [Permission(Code = "IHOCL4")]
        public IHttpActionResult UpdateHandOverCheckListAPI(Guid id, [FromBody] JObject request)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);
            if (IPDIsBlock(ipd, Constant.IPDFormCode.BienBanBangKiemNguoiBenhChuyenKhoa))
            {
                return Content(HttpStatusCode.BadRequest, Message.FORM_IS_LOCKED);
            }

            var hand_over_check_list = ipd.HandOverCheckList;
            if (hand_over_check_list == null || hand_over_check_list.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.HOCL_NOT_FOUND);

            //var user = GetUser();
            //if (IsBlockAfter24h(ed.CreatedAt) && !HasUnlockPermission(ed.Id, "EDHOC", user.Username))
            //    return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);

            UpdateHandOverCheckList(hand_over_check_list, request);

            HandleUpdateOrCreateHandOverCheckList(hand_over_check_list, request["Datas"]);
            // ipd.DischargeDate = hand_over_check_list.HandOverTimeNurse;
            // unitOfWork.Commit();
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }


        [HttpGet]
        [Route("api/IPD/PatientHandOverRecord/{id}")]
        [Permission(Code = "IHOCL5")]
        public IHttpActionResult GetPatientHandOverRecord(Guid id)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            if (ipd.IPDMedicalRecordId == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_MERE_NOT_FOUND);

            var customer = ipd.Customer;
            var gender = new CustomerUtil(customer).GetGender();

            var hocl = ipd.HandOverCheckList;
            if (hocl == null || hocl.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.HOCL_NOT_FOUND);

            var medical_record = ipd.IPDMedicalRecord;
            var medical_record_datas = medical_record.IPDMedicalRecordDatas.Where(e => !e.IsDeleted).ToList();

            var receiving_hospital = medical_record_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDMRPTNTBVANS")?.Value;

            var time_of_transfer = medical_record_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDMRPTCHVHANS")?.Value;
            var receiving_person = medical_record_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDMRPTNDLHANS")?.Value;
            var transportation_method = medical_record_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDMRPTPTVCANS")?.Value;
            var escort_person = medical_record_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDMRPTNGDDANS")?.Value;

            var part_2 = medical_record.IPDMedicalRecordPart2;
            var part_2_datas = part_2?.IPDMedicalRecordPart2Datas.Where(e => !e.IsDeleted).ToList();
            var reasons_for_admission = part_2_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDMRPTLDVVANS")?.Value;

            var part_3 = medical_record.IPDMedicalRecordPart3;
            var part_3_datas = part_3?.IPDMedicalRecordPart3Datas.Where(e => !e.IsDeleted).ToList();
            var clinical_evolution = part_3_datas.FirstOrDefault(e => e.Code == "IPDMRPEQTBLANS")?.Value;
            var diagnosis = part_3_datas.FirstOrDefault(e => e.Code == "IPDMRPECDBCANS")?.Value;
            var icd_diagnosis = part_3_datas.FirstOrDefault(e => e.Code == "IPDMRPEICDCANS")?.Value;
            var co_morbidities = part_3_datas.FirstOrDefault(e => e.Code == "IPDMRPECDKTANS")?.Value;
            var icd_co_morbidities = part_3_datas.FirstOrDefault(e => e.Code == "IPDMRPEICDPANS")?.Value;
            var treatments_and_procedures ="\nPhương pháp điều trị:" + GetTreatmentsLast(id, part_3.Id, part_3_datas);
            var significant_medications = part_3_datas.FirstOrDefault(e => e.Code == "IPDMRPETCDDANS")?.Value;
            var current_condition = part_3_datas.FirstOrDefault(e => e.Code == "IPDMRPETTNBANS")?.Value;
            var recommendation = part_3_datas.FirstOrDefault(e => e.Code == "IPDMRPEHDTVANS")?.Value;

            var site = GetSite();
            var department = string.Format("Khoa {0}", GetSpecialty().ViName);

            return Ok(new
            {
                ipd.RecordCode,
                Department = "Khoa cấp cứu",
                customer.Fullname,
                DateOfBirth = customer.DateOfBirth?.ToString(Constant.DATE_FORMAT),
                Gender = gender,
                customer.Nationality,
                customer.PID,
                customer.Address,
                site?.Location,
                Site = site?.Name,
                site?.Province,
                ReasonsForTransfer = hocl.ReasonForTransfer,
                ReasonsForAdmission = reasons_for_admission,
                ClinicalEvolution = clinical_evolution,
                Diagnosis = diagnosis,
                ICDDiagnosis = icd_diagnosis,
                CoMorbidities = co_morbidities,
                ICDCoMorbidities = icd_co_morbidities,
                TreatmentsAndProcedures = treatments_and_procedures,
                SignificantMedications = significant_medications,
                CurrentCondition = current_condition,
                Recommendation = recommendation,
                Date = DateTime.Now.ToString(Constant.DATE_FORMAT),
                HandOverTimePhysician = hocl.HandOverTimePhysician?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                HandOverPhysician = hocl.HandOverPhysician?.Fullname,
                HandOverUnitPhysician = hocl.HandOverUnitPhysician?.ViName,
                ReceivingPhysician = hocl.ReceivingPhysician?.Fullname,
                ReceivingUnitPhysician = hocl.ReceivingUnitPhysician?.ViName,
                IsLocked = IPDIsBlock(ipd, Constant.IPDFormCode.BienBanBangKiemNguoiBenhChuyenKhoa),
                IsUseHandOverCheckList = ipd.HandOverCheckList.IsUseHandOverCheckList,
                PhysicianAcceptTime = hocl.PhysicianAcceptTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                ipd.Version,
                UserNameHandOverPhysician = hocl.HandOverPhysician?.Username,
                UserNameReceivingPhysician = hocl.ReceivingPhysician?.Username
            });
        }


        [HttpGet]
        [Route("api/IPD/HandOverCheckList/Count")]
        [Permission(Code = "IHOCL6")]
        public IHttpActionResult CountHandOverCheckListAPI()
        {
            var specialty_id = GetSpecialtyId();
            var cnt_ed = CountEDHandOverVisit(specialty_id);
            var cnt_opd = CountOPDHandOverVisit(specialty_id);
            var cnt_ipd = CountIPDHandOverVisit(specialty_id);
            return Content(HttpStatusCode.OK, cnt_opd + cnt_ed + cnt_ipd);
        }

        private void HandleUpdateOrCreateHandOverCheckList(IPDHandOverCheckList hand_over_check_list, JToken request_datas)
        {
            var user = GetUser();
            var hocl_datas = hand_over_check_list.HandOverCheckListDatas.Where(e => !e.IsDeleted).ToList();
            foreach (var item in request_datas)
            {
                var code = item.Value<string>("Code");
                if (code == null)
                    continue;
                var value = item.Value<string>("Value");
                var hocl = hocl_datas.FirstOrDefault(e => e.Code == code);
                if (hocl == null)
                    CreateHandOverCheckListData(hand_over_check_list.Id, code, value);
                else if (hocl.Value != value)
                    UpdateHandOverCheckListData(hocl, value);
            }
            hand_over_check_list.HandOverNurseId = user.Id;
            hand_over_check_list.UpdatedBy = user.Username;
            unitOfWork.IPDHandOverCheckListRepository.Update(hand_over_check_list);
            unitOfWork.Commit();
        }

        private void UpdateHandOverCheckList(IPDHandOverCheckList hand_over_check_list, JObject request)
        {
            DateTime request_hand_over_datetime = DateTime.ParseExact(request["HandOverTimeNurse"]?.ToString(), Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            if (request_hand_over_datetime != null && hand_over_check_list.HandOverTimeNurse != request_hand_over_datetime)
            {
                hand_over_check_list.HandOverTimeNurse = request_hand_over_datetime;
                hand_over_check_list.HandOverNurseId = GetUser()?.Id;
                unitOfWork.IPDHandOverCheckListRepository.Update(hand_over_check_list);
                unitOfWork.Commit();
            }
        }

        private void CreateHandOverCheckListData(Guid hocl_id, string code, string value)
        {
            IPDHandOverCheckListData new_hocl_data = new IPDHandOverCheckListData
            {
                HandOverCheckListId = hocl_id,
                Code = code,
                Value = value
            };
            unitOfWork.IPDHandOverCheckListDataRepository.Add(new_hocl_data);
        }

        private void UpdateHandOverCheckListData(IPDHandOverCheckListData hocl_data, string value)
        {
            hocl_data.Value = value;
            unitOfWork.IPDHandOverCheckListDataRepository.Update(hocl_data);
        }
    }
}
