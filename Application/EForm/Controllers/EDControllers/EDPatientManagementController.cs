using DataAccess.Models.EDModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers;
using EForm.Utils;
using EForm.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.EDControllers
{
    [SessionAuthorize]
    public class EDPatientManagementController : BaseEDApiController
    {
        [HttpGet]
        [Route("api/ED/PatientManagement")]
        [Permission(Code = "EPAMA1")]
        public IHttpActionResult GetEDPatientManagementAPI([FromUri]PagingParameterModel request)
        {
            var site_id = GetSiteId();
            var specialty_id = GetSpecialtyId();
            InHospital in_hospital = new InHospital();
            var in_hospital_status_id = in_hospital.GetStatus().Id;

            var query = (from visit_sql in unitOfWork.EDRepository.AsQueryable().Where(
                            e => !e.IsDeleted &&
                            e.CustomerId != null &&
                            e.EDStatusId != null &&
                            e.EDStatusId == in_hospital_status_id &&
                            e.SiteId != null &&
                            e.SiteId == site_id &&
                            e.SpecialtyId != null &&
                            e.SpecialtyId == specialty_id
                        )
                         join form_sql in unitOfWork.EmergencyTriageRecordRepository.AsQueryable()
                             on visit_sql.EmergencyTriageRecordId equals form_sql.Id
                         join nurse_sql in unitOfWork.UserRepository.AsQueryable() on visit_sql.CurrentNurseId equals nurse_sql.Id
                             into n_list from nurse_sql in n_list.DefaultIfEmpty()
                         join doctor_sql in unitOfWork.UserRepository.AsQueryable() on visit_sql.CurrentDoctorId equals doctor_sql.Id
                             into d_list from doctor_sql in d_list.DefaultIfEmpty()
                         join master_sql in unitOfWork.MasterDataRepository.AsQueryable().Where(m => m.Group == "ETRATS")
                            on visit_sql.ATSScale equals master_sql.Code
                            into m_list from master_sql in m_list.DefaultIfEmpty()
                         select new PatientManagementQueryModel
                         {
                             Id = visit_sql.Id,
                             AdmittedDate = visit_sql.AdmittedDate,
                             Bed = visit_sql.Bed,
                             Reason = visit_sql.Reason,
                             CurrentDoctorId = doctor_sql.Id,
                             CurrentDoctorUsername = doctor_sql.Username,
                             CurrentDoctorFullname = doctor_sql.DisplayName,
                             CurrentDoctorFullShort = doctor_sql.Fullname,
                             CurrentNurseId = nurse_sql.Id,
                             CurrentNurseUsername = nurse_sql.Username,
                             CurrentNurseFullname = nurse_sql.DisplayName,
                             CurrentNurseFullShort = nurse_sql.Fullname,
                             ATSScaleCode = master_sql.Code,
                             ATSScaleViName = master_sql.ViName,
                             ATSScaleEnName = master_sql.EnName,
                             ATSScaleNote = master_sql.Note,
                         }).OrderByDescending(e => e.AdmittedDate);

            int count = query.Count();
            var items = query.Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList()
                .Select(e => new PatientManagementViewModel{
                    Id = e.Id,
                    AdmittedDate = e.AdmittedDate.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                    Bed = e.Bed,
                    Reason = e.Reason,
                    CurrentDoctor = new {
                        Id = e.CurrentDoctorId,
                        Username = e.CurrentDoctorUsername,
                        Fullname = e.CurrentDoctorFullname,
                        FullShort = e.CurrentDoctorFullShort,
                    },
                    CurrentNurse = new {
                        Id = e.CurrentNurseId,
                        Username = e.CurrentNurseUsername,
                        Fullname = e.CurrentNurseFullname,
                        FullShort = e.CurrentNurseFullShort,
                    },
                    ATSScale = new {
                        Code = e.ATSScaleCode,
                        ViName = e.ATSScaleViName,
                        EnName = e.ATSScaleEnName,
                        Note = e.ATSScaleNote,
                    }
                });

            return Ok(new { count, results = items });
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/ED/PatientManagement")]
        [Permission(Code = "EPAMA2")]
        public IHttpActionResult UpdateEDPatientManagementAPI([FromBody] JObject request)
        {
            var specialty_id = GetSpecialtyId();

            foreach (var visit in request["results"])
            {
                var str_id = visit["Id"]?.ToString();
                if (string.IsNullOrEmpty(str_id)) continue;

                var ed = GetPatientVisit(str_id, specialty_id);
                if (ed == null) continue;

                UpdateED(ed, visit);
            }
            unitOfWork.Commit();
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        private ED GetPatientVisit(string str_id, Guid spec_id)
        {
            Guid id = new Guid(str_id);
            return unitOfWork.EDRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.Id == id &&
                e.SpecialtyId != null &&
                e.SpecialtyId == spec_id
            );
        }

        private void UpdateED(ED ed, JToken data)
        {
            bool is_change = false;
            var current_doctor_id = ConvertId(data["CurrentDoctor"]?["Id"]?.ToString());
            var current_nurse_id = ConvertId(data["CurrentNurse"]?["Id"]?.ToString());
            if (current_doctor_id != ed.CurrentDoctorId)
            {
                ed.CurrentDoctorId = current_doctor_id;
                is_change = true;
            }
            if (current_nurse_id != ed.CurrentNurseId)
            {
                ed.CurrentNurseId = current_nurse_id;
                is_change = true;
            }
            if (is_change)
                unitOfWork.EDRepository.Update(ed);
        }

        private Guid? ConvertId(string str_id)
        {
            try
            {
                Guid id = new Guid(str_id);
                return id;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}