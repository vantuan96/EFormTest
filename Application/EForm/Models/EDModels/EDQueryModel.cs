using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess.Models;
using EMRModels;

namespace EForm.Models.EDModels
{
    public class EDQueryModel
    {
        public Guid Id { get; set; }
        
        public string VisitCode { get; set; }
        public string ATSScale { get; set; }
        public DateTime AdmittedDate { get; set; }
        public string AdmittedDateDate { get; set; }
        public bool IsRetailService { get; set; }
        public string CustomerPID { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerFullname { get; set; }
        public DateTime? CustomerDateOfBirth { get; set; }
        public bool? CustomerIsAllergy { get; set; }
        public string CustomerAllergy { get; set; }
        public string CustomerKindOfAllergy { get; set; }
        public Guid? EDStatusId { get; set; }
        public DateTime? EDStatusCreatedAt { get; set; }
        public string EDStatusEnName { get; set; }
        public string EDStatusViName { get; set; }
        public string ATSScaleEnName { get; set; }
        public string ATSScaleViName { get; set; }
        public string ATSScaleCode { get; set; }
        public string ATSScaleNote { get; set; }
        public Guid? EmergencyTriageRecordId { get; set; }
        public bool? IsHasFallRiskScreening { get; set; }
        public int? CovidRiskGroup { get; set; }
        public User PrimaryDoctor { get; set; }
        public int? SelfHarmRiskScreeningResults { get; set; }
        public bool IsVip { get; set; }
        public string UnlockFor { get; set; }
        public List<string> UnlockFors { get; set; }
        public string CreatedBy { get; set; }
        public int Version { get; set; } 
        public string UserDoTriage { get; set; }
        public string UserReceiver { get; set; }
    }

    public class EDViewModel
    {
        public bool IsFallRiskScreening;
        public Guid Id { get; set; }
        public string VisitCode { get; set; }
        public EDATSScaleViewModel ATSScale { get; set; }
        public string AdmittedDate { get; set; }
        public bool IsRetailService { get; set; }
        public EDCustomerViewModel Customer { get; set; }
        public EDStatusViewModel EmergencyStatus { get; set; }
        public int? CovidRiskGroup { get; set; }

        public string Doctor { get; set; }

        public int? SelfHarmRiskScreeningResults { get; set; }
        public VisitAllergyModel VisitAllergy { get; set; }

        public bool IsVip { get; set; }
        public string CreatedBy { get; set; }
        public int Version { get;set; }
        public string UserDoTriage { get; set; }
        public string UserReceiver { get; set; }
    }

    public class EDCustomerViewModel
    {
        public string PID { get; set; }
        public string Phone { get; set; }
        public string Fullname { get; set; }
        public string DateOfBirth { get; set; }
        public bool? IsAllergy { get; set; }
        public string Allergy { get; set; }
        public string KindOfAllergy { get; set; }
        public bool IsVip { get; set; }
    }
    public class EDATSScaleViewModel
    {
        public string ViName { get; set; }
        public string EnName { get; set; }
        public string Code { get; set; }
        public string Note { get; set; }
    }

    public class EDStatusViewModel
    {
        public string ViName { get; set; }
        public string EnName { get; set; }
    }
}