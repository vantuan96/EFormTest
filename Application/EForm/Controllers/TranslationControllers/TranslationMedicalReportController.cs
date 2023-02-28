using DataAccess.Models;
using DataAccess.Models.EDModel;
using DataAccess.Models.OPDModel;
using DataAccess.Models.IPDModel;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using EForm.Utils;
using DataAccess.Models.EOCModel;
using EForm.Models.IPDModels;
using static EForm.Controllers.IPDControllers.IPDSurgeryCertificateController;
using DataAccess.Models.EIOModel;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using EForm.Models;
using System.Data.Entity;
using System.Diagnostics;
using DataAccess.Dapper.Repository;

namespace EForm.Controllers.TranslationControllers
{
    [SessionAuthorize]
    public class TranslationMedicalReportController : BaseApiController
    {
        [HttpGet]
        [Route("api/Translation/MedicalReport/Document/{id}")]
        [Permission(Code = "TMERE1")]
        public IHttpActionResult GetTranslationDocumentMedicalReportAPI(Guid id)
        {
            var translation = unitOfWork.TranslationRepository.FirstOrDefault(e => e.Id == id && !e.IsDeleted);

            if (translation == null)
                return Content(HttpStatusCode.NotFound, Message.TRANSLATION_NOT_FOUND);

            dynamic visit = GetVisit((Guid)translation.VisitId, translation.VisitTypeGroupCode);
            if(visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);

            dynamic response = null;
            if (translation.VisitTypeGroupCode == "ED" )
            {
                response = BuildEDMedicalReport((ED)visit, translation);
            }
            else if (translation.VisitTypeGroupCode == "OPD")
            {
                response = BuildOPDMedicalReport((OPD)visit, translation);
            }
            else if (translation.VisitTypeGroupCode == "IPD")
            {
                response = BuildIPDMedicalReport((IPD)visit, translation);
            }
            else if (translation.VisitTypeGroupCode == "EOC")
            {
                response = BuildEOCMedicalReport((EOC)visit, translation);
            }
            return Content(HttpStatusCode.OK, response);
        }

        [HttpGet]
        [Route("api/Translation/MedicalReport/Trans/{id}")]
        [Permission(Code = "TMERE2")]
        public IHttpActionResult GetTranslationDetailAPI(Guid id)
        {
         
            var translation = unitOfWork.TranslationRepository.FirstOrDefault(x => x.Id == id);
            if (translation == null)
                return Content(HttpStatusCode.NotFound, Message.TRANSLATION_NOT_FOUND);

           
            List<dynamic> from_datas = new List<dynamic>();
            var clinic = translation.VisitTypeGroupCode + "_" + translation.EnName + "_" + translation.FromLanguage;
            
            string form = "TRANSLATE" + translation.FromLanguage;
 
            List<MasterData> masters = unitOfWork.MasterDataRepository.Find(x => x.Form == form && x.Level == 2 && x.Clinic.Contains(clinic) && !x.IsDeleted).OrderBy(x => x.CreatedAt).ToList();

            if (translation.VisitTypeGroupCode == "ED")
                from_datas = GetEDMedicalReport(translation, form, masters);
            else if (translation.VisitTypeGroupCode == "OPD")
                from_datas = GetOPDMedicalReport(translation, form, masters);
            else if (translation.VisitTypeGroupCode == "IPD")
                from_datas = GetIPDMedicalReport((Guid)translation.VisitId, translation.FromLanguage, translation.EnName, translation.FormId, masters);
            else if (translation.VisitTypeGroupCode == "EOC")
                from_datas = GetEOCMedicalReport(translation, form, masters);

            var to_datas = translation.TranslationDatas.Where(e => !e.IsDeleted).Select(e => new { e.Id, e.Code, e.Value }).ToList();
            List<ConvertData> toDataRespon = new List<ConvertData>();
            if (to_datas.Count > 0)
            {
                if(translation.VisitTypeGroupCode == "ED")
                {
                    var klsAns = (translation.UpdatedAt <= new DateTime(2022, 11, 17, 18, 00, 00) ? to_datas.FirstOrDefault(y => y.Code == "TRANSLATEASSESSMENTANSANS")?.Value : to_datas.FirstOrDefault(y => y.Code == "TRANSLATEKLSANS")?.Value);
                    toDataRespon = masters.Select(e => new ConvertData
                    {
                        Id = e.Id,
                        Code = e.Code,
                        Value = (e.Code == "TRANSLATEKLSANS") ? klsAns : to_datas.FirstOrDefault(y => y.Code == e.Code)?.Value

                    }).ToList();  
                }
                else
                {
                    toDataRespon = masters.Select(e => new ConvertData
                    {
                        Id = e.Id,
                        Code = e.Code,
                        Value = to_datas.FirstOrDefault(y => y.Code == e.Code)?.Value
                    }).ToList();
                }      
            }
            else
            {
                //var datas = unitOfWork.TranslationRepository.Find(x => x.VisitId == translation.VisitId && x.ToLanguage == translation.ToLanguage && !x.IsDeleted).Join(unitOfWork.TranslationDataRepository.Find(x => !x.IsDeleted).AsQueryable(), trans => trans.Id, transData => transData.TranslationId, (trans, transData) => new
                //{
                //    Id = transData.Id,
                //    Code = transData.Code,
                //    Value = transData.Value,
                //    UpdatedAt = transData.UpdatedAt,
                //    CreatedAt = transData.CreatedAt
                //}).OrderByDescending(x => x.UpdatedAt).ToList().Join(unitOfWork.MasterDataRepository.Find(x => !x.IsDeleted && x.Form == form && x.Level == 2).AsQueryable(), tdata => tdata.Code, m => m.Code, (tdata, m) => new
                //DataModel
                //{
                //    Id = tdata.Id,
                //    Code = tdata.Code,
                //    Value = tdata.Value,
                //    UpdatedAt = tdata.UpdatedAt,
                //    CreatedAt = tdata.CreatedAt,
                //    DefaultValue = m.DefaultValue,
                //    Note = m.Note,
                //    Data = m.Data,
                //}).ToList();
                var datas = ExecQuery.getValueAndNodeInMaster((Guid)translation.VisitId, translation.ToLanguage, form);
                toDataRespon = masters.Select(e => new ConvertData
                {
                    Id = e.Id,
                    Code = e.Code,
                    Value = datas.FirstOrDefault(x => x.Note.Contains(translation.VisitTypeGroupCode + e.Code))?.Value ?? ""
                }).ToList();
            }
            
       // var first_ipd = GetFirstIpdInVisitTypeIPD(visit_ipd);
           
            return Content(HttpStatusCode.OK, new
            {
                translation.Id,
                translation.VisitId,
                translation.VisitTypeGroupCode,
                translation.ViName,
                translation.EnName,
                translation.FromLanguage,
                translation.ToLanguage,
                translation.Note,
                translation.Status,
                FromDatas = from_datas,
                Datas = toDataRespon,
                translation.CreatedBy,
                translation.CreatedAt,
                translation.UpdatedAt,
                //PkntVersion = oen?.Version,
               // AdmittedDateFirstIpd = first_ipd?.CurrentDate,
                InfoCustomer = new
                {
                    PID = translation.PID,
                    VisitCode = translation.VisitCode,
                    FullName = translation.CustomerName
                }
            });
        }

        public dynamic BuildEDMedicalReport(ED ed, Translation translation)
        {
            var trans_datas = translation.TranslationDatas.Where(e => !e.IsDeleted).ToList();

            var customer = ed.Customer;
            var site = GetSite();

            var etr = ed.EmergencyTriageRecord;

            var emer_record = ed.EmergencyRecord;
            var emer_record_datas = emer_record.EmergencyRecordDatas.Where(e => !e.IsDeleted).AsQueryable().ToList();

            var discharge_info = ed.DischargeInformation;
            var discharge_info_datas = discharge_info.DischargeInformationDatas.AsQueryable().Where(e => !e.IsDeleted).ToList();
            var icd = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0DIAICD")?.Value;
            var icd_option = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0DIAOPT")?.Value;
            var DischargeDate = GetDischargeDate(ed);
            var chief_complain = etr.EmergencyTriageRecordDatas?.FirstOrDefault(e => e.Code == "ETRCC0ANS")?.Value;
            var history = emer_record_datas?.FirstOrDefault(e => e.Code == "ER0HISANS")?.Value;
            var rop_tests = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0RPTANS2")?.Value;
            var current_status = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0CS0ANS")?.Value;
            var co_morbidities = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0DIAOPT2")?.Value;

            var addressTmp = trans_datas?.FirstOrDefault(e => !e.IsDeleted && e.Code == "TRANSLATEADDANS")?.Value;
            var addressTrans = addressTmp == null ? trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEADDFOOTERANS")?.Value : addressTmp;
            var recommendationTrans = "";
            var assessmentTrans = "";
            var diagnosisTrans = "";
            var currentStatusTrans = "";
            var methods = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0TAPANS")?.Value;
            var drug_main = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0SM0ANS2")?.Value;
            string codeCd = "", diagnosis = "", transportation_method = "";


            var principal_tests = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0RPTANS2")?.Value;
            var treatment_plans = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0TAPANS")?.Value;
            var care_plan = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0FCPANS")?.Value;
            var clinical_examination_and_findings = new EmergencyRecordAssessment((Guid)ed.EmergencyRecordId).GetList();

            var escort = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0NTMANS")?.Value;
            var reason_for_transfer = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0RFTANS")?.Value;
            var receiving_person = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0NRHANS")?.Value;
            var followup_care_plan = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0FCPANS")?.Value;

            List<DataClinicalFinding> clinical_examination_and_findingsTmp = new List<DataClinicalFinding>();
            List<DataClinicalFinding> clinical_ex_data = new List<DataClinicalFinding>();
            if (clinical_examination_and_findings != null && clinical_examination_and_findings.Count > 0)
            {
                clinical_examination_and_findingsTmp = clinical_examination_and_findings.GroupBy(x => x.Code).Select(x => x.FirstOrDefault()).ToList();

                foreach (var clin in clinical_examination_and_findingsTmp)
                {
                    var c = new DataClinicalFinding
                    {
                        ViName = clin.ViName,
                        EnName = clin.EnName,
                        Group = clin.Group,
                        Order = clin.Order,
                        Value = trans_datas.FirstOrDefault(e => e.Code == clin.CodeOther)?.Value,
                        Code = clin.CodeOther
                    };
                    clinical_ex_data.Add(c);
                }
            };
            var escort_person = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0NATANS")?.Value;

            switch (translation.EnName.ToUpper())
            {
                case "MEDICAL REPORT":
                    codeCd = "DI0DIAANS";
                    diagnosis = discharge_info_datas?.FirstOrDefault(e => e.Code == codeCd)?.Value;
                    diagnosisTrans = trans_datas?.FirstOrDefault(e =>  e.Code == "TRANSLATEPDIAGNOSISANS")?.Value;
                    recommendationTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEDOCTORRECOMENDATIONANS")?.Value;
                    currentStatusTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATECURSTATUSANS")?.Value;
                    break;
                case "DISCHARGE MEDICAL REPORT":
                    codeCd = "DI0DIAANS";
                    currentStatusTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATCURRENTPATIENTANS")?.Value;
                    diagnosisTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEPDIAGNOSISANS")?.Value;
                    recommendationTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEDOCTORRECOMENDATIONANS")?.Value;
                    diagnosis = discharge_info_datas?.FirstOrDefault(e => e.Code == codeCd)?.Value;
                    break;
                case "DISCHARGE CERTIFICATE":
                    codeCd = "DI0DIAANS";
                    recommendationTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATENOTEANS")?.Value;
                    diagnosisTrans = trans_datas?.FirstOrDefault(e => !e.IsDeleted && e.Code == "TRANSLATEPDIAGNOSISANS")?.Value;
                    diagnosis = discharge_info_datas?.FirstOrDefault(e => !e.IsDeleted && e.Code == codeCd)?.Value;
                    break;
                case "EMERGENCY CONFIRMATION":
                    codeCd = "ER0ID0ANS";
                    assessmentTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATETCCCDHANS")?.Value;
                    diagnosisTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEDIAGNOSISBEFOREMERANS")?.Value;
                    diagnosis = emer_record_datas?.FirstOrDefault(e => e.Code == codeCd)?.Value;
                    break;
                case "INJURY CERTIFICATE":
                    codeCd = "DI0DIAANS";
                    diagnosis = discharge_info_datas?.FirstOrDefault(e => e.Code == codeCd)?.Value;
                    currentStatusTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATESTATUSINJURYDISCHARGEANS")?.Value;
                    assessmentTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATESTATUSADMITTEDANS")?.Value;
                    var diagnosisOptions = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0DIAOPT2")?.Value ?? "";
                    diagnosisTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEPDIAGNOSIS1ANS")?.Value;
                    break;
                case "TRANSFER LETTER":
                    codeCd = "DI0DIAANS";
                    diagnosis = discharge_info_datas?.FirstOrDefault(e => e.Code == codeCd)?.Value;
                    currentStatusTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATESATATUSREFERALANS")?.Value;
                    transportation_method = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0TM1ANS")?.Value;
                    recommendationTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEDOCTORRECOMENDATIONANS")?.Value;
                    diagnosisTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEPDIAGNOSISANS")?.Value;
                    break;
                case "REFERRAL LETTER":
                    codeCd = "DI0DIAANS";
                    transportation_method = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0TM0ANS")?.Value;
                    diagnosis = discharge_info_datas?.FirstOrDefault(e => e.Code == codeCd)?.Value;
                    recommendationTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEDOCTORRECOMENDATIONANS")?.Value;
                    diagnosisTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEPDIAGNOSISANS")?.Value;
                    break;
                default:
                    codeCd = "DI0DIAANS";
                    diagnosis = discharge_info_datas?.FirstOrDefault(e => e.Code == codeCd)?.Value;
                    currentStatusTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATECURSTATUSANS")?.Value;
                    assessmentTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEASSESSMENTANS")?.Value;
                    recommendationTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEDOCTORRECOMENDATIONANS")?.Value;
                    diagnosisTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEPDIAGNOSISANS")?.Value;
                    break;
            }
            return new
            {

                site?.Location,
                Site = site?.Name,
                site?.Province,
                Date = DischargeDate?.ToString(Constant.DATE_FORMAT),
                customer.Fullname,
                customer.Nationality,
                customer.Job,
                customer.Address,
                customer.WorkPlace,
                Gender = new CustomerUtil(customer).GetGender(),
                DateOfBirth = customer.DateOfBirth?.ToString(Constant.DATE_FORMAT),
                customer.PID,
                AdmittedDate = ed.AdmittedDate.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                DischargeDate = DischargeDate?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                ChiefComplain = chief_complain,
                History = history,
                Assessment = new EmergencyRecordAssessment(emer_record.Id).GetList(),
                ResultOfParaclinicalTests = rop_tests,
                Diagnosis = GetDiagnosisED(discharge_info_datas, emer_record_datas, ed.Version, translation.EnName.ToUpper()),
                ICD = icd,
                ICDOption = icd_option,
                TreatmentAndProcedures = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0TAPANS")?.Value,
                SignificantMedications = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0SM0ANS2")?.Value,
                FollowupCarePlan = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0FCPANS")?.Value,
                DoctorRecommendations = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0DR0ANS")?.Value,
                CurrentStatus = current_status,
                CoMorbidities = co_morbidities,
                PrincipalTest = principal_tests,
                TreatmentPlans = treatment_plans,
                CarePlan = care_plan,
                ClinicalExaminationAndFindings = clinical_examination_and_findingsTmp,
                TransportationMethod = transportation_method,
                Escort = escort,
                ReasonForTransfer = reason_for_transfer,
                ReceivingPerson = receiving_person,
                PatientStatusAtTransfer = current_status,
                PlanOfCare = followup_care_plan,
                Method = drug_main != null && drug_main != "" ? methods + ", " + drug_main : methods,
                EscortPerson = escort_person,
                Translation = new
                {
                    Gender = trans_datas?.Where(x => x.Code.StartsWith("TRANSLATEGEN")).ToList().FirstOrDefault()?.Value,
                    site?.Location,
                    Site = site?.Name,
                    site?.Province,
                    Date = DischargeDate?.ToString(Constant.DATE_FORMAT),
                    customer.Fullname,
                    DateOfBirth = customer.DateOfBirth?.ToString(Constant.DATE_FORMAT),
                    customer.PID,
                    AdmittedDate = ed?.AdmittedDate.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                    DischargeDate = DischargeDate?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                    ChiefComplain = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEREASONANS")?.Value,
                    HistoryOfPresentIllness = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEHISTORYOFPRESENTANS")?.Value,
                    Assessment = assessmentTrans,
                    ResultOfParaclinicalTests = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATESUBRESULTANS")?.Value,
                    Diagnosis = diagnosisTrans,
                    ICD = icd,
                    ICDOption = icd_option,
                    TreatmentAndProcedures = trans_datas?.Where(x => x.Code.StartsWith("TRANSLATETREATMENT")).ToList().FirstOrDefault()?.Value,
                    SignificantMedications = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEDRUGUSEDANS")?.Value,
                    CurrentStatus = currentStatusTrans,
                    FollowupCarePlan = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATECLINEVOLUANS")?.Value,
                    DoctorRecommendations = recommendationTrans,
                    History = trans_datas?.Where(x => x.Code.StartsWith("TRANSLATEHISTORY")).ToList().FirstOrDefault()?.Value,
                    Nationality = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATENATANS")?.Value,
                    Job = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEJOBANS")?.Value,
                    Address = trans_datas?.Where(x => x.Code.StartsWith("TRANSLATEADD")).ToList().FirstOrDefault()?.Value,
                    WorkPlace = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEWORKPLACEANS")?.Value,
                    CoMorbidities = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATECOMORBIDITIESANS")?.Value,
                    PrincipalTest = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATELABANDSUBRESULTANS")?.Value,
                    TreatmentPlans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATESATATUSREFERALANS")?.Value,
                    CarePlan = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATETREANTMENTPLANANS")?.Value,
                    ClinicalExaminationAndFindings = translation.UpdatedAt <= new DateTime(2022, 11, 17, 18, 00, 00) ? trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEASSESSMENTANSANS")?.Value : Newtonsoft.Json.JsonConvert.SerializeObject(clinical_ex_data),
                    TransportationMethod = trans_datas?.Where(x => x.Code.StartsWith("TRANSLATETRANSPORT")).ToList().FirstOrDefault()?.Value,
                    Escort = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEPERTRANSPORTANS")?.Value,
                    ReasonForTransfer = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEREASONTRANSFERANS")?.Value,
                    ReceivingPerson = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATENAMEMETHODCONTACTEDANS")?.Value,
                    PatientStatusAtTransfer = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATESTATUSPATIENTTRANSFER1ANS")?.Value,
                    PlanOfCare = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATECAREPLANANS")?.Value,
                    Method = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEMPTDRUGUSETREATMENTANS")?.Value,
                    EscortPerson = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATESENDERANS")?.Value,
                },
            };
        }
        public dynamic BuildOPDMedicalReport(OPD opd, Translation translation)
        {
            var trans_datas = translation.TranslationDatas.Where(e => !e.IsDeleted).ToList();

            var customer = opd?.Customer;
            var dob = customer.DateOfBirth?.ToString(Constant.DATE_FORMAT);
            var gender = new CustomerUtil(customer).GetGender();
            var clinic = opd?.Clinic;

            var oen = opd?.OPDOutpatientExaminationNote;
            var date_of_visit = opd.AdmittedDate.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND);

            var oen_datas = oen?.OPDOutpatientExaminationNoteDatas.Where(e => !e.IsDeleted).ToList();
            var icd_diagnosis = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENICDANS")?.Value;
            var icd_option = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENICDOPT")?.Value;
            var date_of_next_appointment = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENDORANS")?.Value;

            var physician = opd.PrimaryDoctor;
            if (opd.AuthorizedDoctorId != null)
                physician = null;
            var physician_name = physician?.Fullname;

            var date_now = DateTime.Now.ToString(Constant.DATE_FORMAT);


            var site = GetSite();
          
            var reason_for_transfer = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENRFT3ANS")?.Value;
            var contacted_staff = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENSBCANS")?.Value;

            var medical_escort = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENME0ANS")?.Value;
            var reasons_for_admission = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENCC0ANS")?.Value;
            var history = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENHPIANS")?.Value;
            string clinicCode = GetStringClinicCodeUsed(opd);
            List<DataClinicalFinding> clinical_examination_and_findings = hsClinicalExamination(oen) ? new ClinicalExaminationAndFindings(clinicCode, clinic?.Data, oen.Id, opd.IsTelehealth, oen.Version, unitOfWork).GetData() : new List<DataClinicalFinding>();
            var result_of_paraclinical_tests = getOPDPrincipalTest(oen);

            var treatment_and_procedures = getOPDTreatmentPlans(oen);
            var drugs_used = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENDU0ANS")?.Value;
            var patient_status_at_transfer = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENPSAANS")?.Value;
            var plan_of_care = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENRFUANS")?.Value;
            string clinical = "", transportation_method = "";

            var methods = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENMTUANS")?.Value;
            var patient_status = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENPS0ANS")?.Value;
            var escort = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENNTMANS")?.Value;

            List<DataClinicalFinding> clinical_examination_and_findingsTmp = new List<DataClinicalFinding>();
            List<DataClinicalFinding> clinical_ex_data = new List<DataClinicalFinding>();
            if (clinical_examination_and_findings != null && clinical_examination_and_findings.Count > 0)
            {
                clinical_examination_and_findingsTmp = clinical_examination_and_findings.GroupBy(x => x.Code).Select(x => x.FirstOrDefault()).ToList();

                foreach (var clin in clinical_examination_and_findingsTmp)
                {
                    var c = new DataClinicalFinding
                    {
                        ViName = clin.ViName,
                        EnName = clin.EnName,
                        Group = clin.Group,
                        Order = clin.Order,
                        Value = trans_datas?.FirstOrDefault(e => e.Code == clin.CodeOther)?.Value,
                        Code = clin.CodeOther
                    };
                    clinical_ex_data.Add(c);
                }
            }
            switch (translation.EnName.ToUpper())
            {
                case "MEDICAL REPORT":
                    clinical = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEKLSANS")?.Value;
                    break;
                case "REFERRAL LETTER":
                    clinical = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEASSESSMENTANS")?.Value;
                    transportation_method = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENTM1ANS")?.Value;
                    break;
                case "TRANSFER LETTER":
                    clinical = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEDHLSANS")?.Value;
                    transportation_method = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENTM0ANS")?.Value;
                    break;
                case "ILLNESS CERTIFICATE":
                    break;
            }
            return new
            {
                site?.Location,
                Site = site?.Name,
                site?.Province,
                Date = date_now,
                customer.PID,
                customer.Fullname,
                customer.Nationality,
                customer.Job,
                customer.WorkPlace,
                ClinicCode = clinic.Code,
                Gender = gender,
                DateOfBirth = dob,
                customer.Address,
                opd.IsTelehealth,
                DateOfVisit = date_of_visit,
                ReasonOfVisit = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENCC0ANS")?.Value,
                HistoryOfPresentIllness = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENHPIANS")?.Value,
                PrincipalTest = getOPDPrincipalTest(oen),
                Diagnosis = GetDiagnosisOPD(oen_datas, opd.Version, translation.EnName.ToUpper()),
                ICDDiagnosis = icd_diagnosis,
                ICDOption = icd_option,
                TreatmentPlans = getOPDTreatmentPlans(oen),
                RecommendtionAndFollowUp = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENRFUANS")?.Value,
                DateOfNextAppointment = date_of_next_appointment,
                Physician = physician_name,
                Copy = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENCOPYANS")?.Value,
                ReasonForTransfer = reason_for_transfer,
                ContactedStaff = contacted_staff,
                TransportationMethod = transportation_method,
                MedicalEscort = medical_escort,
                ReasonsForAdmission = reasons_for_admission,
                History = history,
                ClinicalExaminationAndFindings = clinical_examination_and_findingsTmp,
                ResultOfParaclinicalTests = result_of_paraclinical_tests,
                DefinitiveDiagnosis = GetDiagnosisOPD(oen_datas, opd.Version, translation.EnName.ToUpper()),
                TreatmentAndProcedures = oen.IsConsultation == true && !string.IsNullOrEmpty(treatment_and_procedures) ? string.Format("{0}{1}", "\n", treatment_and_procedures) : treatment_and_procedures,
                DrugsUsed = drugs_used,
                PatientStatusAtTransfer = patient_status_at_transfer,
                PlanOfCare = plan_of_care,
                Method = methods,
                PatientStatus = patient_status,
                Escort = escort,
                Translation = new
                {
                    site?.Location,
                    Site = site?.Name,
                    site?.Province,
                    Date = date_now,
                    customer.PID,
                    customer.Fullname,
                    Job = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEJOBANS")?.Value,
                    WorkPlace = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEWORKPLACEANS")?.Value,
                    Gender = trans_datas?.Where(x => x.Code.StartsWith("TRANSLATEGEN")).ToList().FirstOrDefault()?.Value,
                    DateOfBirth = dob,
                    Address = trans_datas?.Where(x => x.Code.StartsWith("TRANSLATEADD")).ToList().FirstOrDefault()?.Value,
                    opd.IsTelehealth,
                    DischargeDate = opd.DischargeDate?.ToString(Constant.DATE_FORMAT),
                    DateOfVisit = date_of_visit,
                    ReasonOfVisit = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEREASONFORVISITANS")?.Value,
                    HistoryOfPresentIllness = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEHISTORYOFPRESENTANS")?.Value,
                    PrincipalTest = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEPRINCIPALTESTANS")?.Value ?? trans_datas?.Where(x => x.Code.EndsWith("SUBRESULTANS")).ToList().FirstOrDefault()?.Value,
                    Diagnosis = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEPDIAGNOSISANS")?.Value,
                    ICDDiagnosis = icd_diagnosis,
                    ICDOption = icd_option,
                    TreatmentPlans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATETREANTMENTPLANANS")?.Value,
                    RecommendtionAndFollowUp = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATERECOMMENDATIONANDFOLLOWUPANS")?.Value,
                    DateOfNextAppointment = date_of_next_appointment,
                    Physician = physician_name,
                    PkntVersion = oen.Version,
                    Nationality = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATENATANS")?.Value,
                    ReasonForTransfer = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEREASONTRANSFERANS")?.Value,
                    ContactedStaff = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATENAMEMETHODCONTACTEDANS")?.Value,
                    TransportationMethod = trans_datas?.Where(x => x.Code.StartsWith("TRANSLATETRANSPORT")).ToList().FirstOrDefault()?.Value,
                    MedicalEscort = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATESENDERANS")?.Value,
                    ReasonsForAdmission = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATECHIEFCOMANS")?.Value,
                    History = trans_datas?.Where(x => x.Code.StartsWith("TRANSLATEHISTORY")).ToList().FirstOrDefault()?.Value,
                    //ClinicalExaminationAndFindings = clinical,
                    ResultOfParaclinicalTests = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATESUBRESULTANS")?.Value,
                    DefinitiveDiagnosis = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEPDIAGNOSISANS")?.Value,
                    TreatmentAndProcedures = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATETREATMENTANDPROCEDUREANS")?.Value,
                    DrugsUsed = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEDRUGUSEDANS")?.Value,
                    PatientStatusAtTransfer = trans_datas.FirstOrDefault(e => e.Code == "TRANSLATESTATUSPATIENTTRANSFER1ANS")?.Value,
                    PlanOfCare = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATECAREPLANANS")?.Value,
                    Method = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEMPTDRUGUSETREATMENTANS")?.Value,
                    PatientStatus = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATESATATUSREFERALANS")?.Value,
                    Escort = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEPERTRANSPORTANS")?.Value,
                    ClinicalExaminationAndFindings = translation.UpdatedAt <= new DateTime(2022, 11, 17, 18, 00, 00) ? trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEKLSANS")?.Value : Newtonsoft.Json.JsonConvert.SerializeObject(clinical_ex_data)
                }
            };
        }
        protected TransferInfoModel GetFirstIpd(IPD ipd)
        {
            var spec = ipd.Specialty;
            var current_doctor = ipd.PrimaryDoctor;

            var transfers = new IPDTransfer(ipd).GetListInfo();
            TransferInfoModel first_ipd = null;
            if (transfers.Count() > 0)
            {
                first_ipd = transfers.FirstOrDefault(e => e.CurrentType == "ED" || e.CurrentType == "IPD" && (string.IsNullOrEmpty(e.CurrentSpecialtyCode) || !e.CurrentSpecialtyCode.Contains("PTTT")));
            }
            else
            {
                first_ipd = new TransferInfoModel()
                {
                    CurrentRawDate = ipd.AdmittedDate,
                    CurrentSpecialty = new { spec?.ViName, spec?.EnName },
                    CurrentDoctor = new { current_doctor?.Username, current_doctor?.Fullname, current_doctor?.DisplayName },
                    CurrentDate = ipd.AdmittedDate.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                };
            }
            if (first_ipd == null)
            {
                first_ipd = new TransferInfoModel()
                {
                    CurrentRawDate = ipd.AdmittedDate,
                    CurrentSpecialty = new { spec?.ViName, spec?.EnName },
                    CurrentDoctor = new { current_doctor?.Username, current_doctor?.Fullname, current_doctor?.DisplayName },
                    CurrentDate = ipd.AdmittedDate.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                };
            }
            return first_ipd;
        }
        public dynamic BuildIPDMedicalReport(IPD ipd, Translation translation)
        {
            var trans_datas = translation.TranslationDatas.Where(e => !e.IsDeleted).ToList();

            var customer = ipd.Customer;
            List<string> address = new List<string>();
            if (!string.IsNullOrEmpty(customer?.MOHAddress))
                address.Add(customer?.MOHAddress);
            if (!string.IsNullOrEmpty(customer?.MOHDistrict))
                address.Add(customer?.MOHDistrict);
            if (!string.IsNullOrEmpty(customer?.MOHProvince))
                address.Add(customer?.MOHProvince);

            var medical_record = ipd.IPDMedicalRecord;
            var status = ipd.EDStatus;
            var first_ipd = GetFirstIpd(ipd);
            // var admission = ipd.AdmittedDate.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND);
            var admission = first_ipd.CurrentDate;

            var discharge = ipd.DischargeDate?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND);

            var part_2 = medical_record?.IPDMedicalRecordPart2;
            var part_2_datas = part_2?.IPDMedicalRecordPart2Datas.Where(e => !e.IsDeleted)?.ToList();

            var part_3 = medical_record?.IPDMedicalRecordPart3;
            var part_3_datas = part_3?.IPDMedicalRecordPart3Datas.Where(e => !e.IsDeleted)?.ToList();
            var clinical_evolution = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPEQTBLANS")?.Value;
            var result_of_paraclinical_tests = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPETTKQANS")?.Value;
            var diagnosis = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPECDBCANS")?.Value;
            var icd_diagnosis = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPEICDCANS")?.Value;
            var co_morbidities = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPECDKTANS")?.Value;
            var icd_co_morbidities = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPEICDPANS")?.Value;
            var treatments_and_procedures = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPEPPDTANS")?.Value;
            var significant_medications = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPETCDDANS")?.Value;
            var condition_at_discharge = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPETTNBANS")?.Value;
            var followup_care_plan = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPEHDTVANS")?.Value;

            var discharge_mr = ipd?.IPDDischargeMedicalReport;

            var physician_in_charge = discharge_mr?.PhysicianInCharge;
            var physician_in_charge_info = new { physician_in_charge?.Username, physician_in_charge?.Fullname, physician_in_charge?.Title, physician_in_charge?.DisplayName };
            var physician_in_charge_time = discharge_mr?.PhysicianInChargeTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND);


            var dept_head = discharge_mr?.DeptHead;
            var dept_head_info = new { dept_head?.Username, dept_head?.Fullname, dept_head?.Title, dept_head?.DisplayName };
            var dept_head_time = discharge_mr?.DeptHeadTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND);


            var director = discharge_mr?.Director;
            var director_info = new { director?.Username, director?.Fullname, director?.Title, director?.DisplayName };
            var director_time = discharge_mr?.DirectorTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND);
            var firstipd = GetFirstIpdInVisitTypeIPD(ipd);
            var site = GetSite();
            var medical_record_datas = medical_record.IPDMedicalRecordDatas.Where(e => !e.IsDeleted).ToList();
            string reason_for_transfer = medical_record_datas?.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDRFTANS")?.Value;
            var time_of_transfer = medical_record_datas?.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDMRPTCHVHANS")?.Value;
            var receiving_person = medical_record_datas?.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDNRHANS")?.Value;

            string followupCarePlanTrans = "", transportation_method = "", noteTrans = "", resultOfParaclinicalTestsTrans = "", treatmentsAndProceduresTrans = "",
               conditionAtDischargeTrans = "", chiefComplaintTrans = "", currentStatusTrans = "", transportationMethodTrans = "", addressTrans = "",
              escortPersontrans = "", escort_person = "";

            switch (translation.EnName.ToUpper())
            {
                case "MEDICAL REPORT":
                    followupCarePlanTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATECAREPLANANS")?.Value;
                    treatmentsAndProceduresTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATETREATMENTANDPROCEDUREANS")?.Value;
                    chiefComplaintTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEREASONANS")?.Value;
                    conditionAtDischargeTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATCURRENTPATIENTANS")?.Value;
                    resultOfParaclinicalTestsTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATESUBRESULTANS")?.Value;
                    currentStatusTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATCURRENTPATIENTANS")?.Value;
                    break;
                case "DISCHARGE MEDICAL REPORT":
                    followupCarePlanTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEFOLLOWUPCAREPLANANS")?.Value;
                    treatmentsAndProceduresTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATETREATMENTANDPROCEDUREANS")?.Value;
                    chiefComplaintTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEREASONANS")?.Value;
                    addressTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEADDANS")?.Value;
                    resultOfParaclinicalTestsTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATESUBRESULTANS")?.Value;
                    conditionAtDischargeTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATECONDITIONDISCHARGEANS")?.Value;
                    break;
                case "REFERRAL LETTER":
                    followupCarePlanTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEFOLLOWUPCAREPLANANS")?.Value;
                    resultOfParaclinicalTestsTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATESUBRESULTANS")?.Value;
                    treatmentsAndProceduresTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATETREATMENTANDPROCEDUREANS")?.Value;
                    conditionAtDischargeTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATESTATUSPATIENTTRANSFERANS")?.Value;
                    chiefComplaintTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATECHIEFCOMANS")?.Value;
                    transportationMethodTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATETRANSPORTANS")?.Value;
                    addressTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEADDFOOTERANS")?.Value;
                    escortPersontrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATESENDERANS")?.Value;
                    transportation_method = medical_record_datas?.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDTM0ANS")?.Value;
                    escort_person = medical_record_datas?.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDNATANS")?.Value;
                    break;
                case "DISCHARGE CERTIFICATE":
                    noteTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATENOTEANS")?.Value;
                    treatmentsAndProceduresTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATETREATMENTANDPROCEDUREANS")?.Value;
                    conditionAtDischargeTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATECONDITIONDISCHARGEANS")?.Value;
                    addressTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEADDANS")?.Value;

                    break;
                case "TRANSFER LETTER":
                    resultOfParaclinicalTestsTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATELABANDSUBRESULTANS")?.Value;
                    treatmentsAndProceduresTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEMPTDRUGUSETREATMENTANS")?.Value;
                    conditionAtDischargeTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATESATATUSREFERALANS")?.Value;
                    followupCarePlanTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATETREANTMENTPLANANS")?.Value;
                    transportationMethodTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATETRANSPORTFOOTERANS")?.Value;
                    escortPersontrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEPERTRANSPORTANS")?.Value;
                    transportation_method = medical_record_datas?.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDTM1ANS")?.Value;
                    escort_person = medical_record_datas?.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDNTMANS")?.Value;
                    break;
                case "INJURY CERTIFICATE":
                    treatmentsAndProceduresTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATETREATMENTANS")?.Value;
                    chiefComplaintTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEREASONANS")?.Value;
                    currentStatusTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATESTATUSINJURYDISCHARGEANS")?.Value;

                    break;
                case "SURGERY CERTIFICATE":
                    addressTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEADDANS")?.Value;
                    break;
            }

            var treatment_procedures = GetTreatmentsLast(ipd.Id, part_3.Id, part_3_datas);
            var medical_sign = GetMedicalSigByMedicalRecord(ipd, part_2);
            string medicalSign = "";
            if (medical_sign?.Count > 0)
            {

                foreach (var m in medical_sign)
                {
                    var value = m.Value ?? "";
                    medicalSign += m.ViName + ": " + value + "\n";
                }
                for (int i = 0; i < 20; i++)
                {
                    if (medicalSign.Contains(i + ". "))
                    {
                        medicalSign = medicalSign.Replace(i + ". ", "");
                    }
                }
                if (medicalSign.Contains("Các bộ phận khác"))
                {
                    medicalSign = medicalSign.Replace("Các bộ phận khác", "Khác");
                }
            }

            return new
            {
                site?.Location,
                Site = site?.Name,
                site?.Province,
                Specialty = ipd.Specialty?.ViName,
                customer.PID,
                customer.Fullname,
                Gender = new CustomerUtil(customer).GetGender(),
                DateOfBirth = customer.DateOfBirth?.ToString(Constant.DATE_FORMAT),
                Address = string.Join(", ", address),
                customer.Nationality,
                customer.AgeFormated,
                Ethnic = customer.MOHEthnic,
                ipd.HealthInsuranceNumber,
                ExpireHealthInsuranceDate = ipd.ExpireHealthInsuranceDate?.ToString(Constant.DATE_FORMAT),
                Admission = admission,
                Discharge = discharge,
                ChiefComplaint = part_2_datas?.FirstOrDefault(e => e.Code == "IPDMRPTLDVVANS")?.Value,
                ClinicalEvolution = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPEQTBLANS")?.Value,
                ResultOfParaclinicalTests = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPETTKQANS")?.Value,
                Diagnosis = (part_3_datas != null && part_3_datas.Count > 0) ? GetDiagnosisIPD(part_3_datas, ipd.Version, translation.EnName.ToUpper()) : "",
                ICDDiagnosis = icd_diagnosis,
                CoMorbidities = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPECDKTANS")?.Value,
                ICDCoMorbidities = icd_co_morbidities,
                TreatmentsAndProcedures = treatment_procedures, /*+ "\n" + significant_medications*/
                SignificantMedications = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPETCDDANS")?.Value,
                ConditionAtDischarge = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPETTNBANS")?.Value,
                FollowUpCarePlan = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPEHDTVANS")?.Value,
                Date = discharge,
                PhysicianInCharge = physician_in_charge_info,
                PhysicianInChargeTime = physician_in_charge_time,
                DeptHead = dept_head_info,
                DeptHeadTime = dept_head_time,
                Director = director_info,
                DirectorTime = director_time,
                DoctorRecommendations = part_3_datas?.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDMRPEHDTVANS")?.Value,
                DrugsUsed = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPETCDDANS")?.Value,
                HospitalizedStatus = part_2_datas?.FirstOrDefault(e => e.Code == "IPDMRPTTTBAANS")?.Value,
                DischargeStatus = part_3_datas?.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDMRPETTNBANS")?.Value,
                ReasonForTransfer = reason_for_transfer,
                TimeOfTransfer = time_of_transfer,
                ReceivingPerson = receiving_person,
                TransportationMethod = transportation_method,
                EscortPerson = escort_person,
                customer.MOHJob,
                customer.WorkPlace,
                IdCard = customer.IdentificationCard,
                customer.IssueDate,
                customer.IssuePlace,
                PreoperativeDiagnosisTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEPREDIAGNOSISANS")?.Value,
                PostoperativeDiagnosisTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEDIAGNOSISAFTERSERGERYANS")?.Value,
                ProcedurePerformedTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEPPPTANS")?.Value,
                MethodOfAnesthesiaTrans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEPPVCANS")?.Value,
                MedicalSign = medicalSign,
                Method = treatment_procedures + "\n" + significant_medications,
                Translation = new
                {
                    site?.Location,
                    Site = site?.Name,
                    site?.Province,
                    Specialty = ipd.Specialty?.ViName,
                    customer.PID,
                    customer.Fullname,
                    Gender = trans_datas?.Where(x => x.Code.StartsWith("TRANSLATEGEN")).ToList().FirstOrDefault()?.Value,
                    DateOfBirth = customer.DateOfBirth?.ToString(Constant.DATE_FORMAT),
                    Nationality = trans_datas?.FirstOrDefault(e => !e.IsDeleted && e.Code == "TRANSLATENATANS")?.Value,
                    Address = trans_datas?.Where(x => x.Code.StartsWith("TRANSLATEADD")).ToList().FirstOrDefault()?.Value,
                    DateOfVisit = ipd.AdmittedDate.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                    Admission = admission,
                    Discharge = discharge,
                    ChiefComplaint = chiefComplaintTrans,
                    ClinicalEvolution = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATECLINEVOLUANS")?.Value,
                    ResultOfParaclinicalTests = resultOfParaclinicalTestsTrans,
                    Diagnosis = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEPDIAGNOSISANS")?.Value,
                    ICDDiagnosis = icd_diagnosis,
                    CoMorbidities = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATECOMORBIDITIESANS")?.Value,
                    ICDCoMorbidities = icd_co_morbidities,
                    TreatmentsAndProcedures = treatmentsAndProceduresTrans,
                    SignificantMedications = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEDRUGUSEDANS")?.Value,
                    ConditionAtDischarge = conditionAtDischargeTrans,
                    FollowUpCarePlan = followupCarePlanTrans,
                    Date = discharge,
                    PhysicianInCharge = physician_in_charge_info,
                    PhysicianInChargeTime = physician_in_charge_time,
                    DeptHead = dept_head_info,
                    DeptHeadTime = dept_head_time,
                    Director = director_info,
                    DirectorTime = director_time,
                    CurrentStatus = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATECURSTATUSANS")?.Value,
                    DoctorRecommendations = noteTrans,
                    DrugsUsed = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEDRUGUSEDANS")?.Value,
                    HospitalizedStatus = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATESTATUSADMITTEDANS")?.Value,
                    DischargeStatus = currentStatusTrans,
                    MOHJob = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEJOBANS")?.Value,
                    ReasonForTransfer = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEREASONTRANSFERANS")?.Value,
                    ReceivingPerson = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATENAMEMETHODCONTACTEDANS")?.Value,
                    TransportationMethod = transportationMethodTrans,
                    EscortPerson = escortPersontrans,
                    WorkPlace = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEWORKPLACEANS")?.Value,
                    MedicalSign = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEDHLSANS")?.Value,
                    Method = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEMPTDRUGUSETREATMENTANS")?.Value,

                },
                AdmittedDateFirstIpd = firstipd.CurrentDate
            };
        }
        public dynamic BuildEOCMedicalReport(EOC opd, Translation translation)
        {
            var trans_datas = translation.TranslationDatas.Where(e => !e.IsDeleted);

            var customer = opd.Customer;
            var dob = customer.DateOfBirth?.ToString(Constant.DATE_FORMAT);
            var gender = new CustomerUtil(customer).GetGender();

            var oen = GetOutpatientExaminationNote(opd.Id);
            var date_of_visit = opd.AdmittedDate.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND);

            var oen_datas = GetFormData(opd.Id, oen.Id, "OPDOEN");
            var icd_diagnosis = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENICDANS")?.Value;
            var icd_option = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENICDOPT")?.Value;
            var date_of_next_appointment = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENDORANS")?.Value;

            var physician = opd.PrimaryDoctor;

            var physician_name = physician?.Fullname;

            var date_now = DateTime.Now.ToString(Constant.DATE_FORMAT);

            var site = GetSite();
            string kls = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEKLSANS")?.Value;
            var reason_for_transfer = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENRFT3ANS")?.Value;
            var contacted_staff = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENSBCANS")?.Value;
            string transportation_method = "";
            switch (translation.EnName.ToUpper())
            {
                case "REFERRAL LETTER":
                    transportation_method = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENTM1ANS")?.Value;
                    break;
                case "TRANSFER LETTER":
                    transportation_method = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENTM0ANS")?.Value;
                    break;
            }
            var medical_escort = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENME0ANS")?.Value;
            var reasons_for_admission = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENCC0ANS")?.Value;
            var history = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENHPIANS")?.Value;
            var result_of_paraclinical_tests = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENPT0ANS")?.Value;
            var treatment_and_procedures = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENTP0ANS")?.Value;
            var drugs_used = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENDU0ANS")?.Value;
            var patient_status_at_transfer = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENPSAANS")?.Value;
            var plan_of_care = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENRFUANS")?.Value;
            var methods = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENMTUANS")?.Value;
            var patient_status = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENPS0ANS")?.Value;
            var treatment_plans = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENTP0ANS")?.Value;
            var escort = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENNTMANS")?.Value;

            var clinical_examination_and_findings = GetDataEOCOutpatientExaminationNotesVerson2(oen_datas, Constant.EOC_DATA_FILL_OEN_MEDICAL_REPORT);


            List<DataClinicalFinding> clinical_examination_and_findingsTmp = new List<DataClinicalFinding>();
            List<DataClinicalFinding> clinical_ex_data = new List<DataClinicalFinding>();
            if (clinical_examination_and_findings != null && clinical_examination_and_findings.Count > 0)
            {
                clinical_examination_and_findingsTmp = clinical_examination_and_findings.GroupBy(x => x.Code).Select(x => x.FirstOrDefault()).ToList();

                foreach (var clin in clinical_examination_and_findingsTmp)
                {
                    var c = new DataClinicalFinding
                    {
                        ViName = clin.ViName,
                        EnName = clin.EnName,
                        Group = clin.Group,
                        Order = clin.Order,
                        Value = trans_datas?.FirstOrDefault(e => e.Code == clin.CodeOther)?.Value,
                        Code = clin.CodeOther
                    };
                    clinical_ex_data.Add(c);
                }
            }
            return new
            {
                customer.Nationality,
                site?.Location,
                Site = site?.Name,
                site?.Province,
                Date = date_now,
                customer.PID,
                customer.Fullname,
                ClinicCode = "Normal",
                Gender = gender,
                DateOfBirth = dob,
                customer.Address,
                IsEoc = true,
                DateOfVisit = date_of_visit,
                ReasonOfVisit = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENCC0ANS")?.Value,
                HistoryOfPresentIllness = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENHPIANS")?.Value,
                ClinicalExaminationAndFindings = clinical_examination_and_findingsTmp,
                PrincipalTest = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENPT0ANS")?.Value,
                Diagnosis = GetDiagnosisEOC(oen_datas, opd.Version, translation.EnName.ToUpper()),
                ICDDiagnosis = icd_diagnosis,
                ICDOption = icd_option,
                TreatmentPlans = treatment_plans,
                RecommendtionAndFollowUp = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENRFUANS")?.Value,
                DateOfNextAppointment = date_of_next_appointment,
                Physician = physician_name,
                Copy = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENCOPYANS")?.Value,
                ReasonForTransfer = reason_for_transfer,
                ContactedStaff = contacted_staff,
                TransportationMethod = transportation_method,
                MedicalEscort = medical_escort,
                ReasonsForAdmission = reasons_for_admission,
                History = history,
                // DataOenVersion2 = GetDataEOCOutpatientExaminationNotesVerson2(oen_datas, Constant.EOC_DATA_FILL_OEN_MEDICAL_REPORT),
                oen.Version,
                VisitVersion = opd.Version,
                ResultOfParaclinicalTests = result_of_paraclinical_tests,
                TreatmentAndProcedures = treatment_and_procedures,
                DrugsUsed = drugs_used,
                PatientStatusAtTransfer = patient_status_at_transfer,
                PlanOfCare = plan_of_care,
                customer.Job,
                customer.WorkPlace,
                Method = methods,
                PatientStatus = patient_status,
                Escort = escort,
                Translation = new
                {
                    site?.Location,
                    Site = site?.Name,
                    site?.Province,
                    Date = date_now,
                    customer.PID,
                    customer.Fullname,
                    Gender = trans_datas?.Where(x => x.Code.StartsWith("TRANSLATEGEN")).ToList().FirstOrDefault()?.Value,
                    DateOfBirth = dob,
                    Address = trans_datas?.Where(x => x.Code.StartsWith("TRANSLATEADD")).ToList().FirstOrDefault()?.Value,
                    IsEoc = true,
                    DateOfVisit = date_of_visit,
                    ReasonOfVisit = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEREASONFORVISITANS")?.Value,
                    HistoryOfPresentIllness = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEHISTORYOFPRESENTANS")?.Value,
                    ClinicalExaminationAndFindings = translation.UpdatedAt <= new DateTime(2022, 11, 17, 18, 00, 00) ? trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEKLSANS")?.Value : Newtonsoft.Json.JsonConvert.SerializeObject(clinical_ex_data),
                    PrincipalTest = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEPRINCIPALTESTANS")?.Value,
                    Diagnosis = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEPDIAGNOSISANS")?.Value,
                    ICDDiagnosis = icd_diagnosis,
                    ICDOption = icd_option,
                    TreatmentPlans = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATETREANTMENTPLANANS")?.Value,
                    RecommendtionAndFollowUp = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATERECOMMENDATIONANDFOLLOWUPANS")?.Value,
                    DateOfNextAppointment = date_of_next_appointment,
                    Physician = physician_name,
                    DataOenVersion2 = kls,
                    Nationality = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATENATANS")?.Value,
                    ReasonForTransfer = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEREASONTRANSFERANS")?.Value,
                    ContactedStaff = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATENAMEMETHODCONTACTEDANS")?.Value,
                    TransportationMethod = trans_datas?.Where(x => x.Code.StartsWith("TRANSLATETRANSPORT")).ToList().FirstOrDefault()?.Value,
                    MedicalEscort = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATESENDERANS")?.Value,
                    ReasonsForAdmission = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATECHIEFCOMANS")?.Value,
                    ResultOfParaclinicalTests = trans_datas?.Where(x => x.Code.EndsWith("SUBRESULTANS")).ToList().FirstOrDefault()?.Value,
                    TreatmentAndProcedures = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATETREATMENTANDPROCEDUREANS")?.Value,
                    DrugsUsed = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEDRUGUSEDANS")?.Value,
                    PatientStatusAtTransfer = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATESTATUSPATIENTTRANSFER1ANS")?.Value,
                    PlanOfCare = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATECAREPLANANS")?.Value,
                    Job = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEJOBANS")?.Value,
                    WorkPlace = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEWORKPLACEANS")?.Value,
                    Method = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEMPTDRUGUSETREATMENTANS")?.Value,
                    PatientStatus = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATESATATUSREFERALANS")?.Value,
                    Escort = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEPERTRANSPORTANS")?.Value,
                    History = trans_datas?.FirstOrDefault(e => e.Code == "TRANSLATEHISTORYANS")?.Value,
                }
            };
        }
        private dynamic GetEDMedicalReport(Translation translation,string form,List<MasterData> masterDatas)
        {
            var visit_id = (Guid)translation.VisitId;
            var ed = unitOfWork.EDRepository.GetById(visit_id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            List<dynamic> response = new List<dynamic>();

            var etr = ed.EmergencyTriageRecord;
            var chief_complain = etr.EmergencyTriageRecordDatas?.FirstOrDefault(e => !e.IsDeleted && e.Code == "ETRCC0ANS")?.Value;
            response.Add(new { Code = "EDTRANSCC0ANS", Value = chief_complain });

            var emer_record = ed.EmergencyRecord;
            var emer_record_datas = emer_record.EmergencyRecordDatas.Where(e => !e.IsDeleted).ToList();
            var history = emer_record_datas?.FirstOrDefault(e => !e.IsDeleted && e.Code == "ER0HISANS")?.Value ?? "";
            var assessment = new EmergencyRecordAssessment(emer_record.Id).GetList();
            response.Add(new { Code = "EDTRANSHISANS", Value = history });
            response.Add(new { Code = "EDTRANSASSANS", Value = assessment });

            var discharge_info = ed.DischargeInformation;
            var discharge_info_datas = discharge_info.DischargeInformationDatas.Where(e => !e.IsDeleted).ToList();
            var rop_tests = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0RPTANS2")?.Value;
         
            var treatment_procedures = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0TAPANS")?.Value;
            var current_status = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0CS0ANS")?.Value;
            var followup_care_plan = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0FCPANS")?.Value;
            var doctor_recommendations = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0DR0ANS")?.Value;
            var significan_medications = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0SM0ANS2")?.Value;
            var customer = ed.Customer;
            var job = customer.Job;
            var gender = new CustomerUtil(customer).GetGender();
            var address = customer.Address;
            var principal_tests = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0RPTANS2")?.Value;
            var methods = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0TAPANS")?.Value;
            var patient_status = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0CS0ANS")?.Value;
            var co_morbidities = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0DIAOPT2")?.Value;

            var clinical_examination_and_findings = new EmergencyRecordAssessment((Guid)ed.EmergencyRecordId).GetList();
            string clinical = "";
            var reason_for_transfer = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0RFTANS")?.Value;
            var receiving_person = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0NRHANS")?.Value;
            string transportationmethodTrans = "";
            string escortPersonTrans = "";
                switch (translation.EnName.ToUpper())
                {
                    case "MEDICAL REPORT":
                        transportationmethodTrans = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0TM0ANS")?.Value;
                        escortPersonTrans = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0NATANS")?.Value;
                        break;
                    case "DISCHARGE MEDICAL REPORT":
                        transportationmethodTrans = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0TM0ANS")?.Value;
                        escortPersonTrans = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0NATANS")?.Value;
                        break;
                    case "DISCHARGE CERTIFICATE":
                        transportationmethodTrans = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0TM0ANS")?.Value;
                        escortPersonTrans = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0NATANS")?.Value;
                        break;
                    case "EMERGENCY CONFIRMATION":
                        transportationmethodTrans = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0TM0ANS")?.Value;
                        escortPersonTrans = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0NATANS")?.Value;
                        break;
                    case "INJURY CERTIFICATE":
                        transportationmethodTrans = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0TM0ANS")?.Value;
                        escortPersonTrans = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0NATANS")?.Value;
                        var diagnosisOptions = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0DIAOPT2")?.Value ?? "";
                        break;
                    case "TRANSFER LETTER":
                        transportationmethodTrans = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0TM1ANS")?.Value;
                        escortPersonTrans = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0NTMANS")?.Value;
                        break;
                    case "REFERRAL LETTER":
                        transportationmethodTrans = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0TM0ANS")?.Value;
                        escortPersonTrans = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0NATANS")?.Value;
                        break;
                    default:
                        transportationmethodTrans = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0TM0ANS")?.Value;
                        escortPersonTrans = discharge_info_datas?.FirstOrDefault(e => e.Code == "DI0NATANS")?.Value;
                        break;
                }

            var trans_data = translation?.TranslationDatas;
            List<DataClinicalFinding> clinical_examination_and_findingsTmp = new List<DataClinicalFinding>();
            if (clinical_examination_and_findings != null && clinical_examination_and_findings.Count > 0)
            {
                clinical_examination_and_findingsTmp = clinical_examination_and_findings.GroupBy(x => x.Code).Select(x => x.FirstOrDefault()).ToList();
                if (translation.UpdatedAt <= new DateTime(2022, 11, 17, 18, 00, 00))
                {

                    List<ConvertData> dt = new List<ConvertData>();
                    foreach (var c in clinical_examination_and_findingsTmp)
                    {
                        var cd = new ConvertData
                        {
                            ViName = c.ViName,
                            Value = "",
                            EnName = c.EnName,
                            Code = c.Code,
                        };
                        dt.Add(cd);
                    };
                    var datakls = trans_data.FirstOrDefault(x => x.Code == "TRANSLATEKLSANS");
                    if (datakls == null)
                    {
                        TranslationData transDt = new TranslationData();
                        transDt.TranslationId = translation.Id;
                        transDt.Code = "TRANSLATEKLSANS";
                        transDt.Value = Newtonsoft.Json.JsonConvert.SerializeObject(dt);
                        unitOfWork.TranslationDataRepository.Add(transDt);
                        unitOfWork.Commit();
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(datakls?.Value))
                        {
                            datakls.Value = Newtonsoft.Json.JsonConvert.SerializeObject(dt);
                            unitOfWork.TranslationDataRepository.Update(datakls);
                            unitOfWork.Commit();
                        }
                        else
                        {
                            response.Add(new { Code = "TRANSLATEKLSANS", Value = clinical_examination_and_findingsTmp });
                        }
                    }
                }
                else
                {
                    foreach (var clin in clinical_examination_and_findingsTmp)
                    {
                        if (!string.IsNullOrEmpty(clin.CodeOther))
                        {
                            response.Add(new { Code = clin.CodeOther, Value = clin?.Value });
                        }
                        clinical = $"+ {clin.ViName}:  {clin.Value} + \n";
                    }
                    clinical = clinical.Substring(0, clinical.LastIndexOf("\n") - 1);
                }
            }
            var drug_main = discharge_info_datas.FirstOrDefault(e => e.Code == "DI0SM0ANS2")?.Value;
            response.Add(new { Code = "TRANSLATERESULTPRATESTANS", Value = rop_tests });
            response.Add(new { Code = "TRANSLATEPDIAGNOSISANS", Value = GetDiagnosisED(discharge_info_datas, emer_record_datas, ed.Version, translation.EnName.ToUpper()) });
            response.Add(new { Code = "TRANSLATETREATMENTANDPROCEDUREANS", Value = treatment_procedures });
            response.Add(new { Code = "TRANSLATEDOCTORRECOMENDATIONANS", Value = doctor_recommendations });
            response.Add(new { Code = "TRANSLATEDRUGUSEDANS", Value = significan_medications });
            response.Add(new { Code = "TRANSLATEREASONANS", Value = chief_complain });
            response.Add(new { Code = "TRANSLATECURSTATUSANS", Value = current_status });
            response.Add(new { Code = "TRANSLATECAREPLANANS", Value = followup_care_plan });
            response.Add(new { Code = "TRANSLATEGENANS", Value = gender });
            response.Add(new { Code = "TRANSLATENATANS", Value = customer.Nationality });
            response.Add(new { Code = "TRANSLATEJOBANS", Value = job });
            response.Add(new { Code = "TRANSLATEADDANS", Value = address });
            response.Add(new { Code = "TRANSLATENOTEANS", Value = doctor_recommendations });
            response.Add(new { Code = "TRANSLATESTATUSPATIENTTRANSFER1ANS", Value = current_status });
            response.Add(new { Code = "TRANSLATELABANDSUBRESULTANS", Value = principal_tests });
            response.Add(new { Code = "TRANSLATEMPTDRUGUSETREATMENTANS", Value = drug_main != null && drug_main != "" ? methods + ", " + drug_main : methods, });
            response.Add(new { Code = "TRANSLATESATATUSREFERALANS", Value = patient_status });
            response.Add(new { Code = "TRANSLATETREANTMENTPLANANS", Value = followup_care_plan });
            response.Add(new { Code = "TRANSLATEWORKPLACEANS", Value = customer.WorkPlace });
            response.Add(new { Code = "TRANSLATETREATMENTANS", Value = treatment_procedures });
            response.Add(new { Code = "TRANSLATESTATUSINJURYDISCHARGEANS", Value = current_status });
            response.Add(new { Code = "TRANSLATESUBRESULTANS", Value = rop_tests });
            response.Add(new { Code = "TRANSLATEPERTRANSPORTANS", Value = escortPersonTrans });
            response.Add(new { Code = "TRANSLATEDIAGNOSISBEFOREMERANS", Value = GetDiagnosisED(discharge_info_datas, emer_record_datas, ed.Version, translation.EnName.ToUpper()) });
            response.Add(new { Code = "TRANSLATCURRENTPATIENTANS", Value = current_status });
            response.Add(new { Code = "TRANSLATECOMORBIDITIESANS", Value = co_morbidities });
            response.Add(new { Code = "TRANSLATEADDFOOTERANS", Value = address });
            response.Add(new { Code = "TRANSLATEGENVIANS", Value = gender });
            response.Add(new { Code = "TRANSLATETRANSPORTFOOTERANS", Value = transportationmethodTrans });
            response.Add(new { Code = "TRANSLATEREASONTRANSFERANS", Value = reason_for_transfer });
            response.Add(new { Code = "TRANSLATENAMEMETHODCONTACTEDANS", Value = receiving_person });
            response.Add(new { Code = "TRANSLATETRANSPORTANS", Value = transportationmethodTrans });
            response.Add(new { Code = "TRANSLATESENDERANS", Value = escortPersonTrans });
            response.Add(new { Code = "TRANSLATETCCCDHANS", Value = clinical });
            response.Add(new { Code = "TRANSLATESTATUSADMITTEDANS", Value = clinical });
            response.Add(new { Code = "TRANSLATEASSESSMENTANS", Value = clinical });
            response.Add(new { Code = "TRANSLATECLINEVOLUANS", Value = history });
            response.Add(new { Code = "TRANSLATEGENGIOIANS", Value = gender });
            response.Add(new { Code = "TRANSLATEHISTORYOFPRESENTANS", Value = history });
            response.Add(new { Code = "TRANSLATEHISTORYANS", Value = history });
            response.Add(new { Code = "TRANSLATEPDIAGNOSIS1ANS", Value = GetDiagnosisED(discharge_info_datas, emer_record_datas, ed.Version, translation.EnName.ToUpper()) });
            response.Add(new { Code = "TRANSLATETREATMENT1ANS", Value = treatment_procedures });

            var data = masterDatas.Select(md => new
            {
                md.Code,
                md.Group,
                md.Order,
                md.DataType,
                md.Level,
                md.IsReadOnly,
                md.Note,
                md.Data,
                md.Clinic,
                md.DefaultValue,
                Value = "",
                Id = "",
                md.CreatedAt
            })
             .OrderBy(md => md.CreatedAt)
             .ToList().Join(response, md => md.Code, res => res.Code, (md, res) => new
             {

                 Code = md.Code,
                 Value = res.Value,

             }).ToList<dynamic>();
            return data;
        }
        private dynamic GetOPDMedicalReport(Translation translation, string form, List<MasterData> masterDatas)
        {
           
            Guid visitId = (Guid)translation.VisitId;
            var opd = unitOfWork.OPDRepository.FirstOrDefault(e => e.Id == visitId);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            List<dynamic> response = new List<dynamic>();

            var customer = opd.Customer;
            var gender = ConvertGender((int)customer.Gender, translation.FromLanguage);
            var address = customer.Address;
            var nationality = customer.Nationality;
            response.Add(new { Code = "TRANSLATENATANS", Value = nationality });
            response.Add(new { Code = "TRANSLATEGENANS", Value = gender });
            response.Add(new { Code = "TRANSLATEADDANS", Value = address });

            var clinic = opd?.Clinic;
            var oen = opd?.OPDOutpatientExaminationNote;
            var oen_datas = oen?.OPDOutpatientExaminationNoteDatas.Where(e => !e.IsDeleted).ToList();
            var reason_for_visit = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENCC0ANS")?.Value;
            var hisory_of_present_illness = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENHPIANS")?.Value;

            string clinicCode = GetStringClinicCodeUsed(opd);
            var principal_tests = getOPDPrincipalTest(oen);
            var treatment_plans = getOPDTreatmentPlans(oen);
            var recommendation_and_follow_up = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENRFUANS")?.Value;
            var reasons_for_admission = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENCC0ANS")?.Value;
            var result_of_paraclinical_tests = getOPDPrincipalTest(oen);
            var treatment_and_procedures = getOPDTreatmentPlans(oen);
            var drugs_used = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENDU0ANS")?.Value;
            var plan_of_care = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENRFUANS")?.Value;
            var patient_status_at_transfer = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENPSAANS")?.Value;
            var job = customer.Job;
            var workPlace = customer.WorkPlace;
            var methods = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENMTUANS")?.Value;
            var patient_status = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENPS0ANS")?.Value;
            var escort = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENNTMANS")?.Value;
            var history = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENHPIANS")?.Value;
            var clinical_examination_and_findings = hsClinicalExamination(oen) ? new ClinicalExaminationAndFindings(clinicCode, clinic?.Data, oen.Id, opd.IsTelehealth, oen.Version, unitOfWork).GetData() : null;
            var reason_for_transfer1 = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENRFT3ANS")?.Value;
            var contacted_staff = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENSBCANS")?.Value;
            var medical_escort = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENME0ANS")?.Value;
            string transportation_method = "";

            string clinical = "";

            var trans_data = translation?.TranslationDatas;
            List<DataClinicalFinding> clinical_examination_and_findingsTmp = new List<DataClinicalFinding>();
            if (clinical_examination_and_findings != null && clinical_examination_and_findings.Count > 0)
            {
                clinical_examination_and_findingsTmp = clinical_examination_and_findings.GroupBy(x => x.Code).Select(x => x.FirstOrDefault()).ToList();
                if (translation.UpdatedAt <= new DateTime(2022, 11, 17, 18, 00, 00))
                {
                    List<ConvertData> dt = new List<ConvertData>();
                    foreach (var c in clinical_examination_and_findingsTmp)
                    {
                        var cd = new ConvertData
                        {
                            ViName = c.ViName,
                            Value = "",
                            EnName = c.EnName,
                            Code = c.Code,
                        };
                        dt.Add(cd);
                    };
                    var datakls = trans_data.FirstOrDefault(x => x.Code == "TRANSLATEKLSANS");
                    if (datakls == null)
                    {
                        TranslationData transDt = new TranslationData();
                        transDt.TranslationId = translation.Id;
                        transDt.Code = "TRANSLATEKLSANS";
                        transDt.Value = Newtonsoft.Json.JsonConvert.SerializeObject(dt);
                        unitOfWork.TranslationDataRepository.Add(transDt);
                        unitOfWork.Commit();
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(datakls?.Value))
                        {
                            datakls.Value = Newtonsoft.Json.JsonConvert.SerializeObject(dt);
                            unitOfWork.TranslationDataRepository.Update(datakls);
                            unitOfWork.Commit();
                        }
                        else
                        {
                            response.Add(new { Code = "TRANSLATEKLSANS", Value = clinical_examination_and_findingsTmp });
                        }
                    }
                }
                else
                {
                    foreach (var clin in clinical_examination_and_findingsTmp)
                    {
                        if (!string.IsNullOrEmpty(clin.CodeOther))
                        {
                            response.Add(new { Code = clin.CodeOther, Value = clin?.Value });
                        }
                        clinical = $"+ {clin.ViName}:  {clin.Value} + \n";
                    }
                    clinical = clinical.Substring(0, clinical.LastIndexOf("\n") - 1);
                }
            }
         
            switch (translation.EnName.ToUpper())
            {
                case "REFERRAL LETTER":
                    transportation_method = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENTM1ANS")?.Value;
                    break;
                case "TRANSFER LETTER":
                    transportation_method = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENTM0ANS")?.Value;
                    break;
            }

            response.Add(new { Code = "TRANSLATEREASONFORVISITANS", Value = reason_for_visit });
            response.Add(new { Code = "TRANSLATEHISTORYOFPRESENTANS", Value = hisory_of_present_illness });
            response.Add(new { Code = "TRANSLATECLINTEXAMANS", Value = clinical_examination_and_findings });
            response.Add(new { Code = "TRANSLATEPRINTESTANS", Value = principal_tests });
            response.Add(new { Code = "TRANSLATEPDIAGNOSISANS", Value = GetDiagnosisOPD(oen_datas,opd.Version,translation.EnName.ToUpper()) });
            response.Add(new { Code = "TRANSLATETREANTMENTPLANANS", Value = treatment_plans });
            response.Add(new { Code = "TRANSLATERECOMMENDANS", Value = recommendation_and_follow_up });
            response.Add(new { Code = "TRANSLATECHIEFCOMANS", Value = reasons_for_admission });
            response.Add(new { Code = "TRANSLATERESULTPRATESTANS", Value = result_of_paraclinical_tests });
            response.Add(new { Code = "TRANSLATETREATMENTANDPROCEDUREANS", Value = oen?.IsConsultation == true && !string.IsNullOrEmpty(treatment_and_procedures) ? string.Format("{0}{1}", "\n", treatment_and_procedures) : treatment_and_procedures, });
            response.Add(new { Code = "TRANSLATEDRUGUSEDANS", Value = drugs_used });
            response.Add(new { Code = "TRANSLATECAREPLANANS", Value = plan_of_care });
            response.Add(new { Code = "TRANSLATECURRENTPATIENTANS", Value = patient_status_at_transfer });
            response.Add(new { Code = "TRANSLATEJOBANS", Value = job });
            response.Add(new { Code = "TRANSLATEWORKPLACEANS", Value = workPlace });
            response.Add(new { Code = "TRANSLATECURRENTSATATUSANS", Value = patient_status });
            response.Add(new { Code = "TRANSLATETREATANDDRUGANS", Value = methods });
            response.Add(new { Code = "TRANSLATETRANSPORTANS", Value = transportation_method });
            response.Add(new { Code = "TRANSLATESATATUSREFERALANS", Value = patient_status });
            response.Add(new { Code = "TRANSLATEMPTDRUGUSETREATMENTANS", Value = methods });
            response.Add(new { Code = "TRANSLATELABANDSUBRESULTANS", Value = principal_tests });
            response.Add(new { Code = "TRANSLATESTATUSPATIENTTRANSFER1ANS", Value = patient_status_at_transfer });
            response.Add(new { Code = "TRANSLATECLINEVOLUANS", Value = history });
            response.Add(new { Code = "TRANSLATESUBRESULTANS", Value = principal_tests });
            response.Add(new { Code = "TRANSLATERECOMMENDATIONANDFOLLOWUPANS", Value = recommendation_and_follow_up });
            response.Add(new { Code = "TRANSLATEREASONTRANSFERANS", Value = reason_for_transfer1 });
            response.Add(new { Code = "TRANSLATENAMEMETHODCONTACTEDANS", Value = contacted_staff });
            response.Add(new { Code = "TRANSLATETRANSPORTFOOTERANS", Value = transportation_method });
            response.Add(new { Code = "TRANSLATEPERTRANSPORTANS", Value = escort });
            response.Add(new { Code = "TRANSLATESENDERANS", Value = medical_escort });
            response.Add(new { Code = "TRANSLATEGENVIANS", Value = gender });
            response.Add(new { Code = "TRANSLATEDHLSANS", Value = clinical });
            response.Add(new { Code = "TRANSLATEHISTORYANS", Value = history });
            response.Add(new { Code = "TRANSLATEPRINCIPALTESTANS", Value = result_of_paraclinical_tests });
            //  response.Add(new { Code = "TRANSLATEPRINCIPALTESTANS", Value = result_of_paraclinical_tests });

            var data = masterDatas.Select(md => new
            {
                md.Code,
                md.Group,
                md.Order,
                md.DataType,
                md.Level,
                md.IsReadOnly,
                md.Note,
                md.Data,
                md.Clinic,
                md.DefaultValue,
                Value = "",
                Id = "",
                md.CreatedAt
            })
            .OrderBy(md => md.CreatedAt)
            .ToList().Join(response, md => md.Code, res => res.Code, (md, res) => new
            {

                Code = md.Code,
                Value = res.Value,

            }).ToList<dynamic>();
            return data;
        }
        private List<MappingDatasQuery> GetDatasFromOutpatientExaminationNoteData(OPDOutpatientExaminationNote form)
        {
            var datas = form.OPDOutpatientExaminationNoteDatas.Where(e => !e.IsDeleted).Select(e => new MappingDatasQuery()
            {
                Code = e.Code,
                Value = e.Value
            }).ToList();

            return datas;
        }
        private string FormatDiagnose(List<MappingDatasQuery> datas)
        {
            var code_data = datas.FirstOrDefault(e => e.Code == "OPDOENDD0ANS");
            string textDiagnose = code_data == null ? "" : code_data.Value == null ? "" : code_data.Value;

            var icdchinh = datas.FirstOrDefault(e => e.Code == "OPDOENICDANS");
            string jsonText = icdchinh == null ? "" : icdchinh.Value;
            if (jsonText == null || jsonText == $"\"\"")
                jsonText = "";

            JavaScriptSerializer jss = new JavaScriptSerializer();
            List<Dictionary<string, string>> objs = jss.Deserialize<List<Dictionary<string, string>>>(jsonText);
            string _str = String.Empty;
            if (objs != null)
            {
                int lengthOfobjs = objs.Count;
                for (int i = 0; i < lengthOfobjs; i++)
                {
                    var codeIcd10 = objs[i]["code"]?.ToString();
                    if (i == 0)
                        _str += codeIcd10;
                    else
                        _str += $", {codeIcd10}";
                }
            }

            var icdphu = datas.FirstOrDefault(e => e.Code == "OPDOENICDOPT");
            string jsonphu = icdphu == null ? "" : icdphu.Value;
            if (jsonphu == null || jsonphu == $"\"\"")
                jsonphu = "";

            List<Dictionary<string, string>> objs1 = jss.Deserialize<List<Dictionary<string, string>>>(jsonphu);

            if (objs1 != null)
            {
                int lengthOfobjs = objs1.Count;
                for (int j = 0; j < lengthOfobjs; j++)
                {
                    var codeIcd10phu = objs1[j]["code"]?.ToString();
                    if (j == 0)
                    {
                        if (string.IsNullOrEmpty(_str))
                        {
                            _str += codeIcd10phu;
                        }
                        else
                            _str += $", {codeIcd10phu}";
                    }
                    else
                        _str += $", {codeIcd10phu}";
                }
            }

            if (!string.IsNullOrEmpty(_str))
                textDiagnose += $" ({_str})";

            return textDiagnose;
        }

        private dynamic GetIPDMedicalReport(Guid visit_id, string language, string type, Guid? formId,List<MasterData> masterDatas)
        {
            var ipd = unitOfWork.IPDRepository.GetById(visit_id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            List<dynamic> response = new List<dynamic>();

            var customer = ipd.Customer;
            string gender = null;
            if (customer != null && customer?.Gender != null)
            {
                gender = ConvertGender((int)customer?.Gender, language);
            }

            var nationality = customer.Nationality;
           
            List<string> address = new List<string>();
            if (!string.IsNullOrEmpty(customer.MOHAddress))
                address.Add(customer.MOHAddress);
            if (!string.IsNullOrEmpty(customer.MOHDistrict))
                address.Add(customer.MOHDistrict);
            if (!string.IsNullOrEmpty(customer.MOHProvince))
                address.Add(customer.MOHProvince);
            string strAddress = string.Join(", ", address);
            response.Add(new { Code = "TRANSLATEGENANS", Value = gender });
            response.Add(new { Code = "TRANSLATENATANS", Value = nationality });
            response.Add(new { Code = "TRANSLATEADDANS", Value = strAddress });

            var medical_record = ipd.IPDMedicalRecord;
            var part_2 = medical_record?.IPDMedicalRecordPart2;
            var part_2_datas = part_2?.IPDMedicalRecordPart2Datas.Where(e => !e.IsDeleted)?.ToList();
            var chief_complaint = part_2_datas?.FirstOrDefault(e => e.Code == "IPDMRPTLDVVANS")?.Value;

            var part_3 = medical_record?.IPDMedicalRecordPart3;
            var part_3_datas = part_3?.IPDMedicalRecordPart3Datas.Where(e => !e.IsDeleted).ToList();
            string clinical_evolution = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPEQTBLANS")?.Value ?? "";
            var result_of_paraclinical_tests = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPETTKQANS")?.Value;
            var co_morbidities = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPECDKTANS")?.Value;
            var treatments_and_procedures = (part_3_datas != null && part_3 != null) ? GetTreatmentsLast(ipd.Id, part_3.Id, part_3_datas) : null;
            var significant_medications = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPETCDDANS")?.Value;
            var condition_at_discharge = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPETTNBANS")?.Value;
            var followup_care_plan = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPEHDTVANS")?.Value;
            var doctor_recommendations = part_3_datas?.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDMRPEHDTVANS")?.Value;
            var job = customer.MOHJob;
            var workPlace = customer.WorkPlace;
            var medical_record_datas = medical_record?.IPDMedicalRecordDatas.Where(e => !e.IsDeleted).ToList();

            var receiving_hospital = medical_record_datas?.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDRH1ANS")?.Value;
            string reason_for_transfer1 = medical_record_datas?.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDRFTANS")?.Value;


            string hospitalizedStatus = part_2_datas?.FirstOrDefault(e => e.Code == "IPDMRPTTTBAANS")?.Value ?? string.Empty;
            var dischargeStatus = part_3_datas?.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDMRPETTNBANS")?.Value;

            if (formId != null && formId != Guid.Empty)
            {
                IPDSurgeryCertificate certificate = unitOfWork.IPDSurgeryCertificateRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == formId);
                var dataMap = GenAutofillFromProcedure(certificate);
                if (dataMap.Count > 0)
                {
                    response.Add(new { Code = "TRANSLATEPREDIAGNOSISANS", Value = dataMap?.FirstOrDefault(x => x.Code == "IPDSURCER08")?.Value });
                    response.Add(new { Code = "TRANSLATEDIAGNOSISAFTERSERGERYANS", Value = dataMap?.FirstOrDefault(x => x.Code == "IPDSURCER22")?.Value });
                    response.Add(new { Code = "TRANSLATEPPPTANS", Value = dataMap?.FirstOrDefault(x => x.Code == "IPDSURCER14")?.Value });
                    response.Add(new { Code = "TRANSLATEPPVCANS", Value = dataMap?.FirstOrDefault(x => x.Code == "IPDSURCER16")?.Value });
                }
                else
                {
                    response.Add(new { Code = "TRANSLATEPREDIAGNOSISANS", Value = "" });
                    response.Add(new { Code = "TRANSLATEDIAGNOSISAFTERSERGERYANS", Value = "" });
                    response.Add(new { Code = "TRANSLATEPPPTANS", Value = "" });
                    response.Add(new { Code = "TRANSLATEPPVCANS", Value = "" });
                }
            };
            var medical_sign = GetMedicalSigByMedicalRecord(ipd, part_2);
            string medicalSign = "";
            if (medical_sign?.Count > 0)
            {

                foreach (var m in medical_sign)
                {
                    var value = m.Value ?? "";
                    medicalSign += m.ViName + ": " + value + "\n";
                }
                for (int i = 0; i < 20; i++)
                {
                    if (medicalSign.Contains(i + ". "))
                    {
                        medicalSign = medicalSign.Replace(i + ". ", "");
                    }
                }
                if (medicalSign.Contains("Các bộ phận khác"))
                {
                    medicalSign = medicalSign.Replace("Các bộ phận khác", "Khác");
                }
            }
            var transportation_method = "";
            var escort_person = "";
            var receiving_person = "";

            switch (type)
            {
                case "REFERRAL LETTER":
                    transportation_method = medical_record_datas?.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDTM0ANS")?.Value;
                    escort_person = medical_record_datas?.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDNATANS")?.Value;
                    receiving_person = medical_record_datas?.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDNRHANS")?.Value;
                    break;
                case "TRANSFER LETTER":
                    transportation_method = medical_record_datas?.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDTM1ANS")?.Value;
                    escort_person = medical_record_datas?.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDNTMANS")?.Value;
                    break;
                default:
                    transportation_method = medical_record_datas?.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDTM1ANS")?.Value;
                    escort_person = medical_record_datas?.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDNTMANS")?.Value;
                    receiving_person = medical_record_datas?.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDNTMANS")?.Value;
                    break;
            }

            response.Add(new { Code = "TRANSLATEREASONANS", Value = chief_complaint });
            response.Add(new { Code = "TRANSLATECLINEVOLUANS", Value = clinical_evolution });
            response.Add(new { Code = "TRANSLATERESULTPRATESTANS", Value = result_of_paraclinical_tests });
            response.Add(new { Code = "TRANSLATEPDIAGNOSISANS", Value = (part_3_datas != null && part_3_datas.Count > 0) ? GetDiagnosisIPD(part_3_datas, ipd.Version, type) : "" });
            response.Add(new { Code = "TRANSLATECOMORBIDITIESANS", Value = co_morbidities });
            response.Add(new { Code = "TRANSLATETREATMENTANDPROCEDUREANS", Value = treatments_and_procedures });
            response.Add(new { Code = "TRANSLATEDRUGUSEDANS", Value = significant_medications });
            response.Add(new { Code = "TRANSLATEPERCURRENTPATIENTANS", Value = condition_at_discharge });
            response.Add(new { Code = "TRANSLATEFOLLOWUPCAREPLANANS", Value = followup_care_plan });
            response.Add(new { Code = "TRANSLATENOTEANS", Value = doctor_recommendations });
            response.Add(new { Code = "TRANSLATEJOBANS", Value = job });
            response.Add(new { Code = "TRANSLATEWORKPLACEANS", Value = workPlace });
            response.Add(new { Code = "TRANSLATETREATANDDRUGANS", Value = treatments_and_procedures + "\n" + significant_medications });
            response.Add(new { Code = "TRANSLATECURRENTSATATUSANS", Value = condition_at_discharge });
            response.Add(new { Code = "TRANSLATETRANSPORTANS", Value = transportation_method });
            response.Add(new { Code = "TRANSLATEPERTRANSPORTANS", Value = escort_person });
            response.Add(new { Code = "TRANSLATETREATMENTANS", Value = treatments_and_procedures });
            response.Add(new { Code = "TRANSLATESTATUSINJURYDISCHARGEANS", Value = dischargeStatus });
            response.Add(new { Code = "TRANSLATEFIRSTLASTNAMEPATIENTANS", Value = customer.Fullname });
            response.Add(new { Code = "TRANSLATEGRANDPARENTANS", Value = customer.Fullname });
            response.Add(new { Code = "TRANSLATESUBRESULTANS", Value = result_of_paraclinical_tests });
            response.Add(new { Code = "TRANSLATECONDITIONDISCHARGEANS", Value = condition_at_discharge });
            response.Add(new { Code = "TRANSLATCURRENTPATIENTANS", Value = condition_at_discharge });
            response.Add(new { Code = "TRANSLATELABANDSUBRESULTANS", Value = result_of_paraclinical_tests });
            response.Add(new { Code = "TRANSLATEMPTDRUGUSETREATMENTANS", Value = treatments_and_procedures + "\n" + significant_medications });
            response.Add(new { Code = "TRANSLATESATATUSREFERALANS", Value = condition_at_discharge });
            response.Add(new { Code = "TRANSLATETREANTMENTPLANANS", Value = followup_care_plan });
            response.Add(new { Code = "TRANSLATESTATUSADMITTEDANS", Value = hospitalizedStatus });
            response.Add(new { Code = "TRANSLATECHIEFCOMANS", Value = chief_complaint });
            response.Add(new { Code = "TRANSLATESTATUSPATIENTTRANSFERANS", Value = condition_at_discharge });
            response.Add(new { Code = "TRANSLATEDHLSANS", Value = medicalSign });
            response.Add(new { Code = "TRANSLATECAREPLANANS", Value = followup_care_plan });
            response.Add(new { Code = "TRANSLATEREASONTRANSFERANS", Value = reason_for_transfer1 });
            response.Add(new { Code = "TRANSLATESENDERANS", Value = escort_person });
            response.Add(new { Code = "TRANSLATENAMEMETHODCONTACTEDANS", Value = receiving_person });
            response.Add(new { Code = "TRANSLATEGENVIANS", Value = gender });
            response.Add(new { Code = "TRANSLATESENDERFOOTERANS", Value = escort_person });
            response.Add(new { Code = "TRANSLATETRANSPORTFOOTERANS", Value = transportation_method });
            response.Add(new { Code = "TRANSLATEADDFOOTERANS", Value = strAddress });
            var data = masterDatas.Select(md => new
            {
                md.Code,
                md.Group,
                md.Order,
                md.DataType,
                md.Level,
                md.IsReadOnly,
                md.Note,
                md.Data,
                md.Clinic,
                md.DefaultValue,
                Value = "",
                Id = "",
                md.CreatedAt
            })
            .OrderBy(md => md.CreatedAt)
            .ToList().Join(response, md => md.Code, res => res.Code, (md, res) => new
            {

                Code = md.Code,
                Value = res.Value,
                
            }).ToList<dynamic>();
            return data;
        }
        private dynamic GetEOCMedicalReport(Translation translation, string form,List<MasterData> masterDatas)
        {
            Guid visit_id = (Guid)translation.VisitId;
            var opd = GetEOC(visit_id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var oen = GetOutpatientExaminationNote(visit_id);

            if (oen == null) return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);

            List<dynamic> response = new List<dynamic>();

            var customer = opd.Customer;
            var gender = ConvertGender((int)customer.Gender, translation.FromLanguage);
            var address = customer.Address;
            response.Add(new { Code = "TRANSLATEGENANS", Value = gender });
            response.Add(new { Code = "TRANSLATEADDANS", Value = address });

            var oen_datas = GetFormData(visit_id, oen.Id, "OPDOEN");
            var reason_for_visit = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENCC0ANS")?.Value;
            var hisory_of_present_illness = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENHPIANS")?.Value;
            var clinical_examination_and_findings = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENCEFANS")?.Value;
            var principal_tests = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENPT0ANS")?.Value;
            var treatment_plans = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENTP0ANS")?.Value;
            var recommendation_and_follow_up = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENRFUANS")?.Value;
            var reason_for_transfer = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENRFT3ANS")?.Value;
            string transportation_method = "";
            switch (translation.EnName.ToUpper())
            {
                case "REFERRAL LETTER":
                    transportation_method = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENTM1ANS")?.Value;
                    break;
                case "TRANSFER LETTER":
                    transportation_method = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENTM0ANS")?.Value;
                    break;
            }
            var patient_status = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENPS0ANS")?.Value;
            var result_of_paraclinical_tests = oen_datas.FirstOrDefault(e => e.Code == "OPDOENPT0ANS")?.Value;

            var trans_data = translation?.TranslationDatas;
            string dhls_data = "";
            string ktt = "", kck = "", ktmhh = "", kcbpk = "";
            if (translation.UpdatedAt <= new DateTime(2022, 11, 17, 18, 00, 00))
            {

                switch (oen.Version)
                {
                    case 1:

                        dhls_data = clinical_examination_and_findings;
                        response.Add(new { Code = "TRANSLATEKLSANS", Value = dhls_data });
                        break;
                    case 2:
                        var dhls = GetDataEOCOutpatientExaminationNotesVerson2(oen_datas, Constant.EOC_DATA_FILL_OEN_MEDICAL_REPORT);
                        if (dhls != null && dhls.Count > 0)
                        {
                            foreach (var dataoen in dhls)
                            {
                                response.Add(new { Code = dataoen.CodeOther, Value = dataoen.Value });
                            }
                        }
                        break;
                }
            }
            else
            {
                var dhls = GetDataEOCOutpatientExaminationNotesVerson2(oen_datas, Constant.EOC_DATA_FILL_OEN_MEDICAL_REPORT);
                if (dhls != null && dhls.Count > 0)
                {
                    foreach (var dataoen in dhls)
                    {
                        switch (dataoen.Code)
                        {
                            case "OPDOENKTTLV2":
                                ktt = dataoen.Value;
                                break;
                            case "OPDOENKCKANS":
                                kck = dataoen.Value;
                                break;
                            case "OPDOENKHHLV2":
                                ktmhh = dataoen.Value;
                                break;
                            case "OPDOENKBPKLV2":
                                kcbpk = dataoen.Value;
                                break;
                        }
                    }
                }
                response.Add(new { Code = "TRANSLATEKTTANS", Value = ktt });
                response.Add(new { Code = "TRANSLATEKCKANS", Value = kck });
                response.Add(new { Code = "TRANSLATEKTMHHANS", Value = ktmhh });
                response.Add(new { Code = "TRANSLATEKCBPKANS", Value = kcbpk });

            }

            var escort = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENNTMANS")?.Value;
            var reasons_for_admission = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENCC0ANS")?.Value;
            var history = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENHPIANS")?.Value;
            var patient_status_at_transfer = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENPSAANS")?.Value;
            var drugs_used = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENDU0ANS")?.Value;
            var plan_of_care = oen_datas.FirstOrDefault(e => e.Code == "OPDOENRFUANS")?.Value;
            var medical_escort = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENME0ANS")?.Value;
            var contacted_staff = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENSBCANS")?.Value;
            var methods = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENMTUANS")?.Value;
            var treatment_and_procedures = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENTP0ANS")?.Value;
            response.Add(new { Code = "TRANSLATEREASONFORVISITANS", Value = reason_for_visit });
            response.Add(new { Code = "TRANSLATEHISTORYOFPRESENTANS", Value = hisory_of_present_illness });
            response.Add(new { Code = "TRANSLATEPRINCIPALTESTANS", Value = principal_tests });
            response.Add(new { Code = "TRANSLATEPDIAGNOSISANS", Value = GetDiagnosisEOC(oen_datas, opd.Version, translation.EnName.ToUpper()), });
            response.Add(new { Code = "TRANSLATETREANTMENTPLANANS", Value = treatment_plans });
            response.Add(new { Code = "TRANSLATERECOMMENDATIONANDFOLLOWUPANS", Value = recommendation_and_follow_up });
            response.Add(new { Code = "TRANSLATENATANS", Value = customer.Nationality });
            response.Add(new { Code = "TRANSLATEREASONTRANSFERANS", Value = reason_for_transfer });
            response.Add(new { Code = "TRANSLATETRANSPORTANS", Value = transportation_method });
            response.Add(new { Code = "TRANSLATEJOBANS", Value = customer.Job });
            response.Add(new { Code = "TRANSLATEWORKPLACEANS", Value = customer.WorkPlace });
            response.Add(new { Code = "TRANSLATESATATUSREFERALANS", Value = patient_status });

            response.Add(new { Code = "TRANSLATEGENVIANS", Value = gender });
            response.Add(new { Code = "TRANSLATEMPTDRUGUSETREATMENTANS", Value = methods });
            response.Add(new { Code = "TRANSLATELABANDSUBRESULTANS", Value = principal_tests });
            response.Add(new { Code = "TRANSLATETRANSPORTFOOTERANS", Value = transportation_method });
            response.Add(new { Code = "TRANSLATEPERTRANSPORTANS", Value = escort });
            response.Add(new { Code = "TRANSLATENAMEMETHODCONTACTEDANS", Value = contacted_staff });
            response.Add(new { Code = "TRANSLATESENDERANS", Value = medical_escort });
            response.Add(new { Code = "TRANSLATECHIEFCOMANS", Value = reasons_for_admission });
            response.Add(new { Code = "TRANSLATECLINEVOLUANS", Value = history });
            response.Add(new { Code = "TRANSLATETREATMENTANDPROCEDUREANS", Value = treatment_and_procedures });
            response.Add(new { Code = "TRANSLATEDRUGUSEDANS", Value = drugs_used });
            response.Add(new { Code = "TRANSLATESTATUSPATIENTTRANSFER1ANS", Value = patient_status_at_transfer });
            response.Add(new { Code = "TRANSLATECAREPLANANS", Value = plan_of_care });
            response.Add(new { Code = "TRANSLATEHISTORYANS", Value = history });
            response.Add(new { Code = "TRANSLATESUBRESULTANS", Value = result_of_paraclinical_tests });
            var data = masterDatas.Select(md => new
            {
                md.Code,
                md.Group,
                md.Order,
                md.DataType,
                md.Level,
                md.IsReadOnly,
                md.Note,
                md.Data,
                md.Clinic,
                md.DefaultValue,
                Value = "",
                Id = "",
                md.CreatedAt
            })
           .OrderBy(md => md.CreatedAt)
           .ToList().Join(response, md => md.Code, res => res.Code, (md, res) => new
           {
               Code = md.Code,
               Value = res.Value,

           }).ToList<dynamic>();
            return data;
        }
        private string ConvertGender(int gen, string lang)
        {
            if (lang.Equals("VI"))
            {
                var gender = "Chưa xác định";
                if (gen == 0)
                    gender = "Nữ";
                else if (gen == 1)
                    gender = "Nam";
                return gender;
            }
            else if (lang.Equals("EN"))
            {
                {
                    var gender = "NA";
                    if (gen == 0)
                        gender = "Female";
                    else if (gen == 1)
                        gender = "Male";
                    return gender;
                }
            }
            return string.Empty;
        }
        private EOCOutpatientExaminationNote GetOutpatientExaminationNote(Guid VisitId)
        {
            var form = unitOfWork.EOCOutpatientExaminationNoteRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == VisitId);
            return form;
        }
        private List<MappingData> GetMedicalSigByMedicalRecord(IPD ipd, IPDMedicalRecordPart2 part_2)
        {
            if (part_2 == null) return null;
            var medicalOfpatient = (from m in unitOfWork.IPDMedicalRecordOfPatientRepository.AsQueryable()
                                    where !m.IsDeleted && m.VisitId == ipd.Id
                                    select m).ToList();
            var part3MedicalRecord = (from m in medicalOfpatient
                                      join part3 in unitOfWork.IPDMedicalRecordPart3Repository.AsQueryable()
                                      on m.FormId equals part3.Id
                                      orderby m.UpdatedAt descending
                                      select m).ToList();
            if (part3MedicalRecord == null || part3MedicalRecord.Count == 0) return null;
          
            var checkMedicalRecord = part3MedicalRecord[0];
            var code = new List<string>()
            {
                "IPDMRPTTTYTANS", "IPDMRPTTUHOANS", "IPDMRPTTUHOANS", "IPDMRPTHOHAANS",
                "IPDMRPTTIHOANS", "IPDMRPTTTNSANS", "IPDMRPTTHKIANS", "IPDMRPTCOXKANS",
                "IPDMRPTTAMHANS", "IPDMRPTRAHMANS", "IPDMRPTMMATANS", "IPDMRPTNTDDANS",
                "IPDMRPTCXNCANS", "IPDMRPTTTBAANS", "IPDMRPTCACQ", "IPDMRPTQTBLANS"
            };
            var datas = (from master in unitOfWork.MasterDataRepository.AsQueryable().Where(
                            e => !e.IsDeleted &&
                            !string.IsNullOrEmpty(e.Code) &&
                            code.Contains(e.Code)
                        )
                         join data_sql in unitOfWork.IPDMedicalRecordPart2DataRepository.AsQueryable().Where(
                             e => !e.IsDeleted &&
                             e.IPDMedicalRecordPart2Id != null &&
                             e.IPDMedicalRecordPart2Id == part_2.Id &&
                             !string.IsNullOrEmpty(e.Code) &&
                             code.Contains(e.Code)
                         )
                         on master.Code equals data_sql.Code into data_list
                         from data_sql in data_list.DefaultIfEmpty()
                         select new MappingData
                         {
                             Code = master.Code,
                             Value = data_sql.Value,
                             ViName = master.ViName,
                             EnName = master.EnName,
                             Order = master.Order
                         }).OrderBy(e => e.Order).ToList();
            switch (checkMedicalRecord.FormCode)
            {
                //case "A01_038_050919_V":
                //    string[] code = new string[]
                //     {
                //        "IPDMRPT104", "IPDMRPT105", "IPDMRPT106", "IPDMRPT108",
                //        "IPDMRPT111", "IPDMRPT113", "IPDMRPT120", "IPDMRPT115",
                //        "IPDMRPT116", "IPDMRPT117", "IPDMRPT118", "IPDMRPT119",
                //        "IPDMRPTCACQ", "IPDMRPTHOHAANS", "IPDMRPT142",
                //        "IPDMRPT122", "IPDMRPT124", "IPDMRPT126", "IPDMRPT144",
                //        "IPDMRPT128", "IPDMRPT130", "IPDMRPT132", "IPDMRPT133",
                //        "IPDMRPT135", "IPDMRPT137", "IPDMRPTCXNCANS", "IPDMRPTTTBAANS",
                //        "IPDMRPT139"
                //     };
                //    return GetValueFromMasterData(code, part2_Id);
                //case "A01_037_050919_V":
                //    string[] code_V = new string[]
                //    {
                //        "IPDMRPTTTYTANS", "IPDMRPTCACQANS", "IPDMRPTTUHOANS", "IPDMRPTHOHAANS",
                //        "IPDMRPTTIHOANS", "IPDMRPTTTNSANS", "IPDMRPTTHKIANS", "IPDMRPTCOXKANS",
                //        "IPDMRPTTAMHANS", "IPDMRPTRAHMANS", "IPDMRPTMMATANS", "IPDMRPTNTDDANS"
                //    };
                //    return GetValueFromMasterData(code_V, part_2.Id);
                case "A01_039_050919_V":
                    return GetHistoryMedicalRecord(Constant.IPD_HISTORY_MR_A01_039_050919_V, part_2.Id);
                case "A01_040_050919_V":
                    return GetHistoryMedicalRecord(Constant.IPD_HISTORY_MR_A01_040_050919_V, part_2.Id);
                case "A01_195_050919_V":
                    return GetHistoryMedicalRecord(Constant.IPD_HISTORY_MR_A01_195_050919_V, part_2.Id);
                case "A01_035_050919_V":
                    return GetHistoryMedicalRecord(Constant.IPD_HISTORY_MR_A01_035_050919_V, part_2.Id);
                case "A01_038_050919_V":
                    var toanThan = (from t in unitOfWork.MasterDataRepository.AsQueryable()
                                    where !t.IsDeleted && t.Code == "IPDMRPT104"
                                    select new MappingData
                                    {
                                        Code = t.Code,
                                        Value = "",
                                        ViName = t.ViName,
                                        EnName = t.EnName,
                                        Order = t.Order
                                    }).FirstOrDefault();

                    var lableTt = (from t in unitOfWork.MasterDataRepository.AsQueryable()
                                   where !t.IsDeleted && t.Code == "IPDMRPT133"
                                   select new MappingData
                                   {
                                       Code = t.Code,
                                       Value = "",
                                       ViName = t.ViName,
                                       EnName = t.EnName,
                                       Order = t.Order
                                   }).FirstOrDefault();

                    var hoHap = (from h in unitOfWork.IPDMedicalRecordPart2DataRepository.AsQueryable()
                                 join m in unitOfWork.MasterDataRepository.AsQueryable()
                                 on h.Code equals m.Code
                                 where !h.IsDeleted && h.Code == "IPDMRPTHOHAANS" && h.IPDMedicalRecordPart2Id == part_2.Id
                                 select new MappingData
                                 {
                                     Code = h.Code,
                                     Value = h.Value,
                                     ViName = m.ViName,
                                     EnName = m.EnName,
                                     Order = m.Order
                                 }).FirstOrDefault();
                    var xetNghiemCanLam = datas.Where(d => d.Code == "IPDMRPTCXNCANS" || d.Code == "IPDMRPTTTBAANS");
                    var hoiBenh = (from h in unitOfWork.IPDMedicalRecordPart2DataRepository.AsQueryable()
                                   join m in unitOfWork.MasterDataRepository.AsQueryable()
                                   on h.Code equals m.Code
                                   where h.Code == "IPDMRPT61" && h.IPDMedicalRecordPart2Id == part_2.Id
                                   select new MappingData
                                   {
                                       Code = h.Code,
                                       Value = h.Value,
                                       ViName = m.ViName,
                                       EnName = m.EnName,
                                       Order = m.Order
                                   }).FirstOrDefault();
                    var getFromMasterData = (from m in unitOfWork.MasterDataRepository.AsQueryable()
                                             join d in unitOfWork.IPDMedicalRecordPart2DataRepository.AsQueryable()
                                             on m.Code equals d.Code
                                             where !m.IsDeleted && !d.IsDeleted &&
                                             m.Order >= 644 && m.Order <= 678 && m.Form == "IPDMRPT"
                                             && m.Level == 2 && d.IPDMedicalRecordPart2Id == part_2.Id
                                             orderby m.Order
                                             select new MappingData
                                             {
                                                 Code = m.Code,
                                                 Value = d.Value,
                                                 ViName = m.ViName,
                                                 EnName = m.EnName,
                                                 Order = m.Order
                                             }).Distinct().ToList();

                    var removeVd = (from r in getFromMasterData
                                    where r.Code == "IPDMRPT110"
                                    select new MappingData
                                    {
                                        Code = r.Code,
                                        Value = r.Value,
                                        ViName = r.ViName,
                                        EnName = r.EnName,
                                        Order = r.Order
                                    }).FirstOrDefault();


                    getFromMasterData.Insert(0, hoiBenh);
                    getFromMasterData.Insert(1, toanThan);
                    getFromMasterData.Add(hoHap);
                    getFromMasterData.Add(lableTt);
                    getFromMasterData.AddRange(xetNghiemCanLam);
                    getFromMasterData.Remove(removeVd);
                    return getFromMasterData.ToList();
                case "A01_036_050919_V":
                    return GetHistoryMedicalRecord(Constant.IPD_HISTORY_MR_A01_036_050919_V, part_2.Id);
                case "A01_196_050919_V":
                    string[] code_V = new string[]
                   {
                        "IPDMRPTQTBLANS", "IPDMRPTTTYTANS", "IPDMRPT1003", "IPDMRPT1007",
                        "IPDMRPTCACQ", "IPDMRPTTHKIANS", "IPDMRPTTUHOANS", "IPDMRPTHOHAANS",
                        "IPDMRPTTIHOANS", "IPDMRPTCOXKANS", "IPDMRPT1027", "IPDMRPT1001",
                        "IPDMRPT831"
                   };
                    return GetHistoryMedicalRecord(code_V, part_2.Id);
                case "A01_041_050919_V":
                    var nguyennhan = unitOfWork.IPDMedicalRecordPart2DataRepository.FirstOrDefault(d => !d.IsDeleted && d.Code == "IPDMRPT1054" && d.IPDMedicalRecordPart2Id == part_2.Id);
                    if (string.IsNullOrEmpty(nguyennhan?.Value))
                    {
                        string[] codes_v1 = Constant.IPD_HISTORY_MR_A01_041_050919_V;
                        return GetHistoryMedicalRecord(codes_v1, part_2.Id);
                    }
                    string[] codes_v2 = Constant.IPD_HISTORY_MR_A01_041_050919_V_VESION2;
                    return GetHistoryMedicalRecord(codes_v2, part_2.Id);
                case "BMTIMMACH":
                    string[] codes = Constant.IPD_HISTORY_MR_BMTIMMACH;
                    List<MappingData> datasHis = GetHistoryMedicalRecord(codes, part_2.Id);


                    datasHis.Add(new MappingData
                    {
                        ViName = "+ Bản thân",
                        EnName = "+ Personal medical history",
                        Code = "IPDMRPTBATHANS",
                        Value = GetPersonalHistory(ipd),
                        Group = "",
                        DataType = "",
                        Order = 8888
                    });
                    return datasHis;
                default:
                    return datas;
            }
        }
        //private dynamic GetValueFromMasterData(string[] code, Guid part2_Id)
        //{
        //    var medical_sign = (from master in unitOfWork.MasterDataRepository.AsQueryable().Where(
        //                   e => !e.IsDeleted &&
        //                   !string.IsNullOrEmpty(e.Code) &&
        //                   code.Contains(e.Code)
        //               )
        //                        join data_sql in unitOfWork.IPDMedicalRecordPart2DataRepository.AsQueryable().Where(
        //                            e => !e.IsDeleted &&
        //                            e.IPDMedicalRecordPart2Id != null &&
        //                            e.IPDMedicalRecordPart2Id == part2_Id &&
        //                            !string.IsNullOrEmpty(e.Code) &&
        //                            code.Contains(e.Code)
        //                        )
        //                        on master.Code equals data_sql.Code into data_list
        //                        from data_sql in data_list.DefaultIfEmpty()
        //                        select new
        //                        {
        //                            master.Code,
        //                            Value = data_sql.Value,
        //                            master.ViName,
        //                            master.EnName,
        //                            master.Order
        //                        }).OrderBy(e => e.Order).ToList();
        //    List<dynamic> list_data = new List<dynamic>();
        //    foreach (var item in code)
        //    {
        //        var data = (from d in medical_sign
        //                    where d.Code == item
        //                    select d).FirstOrDefault();
        //        if (data == null) continue;
        //        list_data.Add(data);
        //    }
        //    return list_data;
        //}
        private List<DataClinicalFinding> GetDataEOCOutpatientExaminationNotesVerson2(List<FormDataValue> datas, string[] code_data)
        {
            var result = (from cd in code_data
                          join d in datas on cd equals d.Code
                          join m in unitOfWork.MasterDataRepository.AsQueryable() on cd equals m.Code
                          into data_join
                          from data in data_join.DefaultIfEmpty()
                          select new DataClinicalFinding
                          {

                              Code = cd,
                              Value = d?.Value == null ? "" : d?.Value,
                              ViName = data?.ViName,
                              EnName = data?.EnName,
                              CodeOther = data.DefaultValue
                          }).ToList();
            return result;
        }
        class MappingDatasQuery
        {
            public string Code { get; set; }
            public string Value { get; set; }
        }
        class ConvertData
        {
            public Guid? Id { get; set; }
            public string ViName { get; set; }
            public string EnName { get; set; }
            public string Value { get; set; }
            public string Code { get; set; }

        }
        //class DataModel
        //{
        //    public Guid? Id { get; set; }
        //    public string Code { get; set; }
        //    public string Value { get; set; }
        //    public string Note { get; set; }
        //    public string Data { get; set; }
        //    public string DefaultValue { get; set; }
        //    public DateTime? CreatedAt { get; set; }
        //    public DateTime? UpdatedAt { get; set; }
        //}


    }
}
