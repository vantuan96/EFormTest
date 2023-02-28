using DataAccess.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.IPDModel
{
    public class IPDThrombosisRiskFactorAssessment : IEntity
    {
        public IPDThrombosisRiskFactorAssessment()
        {
            this.IPDThrombosisRiskFactorAssessmentDatas = new HashSet<IPDThrombosisRiskFactorAssessmentData>();
        }
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? IPDId { get; set; }
        [ForeignKey("IPDId")]
        public virtual IPD IPD { get; set; }
        public DateTime? StartingAssessment { get; set; }
        public DateTime? FinishingAssessment { get; set; }
        public double PaduaTotalPoint { get; set; }
        public double IMPROVETotalPoint { get; set; }
        public string VTERiskFactors { get; set; } //Yếu tố nguy cơ HKTT
        public string ConditionOfPatient { get; set; } //Tình trạng bệnh nhân
        public string MechanicalProphylaxis { get; set; } // Phương pháp điều trị
        public string ContraindicationsForAntiCoagulant { get; set; } //CCĐ thuốc chống đông - cân nhắc điều trị dự phòng bằng biện pháp cơ học
        public virtual ICollection<IPDThrombosisRiskFactorAssessmentData> IPDThrombosisRiskFactorAssessmentDatas { get; set; }
        public string FormCode { get; set; }
    }
}
