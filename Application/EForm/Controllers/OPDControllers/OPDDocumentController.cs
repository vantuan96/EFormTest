using DataAccess.Models.EIOModel;
using DataAccess.Models.OPDModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers;
using EForm.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.OPDControllers
{
    [SessionAuthorize]
    public class OPDDocumentController : BaseOPDApiController
    {
        [HttpGet]
        [Route("api/OPD/Document/MedicalReport/{id}")]
        [Permission(Code = "ODOCU1")]
        public IHttpActionResult MedicalReportAPI(Guid id)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var customer = opd.Customer;
            var gender = new CustomerUtil(customer).GetGender();
            var clinic = opd.Clinic;
            var oen = opd.OPDOutpatientExaminationNote;
            var oen_datas = oen.OPDOutpatientExaminationNoteDatas.Where(e => !e.IsDeleted)?.ToList();
            var reason_for_visit = oen_datas.FirstOrDefault(e => e.Code == "OPDOENCC0ANS")?.Value;
            var hisory_of_present_illness = oen_datas.FirstOrDefault(e => e.Code == "OPDOENHPIANS")?.Value;
            string clinicCode = GetStringClinicCodeUsed(opd);
            var clinical_examination_and_findings = hsClinicalExamination(oen) ? new ClinicalExaminationAndFindings(clinicCode, clinic?.Data, oen.Id, opd.IsTelehealth, oen.Version, unitOfWork).GetData() : null;
            List<DataClinicalFinding> clinical_examination_and_findingsTmp = new List<DataClinicalFinding>();
            if (clinical_examination_and_findings != null && clinical_examination_and_findings.Count > 0)
            {
                clinical_examination_and_findingsTmp = clinical_examination_and_findings.GroupBy(x => x.Code).Select(x => x.FirstOrDefault()).ToList();
            }
            var principal_tests = getOPDPrincipalTest(oen);
            //var diagnosis = oen_datas.FirstOrDefault(e => e.Code == "OPDOENDD0ANS")?.Value;
            var icd_diagnosis = oen_datas.FirstOrDefault(e => e.Code == "OPDOENICDANS")?.Value;
            var icd_option = oen_datas.FirstOrDefault(e => e.Code == "OPDOENICDOPT")?.Value;
            var treatment_plans = getOPDTreatmentPlans(oen);
            var recommendation_and_follow_up = oen_datas.FirstOrDefault(e => e.Code == "OPDOENRFUANS")?.Value;
            var date_of_next_appointment = oen_datas.FirstOrDefault(e => e.Code == "OPDOENDORANS")?.Value;
            var copy = oen_datas.FirstOrDefault(e => e.Code == "OPDOENCOPYANS")?.Value;

            var status = opd.EDStatus;

            dynamic physician = null;
            if (opd.AuthorizedDoctorId == null)
                physician = opd.PrimaryDoctor;

            var translation_util = new TranslationUtil(unitOfWork, opd.Id, "OPD", "Medical report");
            var translations = translation_util.GetList();
            var DischargeDate = GetDischargeDate(opd);
            var site = GetSite();
            var user = GetUser();
            // Tạo Id định danh 
            const string formCode = "A01_198_100120_VE";
            var form = CreateIdForForm(opd.Id, formCode, "OPD", opd.Version);

            return Ok(new
            {
                Location = opd.Site?.Location,
                Site = site?.Name,
                site?.Province,
                Date = DischargeDate?.ToString(Constant.DATE_FORMAT),
                customer.PID,
                customer.Fullname,
                ClinicCode = clinic?.Code,
                Gender = gender,
                DateOfBirth = customer.DateOfBirth?.ToString(Constant.DATE_FORMAT),
                customer.AgeFormated,
                customer.Address,
                opd.IsTelehealth,
                DischargeDate = opd.DischargeDate?.ToString(Constant.DATE_FORMAT),
                DateOfVisit = opd.AdmittedDate.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                ReasonOfVisit = reason_for_visit,
                HistoryOfPresentIllness = hisory_of_present_illness,
                ClinicalExaminationAndFindings = clinical_examination_and_findingsTmp,
                PrincipalTest = principal_tests,
                Diagnosis = GetDiagnosisOPD(oen_datas,opd.Version, "MEDICAL REPORT"),
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
                oen.AppointmentDateResult,
                PkntVersion = oen.Version,
                opd.Version,
                Id = form?.Id,
                IsLock24h = Is24hLocked(opd.CreatedAt, opd.Id, formCode, user.Username, form?.Id)
        });
        }

    // Giay chuyen tuyen
        [HttpGet]
        [Route("api/OPD/Document/TransferLetter/{id}")]
        [Permission(Code = "ODOCU2")]
        public IHttpActionResult TransferLetterAPI(Guid id)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var customer = opd.Customer;
            var gender = new CustomerUtil(customer).GetGender();

            var clinic = opd.Clinic;
            var oen = opd?.OPDOutpatientExaminationNote;
            var oen_datas = oen?.OPDOutpatientExaminationNoteDatas.Where(e => !e.IsDeleted)?.ToList();
            var hospital = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENRH0ANS")?.Value;
            string clinicCode = GetStringClinicCodeUsed(opd);
            var clinical_examination_and_findings = hsClinicalExamination(oen) ? new ClinicalExaminationAndFindings(clinicCode, clinic?.Data, oen.Id, opd.IsTelehealth, oen.Version, unitOfWork).GetData() : null;
            List<DataClinicalFinding> clinical_examination_and_findingsTmp = new List<DataClinicalFinding>();
            if (clinical_examination_and_findings != null && clinical_examination_and_findings.Count > 0)
            {
                clinical_examination_and_findingsTmp = clinical_examination_and_findings.GroupBy(x => x.Code).Select(x => x.FirstOrDefault()).ToList();
            }
            var principal_tests = getOPDPrincipalTest(oen);
            //var diagnosis = oen_datas.FirstOrDefault(e => e.Code == "OPDOENDD0ANS")?.Value;
            var methods = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENMTUANS")?.Value;
            var patient_status = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENPS0ANS")?.Value;
            var reason_1 = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENRFT1SHT");
            var reason_1_data = unitOfWork.MasterDataRepository?.FirstOrDefault(m => m.Code == "OPDOENRFT1SHT");
            var reason_2 = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENRFT1LOG");
            var reason_2_data = unitOfWork.MasterDataRepository?.FirstOrDefault(m => m.Code == "OPDOENRFT1LOG");
            var reason_for_transfer = new List<dynamic>() {
                new { reason_1_data?.ViName, reason_1?.Value},
                new { reason_2_data?.ViName, reason_2?.Value},
            };
            var treatment_plans = getOPDTreatmentPlans(oen);
            var transfer_date = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENTD0ANS")?.Value;
            var transportation_method = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENTM0ANS")?.Value;
            var escort = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENNTMANS")?.Value;
            var copy = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENCOPYANS")?.Value;
            var physician = opd.PrimaryDoctor;
            var site = GetSite();
            var DischargeDate = GetDischargeDate(opd);
            var translation_util = new TranslationUtil(unitOfWork, opd.Id, "OPD", "Transfer Letter");
            var translations = translation_util.GetList();

            // Tạo Id định danh
            const string formCode = "A01_167_180220_VE";
            var form = CreateIdForForm(opd.Id, formCode, "OPD", opd.Version);
            var user = GetUser();

            return Ok(new
            {
                Hospital = hospital,
                site?.Location,
                Site = site?.Name,
                site?.Province,
                site?.Level,
                site?.LocationUnit,
                customer.PID,
                customer.Fullname,
                Gender = gender,
                Age = DateTime.Now.Year - customer.DateOfBirth?.Year,
                customer.AgeFormated,
                customer.Address,
                customer.Fork,
                customer.Nationality,
                customer.Job,
                customer.WorkPlace,
                opd.IsTelehealth,
                StartHealthInsuranceDate = opd.StartHealthInsuranceDate?.ToString(Constant.DATE_FORMAT),
                ExpireHealthInsuranceDate = opd.ExpireHealthInsuranceDate?.ToString(Constant.DATE_FORMAT),
                StartDate = opd.AdmittedDate.ToString(Constant.DATE_FORMAT),
                EndDate = DischargeDate?.ToString(Constant.DATE_FORMAT),
                opd.HealthInsuranceNumber,
                ClinicalExaminationAndFindings = clinical_examination_and_findingsTmp,
                PrincipalTest = principal_tests,
                Diagnosis = GetDiagnosisOPD(oen_datas, opd.Version, "TRANSFER LETTER"),
                Method = methods,
                PatientStatus = patient_status,
                ReasonForTransfer = reason_for_transfer,
                TreatmentPlans = oen.IsConsultation == true && !string.IsNullOrEmpty(treatment_plans) ? string.Format("{0}{1}", "\n", treatment_plans) : treatment_plans,
                TransferDate = transfer_date,
                TransportationMethod = transportation_method,
                Escort = escort,
                Date = transfer_date,
                Copy = copy,
                Physician = physician?.Fullname,
                PkntVersion = oen.Version,
                Translations = translations,
                opd.Version,
                Id = form?.Id,
                IsLocked = Is24hLocked(opd.CreatedAt, opd.Id, formCode, user?.Username, form?.Id)
            });
        }

        // Giay chuyen vien
        [HttpGet]
        [Route("api/OPD/Document/ReferralLetter/{id}")]
        [Permission(Code = "ODOCU3")]
        public IHttpActionResult ReferralLetter(Guid id)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var customer = opd.Customer;
            var gender = new CustomerUtil(customer).GetGender();

            var nationality = customer.Nationality;
            if (!string.IsNullOrEmpty(nationality))
                nationality = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(nationality.ToLower());

            var date_now = DateTime.Now.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND);
            var DischargeDate = GetDischargeDate(opd);
            var clinic = opd.Clinic;
            var oen = opd.OPDOutpatientExaminationNote;
            var oen_datas = oen.OPDOutpatientExaminationNoteDatas.Where(e => !e.IsDeleted).ToList();
            var hospital = oen_datas.FirstOrDefault(e => e.Code == "OPDOENRH1ANS")?.Value;
            var examinated_from = opd.AdmittedDate.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND);
            var examinated_to = DischargeDate?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND);
            var reason_for_transfer = oen_datas.FirstOrDefault(e => e.Code == "OPDOENRFT3ANS")?.Value;
            var time_of_transfer = oen_datas.FirstOrDefault(e => e.Code == "OPDOENTOTANS")?.Value;
            var contacted_staff = oen_datas.FirstOrDefault(e => e.Code == "OPDOENSBCANS")?.Value;
            var transportation_method = oen_datas.FirstOrDefault(e => e.Code == "OPDOENTM1ANS")?.Value;
            var medical_escort = oen_datas.FirstOrDefault(e => e.Code == "OPDOENME0ANS")?.Value;
            var reasons_for_admission = oen_datas.FirstOrDefault(e => e.Code == "OPDOENCC0ANS")?.Value;
            var history = oen_datas.FirstOrDefault(e => e.Code == "OPDOENHPIANS")?.Value;

            string clinicCode = GetStringClinicCodeUsed(opd);
            var clinical_examination_and_findings = hsClinicalExamination(oen) ? new ClinicalExaminationAndFindings(clinicCode, clinic?.Data, oen.Id, opd.IsTelehealth, oen.Version, unitOfWork).GetData() : null;
            List<DataClinicalFinding> clinical_examination_and_findingsTmp = new List<DataClinicalFinding>();
            if (clinical_examination_and_findings != null && clinical_examination_and_findings.Count > 0)
            {
                clinical_examination_and_findingsTmp = clinical_examination_and_findings.GroupBy(x => x.Code).Select(x => x.FirstOrDefault()).ToList();
            }
            var result_of_paraclinical_tests = getOPDPrincipalTest(oen);
            //var definitive_diagnosis = oen_datas.FirstOrDefault(e => e.Code == "OPDOENDD0ANS")?.Value;
            var icd_diagnosis = oen_datas.FirstOrDefault(e => e.Code == "OPDOENICDANS")?.Value;
            var icd_option = oen_datas.FirstOrDefault(e => e.Code == "OPDOENICDOPT")?.Value;
            var treatment_and_procedures = getOPDTreatmentPlans(oen);
            var drugs_used = oen_datas.FirstOrDefault(e => e.Code == "OPDOENDU0ANS")?.Value;
            var patient_status_at_transfer = oen_datas.FirstOrDefault(e => e.Code == "OPDOENPSAANS")?.Value;
            var plan_of_care = oen_datas.FirstOrDefault(e => e.Code == "OPDOENRFUANS")?.Value;
            var copy = oen_datas.FirstOrDefault(e => e.Code == "OPDOENCOPYANS")?.Value;

            var physician = opd.PrimaryDoctor;
            var site = GetSite();
            var translation_util = new TranslationUtil(unitOfWork, opd.Id, "OPD", "Referral Letter");
            var translations = translation_util.GetList();


            // Tạo Id định danh
            const string formCode = "A01_145_050919_VE";
            var form = CreateIdForForm(opd.Id, formCode, "OPD", opd.Version);
            var user = GetUser();

            return Content(HttpStatusCode.OK, new
            {
                site?.Location,
                Site = site?.Name,
                site?.Province,
                site?.LocationUnit,
                To = hospital,
                Name = customer.Fullname,
                customer.AgeFormated,
                DOB = customer.DateOfBirth?.ToString(Constant.DATE_FORMAT),
                Gender = gender,
                Nationality = nationality,
                customer.PID,
                ExaminatedFrom = examinated_from,
                ExaminatedTo = examinated_to,
                InsuranceCardNo = opd.HealthInsuranceNumber,
                ExpireDate = opd.ExpireHealthInsuranceDate?.ToString(Constant.DATE_FORMAT),
                ReasonForTransfer = reason_for_transfer,
                TimeOfTransfer = time_of_transfer,
                ContactedStaff = contacted_staff,
                TransportationMethod = transportation_method,
                MedicalEscort = medical_escort,
                ReasonsForAdmission = reasons_for_admission,
                History = history,
                ClinicalExaminationAndFindings = clinical_examination_and_findingsTmp,
                ResultOfParaclinicalTests = result_of_paraclinical_tests,
                DefinitiveDiagnosis = GetDiagnosisOPD(oen_datas, opd.Version, "REFERRAL LETTER"),
                IcdDiagnosis = icd_diagnosis,
                IcdOption = icd_option,
                TreatmentAndProcedures = oen.IsConsultation == true && !string.IsNullOrEmpty(treatment_and_procedures) ? string.Format("{0}{1}", "\n", treatment_and_procedures) : treatment_and_procedures,
                DrugsUsed = drugs_used,
                PatientStatusAtTransfer = patient_status_at_transfer,
                PlanOfCare = plan_of_care,
                Copy = copy,
                Date = DischargeDate?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                PhysicianInCharge = physician?.Fullname,
                Director = "",
                PkntVersion = oen.Version,
                Translations = translations,
                opd.Version,
                Id = form?.Id,
                IsLocked = Is24hLocked(opd.CreatedAt, opd.Id, formCode, user?.Username, form?.Id)
            });
        }

        [HttpGet]
        [Route("api/OPD/Document/ProcedureCertificate/{id}")]
        [Permission(Code = "ODOCU4")]
        public IHttpActionResult GetProcedureCertificateAPI(Guid id)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            EIOProcedureSummary procedure = unitOfWork.EIOProcedureSummaryRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.VisitId == id &&
                e.VisitTypeGroupCode == "OPD"
            );
            if (procedure == null)
                return Content(HttpStatusCode.BadRequest, Message.EIO_PRSU_NOT_FOUND);

            var customer = opd.Customer;
            var gender = new CustomerUtil(customer).GetGender();

            var nationality = customer.Nationality;
            if (!string.IsNullOrEmpty(nationality))
                nationality = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(nationality.ToLower());

            var admission_date = opd.AdmittedDate.ToString(Constant.DATE_FORMAT);
            var oen = opd.OPDOutpatientExaminationNote;
            var discharge_date = oen?.ExaminationTime?.ToString(Constant.DATE_FORMAT);

            var procedure_data = procedure.EIOProcedureSummaryDatas.Where(e => !e.IsDeleted);
            var procedure_date = procedure_data.FirstOrDefault(e => e.Code == "OPDTTTTTGLANS")?.Value;
            var preprocedural_diagnosis = procedure_data.FirstOrDefault(e => e.Code == "OPDTTTTCDTANS")?.Value;
            var postprocedural_diagnosis = procedure_data.FirstOrDefault(e => e.Code == "OPDTTTTCDSANS")?.Value;
            var procedure_performed = procedure_data.FirstOrDefault(e => e.Code == "OPDTTTTPPLANS")?.Value;
            var method_of_anesthesia = procedure_data.FirstOrDefault(e => e.Code == "OPDTTTTPPVCANS")?.Value;
            var surgeon_name = procedure_data.FirstOrDefault(e => e.Code == "OPDTTTTBSLANS")?.Value;
            var anesthetist = procedure_data.FirstOrDefault(e => e.Code == "OPDTTTTBSGMANS")?.Value;

            return Content(HttpStatusCode.OK, new
            {
                Department = opd.Specialty?.ViName,
                Name = customer.Fullname,
                customer.PID,
                DOB = customer.DateOfBirth?.ToString(Constant.DATE_FORMAT),
                Gender = gender,
                Nationality = nationality,
                customer.Address,
                AdmissionDate = admission_date,
                DischargeDate = discharge_date,
                ProcedureDate = procedure_date,
                PreproceduralDiagnosis = preprocedural_diagnosis,
                PostproceduralDiagnosis = postprocedural_diagnosis,
                ProcedurePerformed = procedure_performed,
                MethodOfAnesthesia = method_of_anesthesia,
                SurgeonName = surgeon_name,
                Anesthetist = anesthetist,
                Surgeon = procedure.ProcedureDoctor?.Fullname,
                HeadOfDeft = procedure.HeadOfDepartment?.Fullname,
                Director = procedure.Director?.Fullname,
                opd.Version
            });
        }
    }
}
