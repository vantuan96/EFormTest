using DataAccess.Models;
using DataAccess.Models.EDModel;
using DataAccess.Models.EIOModel;
using DataAccess.Models.OPDModel;
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
    public class EDDocumentController : BaseEDApiController

    {
        // Bao cao y te
        [HttpGet]
        [Route("api/ED/Document/MedicalReport/{id}")]
        [Permission(Code = "EDOCU1")]
        public IHttpActionResult GetMedicalReport(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var customer = ed.Customer;

            var etr = ed.EmergencyTriageRecord;

            var chief_complain = etr.EmergencyTriageRecordDatas.FirstOrDefault(e => !e.IsDeleted && e.Code == "ETRCC0ANS")?.Value;

            var emer_record = ed.EmergencyRecord;
            var emer_record_datas = emer_record.EmergencyRecordDatas.ToList();
            var history = emer_record_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "ER0HISANS")?.Value;
            var assessment = new EmergencyRecordAssessment(emer_record.Id).GetList();

            var discharge_info = ed.DischargeInformation;
            var discharge_info_datas = discharge_info.DischargeInformationDatas.ToList();
            var rop_tests = discharge_info_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0RPTANS2")?.Value;
            var icd = discharge_info_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0DIAICD")?.Value;
            var icd_option = discharge_info_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0DIAOPT")?.Value;
            var treatment_procedures = discharge_info_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0TAPANS")?.Value;
            var significant_medications = discharge_info_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0SM0ANS2")?.Value;
            var current_status = discharge_info_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0CS0ANS")?.Value;
            var followup_care_plan = discharge_info_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0FCPANS")?.Value;
            var doctor_recommendations = discharge_info_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0DR0ANS")?.Value;

            var translation_util = new TranslationUtil(unitOfWork, ed.Id, "ED", "Medical report");
            var translations = translation_util.GetList();

            var status = ed.EDStatus;

            var site = GetSite();
            var DischargeDate = GetDischargeDate(ed);

            // Tạo Id định danh giấy báo cáo y tế
            var form = CreateIdForForm(ed.Id, "A01_144_050919_VE", "ED", ed.Version);

            return Ok(new
            {
                Location = ed.Site?.Location,
                Site = site?.Name,
                site?.Province,
                Date = DischargeDate?.ToString(Constant.DATE_FORMAT),
                customer.Fullname,
                DateOfBirth = customer.DateOfBirth?.ToString(Constant.DATE_FORMAT),
                customer.PID,
                AdmittedDate = ed.AdmittedDate.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                DischargeDate = DischargeDate?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                ChiefComplain = chief_complain,
                History = history,
                Assessment = assessment,
                ResultOfParaclinicalTests = rop_tests,
                Diagnosis = GetDiagnosisED(discharge_info_datas,emer_record_datas, ed.Version, "MEDICAL REPORT"),
                ICD = icd,
                ICDOption = icd_option,
                TreatmentAndProcedures = treatment_procedures,
                SignificantMedications = significant_medications,
                CurrentStatus = current_status,
                FollowupCarePlan = followup_care_plan,
                DoctorRecommendations = doctor_recommendations,
                Translations = translations,
                Status = new
                {
                    status?.Id,
                    status?.ViName,
                    status?.EnName,
                    status?.Code
                },
                Specialty = ed.Specialty?.ViName,
                ed.Version,
                Id = form?.Id
            });
        }

        // Bao cao y te ra vien
        [HttpGet]
        [Route("api/ED/Document/DischargeMedicalReport/{id}")]
        [Permission(Code = "EDOCU2")]
        public IHttpActionResult DischargeMedicalReport(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            
            var customer = ed.Customer;
            var gender = new CustomerUtil(customer).GetGender();

            var etr = ed.EmergencyTriageRecord;
            var chief_complain = etr.EmergencyTriageRecordDatas.FirstOrDefault(e => !e.IsDeleted && e.Code == "ETRCC0ANS")?.Value;

            var emer_record = ed.EmergencyRecord;
            var emer_record_datas = emer_record.EmergencyRecordDatas.ToList();
            var history = emer_record_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "ER0HISANS")?.Value;
            var assessment = new EmergencyRecordAssessment(emer_record.Id).GetList();

            var discharge_info = ed.DischargeInformation;
            var discharge_info_datas = discharge_info.DischargeInformationDatas.ToList();
            var rop_tests = discharge_info_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0RPTANS2")?.Value;
            var treatment_procedures = discharge_info_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0TAPANS")?.Value;
            var significant_medications = discharge_info_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0SM0ANS2")?.Value;
            var current_status = discharge_info_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0CS0ANS")?.Value;
            var followup_care_plan = discharge_info_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0FCPANS")?.Value;
            var doctor_recommendations = discharge_info_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0DR0ANS")?.Value;

            var icd_diagnosis =  discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0DIAICD")?.Value;
            var co_morbidities = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0DIAOPT2")?.Value;
            var icd_co_morbidities = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0DIAOPT")?.Value;

            var site = GetSite();

            var DischargeDate = GetDischargeDate(ed);
            var translation_util = new TranslationUtil(unitOfWork, ed.Id, "ED", "Discharge Medical Report");
            var translations = translation_util.GetList();
            return Ok(new
            {
                Department = "Khoa cấp cứu",
                customer.Fullname,
                DateOfBirth = customer.DateOfBirth?.ToString(Constant.DATE_FORMAT),
                Gender = gender,
                customer.Nationality,
                customer.PID,
                customer.Address,
                AdmittedDate = ed.AdmittedDate.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                DischargeDate = DischargeDate?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                ChiefComplain = chief_complain,
                History = history,
                Assessment = assessment,
                ResultOfParaclinicalTests = rop_tests,
                Diagnosis = GetDiagnosisED(discharge_info_datas, emer_record_datas, ed.Version, "DISCHARGE MEDICAL REPORT"),
                TreatmentAndProcedures = treatment_procedures,
                SignificantMedications = significant_medications,
                CurrentStatus = current_status,
                FollowupCarePlan = followup_care_plan,
                DoctorRecommendations = doctor_recommendations,
                site?.Location,
                Site = site?.Name,
                site?.Province,
                Date = DateTime.Now.ToString(Constant.DATE_FORMAT),
                ICDDiagnosis = icd_diagnosis,
                CoMorbidities = co_morbidities,
                ICDCoMorbidities = icd_co_morbidities,
                Specialty = ed.Specialty?.ViName,
                Translations = translations,
                ed.Version,
                Id = discharge_info.Id
            });
        }


        // Giay ra vien
        [HttpGet]
        [Route("api/ED/Document/DischargeCertificate/{id}")]
        [Permission(Code = "EDOCU3")]
        public IHttpActionResult DischargeCertificate(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);
            var customer = ed.Customer;
            var gender = new CustomerUtil(customer).GetGender();

            var discharge_info = ed.DischargeInformation;
            var discharge_info_datas = discharge_info.DischargeInformationDatas.ToList();
            var diagnosis = discharge_info_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0DIAANS")?.Value;
            var treatment_procedures = discharge_info_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0TAPANS")?.Value;
            var doctor_recommendations = discharge_info_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0DR0ANS")?.Value;
            var department = GetSpecialty();
            var site = GetSite();
            var DischargeDate = GetDischargeDate(ed);
            var diagnosis2 = GetVisitDiagnosisAndICD(ed.Id, "ED", true);
            var confirm = GetFormConfirmForDischargeCertificate(ed);
            var translation_util = new TranslationUtil(unitOfWork, ed.Id, "ED", "Discharge Certificate");
            var translations = translation_util.GetList();
            var emer_record = ed.EmergencyRecord;
            var emer_record_datas = emer_record.EmergencyRecordDatas.ToList();
            return Ok(new
            {
                site?.Location,
                Site = site?.Name,
                site?.Province,
                Date = DischargeDate?.ToString(Constant.DATE_FORMAT),
                customer.PID,
                customer.Fullname,
                Age = DateTime.Now.Year - customer.DateOfBirth?.Year,
                customer.AgeFormated,
                Gender = gender,
                Folk = customer.Fork,
                customer.Job,
                customer.Address,
                ed.HealthInsuranceNumber,
                StartHealthInsuranceDate = ed.StartHealthInsuranceDate?.ToString(Constant.DATE_FORMAT),
                ExpireHealthInsuranceDate = ed.ExpireHealthInsuranceDate?.ToString(Constant.DATE_FORMAT),
                AdmittedDate = ed.AdmittedDate.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                DischareDate = DischargeDate?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                Diagnosis = GetDiagnosisED(discharge_info_datas, emer_record_datas, ed.Version, "DISCHARGE CERTIFICATE"),
                TreatmentAndProcedures = treatment_procedures,
                DoctorRecommendations = doctor_recommendations,
                Department = new
                {
                    department?.EnName,
                    department?.ViName
                },
                Specialty = ed.Specialty?.ViName,
                Diagnosis2 = diagnosis2,
                Confirm = confirm,
                ed.Version,
                Translations = translations,
                Id = ed.Id
            });
        }

        [HttpPost]
        [Route("api/ED/Document/DischargeCertificate/Confirm/{id}")]
        [Permission(Code = "EDDISCHAGECONFIRM")]
        public IHttpActionResult ConfirmDischargeCertificate(Guid id, [FromBody] JObject req)
        {
            ED visit = GetED(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            string userName = req["username"]?.ToString();
            string pass = req["password"]?.ToString();
            var user = GetAcceptUser(userName, pass);
            if (user == null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);

            var acction = GetActionOfUser(user, "EDDISCHAGECONFIRM");
            if (acction == null)
                return Content(HttpStatusCode.Forbidden, Message.DOCTOR_IS_UNAUTHORIZED);

            string confirmType = req["kind"]?.ToString();
            bool success = CreateConfirmDischargeCertificate(visit.Id, confirmType, user);
            if (!success)
                return Content(HttpStatusCode.BadRequest, Message.DOCTOR_ACCEPTED);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        // Giay chuyen vien
        [HttpGet]
        [Route("api/ED/Document/ReferralLetter/{id}")]
        [Permission(Code = "EDOCU4")]
        public IHttpActionResult ReferralLetter(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var customer = ed.Customer;
            var gender = new CustomerUtil(customer).GetGender();

            var nationality = customer.Nationality;
            if (!string.IsNullOrEmpty(nationality))
                nationality = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(nationality.ToLower());

            var date_now = DateTime.Now.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND);

            var etr = ed.EmergencyTriageRecord;
            var chief_complain = etr.EmergencyTriageRecordDatas.FirstOrDefault(e => !e.IsDeleted && e.Code == "ETRCC0ANS")?.Value;

            var emer_record = ed.EmergencyRecord;
            var emer_record_datas = emer_record.EmergencyRecordDatas.ToList();
            var history = emer_record_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "ER0HISANS")?.Value;
            var assessment = new EmergencyRecordAssessment(emer_record.Id).GetList();

            var discharge_info = ed.DischargeInformation;
            var discharge_info_datas = discharge_info.DischargeInformationDatas.ToList();
            var receiving_hospital = discharge_info_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0RH0ANS")?.Value;
            var reason_for_transfer = discharge_info_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0RFTANS")?.Value;
            var receiving_person = discharge_info_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0NRHANS")?.Value;
            var transportation_method = discharge_info_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0TM0ANS")?.Value;
            var escort_person = discharge_info_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0NATANS")?.Value;
            var rop_tests = discharge_info_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0RPTANS2")?.Value;
            var diagnosis = discharge_info_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0DIAANS")?.Value;
            var treatment_procedures = discharge_info_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0TAPANS")?.Value;
            var significant_medications = discharge_info_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0SM0ANS2")?.Value;
            var current_status = discharge_info_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0CS0ANS")?.Value;
            var followup_care_plan = discharge_info_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0FCPANS")?.Value;
            var doctor_recommendations = discharge_info_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0DR0ANS")?.Value;
            var DischargeDate = GetDischargeDate(ed);
            var site = GetSite();
            var translation_util = new TranslationUtil(unitOfWork, ed.Id, "ED", "Referral Letter");
            var translations = translation_util.GetList();

            // Tạo Id định danh
            const string formCode = "A01_145_050919_VE";
            var form = CreateIdForForm(ed.Id, formCode, "ED", ed.Version);

            return Content(HttpStatusCode.OK, new
            {
                site?.Location,
                Site = site?.Name,
                site?.Province,
                To = receiving_hospital,
                Name = customer.Fullname,
                DOB = customer.DateOfBirth?.ToString(Constant.DATE_FORMAT),
                Gender = gender,
                Nationality = nationality,
                customer.PID,
                StartDate = ed.AdmittedDate.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                EndDate = DischargeDate?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                InsuranceCardNo = ed.HealthInsuranceNumber,
                ExpireDate = ed.ExpireHealthInsuranceDate?.ToString(Constant.DATE_FORMAT),
                ReasonForTransfer = reason_for_transfer,
                TimeOfTransfer = DischargeDate?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                ReceivingPerson = receiving_person,
                TransportationMethod = transportation_method,
                EscortPerson = escort_person,
                ReasonsForAdmission = chief_complain,
                History = history,
                Assessment = assessment,
                ResultOfParaclinicalTests = rop_tests,
                Diagnosis = GetDiagnosisED(discharge_info_datas, emer_record_datas, ed.Version, "REFERRAL LETTER"),
                TreatmentAndProcedures = treatment_procedures,
                DrugsUsed = significant_medications,
                PatientStatusAtTransfer = current_status,
                PlanOfCare = followup_care_plan,
                DoctorRecommendations = doctor_recommendations,
                Date = DischargeDate?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                PhysicianInCharge = "",
                Director = "",
                Specialty = ed.Specialty?.ViName,
                ed.Version,
                Translations = translations,
                Id = form?.Id
            });
        }

        // Giay xac nhan cap cuu
        [HttpGet]
        [Route("api/ED/Document/EmergencyConfirmation/{id}")]
        [Permission(Code = "EDOCU5")]
        public IHttpActionResult EmergencyConfirmationAPI(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var yesValue = ed.DischargeInformation?.DischargeInformationDatas?.FirstOrDefault(x => x.Code == "DI0COEMYES")?.Value;
            if (string.IsNullOrEmpty(yesValue) || Convert.ToBoolean(yesValue) == false)
            {
                return Content(HttpStatusCode.NotFound, Message.PATIENT_NOT_CONFIRM_EMERGENCY);
            }
            var customer = ed.Customer;
            var gender = new CustomerUtil(customer).GetGender();

            var emer_record = ed.EmergencyRecord;
            var assessment = new EmergencyRecordAssessment((Guid)ed.EmergencyRecordId).GetList();

            var emer = ed.EmergencyRecord;
            var emer_datas = emer.EmergencyRecordDatas.ToList();
            var diagnosis = emer_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "ER0ID0ANS")?.Value;

            var discharge_info = ed.DischargeInformation;
            var discharge_info_datas = discharge_info.DischargeInformationDatas.ToList();
            var confirm_yes = discharge_info_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0COEMYES")?.Value;
            var confirm_no = discharge_info_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0COEMNOO")?.Value;
            var DischargeDate = GetDischargeDate(ed);
            var site = GetSite();
            var translation_util = new TranslationUtil(unitOfWork, ed.Id, "ED", "Emergency Confirmation");
            var translations = translation_util.GetList();

            //tạo Id cho giấy xác nhận bệnh nhân cấp cứu
            const string formCode = "A01_155_050919_V";
            var form = CreateIdForForm(ed.Id, formCode, "ED", ed.Version);

            return Content(HttpStatusCode.OK, new
            {
                Date = DischargeDate?.ToString(Constant.DATE_FORMAT),
                site?.Location,
                Site = site?.Name,
                site?.Province,
                site?.LocationUnit,
                Name = customer.Fullname,
                Age = DateTime.Now.Year - customer.DateOfBirth?.Year,
                customer.AgeFormated,
                Gender = gender,
                customer.Address,
                customer.PID,
                AdmittedDate = ed.AdmittedDate.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                Department = "Khoa hồi sức cấp cứu",
                Assessment = assessment,
                Diagnosis = GetDiagnosisED(discharge_info_datas, emer_datas, ed.Version, "EMERGENCY CONFIRMATION"),
                Confirm = new { Yes = confirm_yes, No = confirm_no },
                Specialty = ed.Specialty?.ViName,
                Translations = translations,
                ed.Version,
                Id = form?.Id
            });
        }

        // Giay chuyen tuyen
        [HttpGet]
        [Route("api/ED/Document/TransferLetter/{id}")]
        [Permission(Code = "EDOCU6")]
        public IHttpActionResult TransferLetterAPI(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);
            var customer = ed.Customer;
            var gender = new CustomerUtil(customer).GetGender();

            var clinical_examination_and_findings = new EmergencyRecordAssessment((Guid)ed.EmergencyRecordId).GetList();

            var discharge_info = ed.DischargeInformation;
            var discharge_info_datas = discharge_info.DischargeInformationDatas.ToList();
            var hospital = discharge_info_datas.FirstOrDefault(e => e.Code == "DI0RH1ANS")?.Value;
            var principal_tests = discharge_info_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0RPTANS2")?.Value;
            var diagnosis = discharge_info_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0DIAANS")?.Value;
            var care_plan = discharge_info_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0FCPANS")?.Value;
            var methods = discharge_info_datas.FirstOrDefault(e => e.Code == "DI0TAPANS")?.Value;
            var drug_main = discharge_info_datas.FirstOrDefault(e => e.Code == "DI0SM0ANS2")?.Value;
            var patient_status = discharge_info_datas.FirstOrDefault(e => e.Code == "DI0CS0ANS")?.Value;
            var reason_1 = discharge_info_datas.FirstOrDefault(e => e.Code == "DI0RFT1SHT");
            var reason_1_data = unitOfWork.MasterDataRepository.FirstOrDefault(m => m.Code == "DI0RFT1SHT");
            var reason_2 = discharge_info_datas.FirstOrDefault(e => e.Code == "DI0RFT1LOG");
            var reason_2_data = unitOfWork.MasterDataRepository.FirstOrDefault(m => m.Code == "DI0RFT1LOG");
            var reason_for_transfer = new List<dynamic>() {
                new { reason_1_data?.ViName, reason_1?.Value},
                new { reason_2_data?.ViName, reason_2?.Value},
            };
            var treatment_plans = discharge_info_datas.FirstOrDefault(e => e.Code == "DI0TAPANS")?.Value;
            var transfer_date = discharge_info_datas.FirstOrDefault(e => e.Code == "DI0TD0ANS")?.Value;
            var transportation_method = discharge_info_datas.FirstOrDefault(e => e.Code == "DI0TM1ANS")?.Value;
            var escort = discharge_info_datas.FirstOrDefault(e => e.Code == "DI0NTMANS")?.Value;

            var physician = ed.PrimaryDoctor;
            var site = GetSite();

            var DischargeDate = GetDischargeDate(ed);

            var translation_util = new TranslationUtil(unitOfWork, ed.Id, "ED", "Transfer Letter");
            var translations = translation_util.GetList();

            // Tạo Id định danh
            const string formCode = "A01_167_180220_VE";
            var form = CreateIdForForm(ed.Id, formCode, "ED", ed.Version);

            var emer_record = ed.EmergencyRecord;
            var emer_record_datas = emer_record.EmergencyRecordDatas.ToList();
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
                StartHealthInsuranceDate = ed.StartHealthInsuranceDate?.ToString(Constant.DATE_FORMAT),
                ExpireHealthInsuranceDate = ed.ExpireHealthInsuranceDate?.ToString(Constant.DATE_FORMAT),
                StartDate = ed.AdmittedDate.ToString(Constant.DATE_FORMAT),
                EndDate = DischargeDate?.ToString(Constant.DATE_FORMAT),
                EndDateTime = DischargeDate?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                ed.HealthInsuranceNumber,
                ClinicalExaminationAndFindings = clinical_examination_and_findings,
                PrincipalTest = principal_tests,
                Diagnosis = GetDiagnosisED(discharge_info_datas, emer_record_datas, ed.Version, "TRANSFER LETTER"),
                CarePlan = care_plan,
                Method = drug_main != null && drug_main != "" ? methods +", "+ drug_main : methods,
                PatientStatus = patient_status,
                ReasonForTransfer = reason_for_transfer,
                TreatmentPlans = treatment_plans,
                TransferDate = transfer_date,
                TransportationMethod = transportation_method,
                Escort = escort,
                Date = transfer_date,
                Physician = physician?.Fullname,
                Specialty = ed.Specialty?.ViName,
                Translations =  translations,
                ed.Version,
                Id = form?.Id
            });
        }

        // GET XAC NHẬN GIẤY RA VIỆN VERSION 2
        private List<dynamic> GetFormConfirmForDischargeCertificate(ED visit)
        {
            var confirms = (from e in unitOfWork.EIOFormConfirmRepository.AsQueryable()
                            join u in unitOfWork.UserRepository.AsQueryable()
                            on e.ConfirmBy equals u.Username into default_query
                            where !e.IsDeleted && e.FormId == visit.Id
                            && e.Note == "EDCONFIRMDISCHAGE"
                            from user in default_query.DefaultIfEmpty()
                            select new
                            {
                                e.Id,
                                e.ConfirmAt,
                                e.ConfirmBy,
                                e.ConfirmType,
                                FullName = user.Fullname
                            }).ToList();

            return new List<dynamic>(confirms);
        }

        private bool CreateConfirmDischargeCertificate(Guid visitId, string confirmType, User user)
        {
            const string nameConfirm = "EDCONFIRMDISCHAGE";
            var confirm = (from e in unitOfWork.EIOFormConfirmRepository.AsQueryable()
                           where !e.IsDeleted && e.FormId == visitId
                           && e.Note == nameConfirm
                           && e.ConfirmType == confirmType
                           select e).FirstOrDefault();
            if (confirm == null)
            {
                EIOFormConfirm form = new EIOFormConfirm()
                {
                    FormId = visitId,
                    Note = nameConfirm,
                    ConfirmAt = DateTime.Now,
                    ConfirmBy = user.Username,
                    ConfirmType = confirmType
                };
                unitOfWork.EIOFormConfirmRepository.Add(form);
                unitOfWork.Commit();
                return true;
            }
            return false;
        }
        
    }
}