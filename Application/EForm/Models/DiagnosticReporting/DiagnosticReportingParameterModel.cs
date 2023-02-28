using DataAccess.Models;
using DataAccess.Models.GeneralModel;
using EForm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EForm.Models.DiagnosticReporting
{
    public class DiagnosticReportingParameterModel : PagingParameterModel
    {
        public string Type { get; set; }
        public string Status { get; set; }
        public string Search { get; set; }
        public string Username { get; set; }
        public string User { get; set; }
        public string AreaName { get; set; }
        public string ConvertedSearch
        {
            get
            {
                return this.Search.Trim().ToLower();
            }
        }
        public string StartAt { get; set; }
        public DateTime? ConvertedStartAt
        {
            get
            {
                if (string.IsNullOrEmpty(this.StartAt))
                {
                    return null;
                }
                return DateTime.ParseExact(this.StartAt, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            }
        }
        public DateTime? ConvertedEndAt
        {
            get
            {
                if (string.IsNullOrEmpty(this.EndAt))
                {
                    return null;
                }
                return DateTime.ParseExact(this.EndAt, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            }
        }
        public string EndAt { get; set; }
        public string VisitCode { get; set; }
    }
    public class DiagnosticReportingCharge
    {
        public Guid Id { get; set; }
        public string PID { get; set; }
        public string SiteCode { get; set; }
        public string Fullname { get; set; }
        public string AreaName { get; set; }
        public string AreaCode { get; set; }
        public string ServiceName { get; set; }
        public string ServiceCode { get; set; }
        public string ServiceGroupCode { get; set; }
        public DateTime? Dob { get; set; }
        public int? Gender { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public Guid ChargeItemId { get; set; }
        public Guid? CustomerId { get; set; }
        public DateTime? ExamCompleted { get; set; }
        public string ExamCompletedStr { get; set; }
        public int? Status { get; set; } // 1 Đã Tiếp nhận, 2 Đã hoàn thành
        public string Technique { get; set; }
        public string Findings { get; set; }
        public string Impression { get; set; }
        public string VisitGroupType { get; set; }
        public string VisitType { get; set; }
        public string VisitCode { get; set; }
        public string PickupBy { get; set; }
        public bool IsReadony { get; set; }
        public DateTime? PickupAt { get; set; }
        public DateTime? PickupAtDate { get; set; }

        public Guid? DiagnosticReportingId { get; set; }
        public string InitialDiagnosis { get; set; }
        public string UpdatedBy { get; set; }
        public string CompletedBy { get; set; }
        public string ChargeBy { get; set; }
        public string HospitalCode { get; set; }
        public string StrDate { get; set; }
        public bool IsDiagnosticReporting { get; set; }

        public string Nurse { get; set; }
        public string Area { get; set; }
        public dynamic NguoiTraKQ { get; set; }
    }
    public class PathologyMicrobiologyTemp
    {
        public Guid Id { get; set; }
        public string CombinedName { get; set; }
        public string ServiceEnName { get; set; }
        public string ServiceViName { get; set; }
        public string VisitCode { get; set; }
        public string ServiceCode { get; set; }
        public string Area { get; set; }
        public int ItemType { get; set; } // 0 VS, 1 GPB
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public Guid? ChargeItemPathologyId { get; set; }
        public Guid? ChargeItemMicrobiologyId { get; set; }
        public Guid? ChargeId { get; set; }
        public string CreatedBy { get; set; }
        public string SpecimenStatus { get; set; }
    }
    public class PathologyMicrobiology
    {
        public Guid Id { get; set; }
        public string VisitCode { get; set; }
        public string ServiceEnName { get; set; }
        public string ServiceViName { get; set; }
        public string ServiceCode { get; set; }
        public string Area { get; set; }
        public int ItemType { get; set; } // 0 VS, 1 GPB
        public DateTime? CreatedAt { get; set; }
        public DateTime? PathologyUpdatedAt { get; set; }
        public DateTime? MicrobiologyUpdatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public string CreatedBy { get; set; }
        public Guid? ChargeItemPathologyId { get; set; }
        public ChargeItemPathologyDto PathologyDto { get; set; }
        public Guid? ChargeItemMicrobiologyId { get; set; }
        public ChargeItemMicrobiologyDto MicrobiologyDto { get; set; }
        public ChargeItemPathology Pathology { get; set; }
        public ChargeItemMicrobiology Microbiology { get; set; }
        public string SpecimenStatus { get; set; }
        public bool IsReadonly { get; set; }
    }
}