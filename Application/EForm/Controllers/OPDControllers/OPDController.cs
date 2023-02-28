using DataAccess.Models.OPDModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers;
using EForm.Models;
using EForm.Models.OPDModels;
using EMRModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.OPDControllers
{
    [SessionAuthorize]
    public class OPDController : BaseOPDApiController
    {
        private List<listMedicalRecordIsDeploy> getPromissoryNoteBySetup;

        [HttpGet]
        [Route("api/OPD/OPD")]
        [Permission(Code = "OOPD1")]
        public IHttpActionResult GetCustomers([FromUri]OPDParameterModel request)
        {
            if (!request.Validate())
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);
            using (var txn = GetNewReadUncommittedScope())
            {
                var specialtyId = Guid.Empty;
                var specialty = GetSpecialty();
                if (specialty != null)
                    specialtyId = specialty.Id;

                getPromissoryNoteBySetup = (from m in unitOfWork.IPDSetupMedicalRecordRepository.AsQueryable()
                                            where m.SpecialityId == specialtyId && m.IsDeploy
                                            join mas in unitOfWork.MasterDataRepository.AsQueryable()
                                            .Where(m => !m.IsDeleted && m.Note == "OPD")
                                            on m.Formcode equals mas.Group
                                            select new listMedicalRecordIsDeploy()
                                            {
                                                ViName = mas.ViName,
                                                EnName = mas.EnName,
                                                Type = mas.Code,
                                                FormCode = mas.Form
                                            }).ToList();

                var user = GetUser();
                var positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName);
                if (request.IsPid)
                {
                    var customer = GetCustomerByPid(request.Search);
                    if (customer == null) return Content(HttpStatusCode.OK, new { count = 0, results = new Array[] { } });
                    request.CustomerId = customer.Id;
                }

                if (specialty?.IsAnesthesia == true)
                    return Content(HttpStatusCode.OK, GetOPDForNurse(request));

                if (positions.Contains("Nurse")
                    || positions.Contains("Medical Secretary")
                    || positions.Contains("Administrator"))
                    return Content(HttpStatusCode.OK, GetOPDForNurse(request));

                if (positions.Contains("Doctor") || positions.Contains("CMO")
                    || positions.Contains("Director")
                    || positions.Contains("Pharmacist")
                    || positions.Contains("Head Of Department"))
                    return Content(HttpStatusCode.OK, GetOPDForDoctor(request));
                return Content(HttpStatusCode.OK, GetOPDForNurse(request));
            }
        }

        private dynamic GetOPDForNurse(OPDParameterModel request)
        {
            using (var txn = GetNewReadUncommittedScope())
            {
                var site_id = GetSiteId();
                var specialty_id = GetSpecialtyId();
                var query = FilterCustomerForNurse(site_id, specialty_id);

                if (request.Search != null)
                {
                    if (request.ConvertedSearch.Length == 8)
                    {
                        bool isPID = true;
                        char[] arrChar = request.ConvertedSearch.ToCharArray();
                        foreach (char c in arrChar)
                        {
                            if (c < 48 || c > 57)
                            {
                                isPID = false;
                                break;
                            }
                        }

                        if (isPID)
                        {
                            query = FilterByCustomer(query, $"8{request.ConvertedSearch}", request.CustomerId);
                        }
                        else
                        {
                            query = FilterByCustomer(query, request.ConvertedSearch, request.CustomerId);
                        }
                    }
                    else
                    {
                        query = FilterByCustomer(query, request.ConvertedSearch, request.CustomerId);
                    }
                }

                if (request.EmergencyStatus != null)
                    query = FilterByEmergencyStatus(query, request.ConvertedEmergencyStatus);

                if (request.User != null)
                    query = FilterByUser(query, request.ConvertedUser);

                if (request.StartAdmittedDate != null && request.EndAdmittedDate != null)
                    query = FilterByAdmittedDate(query, request.ConvertedStartAdmittedDate, request.ConvertedEndAdmittedDate);
                else if (request.StartAdmittedDate != null)
                    query = FilterByStartAdmittedDate(query, request.ConvertedStartAdmittedDate);
                else if (request.EndAdmittedDate != null)
                    query = FilterByEndAdmittedDate(query, request.ConvertedEndAdmittedDate);

                if (request.IsTelehealth != null)
                    query = FilterByTelehealth(query, (bool)request.IsTelehealth);

                if (request.IsRetailService != null)
                    query = FilterByRetailService(query, (bool)request.IsRetailService);

                if (request.ClinicId != null)
                    query = FilterByClinicId(query, Guid.Parse(request.ClinicId));

                var current_user = getUsername();
                if (!IsVIPMANAGE())
                    query = query.Where(e => e.UnlockFor != null && (e.UnlockFor == "ALL" || ("," + e.UnlockFor + ",").Contains("," + current_user + ",")));

                query = query.OrderByDescending(m => m.AdmittedDateDate).ThenBy(m => m.EDStatusCreatedAt).ThenByDescending(m => m.AdmittedDate);
                int count = query.Count();
                var done_query = query.Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToList();

                var listNure = done_query.Where(e => e.PrimaryNurseId != null).Select(e => e.PrimaryNurseId).ToList();
                var listDoc = done_query.Where(e => e.PrimaryDoctorId != null).Select(e => e.PrimaryDoctorId).ToList();
                var listAutho = done_query.Where(e => e.AuthorizedDoctorId != null).Select(e => e.AuthorizedDoctorId).ToList();
                var listUserId = listNure.Concat(listDoc).Concat(listAutho);

                var listUser = unitOfWork.UserRepository.Find(e => listUserId.Contains(e.Id)).ToList();
                var items = done_query.Select(e => DataFormatted(e, listUser));
                return new { count, results = items };
            }
        }


        private dynamic GetOPDForDoctor(OPDParameterModel request)
        {
            using (var txn = GetNewReadUncommittedScope())
            {
                var site_id = GetSiteId();
                var specialty_id = GetSpecialtyId();
                var user_id = GetUser().Id;
                var query = FilterCustomerForDoctor(site_id, specialty_id, user_id);

                if (request.Search != null)
                {
                    if (request.ConvertedSearch.Length == 8)
                    {
                        bool isPID = true;
                        char[] arrChar = request.ConvertedSearch.ToCharArray();
                        foreach (char c in arrChar)
                        {
                            if (c < 48 || c > 57)
                            {
                                isPID = false;
                                break;
                            }
                        }

                        if (isPID)
                        {
                            query = FilterByCustomer(query, $"8{request.ConvertedSearch}", request.CustomerId);
                        }
                        else
                        {
                            query = FilterByCustomer(query, request.ConvertedSearch, request.CustomerId);
                        }
                    }
                    else
                    {
                        query = FilterByCustomer(query, request.ConvertedSearch, request.CustomerId);
                    }
                }

                if (request.EmergencyStatus != null)
                    query = FilterByEmergencyStatus(query, request.ConvertedEmergencyStatus);

                if (request.User != null)
                    query = FilterByUser(query, request.ConvertedUser);

                if (request.StartAdmittedDate != null && request.EndAdmittedDate != null)
                    query = FilterByAdmittedDate(query, request.ConvertedStartAdmittedDate, request.ConvertedEndAdmittedDate);
                else if (request.StartAdmittedDate != null)
                    query = FilterByStartAdmittedDate(query, request.ConvertedStartAdmittedDate);
                else if (request.EndAdmittedDate != null)
                    query = FilterByEndAdmittedDate(query, request.ConvertedEndAdmittedDate);

                if (request.IsTelehealth != null)
                    query = FilterByTelehealth(query, (bool)request.IsTelehealth);

                if (request.IsRetailService != null)
                    query = FilterByRetailService(query, (bool)request.IsRetailService);

                if (request.ClinicId != null)
                    query = FilterByClinicId(query, Guid.Parse(request.ClinicId));

                var current_user = getUsername();
                if (!IsVIPMANAGE())
                    query = query.Where(e => e.UnlockFor != null && (e.UnlockFor == "ALL" || ("," + e.UnlockFor + ",").Contains("," + current_user + ",")));

                query = query.OrderByDescending(m => m.AdmittedDateDate).ThenBy(m => m.EDStatusCreatedAt).ThenByDescending(m => m.AdmittedDate);
                int count = query.Count();
                var done_query = query.Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToList();

                var listNure = done_query.Where(e => e.PrimaryNurseId != null).Select(e => e.PrimaryNurseId).ToList();
                var listDoc = done_query.Where(e => e.PrimaryDoctorId != null).Select(e => e.PrimaryDoctorId).ToList();
                var listAutho = done_query.Where(e => e.AuthorizedDoctorId != null).Select(e => e.AuthorizedDoctorId).ToList();
                var listUserId = listNure.Concat(listDoc).Concat(listAutho);

                var listUser = unitOfWork.UserRepository.Find(e => listUserId.Contains(e.Id)).ToList();
                var items = done_query.Select(e => DataFormatted(e, listUser));
                return new { count, results = items };
            }
        }

        private IQueryable<OPDQueryModel> FilterCustomerForNurse(Guid? site_id, Guid? specialty_id)
        {
            // var users = unitOfWork.UserRepository.AsQueryable();
            return (from opd_sql in unitOfWork.OPDRepository.AsQueryable().Where(
                       e => !e.IsDeleted &&
                       e.SpecialtyId == specialty_id
                  )
                    join cus_sql in unitOfWork.CustomerRepository.AsQueryable()
                         on opd_sql.CustomerId equals cus_sql.Id
                    join stt_sql in unitOfWork.EDStatusRepository.AsQueryable()
                        on opd_sql.EDStatusId equals stt_sql.Id
                    select new OPDQueryModel
                    {
                        Id = opd_sql.Id,
                        VisitCode = opd_sql.VisitCode,
                        IsTelehealth = opd_sql.IsTelehealth,
                        IsRetailService = opd_sql.IsRetailService,
                        IsBooked = opd_sql.IsBooked,
                        AdmittedDate = opd_sql.AdmittedDate,
                        AdmittedDateDate = SqlFunctions.DateName("year", opd_sql.AdmittedDate) +
                            SqlFunctions.StringConvert((double)SqlFunctions.DatePart("month", opd_sql.AdmittedDate), 2) +
                            SqlFunctions.StringConvert((double)SqlFunctions.DatePart("day", opd_sql.AdmittedDate), 2),
                        BookingTime = opd_sql.BookingTime,
                        CustomerPID = cus_sql.PID,
                        CustomerId = cus_sql.Id,
                        CustomerPhone = cus_sql.Phone,
                        CustomerFullname = cus_sql.Fullname,
                        CustomerDateOfBirth = cus_sql.DateOfBirth,
                        CustomerIsAllergy = (bool)opd_sql.IsAllergy,
                        CustomerAllergy = opd_sql.Allergy,
                        CustomerKindOfAllergy = opd_sql.KindOfAllergy,
                        EDStatusId = stt_sql.Id,
                        EDStatusCreatedAt = stt_sql.CreatedAt,
                        EDStatusEnName = stt_sql.EnName,
                        EDStatusViName = stt_sql.ViName,
                        EDStatusCode = stt_sql.Code,
                        PrimaryDoctorId = opd_sql.PrimaryDoctorId,
                        AuthorizedDoctorId = opd_sql.AuthorizedDoctorId,
                        PrimaryNurseId = opd_sql.PrimaryNurseId,
                        OPDFallRiskScreeningId = opd_sql.OPDFallRiskScreeningId,
                        OPDInitialAssessmentForShortTermId = opd_sql.OPDInitialAssessmentForShortTermId,
                        OPDInitialAssessmentForTelehealthId = opd_sql.OPDInitialAssessmentForTelehealthId,
                        UnlockFor = opd_sql.UnlockFor,
                        IsVip = cus_sql.IsVip,
                        IsAllergy = opd_sql.IsAllergy,
                        Allergy = opd_sql.Allergy,
                        KindOfAllergy = opd_sql.KindOfAllergy,
                        SpecialtyId = (Guid)opd_sql.SpecialtyId,
                        IsAnesthesia = opd_sql.IsAnesthesia, // visit đc tiếp vào khoa set là gây mê
                        ClinicId = opd_sql.ClinicId,
                        ClinicViName = opd_sql.Clinic.ViName,
                        IsHasFallRiskScreening = opd_sql.IsHasFallRiskScreening,
                        IsConsultation = opd_sql.OPDOutpatientExaminationNote != null ? opd_sql.OPDOutpatientExaminationNote.IsConsultation : null, // phiếu tư vấn hay phiếu ngoại trú
                        Version = opd_sql.Version,
                        UpdateByInitialAssessmentForShortTerm = opd_sql.OPDInitialAssessmentForShortTerm.UpdatedBy,
                        UpdateAtInitialAssessmentForShortTerm = opd_sql.OPDInitialAssessmentForShortTerm.UpdatedAt,
                        UpdateByInitialAssessmentForOnGoing = opd_sql.OPDInitialAssessmentForOnGoing.UpdatedBy,
                        UpdateAtInitialAssessmentForOnGoing = opd_sql.OPDInitialAssessmentForOnGoing.UpdatedAt,
                        UpdateByInitialAssessmentForTelehealth = opd_sql.OPDInitialAssessmentForTelehealth.UpdatedBy,
                        UpdateAtInitialAssessmentForTelehealth = opd_sql.OPDInitialAssessmentForTelehealth.UpdatedAt,
                        UpdateByInitialAssessmentForRetailServicePatient = opd_sql.EIOAssessmentForRetailServicePatient.UpdatedBy,
                        UpdateAtnitialAssessmentForRetailServicePatient = opd_sql.EIOAssessmentForRetailServicePatient.DeletedAt,
                        UserReceiver = opd_sql.CreatedBy
                    });
        }
        private IQueryable<OPDQueryModel> FilterCustomerForDoctor(Guid? site_id, Guid? specialty_id, Guid? user_id)
        {
            return (from opd_sql in unitOfWork.OPDRepository.AsQueryable().Where(
                        e => !e.IsDeleted &&
                        e.SpecialtyId == specialty_id &&
                        (e.PrimaryDoctorId == user_id || e.AuthorizedDoctorId == user_id)
                   )
                    join cus_sql in unitOfWork.CustomerRepository.AsQueryable()
                         on opd_sql.CustomerId equals cus_sql.Id
                    join stt_sql in unitOfWork.EDStatusRepository.AsQueryable()
                        on opd_sql.EDStatusId equals stt_sql.Id
                    select new OPDQueryModel
                    {
                        Id = opd_sql.Id,
                        VisitCode = opd_sql.VisitCode,
                        IsTelehealth = opd_sql.IsTelehealth,
                        IsRetailService = opd_sql.IsRetailService,
                        IsBooked = opd_sql.IsBooked,
                        AdmittedDate = opd_sql.AdmittedDate,
                        AdmittedDateDate = SqlFunctions.DateName("year", opd_sql.AdmittedDate) +
                            SqlFunctions.StringConvert((double)SqlFunctions.DatePart("month", opd_sql.AdmittedDate), 2) +
                            SqlFunctions.StringConvert((double)SqlFunctions.DatePart("day", opd_sql.AdmittedDate), 2),
                        BookingTime = opd_sql.BookingTime,
                        CustomerPID = cus_sql.PID,
                        CustomerId = cus_sql.Id,
                        CustomerPhone = cus_sql.Phone,
                        CustomerFullname = cus_sql.Fullname,
                        CustomerDateOfBirth = cus_sql.DateOfBirth,
                        CustomerIsAllergy = (bool)opd_sql.IsAllergy,
                        CustomerAllergy = opd_sql.Allergy,
                        CustomerKindOfAllergy = opd_sql.KindOfAllergy,
                        EDStatusId = stt_sql.Id,
                        EDStatusCreatedAt = stt_sql.CreatedAt,
                        EDStatusEnName = stt_sql.EnName,
                        EDStatusViName = stt_sql.ViName,
                        EDStatusCode = stt_sql.Code,
                        PrimaryDoctorId = opd_sql.PrimaryDoctorId,
                        AuthorizedDoctorId = opd_sql.AuthorizedDoctorId,
                        PrimaryNurseId = opd_sql.PrimaryNurseId,
                        //OPDFallRiskScreeningId = opd_sql.OPDFallRiskScreeningId,
                        OPDInitialAssessmentForShortTermId = opd_sql.OPDInitialAssessmentForShortTermId,
                        OPDInitialAssessmentForTelehealthId = opd_sql.OPDInitialAssessmentForTelehealthId,
                        IsHasFallRiskScreening = opd_sql.IsHasFallRiskScreening,
                        UnlockFor = opd_sql.UnlockFor,
                        IsVip = cus_sql.IsVip,
                        IsAllergy = opd_sql.IsAllergy,
                        Allergy = opd_sql.Allergy,
                        KindOfAllergy = opd_sql.KindOfAllergy,
                        SpecialtyId = (Guid)opd_sql.SpecialtyId,
                        IsAnesthesia = opd_sql.IsAnesthesia, // visit đc tiếp vào khoa set là gây mê
                        ClinicId = opd_sql.ClinicId,
                        ClinicViName = opd_sql.Clinic.ViName,
                        IsConsultation = opd_sql.OPDOutpatientExaminationNote != null ? opd_sql.OPDOutpatientExaminationNote.IsConsultation : null, // phiếu tư vấn hay phiếu ngoại trú
                        Version = opd_sql.Version,
                        UpdateByInitialAssessmentForShortTerm = opd_sql.OPDInitialAssessmentForShortTerm.UpdatedBy,
                        UpdateAtInitialAssessmentForShortTerm = opd_sql.OPDInitialAssessmentForShortTerm.UpdatedAt,
                        UpdateByInitialAssessmentForOnGoing = opd_sql.OPDInitialAssessmentForOnGoing.UpdatedBy,
                        UpdateAtInitialAssessmentForOnGoing = opd_sql.OPDInitialAssessmentForOnGoing.UpdatedAt,
                        UpdateByInitialAssessmentForTelehealth = opd_sql.OPDInitialAssessmentForTelehealth.UpdatedBy,
                        UpdateAtInitialAssessmentForTelehealth = opd_sql.OPDInitialAssessmentForTelehealth.UpdatedAt,
                        UpdateByInitialAssessmentForRetailServicePatient = opd_sql.EIOAssessmentForRetailServicePatient.UpdatedBy,
                        UpdateAtnitialAssessmentForRetailServicePatient = opd_sql.EIOAssessmentForRetailServicePatient.DeletedAt,
                        UserReceiver = opd_sql.CreatedBy
                    });
        }
        private bool IsFallRiskScreening(OPDQueryModel visit)
        {
            if (visit.IsHasFallRiskScreening != null)
            {
                return Convert.ToBoolean(visit.IsHasFallRiskScreening.ToString());
            }
            //var fallRiskScreening = unitOfWork.OPDFallRiskScreeningRepository.Find(x => !x.IsDeleted && x.VisitId == visit.Id).ToList().OrderByDescending(e => e.CreatedAt).FirstOrDefault();
            //if (fallRiskScreening != null)
            //{
            //    if (fallRiskScreening.Version == 1)
            //    {
            //        return unitOfWork.OPDFallRiskScreeningDataRepository
            //            .Count(
            //            e => e.OPDFallRiskScreeningId == fallRiskScreening.Id && !e.IsDeleted &&
            //            e.OPDFallRiskScreeningId != null &&
            //            new List<string> { "OPDFRSFOPDPHANS1", "OPDFRSFOPDPHANS2", "OPDFRSFOPDPHANS3", "OPDFRSFOPDPNANS1", "OPDFRSFOPDPNANS2", "OPDFRSFOPDPNANS3" }.Contains(e.Code) &&
            //            e.Value == "1") > 0;
            //    }
            //    return unitOfWork.OPDFallRiskScreeningDataRepository.Count(e => e.Value == "1" && e.OPDFallRiskScreeningId != null && e.OPDFallRiskScreeningId == fallRiskScreening.Id && e.Code == "OPDFRSFOPUTHANS1") > 0;
            //}
            return false;
        }

        private IQueryable<OPDQueryModel> FilterByCustomer(IQueryable<OPDQueryModel> query, string search, Guid? customerId)
        {
            if (customerId != null) return query.Where(e => e.CustomerId == customerId);
            return query.Where(e =>
                (e.CustomerPID != null && e.CustomerPID == search)
                || (e.CustomerFullname != null && e.CustomerFullname.ToLower().Contains(search))
                || (e.CustomerPhone != null && e.CustomerPhone.Contains(search))
            );
        }
        private IQueryable<OPDQueryModel> FilterByCustomerPID(IQueryable<OPDQueryModel> query, string search)
        {
            return query.Where(e => e.CustomerPID != null && e.CustomerPID == search);
        }
        private IQueryable<OPDQueryModel> FilterByEmergencyStatus(IQueryable<OPDQueryModel> query, List<Guid?> search)
        {
            return query.Where(e => search.Contains(e.EDStatusId));
        }

        private IQueryable<OPDQueryModel> FilterByStartAdmittedDate(IQueryable<OPDQueryModel> query, DateTime? search)
        {
            return query.Where(e => e.AdmittedDate != null && e.AdmittedDate >= search);
        }
        private IQueryable<OPDQueryModel> FilterByEndAdmittedDate(IQueryable<OPDQueryModel> query, DateTime? search)
        {
            return query.Where(e => e.AdmittedDate != null && e.AdmittedDate <= search);
        }
        private IQueryable<OPDQueryModel> FilterByAdmittedDate(IQueryable<OPDQueryModel> query, DateTime? start_date, DateTime? end_date)
        {
            return query.Where(e => e.AdmittedDate != null && e.AdmittedDate >= start_date && e.AdmittedDate <= end_date);
        }

        private IQueryable<OPDQueryModel> FilterByUser(IQueryable<OPDQueryModel> query, List<Guid?> search)
        {
            return query.Where(e => search.Contains(e.PrimaryDoctorId) || search.Contains(e.PrimaryNurseId) || search.Contains(e.AuthorizedDoctorId));
        }

        private IQueryable<OPDQueryModel> FilterByTelehealth(IQueryable<OPDQueryModel> query, bool search)
        {
            return query.Where(e => e.IsTelehealth == search);
        }

        private IQueryable<OPDQueryModel> FilterByRetailService(IQueryable<OPDQueryModel> query, bool search)
        {
            return query.Where(e => e.IsRetailService == search);
        }
        private IQueryable<OPDQueryModel> FilterByClinicId(IQueryable<OPDQueryModel> query, Guid clinicId)
        {
            return query.Where(e => e.ClinicId == clinicId);
        }

        private dynamic DataFormatted(OPDQueryModel visit, List<DataAccess.Models.User> listUser)
        {
            return new
            {
                visit.Id,
                visit.IsTelehealth,
                visit.IsRetailService,
                Customer = new
                {
                    PID = visit.CustomerPID,
                    Phone = visit.CustomerPhone,
                    Fullname = visit.CustomerFullname,
                    DateOfBirth = visit.CustomerDateOfBirth?.ToString(Constant.DATE_FORMAT),
                    IsAllergy = visit.CustomerIsAllergy,
                    Allergy = visit.CustomerAllergy,
                    KindOfAllergy = visit.CustomerKindOfAllergy,
                    visit.IsVip
                },
                visit.AdmittedDateDate,
                PrimaryDoctor = listUser.FirstOrDefault(e => e.Id == visit.PrimaryDoctorId)?.Username,
                AuthorizedDoctor = listUser.FirstOrDefault(e => e.Id == visit.AuthorizedDoctorId)?.Username,
                Nurse = listUser.FirstOrDefault(e => e.Id == visit.PrimaryNurseId)?.Username,
                visit.VisitCode,
                visit.IsBooked,
                BookingTime = visit.BookingTime?.ToString(Constant.TIME_FORMAT_WITHOUT_SECOND),
                AdmittedDate = visit.AdmittedDate.ToString(Constant.DATE_TIME_FORMAT_WITHOUT_SECOND),
                EmergencyStatus = new
                {
                    EnName = visit.EDStatusEnName,
                    ViName = visit.EDStatusViName,
                    Code = visit.EDStatusCode
                },
                //huy sua danh gia nga
                IsFallRiskScreening = IsFallRiskScreening(visit),
                //huy sua danh gia nga
                VisitAllergy = getAllergyModel(visit),
                // MedicalRecordType = GetSetUpMedicalRecords(visit.SpecialtyId, visit.Id, listMedicalRecordIsDeploy),
                PromissoryNote = GetPromissoryNote(visit.Id, visit.SpecialtyId, getPromissoryNoteBySetup),
                visit.IsAnesthesia, // visit đc tiếp vào khoa gây mê
                Clinic = new {
                    Id = visit?.ClinicId,
                    ViName = visit?.ClinicViName
                },
                visit.IsConsultation,
                visit.Version,
                visit.UserLastDoAtnitialAssessment,
                visit.UserReceiver
            };
        }
        private VisitAllergyModel getAllergyModel(OPDQueryModel visit)
        {
            if (visit.IsAllergy != null)
            {
                return new VisitAllergyModel()
                {
                    IsAllergy = visit.IsAllergy,
                    Allergy = visit.Allergy,
                    KindOfAllergy = visit.KindOfAllergy,
                };
            }
            // var allergy_dct = new Dictionary<string, string>();
            //var data = new List<FormDataValue>();
            //if (visit.OPDInitialAssessmentForTelehealthId != null)
            //{
            //    data = unitOfWork.OPDInitialAssessmentForTelehealthDataRepository.Find(
            //        e => !e.IsDeleted &&
            //        Constant.OPD_IAFTP_ALLERGIC_CODE.Contains(e.Code) &&
            //        e.OPDInitialAssessmentForTelehealthId == visit.OPDInitialAssessmentForTelehealthId
            //    ).Select(etrd => new FormDataValue() { Code = etrd.Code, Value = etrd.Value }).ToList();
            //}
            //else
            //{
            //    if (visit.OPDInitialAssessmentForShortTermId != null)
            //    {
            //        data = unitOfWork.OPDInitialAssessmentForShortTermDataRepository.Find(e => !e.IsDeleted && Constant.OPD_IAFST_ALLERGIC_CODE.Contains(e.Code) && e.OPDInitialAssessmentForShortTermId == visit.OPDInitialAssessmentForShortTermId)
            //            .Select(etrd => new FormDataValue() { Code = etrd.Code, Value = etrd.Value }).ToList();
            //    }
            //}
            //foreach (var item in data)
            //{
            //    allergy_dct[Constant.CUSTOMER_ALLERGY_SWITCH[item.Code]] = item.Value;
            //}
            return new VisitAllergyModel() { };
        }
    }
}
