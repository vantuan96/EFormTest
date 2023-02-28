using DataAccess.Models;
using DataAccess.Models.EIOModel;
using DataAccess.Models.IPDModel;
using DataAccess.Repository;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Common;
using EForm.Controllers.BaseControllers;
using EForm.Models;
using EForm.Models.IPDModels;
using EForm.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class IPDDocumentController : BaseIPDApiController
    {
        [HttpGet]
        [Route("api/IPD/Document/DischargeMedicalReport/{id}")]
        [Permission(Code = "IDIMR1")]
        public IHttpActionResult IPDDischargeMedicalReportAPI(Guid id)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            if (ipd.IPDMedicalRecordId == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_MERE_NOT_FOUND);

            var first_ipd = GetFirstIpd(ipd);

            var discharge_mr = ipd.IPDDischargeMedicalReport;
            if (discharge_mr == null)
            {
                discharge_mr = new IPDDischargeMedicalReport();
                unitOfWork.IPDDischargeMedicalReportRepository.Add(discharge_mr);
                ipd.IPDDischargeMedicalReportId = discharge_mr.Id;
                unitOfWork.IPDRepository.Update(ipd);
                unitOfWork.Commit();
            }

            var customer = ipd.Customer;
            var gender = new CustomerUtil(customer).GetGender();

            List<string> address = new List<string>();
            if (!string.IsNullOrEmpty(customer.MOHAddress))
                address.Add(customer.MOHAddress);
            if (!string.IsNullOrEmpty(customer.MOHDistrict))
                address.Add(customer.MOHDistrict);
            if (!string.IsNullOrEmpty(customer.MOHProvince))
                address.Add(customer.MOHProvince);

            var medical_record = ipd.IPDMedicalRecord;
            var status = ipd.EDStatus;
            var admission = first_ipd.CurrentDate;
            var discharge = ipd.DischargeDate?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND);

            var part_2 = medical_record.IPDMedicalRecordPart2;
            var part_2_datas = part_2?.IPDMedicalRecordPart2Datas.Where(e => !e.IsDeleted)?.ToList();
            var chief_complaint = part_2_datas?.FirstOrDefault(e => e.Code == "IPDMRPTLDVVANS")?.Value;

            var part_3 = medical_record.IPDMedicalRecordPart3;
            var part_3_datas = part_3?.IPDMedicalRecordPart3Datas.Where(e => !e.IsDeleted)?.ToList();
            var clinical_evolution = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPEQTBLANS")?.Value;
            var result_of_paraclinical_tests = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPETTKQANS")?.Value;
            // var diagnosis = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPECDBCANS")?.Value;
            var icd_diagnosis = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPEICDCANS")?.Value;
            var co_morbidities = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPECDKTANS")?.Value;
            var icd_co_morbidities = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPEICDPANS")?.Value;
            var treatments_and_procedures = GetTreatmentsLast(id, part_3?.Id, part_3_datas);
            var significant_medications = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPETCDDANS")?.Value;
            var condition_at_discharge = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPETTNBANS")?.Value;
            var followup_care_plan = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPEHDTVANS")?.Value;

            var physician_in_charge = discharge_mr.PhysicianInCharge;
            var dept_head = discharge_mr.DeptHead;
            var director = discharge_mr.Director;

            var translation_util = new TranslationUtil(unitOfWork, ipd.Id, "IPD", "Discharge Medical Report");
            var translations = translation_util.GetList();

            if (medical_record?.IPDMedicalRecordPart3Id != null)
            {
                bool checkvalue = ValueICD10IsFalse(medical_record.IPDMedicalRecordPart3Id);
                if (checkvalue == true)
                {
                    icd_co_morbidities = "";
                    co_morbidities = "Không";
                }
            }


            var firstIpd = GetFirstIpdInVisitTypeIPD(ipd);
            var site = GetSite();
            var dateOfNextAppointment = medical_record?.IPDMedicalRecordDatas?.FirstOrDefault(x => !x.IsDeleted && x.Code == "IPDMRPG1001")?.Value;
            return Ok(new
            {
                site?.Location,
                Site = site?.Name,
                site?.Province,
                Specialty = ipd.Specialty?.ViName,
                customer.PID,
                customer.Fullname,
                Gender = gender,
                DateOfBirth = customer.DateOfBirth?.ToString(Constant.DATE_FORMAT),
                Address = string.Join(", ", address),
                customer.Nationality,
                Admission = admission,
                Discharge = discharge,
                ChiefComplaint = chief_complaint,
                ClinicalEvolution = clinical_evolution,
                ResultOfParaclinicalTests = result_of_paraclinical_tests,
                Diagnosis = GetDiagnosisIPD(part_3_datas, ipd.Version, "DISCHARGE MEDICAL REPORT"),
                ICDDiagnosis = icd_diagnosis,
                CoMorbidities = co_morbidities,
                ICDCoMorbidities = icd_co_morbidities,
                TreatmentsAndProcedures = treatments_and_procedures,
                SignificantMedications = significant_medications,
                ConditionAtDischarge = condition_at_discharge,
                FollowUpCarePlan = followup_care_plan,
                Date = discharge,
                PhysicianInCharge = new { physician_in_charge?.Username, physician_in_charge?.Fullname, physician_in_charge?.Title, physician_in_charge?.DisplayName },
                PhysicianInChargeTime = discharge_mr.PhysicianInChargeTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                DeptHead = new { dept_head?.Username, dept_head?.Fullname, dept_head?.Title, dept_head?.DisplayName },
                DeptHeadTime = discharge_mr.DeptHeadTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                Director = new { director?.Username, director?.Fullname, director?.Title, director?.DisplayName },
                DirectorTime = discharge_mr.DirectorTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                Translations = translations,
                DateOfNextAppointment = dateOfNextAppointment,
                AdmittedDateFirstIpd = firstIpd.CurrentDate,
                ipd.Version,
                Id = discharge_mr.Id
            });
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/IPD/Document/DischargeMedicalReport/Confirm/{id}")]
        [Permission(Code = "IDIMR2")]
        public IHttpActionResult IPDConfirmDischargeMedicalReportAPI(Guid id, [FromBody] JObject request)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);
            if (ipd.IPDMedicalRecordId == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_MERE_NOT_FOUND);
            if (ipd.IPDDischargeMedicalReportId == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);
            var discharge_mr = ipd.IPDDischargeMedicalReport;
            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var kind = request["kind"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);

            var positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName);

            if (kind == "PhysicianInCharge" && positions.Contains("Doctor") && discharge_mr.PhysicianInChargeId == null)
            {
                discharge_mr.PhysicianInChargeId = user.Id;
                discharge_mr.PhysicianInChargeTime = DateTime.Now;
            }

            else if (kind == "DeptHead" && positions.Contains("Head Of Department") && discharge_mr.DeptHeadId == null)
            {
                discharge_mr.DeptHeadId = user.Id;
                discharge_mr.DeptHeadTime = DateTime.Now;
            }

            else if (kind == "Director" && positions.Contains("Director") && discharge_mr.DirectorId == null)
            {
                discharge_mr.DirectorId = user.Id;
                discharge_mr.DirectorTime = DateTime.Now;
            }
            else
                return Content(HttpStatusCode.BadRequest, Message.OWNER_FORBIDDEN);

            unitOfWork.IPDDischargeMedicalReportRepository.Update(discharge_mr);
            unitOfWork.Commit();
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpGet]
        [Route("api/IPD/Document/ReferralLetter/{id}")]
        [Permission(Code = "IRELE1")]
        public IHttpActionResult IPDReferralLetterAPI(Guid id)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            if (ipd.IPDMedicalRecordId == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_MERE_NOT_FOUND);

            var referral_letter = ipd.IPDReferralLetter;
            if (referral_letter == null)
            {
                referral_letter = new IPDReferralLetter();
                unitOfWork.IPDReferralLetterRepository.Add(referral_letter);
                ipd.IPDReferralLetterId = referral_letter.Id;
                unitOfWork.IPDRepository.Update(ipd);
                unitOfWork.Commit();
            }
            var first_ipd = GetFirstIpd(ipd);
            var customer = ipd.Customer;
            var gender = new CustomerUtil(customer).GetGender();

            var status = ipd.EDStatus;
            var admission = first_ipd.CurrentDate;
            var discharge = ipd.DischargeDate?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND);

            var medical_record = ipd.IPDMedicalRecord;
            var medical_record_datas = medical_record.IPDMedicalRecordDatas.Where(e => !e.IsDeleted).ToList();

            var receiving_hospital = medical_record_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDRH0ANS")?.Value;
            string reason_for_transfer = medical_record_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDRFTANS")?.Value;
            //IPDMRPTCHVH

            var time_of_transfer = medical_record_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDMRPTCHVHANS")?.Value;
            var receiving_person = medical_record_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDNRHANS")?.Value;
            var transportation_method = medical_record_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDTM0ANS")?.Value;
            var escort_person = medical_record_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDNATANS")?.Value;

            var part_2 = medical_record.IPDMedicalRecordPart2;
            var part_2_datas = part_2?.IPDMedicalRecordPart2Datas.Where(e => !e.IsDeleted)?.ToList();
            var reasons_for_admission = part_2_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDMRPTLDVVANS")?.Value;

            var part_3 = medical_record.IPDMedicalRecordPart3;
            var part_3_datas = part_3?.IPDMedicalRecordPart3Datas.Where(e => !e.IsDeleted)?.ToList();
            var clinical_evolution = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPEQTBLANS")?.Value;
            var result_of_paraclinical_tests = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPETTKQANS")?.Value;

            var icd_diagnosis = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPEICDCANS")?.Value;
            var co_morbidities = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPECDKTANS")?.Value;
            var icd_co_morbidities = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPEICDPANS")?.Value;
            var treatments_and_procedures = GetTreatmentsLast(id, part_3.Id, part_3_datas);
            var significant_medications = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPETCDDANS")?.Value;
            var condition_at_discharge = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPETTNBANS")?.Value;
            var followup_care_plan = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPEHDTVANS")?.Value;

            var physician_in_charge = referral_letter.PhysicianInCharge;
            var director = referral_letter.Director;
            var site = GetSite();
            var translation_util = new TranslationUtil(unitOfWork, ipd.Id, "IPD", "Referral Letter");
            var translations = translation_util.GetList();
            return Ok(new
            {
                site?.Location,
                Site = site?.Name,
                site?.Province,
                Specialty = ipd.Specialty?.ViName,
                customer.PID,
                customer.Fullname,
                Gender = gender,
                DateOfBirth = customer.DateOfBirth?.ToString(Constant.DATE_FORMAT),
                customer.Nationality,
                Admission = admission,
                Discharge = discharge,
                ipd.HealthInsuranceNumber,
                ExpireHealthInsuranceDate = ipd.ExpireHealthInsuranceDate?.ToString(Constant.DATE_FORMAT),
                ReceivingHospital = receiving_hospital,
                ReasonForTransfer = reason_for_transfer,
                TimeOfTransfer = time_of_transfer,
                ReceivingPerson = receiving_person,
                TransportationMethod = transportation_method,
                EscortPerson = escort_person,
                ReasonsForAdmission = reasons_for_admission,
                ClinicalEvolution = clinical_evolution,
                ResultOfParaclinicalTests = result_of_paraclinical_tests,
                Diagnosis = GetDiagnosisIPD(part_3_datas, ipd.Version, "REFERRAL LETTER"),
                ICDDiagnosis = icd_diagnosis,
                CoMorbidities = co_morbidities,
                ICDCoMorbidities = icd_co_morbidities,
                TreatmentsAndProcedures = treatments_and_procedures,
                DrugsUsed = significant_medications,
                ConditionAtDischarge = condition_at_discharge,
                FollowUpCarePlan = followup_care_plan,
                Date = discharge,
                PhysicianInCharge = new { physician_in_charge?.Username, physician_in_charge?.Fullname, physician_in_charge?.Title, physician_in_charge?.DisplayName },
                PhysicianInChargeTime = referral_letter.PhysicianInChargeTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                Director = new { director?.Username, director?.Fullname, director?.Title, director?.DisplayName },
                DirectorTime = referral_letter.DirectorTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                Translations = translations,
                ipd.Version,
                Id = referral_letter.Id,
                IsLocked = IPDIsBlock(ipd, "A01_145_050919_VE", referral_letter.Id)
            });
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/IPD/Document/ReferralLetter/Confirm/{id}")]
        [Permission(Code = "IRELE2")]
        public IHttpActionResult IPDConfirmReferralLetterReportAPI(Guid id, [FromBody] JObject request)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);
            if (ipd.IPDMedicalRecordId == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_MERE_NOT_FOUND);
            if (ipd.IPDReferralLetterId == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);

            var referral_letter = ipd.IPDReferralLetter;
            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var kind = request["kind"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);

            var positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName);

            if (kind == "PhysicianInCharge" && positions.Contains("Doctor") && referral_letter.PhysicianInChargeId == null)
            {
                referral_letter.PhysicianInChargeId = user.Id;
                referral_letter.PhysicianInChargeTime = DateTime.Now;
            }

            else if (kind == "Director" && positions.Contains("Director") && referral_letter.DirectorId == null)
            {
                referral_letter.DirectorId = user.Id;
                referral_letter.DirectorTime = DateTime.Now;
            }
            else
                return Content(HttpStatusCode.BadRequest, Message.OWNER_FORBIDDEN);

            unitOfWork.IPDReferralLetterRepository.Update(referral_letter);
            unitOfWork.Commit();
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpGet]
        [Route("api/IPD/Document/TransferLetter/{id}")]
        [Permission(Code = "ITFLE1")]
        public IHttpActionResult IPDTransferLetterAPI(Guid id)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            if (ipd.IPDMedicalRecordId == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_MERE_NOT_FOUND);

            var transfer_letter = ipd.IPDTransferLetter;
            if (transfer_letter == null)
            {
                transfer_letter = new IPDTransferLetter();
                unitOfWork.IPDTransferLetterRepository.Add(transfer_letter);

                ipd.IPDTransferLetterId = transfer_letter.Id;
                unitOfWork.IPDRepository.Update(ipd);
                unitOfWork.Commit();
            }
            var first_ipd = GetFirstIpd(ipd);
            var customer = ipd.Customer;
            var gender = new CustomerUtil(customer).GetGender();

            List<string> address = new List<string>();
            if (!string.IsNullOrEmpty(customer.MOHAddress))
                address.Add(customer.MOHAddress);
            if (!string.IsNullOrEmpty(customer.MOHDistrict))
                address.Add(customer.MOHDistrict);
            if (!string.IsNullOrEmpty(customer.MOHProvince))
                address.Add(customer.MOHProvince);

            var status = ipd.EDStatus;
            var admission = first_ipd.CurrentDate;
            var discharge = ipd.DischargeDate?.ToString(Constant.DATE_FORMAT);

            var medical_record = ipd.IPDMedicalRecord;
            var medical_record_datas = medical_record.IPDMedicalRecordDatas.Where(e => !e.IsDeleted).ToList();

            var receiving_hospital = medical_record_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDRH1ANS")?.Value;

            var reason_1 = medical_record_datas.FirstOrDefault(e => e.Code == "IPDRFT1LOG");
            var reason_1_data = unitOfWork.MasterDataRepository.FirstOrDefault(m => m.Code == "IPDRFT1LOG");
            var reason_2 = medical_record_datas.FirstOrDefault(e => e.Code == "IPDRFT1SHT");
            var reason_2_data = unitOfWork.MasterDataRepository.FirstOrDefault(m => m.Code == "IPDRFT1SHT");
            var reason_for_transfer = new List<dynamic>() {
                new { reason_1_data?.ViName, reason_1?.Value},
                new { reason_2_data?.ViName, reason_2?.Value},
            };

            var time_of_transfer = medical_record_datas?.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDTD0ANS")?.Value;
            var receiving_person = medical_record_datas?.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDNTMANS")?.Value;
            var transportation_method = medical_record_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDTM1ANS")?.Value;
            var escort_person = medical_record_datas?.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDNTMANS")?.Value;

            var part_2 = medical_record.IPDMedicalRecordPart2;
            var part_2_datas = part_2?.IPDMedicalRecordPart2Datas.Where(e => !e.IsDeleted)?.ToList();
            var reasons_for_admission = part_2_datas?.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDMRPTLDVVANS")?.Value;

            var medical_sign = GetMedicalSigByMedicalRecord(id, part_2);

            var part_3 = medical_record.IPDMedicalRecordPart3;
            var part_3_datas = part_3?.IPDMedicalRecordPart3Datas.Where(e => !e.IsDeleted)?.ToList();
            var result_of_paraclinical_tests = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPETTKQANS")?.Value;

            var icd_diagnosis = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPEICDCANS")?.Value;
            var co_morbidities = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPECDKTANS")?.Value;
            var icd_co_morbidities = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPEICDPANS")?.Value;
            var treatments_and_procedures = (part_3 != null && part_3_datas != null) ? GetTreatmentsLast(id, part_3.Id, part_3_datas) : null;
            var significant_medications = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPETCDDANS")?.Value;
            var condition_at_discharge = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPETTNBANS")?.Value;
            var followup_care_plan = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPEHDTVANS")?.Value;

            var physician_in_charge = transfer_letter.PhysicianInCharge;
            var director = transfer_letter.Director;

            var site = GetSite();
            var translation_util = new TranslationUtil(unitOfWork, ipd.Id, "IPD", "Transfer Letter");
            var translations = translation_util.GetList();
            return Ok(new
            {
                site?.Location,
                site?.LocationUnit,
                Site = site?.Name,
                site?.Level,
                site?.Province,
                Specialty = ipd.Specialty?.ViName,
                customer.PID,
                customer.Fullname,
                customer.AgeFormated,
                Gender = gender,
                DateOfBirth = customer.DateOfBirth?.ToString(Constant.DATE_FORMAT),
                Address = string.Join(", ", address),
                Ethnic = customer.MOHEthnic,
                Job = customer.MOHJob,
                customer.WorkPlace,
                customer.Nationality,
                Admission = admission,
                Discharge = discharge,
                ipd.HealthInsuranceNumber,
                ExpireHealthInsuranceDate = ipd.ExpireHealthInsuranceDate?.ToString(Constant.DATE_FORMAT),
                ReceivingHospital = receiving_hospital,
                ReasonForTransfer = reason_for_transfer,
                TimeOfTransfer = time_of_transfer,
                ReceivingPerson = receiving_person,
                TransportationMethod = transportation_method,
                EscortPerson = escort_person,
                ReasonsForAdmission = reasons_for_admission,
                MedicalSign = medical_sign,
                ResultOfParaclinicalTests = result_of_paraclinical_tests,
                Diagnosis = GetDiagnosisIPD(part_3_datas, ipd.Version, "TRANSFER LETTER"),
                ICDDiagnosis = icd_diagnosis,
                CoMorbidities = co_morbidities,
                ICDCoMorbidities = icd_co_morbidities,
                TreatmentsAndProcedures = treatments_and_procedures,
                DrugsUsed = significant_medications,
                ConditionAtDischarge = condition_at_discharge,
                FollowUpCarePlan = followup_care_plan,
                Date = discharge,
                PhysicianInCharge = new { physician_in_charge?.Username, physician_in_charge?.Fullname, physician_in_charge?.Title, physician_in_charge?.DisplayName },
                PhysicianInChargeTime = transfer_letter.PhysicianInChargeTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                Director = new { director?.Username, director?.Fullname, director?.Title, director?.DisplayName },
                DirectorTime = transfer_letter.DirectorTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                VisitType = "IPD",
                IPDMedicalRecordOfPatient = GetLastIPDMedicalRecordOfPatients(id),
                Translations = translations,
                ipd.Version,
                Id = transfer_letter.Id,
                IsLocked = IPDIsBlock(ipd, "A01_167_180220_VE", transfer_letter.Id)
            });
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/IPD/Document/TransferLetter/Confirm/{id}")]
        [Permission(Code = "ITFLE2")]
        public IHttpActionResult IPDConfirmTransferLetterReportAPI(Guid id, [FromBody] JObject request)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);
            if (ipd.IPDMedicalRecordId == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_MERE_NOT_FOUND);
            if (ipd.IPDTransferLetterId == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);

            var transfer_letter = ipd.IPDTransferLetter;
            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var kind = request["kind"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);

            var positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName);

            if (kind == "PhysicianInCharge" && positions.Contains("Doctor") && transfer_letter.PhysicianInChargeId == null)
            {
                transfer_letter.PhysicianInChargeId = user.Id;
                transfer_letter.PhysicianInChargeTime = DateTime.Now;
            }

            else if (kind == "Director" && (positions.Contains("Director") || positions.Contains("CMO") || positions.Contains("Head Of Department")) && transfer_letter.DirectorId == null)
            {
                transfer_letter.DirectorId = user.Id;
                transfer_letter.DirectorTime = DateTime.Now;
            }
            else
                return Content(HttpStatusCode.BadRequest, Message.OWNER_FORBIDDEN);

            unitOfWork.IPDTransferLetterRepository.Update(transfer_letter);
            unitOfWork.Commit();
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        // Giay ra vien
        [HttpGet]
        [Route("api/IPD/Document/DischargeCertificate/{id}")]
        [Permission(Code = "ITFLE23")]
        public IHttpActionResult DischargeCertificate(Guid id)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            if (ipd.IPDMedicalRecordId == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_DOC_DISCHA_NOT_FOUND);
            var medical_record = ipd.IPDMedicalRecord;
            if (medical_record.IPDMedicalRecordPart3Id == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_DOC_DISCHA_NOT_FOUND);

            var first_ipd = GetFirstIpd(ipd);

            var customer = ipd.Customer;
            var gender = new CustomerUtil(customer).GetGender();

            var part_3 = medical_record.IPDMedicalRecordPart3;
            var part_3_datas = part_3?.IPDMedicalRecordPart3Datas.Where(e => !e.IsDeleted)?.ToList();


            var treatment_procedures = GetTreatmentsLast(id, part_3.Id, part_3_datas);
            var doctor_recommendations = part_3_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDMRPEHDTVANS")?.Value;

            var icd_diagnosis = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPEICDCANS")?.Value;
            var icd_option = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPEICDPANS")?.Value;

            var DiagnosisOption = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPECDKTANS")?.Value;

            var DischareDate = ipd.DischargeDate?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND);

            var site = GetSite();

            List<string> address = new List<string>();
            if (!string.IsNullOrEmpty(customer.MOHAddress))
                address.Add(customer.MOHAddress);
            if (!string.IsNullOrEmpty(customer.MOHDistrict))
                address.Add(customer.MOHDistrict);
            if (!string.IsNullOrEmpty(customer.MOHProvince))
                address.Add(customer.MOHProvince);


            var firstIpd = GetFirstIpdInVisitTypeIPD(ipd);
            var department = GetSpecialty();
            var confirm = GetFormConfirmForDischargeCertificate(ipd);
            var translation_util = new TranslationUtil(unitOfWork, ipd.Id, "IPD", "Discharge Certificate");
            var translations = translation_util.GetList();
            return Ok(new
            {
                site?.Location,
                Site = site?.Name,
                site?.Province,
                Date = ipd.DischargeDate?.ToString(Constant.DATE_FORMAT),
                customer.PID,
                customer.Fullname,
                Age = DateTime.Now.Year - customer.DateOfBirth?.Year,
                customer.AgeFormated,
                Gender = gender,
                Folk = customer.MOHEthnic,
                Job = customer.MOHJob,
                Address = string.Join(", ", address),
                ipd.HealthInsuranceNumber,
                StartHealthInsuranceDate = ipd.StartHealthInsuranceDate?.ToString(Constant.DATE_FORMAT),
                ExpireHealthInsuranceDate = ipd.ExpireHealthInsuranceDate?.ToString(Constant.DATE_FORMAT),
                AdmittedDate = first_ipd.CurrentDate,
                DischareDate,
                TreatmentAndProcedures = treatment_procedures,
                DoctorRecommendations = doctor_recommendations,
                ICDDiagnosis = icd_diagnosis,
                ICDOption = icd_option,
                Diagnosis = GetDiagnosisIPD(part_3_datas, ipd.Version, "DISCHARGE CERTIFICATE"),
                DiagnosisOption,
                Department = new
                {
                    department?.EnName,
                    department?.ViName
                },
                Specialty = ipd.Specialty?.ViName,
                AdmittedDateFirstIpd = firstIpd.CurrentDate,
                Confirm = confirm,
                Translations = translations,
                ipd.Version,
                Id = ipd.Id
            });
        }

        [HttpPost]
        [Route("api/IPD/Document/DischargeCertificate/Confirm/{id}")]
        [Permission(Code = "IPDDISCHAGECONFIRM")]
        public IHttpActionResult ConfirmDischargeCertificate(Guid id, [FromBody] JObject req)
        {
            IPD visit = GetIPD(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            string userName = req["username"]?.ToString();
            string pass = req["password"]?.ToString();
            var user = GetAcceptUser(userName, pass);
            if (user == null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);

            var acction = GetActionOfUser(user, "IPDDISCHAGECONFIRM");
            if (acction == null)
                return Content(HttpStatusCode.Forbidden, Message.DOCTOR_IS_UNAUTHORIZED);

            string confirmType = req["kind"]?.ToString();
            bool success = CreateConfirmDischargeCertificate(visit.Id, confirmType, user);
            if (!success)
                return Content(HttpStatusCode.BadRequest, Message.DOCTOR_ACCEPTED);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        //Báo cáo y tế - daoducdev
        [HttpGet]
        [Route("api/IPD/Document/MedicalReport/{id}")]
        [Permission(Code = "IMR01")]
        public IHttpActionResult IPDMedicalReportAPI(Guid id)
        {
            var visit = GetIPD(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);

            var customer = visit.Customer;
            var medical_record = visit.IPDMedicalRecord;
            var first_ipd = GetFirstIpd(visit);
            var part_2 = medical_record?.IPDMedicalRecordPart2;
            var part_2_datas = part_2?.IPDMedicalRecordPart2Datas.Where(e => !e.IsDeleted)?.ToList();
            var chief_complaint = part_2_datas?.FirstOrDefault(e => e.Code == "IPDMRPTLDVVANS")?.Value; //Lý do vào viện

            var part_3 = medical_record?.IPDMedicalRecordPart3;
            var part_3_datas = part_3?.IPDMedicalRecordPart3Datas.Where(e => !e.IsDeleted)?.ToList();
            var clinical_evolution = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPEQTBLANS")?.Value; //Quá trình bệnh lý, diễn biến lâm sàng
            var result_of_paraclinical_tests = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPETTKQANS")?.Value; //Kết quả cận lâm sàng

            var treatments_and_procedures = GetTreatmentsLast(id, part_3?.Id, part_3_datas); //Phương pháp điều trị
            var condition_at_discharge = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPETTNBANS")?.Value; //Tình trạng người bệnh hiện tại
            var followup_care_plan = part_3_datas?.FirstOrDefault(e => e.Code == "IPDMRPEHDTVANS")?.Value; //Kế hoạch điều trị và chăm sóc tiếp theo

            var translation_util = new TranslationUtil(unitOfWork, visit.Id, "IPD", "Medical report");
            var translations = translation_util.GetList();
            var status = visit.EDStatus;
            var site = GetSite();

            var dateOfNextAppointment = medical_record?.IPDMedicalRecordDatas?.FirstOrDefault(x => !x.IsDeleted && x.Code == "IPDMRPG1001")?.Value;
            return Ok(new
            {
                Location = visit.Site?.Location,
                Site = site?.Name,
                site?.Province,
                Date = visit.DischargeDate?.ToString("dd/MM/yyyy"),
                DischargeHour = visit.DischargeDate?.ToString(Constant.TIME_FORMAT_WITHOUT_SECOND),
                customer.Fullname,
                DateOfBirth = customer.DateOfBirth?.ToString(Constant.DATE_FORMAT),
                customer.PID,
                AdmittedDate = first_ipd.CurrentDate,
                DischargeDate = visit.DischargeDate?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                ChiefComplain = chief_complaint,
                Diagnosis = GetDiagnosisIPD(part_3_datas, visit.Version, "MEDICAL REPORT"),
                FollowupCarePlan = followup_care_plan,
                Translations = translations,
                CurrentStatus = condition_at_discharge,
                TreatmentAndProcedures = treatments_and_procedures,
                ResultOfParaclinicalTests = result_of_paraclinical_tests,
                ClinicalEvolution = clinical_evolution,
                DateOfNextAppointment = dateOfNextAppointment,
                Status = new
                {
                    status?.Id,
                    status?.ViName,
                    status?.EnName,
                    status?.Code
                },
                visit.Version
            });
        }

        private dynamic GetMedicalSigByMedicalRecord(Guid ipdId, IPDMedicalRecordPart2 part_2)
        {
            var medicalOfpatient = (from m in unitOfWork.IPDMedicalRecordOfPatientRepository.AsQueryable()
                                    where !m.IsDeleted && m.VisitId == ipdId
                                    select m).ToList();
            var part3MedicalRecord = (from m in medicalOfpatient
                                      join part3 in unitOfWork.IPDMedicalRecordPart3Repository.AsQueryable()
                                      on m.FormId equals part3.Id
                                      orderby m.UpdatedAt descending
                                      select m).ToList();
            if (part3MedicalRecord == null || part3MedicalRecord.Count == 0) return null;
            var part2_Id = part_2.Id;
            var checkMedicalRecord = part3MedicalRecord[0];
            switch (checkMedicalRecord.FormCode)
            {
                case "A01_038_050919_V":
                    string[] code = new string[]
                     {
                        "IPDMRPT104", "IPDMRPT105", "IPDMRPT106", "IPDMRPT108",
                        "IPDMRPT111", "IPDMRPT113", "IPDMRPT120", "IPDMRPT115",
                        "IPDMRPT116", "IPDMRPT117", "IPDMRPT118", "IPDMRPT119",
                        "IPDMRPTCACQ", "IPDMRPTHOHAANS", "IPDMRPT142",
                        "IPDMRPT122", "IPDMRPT124", "IPDMRPT126", "IPDMRPT144",
                        "IPDMRPT128", "IPDMRPT130", "IPDMRPT132", "IPDMRPT133",
                        "IPDMRPT135", "IPDMRPT137", "IPDMRPTCXNCANS", "IPDMRPTTTBAANS",
                        "IPDMRPT139"
                     };
                    return GetValueFromMasterData(code, part2_Id);

                case "A01_035_050919_V":
                    code = new string[]
                    {
                        "IPDMRPTTTYTANS", "IPDMRPTTUHOANS", "IPDMRPTHOHAANS", "IPDMRPTTIHOANS",
                        "IPDMRPTTTNSANS", "IPDMRPT831"
                    };
                    return GetValueFromMasterData(code, part2_Id);
                case "A01_037_050919_V":
                    code = new string[]
                    {
                        "IPDMRPTTTYTANS", "IPDMRPTCACQANS", "IPDMRPTTUHOANS", "IPDMRPTHOHAANS",
                        "IPDMRPTTIHOANS", "IPDMRPTTTNSANS", "IPDMRPTTHKIANS", "IPDMRPTCOXKANS",
                        "IPDMRPTTAMHANS", "IPDMRPTRAHMANS", "IPDMRPTMMATANS", "IPDMRPTNTDDANS"
                    };
                    return GetValueFromMasterData(code, part2_Id);

                default:
                    code = new string[]
                    {
                        "IPDMRPTTTYTANS", "IPDMRPTTUHOANS", "IPDMRPTTUHOANS", "IPDMRPTHOHAANS",
                        "IPDMRPTTIHOANS", "IPDMRPTTTNSANS", "IPDMRPTTHKIANS", "IPDMRPTCOXKANS",
                        "IPDMRPTTAMHANS", "IPDMRPTRAHMANS", "IPDMRPTMMATANS", "IPDMRPTNTDDANS",
                        "IPDMRPTCACQ",
                     };

                    var medical_sign = (from master in unitOfWork.MasterDataRepository.AsQueryable().Where(
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
                                        select new
                                        {
                                            master.Code,
                                            data_sql.Value,
                                            master.ViName,
                                            master.EnName,
                                            master.Order
                                        }).OrderBy(e => e.Order).ToList();
                    return medical_sign;
            }
        }
        private dynamic GetValueFromMasterData(string[] code, Guid part2_Id)
        {
            var medical_sign = (from master in unitOfWork.MasterDataRepository.AsQueryable().Where(
                           e => !e.IsDeleted &&
                           !string.IsNullOrEmpty(e.Code) &&
                           code.Contains(e.Code)
                       )
                                join data_sql in unitOfWork.IPDMedicalRecordPart2DataRepository.AsQueryable().Where(
                                    e => !e.IsDeleted &&
                                    e.IPDMedicalRecordPart2Id != null &&
                                    e.IPDMedicalRecordPart2Id == part2_Id &&
                                    !string.IsNullOrEmpty(e.Code) &&
                                    code.Contains(e.Code)
                                )
                                on master.Code equals data_sql.Code into data_list
                                from data_sql in data_list.DefaultIfEmpty()
                                select new
                                {
                                    master.Code,
                                    Value = data_sql.Value,
                                    master.ViName,
                                    master.EnName,
                                    master.Order
                                }).OrderBy(e => e.Order).ToList();
            List<dynamic> list_data = new List<dynamic>();
            foreach (var item in code)
            {
                var data = (from d in medical_sign
                            where d.Code == item
                            select d).FirstOrDefault();
                if (data == null) continue;
                list_data.Add(data);
            }
            return list_data;
        }

        private bool ValueICD10IsFalse(Guid? part3_Id)
        {
            var code1 = unitOfWork.IPDMedicalRecordPart3DataRepository
                        .FirstOrDefault(p => !p.IsDeleted && p.IPDMedicalRecordPart3Id == part3_Id && p.Code == "IPDMRPECDKTNO");
            if (code1 != null)
            {
                if (code1.Value == "True")
                {
                    return true;
                }
                return false;
            }
            return true;
        }
        // GET XAC NHẬN GIẤY RA VIỆN VERSION 2
        private List<dynamic> GetFormConfirmForDischargeCertificate(IPD visit)
        {
            var confirms = (from e in unitOfWork.EIOFormConfirmRepository.AsQueryable()
                            join u in unitOfWork.UserRepository.AsQueryable()
                            on e.ConfirmBy equals u.Username into default_query
                            where !e.IsDeleted && e.FormId == visit.Id
                            && e.Note == "IPDCONFIRMDISCHAGE"
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
            const string nameConfirm = "IPDCONFIRMDISCHAGE";
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
