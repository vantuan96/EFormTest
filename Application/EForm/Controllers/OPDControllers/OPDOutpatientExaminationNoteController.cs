using DataAccess.Models;
using DataAccess.Models.IPDModel;
using DataAccess.Models.OPDModel;
using EForm.Authentication;
using EForm.Client;
using EForm.Common;
using EForm.Controllers.BaseControllers;
using EForm.Models;
using EForm.Utils;
using EMRModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.OPDControllers
{
    [SessionAuthorize]
    public class OPDOutpatientExaminationNoteController : BaseOPDApiController
    {
        private const string code_setupClinic = "SETUPCLINICS";

        [HttpGet]
        [Route("api/OPD/OPDOutpatientExaminationNote/{id}")]
        [Permission(Code = "OOUEN1")]
        public IHttpActionResult GetExaminationNoteAPI(Guid id)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var oen = opd.OPDOutpatientExaminationNote;
            if (oen == null || oen.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.OPD_OEN_NOT_FOUND);
            var custormer = opd.Customer;
            var status = opd.EDStatus;
            var clinic = opd.Clinic;
            var primary_doctor = opd.PrimaryDoctor;
            var authorized_doctor = opd.AuthorizedDoctor;
            var visit_transfer = get_visit_transfer(opd);
            var user = GetUser();
            var locked = isLockOen(opd); // status?.EnName != "Waiting results" && (user.Username != primary_doctor?.Username || user.Username != authorized_doctor?.Username) && Is24hLocked(opd.CreatedAt, opd.Id, "OPDOEN", user.Username);
            var EOCInfo = getEOCInfo(opd.Id, "OPD");
            var datas = unitOfWork.OPDOutpatientExaminationNoteDataRepository.Find(e => e.OPDOutpatientExaminationNoteId == opd.OPDOutpatientExaminationNoteId).ToList();
            List<string> codesSetupClinics = new List<string>();

            if (oen.Version >= 2)
                codesSetupClinics = GetCodeFromSetUpOrForm(clinic, datas);
            var check_visit_PreAnes = PreAnesthe(opd);
            var HandOverCheckList = opd.OPDHandOverCheckList;
            return Ok(new
            {
                EOCInfo,
                locked,
                visit_transfer,
                oen.Id,
                oen.Service,
                opd.IsTelehealth,
                IsNew = IsNew(oen.CreatedAt, oen.UpdatedAt),
                PrimaryDoctor = new
                {
                    primary_doctor?.Id,
                    primary_doctor?.Fullname,
                    primary_doctor?.Username
                },
                AuthorizedDoctor = new
                {
                    authorized_doctor?.Id,
                    authorized_doctor?.Fullname,
                    authorized_doctor?.Username
                },
                ClinicId = clinic?.Id,
                Clinic = new { clinic?.ViName, clinic?.EnName, clinic?.Code, clinic?.Data },
                ExaminationTime = oen.ExaminationTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                Status = new { status.Id, status.ViName, status.EnName, status.Code },
                OPDId = opd.Id,
                Datas = datas.Where(i => !i.IsDeleted).Select(e => new { e.Id, e.Code, e.Value }).ToList(),
                ListStatus = GetStatus(EOCInfo.AcceptBy != null),
                oen.IsConsultation,
                IsUseHandOverCheckList = HandOverCheckList?.IsUseHandOverCheckList,
                IsAcceptPhysician = HandOverCheckList?.IsAcceptPhysician,
                IsAcceptNurse = HandOverCheckList?.IsAcceptNurse,
                oen.AppointmentDateResult,
                SetupClinic = codesSetupClinics,
                oen.Version,
                oen.CreatedBy,
                oen.CreatedAt,
                oen.UpdatedBy,
                oen.UpdatedAt,
                Age = CaculatorAgeCustormer(custormer?.DateOfBirth),
                IscheckVisitPreAnes = check_visit_PreAnes,
                IsLock24hBA = Is24hLocked(opd.CreatedAt, opd.Id, "A01_252_221222_V", user.Username, oen.Id)// khoá 24h của bệnh án ngoại trú
            });
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/OPDOutpatientExaminationNote/SetConsultation/{id}")]
        [Permission(Code = "OOUEN2")]
        public IHttpActionResult SetConsultation(Guid id, [FromUri] SetConsultationRecord model)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var oen = opd.OPDOutpatientExaminationNote;
            if (oen == null || oen.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.OPD_OEN_NOT_FOUND);

            var user = GetUser();
            var is_block_after_24h = IsBlockAfter24h(opd.CreatedAt);
            var has_unlock_permission = HasUnlockPermission(opd.Id, "OPDOEN", user.Username);
            var is_waiting_principal_test = IsWaitingPrincipalTest(opd.EDStatus, opd.CreatedAt);
            if (is_block_after_24h && !has_unlock_permission && !is_waiting_principal_test)
                return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);

            if ((!IsCorrectDoctor(user?.Id, opd.PrimaryDoctorId) && !IsCorrectDoctor(user?.Id, opd.AuthorizedDoctorId)))
                return Content(HttpStatusCode.BadRequest, Message.OWNER_FORBIDDEN);
            oen.IsConsultation = model.IsConsultation;
            SetVersionForFormIsNew(oen);
            unitOfWork.OPDOutpatientExaminationNoteRepository.Update(oen, is_time_change: false);
            unitOfWork.Commit();

            return Content(HttpStatusCode.OK, new { });
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/OPDOutpatientExaminationNote/{id}")]
        [Permission(Code = "OOUEN2")]
        public IHttpActionResult UpdateExaminationNoteAPI(Guid id, [FromBody] JObject request)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var oen = opd.OPDOutpatientExaminationNote;
            if (oen == null || oen.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.OPD_OEN_NOT_FOUND);

            var status_code = request["Status"]["Code"].ToString();
            var visit_transfer = get_visit_transfer(opd);
            if (visit_transfer != null && Constant.NoExamination.Contains(status_code))
            {
                return Content(visit_transfer.Code, visit_transfer.Message);
            }

            var user = GetUser();
            var is_block_after_24h = IsBlockAfter24h(opd.CreatedAt);
            var has_unlock_permission = HasUnlockPermission(opd.Id, "OPDOEN", user.Username);
            var is_waiting_principal_test = IsWaitingPrincipalTest(opd.EDStatus, opd.CreatedAt);
            if (is_block_after_24h && !has_unlock_permission && !is_waiting_principal_test)
                return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);

            if (!IsSuperman() && (!IsCorrectDoctor(user?.Id, opd.PrimaryDoctorId) && !IsCorrectDoctor(user?.Id, opd.AuthorizedDoctorId)))
                return Content(HttpStatusCode.BadRequest, Message.OWNER_FORBIDDEN);

            if (oen.Version >= 2)
                HandleCodeSetupClinic(oen, request["SetupClinic"]);

            HandleUpdateExaminationTime(oen, request["ExaminationTime"]?.ToString());
            HandleUpdateAppointmentDateResult(oen, request["AppointmentDateResult"]?.ToString());
            HandleUpdateOrCreateOutpatientExaminationNoteData(
                opd: opd,
                oen: oen,
                user: user,
                is_block_after_24h: is_block_after_24h,
                is_waiting_principal_test: is_waiting_principal_test,
                has_unlock_permission: has_unlock_permission,
                request_oen_data: request["Datas"]
            );

            if (user.Username != opd.PrimaryDoctor?.Username)
                CreatedOPDChangingNotification(user, opd.PrimaryDoctor?.Username, opd, "Phiếu khám ngoại trú", "OutpatientExaminationNote");

            CreateEOCTranfer(opd.Id, request["Datas"], user.Username, "OPD", (Guid)opd.CustomerId);

            bool isUseHandOverCheckList = false;

            try
            {
                isUseHandOverCheckList = Convert.ToBoolean(request["IsUseHandOverCheckList"]);
            }
            catch (Exception)
            {
                isUseHandOverCheckList = false;
            }
            var check = PreAnesthesiaModel(opd);
            var is_error = HandleUpdateStatus(opd, request["Status"], request["Datas"], isUseHandOverCheckList);

            if (check != null && is_error == "OPDIH") return Content(HttpStatusCode.BadRequest, new { ViMessage = "Không thể đổi trạng thái do Người bệnh đang khám ở gây mê", EnMessage = "Không thể đổi trạng thái do Người bệnh đang khám ở gây mê" });

            if (is_error != null)
                return Content(is_error.Code, is_error.Message);
            HandleDischargeDate(opd);
            var chronic_util = new ChronicUtil(unitOfWork, opd.Customer);
            chronic_util.UpdateChronic();
            return Content(HttpStatusCode.OK, new { opd.Customer.IsChronic });
        }
        private void HandleDischargeDate(OPD opd)
        {
            opd.DischargeDate = GetDischargeDate(opd);
            unitOfWork.Commit();
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/OPDOutpatientExaminationNote/syncRISReport")]
        [Permission(Code = "OOUEN3")]
        public IHttpActionResult getRISReport([FromBody] JObject request)
        {
            dynamic lab_result = OHClient.getRISReport(request["report_ids"]);
            return Content(HttpStatusCode.OK, lab_result);
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/OPDOutpatientExaminationNote/SyncPrincipalTest")]
        [Permission(Code = "OOUEN3")]
        public IHttpActionResult SyncPrincipalTestAPI([FromBody] JObject request)
        {
            if (request == null || string.IsNullOrEmpty(request["Id"]?.ToString()))
            {
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);
            }
            var id = new Guid(request["Id"]?.ToString());
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var customer = opd.Customer;
            if (customer == null || customer.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);
            else if (customer.PID == null)
                return Content(HttpStatusCode.NotFound, Message.PID_IS_MISSING);

            var oen = opd.OPDOutpatientExaminationNote;
            if (oen == null || oen.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.OPD_OEN_NOT_FOUND);

            var site_code = GetSiteCode();
            if (string.IsNullOrEmpty(site_code))
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);

            dynamic lab_result;
            dynamic xray_result;
            if (site_code == "times_city")
            {
                lab_result = EHosClient.GetLabResults(customer.PID);
                xray_result = EHosClient.GetXrayResults(customer.PID);
            }
            else
            {
                var api_code = GetSiteAPICode();
                lab_result = opd.IsEhos == true ? EHosClient.GetLabResults(customer.PIDEhos) : OHClient.GetLabResults(customer.PID, api_code);
                xray_result = opd.IsEhos == true ? EHosClient.GetXrayResults(customer.PIDEhos) : OHClient.GetXrayResultsByPID(customer.PID);
            }
            return Content(HttpStatusCode.OK, new
            {
                XetNghiem = lab_result,
                CDHA = xray_result,
                DiagnosticReporting = GroupDiagnosticReportingByPid(customer.Id)
            });
        }


        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/OPDOutpatientExaminationNote/SyncDiagnosisAndICD/")]
        [Permission(Code = "OOUEN4")]
        public IHttpActionResult SyncDiagnosisAndICDAPI([FromBody] JObject request)
        {
            if (request == null || string.IsNullOrEmpty(request["Id"]?.ToString()))
            {
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);
            }
            var id = new Guid(request["Id"]?.ToString());
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var customer = opd.Customer;
            if (customer == null || customer.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);
            else if (string.IsNullOrEmpty(customer.PID))
                return Content(HttpStatusCode.NotFound, Message.PID_IS_MISSING);

            if (string.IsNullOrEmpty(opd.VisitCode))
                return Content(HttpStatusCode.NotFound, Message.VISIT_CODE_IS_MISSING);

            var oen = opd.OPDOutpatientExaminationNote;
            if (oen == null || oen.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.OPD_OEN_NOT_FOUND);

            var site_code = GetSiteCode();
            if (string.IsNullOrEmpty(site_code))
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);

            dynamic result;
            var primary_doctor = opd.PrimaryDoctor;
            var authorized_doctor = opd.AuthorizedDoctor;
            if (primary_doctor == null && authorized_doctor == null)
                return Content(HttpStatusCode.BadRequest, Message.PRIMARY_DOCTOR_NOT_FOUND);

            if (site_code == "times_city")
            {
                if (string.IsNullOrEmpty(primary_doctor?.EHOSAccount) && string.IsNullOrEmpty(authorized_doctor?.EHOSAccount))
                    return Content(HttpStatusCode.NotFound, Message.EHOS_ACCOUNT_MISSING);

                result = EHosClient.GetDiagnosisAndICD(customer.PID, opd.VisitCode, $"{primary_doctor?.EHOSAccount},{authorized_doctor?.EHOSAccount}");
            }
            else
                result = OHClient.GetDiagnosisAndICD(customer.PID, opd.VisitCode, $"{primary_doctor?.Username},{authorized_doctor?.Username}");

            return Content(HttpStatusCode.OK, result);
        }


        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/OPDOutpatientExaminationNote/SyncVisitHistory/")]
        [Permission(Code = "OOUEN5")]
        public IHttpActionResult SyncVisitHistoryAPI([FromBody] JObject request)
        {
            if (request == null || string.IsNullOrEmpty(request["Id"]?.ToString()))
            {
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);
            }
            var id = new Guid(request["Id"]?.ToString());
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var customer = opd.Customer;
            if (customer == null || customer.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);
            else if (customer.PID == null)
                return Content(HttpStatusCode.NotFound, Message.PID_IS_MISSING);
            List<VisitHistoryModel> familymedicalHistory = new List<VisitHistoryModel>();
           var list_visit_opd = unitOfWork.OPDRepository.Find(e => !e.IsDeleted && e.CustomerId == customer.Id && e.Id != id && e.CreatedAt < opd.CreatedAt).ToList();
            if(list_visit_opd != null && list_visit_opd.Count > 0)
            {
                foreach (var item in list_visit_opd)
                {
                    var data  = OPDFamilyPastMedicalHistory(item);
                    if ((data != null && String.IsNullOrEmpty(data.PastMedicalHistory)) || data == null) continue;
                    familymedicalHistory.Add(data);
                }
            }
            var list_visit_ipd = unitOfWork.IPDRepository.Find(e => !e.IsDeleted && e.CustomerId == customer.Id && e.CreatedAt < opd.CreatedAt).ToList();
            if (list_visit_ipd != null && list_visit_ipd.Count > 0)
            {
                foreach (var item in list_visit_ipd)
                {
                    var data = IPDFamilyPastMedicalHistory(item);
                    if ((data != null && String.IsNullOrEmpty(data.PastMedicalHistory)) || data == null) continue;
                    familymedicalHistory.Add(data);
                }
            }
            familymedicalHistory = familymedicalHistory.OrderByDescending(e => e.UpdateAt).ToList();
            var oen = opd.OPDOutpatientExaminationNote;
            if (oen == null || oen.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.OPD_OEN_NOT_FOUND);

            var site_code = GetSiteCode();
            if (string.IsNullOrEmpty(site_code))
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);

            //VisitHistory visit_history = VisitHistoryFactory.GetVisit("OPD", opd, site_code);
            VisitHistory visit_history = new OPDVisitHistoryForOutpatientExaminationNote(opd, site_code);
            var visit_history_list = visit_history.GetHistory();
            OPDHistory opdHistory = new OPDHistory(opd, opd.Site.Code);
            List<VisitModel> lstPersonalHistory = opdHistory.GetPersonalHistory(opd.Id);

            //var history_fromEDAmbulanceRunReport = GetHistoryFromEDAmbulanceRunReport(customer.Id, opd.AdmittedDate); // bệnh sử từ cấp cứu ngoại viện
            //visit_history_list.AddRange(history_fromEDAmbulanceRunReport);
            //visit_history_list = visit_history_list.OrderByDescending(e => e.ExaminationTime).ToList();

            return Content(HttpStatusCode.OK, new
            {
                PastMedicalHistory = visit_history_list.Where(e => !string.IsNullOrEmpty(e.PastMedicalHistory)).GroupBy(e => e.ExaminationTime).Select(e => e.FirstOrDefault()).Select(e => formatPastMedicalHistory(e)),
                HistoryOfAllergies = visit_history_list.Where(e => !string.IsNullOrEmpty(e.HistoryOfAllergies)).GroupBy(e => e.ExaminationTime).Select(e => e.FirstOrDefault()).Select(e => new             
                {
                    ExaminationTime = e.ExaminationTime.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                    e.ViName,
                    e.EnName,
                    e.Fullname,
                    e.Username,
                    e.HistoryOfAllergies,
                    e.Type
                }),
                InitialAssessmentAllergies = GetAllergyInInitialAssessment(opd),
                HistoryOfPresentIllness = visit_history_list.Where(e => !string.IsNullOrEmpty(e.HistoryOfPresentIllness)).GroupBy(e => e.ExaminationTime).Select(e => e.FirstOrDefault()).Select(e => new
                {
                    ExaminationTime = e.ExaminationTime.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                    e.ViName,
                    e.EnName,
                    e.Fullname,
                    e.Username,
                    e.HistoryOfPresentIllness,
                    e.Type
                }),
                FamilyMedicalHistory = familymedicalHistory
            });
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/OPDOutpatientExaminationNote/AuthorizeDoctor/")]
        [Permission(Code = "OOUEN6")]
        public IHttpActionResult AuthorizeDoctorAPI([FromBody] JObject request)
        {
            if (request == null || string.IsNullOrEmpty(request["Id"]?.ToString()))
            {
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);
            }
            var id = new Guid(request["Id"]?.ToString());
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var user = GetUser();

            var is_block_after_24h = IsBlockAfter24h(opd.CreatedAt);
            var has_unlock_permission = HasUnlockPermission(opd.Id, "OPDOEN", user.Username);
            var is_waiting_principal_test = IsWaitingPrincipalTest(opd.EDStatus, opd.CreatedAt);
            if (is_block_after_24h && !has_unlock_permission && !is_waiting_principal_test)
                return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);

            if (!IsCorrectDoctor(user?.Id, opd.PrimaryDoctorId) && !IsCorrectDoctor(user?.Id, opd.AuthorizedDoctorId))
                return Content(HttpStatusCode.BadRequest, Message.OWNER_FORBIDDEN);

            var oen = opd.OPDOutpatientExaminationNote;
            if (oen.IsAuthorizeDoctorChangeForm)
                return Content(HttpStatusCode.BadRequest, new
                {
                    ViMessage = $"Bác sĩ {opd.AuthorizedDoctor?.Fullname} đã thực hiện thăm khám cho NB",
                    EnMessage = $"Bác sĩ {opd.AuthorizedDoctor?.Fullname} đã thực hiện thăm khám cho NB",
                });

            var str_id = request["AuthorizedDoctor"]?["Id"]?.ToString();
            if (string.IsNullOrEmpty(str_id))
            {
                opd.AuthorizedDoctorId = null;
                unitOfWork.OPDRepository.Update(opd);
                unitOfWork.Commit();
                return Content(HttpStatusCode.OK, Message.SUCCESS);
            }

            var authorized_doctor_id = new Guid(str_id);
            if (authorized_doctor_id == opd.AuthorizedDoctorId)
                return Content(HttpStatusCode.BadRequest, Message.NOTHING_CHANGE);

            var authoried_doctor = unitOfWork.UserRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == authorized_doctor_id);
            if (authoried_doctor == null)
                return Content(HttpStatusCode.BadRequest, Message.USER_NOT_FOUND);

            opd.AuthorizedDoctorId = authorized_doctor_id;
            unitOfWork.OPDRepository.Update(opd);
            unitOfWork.Commit();

            CreateAuthorizedDoctorNotification(user, authoried_doctor, opd, "OutpatientExaminationNote");

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpGet]
        [Route("api/OPD/OPDOutpatientExaminationNote/CheckVersion/{visitId}")]
        [Permission(Code = "OOUEN1")]
        public IHttpActionResult GetVersionForm(Guid visitId)
        {
            OPD visit = GetOPD(visitId);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var form = unitOfWork.OPDOutpatientExaminationNoteRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == visit.OPDOutpatientExaminationNoteId);
            if (form == null)
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);

            return Content(HttpStatusCode.OK, new { Version = form.Version, IsLocked = isLockOen(visit) });
        }

        private bool isLockOen(OPD opd)
        {
            var user = GetUser();
            var is_block_after_24h = IsBlockAfter24h(opd.CreatedAt);
            var has_unlock_permission = HasUnlockPermission(opd.Id, "OPDOEN", user.Username);
            var is_waiting_principal_test = IsWaitingPrincipalTest(opd.EDStatus, opd.CreatedAt);
            var primary_doctor = opd.PrimaryDoctor;
            var authorized_doctor = opd.AuthorizedDoctor;
            return (is_block_after_24h && !has_unlock_permission && !is_waiting_principal_test) || !(user.Username == primary_doctor?.Username || user.Username == authorized_doctor?.Username);
        }
        private bool IsWaitingPrincipalTest(EDStatus status, DateTime? created_at)
        {
            var block_time = created_at?.AddDays(1);
            var now = DateTime.Now;
            return now > block_time &&
                status != null &&
                !string.IsNullOrEmpty(status.EnName) &&
                Constant.WaitingResults.Contains(status.Code);
        }

        private void HandleUpdateExaminationTime(OPDOutpatientExaminationNote oen, string examination_time)
        {
            DateTime request_examination_time;
            bool success = DateTime.TryParseExact(examination_time, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out request_examination_time);          
            if (success && oen.ExaminationTime != request_examination_time)
            {
                oen.ExaminationTime = request_examination_time;
                unitOfWork.OPDOutpatientExaminationNoteRepository.Update(oen);
                unitOfWork.Commit();
            }
        }
        private void HandleUpdateAppointmentDateResult(OPDOutpatientExaminationNote oen, string appointment_date_result)
        {
            if (!string.IsNullOrEmpty(appointment_date_result))
            {
                DateTime request_appointmentDateResult;
                bool success = DateTime.TryParseExact(appointment_date_result, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out request_appointmentDateResult);
                if (success)
                {
                    oen.AppointmentDateResult = request_appointmentDateResult;
                    unitOfWork.OPDOutpatientExaminationNoteRepository.Update(oen);
                    unitOfWork.Commit();
                }
            }
            else
            {
                oen.AppointmentDateResult = null; // khi thao tac xoa ngay hen tai kham
                unitOfWork.OPDOutpatientExaminationNoteRepository.Update(oen);
                unitOfWork.Commit();
            }

        }

        private void HandleUpdateOrCreateOutpatientExaminationNoteData(OPD opd, OPDOutpatientExaminationNote oen, User user, bool is_block_after_24h, bool is_waiting_principal_test, bool has_unlock_permission, JToken request_oen_data)
        {

            var oen_datas = oen.OPDOutpatientExaminationNoteDatas.Where(e => !e.IsDeleted).ToList();
            foreach (var item in request_oen_data)
            {
                var code = item.Value<string>("Code");
                if (code == "TFTEOCANS")
                {
                    var valu1e = item.Value<string>("Value");
                }
                if (code == null || Constant.OPD_OEN_READ_ONLY.Contains(code) || "OPDOENREC2ANS,OPDOENRFT2ANS,OPDOENRECANS,OPDOENRFTANS".Contains(code))
                    continue;

                // if (is_waiting_principal_test && !Constant.OPD_OEN_IN_WAITING_PRINCIPAL_TEST_ACCEPT.Contains(code) && !(is_block_after_24h && has_unlock_permission)) continue;

                var value = item.Value<string>("Value");
                var oen_data = oen_datas.FirstOrDefault(e => e.Code == code);
                if (code == "TFTEOCANS")
                {
                    var valu2e = oen_data;
                }
                if (oen_data == null)
                    CreateExaminationNoteData(oen.Id, code, value);
                else if (oen_data.Value != value)
                    UpdateExaminationNoteData(oen_data, code, value);
            }
            var hocl = opd.OPDHandOverCheckList;
            if (hocl != null)
            {
                hocl.HandOverPhysicianId = user.Id;
                unitOfWork.OPDHandOverCheckListRepository.Update(hocl);
            }
            oen.UpdatedBy = user.Username;
            if (opd.AuthorizedDoctorId != null && opd.AuthorizedDoctorId == user.Id)
                oen.IsAuthorizeDoctorChangeForm = true;

            if (opd.AuthorizedDoctorId != null && opd.AuthorizedDoctorId == user.Id)
                oen.IsDoctorChangeForm = true;

            if (opd.PrimaryDoctorId != null && opd.PrimaryDoctorId == user.Id)
                oen.IsDoctorChangeForm = true;

            unitOfWork.OPDOutpatientExaminationNoteRepository.Update(oen);
            unitOfWork.Commit();
        }
        private void CreateExaminationNoteData(Guid oen_id, string code, string value)
        {
            if (!"OPDOENTD0ANS,OPDOENTOTANS".Contains(code) ||
                //(code.Equals("OPDOENDORANS") && Validator.ValidateDate(value)) || xóa ngày hẹn tái khám
                ("OPDOENTD0ANS, OPDOENTOTANS".Contains(code) && Validator.ValidateTimeDateWithoutSecond(value)))
            {
                if (!string.IsNullOrEmpty(value))
                {
                    OPDOutpatientExaminationNoteData new_oen_data = new OPDOutpatientExaminationNoteData();
                    new_oen_data.OPDOutpatientExaminationNoteId = oen_id;
                    new_oen_data.Code = code;
                    new_oen_data.Value = value;
                    unitOfWork.OPDOutpatientExaminationNoteDataRepository.Add(new_oen_data);
                }
            }
        }
        private void UpdateExaminationNoteData(OPDOutpatientExaminationNoteData oen_data, string code, string value)
        {
            if (!"OPDOENTD0ANS,OPDOENTOTANS".Contains(code) ||
                //(code.Equals("OPDOENDORANS") && Validator.ValidateDate(value)) ||  xóa ngày hẹn tái khám
                ("OPDOENTD0ANS, OPDOENTOTANS".Contains(code) && Validator.ValidateTimeDateWithoutSecond(value)))
            {
                if (oen_data.Value != value)
                {
                    oen_data.Value = value;
                    unitOfWork.OPDOutpatientExaminationNoteDataRepository.Update(oen_data);
                }
            }
        }
        private void UpdateOrCreateExaminationNoteData(Guid? oen_id, string code, string value)
        {
            var data = unitOfWork.OPDOutpatientExaminationNoteDataRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.OPDOutpatientExaminationNoteId == oen_id &&
                e.Code == code
            );
            if (data != null)
            {
                data.Value = value;
                unitOfWork.OPDOutpatientExaminationNoteDataRepository.Update(data);
                return;
            }

            data = new OPDOutpatientExaminationNoteData
            {
                OPDOutpatientExaminationNoteId = oen_id,
                Code = code,
                Value = value,
            };
            unitOfWork.OPDOutpatientExaminationNoteDataRepository.Add(data);
        }

        private dynamic HandleUpdateStatus(OPD opd, JToken request_status, JToken request_datas, bool isUseHandOverCheckList)
        {
            var new_status = request_status["Id"].ToString();
            Guid new_status_id = new Guid(new_status);
            if (new_status_id == Guid.Empty) return null;

            var current_status = opd.EDStatus;

            var new_status_code = request_status["Code"].ToString();

            var err = IsChangeTransferedStatus(opd, request_datas, current_status, new_status_code);
            if (err != null)
                return err;
            var Specialty = GetSpecialty();
            var cus = unitOfWork.CustomerRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == opd.CustomerId);
            var check = PreAnesthesiaModel(opd);
            if (check != null)
            {
                if (check.StatusId != null)
                {
                    Guid statusId = check.StatusId;
                    var status = unitOfWork.EDStatusRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == statusId);

                    if (status.Code == "OPDIH" && new_status_code.Contains("OPDIH")) return "OPDIH";

                    UpdateStatus(opd, request_status);
                }
            }
            else

            if (current_status.Id != new_status_id)
            {
                if (Constant.TransferToED.Contains(current_status.Code) || Constant.Admitted.Contains(current_status.Code))
                {
                    var visit_transfer = new VisitTransfer(unitOfWork);
                    if (visit_transfer.IsExist(opd.OPDHandOverCheckListId, opd.Id))
                        return visit_transfer.BuildMessage();
                }

                if (!IsConfirmStandingOrder(opd.Id))
                    return new
                    {
                        Code = HttpStatusCode.BadRequest,
                        Message = Message.CONFIRM_STANDING_ORDER
                    };

                if (Constant.InHospital.Contains(new_status_code) || Constant.TransferToED.Contains(new_status_code) || Constant.Admitted.Contains(new_status_code))
                {
                    Guid visit_id = opd.Id;
                    Guid customer_id = (Guid)opd.CustomerId;
                    string customer_PID = opd.Customer.PID;
                    Guid? group_id = opd.GroupId;
                    string visit_code = opd.VisitCode;

                    InHospital in_hospital = new InHospital();
                    in_hospital.SetState(customer_id, visit_id, group_id, visit_code);
                    var in_hospital_visit = in_hospital.GetVisit();

                    if (in_hospital_visit != null)
                        return new
                        {
                            Code = HttpStatusCode.BadRequest,
                            Message = in_hospital.BuildErrorMessage(in_hospital_visit)
                        };

                    dynamic in_waiting_visit;
                    if (!string.IsNullOrEmpty(opd.Customer.PID))
                        in_waiting_visit = GetInWaitingAcceptPatientByPID(pid: customer_PID, visit_id: visit_id, group_id: group_id);
                    else
                        in_waiting_visit = GetInWaitingAcceptPatientById(customer_id: customer_id, visit_id: visit_id, group_id: group_id);
                    if (in_waiting_visit != null)
                    {
                        var transfer = GetHandOverCheckListByVisit(in_waiting_visit);
                        return new
                        {
                            Code = HttpStatusCode.BadRequest,
                            Message = BuildInWaitingAccpetErrorMessage(transfer.HandOverUnitPhysician, transfer.ReceivingUnitPhysician)
                        };
                    }
                }

                if (!Constant.TransferToED.Contains(new_status_code) && !Constant.Admitted.Contains(new_status_code))
                    RemoveTransferIfExist(opd);

                var error_mess = OPDValiDatePIDAndVisitCode(opd, new_status_code);
                if (error_mess != null)
                    return error_mess;

                UpdateStatus(opd, request_status);
            }

            if (Constant.TransferToED.Contains(new_status_code) || Constant.Admitted.Contains(new_status_code))
            {
                var error = HandleTransferForAnotherDepartment(opd, request_datas, new_status_code, isUseHandOverCheckList);
                if (error != null)
                    return new
                    {
                        Code = HttpStatusCode.BadRequest,
                        Message = error
                    };
            }

            return null;
        }

        private dynamic OPDValiDatePIDAndVisitCode(OPD visit, string status_code)
        {
            if (Constant.InHospital.Contains(status_code) || Constant.WaitingResults.Contains(status_code) || Constant.NoExamination.Contains(status_code) || Constant.Nonhospitalization.Contains(status_code))
                return null;

            string visitCode = visit.VisitCode;
            var custumer = visit.Customer;
            string pId = custumer?.PID;
            if (string.IsNullOrEmpty(visitCode) && string.IsNullOrEmpty(pId))
            {
                string en_err = "Please sync to the visit code and PID of the patient!";
                string vi_err = "Vui lòng đồng bộ lượt tiếp nhận và PID của NB!";
                return new
                {
                    Code = HttpStatusCode.NotFound,
                    Message = new
                    {
                        IsErorr = true,
                        EnMessage = en_err,
                        ViMessage = vi_err
                    }
                };
            }

            if (string.IsNullOrEmpty(visitCode))
            {
                string en_err = "Please sync to the visit code of the patient!";
                string vi_err = "Vui lòng đồng bộ lượt tiếp nhận của NB!";
                return new
                {
                    Code = HttpStatusCode.NotFound,
                    Message = new
                    {
                        IsErorr = true,
                        EnMessage = en_err,
                        ViMessage = vi_err
                    }
                };
            }

            if (string.IsNullOrEmpty(pId))
            {
                string en_err = "Please sync to the PID of the patient!";
                string vi_err = "Vui lòng đồng bộ PID của NB!";
                return new
                {
                    Code = HttpStatusCode.NotFound,
                    Message = new
                    {
                        IsErorr = true,
                        EnMessage = en_err,
                        ViMessage = vi_err
                    }
                };
            }

            return null;
        }

        private dynamic IsChangeTransferedStatus(OPD opd, JToken jToken, EDStatus current_status, string new_status_code)
        {
            string newStatus_code = new_status_code;
            string newTransfered = "";
            string oldStatus_code = current_status.Code;
            string oldTransfered = "";
            var visit_transfer = get_visit_transfer(opd);
            if (Constant.TransferToED.Contains(new_status_code))
            {
                newTransfered = jToken.FirstOrDefault(d => d.Value<string>("Code") == "OPDOENREC2ANS").Value<string>("Value");
            }
            else
            {
                newTransfered = jToken.FirstOrDefault(d => d.Value<string>("Code") == "OPDOENRECANS").Value<string>("Value");
            }

            if (Constant.TransferToED.Contains(oldStatus_code))
            {
                oldTransfered = opd.OPDOutpatientExaminationNote.OPDOutpatientExaminationNoteDatas.FirstOrDefault(d => d.Code == "OPDOENREC2ANS")?.Value;
            }
            else
            {
                oldTransfered = opd.OPDOutpatientExaminationNote.OPDOutpatientExaminationNoteDatas.FirstOrDefault(d => d.Code == "OPDOENRECANS")?.Value;
            }

            if (visit_transfer != null)
            {
                if (oldStatus_code != newStatus_code) return visit_transfer;
                if (oldTransfered != newTransfered) return visit_transfer;
            }
            return null;
        }
        private dynamic get_visit_transfer(OPD opd)
        {
            var visit_transfer = new VisitTransfer(unitOfWork);
            if (visit_transfer.IsExist(opd.OPDHandOverCheckListId, opd.Id))
                return visit_transfer.BuildMessage();
            return null;
        }
        private bool IsConfirmAllOrder(Guid opd_id)
        {
            var order = unitOfWork.OrderRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == opd_id &&
                !string.IsNullOrEmpty(e.OrderType) &&
                e.OrderType.Equals(Constant.OPD_STANDING_ORDER) &&
                !e.IsConfirm);
            if (order != null) return false;
            return true;
        }
        private void RemoveTransferIfExist(OPD opd)
        {
            var hocl = opd.OPDHandOverCheckList;
            if (hocl != null)
            {
                unitOfWork.OPDHandOverCheckListRepository.Delete(hocl);
                opd.OPDHandOverCheckListId = null;
                unitOfWork.OPDRepository.Update(opd);
                unitOfWork.Commit();
            }
        }
        private void UpdateStatus(OPD opd, JToken status)
        {
            var status_raw = status["Id"].ToString();
            Guid status_id = new Guid(status_raw);
            if (status_id != opd.EDStatusId)
            {
                opd.EDStatusId = status_id;
                var customer = opd.Customer;
                customer.EDStatusId = status_id;
                unitOfWork.OPDRepository.Update(opd);
                unitOfWork.CustomerRepository.Update(customer);
            }
            unitOfWork.Commit();
        }
        private dynamic HandleTransferForAnotherDepartment(OPD opd, JToken datas, string status_code, bool isUseHandOverCheckList)
        {
            Guid? receiving_id = null;
            string receiving = string.Empty;
            string reason = string.Empty;
            if (Constant.TransferToED.Contains(status_code))
            {
                receiving = datas.FirstOrDefault(d => d.Value<string>("Code") == "OPDOENREC2ANS").Value<string>("Value");
                UpdateOrCreateExaminationNoteData(opd.OPDOutpatientExaminationNoteId, "OPDOENREC2ANS", receiving);
                reason = datas.FirstOrDefault(d => d.Value<string>("Code") == "OPDOENRFT2ANS").Value<string>("Value");
                UpdateOrCreateExaminationNoteData(opd.OPDOutpatientExaminationNoteId, "OPDOENRFT2ANS", reason);
            }
            else
            {
                receiving = datas.FirstOrDefault(d => d.Value<string>("Code") == "OPDOENRECANS").Value<string>("Value");
                UpdateOrCreateExaminationNoteData(opd.OPDOutpatientExaminationNoteId, "OPDOENRECANS", receiving);
                reason = datas.FirstOrDefault(d => d.Value<string>("Code") == "OPDOENRFTANS").Value<string>("Value");
                UpdateOrCreateExaminationNoteData(opd.OPDOutpatientExaminationNoteId, "OPDOENRFTANS", reason);
            }
            try
            {
                receiving_id = new Guid(receiving);
            }
            catch (Exception)
            {
                return Message.TRANSFER_ERROR;
            }
            var specialty = opd.Specialty;
            var receiving_unit = unitOfWork.SpecialtyRepository.GetById((Guid)receiving_id);
            if (opd.OPDHandOverCheckListId == null)
                CreateHandOverCheckList(opd, specialty, receiving_unit, reason, isUseHandOverCheckList);
            else
            {
                if (opd.OPDHandOverCheckList?.ReceivingPhysicianId == null && opd.OPDHandOverCheckList?.ReceivingNurseId == null)
                {
                    UpdateHandOverCheckList(opd.OPDHandOverCheckList, specialty, receiving_unit, reason, isUseHandOverCheckList);
                }
            }
            unitOfWork.Commit();
            return null;
        }
        private void CreateHandOverCheckList(OPD opd, Specialty specialty, Specialty receiving, string reason, bool isUseHandOverCheckList)
        {
            OPDHandOverCheckList hocl = new OPDHandOverCheckList();
            hocl.HandOverTimePhysician = DateTime.Now;
            hocl.HandOverUnitNurseId = specialty.Id;
            hocl.HandOverUnitPhysicianId = specialty.Id;
            hocl.ReceivingUnitNurseId = receiving.Id;
            hocl.ReceivingUnitPhysicianId = receiving.Id;
            hocl.HandOverPhysicianId = GetUser()?.Id;
            hocl.ReasonForTransfer = reason;
            hocl.IsUseHandOverCheckList = isUseHandOverCheckList;

            if (!receiving.IsPublish)
            {
                hocl.IsAcceptNurse = true;
                hocl.IsAcceptPhysician = true;
            }
            unitOfWork.OPDHandOverCheckListRepository.Add(hocl);
            unitOfWork.Commit();
            opd.OPDHandOverCheckListId = hocl.Id;

            dynamic ia_id;
            dynamic allery_data;
            dynamic vital_sign;
            if (opd.IsTelehealth && opd.OPDInitialAssessmentForTelehealth?.UpdatedAt > opd.OPDInitialAssessmentForShortTerm?.UpdatedAt)
            {
                ia_id = opd.OPDInitialAssessmentForTelehealthId;
                allery_data = GetAllergyDatasForTelehealth(ia_id);
                vital_sign = GetVitalSignDatasForTelehealth(ia_id);
            }
            else
            {
                ia_id = opd.OPDInitialAssessmentForShortTermId;
                allery_data = GetAllergyDatasForOnGoing(ia_id);
                vital_sign = GetVitalSignDatasForOnGoing(ia_id);
            }

            foreach (var data in allery_data)
                UpdateAllergy(data.Code, data.Value, hocl);
            UpdateVitalSign(vital_sign, hocl.Id);
        }
        private void UpdateHandOverCheckList(OPDHandOverCheckList hocl, Specialty specialty, Specialty receiving, string reason, bool isUseHandOverCheckList)
        {
            hocl.HandOverUnitNurseId = specialty.Id;
            hocl.HandOverUnitPhysicianId = specialty.Id;
            hocl.ReceivingUnitNurseId = receiving.Id;
            hocl.ReceivingUnitPhysicianId = receiving.Id;
            hocl.ReasonForTransfer = reason;

            if (!receiving.IsPublish)
            {
                hocl.IsAcceptNurse = true;
                hocl.IsAcceptPhysician = true;
            }
            else
            {
                if (hocl.ReceivingNurseId == null && hocl.ReceivingPhysicianId == null)
                {
                    hocl.IsAcceptNurse = false;
                    hocl.IsAcceptPhysician = false;
                }
            }
            hocl.IsUseHandOverCheckList = isUseHandOverCheckList;
            unitOfWork.OPDHandOverCheckListRepository.Update(hocl);
        }


        private dynamic GetAllergyDatasForTelehealth(Guid ia_id)
        {
            return unitOfWork.OPDInitialAssessmentForTelehealthDataRepository.Find(
                e => !e.IsDeleted &&
                e.OPDInitialAssessmentForTelehealthId != null &&
                e.OPDInitialAssessmentForTelehealthId == ia_id &&
                !string.IsNullOrEmpty(e.Code) &&
                e.Code.Contains("OPDIAFTPALL")
            ).Select(e => new { e.Code, e.Value }).ToList();
        }
        private dynamic GetAllergyDatasForOnGoing(Guid ia_id)
        {
            return unitOfWork.OPDInitialAssessmentForShortTermDataRepository.Find(
                e => !e.IsDeleted &&
                e.OPDInitialAssessmentForShortTermId != null &&
                e.OPDInitialAssessmentForShortTermId == ia_id &&
                !string.IsNullOrEmpty(e.Code) &&
                e.Code.Contains("OPDIAFSTOPALL")
            ).Select(e => new { e.Code, e.Value }).ToList();
        }
        private void UpdateAllergy(string code, string value, OPDHandOverCheckList hoc)
        {
            OPDHandOverCheckListData new_all = new OPDHandOverCheckListData
            {
                OPDHandOverCheckListId = hoc.Id,
                Code = Constant.OPD_HOC_ALLERGIC_CODE_SWITCH[code],
                Value = value
            };
            unitOfWork.OPDHandOverCheckListDataRepository.Add(new_all);
        }


        private dynamic GetVitalSignDatasForTelehealth(Guid ia_id)
        {
            string accept_code = "OPDIAFTPPULANS,OPDIAFTPTEMANS,OPDIAFTPBP0ANS,OPDIAFTPSPOANS,OPDIAFTPRR0ANS";
            var vital_sign = (from data in unitOfWork.OPDInitialAssessmentForTelehealthDataRepository.AsQueryable()
                             .Where(
                                i => !i.IsDeleted &&
                                i.OPDInitialAssessmentForTelehealthId != null &&
                                i.OPDInitialAssessmentForTelehealthId == ia_id &&
                                !string.IsNullOrEmpty(i.Code) &&
                                accept_code.Contains(i.Code)
                              )
                              join master in unitOfWork.MasterDataRepository.AsQueryable() on data.Code equals master.Code into ulist
                              from master in ulist.DefaultIfEmpty()
                              select new { master.ViName, data.Value, master.Note }).ToList();
            return vital_sign;
        }
        private dynamic GetVitalSignDatasForOnGoing(Guid ia_id)
        {
            string accept_code = "OPDIAFSTOPPULANS,OPDIAFSTOPTEMANS,OPDIAFSTOPBP0ANS,OPDIAFSTOPSPOANS,OPDIAFSTOPRR0ANS";
            var vital_sign = (from data in unitOfWork.OPDInitialAssessmentForShortTermDataRepository.AsQueryable()
                             .Where(
                                i => !i.IsDeleted &&
                                i.OPDInitialAssessmentForShortTermId != null &&
                                i.OPDInitialAssessmentForShortTermId == ia_id &&
                                !string.IsNullOrEmpty(i.Code) &&
                                accept_code.Contains(i.Code)
                             )
                              join master in unitOfWork.MasterDataRepository.AsQueryable() on data.Code equals master.Code into ulist
                              from master in ulist.DefaultIfEmpty()
                              select new { master.ViName, data.Value, master.Note }).ToList();
            return vital_sign;
        }
        private void UpdateVitalSign(dynamic data, Guid hoc_id)
        {
            string vital_sign = JoinVitalSign(data);
            var val = "True";
            if (string.IsNullOrEmpty(vital_sign)) val = "False";

            OPDHandOverCheckListData vs_yes = new OPDHandOverCheckListData
            {
                OPDHandOverCheckListId = hoc_id,
                Code = "OPDHOCVS0YES",
                Value = val
            };
            unitOfWork.OPDHandOverCheckListDataRepository.Add(vs_yes);
            OPDHandOverCheckListData vs_no = new OPDHandOverCheckListData
            {
                OPDHandOverCheckListId = hoc_id,
                Code = "OPDHOCVS0NOO",
                Value = "False"
            };
            unitOfWork.OPDHandOverCheckListDataRepository.Add(vs_no);
            OPDHandOverCheckListData vs_ans = new OPDHandOverCheckListData
            {
                OPDHandOverCheckListId = hoc_id,
                Code = "OPDHOCVS0ANS",
                Value = vital_sign,
            };
            unitOfWork.OPDHandOverCheckListDataRepository.Add(vs_ans);
        }
        private string JoinVitalSign(dynamic data)
        {
            string result = string.Empty;
            foreach (var item in data)
            {
                if (!string.IsNullOrEmpty(item.Value))
                    result += string.Format("{0}: {1} {2}, ", item.ViName, item.Value, item.Note);
            }
            if (!string.IsNullOrEmpty(result))
            {
                result = result.TrimEnd();
                result = result.Remove(result.Length - 1);
            }
            return result;
        }


        private string GetAllergyInInitialAssessment(OPD opd)
        {
            return opd.Allergy;
        }
        private string GetAllergyInInitialAssessmentForTelehealth(OPD opd)
        {
            var ia = opd.OPDInitialAssessmentForTelehealth;
            var ia_data = ia.OPDInitialAssessmentForTelehealthDatas.Where(e => !e.IsDeleted);

            var all = ia_data.FirstOrDefault(
                e => !string.IsNullOrEmpty(e.Code) &&
                e.Code.Contains("OPDIAFTPALLANS")
            )?.Value;
            if (!string.IsNullOrEmpty(all))
                return all;

            var koa = ia_data.FirstOrDefault(
                e => !string.IsNullOrEmpty(e.Code) &&
                e.Code.Contains("OPDIAFTPALLKOA")
            )?.Value;
            if (!string.IsNullOrEmpty(koa))
            {
                var koa_value = "";
                foreach (var i in koa.Split(','))
                    koa_value += $"{Constant.KIND_OF_ALLERGIC[i]}, ";
                return koa_value.Substring(0, koa_value.Length - 2);
            }

            var npa = ia_data.FirstOrDefault(
                e => !string.IsNullOrEmpty(e.Code) &&
                e.Code.Contains("OPDIAFTPALLNPA")
            )?.Value;
            if (!string.IsNullOrEmpty(npa) && npa.Trim().ToLower() == "true")
                return "Không xác định";

            return "Không";
        }
        private string GetAllergyInInitialAssessmentForShortTerm(OPD opd)
        {
            var ia = opd.OPDInitialAssessmentForShortTerm;
            var ia_data = ia.OPDInitialAssessmentForShortTermDatas.Where(e => !e.IsDeleted);

            var all = ia_data.FirstOrDefault(
                    e => !string.IsNullOrEmpty(e.Code) &&
                    e.Code.Contains("OPDIAFSTOPALLANS")
                )?.Value;
            if (!string.IsNullOrEmpty(all))
                return all;

            var koa = ia_data.FirstOrDefault(
                e => !string.IsNullOrEmpty(e.Code) &&
                e.Code.Contains("OPDIAFSTOPALLKOA")
            )?.Value;
            if (!string.IsNullOrEmpty(koa))
            {
                var koa_value = "";
                foreach (var i in koa.Split(','))
                    koa_value += $"{Constant.KIND_OF_ALLERGIC[i]}, ";
                return koa_value.Substring(0, koa_value.Length - 2);
            }

            var npa = ia_data.FirstOrDefault(
                e => !string.IsNullOrEmpty(e.Code) &&
                e.Code.Contains("OPDIAFSTOPALLNPA")
            )?.Value;
            if (!string.IsNullOrEmpty(npa) && npa.Trim().ToLower() == "true")
                return "Không xác định";

            return "Không";
        }
        private bool IsCorrectDoctor(Guid? user_id, Guid? primary_doctor_id)
        {
            if (IsSuperman()) return true;
            if (user_id == null || primary_doctor_id == null || user_id != primary_doctor_id)
            {
                return false;
            }
            return true;
        }
        private void CreatedOPDChangingNotification(User from_user, string to_user, OPD opd, string form_name, string form_code)
        {
            var spec = opd.Specialty;
            var customer = opd.Customer;
            var vi_mes = string.Format(
                "<b>[OPD - {0}] {1}</b> của bệnh nhân <b>{2}</b> đã được chỉnh sửa bởi <b>{3}</b> ({4})",
                spec?.ViName,
                form_name,
                customer?.Fullname,
                from_user.Fullname,
                from_user.Title
            );
            var en_mes = string.Format(
                "<b>[OPD - {0}] {1}</b> của bệnh nhân <b>{2}</b> đã được chỉnh sửa bởi <b>{3}</b> ({4})",
                spec?.ViName,
                form_name,
                customer?.Fullname,
                from_user.Fullname,
                from_user.Title
            );

            var noti_creator = new NotificationCreator(
                unitOfWork: unitOfWork,
                from_user: from_user.Username,
                to_user: to_user,
                priority: 2,
                vi_message: vi_mes,
                en_message: en_mes,
                spec_id: spec?.Id,
                visit_id: opd.Id,
                group_code: "OPD",
                form_frontend: form_code
            );
            noti_creator.Create();
        }
        public dynamic GetStatus(bool is_transfer)
        {
            if (is_transfer)
                return unitOfWork.EDStatusRepository.Find(e => !e.IsDeleted && e.VisitTypeGroupId != null && e.VisitTypeGroup.Code == "OPD" && e.Code != "OPDNE").OrderBy(e => e.CreatedAt).Select(st => new { st.Id, st.ViName, st.EnName, st.Code }).ToList();
            return unitOfWork.EDStatusRepository.Find(e => !e.IsDeleted && e.VisitTypeGroupId != null && e.VisitTypeGroup.Code == "OPD").OrderBy(e => e.CreatedAt).Select(st => new { st.Id, st.ViName, st.EnName, st.Code }).ToList();
        }
        public bool PreAnesthe(OPD opd)
        {
            return unitOfWork.OPDPreAnesthesiaHandOverCheckListRepository.Count(e => e.IsDeleted && (e.IsAcceptNurse || e.IsAcceptPhysician) && e.Id == opd.TransferFromId) > 0;
            //var check = (from op in unitOfWork.OPDPreAnesthesiaHandOverCheckListRepository.AsQueryable()
            //            .Where(e => !e.IsDeleted && (e.IsAcceptNurse || e.IsAcceptPhysician))
            //             join oo in unitOfWork.OPDRepository.AsQueryable().Where(o => !o.IsDeleted && o.Id == opd.Id)
            //             on op.Id equals oo.TransferFromId

            //             select new { }
            //            ).FirstOrDefault();
        }
        private void CreateAuthorizedDoctorNotification(User from_user, User to_user, OPD opd, string form_code)
        {
            var spec = opd.Specialty;
            var customer = opd.Customer;
            var vi_mes = string.Format(
                "<b>[OPD - {0}] Bệnh nhân <b>{1}</b> ({2}) đã được ủy quyền cho bác sĩ <b>{3}</b>",
                spec?.ViName,
                customer?.Fullname,
                customer?.PID,
                to_user.Fullname
            );
            var en_mes = string.Format(
                "<b>[OPD - {0}] Bệnh nhân <b>{1}</b> ({2}) đã được ủy quyền cho bác sĩ <b>{3}</b>",
                spec?.ViName,
                customer?.Fullname,
                customer?.PID,
                to_user.Fullname
            );

            var noti_creator = new NotificationCreator(
                unitOfWork: unitOfWork,
                from_user: from_user.Username,
                to_user: to_user.Username,
                priority: 2,
                vi_message: vi_mes,
                en_message: en_mes,
                spec_id: spec?.Id,
                visit_id: opd.Id,
                group_code: "OPD",
                form_frontend: form_code
            );
            noti_creator.Create();
        }

        private List<string> GetCodeFromSetUpOrForm(Clinic clinic, List<OPDOutpatientExaminationNoteData> datas)
        {
            List<string> result = new List<string>();
            string dataFromSetUp = clinic?.SetUpClinicDatas;
            if (!string.IsNullOrEmpty(dataFromSetUp))
                result.AddRange(dataFromSetUp.Split(','));

            var dataFrom = (from d in datas
                            where !d.IsDeleted
                            && d.Code == code_setupClinic
                            select d).FirstOrDefault();
            if (dataFrom == null || string.IsNullOrEmpty(dataFrom?.Value))
                return result;

            return new List<string>(dataFrom.Value.Split(','));
        }

        private void HandleCodeSetupClinic(OPDOutpatientExaminationNote oen, JToken codes_setup)
        {
            if (oen.Version < 2)
                return;

            string value_setup = String.Join(",", codes_setup);
            var obj = oen.OPDOutpatientExaminationNoteDatas.Where(
                        e => !e.IsDeleted && e.Code == code_setupClinic
                      ).FirstOrDefault();

            // lưu setup khi còn mới 
            if ((obj == null && IsNew(oen.CreatedAt, oen.UpdatedAt)) || string.IsNullOrEmpty(obj?.Value))
                unitOfWork.OPDOutpatientExaminationNoteDataRepository.Add(
                        new OPDOutpatientExaminationNoteData()
                        {
                            OPDOutpatientExaminationNoteId = oen.Id,
                            Code = code_setupClinic,
                            Value = value_setup
                        }
                        );
            unitOfWork.Commit();
        }

        // chưa lưu thì set phiếu khám lên version 2
        private void SetVersionForFormIsNew(OPDOutpatientExaminationNote eon)
        {
            if (IsNew(eon.CreatedAt, eon.UpdatedAt))
                eon.Version = 2;
        }

        private class OPDVisitHistoryForOutpatientExaminationNote : VisitHistory
        {
            private OPD visit;
            private string site_code;
            public OPDVisitHistoryForOutpatientExaminationNote()
            {
            }

            public OPDVisitHistoryForOutpatientExaminationNote(OPD visit, string site_code)
            {
                this.visit = visit;
                this.site_code = site_code;
            }

            private List<VisitModel> GetEOCHistory(Guid customer_id, DateTime visit_time)
            {
                var eocs = (from visit in unitOfWork.EOCRepository.AsQueryable().Where(
                                     e => !e.IsDeleted &&
                                     e.AdmittedDate < visit_time &&
                                     e.CustomerId != null &&
                                     e.CustomerId == customer_id &&
                                     e.SpecialtyId != null &&
                                     !string.IsNullOrEmpty(e.VisitCode)
                             )
                            join form in unitOfWork.EOCOutpatientExaminationNoteRepository.AsQueryable()
                            on visit.Id equals form.VisitId into forms
                            from f in forms.DefaultIfEmpty()
                            join formdata in unitOfWork.FormDatasRepository.AsQueryable()
                            on f.Id equals formdata.FormId
                            where new string[] { "OPDOENHPIANS", "OPDOENPMHANS" }.Contains(formdata.Code)
                            let vi = visit.PrimaryDoctor
                            select new VisitModel()
                            {
                                ExaminationTime = visit.AdmittedDate,
                                Username = visit.PrimaryDoctor == null ? null : visit.PrimaryDoctor.Username,
                                Fullname = visit.PrimaryDoctor == null ? null : visit.PrimaryDoctor.Fullname,
                                ViName = visit.Specialty == null ? null : visit.Specialty.ViName,
                                EnName = visit.Specialty == null ? null : visit.Specialty.EnName,
                                PastMedicalHistory = formdata.Value,
                                FamilyMedicalHistory = "",
                                HistoryOfPresentIllness = formdata.Value,
                                HistoryOfAllergies = visit.Allergy,
                                VisitCode = visit.VisitCode,
                                EHOSVisitCode = vi == null ? null : vi.EHOSAccount + visit.VisitCode,
                                Type = "AD",
                            }).ToList();


                return eocs.GroupBy(e => e.ExaminationTime.ToString()).Select(e => e.FirstOrDefault()).ToList();
                //return eds.GroupBy(e => e.ExaminationTime.ToString()).Select(e => e.FirstOrDefault()).ToList();
            }

            private List<VisitModel> GetHistoryFromEDAmbulanceRunReport(Guid customer_id, DateTime visit_time)
            {
                var eds = (from visit in unitOfWork.EDRepository.AsQueryable().Where(
                            e => !e.IsDeleted &&
                            e.AdmittedDate < visit_time &&
                            e.CustomerId != null &&
                            e.CustomerId == customer_id &&
                            e.SpecialtyId != null &&
                            !string.IsNullOrEmpty(e.VisitCode)
                          )
                           join hpi in unitOfWork.EDAmbulanceRunReportDataRepository.AsQueryable()
                             on new { visit.EDAmbulanceRunReportId, Code = "EDARRPAMAHOPIANS" } equals new { hpi.EDAmbulanceRunReportId, hpi.Code } into hpi_list
                           from history_of_present_illness in hpi_list.DefaultIfEmpty()
                           join di in unitOfWork.DischargeInformationRepository.AsQueryable()
                             on visit.DischargeInformationId equals di.Id
                           join doc in unitOfWork.UserRepository.AsQueryable()
                             on di.UpdatedBy equals doc.Username into doctor_list
                           from doctor in doctor_list.DefaultIfEmpty()
                           join spec in unitOfWork.SpecialtyRepository.AsQueryable()
                             on visit.SpecialtyId equals spec.Id into spec_list
                           from specialty in spec_list.DefaultIfEmpty()
                           select new VisitModel()
                           {
                               Id = visit.Id,
                               ExaminationTime = visit.AdmittedDate,
                               Username = doctor.Username,
                               Fullname = doctor.Fullname,
                               ViName = specialty.ViName,
                               EnName = specialty.EnName,
                               PastMedicalHistory = "",
                               FamilyMedicalHistory = "",
                               HistoryOfPresentIllness = history_of_present_illness.Value,
                               HistoryOfAllergies = visit.Allergy,
                               VisitCode = visit.VisitCode,
                               EHOSVisitCode = doctor.EHOSAccount + visit.VisitCode,
                               Type = "AD",
                           }).ToList();
                return eds.GroupBy(e => e.ExaminationTime.ToString()).Select(e => e.FirstOrDefault()).ToList();
            }


            public override List<VisitModel> GetHistory()
            {
                List<VisitModel> result = new List<VisitModel>();
                List<string> list_visit_code = new List<string>();
                var customer = this.visit.Customer;

                List<VisitModel> eds = GetEDHistory(customer.Id, this.visit.AdmittedDate);
                list_visit_code.AddRange(eds.Select(e => e.EHOSVisitCode).ToList());
                result.AddRange(eds);

                string current_visit = $"{this.visit.PrimaryDoctor?.EHOSAccount}{this.visit.VisitCode}";
                List<VisitModel> opds = GetOPDHistory(customer.Id, this.visit.AdmittedDate);
                list_visit_code.AddRange(opds.Select(e => e.EHOSVisitCode).ToList());

                result.AddRange(opds.Where(e => e.EHOSVisitCode != current_visit).ToList());

                List<VisitModel> ipds = GetIPDHistory(customer.Id, this.visit.AdmittedDate); 
                list_visit_code.AddRange(ipds.Select(e => e.EHOSVisitCode).ToList());
                result.AddRange(ipds);

                List<VisitModel> eocs = GetEOCHistory(customer.Id, this.visit.AdmittedDate);
                list_visit_code.AddRange(eocs.Select(e => e.EHOSVisitCode).ToList());
                result.AddRange(eocs);

                List<VisitModel> painrecord = GetHistoryPainRecord(customer.Id, this.visit.AdmittedDate, PainRecordFormCode);
                list_visit_code.AddRange(painrecord.Select(e => e.EHOSVisitCode).ToList());
                result.AddRange(painrecord);

                //List<VisitModel> his_result;
                //if (this.site_code == "times_city" && !string.IsNullOrEmpty(customer.PIDEhos))
                //{
                //    his_result = EHosClient.GetVisitHistory(customer.PIDEhos, this.visit.AdmittedDate);
                //}
                //else
                //{
                //    his_result = OHClient.GetVisitHistory(customer.PID, this.visit.AdmittedDate);
                //}
                //his_result = his_result.Where(e => !list_visit_code.Contains(e.EHOSVisitCode)).ToList();
                //result.AddRange(his_result);

                result.AddRange(GetHistoryFromEDAmbulanceRunReport(customer.Id, this.visit.AdmittedDate));
                return result.OrderByDescending(e => e.ExaminationTime).Distinct().ToList();
            }

            public override List<VisitModel> GetPromCurrentForCoronaryDiseases(Guid visitId)
            {
                throw new NotImplementedException();
            }

            public override List<VisitModel> GetPromCurrentForheartFailures(Guid visitId)
            {
                throw new NotImplementedException();
            }

            public override List<VisitModel> GetPromForCoronaryDiseasesHistory()
            {
                throw new NotImplementedException();
            }

            public override List<VisitModel> GetPromForheartFailuresHistory()
            {
                throw new NotImplementedException();
            }

            public override List<VisitModel> GetPersonalHistory(Guid visitId)
            {
                throw new NotImplementedException();
            }
        }        
    }
}