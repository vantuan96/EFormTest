using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class InfoRecord
    {
        public string RecordCode { get; set; }
        public DateTime ExaminationTime { get; set; }
        public string EHOSVisitCode { get; set; }
        public string StatusViName { get; set; }
        public string Type { get; set; }
        public string DoctorUsername { get; set; }
        public string NurseUsername { get; set; }
        public Guid? Id { get; set; }
        public string VisitCode { get; set; }
        public Guid? StatusId { get; set; }
        public string StatusEnName { get; set; }
        public string StatusCode { get; set; }

        public Guid? SpecialtyId { get; set; }
        public string SpecialtyApiCode { get; set; }
        public string SpecialtyEnName { get; set; }
        public string SpecialtyViName { get; set; }
        public string SpecialtyCode { get; set; }

        public string SpecialtySite { get; set; }
        public string SpecialtySiteApiCode { get; set; }
        public string SpecialtySiteEnName { get; set; }
        public string SpecialtySiteViName { get; set; }
        public string SpecialtySiteSite { get; set; }
        public string SiteCode { get; set; }

        public DateTime? AdmittedDate { get; set; }
        public DateTime? DischargeDate { get; set; }

        public bool IsPreAnesthesia { get; set; }

        public string Fullname { get; set; }
        public string Assessment { get; set; }
        public string ViName { get; set; }
        public string EnName { get; set; }
        public string PastMedicalHistory { get; set; }
        public string FamilyMedicalHistory { get; set; }
        public string HistoryOfAllergies { get; set; }
        public string HistoryOfPresentIllness { get; set; }
        public string Tests { get; set; }
        public string Diagnosis { get; set; }
        public string ClinicalSymptoms { get; set; }
        public string ICD { get; set; }
        public string ICDName { get; set; }
        public string Username { get; set; }
        public string ChiefComplain { get; set; }
        public string InitialDiagnosis { get; set; }
        public string TreatmentPlans { get; set; }
        public string DoctorRecommendations { get; set; }
        public string ICDOption { get; set; }
    }
}
