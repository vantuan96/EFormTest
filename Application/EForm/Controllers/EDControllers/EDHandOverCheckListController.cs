using DataAccess.Models;
using DataAccess.Models.EDModel;
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

namespace EForm.Controllers.EDControllers
{
    [SessionAuthorize]
    public class EDHandOverCheckListController : EIOHandOverCheckListController
    {
        [HttpGet]
        [Route("api/ED/HandOverCheckList")]
        [Permission(Code = "EHOCL1")]
        public IHttpActionResult GetHandOverCheckListsAPI([FromUri]HandOverCheckListParameterModel request)
        {
            if (!request.Validate())
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);

            var handover_visits = GetHandOverVisit(request);
            var a = handover_visits.ToList();
            return Content(HttpStatusCode.OK, DataFormatted(handover_visits, request.PageNumber, request.PageSize));
        }


        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/HandOverCheckList/Accept/{id}")]
        [Permission(Code = "EHOCL2")]
        public IHttpActionResult AcceptHandOverCheckListAPI(Guid id, [FromBody]JObject request)
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
                !hand_over_check_list.IsAcceptPhysician &&
                specialties.Contains(hand_over_check_list.ReceivingUnitPhysicianId)
            )
            {
                hand_over_check_list.IsAcceptPhysician = true;
                hand_over_check_list.ReceivingPhysicianId = user.Id;
                hand_over_check_list.PhysicianAcceptTime = DateTime.Now;
            }
            else if (
                positions.Contains("Nurse") &&
                !hand_over_check_list.IsAcceptNurse &&
                specialties.Contains(hand_over_check_list.ReceivingUnitNurseId)
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

            new_visit_id = CreateNewEDFromHandOver((Guid)visit.CustomerId, hand_over_check_list_id);
            unitOfWork.Commit();
            return Content(HttpStatusCode.OK, new { Id = new_visit_id });
        }


        [HttpGet]
        [Route("api/ED/HandOverCheckList/{id}")]
        [Permission(Code = "EHOCL3")]
        public IHttpActionResult GetHandOverCheckListAPI(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var customer = ed.Customer;
            var hand_over_check_list = ed.HandOverCheckList;
            if (hand_over_check_list == null || hand_over_check_list.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.HOCL_NOT_FOUND);

            var discharge_info = ed.DischargeInformation;
            if (discharge_info == null || discharge_info.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.ED_DI0_NOT_FOUND);

            var diagnosis = discharge_info.DischargeInformationDatas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0DIAANS");
            var physic_user = unitOfWork.UserRepository.FirstOrDefault(u => !u.IsDeleted && u.Username == discharge_info.UpdatedBy);

            return Ok(new
            {
                ed.RecordCode,
                hand_over_check_list.Id,
                EDId = ed.Id,
                Customer = new { customer.Fullname, customer.DateOfBirth, customer.PID, ed.VisitCode },
                Diagnosis = diagnosis?.Value,
                Physician = physic_user?.Fullname,
                HandOverTimeNurse = hand_over_check_list.HandOverTimeNurse?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                hand_over_check_list.ReasonForTransfer,
                HandOverNurse = hand_over_check_list.HandOverNurse?.Fullname,
                HandOverUnitNurse = hand_over_check_list.HandOverUnitNurse?.ViName ,
                ReceivingNurse = hand_over_check_list.ReceivingNurse?.Fullname,
                ReceivingUnitNurse = hand_over_check_list.ReceivingUnitNurse?.ViName,
                Datas = hand_over_check_list.HandOverCheckListDatas.Where(e=> !e.IsDeleted).Select(hovcld => new { hovcld.Id, hovcld.Code, hovcld.Value, hovcld.EnValue }).ToList(),
                IsUseHandOverCheckList = hand_over_check_list.IsUseHandOverCheckList,
                HandOverPhysician = hand_over_check_list.HandOverPhysician,
                HandOverTimePhysician = hand_over_check_list.HandOverTimePhysician,
                HandOverUnitPhysician = hand_over_check_list.HandOverUnitPhysician,
                NurseAcceptTime = hand_over_check_list.NurseAcceptTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                ed.Version,
                UserNameHandOverNurse = hand_over_check_list.HandOverNurse?.Username,
                UserNameReceivingNurse = hand_over_check_list.ReceivingNurse?.Username
            });
        }


        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/HandOverCheckList/{id}")]
        [Permission(Code = "EHOCL4")]
        public IHttpActionResult UpdateHandOverCheckListAPI(Guid id, [FromBody]JObject request)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var hand_over_check_list = ed.HandOverCheckList;
            if (hand_over_check_list == null || hand_over_check_list.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.HOCL_NOT_FOUND);

            //var user = GetUser();
            //if (IsBlockAfter24h(ed.CreatedAt) && !HasUnlockPermission(ed.Id, "EDHOC", user.Username))
            //    return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);

            UpdateHandOverCheckList(hand_over_check_list, request);

            HandleUpdateOrCreateHandOverCheckList(hand_over_check_list, request["Datas"]);
            
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpGet]
        [Route("api/ED/PatientHandOverRecord/{id}")]
        [Permission(Code = "EHOCL5")]
        public IHttpActionResult GetPatientHandOverRecord(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var customer = ed.Customer;
            var gender = new CustomerUtil(customer).GetGender();

            var hocl = ed.HandOverCheckList;
            if (hocl == null || hocl.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.HOCL_NOT_FOUND);

            var etr = ed.EmergencyTriageRecord;
            var chief_complain = etr.EmergencyTriageRecordDatas.FirstOrDefault(e => !e.IsDeleted && e.Code == "ETRCC0ANS")?.Value;

            var emer_record = ed.EmergencyRecord;
            var past_health_history = emer_record.EmergencyRecordDatas.FirstOrDefault(e => !e.IsDeleted && e.Code == "ER0PHHANS")?.Value;
            var history = emer_record.EmergencyRecordDatas.FirstOrDefault(e => !e.IsDeleted && e.Code == "ER0HISANS")?.Value;
            var assessment = new EmergencyRecordAssessment(emer_record.Id).GetList();

            var discharge_info = ed.DischargeInformation;
            var reson_for_transfer = discharge_info.DischargeInformationDatas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0RFAANS")?.Value;
            var result_of_paraclinical_tests = discharge_info.DischargeInformationDatas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0RPTANS2")?.Value;
            var diagnosis = discharge_info.DischargeInformationDatas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0DIAANS")?.Value;
            var treatment_procedures = discharge_info.DischargeInformationDatas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0TAPANS")?.Value;
            var significant_medications = discharge_info.DischargeInformationDatas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0SM0ANS2")?.Value;
            var current_status = discharge_info.DischargeInformationDatas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0CS0ANS")?.Value;
            var followup_care_plan = discharge_info.DischargeInformationDatas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0FCPANS")?.Value;

            var site = GetSite();
            return Ok(new
            {
                ed.RecordCode,
                Department = "Khoa cấp cứu",
                customer.Fullname,
                DateOfBirth = customer.DateOfBirth?.ToString(Constant.DATE_FORMAT),
                Gender = gender,
                customer.Nationality,
                customer.PID,
                customer.Address,
                AdmittedDate = ed.AdmittedDate.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                DischargeDate = discharge_info.AssessmentAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                hocl.ReasonForTransfer,
                ChiefComplain = chief_complain,
                PastHealthHistory = past_health_history,
                History = history,
                Assessment = assessment,
                ResultOfParaclinicalTests = result_of_paraclinical_tests,
                Diagnosis = diagnosis,
                TreatmentAndProcedures = treatment_procedures,
                SignificantMedications = significant_medications,
                CurrentStatus = current_status,
                FollowupCarePlan = followup_care_plan,
                site?.Location,
                Site = site?.Name,
                site?.Province,
                Date = DateTime.Now.ToString(Constant.DATE_FORMAT),
                HandOverTimePhysician = hocl.HandOverTimePhysician?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                HandOverPhysician = hocl.HandOverPhysician?.Fullname,
                HandOverUnitPhysician = hocl.HandOverUnitPhysician?.ViName,
                ReceivingPhysician = hocl.ReceivingPhysician?.Fullname,
                ReceivingUnitPhysician = hocl.ReceivingUnitPhysician?.ViName,
                IsUseHandOverCheckList = hocl.IsUseHandOverCheckList,
                PhysicianAcceptTime = hocl.PhysicianAcceptTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                ed.Version,
                UserNameHandOverPhysician = hocl.HandOverPhysician?.Username,
                UserNameReceivingPhysician = hocl.ReceivingPhysician?.Username
            });
        }


        [HttpGet]
        [Route("api/ED/HandOverCheckList/Count")]
        [Permission(Code = "EHOCL6")]
        public IHttpActionResult CountHandOverCheckListAPI()
        {
            var specialty_id = GetSpecialtyId();
            var cnt_opd = CountOPDHandOverVisit(specialty_id);
            var cnt_ipd = CountIPDHandOverVisit(specialty_id);
            var cnt_eoc = CountEOCHandOverVisit(specialty_id);
            return Content(HttpStatusCode.OK, cnt_opd + cnt_ipd + cnt_eoc);
        }

        private void HandleUpdateOrCreateHandOverCheckList(EDHandOverCheckList hand_over_check_list, JToken request_datas)
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
            unitOfWork.HandOverCheckListRepository.Update(hand_over_check_list);
            unitOfWork.Commit();
        }

        private void UpdateHandOverCheckList(EDHandOverCheckList hand_over_check_list, JObject request)
        {
            DateTime request_hand_over_datetime = DateTime.ParseExact(request["HandOverTimeNurse"]?.ToString(), Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            if(request_hand_over_datetime != null && hand_over_check_list.HandOverTimeNurse != request_hand_over_datetime)
            {
                hand_over_check_list.HandOverTimeNurse = request_hand_over_datetime;
                hand_over_check_list.HandOverNurseId = GetUser()?.Id;
                unitOfWork.HandOverCheckListRepository.Update(hand_over_check_list);
                unitOfWork.Commit();
            }
        }

        private void CreateHandOverCheckListData(Guid hocl_id, string code, string value)
        {
            EDHandOverCheckListData new_hocl_data = new EDHandOverCheckListData
            {
                HandOverCheckListId = hocl_id,
                Code = code,
                Value = value
            };
            unitOfWork.HandOverCheckListDataRepository.Add(new_hocl_data);
        }

        private void UpdateHandOverCheckListData(EDHandOverCheckListData hocl_data, string value)
        {
            hocl_data.Value = value;
            unitOfWork.HandOverCheckListDataRepository.Update(hocl_data);
        }

    }
}
