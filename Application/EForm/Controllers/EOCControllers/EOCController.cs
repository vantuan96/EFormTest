using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Net;
using System.Web.Http;
using DataAccess.Models.EDModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers;
using EForm.Models;
using EForm.Models.EDModels;
using EForm.Models.EOCModel;
using EForm.Models.OPDModels;
using EMRModels;

namespace EForm.Controllers.EDControllers
{
    [SessionAuthorize]
    public class EOCController : BaseEDApiController
    {
        [HttpGet]
        [Route("api/eoc/eoc-tranfers")]
        [Permission(Code = "EOC005")]
        public IHttpActionResult GetCustomers([FromUri]HandOverCheckListParameterModel request)
        {
            var site_id = GetSiteId();
            var specialty_id = GetSpecialtyId();
            var query = (from eoc_sql in unitOfWork.EOCTransferRepository.AsQueryable().Where(
                            e => !e.IsDeleted && string.IsNullOrEmpty(e.DeletedBy) && string.IsNullOrEmpty(e.AcceptBy)
                         )
                         join cus_sql in unitOfWork.CustomerRepository.AsQueryable()
                            on eoc_sql.CustomerId equals cus_sql.Id into cEd
                         from cus_sql in cEd.DefaultIfEmpty()

                         join sp_sql in unitOfWork.SpecialtyRepository.AsQueryable()
                            on eoc_sql.SpecialtyId equals sp_sql.Id into cpOpd
                         from sp_sql in cpOpd.DefaultIfEmpty()

                         select new EOCTranfers
                         {
                             Id = eoc_sql.Id,
                             PID = cus_sql.PID,
                             Fullname = cus_sql.Fullname,
                             DateOfBirth = cus_sql.DateOfBirth,
                             SpecialtyName = sp_sql.ViName,
                             TransferBy = eoc_sql.TransferBy,
                             TransferAt = eoc_sql.UpdatedAt,
                             AcceptAt = eoc_sql.AcceptAt,
                             AcceptBy = eoc_sql.AcceptBy,
                             SpecialtyId = sp_sql.Id,
                             SiteId = eoc_sql.SiteId
                         }).Where(eoc => eoc.SiteId == site_id);
            if (request.Type == "count")
            {
                return Ok(new { count = query.Count() });
            }

            if (request.Search != null)
                query = FilterBySearch(query, request.ConvertedSearch);
            if (request.Status != null)
                if (request.Status == 0)
                    query = query.Where(e => e.AcceptBy == null);
                else if (request.Status == 1)
                    query = query.Where(e => e.AcceptBy != null);

            if (request.StartDate != null && request.EndDate != null)
                query = FilterByDate(query, request.ConvertedStartDate, request.ConvertedEndDate);
            else if (request.StartDate != null)
                query = FilterByStartDate(query, request.ConvertedStartDate);
            else if (request.EndDate != null)
                query = FilterByEndDate(query, request.ConvertedEndDate);

            query = query.OrderByDescending(m => m.TransferAt);

            int count = query.Count();
            
            var items = query.Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize).ToList()
                .Select(tf => new
                {
                    tf.Id,
                    tf.PID,
                    tf.Fullname,
                    DateOfBirth = tf.DateOfBirth?.ToString(Constant.DATE_FORMAT),
                    tf.SpecialtyName,
                    tf.TransferBy,
                    TransferAt = tf.TransferAt?.ToString(Constant.DATE_TIME_FORMAT_WITHOUT_SECOND),
                    tf.SiteId,
                    tf.SpecialtyId,
                    AcceptAt = tf.AcceptAt?.ToString(Constant.DATE_TIME_FORMAT_WITHOUT_SECOND),
                    tf.AcceptBy
                });
            return Ok(new { count, results = items });
        }

        [HttpGet]
        [Route("api/eoc")]
        [Permission(Code = "EOC006")]
        public IHttpActionResult GetEOC([FromUri]OPDParameterModel request)
        {
            
            if (IsDoctor())
                return Content(HttpStatusCode.OK, GetOPDForDoctor(request));
            return Content(HttpStatusCode.OK, GetOPDForNurse(request));
        }
        private dynamic GetOPDForNurse(OPDParameterModel request)
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

            query = query.OrderByDescending(m => m.AdmittedDateDate).ThenBy(m => m.EDStatusCreatedAt).ThenByDescending(m => m.AdmittedDate);

            var current_user = getUsername();
            if (!IsVIPMANAGE())
                query = query.Where(e => e.UnlockFor != null && (e.UnlockFor == "ALL" || ("," + e.UnlockFor + ",").Contains("," + current_user + ",")));

            int count = query.Count();
            var items = query.Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList()
                .Select(e => DataFormatted(e));
            return new { count, results = items };
        }
        private dynamic GetOPDForDoctor(OPDParameterModel request)
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
                        query = FilterByCustomer(query, $"{request.ConvertedSearch}");
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

            query = query.OrderByDescending(m => m.AdmittedDateDate).ThenBy(m => m.EDStatusCreatedAt).ThenByDescending(m => m.AdmittedDate);
            int count = query.Count();
            var current_user = getUsername();
            if (!IsVIPMANAGE())
                query = query.Where(e => e.UnlockFor != null && (e.UnlockFor == "ALL" || ("," + e.UnlockFor + ",").Contains("," + current_user + ",")));

            var items = query.Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList()
                .Select(e => DataFormatted(e));
            return new { count, results = items };
        }
        private IQueryable<OPDQueryModel> FilterCustomerForNurse(Guid? site_id, Guid? specialty_id)
        {
            var users = unitOfWork.UserRepository.AsQueryable();
            var a = (from opd_sql in unitOfWork.EOCRepository.AsQueryable().Where(
                        e => !e.IsDeleted && string.IsNullOrEmpty(e.DeletedBy) && e.CustomerId != null && e.StatusId != null &&
                        e.SiteId != null && e.SiteId == site_id &&
                        e.SpecialtyId != null && e.SpecialtyId == specialty_id
                   )
                     join cus_sql in unitOfWork.CustomerRepository.AsQueryable()
                          on opd_sql.CustomerId equals cus_sql.Id
                     join stt_sql in unitOfWork.EDStatusRepository.AsQueryable()
                         on opd_sql.StatusId equals stt_sql.Id
                     join doc_sql in users
                         on opd_sql.PrimaryDoctorId equals doc_sql.Id into dlist
                     from doc_sql in dlist.DefaultIfEmpty()
                     join aut_sql in users
                         on opd_sql.AuthorizedDoctorId equals aut_sql.Id into alist
                     from aut_sql in alist.DefaultIfEmpty()
                     join nur_sql in users
                         on opd_sql.PrimaryNurseId equals nur_sql.Id into nlist
                     from nur_sql in nlist.DefaultIfEmpty()
                     select new OPDQueryModel
                     {
                         Id = opd_sql.Id,
                         VisitCode = opd_sql.VisitCode,
                         AdmittedDate = opd_sql.AdmittedDate,
                         AdmittedDateDate = SqlFunctions.DateName("year", opd_sql.AdmittedDate) +
                             SqlFunctions.StringConvert((double)SqlFunctions.DatePart("month", opd_sql.AdmittedDate), 2) +
                             SqlFunctions.StringConvert((double)SqlFunctions.DatePart("day", opd_sql.AdmittedDate), 2),
                         CustomerPID = cus_sql.PID,
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
                         PrimaryDoctorUsername = doc_sql.Username,

                         AuthorizedDoctorId = opd_sql.AuthorizedDoctorId,
                         AuthorizedDoctorUsername = aut_sql.Username,

                         PrimaryNurseId = opd_sql.PrimaryNurseId,
                         PrimaryNurseUsername = nur_sql.Username,
                         UnlockFor = opd_sql.UnlockFor,
                         IsVip = cus_sql.IsVip,
                         IsHasFallRiskScreening = opd_sql.IsHasFallRiskScreening
                     }).ToList();
            return (from opd_sql in unitOfWork.EOCRepository.AsQueryable().Where(
                        e => !e.IsDeleted && string.IsNullOrEmpty(e.DeletedBy) && e.CustomerId != null && e.StatusId != null &&
                        e.SiteId != null && e.SiteId == site_id &&
                        e.SpecialtyId != null && e.SpecialtyId == specialty_id
                   )
                    join cus_sql in unitOfWork.CustomerRepository.AsQueryable()
                         on opd_sql.CustomerId equals cus_sql.Id
                    join stt_sql in unitOfWork.EDStatusRepository.AsQueryable()
                        on opd_sql.StatusId equals stt_sql.Id
                    join doc_sql in users
                        on opd_sql.PrimaryDoctorId equals doc_sql.Id into dlist
                    from doc_sql in dlist.DefaultIfEmpty()
                    join aut_sql in users
                        on opd_sql.AuthorizedDoctorId equals aut_sql.Id into alist
                    from aut_sql in alist.DefaultIfEmpty()
                    join nur_sql in users
                        on opd_sql.PrimaryNurseId equals nur_sql.Id into nlist
                    from nur_sql in nlist.DefaultIfEmpty()
                    select new OPDQueryModel
                    {
                        Id = opd_sql.Id,
                        VisitCode = opd_sql.VisitCode,
                        AdmittedDate = opd_sql.AdmittedDate,
                        AdmittedDateDate = SqlFunctions.DateName("year", opd_sql.AdmittedDate) +
                            SqlFunctions.StringConvert((double)SqlFunctions.DatePart("month", opd_sql.AdmittedDate), 2) +
                            SqlFunctions.StringConvert((double)SqlFunctions.DatePart("day", opd_sql.AdmittedDate), 2),
                        CustomerPID = cus_sql.PID,
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
                        PrimaryDoctorUsername = doc_sql.Username,

                        AuthorizedDoctorId = opd_sql.AuthorizedDoctorId,
                        AuthorizedDoctorUsername = aut_sql.Username,

                        PrimaryNurseId = opd_sql.PrimaryNurseId,
                        PrimaryNurseUsername = nur_sql.Username,
                        UnlockFor = opd_sql.UnlockFor,
                        IsVip = cus_sql.IsVip,
                        IsHasFallRiskScreening = opd_sql.IsHasFallRiskScreening,
                        Version = opd_sql.Version
                    });
        }
        private IQueryable<OPDQueryModel> FilterCustomerForDoctor(Guid? site_id, Guid? specialty_id, Guid? user_id)
        {
            var users = unitOfWork.UserRepository.AsQueryable();

            return (from opd_sql in unitOfWork.EOCRepository.AsQueryable().Where(
                        e => !e.IsDeleted && string.IsNullOrEmpty(e.DeletedBy) && e.CustomerId != null && e.StatusId != null &&
                        e.SiteId != null && e.SiteId == site_id &&
                        e.SpecialtyId != null && e.SpecialtyId == specialty_id &&
                        ((e.PrimaryDoctorId != null && e.PrimaryDoctorId == user_id) ||
                        (e.AuthorizedDoctorId != null && e.AuthorizedDoctorId == user_id))
                   )
                    join cus_sql in unitOfWork.CustomerRepository.AsQueryable()
                         on opd_sql.CustomerId equals cus_sql.Id
                    join stt_sql in unitOfWork.EDStatusRepository.AsQueryable()
                        on opd_sql.StatusId equals stt_sql.Id
                    join doc_sql in users
                        on opd_sql.PrimaryDoctorId equals doc_sql.Id into dlist
                    from doc_sql in dlist.DefaultIfEmpty()
                    join aut_sql in users
                        on opd_sql.AuthorizedDoctorId equals aut_sql.Id into alist
                    from aut_sql in alist.DefaultIfEmpty()
                    join nur_sql in users
                        on opd_sql.PrimaryNurseId equals nur_sql.Id into nlist
                    from nur_sql in nlist.DefaultIfEmpty()
                    select new OPDQueryModel
                    {
                        Id = opd_sql.Id,
                        VisitCode = opd_sql.VisitCode,
                        AdmittedDate = opd_sql.AdmittedDate,
                        AdmittedDateDate = SqlFunctions.DateName("year", opd_sql.AdmittedDate) +
                            SqlFunctions.StringConvert((double)SqlFunctions.DatePart("month", opd_sql.AdmittedDate), 2) +
                            SqlFunctions.StringConvert((double)SqlFunctions.DatePart("day", opd_sql.AdmittedDate), 2),
                        CustomerPID = cus_sql.PID,
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
                        PrimaryDoctorUsername = doc_sql.Username,
                        AuthorizedDoctorId = opd_sql.AuthorizedDoctorId,
                        AuthorizedDoctorUsername = aut_sql.Username,
                        PrimaryNurseId = opd_sql.PrimaryNurseId,
                        PrimaryNurseUsername = nur_sql.Username,
                        UnlockFor = opd_sql.UnlockFor,
                        IsVip = cus_sql.IsVip,
                        IsHasFallRiskScreening = opd_sql.IsHasFallRiskScreening,
                        Version = opd_sql.Version
                    });
        }
        protected IQueryable<EOCTranfers> FilterBySearch(IQueryable<EOCTranfers> query, string search)
        {
            return query.Where(
                e => (e.PID != null && e.PID == search) ||
                (e.Fullname != null && e.Fullname.ToLower().Contains(search))
            );
        }
        private IQueryable<EOCTranfers> FilterByEndDate(IQueryable<EOCTranfers> query, DateTime? convertedEndDate)
        {
            return query.Where(
                e => e.TransferAt != null &&
                e.TransferAt <= convertedEndDate
            );
        }
        private IQueryable<EOCTranfers> FilterByStartDate(IQueryable<EOCTranfers> query, DateTime? convertedStartDate)
        {
            return query.Where(
                 e => e.TransferAt != null &&
                 e.TransferAt >= convertedStartDate
             );
        }
        private IQueryable<EOCTranfers> FilterByDate(IQueryable<EOCTranfers> query, DateTime? convertedStartDate, DateTime? convertedEndDate)
        {
            return query.Where(
                e => e.TransferAt != null &&
                e.TransferAt >= convertedStartDate &&
                e.TransferAt <= convertedEndDate
            );
        }
        private IQueryable<OPDQueryModel> FilterByCustomer(IQueryable<OPDQueryModel> query, string search)
        {
            return query.Where(e =>
                (e.CustomerPID != null && e.CustomerPID == search)
                || (e.CustomerFullname != null && e.CustomerFullname.ToLower().Contains(search))
                || (e.CustomerPhone != null && e.CustomerPhone.Contains(search))
            );
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
        private bool IsFallRiskScreening(OPDQueryModel visit)
        {
            if (visit.IsHasFallRiskScreening != null)
            {
                return Convert.ToBoolean(visit.IsHasFallRiskScreening.ToString());
            }
            return false;
            //bool isFallRiskScreening = false;   
            //var lastFallRick = unitOfWork.EOCFallRiskScreeningRepository.Find(e => e.VisitId == visitId && !e.IsDeleted).OrderByDescending(x=>x.UpdatedAt).FirstOrDefault();
            //if(lastFallRick != null)
            //{
            //  var value =  unitOfWork.FormDatasRepository.FirstOrDefault(x => x.Code == "OPDFRSFOPUTHANS1" && x.VisitId == visitId && x.FormId == lastFallRick.Id && x.VisitType == "EOC")?.Value;
            //    if(!string.IsNullOrEmpty(value))
            //    {
            //        if(value == "1")
            //            isFallRiskScreening = true;
            //    }    
            //}
            //return isFallRiskScreening;
            //return unitOfWork.FormDatasRepository
            //    .Count(
            //    e => !e.IsDeleted && e.VisitId == visitId &&
            //    new List<string> { "OPDFRSFOPDPHANS1", "OPDFRSFOPDPHANS2", "OPDFRSFOPDPHANS3", "OPDFRSFOPDPNANS1", "OPDFRSFOPDPNANS2", "OPDFRSFOPDPNANS3" }.Contains(e.Code) &&
            //    e.Value == "1") > 0;
        }
        private dynamic DataFormatted(OPDQueryModel visit)
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
                PrimaryDoctor = visit.PrimaryDoctorUsername,
                visit.PrimaryDoctorId,
                AuthorizedDoctor = visit.AuthorizedDoctorUsername,
                Nurse = visit.PrimaryNurseUsername,
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
                IsFallRiskScreening = IsFallRiskScreening(visit),
                VisitAllergy = new VisitAllergyModel
                {
                    IsAllergy = visit.CustomerIsAllergy,
                    Allergy = visit.CustomerAllergy,
                    KindOfAllergy = visit.CustomerKindOfAllergy,
                },
                visit.Version
            };
        }
    }
}