using DataAccess.Models;
using DataAccess.Models.IPDModel;
using EForm.Authentication;
using EForm.Client;
using EForm.Common;
using EForm.Controllers.BaseControllers;
using EForm.Models.IPDModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Net;
using System.Web.Http;
using static EForm.Controllers.IPDControllers.IPDSetupMedicalRecordController;

namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class IPDController : BaseIPDApiController
    {
        [HttpGet]
        [Route("api/IPD/IPD")]
        [Permission(Code = "IIPD1")]
        public IHttpActionResult GetCustomers([FromUri]IPDParameterModel request)
        {
            if (!request.Validate())
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);

            //var user = GetUser();
            //var positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName);

            return Content(HttpStatusCode.OK, GetIPDForNurse(request));
        }
        [HttpGet]
        [Route("api/IPD/HISDoctor/{id}")]
        [Permission(Code = "IIPD2")]
        public IHttpActionResult HISDoctor(Guid id)
        {
            return Content(HttpStatusCode.OK, new { });
        }
        [HttpGet]
        [Route("api/IPD/HISDoctor/Sync/{id}")]
        [Permission(Code = "IIPD2")]
        public IHttpActionResult SyncHISDoctor(Guid id)
        {
            return Content(HttpStatusCode.OK, new { });
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/PrimaryDoctor/{id}")]
        [Permission(Code = "IIPD2")]
        public IHttpActionResult PrimaryDoctor(Guid id, [FromBody] JObject request)
        {
            return Content(HttpStatusCode.BadRequest, new { });
        }
        private dynamic GetIPDForNurse(IPDParameterModel request)
        {
            var site_id = GetSiteId();
            var specialty_id = GetSpecialtyId();
            var query = FilterCustomerForNurse(site_id, specialty_id);
            var specialty_setup_forms = GetSpecialtySetupForm(specialty_id);

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
                        query = FilterByCustomer(query, $"8{request.ConvertedSearch}");
                    }
                    else
                    {
                        query = FilterByCustomer(query, request.ConvertedSearch);
                    }
                }
                else
                {
                    query = FilterByCustomer(query, request.ConvertedSearch);
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

            if (request.IsDraft == true)
                query = query.Where(e => e.IsDraft);

            var current_user = getUsername();
            if (!IsVIPMANAGE())
                query = query.Where(e => e.UnlockFor != null && (e.UnlockFor == "ALL" || ("," + e.UnlockFor + ",").Contains("," + current_user + ",")));

            int count = query.Count();
            var timeStartUpdate = GetAppConfig("HIDE_A02_016_050919_VE");
            if (count > 0)
            {
                query = query.AsQueryable().OrderByDescending(m => m.AdmittedDateDate).ThenBy(m => m.EDStatusCreatedAt).ThenByDescending(m => m.AdmittedDate);
                var items = query.Skip((request.PageNumber - 1) * request.PageSize)
                               .Take(request.PageSize)
                               .ToList()
                               .Select(e => DataFormatted(e, timeStartUpdate, specialty_setup_forms));
                return new { count, results = items };
            }
            else
            {
                return new { count = 0, results = new Array[] { } };
            }
        }

        private IQueryable<IPDQueryModel> FilterCustomerForNurse(Guid? site_id, Guid? specialty_id)
        {
            return (from ipd_sql in unitOfWork.IPDRepository.AsQueryable().Where(
                        e => !e.IsDeleted && string.IsNullOrEmpty(e.DeletedBy) && e.CustomerId != null && e.EDStatusId != null &&
                        e.SiteId != null && e.SiteId == site_id &&
                        e.SpecialtyId != null && e.SpecialtyId == specialty_id
                   )
                    join cus_sql in unitOfWork.CustomerRepository.AsQueryable()
                         on ipd_sql.CustomerId equals cus_sql.Id
                    join stt_sql in unitOfWork.EDStatusRepository.AsQueryable()
                        on ipd_sql.EDStatusId equals stt_sql.Id
                    join doc_sql in unitOfWork.UserRepository.AsQueryable()
                        on ipd_sql.PrimaryDoctorId equals doc_sql.Id into dlist
                    from doc_sql in dlist.DefaultIfEmpty()
                    join nur_sql in unitOfWork.UserRepository.AsQueryable()
                        on ipd_sql.PrimaryNurseId equals nur_sql.Id into nlist
                    from nur_sql in nlist.DefaultIfEmpty()
                    select new IPDQueryModel
                    {
                        Id = ipd_sql.Id,
                        VisitCode = ipd_sql.VisitCode,
                        AdmittedDate = ipd_sql.AdmittedDate,
                        PermissionForVisitor = ipd_sql.PermissionForVisitor,
                        AdmittedDateDate = SqlFunctions.DateName("year", ipd_sql.AdmittedDate) +
                                SqlFunctions.StringConvert((double)SqlFunctions.DatePart("month", ipd_sql.AdmittedDate), 2) +
                                SqlFunctions.StringConvert((double)SqlFunctions.DatePart("day", ipd_sql.AdmittedDate), 2),
                        CustomerPID = cus_sql.PID,
                        CustomerPhone = cus_sql.Phone,
                        CustomerFullname = cus_sql.Fullname,
                        CustomerDateOfBirth = cus_sql.DateOfBirth,
                        CustomerIsAllergy = (bool)ipd_sql.IsAllergy,
                        CustomerAllergy = ipd_sql.Allergy,
                        CustomerKindOfAllergy = ipd_sql.KindOfAllergy,
                        EDStatusId = stt_sql.Id,
                        EDStatusCreatedAt = stt_sql.CreatedAt,
                        EDStatusEnName = stt_sql.EnName,
                        EDStatusViName = stt_sql.ViName,
                        PrimaryDoctorId = ipd_sql.PrimaryDoctorId,
                        PrimaryDoctorUsername = doc_sql.Username,
                        PrimaryNurseId = ipd_sql.PrimaryNurseId,
                        PrimaryNurseUsername = nur_sql.Username,
                        UnlockFor = ipd_sql.UnlockFor,
                        IsVip = cus_sql.IsVip,
                        IsDraft = ipd_sql.IsDraft,
                        CreatedAt = ipd_sql.CreatedAt,
                        Version = ipd_sql.Version,
                        UserDoNormalInpatiens = ipd_sql.IPDInitialAssessmentForAdult.CreatedBy,
                        UserReceiver = ipd_sql.CreatedBy,
                        MedicalRecordId = ipd_sql.IPDMedicalRecordId
                    });
        }

        private IQueryable<IPDQueryModel> FilterByCustomer(IQueryable<IPDQueryModel> query, string search)
        {
            return query.Where(e =>
                (e.CustomerPID != null && e.CustomerPID == search)
                || (e.CustomerFullname != null && e.CustomerFullname.ToLower().Contains(search))
                || (e.CustomerPhone != null && e.CustomerPhone.Contains(search))
            );
        }

        private IQueryable<IPDQueryModel> FilterByEmergencyStatus(IQueryable<IPDQueryModel> query, List<Guid?> search)
        {
            return query.Where(e => search.Contains(e.EDStatusId));
        }

        private IQueryable<IPDQueryModel> FilterByStartAdmittedDate(IQueryable<IPDQueryModel> query, DateTime? search)
        {
            return query.Where(e => e.AdmittedDate != null && e.AdmittedDate >= search);
        }
        private IQueryable<IPDQueryModel> FilterByEndAdmittedDate(IQueryable<IPDQueryModel> query, DateTime? search)
        {
            return query.Where(e => e.AdmittedDate != null && e.AdmittedDate <= search);
        }
        private IQueryable<IPDQueryModel> FilterByAdmittedDate(IQueryable<IPDQueryModel> query, DateTime? start_date, DateTime? end_date)
        {
            return query.Where(e => e.AdmittedDate != null && e.AdmittedDate >= start_date && e.AdmittedDate <= end_date);
        }

        private IQueryable<IPDQueryModel> FilterByUser(IQueryable<IPDQueryModel> query, List<Guid?> search)
        {
            return query.Where(e => search.Contains(e.PrimaryDoctorId) || search.Contains(e.PrimaryNurseId));
        }

        private dynamic DataFormatted(IPDQueryModel visit, string timeStartUpdate, List<MasterData> list_specialty_setup)
        {
            return new
            {
                visit.Id,
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
                PrimaryDoctor = visit.PrimaryDoctorUsername,
                Nurse = visit.PrimaryNurseUsername,
                visit.PermissionForVisitor,
                visit.VisitCode,
                AdmittedDate = visit.AdmittedDate.ToString(Constant.DATE_TIME_FORMAT_WITHOUT_SECOND),
                EmergencyStatus = new { EnName = visit.EDStatusEnName, ViName = visit.EDStatusViName },
                MedicalRecordType = GetMedicalRecordsSavedOrSetup(visit.Id, list_specialty_setup),
                VisitAllergy = new EMRModels.VisitAllergyModel()
                {
                    IsAllergy = visit.CustomerIsAllergy,
                    Allergy = visit.CustomerAllergy,
                    KindOfAllergy = visit.CustomerKindOfAllergy
                },
                visit.IsDraft,
                HideFormNewborn = IsForNeonatalMaternityV2((DateTime)visit.CreatedAt, timeStartUpdate),
                visit.Version,
                visit.UserDoNormalInpatiens,
                visit.UserReceiver
            };
        }
    }
}
