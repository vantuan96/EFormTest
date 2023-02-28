using DataAccess.Models;
using DataAccess.Models.EDModel;
using DataAccess.Models.EOCModel;
using DataAccess.Models.IPDModel;
using DataAccess.Models.OPDModel;
using DataAccess.Repository;
using EForm.Client;
using EForm.Common;
using EForm.Helper;
using EForm.Models;
using EMRModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EForm.Utils
{
    public abstract class VisitHistory
    {
        public string PainRecordFormCode = "A01_042_050919_VE_Synthesis";
        public string PACFormCode = "A03_034_200520_VE";

        protected IUnitOfWork unitOfWork = new EfUnitOfWork();
        public abstract List<VisitModel> GetHistory();
        public abstract List<VisitModel> GetPromForCoronaryDiseasesHistory();
        public abstract List<VisitModel> GetPromForheartFailuresHistory();
        public abstract List<VisitModel> GetPromCurrentForCoronaryDiseases(Guid visitId);
        public abstract List<VisitModel> GetPromCurrentForheartFailures(Guid visitId);
        public abstract List<VisitModel> GetPersonalHistory(Guid visitId);
        protected List<VisitModel> GetEDHistory(Guid customer_id, DateTime visit_time)
        {
            var eds = (from visit in unitOfWork.EDRepository.AsQueryable().Where(
                        e => !e.IsDeleted &&
                        e.AdmittedDate < visit_time &&
                        e.CustomerId != null &&
                        e.CustomerId == customer_id &&
                        e.SpecialtyId != null &&
                        !string.IsNullOrEmpty(e.VisitCode)
                      )
                       join phm in unitOfWork.EmergencyRecordDataRepository.AsQueryable()
                         on new { visit.EmergencyRecordId, Code = "ER0PHHANS" } equals new { phm.EmergencyRecordId, phm.Code } into phm_list
                       from past_medical_history in phm_list.DefaultIfEmpty()

                       join hpi in unitOfWork.EmergencyRecordDataRepository.AsQueryable()
                         on new { visit.EmergencyRecordId, Code = "ER0HISANS" } equals new { hpi.EmergencyRecordId, hpi.Code } into hpi_list
                       from history_of_present_illness in hpi_list.DefaultIfEmpty()
                       join di in unitOfWork.DischargeInformationRepository.AsQueryable()
                         on visit.DischargeInformationId equals di.Id
                       join doc in unitOfWork.UserRepository.AsQueryable()
                         on di.UpdatedBy equals doc.Username into doctor_list
                       from doctor in doctor_list.DefaultIfEmpty()
                       join spec in unitOfWork.SpecialtyRepository.AsQueryable()
                         on visit.SpecialtyId equals spec.Id into spec_list
                       from specialty in spec_list.DefaultIfEmpty()
                       select new VisitModel()
                       {
                           ExaminationTime = visit.AdmittedDate,
                           Username = doctor.Username,
                           Fullname = doctor.Fullname,
                           ViName = specialty.ViName,
                           EnName = specialty.EnName,
                           PastMedicalHistory = past_medical_history.Value,
                           FamilyMedicalHistory = "",
                           HistoryOfPresentIllness = history_of_present_illness.Value,
                           HistoryOfAllergies = visit.Allergy,
                           VisitCode = visit.VisitCode,
                           EHOSVisitCode = doctor.EHOSAccount + visit.VisitCode,
                           Type = "AD",
                       }).ToListNoLock();
            return eds.GroupBy(e => e.ExaminationTime.ToString()).Select(e => e.FirstOrDefault()).ToList();
        }
        protected List<VisitModel> GetOPDHistory(Guid customer_id, DateTime visit_time)
        {
            var no_examination = unitOfWork.EDStatusRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                !string.IsNullOrEmpty(e.Code) &&
                Constant.NoExamination.Contains(e.Code) &&
                e.VisitTypeGroupId != null &&
                e.VisitTypeGroup.Code == "OPD"
            );

            var reslut = (from visit in unitOfWork.OPDRepository.AsQueryable().Where(
                       e => !e.IsDeleted &&
                       e.AdmittedDate < visit_time &&
                       e.CustomerId != null &&
                       e.CustomerId == customer_id &&
                       !string.IsNullOrEmpty(e.VisitCode) &&
                       e.EDStatusId != null &&
                       e.EDStatusId != no_examination.Id
                     )
                          join phm in unitOfWork.OPDOutpatientExaminationNoteDataRepository.AsQueryable()
                              on new { visit.OPDOutpatientExaminationNoteId, Code = "OPDOENPMHANS" } equals new { phm.OPDOutpatientExaminationNoteId, phm.Code } into phm_list
                          from past_medical_history in phm_list.DefaultIfEmpty()

                          join phm_OPDOENTSSKANS in unitOfWork.OPDOutpatientExaminationNoteDataRepository.AsQueryable()
                              on new { visit.OPDOutpatientExaminationNoteId, Code = "OPDOENTSSKANS" } equals new { phm_OPDOENTSSKANS.OPDOutpatientExaminationNoteId, phm_OPDOENTSSKANS.Code } into phm_OPDOENTSSKANS_list
                          from past_medical_history_OPDOENTSSKANS in phm_OPDOENTSSKANS_list.DefaultIfEmpty()

                          join phm_OPDOENTSKNANS in unitOfWork.OPDOutpatientExaminationNoteDataRepository.AsQueryable()
                              on new { visit.OPDOutpatientExaminationNoteId, Code = "OPDOENTSKNANS" } equals new { phm_OPDOENTSKNANS.OPDOutpatientExaminationNoteId, phm_OPDOENTSKNANS.Code } into phm_OPDOENTSKNANS_list
                          from past_medical_history_OPDOENTSKNANS in phm_OPDOENTSKNANS_list.DefaultIfEmpty()

                          join phm_OPDOENTSKANS in unitOfWork.OPDOutpatientExaminationNoteDataRepository.AsQueryable()
                              on new { visit.OPDOutpatientExaminationNoteId, Code = "OPDOENTSKANS" } equals new { phm_OPDOENTSKANS.OPDOutpatientExaminationNoteId, phm_OPDOENTSKANS.Code } into phm_OPDOENTSKANS_list
                          from past_medical_history_OPDOENTSKANS in phm_OPDOENTSKANS_list.DefaultIfEmpty()

                          join hpi in unitOfWork.OPDOutpatientExaminationNoteDataRepository.AsQueryable()
                            on new { visit.OPDOutpatientExaminationNoteId, Code = "OPDOENHPIANS" } equals new { hpi.OPDOutpatientExaminationNoteId, hpi.Code } into hpi_list
                          from history_of_present_illness in hpi_list.DefaultIfEmpty()
                          
                          //join hoa in unitOfWork.OPDOutpatientExaminationNoteDataRepository.AsQueryable()
                          //  on new { visit.OPDOutpatientExaminationNoteId, Code = "OPDOENHOAANS" } equals new { hoa.OPDOutpatientExaminationNoteId, hoa.Code } into hoa_list
                          //from history_of_allergies in hoa_list.DefaultIfEmpty()
                          


                          join doc in unitOfWork.UserRepository.AsQueryable()
                            on visit.PrimaryDoctorId equals doc.Id into doctor_list
                          from doctor in doctor_list.DefaultIfEmpty()
                          
                          join clin in unitOfWork.ClinicRepository.AsQueryable()
                            on visit.ClinicId equals clin.Id into clin_list
                          from clinic in clin_list.DefaultIfEmpty()
                          
                          select new VisitModel()
                          {
                              ExaminationTime = visit.AdmittedDate,
                              Username = doctor.Username,
                              Fullname = doctor.Fullname,
                              ViName = clinic.ViName,
                              EnName = clinic.EnName,
                              ClinicCode = clinic.SetUpClinicDatas,
                              PastMedicalHistory = past_medical_history.Value,
                              FamilyMedicalHistory = "",
                              HistoryOfPresentIllness = history_of_present_illness.Value,
                              HistoryOfAllergies = visit.Allergy,
                              VisitCode = visit.VisitCode,
                              EHOSVisitCode = doctor.EHOSAccount + visit.VisitCode,
                              Type = "AD",
                              Sk_OPDOENTSKANS = past_medical_history_OPDOENTSKANS.Value,
                              Sk_OPDOENTSKNANS = past_medical_history_OPDOENTSKNANS.Value,
                              Sk_OPDOENTSSKANS = past_medical_history_OPDOENTSSKANS.Value
                          }).ToListNoLock();

            return reslut.GroupBy(e => e.ExaminationTime.ToString()).Select(e => e.FirstOrDefault()).ToList();
        }
        protected List<VisitModel> GetIPDHistory(Guid customer_id, DateTime visit_time)
        {
            var result = (from visit in unitOfWork.IPDRepository.AsQueryable().Where(
                        e => !e.IsDeleted &&
                        e.AdmittedDate < visit_time &&
                        e.CustomerId != null &&
                        e.CustomerId == customer_id &&
                        !string.IsNullOrEmpty(e.VisitCode)
                      )
                          join mere in unitOfWork.IPDMedicalRecordRepository.AsQueryable() on visit.IPDMedicalRecordId equals mere.Id
                          join phm in unitOfWork.IPDMedicalRecordPart2DataRepository.AsQueryable()
                              on new { mere.IPDMedicalRecordPart2Id, Code = "IPDMRPTBATHANS" } equals new { phm.IPDMedicalRecordPart2Id, phm.Code } into phm_list
                          from past_medical_history in phm_list.DefaultIfEmpty()
                          join hpi in unitOfWork.IPDMedicalRecordPart2DataRepository.AsQueryable()
                            on new { mere.IPDMedicalRecordPart2Id, Code = "IPDMRPTQTBLANS" } equals new { hpi.IPDMedicalRecordPart2Id, hpi.Code } into hpi_list
                          from history_of_present_illness in hpi_list.DefaultIfEmpty()
                          join famh in unitOfWork.IPDMedicalRecordPart2DataRepository.AsQueryable()
                            on new { mere.IPDMedicalRecordPart2Id, Code = "IPDMRPTGIDIANS" } equals new { famh.IPDMedicalRecordPart2Id, famh.Code } into famh_list
                          from family_medical_history in famh_list.DefaultIfEmpty()
                          join part_2 in unitOfWork.IPDMedicalRecordPart2Repository.AsQueryable() on mere.IPDMedicalRecordPart2Id equals part_2.Id
                          join doc in unitOfWork.UserRepository.AsQueryable()
                            on part_2.UpdatedBy equals doc.Username into doctor_list
                          from doctor in doctor_list.DefaultIfEmpty()
                          join spec in unitOfWork.SpecialtyRepository.AsQueryable()
                               on visit.SpecialtyId equals spec.Id into spec_list
                          from specialty in spec_list.DefaultIfEmpty()
                          select new VisitModel()
                          {
                              ExaminationTime = visit.AdmittedDate,
                              Username = doctor.Username,
                              Fullname = doctor.Fullname,
                              ViName = specialty.ViName,
                              EnName = specialty.EnName,
                              PastMedicalHistory = past_medical_history.Value,
                              FamilyMedicalHistory = family_medical_history.Value,
                              HistoryOfPresentIllness = history_of_present_illness.Value,
                              HistoryOfAllergies = visit.Allergy,
                              VisitCode = visit.VisitCode,
                              EHOSVisitCode = doctor.EHOSAccount + visit.VisitCode,
                              Type = "AD",
                          }).ToListNoLock();

            return result.GroupBy(e => e.ExaminationTime.ToString()).Select(e => e.FirstOrDefault()).ToList();
        }
        protected List<VisitModel> GetEDHistoryPAC(Guid customer_id, DateTime visit_time, string formCode)
        {
            var eds = (from visit in unitOfWork.EDRepository.AsQueryable().Where(
                       e => !e.IsDeleted &&
                       e.AdmittedDate < visit_time &&
                       e.CustomerId != null &&
                       e.CustomerId == customer_id &&
                       e.SpecialtyId != null &&
                       !string.IsNullOrEmpty(e.VisitCode)
                     )
                       join pac in unitOfWork.PreAnesthesiaConsultationRepository.AsQueryable() on visit.Id equals pac.VisitId
                       join formdata in unitOfWork.FormDatasRepository.AsQueryable()
                       on new { FormId = pac.Id, Code = "PRANCO41", VisitId = pac.VisitId, VisitType = "ED", FormCode = formCode + "_ED" } equals new { FormId = (Guid)formdata.FormId, formdata.Code, VisitId = (Guid)formdata.VisitId, formdata.VisitType, formdata.FormCode } into phm_list
                       from phm in phm_list.DefaultIfEmpty()
                      // join formdata in unitOfWork.FormDatasRepository.AsQueryable()
                      // on new { FormId = pac.Id, Code = "PRANCO59", VisitId = pac.VisitId, VisitType = "ED", FormCode = formCode + "_ED" } equals new { FormId = (Guid)formdata.FormId, formdata.Code, VisitId = (Guid)formdata.VisitId, formdata.VisitType, formdata.FormCode } into hoa_drug_list
                      // from hoa_drug in hoa_drug_list.DefaultIfEmpty()
                      // join formdata in unitOfWork.FormDatasRepository.AsQueryable()
                      //on new { FormId = pac.Id, Code = "PRANCO61", VisitId = pac.VisitId, VisitType = "ED", FormCode = formCode + "_ED" } equals new { FormId = (Guid)formdata.FormId, formdata.Code, VisitId = (Guid)formdata.VisitId, formdata.VisitType, formdata.FormCode } into hoa_food_list
                      // from hoa_food in hoa_food_list.DefaultIfEmpty()
                       join formdata in unitOfWork.FormDatasRepository.AsQueryable()
                       on new { FormId = pac.Id, Code = "PRANCO76", VisitId = pac.VisitId, VisitType = "ED", FormCode = formCode + "_ED" } equals new { FormId = (Guid)formdata.FormId, formdata.Code, VisitId = (Guid)formdata.VisitId, formdata.VisitType, formdata.FormCode } into ef_list
                       from ef in ef_list.DefaultIfEmpty()
                       join di in unitOfWork.DischargeInformationRepository.AsQueryable()
                               on visit.DischargeInformationId equals di.Id
                       join doc in unitOfWork.UserRepository.AsQueryable()
                         on di.UpdatedBy equals doc.Username into doctor_list
                       from doctor in doctor_list.DefaultIfEmpty()
                       join spec in unitOfWork.SpecialtyRepository.AsQueryable()
                         on visit.SpecialtyId equals spec.Id into spec_list
                       from specialty in spec_list.DefaultIfEmpty()
                       select new VisitModel()
                       {
                           ExaminationTime = visit.AdmittedDate,
                           Username = doctor.Username,
                           Fullname = doctor.Fullname,
                           ViName = specialty.ViName,
                           EnName = specialty.EnName,
                           PastMedicalHistory = phm.Value,
                           HistoryOfAllergies = visit.Allergy, //hoa_drug.Value != null ? (hoa_food.Value != null ? hoa_drug.Value + "," + hoa_food.Value : "") : hoa_food.Value != null ? hoa_food.Value : "",
                           HistoryOfExaminationFindings = ef.Value,
                           VisitCode = visit.VisitCode,
                           EHOSVisitCode = doctor.EHOSAccount + visit.VisitCode,
                           Type = "AD",
                       }).ToListNoLock();

            return eds.GroupBy(e => e.ExaminationTime.ToString()).Select(e => e.FirstOrDefault()).ToList();
        }
        protected List<VisitModel> GetOPDHistoryPAC(Guid customer_id, DateTime visit_time, string formCode)
        {
            var no_examination = unitOfWork.EDStatusRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                !string.IsNullOrEmpty(e.Code) &&
                Constant.NoExamination.Contains(e.Code) &&
                e.VisitTypeGroupId != null &&
                e.VisitTypeGroup.Code == "OPD"
            );

            var result = (from visit in unitOfWork.OPDRepository.AsQueryable().Where(
                       e => !e.IsDeleted &&
                       e.AdmittedDate < visit_time &&
                       e.CustomerId != null &&
                       e.CustomerId == customer_id &&
                       !string.IsNullOrEmpty(e.VisitCode) &&
                       e.EDStatusId != null &&
                       e.EDStatusId != no_examination.Id
                     )
                          join pac in unitOfWork.PreAnesthesiaConsultationRepository.AsQueryable() on visit.Id equals pac.VisitId
                          join formdata in unitOfWork.FormDatasRepository.AsQueryable()
                            on new { FormId = pac.Id, Code = "PRANCO41", VisitId = pac.VisitId, VisitType = "ED", FormCode = formCode + "_ED" } equals new { FormId = (Guid)formdata.FormId, formdata.Code, VisitId = (Guid)formdata.VisitId, formdata.VisitType, formdata.FormCode } into phm_list
                          from phm in phm_list.DefaultIfEmpty()
                         // join formdata in unitOfWork.FormDatasRepository.AsQueryable()
                         // on new { FormId = pac.Id, Code = "PRANCO59", VisitId = pac.VisitId, VisitType = "ED", FormCode = formCode + "_ED" } equals new { FormId = (Guid)formdata.FormId, formdata.Code, VisitId = (Guid)formdata.VisitId, formdata.VisitType, formdata.FormCode } into hoa_drug_list
                         // from hoa_drug in hoa_drug_list.DefaultIfEmpty()
                         // join formdata in unitOfWork.FormDatasRepository.AsQueryable()
                         //on new { FormId = pac.Id, Code = "PRANCO61", VisitId = pac.VisitId, VisitType = "ED", FormCode = formCode + "_ED" } equals new { FormId = (Guid)formdata.FormId, formdata.Code, VisitId = (Guid)formdata.VisitId, formdata.VisitType, formdata.FormCode } into hoa_food_list
                         // from hoa_food in hoa_food_list.DefaultIfEmpty()
                          join formdata in unitOfWork.FormDatasRepository.AsQueryable()
                          on new { FormId = pac.Id, Code = "PRANCO76", VisitId = pac.VisitId, VisitType = "ED", FormCode = formCode + "_ED" } equals new { FormId = (Guid)formdata.FormId, formdata.Code, VisitId = (Guid)formdata.VisitId, formdata.VisitType, formdata.FormCode } into ef_list
                          from ef in ef_list.DefaultIfEmpty()
                          join doc in unitOfWork.UserRepository.AsQueryable()
                            on visit.PrimaryDoctorId equals doc.Id into doctor_list
                          from doctor in doctor_list.DefaultIfEmpty()
                          join clin in unitOfWork.ClinicRepository.AsQueryable()
                            on visit.ClinicId equals clin.Id into clin_list
                          from clinic in clin_list.DefaultIfEmpty()
                          select new VisitModel()
                          {
                              ExaminationTime = visit.AdmittedDate,
                              Username = doctor.Username,
                              Fullname = doctor.Fullname,
                              ViName = clinic.ViName,
                              EnName = clinic.EnName,
                              PastMedicalHistory = phm.Value,
                              HistoryOfAllergies = visit.Allergy,  //hoa_drug.Value != null ? (hoa_food.Value != null ? hoa_drug.Value + "," + hoa_food.Value : "") : hoa_food.Value != null ? hoa_food.Value : "",
                              HistoryOfExaminationFindings = ef.Value,
                              VisitCode = visit.VisitCode,
                              EHOSVisitCode = doctor.EHOSAccount + visit.VisitCode,
                              Type = "AD",
                          }).ToListNoLock();

            return result.GroupBy(e => e.ExaminationTime.ToString()).Select(e => e.FirstOrDefault()).ToList();
        }
        protected List<VisitModel> GetIPDHistoryPAC(Guid customer_id, DateTime visit_time, string formCode)
        {
            var result = (from visit in unitOfWork.IPDRepository.AsQueryable().Where(
                       e => !e.IsDeleted &&
                       e.AdmittedDate < visit_time &&
                       e.CustomerId != null &&
                       e.CustomerId == customer_id &&
                       !string.IsNullOrEmpty(e.VisitCode)
                     )
                          join pac in unitOfWork.PreAnesthesiaConsultationRepository.AsQueryable() on visit.Id equals pac.VisitId
                          join formdata in unitOfWork.FormDatasRepository.AsQueryable()
                            on new { FormId = pac.Id, Code = "PRANCO41", VisitId = pac.VisitId, VisitType = "ED", FormCode = formCode + "_ED" } equals new { FormId = (Guid)formdata.FormId, formdata.Code, VisitId = (Guid)formdata.VisitId, formdata.VisitType, formdata.FormCode } into phm_list
                          from phm in phm_list.DefaultIfEmpty()
                         // join formdata in unitOfWork.FormDatasRepository.AsQueryable()
                         // on new { FormId = pac.Id, Code = "PRANCO59", VisitId = pac.VisitId, VisitType = "ED", FormCode = formCode + "_ED" } equals new { FormId = (Guid)formdata.FormId, formdata.Code, VisitId = (Guid)formdata.VisitId, formdata.VisitType, formdata.FormCode } into hoa_drug_list
                         // from hoa_drug in hoa_drug_list.DefaultIfEmpty()
                         // join formdata in unitOfWork.FormDatasRepository.AsQueryable()
                         //on new { FormId = pac.Id, Code = "PRANCO61", VisitId = pac.VisitId, VisitType = "ED", FormCode = formCode + "_ED" } equals new { FormId = (Guid)formdata.FormId, formdata.Code, VisitId = (Guid)formdata.VisitId, formdata.VisitType, formdata.FormCode } into hoa_food_list
                         // from hoa_food in hoa_food_list.DefaultIfEmpty()
                          join formdata in unitOfWork.FormDatasRepository.AsQueryable()
                          on new { FormId = pac.Id, Code = "PRANCO76", VisitId = pac.VisitId, VisitType = "ED", FormCode = formCode + "_ED" } equals new { FormId = (Guid)formdata.FormId, formdata.Code, VisitId = (Guid)formdata.VisitId, formdata.VisitType, formdata.FormCode } into ef_list
                          from ef in ef_list.DefaultIfEmpty()
                          join doc in unitOfWork.UserRepository.AsQueryable()
                            on pac.UpdatedBy equals doc.Username into doctor_list
                          from doctor in doctor_list.DefaultIfEmpty()
                          join spec in unitOfWork.SpecialtyRepository.AsQueryable()
                               on visit.SpecialtyId equals spec.Id into spec_list
                          from specialty in spec_list.DefaultIfEmpty()
                          select new VisitModel()
                          {
                              ExaminationTime = visit.AdmittedDate,
                              Username = doctor.Username,
                              Fullname = doctor.Fullname,
                              ViName = specialty.ViName,
                              EnName = specialty.EnName,
                              PastMedicalHistory = phm.Value,
                              HistoryOfAllergies = visit.Allergy, //hoa_drug.Value != null ? (hoa_food.Value != null ? hoa_drug.Value + "," + hoa_food.Value : "") : hoa_food.Value != null ? hoa_food.Value : "",
                              HistoryOfExaminationFindings = ef.Value,
                              VisitCode = visit.VisitCode,
                              EHOSVisitCode = doctor.EHOSAccount + visit.VisitCode,
                          }).ToListNoLock();

            return result.GroupBy(e => e.ExaminationTime.ToString()).Select(e => e.FirstOrDefault()).ToList();
        }
        protected List<VisitModel> GetEOCHistoryPAC(Guid customer_id, DateTime visit_time, string formCode)
        {
            var result = (from visit in unitOfWork.EOCRepository.AsQueryable().Where(
                       e => !e.IsDeleted &&
                       e.AdmittedDate < visit_time &&
                       e.CustomerId != null &&
                       e.CustomerId == customer_id &&
                       !string.IsNullOrEmpty(e.VisitCode)
                     )
                          join pac in unitOfWork.PreAnesthesiaConsultationRepository.AsQueryable() on visit.Id equals pac.VisitId
                          join formdata in unitOfWork.FormDatasRepository.AsQueryable()
                            on new { FormId = pac.Id, Code = "PRANCO41", VisitId = pac.VisitId, VisitType = "ED", FormCode = formCode + "_ED" } equals new { FormId = (Guid)formdata.FormId, formdata.Code, VisitId = (Guid)formdata.VisitId, formdata.VisitType, formdata.FormCode } into phm_list
                          from phm in phm_list.DefaultIfEmpty()
                         // join formdata in unitOfWork.FormDatasRepository.AsQueryable()
                         // on new { FormId = pac.Id, Code = "PRANCO59", VisitId = pac.VisitId, VisitType = "ED", FormCode = formCode + "_ED" } equals new { FormId = (Guid)formdata.FormId, formdata.Code, VisitId = (Guid)formdata.VisitId, formdata.VisitType, formdata.FormCode } into hoa_drug_list
                         // from hoa_drug in hoa_drug_list.DefaultIfEmpty()
                         // join formdata in unitOfWork.FormDatasRepository.AsQueryable()
                         //on new { FormId = pac.Id, Code = "PRANCO61", VisitId = pac.VisitId, VisitType = "ED", FormCode = formCode + "_ED" } equals new { FormId = (Guid)formdata.FormId, formdata.Code, VisitId = (Guid)formdata.VisitId, formdata.VisitType, formdata.FormCode } into hoa_food_list
                         // from hoa_food in hoa_food_list.DefaultIfEmpty()
                          join formdata in unitOfWork.FormDatasRepository.AsQueryable()
                          on new { FormId = pac.Id, Code = "PRANCO76", VisitId = pac.VisitId, VisitType = "ED", FormCode = formCode + "_ED" } equals new { FormId = (Guid)formdata.FormId, formdata.Code, VisitId = (Guid)formdata.VisitId, formdata.VisitType, formdata.FormCode } into ef_list
                          from ef in ef_list.DefaultIfEmpty()
                          join doc in unitOfWork.UserRepository.AsQueryable()
                            on pac.UpdatedBy equals doc.Username into doctor_list
                          from doctor in doctor_list.DefaultIfEmpty()
                          join spec in unitOfWork.SpecialtyRepository.AsQueryable()
                               on visit.SpecialtyId equals spec.Id into spec_list
                          from specialty in spec_list.DefaultIfEmpty()
                          select new VisitModel()
                          {
                              ExaminationTime = visit.AdmittedDate,
                              Username = doctor.Username,
                              Fullname = doctor.Fullname,
                              ViName = specialty.ViName,
                              EnName = specialty.EnName,
                              PastMedicalHistory = phm.Value,
                              HistoryOfAllergies = visit.Allergy, //hoa_drug.Value != null ? (hoa_food.Value != null ? hoa_drug.Value + "," + hoa_food.Value : "") : hoa_food.Value != null ? hoa_food.Value : "",
                              HistoryOfExaminationFindings = ef.Value,
                              VisitCode = visit.VisitCode,
                              EHOSVisitCode = doctor.EHOSAccount + visit.VisitCode,
                          }).ToListNoLock();

            return result.GroupBy(e => e.ExaminationTime.ToString()).Select(e => e.FirstOrDefault()).ToList();
        }
        protected List<VisitModel> GetHistoryPainRecord(Guid customer_id, DateTime visit_time, string formCode)
        {
            var a = (from visit in unitOfWork.IPDRepository.AsQueryable().Where(
                        e => !e.IsDeleted &&
                        e.AdmittedDate < visit_time &&
                        e.CustomerId != null &&
                        e.CustomerId == customer_id &&
                        !string.IsNullOrEmpty(e.VisitCode)
                      )
                     join painrecord in unitOfWork.IPDPainRecordRepository.AsQueryable() on visit.Id equals painrecord.VisitId
                     join formdata in unitOfWork.FormDatasRepository.AsQueryable()
                     on new { FormId = painrecord.Id, Code = "IPDMRPTBATHANS", VisitId = painrecord.VisitId, VisitType = "IPD", FormCode = formCode } equals new { FormId = (Guid)formdata.FormId, formdata.Code, VisitId = formdata.VisitId, formdata.VisitType, formdata.FormCode } into pmh_list
                     from past_medical_history in pmh_list.DefaultIfEmpty()

                     select new VisitModel()
                     {
                         PastMedicalHistory = past_medical_history.Value,
                     }).ToList();

            var result = (from visit in unitOfWork.IPDRepository.AsQueryable().Where(
                       e => !e.IsDeleted &&
                       e.AdmittedDate < visit_time &&
                       e.CustomerId != null &&
                       e.CustomerId == customer_id &&
                       !string.IsNullOrEmpty(e.VisitCode)
                     )
                          join painrecord in unitOfWork.IPDPainRecordRepository.AsQueryable() on visit.Id equals painrecord.VisitId
                          join formdata in unitOfWork.FormDatasRepository.AsQueryable()
                          on new { FormId = painrecord.Id, Code = "IPDMRPTBATHANS", VisitId = painrecord.VisitId, VisitType = "IPD", FormCode = formCode } equals new { FormId = (Guid)formdata.FormId, formdata.Code, VisitId = formdata.VisitId, formdata.VisitType, formdata.FormCode } into pmh_list
                          from past_medical_history in pmh_list.DefaultIfEmpty()
                          //join formdata in unitOfWork.FormDatasRepository.AsQueryable()
                          //on new { FormId = painrecord.Id, Code = "DEF", VisitId = painrecord.VisitId, VisitType = "IPD", FormCode = formCode } equals new { FormId = (Guid)formdata.FormId, formdata.Code, VisitId = formdata.VisitId, formdata.VisitType, formdata.FormCode } into hoa_list
                          //from history_of_allergies in hoa_list.DefaultIfEmpty()
                          join doc in unitOfWork.UserRepository.AsQueryable()
                            on painrecord.UpdatedBy equals doc.Username into doctor_list
                          from doctor in doctor_list.DefaultIfEmpty()
                          join spec in unitOfWork.SpecialtyRepository.AsQueryable()
                               on visit.SpecialtyId equals spec.Id into spec_list
                          from specialty in spec_list.DefaultIfEmpty()
                          select new VisitModel()
                          {
                              ExaminationTime = visit.AdmittedDate,
                              Username = doctor.Username,
                              Fullname = doctor.Fullname,
                              ViName = specialty.ViName,
                              EnName = specialty.EnName,
                              PastMedicalHistory = past_medical_history.Value,
                              FamilyMedicalHistory = "",
                              HistoryOfPresentIllness = "",
                              HistoryOfAllergies = visit.Allergy, //history_of_allergies.Value,
                              VisitCode = visit.VisitCode,
                              EHOSVisitCode = doctor.EHOSAccount + visit.VisitCode,
                              Type = "AD",
                          }).ToListNoLock();

            return result.GroupBy(e => e.ExaminationTime.ToString()).Select(e => e.FirstOrDefault()).ToList();
        }
        // lấy lịch sử khám chuyên khoa
        public List<VisitModel> GetOPDHistorySpecialistExamination(Guid customer_id, DateTime visit_time)
        {
            var no_examination = unitOfWork.EDStatusRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                !string.IsNullOrEmpty(e.Code) &&
                Constant.NoExamination.Contains(e.Code) &&
                e.VisitTypeGroupId != null &&
                e.VisitTypeGroup.Code == "OPD"
            );

            return (from visit in unitOfWork.OPDRepository.AsQueryable().Where(
                        e => !e.IsDeleted &&
                        e.AdmittedDate < visit_time &&
                        e.CustomerId != null &&
                        e.CustomerId == customer_id &&
                        !string.IsNullOrEmpty(e.VisitCode) &&
                        e.EDStatusId != null &&
                        e.EDStatusId != no_examination.Id
                      )
                    join spec in unitOfWork.OPDOutpatientExaminationNoteDataRepository.AsQueryable()
                        on new { visit.OPDOutpatientExaminationNoteId, Code = "OPDOENKCKANS" } equals new { spec.OPDOutpatientExaminationNoteId, spec.Code } into spec_list
                    from specialist_examination in spec_list.DefaultIfEmpty()
                    join doc in unitOfWork.UserRepository.AsQueryable()
                      on visit.PrimaryDoctorId equals doc.Id into doctor_list
                    from doctor in doctor_list.DefaultIfEmpty()
                    join clin in unitOfWork.ClinicRepository.AsQueryable()
                      on visit.ClinicId equals clin.Id into clin_list
                    from clinic in clin_list.DefaultIfEmpty()
                    select new VisitModel()
                    {
                        ExaminationTime = visit.AdmittedDate,
                        Username = doctor.Username,
                        Fullname = doctor.Fullname,
                        ViName = clinic.ViName,
                        EnName = clinic.EnName,
                        VisitCode = visit.VisitCode,
                        SpecialistExamination = specialist_examination.Value
                    }).Where(x => !string.IsNullOrEmpty(x.SpecialistExamination)).ToListNoLock().OrderByDescending(x => x.ExaminationTime).ToList();
        }
        public List<VisitModel> GetOPDForCoronaryDiseases(Guid customer_id, DateTime visit_time)
        {
            var no_examination = unitOfWork.EDStatusRepository.FirstOrDefault(
               e => !e.IsDeleted &&
               !string.IsNullOrEmpty(e.Code) &&
               Constant.NoExamination.Contains(e.Code) &&
               e.VisitTypeGroupId != null &&
               e.VisitTypeGroup.Code == "OPD"
           );
            return (from visit in unitOfWork.OPDRepository.AsQueryable().Where(
                        e => !e.IsDeleted &&
                        e.CustomerId != null &&
                        e.CustomerId == customer_id &&
                        e.AdmittedDate <= visit_time &&
                        !string.IsNullOrEmpty(e.VisitCode) &&
                        e.EDStatusId != null &&
                        e.EDStatusId != no_examination.Id
                      )
                    join forheart in unitOfWork.PROMForCoronaryDiseaseRepository.AsQueryable().Where(x => x.VisitType == "OPD" && !x.IsDeleted)
                       on visit.Id equals forheart.VisitId
                    join formdatas in unitOfWork.FormDatasRepository.AsQueryable().Where(x => x.Code == "PROMFCD71"
                                                                                         || x.Code == "PROMFCD72"
                                                                                         || x.Code == "PROMFCD73"
                                                                                         || x.Code == "PROMFCD74"
                                                                                         && x.FormCode == "PROMFCD")
                   on new { forheart.VisitId, FormId = (Guid)forheart.Id } equals new { formdatas.VisitId, FormId = (Guid)formdatas.FormId } into datas
                    join doc in unitOfWork.UserRepository.AsQueryable().Where(x => !x.IsDeleted && string.IsNullOrEmpty(x.Username) == false)
                       on visit.PrimaryDoctorId equals doc.Id into doctor_list
                    from doctor in doctor_list.DefaultIfEmpty()
                    join clin in unitOfWork.ClinicRepository.AsQueryable()
                      on visit.ClinicId equals clin.Id into clin_list
                    from clinic in clin_list.DefaultIfEmpty()
                    select new VisitModel
                    {
                        Id = visit.Id,
                        ViName = clinic.ViName,
                        EnName = clinic.EnName,
                        CreatedAt = forheart.CreatedAt,
                        VisitTypeGroup = visit.Specialty.VisitTypeGroup.Code,
                        Username = forheart.CreatedBy,
                        VisitCode = visit.VisitCode,
                        PromSumaries = datas.Select(x => x.Value).ToList()
                    }
                     ).ToListNoLock();
        }
        public List<VisitModel> GetCurrentOPDForCoronaryDiseases(Guid visitId)
        {
            var no_examination = unitOfWork.EDStatusRepository.FirstOrDefault(
               e => !e.IsDeleted &&
               !string.IsNullOrEmpty(e.Code) &&
               Constant.NoExamination.Contains(e.Code) &&
               e.VisitTypeGroupId != null &&
               e.VisitTypeGroup.Code == "OPD"
           );
            return (from visit in unitOfWork.OPDRepository.AsQueryable().Where(
                        e => !e.IsDeleted &&
                        e.Id != null &&
                        e.Id == visitId &&
                        !string.IsNullOrEmpty(e.VisitCode) &
                        e.EDStatusId != null &&
                        e.EDStatusId != no_examination.Id
                      )
                    join forheart in unitOfWork.PROMForCoronaryDiseaseRepository.AsQueryable().Where(x => x.VisitType == "OPD" && !x.IsDeleted)
                       on visit.Id equals forheart.VisitId
                    join formdatas in unitOfWork.FormDatasRepository.AsQueryable().Where(x => x.Code == "PROMFCD71"
                                                                                         || x.Code == "PROMFCD72"
                                                                                         || x.Code == "PROMFCD73"
                                                                                         || x.Code == "PROMFCD74"
                                                                                         && x.FormCode == "PROMFCD")
                   on new { forheart.VisitId, FormId = (Guid)forheart.Id } equals new { formdatas.VisitId, FormId = (Guid)formdatas.FormId } into datas
                    join doc in unitOfWork.UserRepository.AsQueryable().Where(x => !x.IsDeleted && string.IsNullOrEmpty(x.Username) == false)
                       on visit.PrimaryDoctorId equals doc.Id into doctor_list
                    from doctor in doctor_list.DefaultIfEmpty()
                    join clin in unitOfWork.ClinicRepository.AsQueryable()
                      on visit.ClinicId equals clin.Id into clin_list
                    from clinic in clin_list.DefaultIfEmpty()
                    select new VisitModel
                    {
                        Id = visit.Id,
                        ViName = clinic.ViName,
                        EnName = clinic.EnName,
                        CreatedAt = forheart.CreatedAt,
                        VisitTypeGroup = visit.Specialty.VisitTypeGroup.Code,
                        Username = forheart.CreatedBy,
                        VisitCode = visit.VisitCode,
                        PromSumaries = datas.Select(x => x.Value).ToList()
                    }
                     ).ToListNoLock();
        }
        public List<VisitModel> GetIPDForCoronaryDiseases(Guid customer_id, DateTime visit_time)
        {
            return (from visit in unitOfWork.IPDRepository.AsQueryable().Where(
                        e => !e.IsDeleted &&
                        e.CustomerId != null &&
                        e.CustomerId == customer_id &&
                        e.AdmittedDate <= visit_time
                      )
                    join forheart in unitOfWork.PROMForCoronaryDiseaseRepository.AsQueryable().Where(x => x.VisitType == "IPD" && !x.IsDeleted)
                        on visit.Id equals forheart.VisitId
                    join formdatas in unitOfWork.FormDatasRepository.AsQueryable().Where(x => x.Code == "PROMFCD71"
                                                                                         || x.Code == "PROMFCD72"
                                                                                         || x.Code == "PROMFCD73"
                                                                                         || x.Code == "PROMFCD74"
                                                                                         && x.FormCode == "PROMFCD")
                   on new { forheart.VisitId, FormId = (Guid)forheart.Id } equals new { formdatas.VisitId, FormId = (Guid)formdatas.FormId } into datas
                    join doc in unitOfWork.UserRepository.AsQueryable().Where(x => !x.IsDeleted && string.IsNullOrEmpty(x.Username) == false)
                             on forheart.UpdatedBy equals doc.Username into doctor_list
                    from doctor in doctor_list.DefaultIfEmpty()
                    join spec in unitOfWork.SpecialtyRepository.AsQueryable()
                         on visit.SpecialtyId equals spec.Id into spec_list
                    from specialty in spec_list.DefaultIfEmpty()
                    select new VisitModel
                    {
                        Id = visit.Id,
                        ViName = specialty.ViName,
                        EnName = specialty.EnName,
                        CreatedAt = forheart.CreatedAt,
                        VisitTypeGroup = visit.Specialty.VisitTypeGroup.Code,
                        Username = forheart.CreatedBy,
                        VisitCode = visit.VisitCode,
                        PromSumaries = datas.Select(x => x.Value).ToList()
                    }
                     ).ToListNoLock();
        }
        public List<VisitModel> GetCurrentIPDForCoronaryDiseases(Guid visitId)
        {
            return (from visit in unitOfWork.IPDRepository.AsQueryable().Where(
                        e => !e.IsDeleted &&
                        e.Id != null &&
                        e.Id == visitId
                      )
                    join forheart in unitOfWork.PROMForCoronaryDiseaseRepository.AsQueryable().Where(x => x.VisitType == "IPD" && !x.IsDeleted)
                        on visit.Id equals forheart.VisitId
                    join formdatas in unitOfWork.FormDatasRepository.AsQueryable().Where(x => x.Code == "PROMFCD71"
                                                                                         || x.Code == "PROMFCD72"
                                                                                         || x.Code == "PROMFCD73"
                                                                                         || x.Code == "PROMFCD74"
                                                                                         && x.FormCode == "PROMFCD")
                   on new { forheart.VisitId, FormId = (Guid)forheart.Id } equals new { formdatas.VisitId, FormId = (Guid)formdatas.FormId } into datas
                    join doc in unitOfWork.UserRepository.AsQueryable().Where(x => !x.IsDeleted && string.IsNullOrEmpty(x.Username) == false)
                             on forheart.UpdatedBy equals doc.Username into doctor_list
                    from doctor in doctor_list.DefaultIfEmpty()
                    join spec in unitOfWork.SpecialtyRepository.AsQueryable()
                         on visit.SpecialtyId equals spec.Id into spec_list
                    from specialty in spec_list.DefaultIfEmpty()
                    select new VisitModel
                    {
                        Id = visit.Id,
                        ViName = specialty.ViName,
                        EnName = specialty.EnName,
                        CreatedAt = forheart.CreatedAt,
                        VisitTypeGroup = visit.Specialty.VisitTypeGroup.Code,
                        Username = forheart.CreatedBy,
                        VisitCode = visit.VisitCode,
                        PromSumaries = datas.Select(x => x.Value).ToList()
                    }
                     ).ToListNoLock();
        }
        public List<VisitModel> GetOPDForheartFailures(Guid customer_id, DateTime visit_time)
        {
            var no_examination = unitOfWork.EDStatusRepository.FirstOrDefault(
               e => !e.IsDeleted &&
               !string.IsNullOrEmpty(e.Code) &&
               Constant.NoExamination.Contains(e.Code) &&
               e.VisitTypeGroupId != null &&
               e.VisitTypeGroup.Code == "OPD"
           );
            return (from visit in unitOfWork.OPDRepository.AsQueryable().Where(
                        e => !e.IsDeleted &&
                        e.CustomerId != null &&
                        e.CustomerId == customer_id &&
                        !string.IsNullOrEmpty(e.VisitCode) &&
                        e.AdmittedDate <= visit_time &&
                        e.EDStatusId != null &&
                        e.EDStatusId != no_examination.Id
                      )
                    join forheart in unitOfWork.PROMForheartFailureRepository.AsQueryable().Where(x => x.VisitType == "OPD" && !x.IsDeleted)
                       on visit.Id equals forheart.VisitId
                    join formdatas in unitOfWork.FormDatasRepository.AsQueryable().Where(x => x.Code == "PROMFHFDATACD1"
                                                                                         || x.Code == "PROMFHFDATACD2"
                                                                                         || x.Code == "PROMFHFDATACD3"
                                                                                         || x.Code == "PROMFHFDATACD4"
                                                                                         || x.Code == "PROMFHFDATACD5"
                                                                                         || x.Code == "PROMFHFDATACD6"
                                                                                         && x.FormCode == "PROMFHF")
                           on new { forheart.VisitId, FormId = (Guid)forheart.Id } equals new { formdatas.VisitId, FormId = (Guid)formdatas.FormId } into datas
                    join doc in unitOfWork.UserRepository.AsQueryable().Where(x => !x.IsDeleted && string.IsNullOrEmpty(x.Username) == false)
                        on visit.PrimaryDoctorId equals doc.Id into doctor_list
                    from doctor in doctor_list.DefaultIfEmpty()
                    join clin in unitOfWork.ClinicRepository.AsQueryable()
                      on visit.ClinicId equals clin.Id into clin_list
                    from clinic in clin_list.DefaultIfEmpty()
                    select new VisitModel
                    {
                        Id = visit.Id,
                        ViName = clinic.ViName,
                        EnName = clinic.EnName,
                        CreatedAt = forheart.CreatedAt,
                        VisitTypeGroup = visit.Specialty.VisitTypeGroup.Code,
                        Username = forheart.CreatedBy,
                        VisitCode = visit.VisitCode,
                        PromSumaries =  datas.Select(x => x.Value).ToList()
                    }
                     ).ToListNoLock();
        }
        public List<VisitModel> GetCurrentOPDForheartFailures(Guid visitId)
        {
            var no_examination = unitOfWork.EDStatusRepository.FirstOrDefault(
               e => !e.IsDeleted &&
               !string.IsNullOrEmpty(e.Code) &&
               Constant.NoExamination.Contains(e.Code) &&
               e.VisitTypeGroupId != null &&
               e.VisitTypeGroup.Code == "OPD"
           );
            return (from visit in unitOfWork.OPDRepository.AsQueryable().Where(
                        e => !e.IsDeleted &&
                        e.Id != null &&
                        e.Id == visitId &&
                        !string.IsNullOrEmpty(e.VisitCode) &&
                        e.EDStatusId != null &&
                        e.EDStatusId != no_examination.Id
                      )
                    join forheart in unitOfWork.PROMForheartFailureRepository.AsQueryable().Where(x => x.VisitType == "OPD" && !x.IsDeleted)
                       on visit.Id equals forheart.VisitId
                    join formdatas in unitOfWork.FormDatasRepository.AsQueryable().Where(x => x.Code == "PROMFHFDATACD1"
                                                                                         || x.Code == "PROMFHFDATACD2"
                                                                                         || x.Code == "PROMFHFDATACD3"
                                                                                         || x.Code == "PROMFHFDATACD4"
                                                                                         || x.Code == "PROMFHFDATACD5"
                                                                                         || x.Code == "PROMFHFDATACD6"
                                                                                         && x.FormCode == "PROMFHF")
                           on new { forheart.VisitId, FormId = (Guid)forheart.Id } equals new { formdatas.VisitId, FormId = (Guid)formdatas.FormId } into datas
                    join doc in unitOfWork.UserRepository.AsQueryable().Where(x => !x.IsDeleted && string.IsNullOrEmpty(x.Username) == false)
                        on visit.PrimaryDoctorId equals doc.Id into doctor_list
                    from doctor in doctor_list.DefaultIfEmpty()
                    join clin in unitOfWork.ClinicRepository.AsQueryable()
                      on visit.ClinicId equals clin.Id into clin_list
                    from clinic in clin_list.DefaultIfEmpty()
                    select new VisitModel
                    {
                        Id = visit.Id,
                        ViName = clinic.ViName,
                        EnName = clinic.EnName,
                        CreatedAt = forheart.CreatedAt,
                        VisitTypeGroup = visit.Specialty.VisitTypeGroup.Code,
                        Username = forheart.CreatedBy,
                        VisitCode = visit.VisitCode,
                        PromSumaries = datas.Select(x => x.Value).ToList()
                    }
                     ).ToListNoLock();
        }
        public List<VisitModel> GetIPDForheartFailures(Guid customer_id, DateTime visit_time)
        {
            return (from visit in unitOfWork.IPDRepository.AsQueryable().Where(
                        e => !e.IsDeleted &&
                        e.CustomerId != null &&
                        e.CustomerId == customer_id &&
                        e.AdmittedDate <= visit_time
                      )
                    join forheart in unitOfWork.PROMForheartFailureRepository.AsQueryable().Where(x => x.VisitType == "IPD")
                        on visit.Id equals forheart.VisitId
                    join formdatas in unitOfWork.FormDatasRepository.AsQueryable().Where(x => x.Code == "PROMFHFDATACD1"
                                                                                         || x.Code == "PROMFHFDATACD2"
                                                                                         || x.Code == "PROMFHFDATACD3"
                                                                                         || x.Code == "PROMFHFDATACD4"
                                                                                         || x.Code == "PROMFHFDATACD5"
                                                                                         || x.Code == "PROMFHFDATACD6"
                                                                                         && x.FormCode == "PROMFHF")
                   on new { forheart.VisitId, FormId = (Guid)forheart.Id } equals new { formdatas.VisitId, FormId = (Guid)formdatas.FormId } into datas
                    join doc in unitOfWork.UserRepository.AsQueryable().Where(x => !x.IsDeleted && string.IsNullOrEmpty(x.Username) == false)
                             on forheart.UpdatedBy equals doc.Username into doctor_list
                    from doctor in doctor_list.DefaultIfEmpty()
                    join spec in unitOfWork.SpecialtyRepository.AsQueryable()
                         on visit.SpecialtyId equals spec.Id into spec_list
                    from specialty in spec_list.DefaultIfEmpty()
                    select new VisitModel
                    {
                        Id = visit.Id,
                        ViName = specialty.ViName,
                        EnName = specialty.EnName,
                        CreatedAt = forheart.CreatedAt,
                        VisitTypeGroup = visit.Specialty.VisitTypeGroup.Code,
                        Username = forheart.CreatedBy,
                        VisitCode = visit.VisitCode,
                        PromSumaries = datas.Select(x => x.Value).ToList()
                    }
                     ).ToListNoLock();

        }
        public List<VisitModel> GetCurrentIPDForheartFailures(Guid visitId)
        {

            return (from visit in unitOfWork.IPDRepository.AsQueryable().Where(
                        e => !e.IsDeleted &&
                        e.Id == visitId
                   )
                    join forheart in unitOfWork.PROMForheartFailureRepository.AsQueryable().Where(x => x.VisitType == "IPD" && !x.IsDeleted)
                        on visit.Id equals forheart.VisitId
                    join formdatas in unitOfWork.FormDatasRepository.AsQueryable().Where(x => x.Code == "PROMFHFDATACD1"
                                                                                         || x.Code == "PROMFHFDATACD2"
                                                                                         || x.Code == "PROMFHFDATACD3"
                                                                                         || x.Code == "PROMFHFDATACD4"
                                                                                         || x.Code == "PROMFHFDATACD5"
                                                                                         || x.Code == "PROMFHFDATACD6"
                                                                                         && x.FormCode == "PROMFHF")
                   on new { forheart.VisitId, FormId = (Guid)forheart.Id } equals new { formdatas.VisitId, FormId = (Guid)formdatas.FormId } into datas
                    join doc in unitOfWork.UserRepository.AsQueryable().Where(x => !x.IsDeleted && string.IsNullOrEmpty(x.Username) == false)
                             on forheart.UpdatedBy equals doc.Username into doctor_list
                    from doctor in doctor_list.DefaultIfEmpty()
                    join spec in unitOfWork.SpecialtyRepository.AsQueryable()
                         on visit.SpecialtyId equals spec.Id into spec_list
                    from specialty in spec_list.DefaultIfEmpty()
                    select new VisitModel
                    {
                        Id = visit.Id,
                        ViName = specialty.ViName,
                        EnName = specialty.EnName,
                        CreatedAt = forheart.CreatedAt,
                        VisitTypeGroup = visit.Specialty.VisitTypeGroup.Code,
                        Username = forheart.CreatedBy,
                        VisitCode = visit.VisitCode,
                        PromSumaries = datas.Select(x => x.Value).ToList()
                    }
                     ).ToListNoLock();

        }
        // lấy tiền sử bệnh gia đình
        public List<VisitModel> GetCurrentIPDPersonalHistory(Guid customer_id, DateTime visit_time)
        {
            var personalHistory = (from visit in unitOfWork.IPDRepository.AsQueryable().Where(
                        e => !e.IsDeleted &&
                        e.CustomerId != null &&
                        e.CustomerId == customer_id &&
                        e.AdmittedDate <= visit_time
                      )
                    join medicalrecord in unitOfWork.IPDMedicalRecordRepository.AsQueryable().Where(x => !x.IsDeleted)
                        on visit.IPDMedicalRecordId equals medicalrecord.Id
                    join part2 in unitOfWork.IPDMedicalRecordPart2Repository.AsQueryable().Where(x => !x.IsDeleted)
                       on medicalrecord.IPDMedicalRecordPart2Id equals part2.Id
                    join part2data in unitOfWork.IPDMedicalRecordPart2DataRepository.AsQueryable().Where(x => !x.IsDeleted && (x.Code == "IPDMRPTGIDIANS" || x.Code == "IPDMRPT1060" || x.Code == "IPDMRPT1062")) 
                    on part2.Id equals part2data.IPDMedicalRecordPart2Id into datas

                    join doc in unitOfWork.UserRepository.AsQueryable().Where(x => !x.IsDeleted && string.IsNullOrEmpty(x.Username) == false)
                       on visit.PrimaryDoctorId equals doc.Id into doctor_list
                    from doctor in doctor_list.DefaultIfEmpty()
                    join spec in unitOfWork.SpecialtyRepository.AsQueryable()
                         on visit.SpecialtyId equals spec.Id into spec_list
                    from specialty in spec_list.DefaultIfEmpty()
                    select new VisitModel
                    {
                        Id = visit.Id,
                        ViName = specialty.ViName,
                        EnName = specialty.EnName,
                        CreatedAt = part2.CreatedAt,
                        VisitTypeGroup = visit.Specialty.VisitTypeGroup.Code,
                        Username = doctor.Username,
                        VisitCode = visit.VisitCode,
                        FamilyMedicalHistoryData = datas.Select(x => x.Value).ToList<string>(),
                        Fullname = doctor.Fullname,
                        ExaminationTime = visit.AdmittedDate
                    }
                     ).ToListNoLock();
            for(int i = 0; i < personalHistory.Count; i++)
            {
                personalHistory[i].FamilyMedicalHistory = (personalHistory[i].FamilyMedicalHistoryData.Count == 0) ? "" : personalHistory[i].FamilyMedicalHistoryData.Aggregate((s1, s2) => s1 + "\n" + s2);
            }
            return personalHistory;
        }
        public List<VisitModel> GetCurrentOPDPersonalHistory(Guid customer_id, Guid visitId, DateTime visit_time)
        {
            var no_examination = unitOfWork.EDStatusRepository.FirstOrDefault(
               e => !e.IsDeleted &&
               !string.IsNullOrEmpty(e.Code) &&
               Constant.NoExamination.Contains(e.Code) &&
               e.VisitTypeGroupId != null &&
               e.VisitTypeGroup.Code == "OPD"
           );
            return (from visit in unitOfWork.OPDRepository.AsQueryable().Where(
                        e => !e.IsDeleted &&
                        e.CustomerId != null &&
                        e.CustomerId == customer_id &&
                        !string.IsNullOrEmpty(e.VisitCode) &&
                        e.EDStatusId != null &&
                        e.EDStatusId != no_examination.Id &&
                        e.AdmittedDate <= visit_time &&
                        e.Id != visitId
                      )
                    join eioform in unitOfWork.EIOFormRepository.AsQueryable().Where(x => x.VisitTypeGroupCode == "OPD" && x.FormCode == "A01_067_050919_VE_TAB1" &&  !x.IsDeleted)
                       on visit.Id equals eioform.VisitId
                    join formdatas in unitOfWork.FormDatasRepository.AsQueryable().Where(x => x.Code == "PRFOURE47")
                        on eioform.Id equals formdatas.FormId into datas


                    join doc in unitOfWork.UserRepository.AsQueryable().Where(x => !x.IsDeleted && string.IsNullOrEmpty(x.Username) == false)
                        on visit.PrimaryDoctorId equals doc.Id into doctor_list
                    from doctor in doctor_list.DefaultIfEmpty()
                    join clin in unitOfWork.ClinicRepository.AsQueryable()
                      on visit.ClinicId equals clin.Id into clin_list
                    from clinic in clin_list.DefaultIfEmpty()
                    select new VisitModel
                    {
                        Id = visit.Id,
                        ViName = clinic.ViName,
                        EnName = clinic.EnName,
                        CreatedAt = eioform.CreatedAt,
                        VisitTypeGroup = visit.Specialty.VisitTypeGroup.Code,
                        Username = doctor.Username,
                        VisitCode = visit.VisitCode,
                        Fullname = doctor.Fullname,
                        FamilyMedicalHistory = datas.FirstOrDefault(x => x.Code == "PRFOURE47").Value,
                        ExaminationTime = visit.AdmittedDate
                    }
                     ).ToListNoLock();
        }
    }

    public class EDHistory : VisitHistory
    {
        private ED visit;
        private string site_code;

        public EDHistory(ED visit, string site_code)
        {
            this.visit = visit;
            this.site_code = site_code;
        }

        public override List<VisitModel> GetHistory()
        {
            List<VisitModel> result = new List<VisitModel>();
            List<string> list_visit_code = new List<string>();
            var customer = this.visit.Customer;

            string current_visit = $"{this.visit.PrimaryDoctor?.EHOSAccount}{this.visit.VisitCode}";
            List<VisitModel> eds = GetEDHistory(customer.Id, this.visit.AdmittedDate);
            list_visit_code.AddRange(eds.Select(e => e.EHOSVisitCode).ToList());
            result.AddRange(eds.Where(e => e.EHOSVisitCode != current_visit).ToList());

            List<VisitModel> opds = GetOPDHistory(customer.Id, this.visit.AdmittedDate);
            list_visit_code.AddRange(opds.Select(e => e.EHOSVisitCode).ToList());
            result.AddRange(opds);

            List<VisitModel> ipds = GetIPDHistory(customer.Id, this.visit.AdmittedDate);
            list_visit_code.AddRange(ipds.Select(e => e.EHOSVisitCode).ToList());
            result.AddRange(ipds);


            List<VisitModel> painrecord = GetHistoryPainRecord(customer.Id, this.visit.AdmittedDate, this.PainRecordFormCode);
            list_visit_code.AddRange(painrecord.Select(e => e.EHOSVisitCode).ToList());
            result.AddRange(painrecord);

            List<VisitModel> edsPAC = GetEDHistoryPAC(customer.Id, this.visit.AdmittedDate, this.PainRecordFormCode);
            list_visit_code.AddRange(edsPAC.Select(e => e.EHOSVisitCode).ToList());
            result.AddRange(edsPAC);

            List<VisitModel> opdsPAC = GetOPDHistoryPAC(customer.Id, this.visit.AdmittedDate, this.PainRecordFormCode);
            list_visit_code.AddRange(opdsPAC.Select(e => e.EHOSVisitCode).ToList());
            result.AddRange(opdsPAC);

            List<VisitModel> eocsPAC = GetEOCHistoryPAC(customer.Id, this.visit.AdmittedDate, this.PainRecordFormCode);
            list_visit_code.AddRange(eocsPAC.Select(e => e.EHOSVisitCode).ToList());
            result.AddRange(eocsPAC);

            List<VisitModel> ipdsPAC = GetIPDHistoryPAC(customer.Id, this.visit.AdmittedDate, this.PainRecordFormCode);
            list_visit_code.AddRange(ipdsPAC.Select(e => e.EHOSVisitCode).ToList());
            result.AddRange(ipdsPAC);

            //List<VisitModel> his_result;
            //if (this.site_code == "times_city" && !string.IsNullOrEmpty(customer.PIDEhos))
            //    his_result = EHosClient.GetVisitHistory(customer.PIDEhos, this.visit.AdmittedDate);
            //else
            //    his_result = new List<VisitModel>();
            //his_result = his_result.Where(e => !list_visit_code.Contains(e.EHOSVisitCode)).ToList();
            //result.AddRange(his_result);

            return result.OrderByDescending(e => e.ExaminationTime).Distinct().ToList();
        }
        public override List<VisitModel> GetPromForCoronaryDiseasesHistory()
        {
            return null;
        }
        public override List<VisitModel> GetPromForheartFailuresHistory()
        {
            return null;
        }
        public override List<VisitModel> GetPromCurrentForCoronaryDiseases(Guid visitId)
        {
            return null;
        }
        public override List<VisitModel> GetPromCurrentForheartFailures(Guid visitId)
        {
            return null;
        }

        public override List<VisitModel> GetPersonalHistory(Guid visitId)
        {
            return null;
        }
    }

    public class OPDHistory : VisitHistory
    {
        private OPD visit;
        private string site_code;
        public OPDHistory()
        {
        }

        public OPDHistory(OPD visit, string site_code)
        {
            this.visit = visit;
            this.site_code = site_code;
        }

        public override List<VisitModel> GetHistory()
        {
            List<VisitModel> result = new List<VisitModel>();
            List<string> list_visit_code = new List<string>();
            var customer = this.visit.Customer;

            List<VisitModel> eds = GetEDHistory(customer.Id, this.visit.AdmittedDate);
            list_visit_code.AddRange(eds.Select(e => e.EHOSVisitCode).ToList());
            result.AddRange(eds);

            string current_visit = $"{this.visit.PrimaryDoctor?.EHOSAccount}{this.visit.VisitCode}";
            List<VisitModel> opds = GetOPDHistory(customer.Id, this.visit.AdmittedDate);
            list_visit_code.AddRange(opds.Select(e => e.EHOSVisitCode).ToList());
            result.AddRange(opds.Where(e => e.EHOSVisitCode != current_visit).ToList());

            List<VisitModel> ipds = GetIPDHistory(customer.Id, this.visit.AdmittedDate);
            list_visit_code.AddRange(ipds.Select(e => e.EHOSVisitCode).ToList());
            result.AddRange(ipds);

            List<VisitModel> painrecord = GetHistoryPainRecord(customer.Id, this.visit.AdmittedDate, PainRecordFormCode);
            list_visit_code.AddRange(painrecord.Select(e => e.EHOSVisitCode).ToList());
            result.AddRange(painrecord);

            List<VisitModel> his_result;
            if (this.site_code == "times_city" && !string.IsNullOrEmpty(customer.PIDEhos))
            {
                his_result = EHosClient.GetVisitHistory(customer.PIDEhos, this.visit.AdmittedDate);
            }
            else
            {
                his_result = OHClient.GetVisitHistory(customer.PID, this.visit.AdmittedDate);
            }
            his_result = his_result.Where(e => !list_visit_code.Contains(e.EHOSVisitCode)).ToList();
            result.AddRange(his_result);

            return result.OrderByDescending(e => e.ExaminationTime).Distinct().ToList();
        }
        public override List<VisitModel> GetPromForCoronaryDiseasesHistory()
        {
            List<VisitModel> result = new List<VisitModel>();
            List<string> list_visit_code = new List<string>();
            var customer = this.visit.Customer;

            List<VisitModel> ipds = GetIPDForCoronaryDiseases(customer.Id, this.visit.AdmittedDate);
            result.AddRange(ipds);

            List<VisitModel> opds = GetOPDForCoronaryDiseases(customer.Id, this.visit.AdmittedDate);
            result.AddRange(opds);
            for (int i = 0; i < result.Count; i++)
            {
                for (int j = 0; j < result[i].PromSumaries.Count; j++)
                {
                    if (!string.IsNullOrEmpty(result[i].PromSumaries[j]))
                    {
                        PromSumarie p = new PromSumarie();
                        p = JsonConvert.DeserializeObject<PromSumarie>(result[i].PromSumaries[j].ToString());
                        result[i].PromSumarieObj.Add(p);
                    }
                }
            }
            return result.OrderByDescending(e => e.CreatedAt).Distinct().ToList();
        }
        public override List<VisitModel> GetPromForheartFailuresHistory()
        {
            List<VisitModel> result = new List<VisitModel>();
            List<string> list_visit_code = new List<string>();
            var customer = this.visit.Customer;

            List<VisitModel> ipds = GetIPDForheartFailures(customer.Id, this.visit.AdmittedDate);
            result.AddRange(ipds);

            List<VisitModel> opds = GetOPDForheartFailures(customer.Id, this.visit.AdmittedDate);
            result.AddRange(opds);
            for (int i = 0; i < result.Count; i++)
            {
                for (int j = 0; j < result[i].PromSumaries.Count; j++)
                {
                    if (!string.IsNullOrEmpty(result[i].PromSumaries[j]))
                    {
                        PromSumarie p = new PromSumarie();
                        p = JsonConvert.DeserializeObject<PromSumarie>(result[i].PromSumaries[j].ToString());
                        result[i].PromSumarieObj.Add(p);
                    }
                }
            }
            return result.OrderByDescending(e => e.CreatedAt).Distinct().ToList();
        }
        public override List<VisitModel> GetPromCurrentForCoronaryDiseases(Guid visitId)
        {
            List<VisitModel> result = new List<VisitModel>();
            List<string> list_visit_code = new List<string>();

            List<VisitModel> ipds = GetCurrentIPDForCoronaryDiseases(visitId);
            result.AddRange(ipds);

            List<VisitModel> opds = GetCurrentOPDForCoronaryDiseases(visitId);
            result.AddRange(opds);
            for (int i = 0; i < result.Count; i++)
            {
                for (int j = 0; j < result[i].PromSumaries.Count; j++)
                {
                    if (!string.IsNullOrEmpty(result[i].PromSumaries[j]))
                    {
                        PromSumarie p = new PromSumarie();
                        p = JsonConvert.DeserializeObject<PromSumarie>(result[i].PromSumaries[j].ToString());
                        result[i].PromSumarieObj.Add(p);
                    }
                }
            }
            return result.OrderByDescending(e => e.CreatedAt).Distinct().ToList();
        }
        public override List<VisitModel> GetPromCurrentForheartFailures(Guid visitId)
        {
            List<VisitModel> result = new List<VisitModel>();
            List<string> list_visit_code = new List<string>();

            List<VisitModel> ipds = GetCurrentIPDForheartFailures(visitId);
            result.AddRange(ipds);

            List<VisitModel> opds = GetCurrentOPDForheartFailures(visitId);
            result.AddRange(opds);
            for (int i = 0; i < result.Count; i++)
            {
                for (int j = 0; j < result[i].PromSumaries.Count; j++)
                {
                    if (!string.IsNullOrEmpty(result[i].PromSumaries[j]))
                    {
                        PromSumarie p = new PromSumarie();
                        p = JsonConvert.DeserializeObject<PromSumarie>(result[i].PromSumaries[j].ToString());
                        result[i].PromSumarieObj.Add(p);
                    }
                }
            }
            return result.OrderByDescending(e => e.CreatedAt).Distinct().ToList();
        }
        public override List<VisitModel> GetPersonalHistory(Guid visitId)
        {
            List<VisitModel> result = new List<VisitModel>();
            List<string> list_visit_code = new List<string>();
            var customer = this.visit.Customer;

            List<VisitModel> ipds = GetCurrentIPDPersonalHistory(customer.Id, this.visit.AdmittedDate);
            if(ipds.Count > 0)
            {
                result.AddRange(ipds.AsQueryable().Where(x => !string.IsNullOrWhiteSpace(x.FamilyMedicalHistory)));
            }
            List<VisitModel> opds = GetCurrentOPDPersonalHistory(customer.Id, visitId, this.visit.AdmittedDate);
            if (opds.Count > 0)
            {
                result.AddRange(opds.AsQueryable().Where(x => !string.IsNullOrWhiteSpace(x.FamilyMedicalHistory)));
            }
              
            return result.OrderByDescending(e => e.CreatedAt).Distinct().ToList();
        }
    }
    public class EOCHistory : VisitHistory
    {
        private EOC visit;
        private string site_code;

        public EOCHistory(EOC visit, string site_code)
        {
            this.visit = visit;
            this.site_code = site_code;
        }

        public override List<VisitModel> GetHistory()
        {
            List<VisitModel> result = new List<VisitModel>();
            List<string> list_visit_code = new List<string>();
            var customer = this.visit.Customer;

            List<VisitModel> eds = GetEDHistory(customer.Id, this.visit.AdmittedDate);
            list_visit_code.AddRange(eds.Select(e => e.EHOSVisitCode).ToList());
            result.AddRange(eds);

            string current_visit = $"{this.visit.PrimaryDoctor?.EHOSAccount}{this.visit.VisitCode}";
            List<VisitModel> opds = GetOPDHistory(customer.Id, this.visit.AdmittedDate);
            list_visit_code.AddRange(opds.Select(e => e.EHOSVisitCode).ToList());
            result.AddRange(opds.Where(e => e.EHOSVisitCode != current_visit).ToList());

            List<VisitModel> ipds = GetIPDHistory(customer.Id, this.visit.AdmittedDate);
            list_visit_code.AddRange(ipds.Select(e => e.EHOSVisitCode).ToList());
            result.AddRange(ipds);

            List<VisitModel> painrecord = GetHistoryPainRecord(customer.Id, this.visit.AdmittedDate, PainRecordFormCode);
            list_visit_code.AddRange(painrecord.Select(e => e.EHOSVisitCode).ToList());
            result.AddRange(painrecord);

            List<VisitModel> his_result;
            if (this.site_code == "times_city" && !string.IsNullOrEmpty(customer.PIDEhos))
                his_result = EHosClient.GetVisitHistory(customer.PID, this.visit.AdmittedDate);
            else
                his_result = OHClient.GetVisitHistory(customer.PID, this.visit.AdmittedDate);
            his_result = his_result.Where(e => !list_visit_code.Contains(e.EHOSVisitCode)).ToList();
            result.AddRange(his_result);

            return result.OrderByDescending(e => e.ExaminationTime).Distinct().ToList();
        }
        public override List<VisitModel> GetPromForCoronaryDiseasesHistory()
        {
            return null;
        }
        public override List<VisitModel> GetPromForheartFailuresHistory()
        {
            return null;
        }
        public override List<VisitModel> GetPromCurrentForCoronaryDiseases(Guid visitId)
        {
            return null;
        }
        public override List<VisitModel> GetPromCurrentForheartFailures(Guid visitId)
        {
            return null;
        }

        public override List<VisitModel> GetPersonalHistory(Guid visitId)
        {
            return null;
        }
    }
    public class IPDHistory : VisitHistory
    {
        private IPD visit;
        private string site_code;

        public IPDHistory(IPD visit, string site_code)
        {
            this.visit = visit;
            this.site_code = site_code;
        }

        public override List<VisitModel> GetHistory()
        {
            List<VisitModel> result = new List<VisitModel>();
            List<string> list_visit_code = new List<string>();
            var customer = this.visit.Customer;

            List<VisitModel> eds = GetEDHistory(customer.Id, this.visit.AdmittedDate);
            list_visit_code.AddRange(eds.Select(e => e.EHOSVisitCode).ToList());
            result.AddRange(eds);

            List<VisitModel> opds = GetOPDHistory(customer.Id, this.visit.AdmittedDate);
            list_visit_code.AddRange(opds.Select(e => e.EHOSVisitCode).ToList());
            result.AddRange(opds);

            var medical_report = this.visit.IPDMedicalRecord;
            var part_2 = medical_report?.IPDMedicalRecordPart2;
            User primary_doctor = null;
            if (part_2 != null)
                primary_doctor = this.unitOfWork.UserRepository.FirstOrDefault(e => !e.IsDeleted && e.Username == part_2.UpdatedBy);
            string current_visit = $"{primary_doctor?.EHOSAccount}{this.visit.VisitCode}";
            List<VisitModel> ipds = GetIPDHistory(customer.Id, this.visit.AdmittedDate);
            list_visit_code.AddRange(ipds.Select(e => e.EHOSVisitCode).ToList());
            result.AddRange(ipds.Where(e => e.EHOSVisitCode != current_visit).ToList());

            List<VisitModel> painrecord = GetHistoryPainRecord(customer.Id, this.visit.AdmittedDate, PainRecordFormCode);
            list_visit_code.AddRange(painrecord.Select(e => e.EHOSVisitCode).ToList());
            result.AddRange(painrecord);

            List<VisitModel> edsPAC = GetEDHistoryPAC(customer.Id, this.visit.AdmittedDate, PACFormCode);
            list_visit_code.AddRange(edsPAC.Select(e => e.EHOSVisitCode).ToList());
            result.AddRange(edsPAC);

            List<VisitModel> opdsPAC = GetOPDHistoryPAC(customer.Id, this.visit.AdmittedDate, PACFormCode);
            list_visit_code.AddRange(opdsPAC.Select(e => e.EHOSVisitCode).ToList());
            result.AddRange(opdsPAC);

            List<VisitModel> ipdsPAC = GetIPDHistoryPAC(customer.Id, this.visit.AdmittedDate, PACFormCode);
            list_visit_code.AddRange(ipds.Select(e => e.EHOSVisitCode).ToList());
            result.AddRange(ipdsPAC.Where(e => e.EHOSVisitCode != current_visit).ToList());

            List<VisitModel> his_result;
            if (this.site_code == "times_city" && !string.IsNullOrEmpty(customer.PIDEhos))
                his_result = EHosClient.GetVisitHistory(customer.PID, this.visit.AdmittedDate);
            else
                his_result = new List<VisitModel>();
            his_result = his_result.Where(e => !list_visit_code.Contains(e.EHOSVisitCode)).ToList();
            result.AddRange(his_result);

            return result.OrderByDescending(e => e.ExaminationTime).Distinct().ToList();
        }
        public override List<VisitModel> GetPromForCoronaryDiseasesHistory()
        {
            List<VisitModel> result = new List<VisitModel>();
            List<string> list_visit_code = new List<string>();
            var customer = this.visit.Customer;

            List<VisitModel> ipds = GetIPDForCoronaryDiseases(customer.Id, this.visit.AdmittedDate);
            result.AddRange(ipds);

            List<VisitModel> opds = GetOPDForCoronaryDiseases(customer.Id, this.visit.AdmittedDate);
            result.AddRange(opds);

            for(int i = 0; i < result.Count;i++)
            {
                for(int j = 0; j < result[i].PromSumaries.Count;j++)
                {
                    if (!string.IsNullOrEmpty(result[i].PromSumaries[j]))
                    {
                        PromSumarie p = new PromSumarie();
                         p = JsonConvert.DeserializeObject<PromSumarie>(result[i].PromSumaries[j].ToString());
                        result[i].PromSumarieObj.Add(p);
                    }
                }
            }
            return result.OrderByDescending(e => e.CreatedAt).Distinct().ToList();
        }
        public override List<VisitModel> GetPromForheartFailuresHistory()
        {
            List<VisitModel> result = new List<VisitModel>();
            List<string> list_visit_code = new List<string>();
            var customer = this.visit.Customer;

            List<VisitModel> ipds = GetIPDForheartFailures(customer.Id, this.visit.AdmittedDate);
            result.AddRange(ipds);

            List<VisitModel> opds = GetOPDForheartFailures(customer.Id, this.visit.AdmittedDate);
            result.AddRange(opds);

            for (int i = 0; i < result.Count; i++)
            {
                for (int j = 0; j < result[i].PromSumaries.Count; j++)
                {
                    if (!string.IsNullOrEmpty(result[i].PromSumaries[j]))
                    {
                        PromSumarie p = new PromSumarie();
                        p = JsonConvert.DeserializeObject<PromSumarie>(result[i].PromSumaries[j].ToString());
                        result[i].PromSumarieObj.Add(p);
                    }
                }
            }
            return result.OrderByDescending(e => e.CreatedAt).Distinct().ToList();
        }
        public override List<VisitModel> GetPromCurrentForCoronaryDiseases(Guid visitId)
        {
            List<VisitModel> result = new List<VisitModel>();
            List<string> list_visit_code = new List<string>();

            List<VisitModel> ipds = GetCurrentIPDForCoronaryDiseases(visitId);
            result.AddRange(ipds);

            List<VisitModel> opds = GetCurrentOPDForCoronaryDiseases(visitId);
            result.AddRange(opds);
            for (int i = 0; i < result.Count; i++)
            {
                for (int j = 0; j < result[i].PromSumaries.Count; j++)
                {
                    if (!string.IsNullOrEmpty(result[i].PromSumaries[j]))
                    {
                        PromSumarie p = new PromSumarie();
                        p = JsonConvert.DeserializeObject<PromSumarie>(result[i].PromSumaries[j].ToString());
                        result[i].PromSumarieObj.Add(p);
                    }
                }
            }
            return result.OrderByDescending(e => e.CreatedAt).Distinct().ToList();
        }
        public override List<VisitModel> GetPromCurrentForheartFailures(Guid visitId)
        {
            List<VisitModel> result = new List<VisitModel>();
            List<string> list_visit_code = new List<string>();

            List<VisitModel> ipds = GetCurrentIPDForheartFailures(visitId);
            result.AddRange(ipds);

            List<VisitModel> opds = GetCurrentOPDForheartFailures(visitId);
            result.AddRange(opds);
            for (int i = 0; i < result.Count; i++)
            {
                for (int j = 0; j < result[i].PromSumaries.Count; j++)
                {
                    if (!string.IsNullOrEmpty(result[i].PromSumaries[j]))
                    {
                        PromSumarie p = new PromSumarie();
                        p = JsonConvert.DeserializeObject<PromSumarie>(result[i].PromSumaries[j].ToString());
                        result[i].PromSumarieObj.Add(p);
                    }
                }
            }
            return result.OrderByDescending(e => e.CreatedAt).Distinct().ToList();
        }
       
        public override List<VisitModel> GetPersonalHistory(Guid visitId)
        {
            List<VisitModel> result = new List<VisitModel>();
            List<string> list_visit_code = new List<string>();
            var customer = this.visit.Customer;

            List<VisitModel> ipds = GetCurrentIPDPersonalHistory(customer.Id, this.visit.AdmittedDate);
            result.AddRange(ipds);

            List<VisitModel> opds = GetCurrentOPDPersonalHistory(customer.Id, visitId , this.visit.AdmittedDate);
            result.AddRange(opds);

            return result.OrderByDescending(e => e.CreatedAt).Distinct().ToList();
        }
    }
    public class VisitHistoryModel
    {
        public DateTime? ExaminationTime { get; set; }
        public string DoctorExam { get; set; }
        public string SpecialtyVi { get; set; }
        public string SpecialtyEn { get; set; }
        public string PastMedicalHistory { get; set; }
        public DateTime? UpdateAt { get; set; }
    }

    public class VisitHistoryFactory
    {
        public static VisitHistory GetVisit(string visit_type, dynamic visit, string site_code)
        {
            switch (visit_type)
            {
                case "ED":
                    return new EDHistory(visit, site_code);

                case "OPD":
                    return new OPDHistory(visit, site_code);

                case "IPD":
                    return new IPDHistory(visit, site_code);
                case "EOC":
                    return new EOCHistory(visit, site_code);
                default:
                    throw new ArgumentException("This visit type is unsupported");
            }
        }
    }
}