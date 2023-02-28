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
using EForm.Models.EDModels;
using EMRModels;

namespace EForm.Controllers.EDControllers
{
    [SessionAuthorize]
    public class EDController : BaseEDApiController
    {
        [HttpGet]
        [Route("api/ED/ED")]
        [Permission(Code = "EEMDE1")]
        public IHttpActionResult GetCustomers([FromUri]EDParameterModel customer)
        {
            if (!customer.Validate())
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);

            var site_id = GetSiteId();
            var current_user = getUsername();
            var specialty_id = GetSpecialtyId();
            var query = (from ed_sql in unitOfWork.EDRepository.AsQueryable().Where(
                            e => !e.IsDeleted && string.IsNullOrEmpty(e.DeletedBy) && e.CustomerId != null && e.EDStatusId != null &&
                            e.SiteId != null && e.SiteId == site_id &&
                            e.SpecialtyId != null && e.SpecialtyId == specialty_id
                         )
                         join cus_sql in unitOfWork.CustomerRepository.AsQueryable()
                            on ed_sql.CustomerId equals cus_sql.Id
                         join stt_sql in unitOfWork.EDStatusRepository.AsQueryable()
                            on ed_sql.EDStatusId equals stt_sql.Id
                         join ats_sql in unitOfWork.MasterDataRepository.AsQueryable().Where(e => e.Group == "ETRATS")
                            on ed_sql.ATSScale equals ats_sql.Code into alist from ats_sql in alist.DefaultIfEmpty()
                         select new EDQueryModel
                         {
                             Id = ed_sql.Id,
                             VisitCode = ed_sql.VisitCode,
                             ATSScale = ed_sql.ATSScale,
                             AdmittedDate = ed_sql.AdmittedDate,
                             AdmittedDateDate = SqlFunctions.DateName("year", ed_sql.AdmittedDate) +
                                SqlFunctions.StringConvert((double)SqlFunctions.DatePart("month", ed_sql.AdmittedDate), 2) +
                                SqlFunctions.StringConvert((double)SqlFunctions.DatePart("day", ed_sql.AdmittedDate), 2),
                             IsRetailService = ed_sql.IsRetailService,
                             CustomerPID = cus_sql.PID,
                             CustomerPhone = cus_sql.Phone,
                             CustomerFullname = cus_sql.Fullname,
                             CustomerDateOfBirth = cus_sql.DateOfBirth,
                             CustomerIsAllergy = (bool)ed_sql.IsAllergy,
                             CustomerAllergy = ed_sql.Allergy,
                             CustomerKindOfAllergy = ed_sql.KindOfAllergy,
                             EDStatusId = stt_sql.Id,
                             EDStatusCreatedAt = stt_sql.CreatedAt,
                             EDStatusEnName = stt_sql.EnName,
                             EDStatusViName = stt_sql.ViName,
                             ATSScaleEnName = ats_sql.EnName,
                             ATSScaleViName = ats_sql.ViName,
                             ATSScaleCode = ats_sql.Code,
                             ATSScaleNote = ats_sql.Note,
                             EmergencyTriageRecordId = ed_sql.EmergencyTriageRecordId,
                             IsHasFallRiskScreening = ed_sql.IsHasFallRiskScreening,
                             CovidRiskGroup = ed_sql.CovidRiskGroup,
                             PrimaryDoctor = ed_sql.PrimaryDoctor,
                             SelfHarmRiskScreeningResults = ed_sql.SelfHarmRiskScreeningResults,
                             IsVip = cus_sql.IsVip,
                             UnlockFor = ed_sql.UnlockFor,
                             CreatedBy = ed_sql.CreatedBy,
                             Version = ed_sql.Version,
                             UserDoTriage = ed_sql.EmergencyTriageRecord.UpdatedBy,
                             UserReceiver = ed_sql.CreatedBy
                         });

            if (customer.Search != null)
            {
                query = query.Where(e =>
                   ((e.CustomerPID != null && e.CustomerPID.Contains(customer.ConvertedSearch))
                   || (e.CustomerFullname != null && e.CustomerFullname.ToLower().Contains(customer.ConvertedSearch))
                   || (e.CustomerPhone != null && e.CustomerPhone.Contains(customer.ConvertedSearch))
                   )
                );
            }

            if (customer.EmergencyStatus != null)
                query = query.Where(e => customer.ConvertedEmergencyStatus.Contains(e.EDStatusId));
            
            if (customer.RickGroups != null)
                query = query.Where(e => customer.ConvertedRickGroups.Contains(e.CovidRiskGroup));
            
            if (customer.IsRetailService != null)
                query = query.Where(e => e.IsRetailService == customer.IsRetailService);

            if (customer.ATSScale != null)
            {
                var ats = customer.ConvertedATSScale;
                if (ats.Contains("-1"))
                    query = query.Where(e => e.ATSScale == null || (e.ATSScale != null && ats.Contains(e.ATSScale)));
                else
                    query = query.Where(e => e.ATSScale != null && ats.Contains(e.ATSScale));
            }

            if (customer.StartAdmittedDate != null && customer.EndAdmittedDate != null)
                query = query.Where(e => e.AdmittedDate != null && e.AdmittedDate >= customer.ConvertedStartAdmittedDate && e.AdmittedDate <= customer.ConvertedEndAdmittedDate);
            else if (customer.StartAdmittedDate != null)
                query = query.Where(e => e.AdmittedDate != null && e.AdmittedDate >= customer.ConvertedStartAdmittedDate);
            else if (customer.EndAdmittedDate != null)
                query = query.Where(e => e.AdmittedDate != null && e.AdmittedDate <= customer.ConvertedEndAdmittedDate);

            if (!IsVIPMANAGE())
                query = query.Where(e => e.UnlockFor != null && (e.UnlockFor == "ALL" || ("," + e.UnlockFor + ",").Contains("," + current_user + ",")));
            int count = query.Count();
            
            query = query.OrderByDescending(m => m.AdmittedDateDate).ThenBy(m => m.ATSScale).ThenBy(m => m.EDStatusCreatedAt).ThenByDescending(m => m.AdmittedDate);
            
            var items = query.Skip((customer.PageNumber - 1) * customer.PageSize)
                .Take(customer.PageSize)
                .ToList()
                .Select(visit => new EDViewModel
                {
                    Id = visit.Id,
                    VisitCode = visit.VisitCode,
                    ATSScale = new EDATSScaleViewModel
                    {
                        EnName = visit.ATSScaleEnName,
                        ViName = visit.ATSScaleViName,
                        Code = visit.ATSScaleCode,
                        Note = visit.ATSScaleNote
                    },
                    AdmittedDate = visit.AdmittedDate.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                    IsRetailService = visit.IsRetailService,
                    Customer = new EDCustomerViewModel
                    {
                        PID = visit.CustomerPID,
                        Phone = visit.CustomerPhone,
                        Fullname = visit.CustomerFullname,
                        DateOfBirth = visit.CustomerDateOfBirth?.ToString(Constant.DATE_FORMAT),
                        IsAllergy = visit.CustomerIsAllergy,
                        Allergy = visit.CustomerAllergy,
                        KindOfAllergy = visit.CustomerKindOfAllergy,
                        IsVip = visit.IsVip
                    },
                    EmergencyStatus = new EDStatusViewModel
                    {
                        EnName = visit.EDStatusEnName,
                        ViName = visit.EDStatusViName
                    },
                    IsFallRiskScreening = IsFallRiskScreening(visit),
                    CovidRiskGroup = visit.CovidRiskGroup,
                    Doctor = visit.PrimaryDoctor?.Username,
                    SelfHarmRiskScreeningResults = visit.SelfHarmRiskScreeningResults,
                    VisitAllergy = getEDAllergy(visit),
                    IsVip = visit.IsVip,
                    CreatedBy = visit.CreatedBy == null ? "" : visit.CreatedBy,
                    Version = visit.Version,
                    UserDoTriage = visit.UserDoTriage,
                    UserReceiver = visit.UserReceiver
                });
            return Ok(new { count, results = items, IsCovidSpecialty = IsCovidSpecialty() });
        }
        protected VisitAllergyModel getEDAllergy(EDQueryModel edmodel)
        {
            if (edmodel.CustomerIsAllergy != null)
            {
                return new VisitAllergyModel()
                {
                    IsAllergy = edmodel.CustomerIsAllergy,
                    Allergy = edmodel.CustomerAllergy,
                    KindOfAllergy = edmodel.CustomerKindOfAllergy,
                };
            }
            return new VisitAllergyModel() { };
        }
        private bool IsFallRiskScreening(EDQueryModel visit)
        {
            if (visit.IsHasFallRiskScreening != null)
            {
                return Convert.ToBoolean(visit.IsHasFallRiskScreening.ToString());
            }
            return false;
            //var fallRick_version2 = unitOfWork.EDFallRickScreenningRepository.AsQueryable()
            //                        .Where(e => !e.IsDeleted && e.VisitId == visit.Id)
            //                        .OrderByDescending(o => o.UpdatedAt)
            //                        .FirstOrDefault();
            //if(fallRick_version2 != null)
            //{
            //    var customer_isFallRick = (from d in unitOfWork.FormDatasRepository.AsQueryable()
            //                               where !d.IsDeleted && d.FormId == fallRick_version2.Id
            //                               && d.FormCode == "A02_007_220321_VE_VERSION2"
            //                               && d.Code == "ETRUTHANS1" && d.VisitType == "ED"
            //                               select d).FirstOrDefault();
            //    if( customer_isFallRick != null)
            //    {
            //        if (customer_isFallRick.Value == "1")
            //            return true;
            //    }

            //    return false;
            //}

            //if (visit.IsHasFallRiskScreening != null)
            //    return (bool)visit.IsHasFallRiskScreening;
            // return unitOfWork.EmergencyTriageRecordDataRepository
            //    .Count(
            //    e => !e.IsDeleted &&
            //    e.EmergencyTriageRecordId != null &&
            //    e.EmergencyTriageRecordId == visit.EmergencyTriageRecordId &&
            //    new List<string> { "ETRDPH1", "ETRDPH2", "ETRDPHA3", "ETRDPN1", "ETRDPN2", "ETRDPN3" }.Contains(e.Code) &&
            //    e.Value == "1") > 0;
        }
    }
}