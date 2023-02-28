using DataAccess.Models;
using DataAccess.Models.EOCModel;
using DataAccess.Repository;
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
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.EOCControllers
{
    [SessionAuthorize]
    public class EOCOutpatientExaminationNoteController : BaseEOCApiController
    {
        private readonly string formCode = "OPDOEN";
        // GET: EOCOutpatientExaminationNote
        [HttpGet]
        [Route("api/eoc/OutpatientExaminationNote/{id}")]
        [Permission(Code = "EOC039")]
        public IHttpActionResult Get(Guid id)
        {
            var visit = GetEOC(id);
            if (visit == null) return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);
            var status = visit.Status;
            var primary_doctor = visit.PrimaryDoctor;
            var form = GetForm(id);
            if (form == null) return Content(HttpStatusCode.NotFound, new {
                PrimaryDoctor = new
                {
                    primary_doctor?.Id,
                    primary_doctor?.Fullname,
                    primary_doctor?.Username
                },
                Status = new { status.Id, status.ViName, status.EnName, status.Code },
                ListStatus = GetStatus(false).FindAll(e => Constant.EOC_WITH_WITHOUT_OEN_FORM.Contains(e.Code)).Select(s => new { s.Id, s.ViName, s.EnName, s.Code }),
            });

            var user = GetUser();

           
            var clinic = clinic_code;
            // ConfigurationManager.AppSettings["DevWriteLists"]
            
            var authorized_doctor = visit.AuthorizedDoctor;
            var visit_transfer = get_visit_transfer(visit);
            var eocHandOverCheckList = GetHandOverCheckList(visit.Id);
            var locked = false;

            //status?.EnName != "Waiting results" && (user.Username == primary_doctor?.Username) && Is24hLocked(form.CreatedAt, visit.Id, formCode, user.Username);

            return Ok(new
            {
                locked,
                visit_transfer,
                form.Id,
                IsNew = IsNew(form.CreatedAt, form.UpdatedAt),
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
                Clinic = new { Code = clinic },
                ExaminationTime = form.ExaminationTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                Status = new { status.Id, status.ViName, status.EnName, status.Code },
                OPDId = visit.Id,
                Datas = GetFormData(visit.Id, form.Id, formCode),
                Customer = GetCustomerInfoInVisit(visit, "EOC"),
                ListStatus = GetStatus(visit.IsTransfer).Select(s => new { s.Id, s.ViName, s.EnName, s.Code }),
                TransferFrom = GetTransferFrom(visit),
                IsUseHandOverCheckList = eocHandOverCheckList?.IsUseHandOverCheckList,
                IsAcceptPhysician = eocHandOverCheckList?.IsAcceptPhysician,
                IsAcceptNurse = eocHandOverCheckList?.IsAcceptNurse,
                UpdatedAt = form.UpdatedAt?.ToString("HH:mm dd/MM/yyyy"),
                form.UpdatedBy,
                form.Version,
                IsCheckTranfer = IsCheckHandOverCheckList(id)
        });
        }
        
        [HttpPost]
        [CSRFCheck]
        [Route("api/eoc/OutpatientExaminationNote/Create/{id}")]
        [Permission(Code = "EOC040")]
        public IHttpActionResult Post(Guid id)
        {
            if (!IsSuperman() && !IsDoctor()) return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);

            var visit = GetEOC(id);
            if (visit == null) return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);

            var user = GetUser();

            if (!IsSuperman() && user.Username != visit.PrimaryDoctor?.Username) return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);

            var form = GetForm(id);
            if (form != null) return Content(HttpStatusCode.BadRequest, Message.FORM_EXIST);

            var form_data = new EOCOutpatientExaminationNote
            {
                VisitId = id,
                Version = 2
            };
            unitOfWork.EOCOutpatientExaminationNoteRepository.Add(form_data);
            // visit.StatusId = GetStatusIdByCode("EOCIH");
            UpdateVisit(visit);
            unitOfWork.Commit();
            return Content(HttpStatusCode.OK, new { form_data.Id });
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/eoc/OutpatientExaminationNote/ChangeStatus/{id}")]
        [Permission(Code = "EOC041")]
        public IHttpActionResult ChangeStatus(Guid id, [FromBody] JObject request)
        {
            if (!IsDoctor()) return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);

            var visit = GetEOC(id);
            if (visit == null) return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);

            var user = GetUser();

            if (user.Username != visit.PrimaryDoctor?.Username) return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);

            var form = GetForm(id);            
            if (form == null)
            {
                var is_error = HandleUpdateStatus(visit, request["Status"]);
                if (is_error != null)
                {
                    return Content(is_error.Code, is_error.Message);
                }
            }
            UpdateVisit(visit);
            unitOfWork.Commit();
            return Content(HttpStatusCode.OK, new { visit.Id });
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/eoc/OutpatientExaminationNote/Update/{id}")]
        [Permission(Code = "EOC041")]
        public IHttpActionResult UpdateExaminationNoteAPI(Guid id, [FromBody]JObject request)
        {
            if (!IsSuperman() && !IsDoctor()) return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);

            var visit = GetEOC(id);
            if (visit == null) return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);          
            
            var user = GetUser();

            if (!IsSuperman() && (user.Username != visit.PrimaryDoctor?.Username && user.Username != visit.AuthorizedDoctor?.Username)) return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);

            var form = GetForm(id);
            if (form == null) return Content(HttpStatusCode.BadRequest, Message.FORM_NOT_FOUND);
            HandleUpdateOrCreateFormDatas(id, form.Id, formCode, request["Datas"], null, IsCheckHandOverCheckList(visit.Id));

            HandleUpdateExaminationTime(form, request["ExaminationTime"]?.ToString());

            if (visit.AuthorizedDoctorId != null && visit.AuthorizedDoctorId == user.Id)
                form.IsAuthorizeDoctorChangeForm = true;


            if (visit.AuthorizedDoctorId != null && visit.AuthorizedDoctorId == user.Id)
                form.IsDoctorChangeForm = true;

            if (visit.PrimaryDoctorId != null && visit.PrimaryDoctorId == user.Id)
                form.IsDoctorChangeForm = true;

            unitOfWork.EOCOutpatientExaminationNoteRepository.Update(form);
            UpdateVisit(visit);
            unitOfWork.Commit();

            bool isUseHandOverCheckList = false;

            try
            {
                isUseHandOverCheckList = Convert.ToBoolean(request["IsUseHandOverCheckList"]);
            }
            catch (Exception)
            {
                isUseHandOverCheckList = false;
            }
            var is_error = HandleUpdateStatus(visit, request["Status"]);
            if (is_error != null)
            {
                //unitOfWork.Commit();
                return Content(is_error.Code, is_error.Message);
            }
            if (isUseHandOverCheckList)
            {
                var receiving_id = GetReceivingUnitPhysicianId(visit);
                var receiving = unitOfWork.SpecialtyRepository.GetById((Guid)receiving_id);
                var oen = GetOutpatientExaminationNote(id);
                var eocHandOverCheckList = GetHandOverCheckList(visit.Id);
                if (eocHandOverCheckList == null)
                {

                    var form_data = new EOCHandOverCheckList();
                    {
                        form_data.VisitId = id;
                        form_data.HandOverPhysicianId = visit.PrimaryDoctorId;
                        form_data.HandOverUnitPhysicianId = visit.SpecialtyId;
                        form_data.HandOverNurseId = GetUser().Id;
                        form_data.HandOverTimeNurse = DateTime.Now;
                        form_data.HandOverUnitNurseId = visit.SpecialtyId;
                        form_data.ReceivingUnitPhysicianId = receiving?.Id;
                        form_data.ReceivingUnitNurseId = receiving?.Id;
                        form_data.HandOverTimePhysician = oen.UpdatedAt;
                        form_data.IsUseHandOverCheckList = isUseHandOverCheckList;

                    };
                    unitOfWork.EOCHandOverCheckListRepository.Add(form_data);
                    if (!receiving.IsPublish)
                    {
                        form_data.IsAcceptNurse = true;
                        form_data.IsAcceptPhysician = true;
                    }
                }
                else
                {
                    if (eocHandOverCheckList.ReceivingPhysicianId == null && eocHandOverCheckList.ReceivingNurseId == null)
                    {
                        eocHandOverCheckList.IsUseHandOverCheckList = isUseHandOverCheckList;
                        eocHandOverCheckList.ReceivingUnitPhysicianId = receiving?.Id;
                        eocHandOverCheckList.ReceivingUnitNurseId = receiving?.Id;
                        eocHandOverCheckList.HandOverUnitPhysicianId = visit.SpecialtyId;

                        if (!receiving.IsPublish)
                        {
                            eocHandOverCheckList.IsAcceptNurse = true;
                            eocHandOverCheckList.IsAcceptPhysician = true;
                        }
                        else
                        {
                            if (eocHandOverCheckList.ReceivingNurseId == null && eocHandOverCheckList.ReceivingPhysicianId == null)
                            {
                                eocHandOverCheckList.IsAcceptNurse = false;
                                eocHandOverCheckList.IsAcceptPhysician = false;
                            }
                        }
                        unitOfWork.EOCHandOverCheckListRepository.Update(eocHandOverCheckList);
                    }
                }
            }
            unitOfWork.Commit();
            HandleDischargeDate(visit);
            var chronic_util = new ChronicUtil(unitOfWork, visit.Customer);
            chronic_util.UpdateChronic();
            return Content(HttpStatusCode.OK, new { visit.Customer.IsChronic, form.Id });
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/eoc/OutpatientExaminationNote/SyncDiagnosisAndICD/{id}")]
        [Permission(Code = "EOC039")]
        public IHttpActionResult SyncDiagnosisAndICDAPI(Guid id)
        {
            var visit = GetEOC(id);
            if (visit == null) return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);

            var customer = visit.Customer;
            if (string.IsNullOrEmpty(customer.PID))
                return Content(HttpStatusCode.NotFound, Message.PID_IS_MISSING);

            var form = GetForm(id);
            if (form == null) return Content(HttpStatusCode.BadRequest, Message.FORM_NOT_FOUND);

            var site_code = visit.Site.Code;

            dynamic result;
            var primary_doctor = visit.PrimaryDoctor;
            if (primary_doctor == null)
                return Content(HttpStatusCode.BadRequest, Message.PRIMARY_DOCTOR_NOT_FOUND);

            if (site_code == "times_city")
            {
                if (string.IsNullOrEmpty(primary_doctor?.EHOSAccount))
                    return Content(HttpStatusCode.NotFound, Message.EHOS_ACCOUNT_MISSING);

                result = EHosClient.GetDiagnosisAndICD(visit.Customer.PID, visit.VisitCode, $"{primary_doctor?.EHOSAccount}");
            }
            else
                result = OHClient.GetDiagnosisAndICD(visit.Customer.PID, visit.VisitCode, $"{primary_doctor?.Username}");

            return Content(HttpStatusCode.OK, result);
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/EOC/OutpatientExaminationNote/SyncVisitHistory/")]
        [Permission(Code = "EOC039")]
        public IHttpActionResult SyncVisitHistoryAPI([FromBody] JObject request)
        {
            if (request == null || string.IsNullOrEmpty(request["Id"]?.ToString()))
            {
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);
            }

            var id = new Guid(request["Id"]?.ToString());
            var opd = GetEOC(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var customer = opd.Customer;
            if (customer == null || customer.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);
            else if (customer.PID == null)
                return Content(HttpStatusCode.NotFound, Message.PID_IS_MISSING);

            var oen = GetForm(id);
            if (oen == null || oen.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.OPD_OEN_NOT_FOUND);

            var site_code = GetSiteCode();
            if (string.IsNullOrEmpty(site_code))
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);

            VisitHistory visit_history = VisitHistoryFactory.GetVisit("EOC", opd, site_code);
            var visit_history_list = visit_history.GetHistory();

            return Content(HttpStatusCode.OK, new
            {
                PastMedicalHistory = visit_history_list.Where(e => !string.IsNullOrEmpty(e.PastMedicalHistory)).Select(e => formatPastMedicalHistory(e)),
                HistoryOfAllergies = visit_history_list.Where(e => !string.IsNullOrEmpty(e.HistoryOfAllergies)).Select(e => new {
                    ExaminationTime = e.ExaminationTime.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                    e.ViName,
                    e.EnName,
                    e.Fullname,
                    e.Username,
                    e.HistoryOfAllergies,
                    e.Type
                }),
                InitialAssessmentAllergies = GetAllergyInInitialAssessment(opd),
                HistoryOfPresentIllness = visit_history_list.Where(e => !string.IsNullOrEmpty(e.HistoryOfPresentIllness)).Select(e => new {
                    ExaminationTime = e.ExaminationTime.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                    e.ViName,
                    e.EnName,
                    e.Fullname,
                    e.Username,
                    e.HistoryOfPresentIllness,
                    e.Type
                }),
            });
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/EOC/OutpatientExaminationNote/AuthorizeDoctor/")]
        [Permission(Code = "EOC042")]
        public IHttpActionResult AuthorizeDoctorAPI([FromBody] JObject request)
        {
            if (!IsDoctor()) return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);
            var id = new Guid(request["Id"]?.ToString());
            var opd = GetEOC(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var user = GetUser();

            if (user.Username != opd.PrimaryDoctor?.Username) return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);

            var oen = GetForm(id);
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
                unitOfWork.EOCRepository.Update(opd);
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
            unitOfWork.EOCRepository.Update(opd);
            unitOfWork.Commit();

            // CreateAuthorizedDoctorNotification(user, authoried_doctor, opd, "OutpatientExaminationNote");

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        private void HandleDischargeDate(EOC eoc)
        {
            eoc.DischargeDate = GetDischargeDate(eoc);
            unitOfWork.Commit();
        }
        public dynamic GetTransferFrom(EOC eoc)
        {
            if (eoc.IsTransfer && eoc.TransferFromId != null)
            {
                var visit = GetVisit((Guid)eoc.TransferFromId, eoc.TransferFromType);
                return new
                {
                    visit.Id,
                    visit.Specialty.ViName,
                    visit.Specialty.EnName
                };
            }
            return null;
        }
        public List<EDStatus> GetStatus(bool is_transfer)
        {
            if (is_transfer)
                return unitOfWork.EDStatusRepository.Find(e => !e.IsDeleted && e.VisitTypeGroupId != null && Constant.EOC_WITH_TRANFER_STATUS_CODE.Contains(e.Code)).OrderBy(e => e.CreatedAt).ToList();
            return unitOfWork.EDStatusRepository.Find(e => !e.IsDeleted && e.VisitTypeGroupId != null && e.VisitTypeGroup.Code == "EOC").OrderBy(e => e.CreatedAt).ToList();
        }
        private bool IsWaitingPrincipalTest(EDStatus status, DateTime? created_at)
        {
            var block_time = created_at?.AddDays(1);
            var now = DateTime.Now;
            return now > block_time &&
                status != null &&
                !string.IsNullOrEmpty(status.Code) &&
                Constant.WaitingResults.Contains(status.Code);
        }
        private bool IsCorrectDoctor(Guid? user_id, Guid? primary_doctor_id)
        {
            if (user_id == null || primary_doctor_id == null || user_id != primary_doctor_id)
            {
                return false;
            }
            return true;
        }
        
        private string GetAllergyInInitialAssessment(EOC visit)
        {
            return GetAllergyInInitialAssessmentForShortTerm(visit);
        }
        private string GetAllergyInInitialAssessmentForShortTerm(EOC visit)
        {
            var form = unitOfWork.EOCInitialAssessmentForShortTermRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == visit.Id);
            if (form == null)
                return String.Empty;

            var ia_data = GetFormData(visit.Id, form.Id, "OPDIAFSTOP");

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
        private EOCOutpatientExaminationNote GetForm(Guid VisitId)
        {
            var form = unitOfWork.EOCOutpatientExaminationNoteRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == VisitId);
            return form;
        }
        private dynamic get_visit_transfer(EOC visit)
        {
            var hocl = GetHandOverCheckList(visit.Id);
            if (hocl != null) {
                var visit_transfer = new VisitTransfer(unitOfWork);
                if (visit_transfer.IsExist(hocl.Id, visit.Id))
                    return visit_transfer.BuildMessage();
            }
            return null;
        }
        private EOCHandOverCheckList GetHandOverCheckList(Guid VisitId)
        {
            var form = unitOfWork.EOCHandOverCheckListRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == VisitId);
            return form;
        }
        private dynamic HandleUpdateStatus(EOC visit, JToken request_status)
        {
            Guid new_status_id = new Guid(request_status["Id"].ToString());

            if (new_status_id == Guid.Empty) return null;
            
            var old_status_id = visit.StatusId;
            if (new_status_id != old_status_id) 
            {
                var new_status = GetStatusById(new_status_id);
                //if (!IsConfirmAllOrder(visit.Id))
                //    return new
                //    {
                //        Code = HttpStatusCode.BadRequest,
                //        Message = Message.OPD_CONFIRM_PLS
                //    };
                if (!IsConfirmStandingOrder(visit.Id))
                    return new
                    {
                        Code = HttpStatusCode.BadRequest,
                        Message = Message.CONFIRM_STANDING_ORDER
                    };
                if (new_status.Code == "EOCIH" && !IsCheckHandOverCheckList(visit.Id)) {
                    InHospital in_hospital = new InHospital();
                    in_hospital.SetState((Guid)visit.CustomerId, visit.Id, null, visit.VisitCode);
                    var in_hospital_visit = in_hospital.GetVisit();
                    if (in_hospital_visit != null)
                        return new
                        {
                            Code = HttpStatusCode.BadRequest,
                            Message = in_hospital.BuildErrorMessage(in_hospital_visit)
                        };
                    visit.Status = new_status;
                }
                else if (new_status.Code == "EOCTE" && !IsCheckHandOverCheckList(visit.Id))
                {
                    visit.Status = new_status;
                }
                else if (new_status.Code == "EOCA0" && !IsCheckHandOverCheckList(visit.Id))
                {
                    visit.Status = new_status;
                }
                
                var erorr = EOCValiDatePIDAndVisitCode(visit, new_status.Code);
                if (erorr != null)
                    return erorr;
                if(!IsCheckHandOverCheckList(visit.Id))
                {
                    visit.StatusId = new_status_id;
                    unitOfWork.EOCRepository.Update(visit);
                    unitOfWork.Commit();
                }   
;            }
            return null;
        }

        private dynamic EOCValiDatePIDAndVisitCode(EOC visit, string status_code)
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

        private bool IsConfirmAllOrder(Guid opd_id)
        {
            var order = unitOfWork.OrderRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == opd_id &&
                !string.IsNullOrEmpty(e.OrderType) &&
                e.OrderType.Equals(Constant.EOC_STANDING_ORDER) &&
                !e.IsConfirm);
            if (order != null) return false;
            return true;
        }
        private void HandleUpdateExaminationTime(EOCOutpatientExaminationNote oen, string examination_time)
        {
            DateTime request_examination_time = DateTime.ParseExact(examination_time, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            if (request_examination_time != null && oen.ExaminationTime != request_examination_time)
            {
                oen.ExaminationTime = request_examination_time;
                unitOfWork.EOCOutpatientExaminationNoteRepository.Update(oen);
            }
        }

        private Guid? GetReceivingUnitPhysicianId(EOC visit)
        {
            Guid? receiving_id = null;

            string receiving = string.Empty;
            if (Constant.TransferToED.Contains(visit.Status?.Code))
                receiving = GetMdValueInVisit(visit.Id, "OPDOENREC2ANS");
            if (Constant.Admitted.Contains(visit.Status?.Code))
                receiving = GetMdValueInVisit(visit.Id, "OPDOENRECANS");

            try
            {
                receiving_id = new Guid(receiving);
            }
            catch (Exception)
            {
                receiving_id = Guid.Empty;
            }

            return receiving_id;
        }
        private bool IsCheckHandOverCheckList(Guid visitId)
        {
            var ischeckipd = (from eo in unitOfWork.EOCRepository.AsQueryable().Where(e => !e.IsDeleted && e.Id == visitId)
                           join hoc in unitOfWork.EOCHandOverCheckListRepository.AsQueryable()
                           on eo.Id equals hoc.VisitId
                           join ipd in unitOfWork.IPDRepository.AsQueryable().Where(e => !e.IsDeleted)
                           on hoc.Id equals ipd.TransferFromId select new VisitModel { }).FirstOrDefault();

            var ischecked = (from eo in unitOfWork.EOCRepository.AsQueryable().Where(e => !e.IsDeleted && e.Id == visitId)
                              join hoc in unitOfWork.EOCHandOverCheckListRepository.AsQueryable()
                              on eo.Id equals hoc.VisitId
                              join ipd in unitOfWork.EDRepository.AsQueryable().Where(e => !e.IsDeleted)
                              on hoc.Id equals ipd.TransferFromId
                              select new VisitModel { }).FirstOrDefault();
            if (ischeckipd != null || ischecked != null) return true;
            return false;
           
        }
       
      
    }
}