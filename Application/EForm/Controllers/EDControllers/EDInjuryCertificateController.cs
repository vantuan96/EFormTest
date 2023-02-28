using DataAccess.Models.EDModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers;
using EForm.Models.IPDModels;
using EForm.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.EDControllers
{
    [SessionAuthorize]
    public class EDInjuryCertificateController : BaseEDApiController
    {

        [HttpGet]
        [Route("api/ED/Document/InjuryCertificate/{id}")]
        [Permission(Code = "EINCE1")]
        public IHttpActionResult CreateEDCardiacArrestRecordAPI(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

           var yesValue = ed.DischargeInformation?.DischargeInformationDatas?.FirstOrDefault(x => x.Code == "DI0COINYES")?.Value;
            if (string.IsNullOrEmpty(yesValue) || Convert.ToBoolean(yesValue) == false)
            {
                return Content(HttpStatusCode.NotFound, Message.PATIENT_NOT_CONFIRM_INJURY);
            }

            var ic = GetOrCreateInjuryCertificate(ed.Id);
            var result = GetDetailInjuryCertificate(ic, ed);

            return Content(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/Document/InjuryCertificate/Confirm/{id}")]
        [Permission(Code = "EINCE2")]
        public IHttpActionResult ConfirmEDInjuryCertificateAPI(Guid id, [FromBody]JObject request)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var ic = GetOrCreateInjuryCertificate(ed.Id);
            if (ic == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var kind = request["kind"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);

            var positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName);

            if (kind == "Doctor" && positions.Contains("Doctor") && ic.DoctorId == null)
            {
                ic.DoctorId = user.Id;
                ic.DoctorTime = DateTime.Now;
            }
            else if (kind == "HeadOfDept" && positions.Contains("Head Of Department") && ic.HeadOfDeptId == null)
            {
                ic.HeadOfDeptId = user.Id;
                ic.HeadOfDeptTime = DateTime.Now;
            }
            else if (kind == "Director" && positions.Contains("Director") && ic.DirectorId == null)
            {
                ic.DirectorId = user.Id;
                ic.DirectorTime = DateTime.Now;
            }
            else
                return Content(HttpStatusCode.BadRequest, Message.OWNER_FORBIDDEN);

            unitOfWork.EDInjuryCertificateRepository.Update(ic);
            unitOfWork.Commit();
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        private EDInjuryCertificate GetOrCreateInjuryCertificate(Guid ed_id)
        {
            var ic = unitOfWork.EDInjuryCertificateRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == ed_id
            );
            if (ic == null)
            {
                ic = new EDInjuryCertificate() { VisitId = ed_id };
                unitOfWork.EDInjuryCertificateRepository.Add(ic);
                unitOfWork.Commit();
            }
            return ic;
        }

        private dynamic GetDetailInjuryCertificate(EDInjuryCertificate ic, ED ed)
        {
            var customer = ed.Customer;
            var gender = new CustomerUtil(customer).GetGender();

            var admitted_date = ed.AdmittedDate.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND);

            var etr = ed.EmergencyTriageRecord;
            var chief_complain = etr.EmergencyTriageRecordDatas.FirstOrDefault(e => !e.IsDeleted && e.Code == "ETRCC0ANS")?.Value;

            var emer_record = ed.EmergencyRecord;
            var emer_record_datas = emer_record.EmergencyRecordDatas.ToList();
            var assessment = new EmergencyRecordAssessment((Guid)ed.EmergencyRecordId).GetList();
            var discharge_info = ed.DischargeInformation;
            var discharge_date = discharge_info.AssessmentAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND);
            var discharge_info_datas = discharge_info.DischargeInformationDatas.ToList();
            var treatment_procedures = discharge_info_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0TAPANS")?.Value;
            var current_status = discharge_info_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0CS0ANS")?.Value;

            var doctor = ic.Doctor;
            var head_of_dept = ic.HeadOfDept;
            var director = ic.Director;
            var DischargeDate = GetDischargeDate(ed);
            var site = GetSite();
            var translation_util = new TranslationUtil(unitOfWork, ed.Id, "ED", "Injury Certificate");
            var translations = translation_util.GetList();
            return new {
                site?.Location,
                Site = site?.Name,
                site?.Province,
                site?.LocationUnit,

                customer.PID,
                Name = customer.Fullname,
                DOB = customer.DateOfBirth?.ToString(Constant.DATE_FORMAT),
                Gender = gender,
                customer.Job,
                customer.WorkPlace,
                customer.IdentificationCard,
                IssueDate = customer.IssueDate?.ToString(Constant.DATE_FORMAT),
                customer.IssuePlace,
                customer.Address,

                AdmittedDate = admitted_date,
                DischargeDate = DischargeDate?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                ChiefComplain = chief_complain,
                Diagnosis = GetDiagnosisED(discharge_info_datas, emer_record_datas, ed.Version, "INJURY CERTIFICATE"),
                TreatmentProcedures = treatment_procedures,
                Assessment = assessment,
                CurrentStatus = current_status,

                DoctorTime = ic.DoctorTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                Doctor = new { doctor?.Username, doctor?.Fullname, doctor?.DisplayName, doctor?.Title},
                HeadOfDeptTime = ic.HeadOfDeptTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                HeadOfDept = new { head_of_dept?.Username, head_of_dept?.Fullname, head_of_dept?.DisplayName, head_of_dept?.Title},
                DirectorTime = ic.DirectorTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                Director = new { director?.Username, director?.Fullname, director?.DisplayName, director?.Title},
                Translations = translations
            };
        }

    }
}
