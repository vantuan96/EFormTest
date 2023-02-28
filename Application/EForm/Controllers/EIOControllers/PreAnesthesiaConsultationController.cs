using DataAccess.Models;
using DataAccess.Models.GeneralModel;
using DataAccess.Models.OPDModel;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Common;
using EForm.Models;
using EForm.Models.OPDModels;
using EForm.Utils;
using EMRModels;
using Helper;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.EIOControllers
{
    [SessionAuthorize]
    public class PreAnesthesiaConsultationController : BaseApiController
    {
        [HttpGet]
        [Route("api/OPD/PreAnesthesiaConsultation/Info/{visitId}")]
        [Permission(Code = "OPDPACGM")]
        public IHttpActionResult GetInfo(Guid visitId)
        {
            dynamic visit = GetVisit(visitId, "OPD");
            if (visit == null)
                return Content(HttpStatusCode.NotFound, new
                {
                    ViMessage = string.Format("Lượt khám không tồn tại"),
                    EnMessage = string.Format("Visit is not found")
                });
            var site_code = GetSiteCode();
            if (string.IsNullOrEmpty(site_code))
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);
            return Content(HttpStatusCode.OK, GetInfoForOPD(visit,site_code));
        }
        [HttpGet]
        [Route("api/OPD/PreAnesthesiaConsultation/{id}")]
        [Permission(Code = "OPDPACGM")]
        public IHttpActionResult GetExaminationNoteAPI(Guid id)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);
            var oen = opd.OPDOutpatientExaminationNote;
            if (oen == null || oen.IsDeleted)
                return Content(HttpStatusCode.NotFound, new
                {
                    ViMessage = "Phiếu khám gây mê không tồn tại",
                    EnMessage = "Pre-Anesthesia Consultation (PAC) is not found"
                });            
           // var clinic = opd.Clinic;
            var primary_doctor = opd.PrimaryDoctor;
            var authorized_doctor = opd.AuthorizedDoctor;
            var user = GetUser();
            var locked = isLockOen(opd, oen.Id);             
            var datas = oen.OPDOutpatientExaminationNoteDatas.ToList(); 
            var custormer = opd.Customer;
            var status = unitOfWork.EDStatusRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == opd.EDStatusId);
            if (status?.Code == "OPDWR")
                locked = false;
            var confirminfo = GetFormConfirms(oen.Id);
            if (confirminfo.Count() > 0)
            {
                foreach (var con in confirminfo)
                {
                    var fullname = unitOfWork.UserRepository.FirstOrDefault(e => !e.IsDeleted && e.Username == con.ConfirmBy).Fullname;
                    con.Note = fullname;
                }
            }
            var spec = SpecInfo(opd);
            var preSRespiratory = unitOfWork.EIOFormRepository.FirstOrDefault(e => !e.IsDeleted && e.FormCode == "FORMOPDPYKKCKHHTP" && e.VisitId == id);
            var preSCcardiology = unitOfWork.EIOFormRepository.FirstOrDefault(e => !e.IsDeleted && e.FormCode == "A01_204_030320_VE" && e.VisitId == id);
           
            return Ok(new
            {
                IsLock24h = locked,
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
                //ClinicId = clinic?.Id,
                //Clinic = new { clinic?.ViName, clinic?.EnName, clinic?.Code, clinic?.Data },
                ExaminationTime = oen.ExaminationTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),              
                OPDId = opd.Id,
                Datas = datas.Where(i => !i.IsDeleted).Select(e => new { e.Id, e.Code, e.Value }).ToList(),
                oen.IsConsultation,               
                oen.AppointmentDateResult,
                oen.Version,
                oen.CreatedBy,
                oen.CreatedAt,
                oen.UpdatedBy,
                oen.UpdatedAt,
                Age = CaculatorAgeCustormer(custormer?.DateOfBirth),
                PID = custormer?.PID,               
                Confirm = confirminfo,
                Spec_Datas = spec,
                IspreSRespiratory = preSRespiratory == null? false: true,
                IspreSCcardiology = preSCcardiology == null? false: true,
                StatusCode = status?.Code,
            });
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/PreAnesthesiaConsultation/SetConsultation/{id}")]
        [Permission(Code = "OPDPACS")]
        public IHttpActionResult SetConsultation(Guid id, [FromUri] string consultation)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);
            var current_username = getUsername();
            if (opd.Customer.IsVip && !IsVIPMANAGE())
            {
                if (opd.UnlockFor == null) return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);
                if (!("," + opd.UnlockFor + ",").Contains("," + current_username + ",")) return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND); ;
            }
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);
            var user = GetUser();
            if (!IsCorrectDoctor(user?.Id, opd.PrimaryDoctorId) && !IsCorrectDoctor(user?.Id, opd.AuthorizedDoctorId))
                return Content(HttpStatusCode.BadRequest, Message.OWNER_FORBIDDEN);
            var oen = opd.OPDOutpatientExaminationNote;
            if (oen == null || oen.IsDeleted)
                return Content(HttpStatusCode.NotFound, new
                {
                    ViMessage = "Phiếu khám gây mê không tồn tại",
                    EnMessage = "Pre-Anesthesia Consultation (PAC) is not found"
                });           
            oen.Version = 1;
            oen.CreatedAt = DateTime.Now;
            bool isConsultation = Boolean.Parse(consultation);
            oen.IsConsultation = isConsultation;
            unitOfWork.OPDOutpatientExaminationNoteRepository.Update(oen, is_time_change: false);
            unitOfWork.Commit();
            return Content(HttpStatusCode.OK, new { ViName ="Tạo mới thành công", FormId = oen.Id, oen.Version });
        }
        private object GetInfoForOPD(OPD visit, string site_code)
        {
            var opdInitialAssessmentForShortTerm = visit.OPDInitialAssessmentForShortTerm?.OPDInitialAssessmentForShortTermDatas;
            VisitHistory visit_history = VisitHistoryFactory.GetVisit("OPD", visit, site_code);           
            var visit_history_list = visit_history.GetHistory();      
            return new
            {
                VitalSigns = new
                {
                    Weight = opdInitialAssessmentForShortTerm?.FirstOrDefault(x => x.Code == "OPDIAFSTOPWEIANS")?.Value,
                    Height = opdInitialAssessmentForShortTerm?.FirstOrDefault(x => x.Code == "OPDIAFSTOPHEIANS")?.Value,
                    SpO2 = opdInitialAssessmentForShortTerm?.FirstOrDefault(x => x.Code == "OPDIAFSTOPSPOANS")?.Value,
                    BP = opdInitialAssessmentForShortTerm?.FirstOrDefault(x => x.Code == "OPDIAFSTOPBP0ANS")?.Value,
                    Pulse = opdInitialAssessmentForShortTerm?.FirstOrDefault(x => x.Code == "OPDIAFSTOPPULANS")?.Value,
                    RR = opdInitialAssessmentForShortTerm?.FirstOrDefault(x => x.Code == "OPDIAFSTOPRR0ANS")?.Value,
                    T = opdInitialAssessmentForShortTerm?.FirstOrDefault(x => x.Code == "OPDIAFSTOPTEMANS")?.Value,
                    PrePregnancyWeight = opdInitialAssessmentForShortTerm?.FirstOrDefault(x => x.Code == "OPDIAFSTOPWEIPRT")?.Value,
                    CNTMT = opdInitialAssessmentForShortTerm?.FirstOrDefault(x => x.Code == "OPDIAFSTOPWEIPRT")?.Value,
                    IsApplicationCNTMT = opdInitialAssessmentForShortTerm?.FirstOrDefault(x => x.Code == "OPDIAFSTOPWEIPRY")?.Value,
                    IsNoApplicationCNTMT = opdInitialAssessmentForShortTerm?.FirstOrDefault(x => x.Code == "OPDIAFSTOPWEIPRN")?.Value,
                    HistoryOfAllergiesDGBD = EMRVisitAllergy.GetOPDVisitAllergy(visit),
                },
                HistoryOfAllergies = visit_history_list.Where(e => !string.IsNullOrEmpty(e.HistoryOfAllergies)).Select(e => new {
                    ExaminationTime = e.ExaminationTime.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                    e.ViName,
                    e.EnName,
                    e.Fullname,
                    e.Username,
                    e.HistoryOfAllergies,
                    e.Type
                }),
            };
        }        
        [HttpPost]
        [Route("api/OPD/PreAnesthesiaConsultation/Update/{visitId}/{id}")]
        [Permission(Code = "PACPUT")]
        public IHttpActionResult UpdatePreAnesthesiaConsultation(Guid visitId, Guid Id, [FromBody] JObject request)
        {
            var opd = GetVisit(visitId, "OPD");
            if (opd == null)
                return Content(HttpStatusCode.BadRequest, Message.OPD_NOT_FOUND);
            var oen = opd.OPDOutpatientExaminationNote;
            if (oen == null || oen.IsDeleted)
                return Content(HttpStatusCode.NotFound, new {
                ViMessage = "Phiếu khám gây mê không tồn tại",
                EnMessage = "Pre-Anesthesia Consultation (PAC) is not found"
        });
            var user = GetUser();
            if (!IsCorrectDoctor(user?.Id, opd.PrimaryDoctorId) && !IsCorrectDoctor(user?.Id, opd.AuthorizedDoctorId) && !IsCheckConfirm(oen.Id))
                return Content(HttpStatusCode.BadRequest, Message.OWNER_FORBIDDEN);
            var codeStatus = request["Status"]?.ToString();   
            if (codeStatus != "OPDNE")
            {
                HandleUpdateExaminationTime(oen, request["ExaminationTime"]?.ToString());
            }
            HandleUpdateOrCreateOutpatientExaminationNoteData(
               opd: opd,
               oen: oen,
               user: user,
               request_oen_data: request["Datas"]
           );
            if (user.Username != opd.PrimaryDoctor?.Username)
                CreatedOPDChangingNotification(user, opd.PrimaryDoctor?.Username, opd, "Phiếu khám gây mê", "A03_034_200520_VE");
            var error_mess = OPDValiDatePIDAndVisitCode(opd, codeStatus);
            if (error_mess != null)
                return Content(HttpStatusCode.NotFound, error_mess.Message);
            UpdateStatus(opd, codeStatus);
            return Content(HttpStatusCode.OK, "Chỉnh sửa thành công");
        }
        [HttpPost]
        [Route("api/PreAnesthesiaConsultation/{visitId}")]
        [Permission(Code = "APIPACCF")]
        public IHttpActionResult ConfirmForm(Guid visitId, [FromBody] JObject request)
        {
            var visit = GetVisit(visitId, "OPD");
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, "Không tồn tại");
            var oen = visit.OPDOutpatientExaminationNote;
            if (oen == null || oen.IsDeleted)
                return Content(HttpStatusCode.NotFound, new
                {
                    ViMessage = "Phiếu khám gây mê không tồn tại",
                    EnMessage = "Pre-Anesthesia Consultation (PAC) is not found"
                });
            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var kind = request["kind"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);
            var PermissionCode = "APIPACCF";
            var ischeckpermission = IsCheckPermission(username, PermissionCode);
            if (!ischeckpermission)
                return Content(HttpStatusCode.Forbidden, Message.OWNER_FORBIDDEN);
            Guid? formid = oen.Id;
            var getconfirm = GetFormConfirms(oen.Id);
            if (getconfirm.Count > 0)
            {
                foreach (var item in getconfirm)
                {
                    if (kind != item.ConfirmType)
                    {
                        SaveConfirm(username, kind, formid);
                    }
                    else
                    {
                        return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);
                    }
                }
            }
            else
            {
                SaveConfirm(username, kind, formid);
                UnLockForm(visit.Id);
            }
            UpdateVisit(visit, "OPD");

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }
        [HttpPost]
        [Route("api/OPD/PreAnesthesiaConsultation/UpdateExamRequire/{visitId}")]
        [Permission(Code = "PACPUT")]
        public IHttpActionResult UpdateExamRequire(Guid visitId, [FromBody] JObject request)
        {
            var opd = GetVisit(visitId, "OPD");
            if (opd == null)
                return Content(HttpStatusCode.BadRequest, Message.OPD_NOT_FOUND);
            Guid customerId = opd.CustomerId;
            var oen = opd.OPDOutpatientExaminationNote;
            if (oen == null || oen.IsDeleted)
                return Content(HttpStatusCode.NotFound, new
                {
                    ViMessage = "Phiếu khám gây mê không tồn tại",
                    EnMessage = "Pre-Anesthesia Consultation (PAC) is not found"
                });
            bool isSpecialistExamination = Convert.ToBoolean(request["IsSpecialistExamination"].ToString());
            if (isSpecialistExamination)
            {
                var specialyty_Data = request["Datas"];
                HandleUpdateRequestExam(opd, specialyty_Data);
            }
            else if (!isSpecialistExamination)
            {
                //xoá hàng chờ
                var hovcls = unitOfWork.OPDPreAnesthesiaHandOverCheckListRepository.Find(e => !e.IsDeleted && e.VisitId == visitId).ToList();
                if (hovcls.Count > 0)
                {
                    foreach (var item in hovcls)
                    {
                        if (!item.IsAcceptNurse && !item.IsAcceptPhysician)
                        {
                            item.IsDeleted = true;
                            unitOfWork.OPDPreAnesthesiaHandOverCheckListRepository.Update(item);
                            unitOfWork.Commit();
                        }
                    }
                }
            }  
            return Content(HttpStatusCode.OK,new { ViName = "Thành công"});
        }
        private List<PreAnesthesiaModel> SpecInfo(OPD opd)
        {
            var spec = (from visit in unitOfWork.OPDRepository.AsQueryable()
                          .Where(e => !e.IsDeleted)
                        join spec_sql in unitOfWork.OPDPreAnesthesiaHandOverCheckListRepository
                        .Find(u => !u.IsDeleted && (u.IsAcceptPhysician || u.IsAcceptNurse) && u.VisitId == opd.Id).AsQueryable()                        
                        on visit.TransferFromId equals spec_sql.Id                        
                        join user1 in unitOfWork.UserRepository.Find(a => !a.IsDeleted).AsQueryable()
                        on spec_sql.ReceivingNurseId equals user1.Id into user1_list 
                        from us in user1_list.DefaultIfEmpty()
                        join user2 in unitOfWork.UserRepository.Find(a => !a.IsDeleted).AsQueryable()
                        on spec_sql.ReceivingPhysicianId equals user2.Id into user2_list
                        from us2 in user2_list.DefaultIfEmpty()                       
                        join tk in unitOfWork.SpecialtyRepository.Find(s => !s.IsDeleted).AsQueryable()
                        on visit.SpecialtyId.Value equals tk.Id
                        select new PreAnesthesiaModel
                        {
                            Status = new { EnName = visit.EDStatus.EnName, ViName = visit.EDStatus.ViName },
                            AcceptByNurse = new
                            {
                                IsAcceptByNurse = spec_sql.IsAcceptNurse,
                                AcceptBy = us.Username
                            },
                            AcceptByPhysician = new
                            {
                                IsAcceptByPhysician = spec_sql.IsAcceptPhysician,
                                AcceptBy = us2.Username
                            },
                            CodeCK = spec_sql.Code,
                            Specialyty = new { ViNameSpec = tk.ViName, EnNameSpec = tk.ViName, Code = tk.Code },
                            IsAccept = spec_sql.IsAcceptNurse == true ? true : spec_sql.IsAcceptPhysician,
                            ReciveVisitId = visit.Id

                        }).Distinct().ToList();
            return spec;
        }
        private void HandleUpdateOrCreateOutpatientExaminationNoteData(OPD opd, OPDOutpatientExaminationNote oen, User user, JToken request_oen_data)
        {
            var oen_datas = oen.OPDOutpatientExaminationNoteDatas.Where(e => !e.IsDeleted).ToList();
            foreach (var item in request_oen_data)
            {
                var code = item.Value<string>("Code");               
                var value = item.Value<string>("Value");
                var oen_data = oen_datas.FirstOrDefault(e => e.Code == code);               
                if (oen_data == null)
                    CreateExaminationNoteData(oen.Id, code, value);
                else if (oen_data.Value != value)
                    UpdateExaminationNoteData(oen_data, code, value);
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
        private dynamic HandleUpdateRequestExam(OPD opd, JToken request_tm_data)
        {
            Guid? receiving_id = null;
            string code = string.Empty;
            var specialty = opd.Specialty;
            foreach (var item in request_tm_data)
            {
                code = item.Value<string>("Code");
                var spec_id = item.Value<string>("SpecialytyId");
                if (!string.IsNullOrEmpty(spec_id))
                {
                    try
                    {
                        receiving_id = Guid.Parse(spec_id);
                        var receiving_unit = unitOfWork.SpecialtyRepository.GetById((Guid)receiving_id);
                        CreateOrUpdateHandOverCheckList(opd, specialty, receiving_unit, code);
                    }
                    catch (Exception)
                    {
                        return Message.TRANSFER_ERROR;
                    }
                }
                else
                {
                    var opdPre = unitOfWork.OPDPreAnesthesiaHandOverCheckListRepository.FirstOrDefault(e => !e.IsDeleted && e.Code == code && e.VisitId == opd.Id);
                    if(opdPre != null)
                    {
                        opdPre.IsDeleted = true;
                        unitOfWork.OPDPreAnesthesiaHandOverCheckListRepository.Update(opdPre);
                        unitOfWork.Commit();
                    }
                }
            }           
            unitOfWork.Commit();
            return null;
        }
        private void UpdateStatus(OPD opd, string code)
        {
            var status = unitOfWork.EDStatusRepository.FirstOrDefault(e => !e.IsDeleted && e.Code == code);
            opd.EDStatusId = status.Id;
            unitOfWork.OPDRepository.Update(opd);
            unitOfWork.Commit();
        }
        private void CreateOrUpdateHandOverCheckList(OPD opd, Specialty specialty, Specialty receiving, string code)
        {
            var check = unitOfWork.OPDPreAnesthesiaHandOverCheckListRepository.FirstOrDefault(e => !e.IsDeleted && e.Code == code && e.VisitId == opd.Id);
            if(check == null)
            {
                OPDPreAnesthesiaHandOverCheckList newhocl = new OPDPreAnesthesiaHandOverCheckList();
                newhocl.HandOverTimePhysician = DateTime.Now;
                newhocl.HandOverUnitNurseId = specialty.Id;
                newhocl.HandOverUnitPhysicianId = specialty.Id;
                newhocl.ReceivingUnitNurseId = receiving.Id;
                newhocl.ReceivingUnitPhysicianId = receiving.Id;
                newhocl.HandOverPhysicianId = GetUser()?.Id;
                newhocl.Code = code;
                newhocl.VisitId = opd.Id;

                if (!receiving.IsPublish)
                {
                    newhocl.IsAcceptNurse = true;
                    newhocl.IsAcceptPhysician = true;
                }
                unitOfWork.OPDPreAnesthesiaHandOverCheckListRepository.Add(newhocl);
            }
            else if(!check.IsAcceptPhysician && !check.IsAcceptNurse )
            {
                if(check.ReceivingUnitNurseId != receiving.Id)
                {
                    check.HandOverPhysicianId = GetUser()?.Id;
                    check.HandOverTimePhysician = DateTime.Now;
                }
                check.HandOverUnitNurseId = specialty.Id;
                check.HandOverUnitPhysicianId = specialty.Id;
                check.ReceivingUnitNurseId = receiving.Id;
                check.ReceivingUnitPhysicianId = receiving.Id;
                if (!receiving.IsPublish)
                {
                    check.IsAcceptNurse = true;
                    check.IsAcceptPhysician = true;
                }
                else
                {
                    if (check.ReceivingNurseId == null && check.ReceivingPhysicianId == null)
                    {
                        check.IsAcceptNurse = false;
                        check.IsAcceptPhysician = false;
                    }
                }
                unitOfWork.OPDPreAnesthesiaHandOverCheckListRepository.Update(check);
            }
            unitOfWork.Commit();
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
        private bool IsCorrectDoctor(Guid? user_id, Guid? primary_doctor_id)
        {
            if (IsSuperman()) return true;
            if (user_id == null || primary_doctor_id == null || user_id != primary_doctor_id)
            {
                return false;
            }
            return true;
        }
        private void HandleUpdateExaminationTime(OPDOutpatientExaminationNote oen, string examination_time)
        {
            DateTime request_examination_time = DateTime.ParseExact(examination_time, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            if (request_examination_time != null && oen.ExaminationTime != request_examination_time)
            {
                oen.ExaminationTime = request_examination_time;
                unitOfWork.OPDOutpatientExaminationNoteRepository.Update(oen);
                unitOfWork.Commit();
            }
        }
        private bool isLockOen(OPD opd, Guid formId)
        {
            var user = GetUser();
            var is_block_after_24h = IsBlockAfter24h(opd.CreatedAt, formId);
            var has_unlock_permission = HasUnlockPermission(opd.Id, "A03_034_200520_VE", user.Username, formId);            
            return (is_block_after_24h && !has_unlock_permission);
        }
        private dynamic get_visit_transfer(OPD opd)
        {
            var visit_transfer = new VisitTransfer(unitOfWork);
            if (visit_transfer.IsExist(opd.OPDHandOverCheckListId, opd.Id))
                return visit_transfer.BuildMessage();
            return null;
        }
        private void CreateExaminationNoteData(Guid oen_id, string code, string value)
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
        private void UpdateExaminationNoteData(OPDOutpatientExaminationNoteData oen_data, string code, string value)
        {
           if (oen_data.Value != value)
           {
               oen_data.Value = value;
               unitOfWork.OPDOutpatientExaminationNoteDataRepository.Update(oen_data);
           }
        }
        private dynamic OPDValiDatePIDAndVisitCode(OPD visit, string status_code)
        {
            if (status_code == "OPDIH" || status_code == "OPDWR" || status_code == "OPDNE")
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
        private void UnLockForm(Guid visitId)
        {
            var unlockform = unitOfWork.UnlockFormToUpdateRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == visitId && e.FormCode == "A03_034_200520_VE");
            if (unlockform != null)
            {
                unlockform.IsDeleted = true;
                unitOfWork.UnlockFormToUpdateRepository.Update(unlockform);
                unitOfWork.Commit();
            }
        }
    }
}
