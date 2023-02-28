using DataAccess.Models;
using DataAccess.Models.EOCModel;
using DataAccess.Repository;
using EForm.Authentication;
using EForm.Client;
using EForm.Common;
using EForm.Controllers.BaseControllers;
using EForm.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.EOCControllers
{
    [SessionAuthorize]
    public class EOCHandOverCheckListController : BaseEOCApiController
    {
        private readonly string formCode = "OPDHOC";
        // GET: EOCHandOverCheckList
        [HttpGet]
        [Route("api/eoc/HandOverCheckList/{id}")]
        [Permission(Code = "EOC017")]
        public IHttpActionResult Get(Guid id)
        {
            var visit = GetEOC(id);
            if (visit == null) return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);

            if (!Constant.TransferToED.Contains(visit.Status.Code) && !Constant.Admitted.Contains(visit.Status.Code)) return Content(HttpStatusCode.BadRequest, Message.OPD_OEN_NOT_TRANSFER);

            var oen = GetOutpatientExaminationNote(id);

            if (oen == null) return Content(HttpStatusCode.BadRequest, Message.OPD_OEN_NOT_FOUND);

            var form = GetForm(id);
            if (form == null) return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);

            var physic_user = visit.PrimaryDoctor;
            var customer = visit.Customer;
            var gender = new CustomerUtil(customer).GetGender();

            var site = GetSite();
            var department = string.Format("Khoa {0}", GetSpecialty().ViName);
            var oen_datas = GetFormData(visit.Id, oen.Id, "OPDOEN");

            var chief_complain = oen_datas.FirstOrDefault(e => e.Code == "OPDOENCC0ANS")?.Value;
            var hisory_of_present_illness = oen_datas.FirstOrDefault(e => e.Code == "OPDOENHPIANS")?.Value;
            var clinical_examination_and_findings = oen_datas.FirstOrDefault(e => e.Code == "OPDOENCEFANS")?.Value;
            //var diagnosis = oen_datas.FirstOrDefault(e => e.Code == "OPDOENDD0ANS")?.Value;
            var treatment_plans = oen_datas.FirstOrDefault(e => e.Code == "OPDOENTP0ANS")?.Value;
            var principal_test = oen_datas.FirstOrDefault(e => e.Code == "OPDOENPT0ANS")?.Value;
            var resason_for_transfer = string.Empty;
            var patient_status = string.Empty;
            var follow_up_plans = string.Empty;
            if (Constant.Admitted.Contains(visit.Status.Code))
            {
                resason_for_transfer = oen_datas.FirstOrDefault(e => e.Code == "OPDOENRFTANS")?.Value;
                patient_status = oen_datas.FirstOrDefault(e => e.Code == "OPDOENPS1ANS")?.Value;
                follow_up_plans = oen_datas.FirstOrDefault(e => e.Code == "OPDOENFP1ANS")?.Value;
            }
            else
            {
                resason_for_transfer = oen_datas.FirstOrDefault(e => e.Code == "OPDOENRFT2ANS")?.Value;
                patient_status = oen_datas.FirstOrDefault(e => e.Code == "OPDOENPS2ANS")?.Value;
                follow_up_plans = oen_datas.FirstOrDefault(e => e.Code == "OPDOENFP2ANS")?.Value;
            }

            var eocHandOverCheckList = GetHandOverCheckList(visit.Id);

            return Ok(new
            {
                Department = department,
                site?.Location,
                Site = site?.Name,
                site?.Province,
                customer.Fullname,
                DateOfBirth = customer.DateOfBirth?.ToString(Constant.DATE_FORMAT),
                Gender = gender,
                customer.Nationality,
                customer.PID,
                customer.Address,
                AdmittedDate = visit.AdmittedDate.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                ChiefComplain = chief_complain,
                PastMedicalHistory = hisory_of_present_illness,
                ClinicalExaminationAndFindings = clinical_examination_and_findings,
                Diagnosis = GetDiagnosisEOC(oen_datas,visit.Version, "PATIENT HANDOVER RECORD"),
                TreatmentPlans = treatment_plans,
                PrincipalTest = principal_test,
                PatientStatus = patient_status,
                FollowupPlan = follow_up_plans,
                ReasonForTransfer = resason_for_transfer,
                visit.RecordCode,
                form.Id,
                VisitId = visit.Id,
                Customer = new { customer.Fullname, customer.DateOfBirth, customer.PID},
                Physician = physic_user?.Fullname,
                HandOverTimeNurse = form.HandOverTimeNurse?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                HandOverNurse = form.HandOverNurse?.Fullname,
                HandOverUnitNurse = form.HandOverUnitNurse?.ViName,
                ReceivingNurse = form.ReceivingNurse?.Fullname,
                ReceivingUnitNurse = form.ReceivingUnitNurse?.ViName,
                HandOverTimePhysician = eocHandOverCheckList?.HandOverTimePhysician?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                HandOverPhysician = eocHandOverCheckList?.HandOverPhysician,
                HandOverUnitPhysician = eocHandOverCheckList?.HandOverUnitPhysician,
                ReceivingPhysician = form.ReceivingPhysician?.Fullname,
                ReceivingUnitPhysician = form.ReceivingUnitPhysician?.ViName,
                Datas = GetFormData(visit.Id, form.Id, formCode),
                IsUseHandOverCheckList = eocHandOverCheckList?.IsUseHandOverCheckList,
                VisitCode = visit.VisitCode,
                NurseAcceptTime = eocHandOverCheckList.NurseAcceptTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                PhysicianAcceptTime = eocHandOverCheckList.PhysicianAcceptTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                UserNameHandOverPhysician = eocHandOverCheckList?.HandOverPhysician?.Username,
                UserNameReceivingPhysician = eocHandOverCheckList?.ReceivingPhysician?.Username,
                UserNameHandOverNurse = form.HandOverNurse?.Username,
                UserNameReceivingNurse = form.ReceivingNurse?.Username
            });
        }
        
        [HttpPost]
        [CSRFCheck]
        [Route("api/eoc/HandOverCheckList/Create/{id}")]
        [Permission(Code = "EOC018")]
        public IHttpActionResult Post(Guid id)
        {
            var visit = GetEOC(id);
            if (visit == null) return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);

            if (!Constant.TransferToED.Contains(visit.Status.Code) && !Constant.Admitted.Contains(visit.Status.Code)) return Content(HttpStatusCode.BadRequest, Message.OPD_OEN_NOT_TRANSFER);

            var oen = GetOutpatientExaminationNote(id);
            if (oen == null) return Content(HttpStatusCode.BadRequest, Message.OPD_OEN_NOT_FOUND);

            var form = GetForm(id);
            if (form != null) return Content(HttpStatusCode.BadRequest, Message.FORM_EXIST);
            var receiving = GetReceivingUnitPhysicianId(visit);
            var form_data = new EOCHandOverCheckList
            {
                VisitId = id,
                HandOverPhysicianId = visit.PrimaryDoctorId,
                HandOverUnitPhysicianId = GetSpecialtyId(),
                HandOverNurseId = GetUser().Id,
                HandOverTimeNurse = DateTime.Now,
                HandOverUnitNurseId = GetSpecialtyId(),
                ReceivingUnitPhysicianId = receiving,
                ReceivingUnitNurseId = receiving,
                HandOverTimePhysician = oen.UpdatedAt
            };
            unitOfWork.EOCHandOverCheckListRepository.Add(form_data);
            UpdateVisit(visit);
            unitOfWork.Commit();
            return Content(HttpStatusCode.OK, new { form_data.Id });
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/eoc/HandOverCheckList/Update/{id}")]
        [Permission(Code = "EOC019")]
        public IHttpActionResult Update(Guid id, [FromBody]JObject request)
        {
            var visit = GetEOC(id);
            if (visit == null) return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);

            if (!Constant.TransferToED.Contains(visit.Status.Code) && !Constant.Admitted.Contains(visit.Status.Code)) return Content(HttpStatusCode.BadRequest, Message.OPD_OEN_NOT_TRANSFER);

            var oen = GetOutpatientExaminationNote(id);
            if (oen == null) return Content(HttpStatusCode.BadRequest, Message.OPD_OEN_NOT_FOUND);

            var form = GetForm(id);
            if (form == null) return Content(HttpStatusCode.BadRequest, Message.FORM_NOT_FOUND);

            
            HandleUpdateOrCreateFormDatas(id, form.Id, formCode, request["Datas"]);

            DateTime request_hand_over_datetime = DateTime.ParseExact(request["HandOverTimeNurse"]?.ToString(), Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            if (request_hand_over_datetime != null && form.HandOverTimeNurse != request_hand_over_datetime)
            {
                form.HandOverTimeNurse = request_hand_over_datetime;
                form.HandOverNurseId = GetUser()?.Id;
            }

            unitOfWork.EOCHandOverCheckListRepository.Update(form);
            UpdateVisit(visit);
            unitOfWork.Commit();

            return Content(HttpStatusCode.OK, new { form.Id });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/eoc/HandOverCheckList/Accept/{id}")]
        [Permission(Code = "EOC020")]
        public IHttpActionResult Accept(Guid id, [FromBody]JObject request)
        {
            var obj = GetHandOverCheckListById(id);
            if (obj == null)
                return Content(HttpStatusCode.NotFound, Message.HOCL_NOT_FOUND);
            if (obj.Visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);

            User user = null;
            if (obj.IsUseHandOverCheckList)
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

            var hand_over_check_list = obj;

            var is_create = !hand_over_check_list.IsAcceptPhysician && !hand_over_check_list.IsAcceptNurse;
            var specialties = GetListSpecicalty(user);
            var positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName);
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

            var visit = obj.Visit;

            Guid? new_visit_id = null;
            var visit_type_group_hand_over_code = unitOfWork.SpecialtyRepository.GetById(
                (Guid)hand_over_check_list.HandOverUnitPhysicianId
            ).VisitTypeGroup.Code;
            if (!is_create)
            {
                unitOfWork.EOCHandOverCheckListRepository.Update(hand_over_check_list);
                unitOfWork.Commit();
                return Content(HttpStatusCode.OK, new { Id = new_visit_id });
            }

            

            var is_create_error = IsCreateVisitError(visit);
            if (is_create_error != null)
                return Content(HttpStatusCode.BadRequest, is_create_error);


            unitOfWork.EOCHandOverCheckListRepository.Update(hand_over_check_list);

            new_visit_id = CreateNewOPDFromHandOver((Guid)visit.CustomerId, obj.Id);
            unitOfWork.Commit();
            return Content(HttpStatusCode.OK, new { Id = new_visit_id });
        }
        protected dynamic IsCreateVisitError(dynamic visit)
        {
            Guid customer_id = visit.CustomerId;
            Guid? group_id = null;
            try { group_id = visit.GroupId; }
            catch (Exception) { }

            InHospital in_hospital = new InHospital();
            in_hospital.SetState(customer_id, null, group_id, null);
            var in_hospital_visit = in_hospital.GetVisit();
            if (in_hospital_visit != null)
                return in_hospital.BuildErrorMessage(in_hospital_visit);

            string customer_PID = visit.Customer.PID;
            dynamic in_waiting_visit;
            if (!string.IsNullOrEmpty(customer_PID))
                in_waiting_visit = GetInWaitingAcceptPatientByPID(pid: customer_PID, visit_id: visit.Id, group_id: group_id);
            else
                in_waiting_visit = GetInWaitingAcceptPatientById(customer_id: visit.CustomerId, visit_id: visit.Id, group_id: group_id);
            if (in_waiting_visit != null)
            {
                var transfer = GetHandOverCheckListByVisit(in_waiting_visit);
                return BuildInWaitingAccpetErrorMessage(transfer.HandOverUnitPhysician, transfer.ReceivingUnitPhysician);
            }
            return null;
        }
        protected Guid CreateNewOPDFromHandOver(Guid customer_id, Guid hand_over_check_list_id)
        {
            InHospital in_hospital = new InHospital();
            var creater = new VisitCreater(
                unitOfWork,
                in_hospital.GetStatus("EOC").Id,
                customer_id,
                hand_over_check_list_id,
                null,
                null,
                GetSiteId(),
                GetSpecialtyId(),
                GetUser().Id
            );
            var visit = creater.CreateNewEOC();
            return visit.Id;
        }
        private EOCHandOverCheckList GetHandOverCheckListById(Guid id)
        {

            var eoc_hocl = unitOfWork.EOCHandOverCheckListRepository.GetById(id);
            if (eoc_hocl != null) return eoc_hocl;

            return null;
        }
        private EOCHandOverCheckList GetForm(Guid VisitId)
        {
            var form = unitOfWork.EOCHandOverCheckListRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == VisitId);
            return form;
        }
        private string GetReasonForTransfer(EOC visit)
        {
            if (Constant.TransferToED.Contains(visit.Status.Code))
                return GetMdValueInVisit(visit.Id, "OPDOENRFT2ANS");
            if (Constant.Admitted.Contains(visit.Status.Code))
                return GetMdValueInVisit(visit.Id, "OPDOENRFTANS");
            return "";
        }
        private Guid? GetReceivingUnitPhysicianId  (EOC visit)
        {
            Guid? receiving_id = null;
            string receiving = string.Empty;
            if (Constant.TransferToED.Contains(visit.Status.Code))
                receiving = GetMdValueInVisit(visit.Id, "OPDOENREC2ANS");
            if (Constant.Admitted.Contains(visit.Status.Code))
                receiving = GetMdValueInVisit(visit.Id, "OPDOENRECANS");
            try
            {
                receiving_id = new Guid(receiving);
            }
            catch (Exception)
            {
                return Message.TRANSFER_ERROR;
            }
            
            return receiving_id;
        }


        private EOCHandOverCheckList GetHandOverCheckList(Guid VisitId)
        {
            var form = unitOfWork.EOCHandOverCheckListRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == VisitId);
            return form;
        }
    }
}