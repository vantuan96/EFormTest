using DataAccess.Models.EIOModel;
using DataAccess.Models.IPDModel;
using EMRModels;
using System;
using System.Collections.Generic;

namespace EMRModels
{
    public class VisitModel
    {
        public Guid? Id { get; set; }
        public DateTime ExaminationTime { get; set; }
        public DateTime? DischargeDate { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string NurseUsername { get; set; }
        public string DoctorUsername { get; set; }
        public string AuthorizedDoctorUsername { get; set; }
        public string ViName { get; set; }
        public string EnName { get; set; }
        public string PastMedicalHistory { get; set; }
        public string FamilyMedicalHistory { get; set; }
        public List<string> FamilyMedicalHistoryData { get; set; }
        public string HistoryOfAllergies { get; set; }
        public string HistoryOfPresentIllness { get; set; }
        public string VisitCode { get; set; }
        public string EHOSVisitCode { get; set; }
        public string Type { get; set; }
        public string Assessment { get; set; }
        public string Diagnosis { get; set; }
        public DiagnosisAndICDModel DiagnosisInfo { get; set; }
        public string ICD { get; set; }
        public string ICDName { get; set; }
        public string Tests { get; set; }
        public string ClinicalSymptoms { get; set; }
        public string RecordCode { get; set; }
        public Guid? StatusId { get; set; }
        public dynamic Status { get; set; }
        public string StatusCode { get; set; }
        public Guid? SpecialtyId { get; set; }
        public dynamic Specialty { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string ChiefComplain { get; set; }
        public string InitialDiagnosis { get; set; }
        public string TreatmentPlans { get; set; }
        public string DoctorRecommendations { get; set; }
        public string ICDOption { get; set; }
        public string UnlockFor { get; set; }
        public string HistoryOfTreated { get; set; }
        public string HistoryOfExaminationFindings { get; set; }
        public bool IsTransfer { get; set; }
        public Guid? TransferFromId { get; set; }
        public Guid? HandOverCheckListId { get; set; }
        public string CheckStatus { get; set; }
        public string CheckOncology {get; set; }
        public string SpecialistExamination { get; set; }
        public dynamic SpecialtySite { get; set; }
        public List<string> PromSumaries { get; set; }
        public List<PromSumarie> PromSumarieObj { get; set; } = new List<PromSumarie>();
        public string VisitTypeGroup { get; set; }
        public bool? CustomerIsAllergy { get; set; }
        public string CustomerAllergy { get; set; }
        public string CustomerKindOfAllergy { get; set; }
        public bool IsPreAnesthesia { get; set; } = false;
        public string Sk_OPDOENTSSKANS { get; set; }
        public string Sk_OPDOENTSKNANS { get; set; }
        public string Sk_OPDOENTSKANS { get; set; }
        public string ClinicCode { get; set; }
        public string PID { get; set; }
        public VitalSigns VitalSigns { get; set; }
        public DateTime? Recept { get; set; }
    }
    public class VisitGroupInfoModel
    {
        public Guid Id { get; set; }
        public DateTime? AdmittedDate { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? DischargeDate { get; set; }
        public string VisitType { get; set; }
        public string PID { get; set; }
        public List<VisitModel> VisitList { get; set; }
        public string StatusName { get; set; }
        public dynamic Status { get; set; }
        public string StatusCode { get; set; }
        public string CheckOncology { get; set; }
        public string CheckStatus { get; set; }
        public dynamic Site { get; set; }
    }
    public class VisitIdModel
    {
        public dynamic Object { get; set; }
        public DateTime AdmittedDate { get; set; }
        public string VisitType { get; set; }
    }
    public class VisitInfoModel
    {
        public Guid Id { get; set; }
        public DateTime AdmittedDate { get; set; }
        public string VisitType { get; set; }
        public string Specialty { get; set; }
        public string UnlockFor { get; set; }
    }
    public class VisitDiagnosisAndICD
    {
        public string AdmittedDate { get; set; }
        public string VisitType { get; set; }
        public string Diagnosis { get; set; }
        public string ICD { get; set; }
    }
    public class VisitAllergyModel
    {
        public bool? IsAllergy { get; set; }
        public string Allergy { get; set; }
        public string KindOfAllergy { get; set; }
    }
    public class MsgModel
    {
        public string ViMessage { get; set; }
        public string EnMessage { get; set; }
    }
    public class OpenVisitResult
    {
        public Guid? PatientLocationId { get; set; }
        public Guid? PatientVisitId { get; set; }
        public Guid? CostCentreId { get; set; }
        public string VisitCode { get; set; }
        public string VisitTypeName { get; set; }
        public string VisitType { get; set; }
        public string AreaName { get; set; }
        public string HospitalCode { get; set; }
        public string DoctorAD { get; set; }
        public string VisitGroupType { get; set; }
        public string PatientLocationCode { get; set; }
        public string ActualVisitDate { get; set; }
    }
    public class GrowthProcess
    {
        public Guid Id { get; set; }
        public string VisitCode { get; set; }
        public string VisitDate { get; set; }
        public DateTime? RawVisitDate { get; set; }
        public string Info { get; set; }
        public string Username { get; set; }
        public string DoctorUsername { get; set; }
        public List<MasterDataValue> Data { get; set; }
    }
    public class VisitSetupFormModel
    {
        public Guid Id { get; set; }
        public Guid? FormId { get; set; }
        public string Username { get; set; }
        public string Doctor { get; set; }
        public string Code { get; set; }
        public string PID { get; set; }
        public string VisitCode { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? AdmittedDate { get; set; }
        public Guid? IPDMedicalRecordId { get; set; }
        public virtual IPDMedicalRecord IPDMedicalRecord { get; set; }
    }

    public class ProcedureSumaryModel
    {
        public virtual ICollection<EIOProcedureSummaryData> EIOProcedureSummaryDatas { get; set; }
        public Guid Id { get; set; }
        public DateTime? CreateAt { get; set; }
        public string CreateBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string TransactionDate { get; set; }
        public Guid? VisitId { get; set; }
        public string VisitType { get; set; }        
        public string Version { get; set; }
    }
    public class PromSumarie
    {
        public string ViTitle { get; set; }
        public string EnTitle { get; set; }
        public string Point { get; set; }
        public string Summary { get; set; }
        public int Order { get; set; }
    }
    public class PromResponse
    {
        public Guid? VisitId { get; set; }
        public string ViName { get; set; }
        public string EnName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string VisitTypeGroup { get; set; }
        public string Username { get; set; }
        public string VisitCode { get; set; }
        public List<PromSumarie> PromSumaries { get; set; }
        public bool HavePromForCoronary { get; set; }
        public bool havePromHeart { get; set; }
    }
    public class CustomerInfo
    {
        public string Fullname { get; set; }
        public string PID { get; set; }
        public Nullable<int> Gender { get; set; }
        public string DateOfBirth { get; set; }
    }
    public class UploadImageModel : PagingParameterModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Title { get; set; }
        public string CreatedBy { get; set; }
        public string FileType { get; set; }
        public string VisitType { get; set; }

    }
    public class PreAnesthesiaModel
    {
         public string CodeCK { get; set; }
        public dynamic Status { get; set; }
        public dynamic AcceptByNurse { get; set; }
        public dynamic AcceptByPhysician { get; set; }
        public dynamic Specialyty { get; set; }
        public bool IsAccept { get; set; }
        public Guid ReciveVisitId { get; set; } 

    }
    public class VisitPreAnesModel
    {
        public Guid Id { get; set; }
        public string VisitCode { get; set; }
        public Guid CustomerId { get; set; }
        public bool IsAcceptByNurse { get; set; }
        public bool IsAcceptPhysician { get; set; }
        public Guid? StatusId { get; set; }
        public string Fullname { get; set; }
        public string PID { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string SpecialtyName { get; set; }
    }
    public class UploadModel
    {
        public Guid? Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string EnName { get; set; }
        public string ViName { get; set; }
        public string Url { get; set; }
        public bool IsCheckFile { get; set; }
        public bool IsDeleted { get; set; }
        public string FormCodeUrl { get; set; }
    }
    public class VisitUploadModel
    {
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string Url { get; set; }
        public string FileSize { get; set; }
    }
    public class VitalSigns
    {
        public string Pulse { get; set; }
        public string BP { get; set; }
        public string To { get; set; }
        public string RR { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Spo2 { get; set; }
    }
    public class TrackingModel
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
    public class UserInfo
    {
        public string userId { get; set; }
        public string userName { get; set; }
        public string employeeId { get; set; }
        public string email { get; set; }

    }
}