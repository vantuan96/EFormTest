using DataAccess.Model.BaseModel;
using DataAccess.Models.BaseModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.OPDModel
{
    public class OPDClinicalBreastExamNote : VBaseModel
    {
        public OPDClinicalBreastExamNote()
        {
            this.OPDClinicalBreastExamNoteDatas = new HashSet<OPDClinicalBreastExamNoteData>();
        }
        public DateTime? ExaminationTime { get; set; }
        public DateTime? BlockTime { get; set; }
        public string RiskAssessmentForCancer { get; set; }
        public bool IsFamilyHistoryOfBreastCancer { get; set; }
        public string FamilyHealthHistory { get; set; } 
        public int AgeOfIllness { get; set; }             
        public virtual ICollection<OPDClinicalBreastExamNoteData> OPDClinicalBreastExamNoteDatas { get; set; }
        public bool? ThoracicIrradiation { get; set; } = false;//chiếu xạ lồng ngực
        public Guid? VisitId { get; set; }
        public string VisitTypeGroupCode { get; set; }
       

    } 
}
