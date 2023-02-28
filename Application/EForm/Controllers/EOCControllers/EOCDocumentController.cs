using DataAccess.Models.EIOModel;
using DataAccess.Models.OPDModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers;
using EForm.Models;
using EForm.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.EOCControllers
{
    [SessionAuthorize]
    public class EOCDocumentController : BaseEOCApiController
    {        
        [HttpGet]
        [Route("api/eoc/Document/MedicalReport/{id}")]
        [Permission(Code = "EOC011")]
        public IHttpActionResult MedicalReport(Guid id)
        {
            var visit = GetEOC(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);
            var oen = GetOutpatientExaminationNote(id);

            if (oen == null) return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);

            var customer = visit.Customer;
            var gender = new CustomerUtil(customer).GetGender();
            var clinic = clinic_code;
            var oen_datas = GetFormData(visit.Id, oen.Id, "OPDOEN");
            var reason_for_visit = oen_datas.FirstOrDefault(e => e.Code == "OPDOENCC0ANS")?.Value;
            var hisory_of_present_illness = oen_datas.FirstOrDefault(e => e.Code == "OPDOENHPIANS")?.Value;
            var clinical_examination_and_findings = oen_datas.FirstOrDefault(e => e.Code == "OPDOENCEFANS")?.Value;
            var principal_tests = oen_datas.FirstOrDefault(e => e.Code == "OPDOENPT0ANS")?.Value;
            //var diagnosis = oen_datas.FirstOrDefault(e => e.Code == "OPDOENDD0ANS")?.Value;
            var icd_diagnosis = oen_datas.FirstOrDefault(e => e.Code == "OPDOENICDANS")?.Value;
            var icd_option = oen_datas.FirstOrDefault(e => e.Code == "OPDOENICDOPT")?.Value;
            var treatment_plans = oen_datas.FirstOrDefault(e => e.Code == "OPDOENTP0ANS")?.Value;
            var recommendation_and_follow_up = oen_datas.FirstOrDefault(e => e.Code == "OPDOENRFUANS")?.Value;
            var date_of_next_appointment = oen_datas.FirstOrDefault(e => e.Code == "OPDOENDORANS")?.Value;
            var copy = oen_datas.FirstOrDefault(e => e.Code == "OPDOENCOPYANS")?.Value;

            var status = visit.Status;

            dynamic physician = visit.PrimaryDoctor;

            var translation_util = new TranslationUtil(unitOfWork, visit.Id, "EOC", "Medical report");
            var translations = translation_util.GetList();
            var DischargeDate = GetDischargeDate(visit);
            var site = GetSite();

            const string formCode = "A01_198_100120_VE";
            var form = CreateIdForForm(visit.Id, formCode, "EOC", visit.Version);

            return Ok(new
            {
                IsEoc = true,
                site?.Location,
                Site = site?.Name,
                site?.Province,
                Date = DischargeDate?.ToString(Constant.DATE_FORMAT),
                customer.PID,
                customer.Fullname,
                ClinicCode = clinic_code,
                customer.AgeFormated,
                Gender = gender,
                DateOfBirth = customer.DateOfBirth?.ToString(Constant.DATE_FORMAT),
                customer.Address,
                DateOfVisit = visit.AdmittedDate.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                ReasonOfVisit = reason_for_visit,
                HistoryOfPresentIllness = hisory_of_present_illness,
                ClinicalExaminationAndFindings = clinical_examination_and_findings,
                PrincipalTest = principal_tests,
                Diagnosis = GetDiagnosisEOC(oen_datas,visit.Version, "MEDICAL REPORT"),
                ICDDiagnosis = icd_diagnosis,
                ICDOption = icd_option,
                TreatmentPlans = treatment_plans,
                RecommendtionAndFollowUp = recommendation_and_follow_up,
                DateOfNextAppointment = date_of_next_appointment,
                Physician = physician?.Fullname,
                Sign = "",
                Copy = copy,
                Translations = translations,
                Status = new
                {
                    status?.Id,
                    status?.EnName,
                    status?.ViName,
                    status?.Code
                },
                oen.Version,
                DataOenVersion2 = GetDataEOCOutpatientExaminationNotesVerson2(oen_datas, Constant.EOC_DATA_FILL_OEN_MEDICAL_REPORT),
                VisitVersion = visit.Version,
                Id = form.Id
            });
        }
        [HttpGet]
        [Route("api/eoc/Document/TransferLetter/{id}")]
        [Permission(Code = "EOC012")]
        public IHttpActionResult TransferLetterAPI(Guid id)
        {
            var visit = GetEOC(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);
            var oen = GetOutpatientExaminationNote(id);

            if (oen == null) return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);

            var customer = visit.Customer;
            var gender = new CustomerUtil(customer).GetGender();

            var clinic = clinic_code;
            var oen_datas = GetFormData(visit.Id, oen.Id, "OPDOEN");
            var hospital = oen_datas.FirstOrDefault(e => e.Code == "OPDOENRH0ANS")?.Value;
            var clinical_examination_and_findings = oen_datas.FirstOrDefault(e => e.Code == "OPDOENCEFANS")?.Value;
            var principal_tests = oen_datas.FirstOrDefault(e => e.Code == "OPDOENPT0ANS")?.Value;
            var diagnosis = oen_datas.FirstOrDefault(e => e.Code == "OPDOENDD0ANS")?.Value;
            var methods = oen_datas.FirstOrDefault(e => e.Code == "OPDOENMTUANS")?.Value;
            var patient_status = oen_datas.FirstOrDefault(e => e.Code == "OPDOENPS0ANS")?.Value;
            var reason_1 = oen_datas.FirstOrDefault(e => e.Code == "OPDOENRFT1SHT");
            var reason_1_data = unitOfWork.MasterDataRepository.FirstOrDefault(m => m.Code == "OPDOENRFT1SHT");
            var reason_2 = oen_datas.FirstOrDefault(e => e.Code == "OPDOENRFT1LOG");
            var reason_2_data = unitOfWork.MasterDataRepository.FirstOrDefault(m => m.Code == "OPDOENRFT1LOG");
            var reason_for_transfer = new List<dynamic>() {
                new { reason_1_data?.ViName, reason_1?.Value},
                new { reason_2_data?.ViName, reason_2?.Value},
            };
            var treatment_plans = oen_datas.FirstOrDefault(e => e.Code == "OPDOENTP0ANS")?.Value;
            var transfer_date = oen_datas.FirstOrDefault(e => e.Code == "OPDOENTD0ANS")?.Value;
            var transportation_method = oen_datas.FirstOrDefault(e => e.Code == "OPDOENTM0ANS")?.Value;
            var escort = oen_datas.FirstOrDefault(e => e.Code == "OPDOENNTMANS")?.Value;
            var copy = oen_datas.FirstOrDefault(e => e.Code == "OPDOENCOPYANS")?.Value;
            var physician = visit.PrimaryDoctor;
            var site = GetSite();
            var DischargeDate = GetDischargeDate(visit);
            var end_date = DischargeDate?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND);
            var translation_util = new TranslationUtil(unitOfWork, visit.Id, "EOC", "Transfer Letter");
            var translations = translation_util.GetList();

            // Tạo Id định danh
            const string formCode = "A01_167_180220_VE";
            var form = CreateIdForForm(visit.Id, formCode, "EOC", visit.Version);

            return Ok(new
            {
                Hospital = hospital,
                site?.Location,
                Site = site?.Name,
                site?.Province,
                site?.Level,
                site?.LocationUnit,
                customer.AgeFormated,
                customer.PID,
                customer.Fullname,
                Gender = gender,
                Age = DateTime.Now.Year - customer.DateOfBirth?.Year,
                customer.Address,
                customer.Fork,
                customer.Nationality,
                customer.Job,
                customer.WorkPlace,
                StartHealthInsuranceDate = visit.StartHealthInsuranceDate?.ToString(Constant.DATE_FORMAT),
                ExpireHealthInsuranceDate = visit.ExpireHealthInsuranceDate?.ToString(Constant.DATE_FORMAT),
                StartDate = visit.AdmittedDate.ToString(Constant.DATE_FORMAT),
                EndDate = end_date,
                visit.HealthInsuranceNumber,
                ClinicalExaminationAndFindings = clinical_examination_and_findings,
                PrincipalTest = principal_tests,
                Diagnosis = GetDiagnosisEOC(oen_datas, visit.Version, "TRANSFER LETTER"),
                Method = methods,
                PatientStatus = patient_status,
                ReasonForTransfer = reason_for_transfer,
                TreatmentPlans = treatment_plans,
                TransferDate = transfer_date,
                TransportationMethod = transportation_method,
                Escort = escort,
                Date = end_date,
                Copy = copy,
                Physician = physician?.Fullname,
                oen.Version,
                DataOenVersion2 = GetDataEOCOutpatientExaminationNotesVerson2(oen_datas, Constant.EOC_DATA_FILL_OEN_MEDICAL_REPORT), // Khám lâm sàng version2 phiếu khám bệnh EOC 
                VisitVersion = visit.Version,
                Translations = translations,
                Id = form?.Id
            });
        }

        private string conventDate(string transfer_date)
        {
            DateTime request_examination_time = DateTime.ParseExact(transfer_date, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            if (request_examination_time != null)
            {
                return request_examination_time.ToString(Constant.DATE_FORMAT);
            }
            return "";
        }

        // Giay chuyen vien
        [HttpGet]
        [Route("api/eoc/Document/ReferralLetter/{id}")]
        [Permission(Code = "EOC013")]
        public IHttpActionResult ReferralLetter(Guid id)
        {
            var visit = GetEOC(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);
            var oen = GetOutpatientExaminationNote(id);

            if (oen == null) return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);

            var customer = visit.Customer;
            var gender = new CustomerUtil(customer).GetGender();

            var nationality = customer.Nationality;
            if (!string.IsNullOrEmpty(nationality))
                nationality = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(nationality.ToLower());
            var DischargeDate = GetDischargeDate(visit);
            var date_now = DischargeDate?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND);

            var oen_datas = GetFormData(visit.Id, oen.Id, "OPDOEN");
            var hospital = oen_datas.FirstOrDefault(e => e.Code == "OPDOENRH1ANS")?.Value;
            var examinated_from = visit.AdmittedDate.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND);
            var examinated_to = oen.ExaminationTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND);
            var reason_for_transfer = oen_datas.FirstOrDefault(e => e.Code == "OPDOENRFT3ANS")?.Value;
            var time_of_transfer = oen_datas.FirstOrDefault(e => e.Code == "OPDOENTOTANS")?.Value;
            var contacted_staff = oen_datas.FirstOrDefault(e => e.Code == "OPDOENSBCANS")?.Value;
            var transportation_method = oen_datas.FirstOrDefault(e => e.Code == "OPDOENTM1ANS")?.Value;
            var medical_escort = oen_datas.FirstOrDefault(e => e.Code == "OPDOENME0ANS")?.Value;
            var reasons_for_admission = oen_datas.FirstOrDefault(e => e.Code == "OPDOENCC0ANS")?.Value;
            var history = oen_datas.FirstOrDefault(e => e.Code == "OPDOENHPIANS")?.Value;
            var clinical_examination_and_findings = oen_datas.FirstOrDefault(e => e.Code == "OPDOENCEFANS")?.Value;
            var result_of_paraclinical_tests = oen_datas.FirstOrDefault(e => e.Code == "OPDOENPT0ANS")?.Value;
            var definitive_diagnosis = oen_datas.FirstOrDefault(e => e.Code == "OPDOENDD0ANS")?.Value;
            var icd_diagnosis = oen_datas.FirstOrDefault(e => e.Code == "OPDOENICDANS")?.Value;
            var icd_option = oen_datas.FirstOrDefault(e => e.Code == "OPDOENICDOPT")?.Value;
            var treatment_and_procedures = oen_datas.FirstOrDefault(e => e.Code == "OPDOENTP0ANS")?.Value;
            var drugs_used = oen_datas.FirstOrDefault(e => e.Code == "OPDOENDU0ANS")?.Value;
            var patient_status_at_transfer = oen_datas.FirstOrDefault(e => e.Code == "OPDOENPSAANS")?.Value;
            var plan_of_care = oen_datas.FirstOrDefault(e => e.Code == "OPDOENRFUANS")?.Value;
            var copy = oen_datas.FirstOrDefault(e => e.Code == "OPDOENCOPYANS")?.Value;

            var physician = visit.PrimaryDoctor;
            var site = GetSite();
            var translation_util = new TranslationUtil(unitOfWork, visit.Id, "EOC", "Referral Letter");
            var translations = translation_util.GetList();

            // Tạo Id định danh
            const string formCode = "A01_145_050919_VE";
            var form = CreateIdForForm(visit.Id, formCode, "EOC", visit.Version);

            return Content(HttpStatusCode.OK, new
            {
                site?.Location,
                Site = site?.Name,
                site?.Province,
                site?.LocationUnit,
                To = hospital,
                Name = customer.Fullname,
                DOB = customer.DateOfBirth?.ToString(Constant.DATE_FORMAT),
                customer.AgeFormated,
                Gender = gender,
                Nationality = nationality,
                customer.PID,
                ExaminatedFrom = examinated_from,
                ExaminatedTo = DischargeDate?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                InsuranceCardNo = visit.HealthInsuranceNumber,
                ExpireDate = visit.ExpireHealthInsuranceDate?.ToString(Constant.DATE_FORMAT),
                ReasonForTransfer = reason_for_transfer,
                TimeOfTransfer = time_of_transfer,
                ContactedStaff = contacted_staff,
                TransportationMethod = transportation_method,
                MedicalEscort = medical_escort,
                ReasonsForAdmission = reasons_for_admission,
                History = history,
                ClinicalExaminationAndFindings = clinical_examination_and_findings,
                ResultOfParaclinicalTests = result_of_paraclinical_tests,
                DefinitiveDiagnosis = GetDiagnosisEOC(oen_datas, visit.Version, "REFERRAL LETTER"),
                IcdDiagnosis = icd_diagnosis,
                IcdOption = icd_option,
                TreatmentAndProcedures = treatment_and_procedures,
                DrugsUsed = drugs_used,
                PatientStatusAtTransfer = patient_status_at_transfer,
                PlanOfCare = plan_of_care,
                Copy = copy,
                Date = date_now,
                PhysicianInCharge = physician?.Fullname,
                Director = "",
                oen.Version,
                DataOenVersion2 = GetDataEOCOutpatientExaminationNotesVerson2(oen_datas, Constant.EOC_DATA_FILL_OEN_MEDICAL_REPORT), // Khám lâm sàng version2 phiếu khám bệnh EOC,
                Translations = translations,
                VisitVersion = visit.Version,
                Id = form?.Id
            });
        }

        private dynamic GetDataEOCOutpatientExaminationNotesVerson2(List<FormDataValue> datas ,string[] code_data)
        {
            var result = (from cd in code_data
                          join d in datas on cd equals d.Code
                          join m in unitOfWork.MasterDataRepository.AsQueryable() on cd equals m.Code
                          into data_join
                          from data in data_join.DefaultIfEmpty()
                          select new 
                          {
                              
                              Code = cd,
                              Value = d?.Value == null ? "" : d?.Value,
                              Id = d?.Id,
                              FormId = d?.FormId,
                              FormCode = d?.FormCode,
                              data?.ViName,
                              data?.EnName,
                          }).ToList();
            return result;
        }
    }
}
