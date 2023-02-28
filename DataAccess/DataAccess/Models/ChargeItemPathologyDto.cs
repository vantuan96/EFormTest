using DataAccess.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class ChargeItemPathologyDto : VBaseModel
    {
        public string PathologyType { get; set; }
        public string SurgeryMethod { get; set; }
        public string SiteOfSpecimen { get; set; }
        public string CollectionTime { get; set; }
        public string StoreSpecimenWithSolution { get; set; }
        public string TimeOfStore { get; set; }
        public string SpecimenOrientation { get; set; }
        public string ClinicalHistoryAndLabTests { get; set; }
        public string Treatment { get; set; }
        public string GrosDescription { get; set; }
        public string PreviousResults { get; set; }
        public string ClinicalDiagnosis { get; set; }
        public string LatestMenstrualPeriod { get; set; }
        public string TheLastDayOfLatestMenstrualPeriod { get; set; }
        public string PostMenopause { get; set; }
        public string CervicalCytologyTestHistoryAndTreatmentBefore { get; set; }
        public string HistoryOfSquamousCellCarcinoma { get; set; }
        public string BirthControlMethod { get; set; }
        public string HormoneTreatment { get; set; }
        public string UterusRemoval { get; set; }
        public string RadiationTheorapy { get; set; }
        public string PostMenopauseBleeding { get; set; }
        public string Pregnant { get; set; }
        public string Postpartum { get; set; }
        public string BirthControlPills { get; set; }
        public string Breastfeeding { get; set; }
        public string GynecologicalCytologyType { get; set; }
        public bool IsBirthControlMethod { get; set; }
        public bool IsHormoneTreatment { get; set; }
        public bool IsUterusremoval { get; set; }
        public bool IsRadiationTheorapy { get; set; }
        public bool IsPostMenopauseBleeding { get; set; }
        public bool IsPregnant { get; set; }
        public bool IsPostpartum { get; set; }
        public bool IsBirthControlPills { get; set; }
        public bool IsBreastfeeding { get; set; }
    }
}
