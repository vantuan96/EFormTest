using DataAccess.Models;
using DataAccess.Models.EDModel;
using DataAccess.Models.EOCModel;
using DataAccess.Models.OPDModel;
using EForm.Authentication;
using EForm.Client;
using EForm.Common;
using EForm.Controllers.BaseControllers;
using EForm.Models;
using EForm.Models.OPDModels;
using EForm.Models.PrescriptionModels;
using EForm.Utils;
using EMRModels;
using Helper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using static EForm.Models.OrdersResponse;

namespace EForm.Controllers.OPDControllers
{
    [SessionAuthorize]
    public class OPDInitialAssessmentController : BaseOPDApiController
    {
        [HttpGet]
        [Route("api/OPD/InitialAssessments/ForShortTerm/{type}/{id}")]
        [Permission(Code = "OINAS1")]
        public IHttpActionResult GetInitialAssessmentsForShortTermAPI(Guid id)
        {
            string type = "OPD_A02_007_080121_VE";
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var iafst = opd.OPDInitialAssessmentForShortTerm;
            if (iafst == null || iafst.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.OPD_IAFST_NOT_FOUND);

            var clinic = opd.Clinic;
            var primary_doctor = opd.PrimaryDoctor;

            var init_assessment_form = new List<dynamic>();
            if (opd.GroupId != null)
                init_assessment_form.AddRange(unitOfWork.OPDRepository.Find(
                    e => !e.IsDeleted &&
                    e.GroupId != null &&
                    e.GroupId == opd.GroupId
                ).OrderByDescending(e => e.AdmittedDate)
                .Select(e => new
                {
                    e.Id,
                    Clinic = new { e.Clinic?.Id, e.Clinic?.ViName, e.Clinic?.EnName, e.Clinic?.Code },
                    PrimaryDoctor = new { e.PrimaryDoctor?.Id, e.PrimaryDoctor?.Username, e.PrimaryDoctor?.Fullname, e.IsBooked, BookingTime = e.BookingTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND) },
                    AdmittedDate = e.OPDInitialAssessmentForShortTerm.AdmittedDate?.ToString(Constant.TIME_DATE_FORMAT),
                    HasOENDData = checkHasOENDData(e.OPDOutpatientExaminationNote)
                }));
            else
                init_assessment_form.Add(new
                {
                    opd.Id,
                    Clinic = new { clinic?.Id, clinic?.ViName, clinic?.EnName, clinic?.Code },
                    PrimaryDoctor = new { primary_doctor?.Id, primary_doctor?.Username, primary_doctor?.Fullname, opd.IsBooked, BookingTime = opd.BookingTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND) },
                    AdmittedDate = iafst.AdmittedDate?.ToString(Constant.TIME_DATE_FORMAT),
                    HasOENDData = checkHasOENDData(opd.OPDOutpatientExaminationNote)
                });

            var user = GetUser();

            return Content(HttpStatusCode.OK, new
            {
                //opd.PrimaryNurse?.Username,
                Username = IsNew(iafst.CreatedAt, iafst.UpdatedAt) ? "" : iafst.CreatedBy,
                opd.RecordCode,
                iafst.Id,
                Version = opd.Version >= 7 ? opd.Version : iafst.Version,
                ClinicId = clinic?.Id,
                InitialAssessmentForm = init_assessment_form,
                OPDId = opd.Id,
                Datas = iafst.OPDInitialAssessmentForShortTermDatas.Where(i => !i.IsDeleted).Select(e => new { e.Id, e.Code, e.Value }).ToList(),
                Orders = "",
                IsNew = IsNew(iafst.CreatedAt, iafst.UpdatedAt),
                IsShowSyncButton = IsShowSyncButton(opd.CustomerId, opd.Id, opd.GroupId, opd.CreatedAt) || IsShowSyncEDCButton(opd.CustomerId, opd.CreatedAt),
                VisitType = opd.VisitType,
                IsLocked = Is24hLocked(opd.CreatedAt, opd.Id, type, user.Username, iafst.Id),
                IsAnesthesia = opd.IsAnesthesia,
                UserNameReceiving = opd.CreatedBy, // người tiếp nhận
                iafst.UpdatedAt,
                iafst.UpdatedBy
            });
        }


        private bool checkHasOENDData(OPDOutpatientExaminationNote oPDOutpatientExaminationNote)
        {
            if (oPDOutpatientExaminationNote == null)
                return false;
            //var data = unitOfWork.OPDOutpatientExaminationNoteDataRepository.FirstOrDefault(
            //    e => !e.IsDeleted &&
            //    e.OPDOutpatientExaminationNoteId == oPDOutpatientExaminationNote.Id &&
            //    e.Code == "OPDOENCEFANS"
            //);
            return (oPDOutpatientExaminationNote.IsDoctorChangeForm || oPDOutpatientExaminationNote.IsAuthorizeDoctorChangeForm);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/InitialAssessments/ForShortTerm/{type}/{id}")]
        [Permission(Code = "OINAS2")]
        public IHttpActionResult UpdateInitialAssessmentsForShortTermAPI(Guid id, [FromBody] JObject request)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var iafst = opd.OPDInitialAssessmentForShortTerm;
            if (iafst == null || iafst.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.OPD_IAFST_NOT_FOUND);

            var user = GetUser();
            if (IsNew(iafst.CreatedAt, iafst.UpdatedAt))
            {
                iafst.CreatedBy = user.Username; 
                iafst.UpdatedBy = user.Username;
            }

            if (IsBlockAfter24h(opd.CreatedAt, iafst.Id) && !HasUnlockPermission(opd.Id, "OPD_A02_007_080121_VE", user.Username, iafst.Id))
                return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);
            //var a = CanWriteShortTerm(user, iafst.UpdatedBy, iafst.Id, iafst.CreatedAt, iafst.UpdatedAt);
            if (!IsSuperman() && !CanWriteShortTerm(user, iafst.CreatedBy, iafst.Id, iafst.CreatedAt, iafst.UpdatedAt))
                return Content(HttpStatusCode.BadRequest, Message.OWNER_FORBIDDEN);

            HandleInitialAssessmentForShortTermDatas(opd, iafst, request["Datas"]);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/InitialAssessments/ForShortTerm/Sync/{id}")]
        [Permission(Code = "OINAS3")]
        public IHttpActionResult SyncInitialAssessmentsForShortTermAPI(Guid id)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);
            if (opd.AdmittedDate == null)
                return Content(HttpStatusCode.NotFound, Message.SYNC_24H_NOT_FOUND);

            var lastest_opd = GetLastestOPDIn24H(opd.CustomerId, opd.Id, opd.GroupId, opd.AdmittedDate);
            
            if (lastest_opd != null)
            {
                var iafst = lastest_opd.OPDInitialAssessmentForShortTerm;
                if (iafst != null) { 
                    var clinic = lastest_opd.Clinic;
                    return Content(HttpStatusCode.OK, new
                    {
                        iafst.Id,
                        Version = lastest_opd.Version >= 7 ? lastest_opd.Version : iafst.Version,
                        ClinicId = clinic?.Id,
                        Clinic = new { clinic?.ViName, clinic?.EnName, clinic?.Code },
                        AdmittedDate = iafst.AdmittedDate?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                        OPDId = lastest_opd.Id,
                        Datas = iafst.OPDInitialAssessmentForShortTermDatas.Where(i => !i.IsDeleted).Select(etrd => new { etrd.Id, etrd.Code, etrd.Value }).ToList(),
                        Orders = "",
                        IsNew = IsNew(iafst.CreatedAt, iafst.UpdatedAt),
                    });
                }
            }

            var lastest_EDC = GetLastestEDCIn24H(opd.CustomerId, opd.AdmittedDate);

            if (lastest_EDC != null)
            {
                var iafst_form = unitOfWork.EOCInitialAssessmentForShortTermRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == lastest_EDC.Id);
                if (iafst_form != null) {
                    return Content(HttpStatusCode.OK, new
                    {
                        Datas = GetFormData(lastest_EDC.Id, iafst_form.Id, "OPDIAFSTOP"),
                    });
                }
            }

            return Content(HttpStatusCode.NotFound, Message.SYNC_24H_NOT_FOUND);
        }

        [HttpGet]
        [Route("api/OPD/InitialAssessments/ForOnGoing/{id}")]
        [Permission(Code = "OINAS4")]
        public IHttpActionResult GetInitialAssessmentsForOnGoingAPI(Guid id, string type = "OPDIAFOGOP")
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var iafog = opd.OPDInitialAssessmentForOnGoing;
            if (iafog == null || iafog.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.OPD_IAFST_NOT_FOUND);

            var clinic = opd.Clinic;
            var primary_doctor = opd.PrimaryDoctor;

            var init_assessment_form = new List<dynamic>();
            if (opd.GroupId != null)
                init_assessment_form.AddRange(
                    unitOfWork.OPDRepository.Find(
                        e => !e.IsDeleted &&
                        e.GroupId != null &&
                        e.GroupId == opd.GroupId
                    ).OrderByDescending(e => e.AdmittedDate)
                    .Select(e => new
                    {
                        e.Id,
                        Clinic = new { e.Clinic?.Id, e.Clinic?.ViName, e.Clinic?.EnName, e.Clinic?.Code },
                        PrimaryDoctor = new { e.PrimaryDoctor?.Id, e.PrimaryDoctor?.Username, e.PrimaryDoctor?.Fullname, e.IsBooked, BookingTime = e.BookingTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND) },
                        AdmittedDate = e.OPDInitialAssessmentForOnGoing.AdmittedDate?.ToString(Constant.TIME_DATE_FORMAT),
                        HasOENDData = checkHasOENDData(e.OPDOutpatientExaminationNote)
                    }));
            else
                init_assessment_form.Add(new
                {
                    opd.Id,
                    Clinic = new { clinic?.Id, clinic?.ViName, clinic?.EnName, clinic?.Code },
                    PrimaryDoctor = new { primary_doctor?.Id, primary_doctor?.Username, primary_doctor?.Fullname, opd.IsBooked, BookingTime = opd.BookingTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND) },
                    AdmittedDate = iafog.AdmittedDate?.ToString(Constant.TIME_DATE_FORMAT),
                    HasOENDData = checkHasOENDData(opd.OPDOutpatientExaminationNote)
                });
            var user = GetUser();
            return Content(HttpStatusCode.OK, new
            {
                iafog.UpdatedBy,
                OPDUpdatedBy = opd.UpdatedBy,
                opd.RecordCode,
                iafog.Id,
                Version = opd.Version >= 7 ? opd.Version : iafog.Version,
                ClinicId = clinic?.Id,
                InitialAssessmentForm = init_assessment_form,
                OPDId = opd.Id,
                Datas = iafog.OPDInitialAssessmentForOnGoingDatas.Where(i => !i.IsDeleted).Select(etrd => new { etrd.Id, etrd.Code, etrd.Value }).ToList(),
                Orders = "",
                IsNew = IsNew(iafog.CreatedAt, iafog.UpdatedAt),
                IsShowSyncButton = IsShowSyncButton(opd.CustomerId, opd.Id, opd.GroupId, opd.CreatedAt),
                IsLocked = Is24hLocked(opd.CreatedAt, opd.Id, type, user.Username, iafog.Id)
            });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/InitialAssessments/ForOnGoing/{id}")]
        [Permission(Code = "OINAS5")]
        public IHttpActionResult UpdateInitialAssessmentsForOnGoingAPI(Guid id, [FromBody] JObject request)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var iafog = opd.OPDInitialAssessmentForOnGoing;
            if (iafog == null || iafog.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.OPD_IAFST_NOT_FOUND);

            var user = GetUser();
            if (!CanWrite(user, iafog.UpdatedBy, iafog.CreatedAt, iafog.UpdatedAt))
                return Content(HttpStatusCode.BadRequest, Message.OWNER_FORBIDDEN);

            if (IsBlockAfter24h(opd.CreatedAt, iafog.Id) && !HasUnlockPermission(opd.Id, "OPDIAFOGOP", user.Username, iafog.Id))
                return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);

            HandleInitialAssessmentForOnGoingDatas(opd, iafog, request["Datas"]);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/InitialAssessments/ForOnGoing/Sync/{id}")]
        [Permission(Code = "OINAS6")]
        public IHttpActionResult SyncInitialAssessmentsForOnGoingAPI(Guid id)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);
            if (opd.AdmittedDate == null)
                return Content(HttpStatusCode.NotFound, Message.SYNC_24H_NOT_FOUND);

            var lastest_opd = GetLastestOPDIn24H(opd.CustomerId, opd.Id, opd.GroupId, opd.AdmittedDate);
            if (lastest_opd == null)
                return Content(HttpStatusCode.NotFound, Message.SYNC_24H_NOT_FOUND);

            var iafog = lastest_opd.OPDInitialAssessmentForOnGoing;
            if (iafog == null || iafog.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.SYNC_24H_NOT_FOUND);

            var clinic = lastest_opd.Clinic;
            return Content(HttpStatusCode.OK, new
            {
                iafog.Id,
                Version = opd.Version >= 7 ? opd.Version : iafog.Version,
                ClinicId = clinic?.Id,
                Clinic = new { clinic?.ViName, clinic?.EnName, clinic?.Code },
                AdmittedDate = iafog.AdmittedDate?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                OPDId = lastest_opd.Id,
                Datas = iafog.OPDInitialAssessmentForOnGoingDatas.Where(i => !i.IsDeleted).Select(e => new { e.Id, e.Code, e.Value }).ToList(),
                Orders = "",
                IsNew = IsNew(iafog.CreatedAt, iafog.UpdatedAt),
            });
        }

        [HttpGet]
        [Route("api/OPD/InitialAssessments/FallRiskScreening/Info/{type}/{visitId}")]
        [Permission(Code = "OINAS7")]
        public IHttpActionResult GetInfoFallRiskScreenings(Guid visitId)
        {
            string type = "OPD_A02_007_220321_VE";
            var opd = GetOPD(visitId);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);
            var user = GetUser();
            return Content(HttpStatusCode.OK, new
            {
                IsLocked = Is24hLocked(opd.CreatedAt, opd.Id, type, user.Username)
            });
        }

        [HttpGet]
        [Route("api/OPD/InitialAssessments/FallRiskScreening/{type}/{visitId}")]
        [Permission(Code = "OINAS7")]
        public IHttpActionResult GetFallRiskScreenings(Guid visitId)
        {
            string type = "OPD_A02_007_220321_VE";
            var opd = GetOPD(visitId);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var forms = unitOfWork.OPDFallRiskScreeningRepository.Find(e => !e.IsDeleted && e.VisitId == visitId).OrderBy(o => o.CreatedAt).ToList().Select(form => new
            {
                form.Id,
                form.VisitId,
                form.CreatedBy,
                form.CreatedAt,
                form.UpdatedAt,
                form.UpdatedBy,
                form.Version
            }).ToList();
            var user = GetUser();
            return Content(HttpStatusCode.OK, new
            {
                Datas = forms,
                IsLocked = Is24hLocked(opd.CreatedAt, opd.Id, type, user.Username)
            });
        }

        [HttpGet]
        [Route("api/OPD/InitialAssessments/FallRiskScreening/{type}/{visitId}/{id}")]
        [Permission(Code = "OINAS7")]
        public IHttpActionResult GetFallRiskScreeningForOnGoingAPI(Guid visitId, Guid id)
        {
            string type = "OPD_A02_007_220321_VE";
            var opd = GetOPD(visitId);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var is_waiting_accept_error_message = GetWaitingNurseAcceptMessage(opd);
            if (is_waiting_accept_error_message != null)
                return Content(HttpStatusCode.OK, is_waiting_accept_error_message);

            var form = GetForm(visitId, id);
            if (form == null) return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);
            //var opdOlds = unitOfWork.OPDRepository.Find(x => x.CustomerId == opd.CustomerId).OrderByDescending(x => x.AdmittedDate).ToList();
            var clinicBefore = string.Empty;
            //if(opdOlds.Count > 0)
            //{
            //    clinicBefore = opdOlds[0].OPDOutpatientExaminationNote.Service;
            //}
            var user = GetUser();
            return Content(HttpStatusCode.OK, new
            {
                opd.PrimaryNurse?.Username,
                opd.RecordCode,
                form.Id,
                OPDId = opd.Id,
                Datas = form.OPDFallRiskScreeningDatas.Where(i => !i.IsDeleted).Select(e => new { e.Id, e.Code, e.Value }).ToList(),
                Orders = "",
                IsShowSyncButton = IsShowSyncButton(opd.CustomerId, opd.Id, opd.GroupId, opd.CreatedAt),
                form.Version,
                form.CreatedBy,
                form.CreatedAt,
                form.UpdatedAt,
                form.UpdatedBy,
                DoctorPrimary = new
                {
                    opd?.PrimaryDoctor?.Fullname,
                    opd?.PrimaryDoctor?.Username
                },
                ClinicOfDoctorPrimary = opd?.OPDOutpatientExaminationNote?.Service,
                IsLocked = Is24hLocked(opd.CreatedAt, opd.Id, type, user.Username, form.Id)
            });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/InitialAssessments/FallRiskScreening/Create/{type}/{visitId}")]
        [Permission(Code = "OINAS8")]
        public IHttpActionResult UpdateFallRiskScreeningForOnGoingAPI(Guid visitId, [FromBody] JObject request)
        {
            var opd = GetOPD(visitId);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            //var is_waiting_accept_error_message = GetWaitingNurseAcceptMessage(opd);
            //if (is_waiting_accept_error_message != null)
            //    return Content(HttpStatusCode.OK, is_waiting_accept_error_message);

            //var frs = opd.OPDFallRiskScreening;
            //if (frs == null || frs.IsDeleted)
            //    return Content(HttpStatusCode.NotFound, Message.OPD_FRS_NOT_FOUND);

            //var user = GetUser();
            //if (IsBlockAfter24h(opd.CreatedAt) && !HasUnlockPermission(opd.Id, "OPDFRSFOP", user.Username))
            //    return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);

            //if (!CanWrite(user, frs.UpdatedBy, frs.CreatedAt, frs.UpdatedAt))
            //    return Content(HttpStatusCode.BadRequest, Message.OWNER_FORBIDDEN);
            var form_data = new OPDFallRiskScreening
            {
                VisitId = visitId,
                Version = opd.Version >= 9 ? opd.Version : 2
            };

            unitOfWork.OPDFallRiskScreeningRepository.Add(form_data);
            UpdateVisit(opd, "OPD");
            unitOfWork.Commit();
            return Content(HttpStatusCode.OK, new
            {
                form_data.Id,
                form_data.VisitId,
                form_data.CreatedBy,
                form_data.CreatedAt,
                form_data.Version
            });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/InitialAssessments/FallRiskScreening/Update/{type}/{visitId}/{id}")]
        [Permission(Code = "OINAS8")]
        public IHttpActionResult UpdateFallRiskScreeningForOnGoingAPI(Guid visitId, Guid id, [FromBody] JObject request)
        {
            string type = "OPD_A02_007_220321_VE";
            var opd = GetOPD(visitId);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var is_waiting_accept_error_message = GetWaitingNurseAcceptMessage(opd);
            if (is_waiting_accept_error_message != null)
                return Content(HttpStatusCode.OK, is_waiting_accept_error_message);

            var form = GetForm(visitId, id);
            if (form == null) return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);

            var user = GetUser();
            if (IsBlockAfter24h(opd.CreatedAt, form.Id) && !HasUnlockPermission(opd.Id, type, user.Username, form.Id))
                return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);

            //if (!CanWrite(user, frs.UpdatedBy, frs.CreatedAt, frs.UpdatedAt))
            //    return Content(HttpStatusCode.BadRequest, Message.OWNER_FORBIDDEN);

            HandleFallRiskScreening(opd, id, request["Datas"]);

            unitOfWork.OPDFallRiskScreeningRepository.Update(form);
            var lastFallRick = unitOfWork.OPDFallRiskScreeningRepository.Find(x => !x.IsDeleted && x.VisitId == opd.Id).ToList().OrderByDescending(x => x.CreatedAt).FirstOrDefault();
            if (lastFallRick.Id == id)
            {
                if (lastFallRick.Version == 1)
                {
                    opd.IsHasFallRiskScreening = unitOfWork.OPDFallRiskScreeningDataRepository
                      .Count(
                      e => e.OPDFallRiskScreeningId == id && !e.IsDeleted &&
                      e.OPDFallRiskScreeningId != null &&
                      new List<string> { "OPDFRSFOPDPHANS1", "OPDFRSFOPDPHANS2", "OPDFRSFOPDPHANS3", "OPDFRSFOPDPNANS1", "OPDFRSFOPDPNANS2", "OPDFRSFOPDPNANS3" }.Contains(e.Code) &&
                      e.Value == "1") > 0;
                }
                else
                {
                    opd.IsHasFallRiskScreening = unitOfWork.OPDFallRiskScreeningDataRepository.Count(e => e.Value == "1" && e.OPDFallRiskScreeningId != null && e.OPDFallRiskScreeningId == id && e.Code == "OPDFRSFOPUTHANS1") > 0;
                }
            }

            UpdateVisit(opd, "OPD");
            unitOfWork.Commit();

            return Content(HttpStatusCode.OK, new
            {
                form.Id,
                form.VisitId,
                form.UpdatedAt,
                form.UpdatedBy
            });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/InitialAssessments/FallRiskScreening/Sync/{type}/{visitId}")]
        [Permission(Code = "OINAS9")]
        public IHttpActionResult SyncFallRiskScreeningForOnGoingAPI(Guid visitId)
        {
            var opd = GetOPD(visitId);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);
            if (opd.AdmittedDate == null)
                return Content(HttpStatusCode.NotFound, Message.SYNC_24H_NOT_FOUND);

            var lastest_opd = GetLastestOPDIn24H(opd.CustomerId, opd.Id, opd.GroupId, opd.AdmittedDate);
            if (lastest_opd == null)
                return Content(HttpStatusCode.NotFound, Message.SYNC_24H_NOT_FOUND);

            var frs = lastest_opd.OPDFallRiskScreening;
            if (frs == null || frs.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.SYNC_24H_NOT_FOUND);

            return Content(HttpStatusCode.OK, new
            {
                frs.Id,
                OPDId = lastest_opd.Id,
                Datas = frs.OPDFallRiskScreeningDatas.Where(i => !i.IsDeleted).Select(e => new { e.Id, e.Code, e.Value }).ToList(),
                Orders = "",
            });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/InitialAssessments/ForTelehealth/Confirm/{id}")]
        [Permission(Code = "OINAS10")]
        public IHttpActionResult CreateInitialAssessmentsForTelehealthAPI(Guid id)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var iafth = opd.OPDInitialAssessmentForTelehealth;
            if (iafth != null && !iafth.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.OPD_IAFTH_EXIST);

            var user = GetUser();
            if (IsBlockAfter24h(opd.CreatedAt, iafth?.Id) && !HasUnlockPermission(opd.Id, "OPDIAFTP", user.Username, iafth?.Id))
                return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);

            CreateInitialAssessmentForTelehealth(opd);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpGet]
        [Route("api/OPD/InitialAssessments/ForTelehealth/{id}")]
        [Permission(Code = "OINAS11")]
        public IHttpActionResult GetInitialAssessmentsForTelehealthAPI(Guid id, string type = "OPDIAFTP")
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);
            var user = GetUser();
            var iafth = opd.OPDInitialAssessmentForTelehealth;
            var IsLocked = Is24hLocked(opd.CreatedAt, opd.Id, type, user.Username, opd.OPDInitialAssessmentForTelehealthId);

            if (iafth == null || iafth.IsDeleted)
                return Content(HttpStatusCode.NotFound, new
                {
                    ViMessage = "Đánh giá ban đầu người chăm sóc từ xa không tồn tại",
                    EnMessage = "Initial assessment for telehealth patient is not found",
                    IsLocked
                });

            var clinic = opd.Clinic;
            var primary_doctor = opd.PrimaryDoctor;
            var init_assessment_form = new List<dynamic>();
            if (opd.GroupId != null)
                init_assessment_form.AddRange(
                    unitOfWork.OPDRepository.Find(
                        e => !e.IsDeleted &&
                        e.GroupId != null &&
                        e.GroupId == opd.GroupId
                    ).OrderByDescending(e => e.AdmittedDate)
                    .Select(e => new
                    {
                        e.Id,
                        Clinic = new { e.Clinic?.Id, e.Clinic?.ViName, e.Clinic?.EnName, e.Clinic?.Code },
                        PrimaryDoctor = new { e.PrimaryDoctor?.Id, e.PrimaryDoctor?.Username, e.PrimaryDoctor?.Fullname, e.IsBooked, BookingTime = e.BookingTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND) },
                        AdmittedDate = e.OPDInitialAssessmentForTelehealth.AdmittedDate?.ToString(Constant.TIME_DATE_FORMAT),
                        HasOENDData = checkHasOENDData(e.OPDOutpatientExaminationNote)
                    }));
            else
                init_assessment_form.Add(new
                {
                    opd.Id,
                    Clinic = new { clinic?.Id, clinic?.ViName, clinic?.EnName, clinic?.Code },
                    PrimaryDoctor = new { primary_doctor?.Id, primary_doctor?.Username, primary_doctor?.Fullname, opd.IsBooked, BookingTime = opd.BookingTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND) },
                    AdmittedDate = iafth.AdmittedDate?.ToString(Constant.TIME_DATE_FORMAT),
                    HasOENDData = checkHasOENDData(opd.OPDOutpatientExaminationNote)
                });

            bool isNew = IsNew(iafth.CreatedAt, iafth.UpdatedAt);
            return Content(HttpStatusCode.OK, new
            {
                Username = isNew ? null : iafth.CreatedBy,
                opd.RecordCode,
                iafth.Id,
                Version = opd.Version >= 7 ? opd.Version : iafth.Version,
                ClinicId = clinic?.Id,
                InitialAssessmentForm = init_assessment_form,
                OPDId = opd.Id,
                Datas = iafth.OPDInitialAssessmentForTelehealthDatas.Where(i => !i.IsDeleted).Select(etrd => new { etrd.Id, etrd.Code, etrd.Value }).ToList(),
                Orders = "",
                IsNew = isNew,
                IsShowSyncButton = IsShowSyncButton(opd.CustomerId, opd.Id, opd.GroupId, opd.CreatedAt),
                IsLocked
            });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/InitialAssessments/ForTelehealth/{id}")]
        [Permission(Code = "OINAS12")]
        public IHttpActionResult UpdateInitialAssessmentsForTelehealthAPI(Guid id, [FromBody] JObject request)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var iafth = opd.OPDInitialAssessmentForTelehealth;
            if (iafth == null || iafth.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.OPD_IAFTH_NOT_FOUND);

            var user = GetUser();
            if (IsBlockAfter24h(opd.CreatedAt, iafth.Id) && !HasUnlockPermission(opd.Id, "OPDIAFTP", user.Username, iafth.Id))
                return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);

            if (!CanWrite(user, iafth.UpdatedBy, iafth.CreatedAt, iafth.UpdatedAt))
                return Content(HttpStatusCode.BadRequest, Message.OWNER_FORBIDDEN);

            HandleInitialAssessmentForTelehealthDatas(opd, iafth, request["Datas"]);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/InitialAssessments/ForTelehealth/Sync/{id}")]
        [Permission(Code = "OINAS13")]
        public IHttpActionResult SyncInitialAssessmentsForTelehealthAPI(Guid id)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);
            if (opd.AdmittedDate == null)
                return Content(HttpStatusCode.NotFound, Message.SYNC_24H_NOT_FOUND);

            var lastest_opd = GetLastestOPDIn24H(opd.CustomerId, opd.Id, opd.GroupId, opd.AdmittedDate);
            if (lastest_opd == null)
                return Content(HttpStatusCode.NotFound, Message.SYNC_24H_NOT_FOUND);

            var iafth = lastest_opd.OPDInitialAssessmentForTelehealth;
            if (iafth == null || iafth.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.SYNC_24H_NOT_FOUND);

            var clinic = lastest_opd.Clinic;

            return Content(HttpStatusCode.OK, new
            {
                iafth.Id,
                Version = opd.Version >= 7 ? opd.Version : iafth.Version,
                ClinicId = clinic?.Id,
                Clinic = new { clinic?.ViName, clinic?.EnName, clinic?.Code },
                AdmittedDate = iafth.AdmittedDate?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                OPDId = lastest_opd.Id,
                Datas = iafth.OPDInitialAssessmentForTelehealthDatas.Where(i => !i.IsDeleted).Select(etrd => new { etrd.Id, etrd.Code, etrd.Value }).ToList(),
                Orders = "",
                IsNew = IsNew(iafth.CreatedAt, iafth.UpdatedAt),
            });
        }

        [HttpGet]
        [Route("api/OPD/InitialAssessments/HISDoctor/{id}")]
        [Permission(Code = "OINAS14")]
        public IHttpActionResult GetHISDoctorAPI(Guid id)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);
            if (string.IsNullOrEmpty(opd.VisitCode))
                return Content(HttpStatusCode.NotFound, Message.VISIT_CODE_IS_MISSING);

            var customer = opd.Customer;
            if (customer == null || customer.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.CUSTOMER_NOT_FOUND);

            var site_code = GetSiteCode();
            if (string.IsNullOrEmpty(site_code))
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);
            else if (site_code == "times_city")
                return Content(HttpStatusCode.OK, EHosClient.GetHISDoctor(customer.PID, opd.VisitCode));
            else
                return Content(HttpStatusCode.OK, OHClient.GetHISDoctor(customer.PID, opd.VisitCode));
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/InitialAssessments/Delete/{id}")]
        [Permission(Code = "OINAS15")]
        public IHttpActionResult DeleteInitialAssessmentsAPI(Guid id, [FromBody] DeleteMedicalRecord request)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            if (string.IsNullOrWhiteSpace(request.Note)) return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);

            var user = GetUser();
            if (IsBlockAfter24h(opd.CreatedAt) && !HasUnlockPermission(opd.Id, new string[] { "OPDIAFTP", "OPDIAFSTOP", "OPDIAFOGOP", "OPD_A02_007_080121_VE" }, user.Username))
                return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);
            var response = TryToDeleteVisit(opd, user, request);
            if (opd.TransferFromId != null)
            {
                var check = GetAnesthesia(opd.TransferFromId.Value);
                if (check != null)
                {
                    check.IsAcceptNurse = false;
                    check.IsAcceptPhysician = false;
                    check.ReceivingNurseId = null;
                    check.ReceivingPhysicianId = null;
                    unitOfWork.OPDPreAnesthesiaHandOverCheckListRepository.Update(check);
                    unitOfWork.Commit();
                }
                //hàng chờ từ ED
                var checkEDHandOverCheckList = GetEDHandOverCheckList(opd.TransferFromId.Value);
                if (checkEDHandOverCheckList != null)
                {
                    checkEDHandOverCheckList.IsAcceptNurse = false;
                    checkEDHandOverCheckList.IsAcceptPhysician = false;
                    checkEDHandOverCheckList.ReceivingNurseId = null;
                    checkEDHandOverCheckList.ReceivingPhysicianId = null;
                    unitOfWork.HandOverCheckListRepository.Update(checkEDHandOverCheckList);
                    unitOfWork.Commit();
                }
            }
            //Hàng chờ OPD

            return response;
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/InitialAssessments/ChoosePrimaryDoctor/{id}/{*formId}")]
        [Permission(Code = "OINAS16")]
        public IHttpActionResult ChoosePrimaryDoctorAPI(Guid id, [FromBody] JObject request, Guid? formId = null)
        {
            var current_opd = GetOPD(id);
            if (current_opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var user = GetUser();
            if (IsBlockAfter24h(current_opd.CreatedAt, formId) && !HasUnlockPermission(current_opd.Id, new string[] { "OPDIAFTP", "OPDIAFSTOP", "OPDIAFOGOP", "OPD_A02_007_080121_VE" }, user.Username))
                return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);

            var current_iafst = current_opd.OPDInitialAssessmentForShortTerm;
            var current_iafst_data = current_iafst.OPDInitialAssessmentForShortTermDatas.Where(e => !e.IsDeleted).ToList();
            DateTime? last_iafst_update = current_iafst.UpdatedAt;

            var current_iafog = current_opd.OPDInitialAssessmentForOnGoing;
            var current_iafog_data = current_iafog.OPDInitialAssessmentForOnGoingDatas.Where(e => !e.IsDeleted).ToList();

            //var current_frs = current_opd.OPDFallRiskScreening;
            //var current_frs_data = current_frs.OPDFallRiskScreeningDatas.Where(e => !e.IsDeleted).ToList();

            OPDInitialAssessmentForTelehealth current_iatlh;
            List<OPDInitialAssessmentForTelehealthData> current_iatlh_data = new List<OPDInitialAssessmentForTelehealthData>();
            DateTime? last_iatlh_update = null;
            if (current_opd.IsTelehealth)
            {
                current_iatlh = current_opd.OPDInitialAssessmentForTelehealth;
                current_iatlh_data = current_iatlh.OPDInitialAssessmentForTelehealthDatas.Where(e => !e.IsDeleted).ToList();
                last_iatlh_update = current_iatlh.UpdatedAt;
            }

            foreach (var form in request["InitialAssessmentForm"])
            {
                OPD opd = GetOrCreateVisitInGroup(current_opd, form["Id"]);
                if (form["Clinic"] != null)
                    UpdateClinic(opd, form["Clinic"]);
                if (form["PrimaryDoctor"] != null)
                    UpdatePrimaryDoctor(opd, form["PrimaryDoctor"]);

                DateTime request_admitted_date = DateTime.ParseExact(form["AdmittedDate"]?.ToString(), Constant.TIME_DATE_FORMAT, null);

                var iafst = opd.OPDInitialAssessmentForShortTerm;
                if (request_admitted_date != null && iafst.AdmittedDate != request_admitted_date)
                    UpdateAdmittedDateInShortTerm(opd, request_admitted_date);

                var iafog = opd.OPDInitialAssessmentForOnGoing;
                if (request_admitted_date != null && iafog.AdmittedDate != request_admitted_date)
                    UpdateAdmittedDateInOnGoing(opd, request_admitted_date);

                OPDInitialAssessmentForTelehealth iatlh = null;
                if (current_opd.IsTelehealth)
                {
                    iatlh = opd.OPDInitialAssessmentForTelehealth;
                    if (request_admitted_date != null && iatlh.AdmittedDate != request_admitted_date)
                        UpdateAdmittedDateInTelehealth(opd, request_admitted_date);
                }

                if (!string.IsNullOrEmpty(form["Id"]?.ToString()) && form["Id"]?.ToString() == current_opd.Id.ToString()) continue;


                UpdateInitailAssetmentsForShortTerm(iafst, opd, current_iafst_data, last_iafst_update, last_iatlh_update);

                UpdateInitailAssetmentsForForOnGoing(iafog, current_iafog_data);

                //var frs = opd.OPDFallRiskScreening;
                //UpdateFallRiskScreening(opd, current_frs_data);

                if (current_opd.IsTelehealth)
                    UpdateInitailAssetmentsForTelehealth(iatlh, opd, current_iatlh_data, last_iafst_update, last_iatlh_update);
            }


            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpGet]
        [Route("api/OPD/InitialAssessments/ForShortTerm/DataFromViHC/{visitId}")]
        public IHttpActionResult GetDataFromViHC(Guid visitId)
        {
            var opd = GetOPD(visitId);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);
            DataFromViHCModel.Root root = new DataFromViHCModel.Root();
            string urlPostfix = string.Format("/EMRVinmecCom/1.0.0/VihcGetDauHieuSinhTon?PID={0}&VisitCode={1}", opd.Customer.PID, opd.VisitCode);

            try
            {
                var response = HISClient.RequestAsyncAPIDauHieuSinhTon(urlPostfix, "Result", "ICD10s");
                if (response != null)
                {
                    root = JsonConvert.DeserializeObject<DataFromViHCModel.Root>(response.ToString());
                }
            }
            catch
            {

            }

            return Content(HttpStatusCode.OK, new
            {
                DataFromViHC = root.Result
            });

        }

        #region ShorTerm
        private void HandleInitialAssessmentForShortTermDatas(OPD opd, OPDInitialAssessmentForShortTerm iafst, JToken request_iafst_data)
        {
            var iafst_datas = iafst.OPDInitialAssessmentForShortTermDatas.Where(i => !i.IsDeleted).ToList();
            bool is_change_data = false;

            CustomerUtil customer_util = new CustomerUtil(unitOfWork, opd.Customer);
            var allergy_dct = new Dictionary<string, string>();
            foreach (var item in request_iafst_data)
            {
                var code = item.Value<string>("Code");
                if (code == null)
                    continue;

                var value = item.Value<string>("Value");

                if (Constant.OPD_IAFST_VITAL_SIGN_CODE.Contains(code))
                    UpdateVitalSign(opd, code, value);

                if (Constant.OPD_IAFST_ALLERGIC_CODE.Contains(code))
                    allergy_dct[Constant.CUSTOMER_ALLERGY_SWITCH[code]] = value;

                var iafst_data = GetOrCreateOPDInitialAssessmentForShortTermData(iafst_datas, iafst.Id, code);
                if (iafst_data != null)
                {
                    var is_change_temp = UpdateOPDInitialAssessmentForShortTermData(opd, customer_util, iafst_data, code, value);
                    is_change_data = is_change_data || is_change_temp;
                }
            }
            var visit_util = new VisitAllergy(opd);
            visit_util.UpdateAllergy(allergy_dct);
            if (is_change_data)
            {
                var user = GetUser();
                iafst.UpdatedBy = user.Username;
                unitOfWork.OPDInitialAssessmentForShortTermRepository.Update(iafst);
                opd.PrimaryNurseId = user.Id;
                //opd.IsAllergy = customer_util.customer.IsAllergy;
                //opd.Allergy = customer_util.customer.Allergy;
                //opd.KindOfAllergy = customer_util.customer.KindOfAllergy;
                unitOfWork.OPDRepository.Update(opd);
                unitOfWork.Commit();
                PushNotification(opd, "đánh giá ban đầu thông thường", "OPDIAFST Change");
            }
        }
        private OPDInitialAssessmentForShortTermData GetOrCreateOPDInitialAssessmentForShortTermData(List<OPDInitialAssessmentForShortTermData> list_data, Guid iafst_id, string code)
        {
            OPDInitialAssessmentForShortTermData data = list_data.FirstOrDefault(e => !e.IsDeleted && !string.IsNullOrEmpty(e.Code) && e.Code == code);
            if (data != null)
                return data;

            data = new OPDInitialAssessmentForShortTermData
            {
                OPDInitialAssessmentForShortTermId = iafst_id,
                Code = code,
            };
            unitOfWork.OPDInitialAssessmentForShortTermDataRepository.Add(data);
            return data;
        }
        private bool UpdateOPDInitialAssessmentForShortTermData(OPD opd, CustomerUtil customer_util, OPDInitialAssessmentForShortTermData iafst_data, string code, string value)
        {
            if (iafst_data.Value == value)
                return false;

            iafst_data.Value = value;
            unitOfWork.OPDInitialAssessmentForShortTermDataRepository.Update(iafst_data);

            if (code == "OPDIAFSTOPHEIANS" && !string.IsNullOrEmpty(value))
                customer_util.UpdateHeight(value);
            else if (code == "OPDIAFSTOPWEIANS" && !string.IsNullOrEmpty(value))
                customer_util.UpdateWeight(value);
            return true;
        }
        #endregion

        #region OnGoing
        private void HandleInitialAssessmentForOnGoingDatas(OPD opd, OPDInitialAssessmentForOnGoing iafog, JToken request_iafog_data)
        {
            bool is_change_data = false;
            var iafog_datas = iafog.OPDInitialAssessmentForOnGoingDatas.Where(i => !i.IsDeleted).ToList();
            foreach (var item in request_iafog_data)
            {
                var code = item.Value<string>("Code");
                if (code == null)
                    continue;

                var value = item.Value<string>("Value");
                var iafog_data = GetOrCreateOPDInitialAssessmentForOnGoingData(iafog_datas, iafog.Id, code);
                if (iafog_data != null)
                {
                    var is_change_temp = UpdateOPDInitialAssessmentForOnGoingData(iafog_data, value);
                    is_change_data = is_change_data || is_change_temp;
                }
            }
            if (is_change_data)
            {
                var user = GetUser();
                iafog.UpdatedBy = user.Username;
                unitOfWork.OPDInitialAssessmentForOnGoingRepository.Update(iafog);
                opd.PrimaryNurseId = user.Id;
                unitOfWork.OPDRepository.Update(opd);
                unitOfWork.Commit();
                PushNotification(opd, "đánh giá ban đầu dài hạn", "OPDIAFOG Change");
            }
        }
        private OPDInitialAssessmentForOnGoingData GetOrCreateOPDInitialAssessmentForOnGoingData(List<OPDInitialAssessmentForOnGoingData> list_data, Guid iafog_id, string code)
        {
            OPDInitialAssessmentForOnGoingData data = list_data.FirstOrDefault(e => !e.IsDeleted && !string.IsNullOrEmpty(e.Code) && e.Code == code);
            if (data != null)
                return data;

            data = new OPDInitialAssessmentForOnGoingData
            {
                OPDInitialAssessmentForOnGoingId = iafog_id,
                Code = code,
            };
            unitOfWork.OPDInitialAssessmentForOnGoingDataRepository.Add(data);
            return data;
        }
        private bool UpdateOPDInitialAssessmentForOnGoingData(OPDInitialAssessmentForOnGoingData iafog_data, string value)
        {
            if (iafog_data.Value == value)
                return false;

            iafog_data.Value = value;
            unitOfWork.OPDInitialAssessmentForOnGoingDataRepository.Update(iafog_data);
            return true;
        }
        #endregion

        #region Telehealth
        private void HandleInitialAssessmentForTelehealthDatas(OPD opd, OPDInitialAssessmentForTelehealth iafth, JToken request_iafth_data)
        {
            var user = GetUser();
            if (IsNew(iafth.CreatedAt, iafth.UpdatedAt))
                iafth.CreatedBy = user.Username;

            bool is_change_data = false;
            var iafth_datas = iafth.OPDInitialAssessmentForTelehealthDatas.Where(i => !i.IsDeleted).ToList();

            var customer_util = new CustomerUtil(unitOfWork, opd.Customer);
            var allergy_dct = new Dictionary<string, string>();
            foreach (var item in request_iafth_data)
            {
                var code = item.Value<string>("Code");
                if (code == null)
                    continue;
                var value = item.Value<string>("Value");

                if (Constant.OPD_IAFTP_VITAL_SIGN_CODE.Contains(code))
                    UpdateVitalSign(opd, code, value);

                if (Constant.OPD_IAFTP_ALLERGIC_CODE.Contains(code))
                    allergy_dct[Constant.CUSTOMER_ALLERGY_SWITCH[code]] = value;

                var iafth_data = GetOrCreateOPDInitialAssessmentForTelehealthData(iafth_datas, iafth.Id, code);
                if (iafth_data != null)
                {
                    var is_change_temp = UpdateOPDInitialAssessmentForTelehealthData(opd, customer_util, iafth_data, code, value);
                    is_change_data = is_change_data || is_change_temp;
                }
            }
            if (is_change_data)
            {
                var visit_util = new VisitAllergy(opd);
                visit_util.UpdateAllergy(allergy_dct);
                iafth.UpdatedBy = user.Username;
                unitOfWork.OPDInitialAssessmentForTelehealthRepository.Update(iafth);
                opd.PrimaryNurseId = user.Id;
                unitOfWork.OPDRepository.Update(opd);
                unitOfWork.Commit();
                PushNotification(opd, "đánh giá ban đầu NB từ xa", "OPDIAFTH Change");
            }
        }
        private OPDInitialAssessmentForTelehealthData GetOrCreateOPDInitialAssessmentForTelehealthData(List<OPDInitialAssessmentForTelehealthData> list_data, Guid iafth_id, string code)
        {
            OPDInitialAssessmentForTelehealthData data = list_data.FirstOrDefault(e => !e.IsDeleted && !string.IsNullOrEmpty(e.Code) && e.Code == code);
            if (data != null)
                return data;

            data = new OPDInitialAssessmentForTelehealthData
            {
                OPDInitialAssessmentForTelehealthId = iafth_id,
                Code = code,
            };
            unitOfWork.OPDInitialAssessmentForTelehealthDataRepository.Add(data);
            return data;
        }
        private bool UpdateOPDInitialAssessmentForTelehealthData(OPD opd, CustomerUtil customer_util, OPDInitialAssessmentForTelehealthData iafth_data, string code, string value)
        {
            if (iafth_data.Value == value)
                return false;

            iafth_data.Value = value;
            unitOfWork.OPDInitialAssessmentForTelehealthDataRepository.Update(iafth_data);

            if (code == "OPDIAFTPHEIANS" && !string.IsNullOrEmpty(value))
                customer_util.UpdateHeight(value);
            else if (code == "OPDIAFTPWEIANS" && !string.IsNullOrEmpty(value))
                customer_util.UpdateWeight(value);
            return true;
        }
        private void CreateInitialAssessmentForTelehealth(OPD current_opd)
        {
            var group_opd = new List<OPD>();
            if (current_opd.GroupId != null)
                group_opd.AddRange(unitOfWork.OPDRepository.Find(
                    e => !e.IsDeleted && e.GroupId != null && e.GroupId == current_opd.GroupId)
                );
            else
                group_opd.Add(current_opd);

            foreach (var opd in group_opd)
            {
                if (opd.OPDInitialAssessmentForTelehealthId != null) continue;
                var telehealth = new OPDInitialAssessmentForTelehealth();
                telehealth.AdmittedDate = opd.AdmittedDate;
                telehealth.Version = opd.Version >= 7 ? opd.Version : 2;
                unitOfWork.OPDInitialAssessmentForTelehealthRepository.Add(telehealth);
                opd.OPDInitialAssessmentForTelehealth = telehealth;
                opd.IsTelehealth = true;
                unitOfWork.OPDRepository.Update(opd);
            }
            unitOfWork.Commit();
        }
        #endregion

        #region FallRiskScreen
        private void HandleFallRiskScreening(OPD current_opd, Guid id, JToken request_frs_data)
        {
            //var list_group = new List<OPD>();
            //if (current_opd.GroupId != null)
            //    list_group.AddRange(
            //        unitOfWork.OPDRepository.Find(e => !e.IsDeleted && e.GroupId != null && e.GroupId == current_opd.GroupId)
            //    );
            //else
            //    list_group.Add(current_opd);

            var user = GetUser();
            //foreach (var opd in list_group)
            //{
            bool is_change = false;
            var frs = unitOfWork.OPDFallRiskScreeningRepository.FirstOrDefault(x => x.VisitId == current_opd.Id && x.Id == id && !x.IsDeleted);
            var frs_datas = unitOfWork.OPDFallRiskScreeningDataRepository.Find(x => !x.IsDeleted && x.OPDFallRiskScreeningId == frs.Id).ToList();
            foreach (var item in request_frs_data)
            {
                var code = item.Value<string>("Code");
                if (code == null)
                    continue;

                var value = item.Value<string>("Value");
                var frs_data = GetOrCreateOPDFallRiskScreeningData(frs_datas, frs.Id, code);
                if (frs_data != null)
                {
                    var is_change_temp = UpdateOPDFallRiskScreeningData(frs_data, code, value);
                    is_change = is_change || is_change_temp;
                }
            }
            frs.UpdatedBy = user.Username;
            unitOfWork.OPDFallRiskScreeningRepository.Update(frs);
            //}
            unitOfWork.Commit();
        }
        private OPDFallRiskScreeningData GetOrCreateOPDFallRiskScreeningData(List<OPDFallRiskScreeningData> list_data, Guid frs_id, string code)
        {
            OPDFallRiskScreeningData data = list_data.FirstOrDefault(e => !e.IsDeleted && !string.IsNullOrEmpty(e.Code) && e.Code == code);
            if (data != null)
                return data;

            data = new OPDFallRiskScreeningData
            {
                OPDFallRiskScreeningId = frs_id,
                Code = code,
            };
            unitOfWork.OPDFallRiskScreeningDataRepository.Add(data);
            return data;
        }
        private bool UpdateOPDFallRiskScreeningData(OPDFallRiskScreeningData frs_data, string code, string value)
        {
            if ("OPDFRSFOPTD0ANS1,OPDFRSFOPTD0ANS2,OPDFRSFOPTD0ANS3".Contains(code) && !Validator.ValidateTimeDateWithoutSecond(value))
                return false;

            if (frs_data.Value == value)
                return false;

            frs_data.Value = value;
            unitOfWork.OPDFallRiskScreeningDataRepository.Update(frs_data);
            return true;
        }
        #endregion
        #region For Chemotherapy patient

        [HttpGet]
        [Route("api/OPD/InitialAssessments/ForChemotherapyPatient/{visitId}")]
        public IHttpActionResult GetForChemotherapyPatient(Guid visitId)
        {
            var opd = GetOPD(visitId);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);
            var customer = opd.Customer;
            return Content(HttpStatusCode.OK, new
            {
                customer.Job,
                allergy = EMRVisitAllergy.GetOPDVisitAllergy(opd)
        });

        }
        #endregion
        #region General
        private OPD GetOrCreateVisitInGroup(OPD current_opd, JToken request_id)
        {
            OPD new_opd;
            var str_id = request_id?.ToString();
            if (!string.IsNullOrEmpty(str_id))
            {
                var opd_id = new Guid(str_id);
                if (current_opd.Id == opd_id)
                    return current_opd;

                new_opd = unitOfWork.OPDRepository.GetById(opd_id);
                SyncOPDVisitInfo(src: current_opd, dst: new_opd);
                return new_opd;
            }

            if (current_opd.GroupId == null)
            {
                current_opd.GroupId = Guid.NewGuid();
                unitOfWork.OPDRepository.Update(current_opd);
            }

            InHospital in_hospital = new InHospital();
            var specialty = GetSpecialty();
            bool isAnesthesia = specialty == null ? false : specialty.IsAnesthesia;
            var creater = new VisitCreater(
                unitOfWork,
                in_hospital.GetStatus("OPD").Id,
                current_opd.CustomerId,
                null,
                current_opd.GroupId,
                current_opd.VisitCode,
                GetSiteId(),
                GetSpecialtyId(),
                current_opd.PrimaryNurseId,
                isAnesthesia
            );
            new_opd = creater.CreateNewOPD();

            if (current_opd.IsTelehealth)
                CreateOPDInitialAssessmentForTelehealth(new_opd);

            SyncOPDVisitInfo(src: current_opd, dst: new_opd);
            return new_opd;
        }

        private void UpdateInitailAssetmentsForShortTerm(OPDInitialAssessmentForShortTerm iafst, OPD opd, List<OPDInitialAssessmentForShortTermData> current_iafst_data, DateTime? last_iafst_update, DateTime? last_iatlh_update)
        {
            //iafst.UpdatedAt = DateTime.Now.AddMinutes(1);
            unitOfWork.OPDInitialAssessmentForShortTermRepository.Update(iafst, is_time_change: false, is_anonymous: true);
            var iafst_datas = iafst.OPDInitialAssessmentForShortTermDatas.Where(i => !i.IsDeleted).ToList();
            foreach (var current_data in current_iafst_data)
            {
                var data = GetOrCreateOPDInitialAssessmentForShortTermData(iafst_datas, iafst.Id, current_data.Code);
                data.Value = current_data.Value;
                unitOfWork.OPDInitialAssessmentForShortTermDataRepository.Update(data);

                if (Constant.OPD_IAFST_VITAL_SIGN_CODE.Contains(current_data.Code) && (last_iatlh_update == null || (last_iatlh_update != null && last_iafst_update > last_iatlh_update)))
                    UpdateVitalSign(opd, current_data.Code, current_data.Value);
            }
            unitOfWork.Commit();
        }
        private void UpdateInitailAssetmentsForForOnGoing(OPDInitialAssessmentForOnGoing iafog, List<OPDInitialAssessmentForOnGoingData> current_iafog_data)
        {
            // iafog.UpdatedAt = DateTime.Now.AddMinutes(1);
            // unitOfWork.OPDInitialAssessmentForOnGoingRepository.Update(iafog);
            var iafst_datas = iafog.OPDInitialAssessmentForOnGoingDatas.Where(i => !i.IsDeleted).ToList();
            foreach (var current_data in current_iafog_data)
            {
                var data = GetOrCreateOPDInitialAssessmentForOnGoingData(iafst_datas, iafog.Id, current_data.Code);
                data.Value = current_data.Value;
                unitOfWork.OPDInitialAssessmentForOnGoingDataRepository.Update(data);
            }
            unitOfWork.Commit();
        }
        private void UpdateFallRiskScreening(OPD new_opd, List<OPDFallRiskScreeningData> current_frs_data)
        {
            var iafst = new_opd.OPDFallRiskScreening;
            iafst.UpdatedAt = DateTime.Now.AddMinutes(1);
            unitOfWork.OPDFallRiskScreeningRepository.Update(iafst);
            var iafst_datas = iafst.OPDFallRiskScreeningDatas.Where(i => !i.IsDeleted).ToList();
            foreach (var current_data in current_frs_data)
            {
                var data = GetOrCreateOPDFallRiskScreeningData(iafst_datas, iafst.Id, current_data.Code);
                data.Value = current_data.Value;
                unitOfWork.OPDFallRiskScreeningDataRepository.Update(data);
            }
            unitOfWork.Commit();
        }
        private void CreateOPDInitialAssessmentForTelehealth(OPD new_opd)
        {
            var telehealth = new OPDInitialAssessmentForTelehealth();
            telehealth.AdmittedDate = new_opd.AdmittedDate;
            unitOfWork.OPDInitialAssessmentForTelehealthRepository.Add(telehealth);
            new_opd.OPDInitialAssessmentForTelehealth = telehealth;
            new_opd.IsTelehealth = true;
            unitOfWork.OPDRepository.Update(new_opd);
        }
        private void UpdateInitailAssetmentsForTelehealth(OPDInitialAssessmentForTelehealth iafth, OPD opd, List<OPDInitialAssessmentForTelehealthData> current_iafth_data, DateTime? last_iafst_update, DateTime? last_iatlh_update)
        {
            iafth.UpdatedAt = DateTime.Now.AddMinutes(1);
            unitOfWork.OPDInitialAssessmentForTelehealthRepository.Update(iafth);
            var iafst_datas = iafth.OPDInitialAssessmentForTelehealthDatas.Where(i => !i.IsDeleted).ToList();
            foreach (var current_data in current_iafth_data)
            {
                var data = GetOrCreateOPDInitialAssessmentForTelehealthData(iafst_datas, iafth.Id, current_data.Code);
                data.Value = current_data.Value;
                unitOfWork.OPDInitialAssessmentForTelehealthDataRepository.Update(data);

                if (Constant.OPD_IAFTP_VITAL_SIGN_CODE.Contains(current_data.Code) && last_iafst_update <= last_iatlh_update)
                    UpdateVitalSign(opd, current_data.Code, current_data.Value);
            }
            unitOfWork.Commit();
        }


        private void UpdateAdmittedDateInOnGoing(OPD opd, DateTime admited_date)
        {
            var iafog = opd.OPDInitialAssessmentForOnGoing;
            iafog.AdmittedDate = admited_date;
            unitOfWork.OPDInitialAssessmentForOnGoingRepository.Update(iafog);

            var iafst = opd.OPDInitialAssessmentForShortTerm;
            iafst.AdmittedDate = admited_date;
            unitOfWork.OPDInitialAssessmentForShortTermRepository.Update(iafst, is_anonymous: true, is_time_change: false);

            if (opd.IsTelehealth)
            {
                var tele = opd.OPDInitialAssessmentForTelehealth;
                tele.AdmittedDate = admited_date;
                unitOfWork.OPDInitialAssessmentForTelehealthRepository.Update(tele, is_anonymous: true, is_time_change: false);
            }

            if (opd.IsRetailService)
            {
                var afrsp = opd.EIOAssessmentForRetailServicePatient;
                afrsp.TriageDateTime = admited_date;
                unitOfWork.EIOAssessmentForRetailServicePatientRepository.Update(afrsp, is_anonymous: true, is_time_change: false);
            }

            opd.AdmittedDate = admited_date;
            unitOfWork.OPDRepository.Update(opd);
            unitOfWork.Commit();

            UpdateRecordCodeOfCustomer((Guid)opd.CustomerId);
        }
        private void UpdateAdmittedDateInShortTerm(OPD opd, DateTime admited_date)
        {
            var iafst = opd.OPDInitialAssessmentForShortTerm;
            iafst.AdmittedDate = admited_date;
            unitOfWork.OPDInitialAssessmentForShortTermRepository.Update(iafst, is_time_change: false, is_anonymous: true);

            var iafog = opd.OPDInitialAssessmentForOnGoing;
            iafog.AdmittedDate = admited_date;
            unitOfWork.OPDInitialAssessmentForOnGoingRepository.Update(iafog, is_anonymous: true, is_time_change: false);

            if (opd.IsTelehealth)
            {
                var tele = opd.OPDInitialAssessmentForTelehealth;
                tele.AdmittedDate = admited_date;
                unitOfWork.OPDInitialAssessmentForTelehealthRepository.Update(tele, is_anonymous: true, is_time_change: false);
            }

            if (opd.IsRetailService)
            {
                var afrsp = opd.EIOAssessmentForRetailServicePatient;
                afrsp.TriageDateTime = admited_date;
                unitOfWork.EIOAssessmentForRetailServicePatientRepository.Update(afrsp, is_anonymous: true, is_time_change: false);
            }

            opd.AdmittedDate = admited_date;
            unitOfWork.OPDRepository.Update(opd);
            unitOfWork.Commit();

            UpdateRecordCodeOfCustomer((Guid)opd.CustomerId);
        }
        private void UpdateAdmittedDateInTelehealth(OPD opd, DateTime admited_date)
        {
            var tele = opd.OPDInitialAssessmentForTelehealth;
            if (tele != null)
            {
                tele.AdmittedDate = admited_date;
                unitOfWork.OPDInitialAssessmentForTelehealthRepository.Update(tele);

                var iafst = opd.OPDInitialAssessmentForShortTerm;
                iafst.AdmittedDate = admited_date;
                unitOfWork.OPDInitialAssessmentForShortTermRepository.Update(iafst, is_anonymous: true, is_time_change: false);

                var iafog = opd.OPDInitialAssessmentForOnGoing;
                iafog.AdmittedDate = admited_date;
                unitOfWork.OPDInitialAssessmentForOnGoingRepository.Update(iafog, is_anonymous: true, is_time_change: false);

                if (opd.IsRetailService)
                {
                    var afrsp = opd.EIOAssessmentForRetailServicePatient;
                    afrsp.TriageDateTime = admited_date;
                    unitOfWork.EIOAssessmentForRetailServicePatientRepository.Update(afrsp, is_anonymous: true, is_time_change: false);
                }

                opd.AdmittedDate = admited_date;
                unitOfWork.OPDRepository.Update(opd);

                unitOfWork.Commit();

                UpdateRecordCodeOfCustomer((Guid)opd.CustomerId);
            }
        }
        private void UpdateClinic(OPD opd, JToken clinic_request)
        {
            var current_clinic_id = opd.ClinicId;
            var request_id = string.Empty;
            try
            {
                request_id = clinic_request["Id"]?.ToString();
            }
            catch (Exception) { }
            if (string.IsNullOrEmpty(request_id))
                return;

            var new_id = new Guid(request_id);
            if (current_clinic_id != null && current_clinic_id == new_id)
                return;

            var service = unitOfWork.ClinicRepository.GetById((Guid)new_id).ViName;
            var note = opd.OPDOutpatientExaminationNote;
            note.Service = service;
            unitOfWork.OPDOutpatientExaminationNoteRepository.Update(note, is_anonymous: true, is_time_change: false);

            opd.ClinicId = new_id;
            unitOfWork.OPDRepository.Update(opd);

            unitOfWork.Commit();
        }
        private void UpdateVitalSign(OPD opd, string code, string value)
        {
            var oen_data_code = Constant.OPD_OEN_VITAL_SIGN_CODE_SWITCH[code];
            var oen = opd.OPDOutpatientExaminationNote;
            var oen_data = oen.OPDOutpatientExaminationNoteDatas.FirstOrDefault(e => e.Code == oen_data_code);
            if (oen_data == null)
                CreateExaminationNoteData(oen.Id, oen_data_code, value);
            else if (oen_data.Value != value)
                UpdateExaminationNoteData(oen_data, value);
        }
        private void CreateExaminationNoteData(Guid oen_id, string code, string value)
        {
            OPDOutpatientExaminationNoteData new_oen_data = new OPDOutpatientExaminationNoteData
            {
                OPDOutpatientExaminationNoteId = oen_id,
                Code = code,
                Value = value
            };
            unitOfWork.OPDOutpatientExaminationNoteDataRepository.Add(new_oen_data);
        }
        private void UpdateExaminationNoteData(OPDOutpatientExaminationNoteData oen_data, string value)
        {
            oen_data.Value = value;
            unitOfWork.OPDOutpatientExaminationNoteDataRepository.Update(oen_data);
        }
        private OPD GetLastestOPDIn24H(Guid? customer_id, Guid opd_id, Guid? group_id, DateTime? opd_admitted_date)
        {
            var time = DateTime.Now.AddDays(-1);
            if (group_id == null)
            {
                var opd_lists = unitOfWork.OPDRepository.Find(
                    e => !e.IsDeleted &&
                    e.Id != opd_id &&
                    e.CustomerId != null &&
                    e.CustomerId == customer_id &&
                    e.AdmittedDate != null &&
                    e.AdmittedDate >= time &&
                    e.AdmittedDate < opd_admitted_date
                ).OrderByDescending(e => e.AdmittedDate).ToList();
                if (opd_lists.Count > 0)
                    return opd_lists[0];
                return null;
            }
            var opd_list = unitOfWork.OPDRepository.Find(
                    e => !e.IsDeleted &&
                    e.Id != opd_id &&
                    (e.GroupId == null || e.GroupId != group_id) &&
                    e.CustomerId != null &&
                    e.CustomerId == customer_id &&
                    e.AdmittedDate != null &&
                    e.AdmittedDate >= time &&
                    e.AdmittedDate < opd_admitted_date
                ).OrderByDescending(e => e.AdmittedDate).ToList();
            if (opd_list.Count > 0)
                return opd_list[0];
            return null;
        }
        private EOC GetLastestEDCIn24H(Guid? customer_id, DateTime? opd_admitted_date)
        {
            var time = DateTime.Now.AddDays(-1);
            var opd_lists = unitOfWork.EOCRepository.Find(
                     e => !e.IsDeleted &&
                     e.CustomerId != null &&
                     e.CustomerId == customer_id &&
                     e.AdmittedDate != null &&
                     e.AdmittedDate >= time &&
                     e.AdmittedDate < opd_admitted_date
                 ).OrderByDescending(e => e.AdmittedDate).ToList();
            if (opd_lists.Count > 0)
                return opd_lists[0];
            return null;
        }
        private bool IsShowSyncButton(Guid? customer_id, Guid opd_id, Guid? group_id, DateTime? opd_admitted_date)
        {
            var opd = GetLastestOPDIn24H(customer_id, opd_id, group_id, opd_admitted_date);
            return opd != null;
        }
        private bool IsShowSyncEDCButton(Guid? customer_id, DateTime? opd_admitted_date)
        {
            var opd = GetLastestEDCIn24H(customer_id, opd_admitted_date);
            return opd != null;
        }
        private void UpdatePrimaryDoctor(OPD opd, JToken doctor)
        {
            var primary_doctor = opd.PrimaryDoctor;
            var request_id = string.Empty;
            try
            {
                request_id = doctor["Id"]?.ToString();
            }
            catch (Exception) { }
            if (!string.IsNullOrEmpty(request_id))
            {
                var id = new Guid(request_id);
                if (primary_doctor == null || primary_doctor.Id != id)
                {
                    opd.PrimaryDoctorId = id;
                    var is_booked = doctor["IsBooked"]?.ToString();
                    if (!string.IsNullOrEmpty(is_booked))
                    {
                        opd.IsBooked = is_booked == "true" ? true : false;
                    }
                    var booking_time = doctor["BookingTime"]?.ToString();
                    if (!string.IsNullOrEmpty(booking_time))
                    {
                        opd.BookingTime = DateTime.ParseExact(booking_time, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
                    }
                    unitOfWork.OPDRepository.Update(opd);
                    unitOfWork.Commit();
                }
            }
        }

        private dynamic TryToDeleteVisit(OPD opd, User user, DeleteMedicalRecord request)
        {
            var oen = opd.OPDOutpatientExaminationNote;
            if (!IsNew(oen.CreatedAt, oen.UpdatedAt))
                return Content(HttpStatusCode.BadRequest, Message.DELETE_FORBIDDEN);

            if (string.IsNullOrWhiteSpace(request.Note)) return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);

            var initial_assessment = new List<dynamic>() {
                    opd.OPDInitialAssessmentForShortTerm,
                    opd.OPDInitialAssessmentForOnGoing,
                    //opd.OPDFallRiskScreening,
                    opd.OPDInitialAssessmentForTelehealth
                };

            bool is_delete = user.Username == opd.CreatedBy;
            if (!is_delete)
                foreach (var form in initial_assessment)
                    if (form != null && user.Username == form.UpdatedBy)
                    {
                        is_delete = true;
                        break;
                    }
            if (is_delete)
            {
                try
                {
                    // unitOfWork.OPDInitialAssessmentForShortTermRepository.Delete(initial_assessment[0]);
                    // unitOfWork.OPDInitialAssessmentForOnGoingRepository.Delete(initial_assessment[1]);
                    /// var OPDFallRiskScreenings = unitOfWork.OPDFallRiskScreeningRepository.Find(x => x.VisitId == opd.Id && !x.IsDeleted);
                    // foreach(var item  in OPDFallRiskScreenings)
                    // {
                    // unitOfWork.OPDFallRiskScreeningRepository.Delete(item);
                    // }    
                    // if (initial_assessment[3] != null)
                    // unitOfWork.OPDInitialAssessmentForTelehealthRepository.Delete(initial_assessment[3]);
                    // unitOfWork.OPDOutpatientExaminationNoteRepository.Delete(opd.OPDOutpatientExaminationNote);
                    unitOfWork.OPDRepository.Delete(opd);
                    unitOfWork.Commit();
                    setLog(new Log
                    {
                        Action = "DELETE OPD",
                        URI = opd.Id.ToString(),
                        Name = "DELETE OPD",
                        Request = opd.Id.ToString(),
                        Reason = request.Note,
                    });
                }
                catch (Exception)
                {
                    return Content(HttpStatusCode.BadRequest, Message.INTERAL_SERVER_ERROR);
                }
                return Content(HttpStatusCode.OK, Message.SUCCESS);
            }
            return Content(HttpStatusCode.BadRequest, Message.DELETE_FORBIDDEN);
        }

        private void SyncOPDVisitInfo(OPD src, OPD dst)
        {
            dst.VisitCode = src.VisitCode;
            dst.HealthInsuranceNumber = src.HealthInsuranceNumber;
            dst.StartHealthInsuranceDate = src.StartHealthInsuranceDate;
            dst.ExpireHealthInsuranceDate = src.ExpireHealthInsuranceDate;
            unitOfWork.OPDRepository.Update(dst);
            unitOfWork.Commit();
        }

        private bool CanWrite(User user, string updated_by, DateTime? created_at, DateTime? updated_at)
        {
            if (created_at == updated_at)
                return true;

            var positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName);
            if (positions.Contains("Administrator"))
                return true;

            var last_user = unitOfWork.UserRepository.FirstOrDefault(e => !e.IsDeleted && e.Username == updated_by);
            var last_user_positions = last_user?.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName);
            if (last_user_positions == null)
                return false;

            if (last_user_positions.Contains("Administrator") || (last_user_positions.Contains("Nurse") && last_user == user))
                return true;

            return false;
        }

        private void PushNotification(OPD opd, string form_name, string form_frontend)
        {
            var user = GetUser();
            var spec = opd.Specialty;
            var customer = opd.Customer;

            var noti_creator = new NotificationCreator(
                unitOfWork: unitOfWork,
                from_user: user?.Username,
                to_user: opd.PrimaryDoctor?.Username,
                priority: 7,
                vi_message: $"<b>[OPD-{spec?.ViName}]</b> Điều dưỡng <b>{user.Fullname}</b> đã chỉnh sửa <b>{form_name}</b> của bệnh nhân <b>{customer.Fullname}</b>",
                en_message: $"<b>[OPD-{spec?.ViName}]</b> Điều dưỡng <b>{user.Fullname}</b> đã chỉnh sửa <b>{form_name}</b> của bệnh nhân <b>{customer.Fullname}</b>",
                spec_id: spec?.Id,
                visit_id: opd.Id,
                group_code: "OPD",
                form_frontend: form_frontend
            );
            noti_creator.Create();
        }
        #endregion
        private bool CanWriteShortTerm(User user, string created_by, Guid idShortTerm, DateTime? created_at, DateTime? updated_at)
        {
            var shortTermData = unitOfWork.OPDInitialAssessmentForShortTermDataRepository.Find(x => x.OPDInitialAssessmentForShortTermId == idShortTerm && !x.IsDeleted).ToList();
            if (created_at == updated_at)
                return true;

            var positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName);
            if (positions.Contains("Administrator"))
                return true;

            var last_user = unitOfWork.UserRepository.FirstOrDefault(e => !e.IsDeleted && e.Username == created_by);
            var last_user_positions = last_user?.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName);
            if (last_user_positions == null)
                return false;
            if (last_user_positions.Contains("Administrator"))
            {
                return true;
            }
            if (shortTermData.Count > 0)
            {
                if (last_user_positions.Contains("Nurse") && last_user == user)
                {
                    return true;
                }
            }
            else
            {
                return true;
            }

            return false;
        }
        private OPDFallRiskScreening GetForm(Guid visitId, Guid id)
        {
            var form = unitOfWork.OPDFallRiskScreeningRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == visitId && e.Id == id);
            return form;
        }
        public OPDPreAnesthesiaHandOverCheckList GetAnesthesia(Guid id)
        {
            var check = unitOfWork.OPDPreAnesthesiaHandOverCheckListRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == id);
            return check;
        }
        public EDHandOverCheckList GetEDHandOverCheckList(Guid id)
        {
            var check = unitOfWork.HandOverCheckListRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == id);
            return check;
        }
    }
}
