using DataAccess.Models;
using DataAccess.Models.OPDModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers.BaseEIOControllers;
using EForm.Models;
using EForm.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.OPDControllers
{
    [SessionAuthorize]
    public class OPDHandOverCheckListController : EIOHandOverCheckListController
    {
        [HttpGet]
        [Route("api/OPD/HandOverCheckList")]
        [Permission(Code = "OHOCL1")]
        public IHttpActionResult GetHandOverCheckListsAPI([FromUri]HandOverCheckListParameterModel request)
        {
            if (!request.Validate())
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);

            var handover_visits = GetHandOverVisit(request);

            return Content(HttpStatusCode.OK, DataFormatted(handover_visits, request.PageNumber, request.PageSize));
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/HandOverCheckList/Accept/{id}")]
        [Permission(Code = "OHOCL2")]
        public IHttpActionResult AcceptHandOverCheckListAPI(Guid id, [FromBody]JObject request)
        {
            var obj = GetVisitHandOverCheckList(id);
            if (obj == null)
                return Content(HttpStatusCode.NotFound, Message.HOCL_NOT_FOUND);

            var hand_over_check_list = obj.hocl;
            Guid hand_over_check_list_id = hand_over_check_list.Id;
            if (hand_over_check_list == null || hand_over_check_list.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.HOCL_NOT_FOUND);
            if (GetSpecialty().Id != hand_over_check_list.ReceivingUnitNurseId)
                return Content(HttpStatusCode.BadRequest, new {Viname = "Sai khoa tiếp nhận"});
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
            var isCheckPreAnes = CheckPreAnes(id);
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
                if (!isCheckPreAnes)
                {
                    hand_over_check_list.PhysicianAcceptTime = DateTime.Now;
                }
                
            }
            else if (
                positions.Contains("Nurse") &&
                !hand_over_check_list.IsAcceptNurse &&
                specialties.Contains(hand_over_check_list.ReceivingUnitNurseId)
            )
            {
                hand_over_check_list.IsAcceptNurse = true;
                hand_over_check_list.ReceivingNurseId = user.Id;
                if (!isCheckPreAnes)
                {
                    hand_over_check_list.NurseAcceptTime = DateTime.Now;
                }               
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
                UpdateHandOverCheckList(visit_type_group_hand_over_code, hand_over_check_list, isCheckPreAnes);
                unitOfWork.Commit();
                return Content(HttpStatusCode.OK, new { Id = new_visit_id });
            }   
            var is_create_error = IsCreateVisitError(visit);
            if (is_create_error != null && !isCheckPreAnes)
                return Content(HttpStatusCode.BadRequest, is_create_error);

            UpdateHandOverCheckList(visit_type_group_hand_over_code, hand_over_check_list, isCheckPreAnes);

            new_visit_id = CreateNewOPDFromHandOver((Guid)visit.CustomerId, hand_over_check_list_id);
            if (isCheckPreAnes)
            {
                var newopd = unitOfWork.OPDRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == new_visit_id);
                if(newopd != null)
                {
                    newopd.VisitCode = visit?.VisitCode;
                    newopd.TransferFromId = hand_over_check_list.Id;
                    unitOfWork.OPDRepository.Update(newopd);
                }
            }
            unitOfWork.Commit();
            
            return Content(HttpStatusCode.OK, new { Id = new_visit_id });
        }
        private bool CheckPreAnes(Guid id)
        {
            var opdhovcl = unitOfWork.OPDHandOverCheckListRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == id);
            var opdpreaneshovcl = unitOfWork.OPDPreAnesthesiaHandOverCheckListRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == id);
            if (opdhovcl == null && opdpreaneshovcl != null) return true;
            return false;
        }

        [HttpGet]
        [Route("api/OPD/HandOverCheckList/{id}")]
        [Permission(Code = "OHOCL3")]
        public IHttpActionResult GetHandOverCheckListAPI(Guid id)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var customer = opd.Customer;
            var hand_over_check_list = opd.OPDHandOverCheckList;
            if (hand_over_check_list == null || hand_over_check_list.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.HOCL_NOT_FOUND);

            var oen = opd?.OPDOutpatientExaminationNote;
            if (oen == null || oen.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.OPD_OEN_NOT_FOUND);
            
            var oen_datas = oen?.OPDOutpatientExaminationNoteDatas.Where(e => !e.IsDeleted)?.ToList();
            var physic_user = opd.PrimaryDoctor;

            return Ok(new
            {
                opd.RecordCode,
                opd.IsTelehealth,
                hand_over_check_list.Id,
                OPDId = opd.Id,
                Customer = new { customer.Fullname, customer.DateOfBirth, customer.PID },
                Diagnosis = GetDiagnosisOPD(oen_datas, opd.Version, "PATIENT HANDOVER RECORD"),
                Physician = physic_user?.Fullname,
                HandOverTimeNurse = hand_over_check_list.HandOverTimeNurse?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                hand_over_check_list.ReasonForTransfer,
                HandOverNurse = hand_over_check_list.HandOverNurse?.Fullname,
                HandOverUnitNurse = hand_over_check_list.HandOverUnitNurse?.ViName,
                ReceivingNurse = hand_over_check_list.ReceivingNurse?.Fullname,
                ReceivingUnitNurse = hand_over_check_list.ReceivingUnitNurse?.ViName,
                Datas = hand_over_check_list.OPDHandOverCheckListDatas.Where(e => !e.IsDeleted).Select(hovcld => new { hovcld.Id, hovcld.Code, hovcld.Value, hovcld.EnValue }).ToList(),
                IsUseHandOverCheckList = hand_over_check_list.IsUseHandOverCheckList,
                VisitCode = opd.VisitCode,
                HandOverPhysician = hand_over_check_list.HandOverPhysician,
                HandOverTimePhysician = hand_over_check_list.HandOverTimePhysician,
                HandOverUnitPhysician = hand_over_check_list.HandOverUnitPhysician,
                NurseAcceptTime = hand_over_check_list.NurseAcceptTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                opd.Version,
                UserNameHandOverNurse = hand_over_check_list.HandOverNurse?.Username,
                UserNameReceivingNurse = hand_over_check_list.ReceivingNurse?.Username,
                IsLocked = Is24hLocked(opd.CreatedAt, opd.Id, "OPDHOC", GetUser()?.Username, hand_over_check_list.Id)
            });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/HandOverCheckList/{id}")]
        [Permission(Code = "OHOCL4")]
        public IHttpActionResult UpdateHandOverCheckListAPI(Guid id, [FromBody]JObject request)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var hand_over_check_list = opd.OPDHandOverCheckList;
            if (hand_over_check_list == null || hand_over_check_list.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.HOCL_NOT_FOUND);

            var user = GetUser();
            if (IsBlockAfter24h(opd.CreatedAt) && !HasUnlockPermission(opd.Id, "OPDHOC", user.Username))
                return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);

            if(hand_over_check_list.HandOverNurseId != null && hand_over_check_list.HandOverNurseId != user.Id)
                return Content(HttpStatusCode.BadRequest, Message.OWNER_FORBIDDEN);

            UpdateHandOverCheckList(hand_over_check_list, request);
            HandleUpdateHandOverCheckListData(hand_over_check_list, request["Datas"]);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpGet]
        [Route("api/OPD/PatientHandOverRecord/{id}")]
        [Permission(Code = "OHOCL5")]
        public IHttpActionResult GetPatientHandOverRecord(Guid id)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var customer = opd.Customer;
            var gender = new CustomerUtil(customer).GetGender();

            var hocl = opd.OPDHandOverCheckList;
            if (hocl == null || hocl.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.HOCL_NOT_FOUND);

            var clinic = opd.Clinic;
            var oen = opd.OPDOutpatientExaminationNote;
            var oen_datas = oen.OPDOutpatientExaminationNoteDatas.Where(e => !e.IsDeleted).ToList();
            var chief_complain = oen_datas.FirstOrDefault(e => e.Code == "OPDOENCC0ANS")?.Value;
            var hisory_of_present_illness = oen_datas.FirstOrDefault(e => e.Code == "OPDOENHPIANS")?.Value;

            string clinicCode = GetStringClinicCodeUsed(opd);
            var clinical_examination_and_findings = new ClinicalExaminationAndFindings(clinicCode, clinic?.Data, oen.Id, opd.IsTelehealth, oen.Version, unitOfWork).GetData();
            var diagnosis = oen_datas.FirstOrDefault(e => e.Code == "OPDOENDD0ANS")?.Value;
            var treatment_plans = oen_datas.FirstOrDefault(e => e.Code == "OPDOENTP0ANS")?.Value;
            var principal_test = oen_datas.FirstOrDefault(e => e.Code == "OPDOENPT0ANS")?.Value; 
            var resason_for_transfer = string.Empty;
            var patient_status = string.Empty;
            var follow_up_plans = string.Empty;
            if (Constant.Admitted.Contains(opd.EDStatus.Code))
            {
                resason_for_transfer = oen_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "OPDOENRFTANS")?.Value;
                patient_status = oen_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "OPDOENPS1ANS")?.Value;
                follow_up_plans = oen_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "OPDOENFP1ANS")?.Value;
            }
            else
            {
                resason_for_transfer = oen_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "OPDOENRFT2ANS")?.Value;
                patient_status = oen_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "OPDOENPS2ANS")?.Value;
                follow_up_plans = oen_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "OPDOENFP2ANS")?.Value;
            }

            var site = GetSite();
            var department = string.Format("Khoa {0}", GetSpecialty().ViName);
            return Ok(new
            {
                Department = department,
                customer.Fullname,
                DateOfBirth = customer.DateOfBirth?.ToString(Constant.DATE_FORMAT),
                Gender = gender,
                customer.Nationality,
                customer.PID,
                customer.Address,
                opd.IsTelehealth,
                AdmittedDate = opd.AdmittedDate.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                ReasonForTransfer = resason_for_transfer,
                ChiefComplain = chief_complain,
                PastMedicalHistory = hisory_of_present_illness,
                ClinicalExaminationAndFindings = clinical_examination_and_findings,
                Diagnosis = diagnosis,
                TreatmentPlans = treatment_plans,
                PrincipalTest = principal_test,
                PatientStatus = patient_status,
                FollowupPlan = follow_up_plans,
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
                PkntVersion = oen.Version,
                PhysicianAcceptTime = hocl.PhysicianAcceptTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                opd.Version,
                UserNameHandOverPhysician = hocl.HandOverPhysician?.Username,
                UserNameReceivingPhysician = hocl.ReceivingPhysician?.Username,
                IsLocked = Is24hLocked(opd.CreatedAt, opd.Id, "OPDHOC", GetUser()?.Username, hocl.Id)
            });
        }
       
        [HttpGet]
        [Route("api/OPD/HandOverCheckList/Count")]
        [Permission(Code = "OHOCL6")]
        public IHttpActionResult CountHandOverCheckListAPI()
        {
            var specialty_id = GetSpecialtyId();
            var cnt_ed = CountEDHandOverVisit(specialty_id);
            var cnt_ipd = CountIPDHandOverVisit(specialty_id);
            var cnt_opdPre = CountOPDPreAnesthesiaHandOverVisit(specialty_id);
            return Content(HttpStatusCode.OK, cnt_ed + cnt_ipd + cnt_opdPre);
        }

        private void UpdateHandOverCheckList(OPDHandOverCheckList hand_over_check_list, JObject request)
        {
            DateTime request_hand_over_datetime = DateTime.ParseExact(request["HandOverTimeNurse"]?.ToString(), Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            if (request_hand_over_datetime != null && hand_over_check_list.HandOverTimeNurse != request_hand_over_datetime)
            {
                hand_over_check_list.HandOverTimeNurse = request_hand_over_datetime;
                hand_over_check_list.HandOverNurseId = GetUser()?.Id;
                unitOfWork.OPDHandOverCheckListRepository.Update(hand_over_check_list);
                unitOfWork.Commit();
            }
        }

        private void CreateHandOverCheckListData(Guid hocl_id, string code, string value)
        {
            OPDHandOverCheckListData new_hocl_data = new OPDHandOverCheckListData
            {
                OPDHandOverCheckListId = hocl_id,
                Code = code,
                Value = value
            };
            unitOfWork.OPDHandOverCheckListDataRepository.Add(new_hocl_data);
        }

        private void UpdateHandOverCheckListData(OPDHandOverCheckListData hocl_data, string value)
        {
            hocl_data.Value = value;
            unitOfWork.OPDHandOverCheckListDataRepository.Update(hocl_data);
        }

        private void HandleUpdateHandOverCheckListData(OPDHandOverCheckList hand_over_check_list,JToken request_data)
        {
            var hocl_datas = hand_over_check_list.OPDHandOverCheckListDatas.Where(e => !e.IsDeleted).ToList();
            var user = GetUser();
            foreach (var item in request_data)
            {
                var code = item.Value<string>("Code");
                if (code == null)
                    continue;

                var value = item.Value<string>("Value");

                var hocl = hocl_datas.FirstOrDefault(e => e.Code == code);
                if (hocl == null)
                    CreateHandOverCheckListData(hand_over_check_list.Id, code, value);
                else if (hocl.Value != value && IsUserCreateFormAuto(user.Username, hocl.CreatedBy, hocl.CreatedAt, hocl.UpdatedAt))
                    UpdateHandOverCheckListData(hocl, value);

                hand_over_check_list.HandOverNurseId = user.Id;
                hand_over_check_list.UpdatedBy = user.Username;
                unitOfWork.OPDHandOverCheckListRepository.Update(hand_over_check_list);
                unitOfWork.Commit();
            }
        }
    }
}
