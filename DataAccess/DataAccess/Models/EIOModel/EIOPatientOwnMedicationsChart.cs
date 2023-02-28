using DataAccess.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.EIOModel
{
    public class EIOPatientOwnMedicationsChart : IEntity
    {
        public EIOPatientOwnMedicationsChart()
        {
            this.EIOPatientOwnMedicationsChartDatas = new HashSet<EIOPatientOwnMedicationsChartData>();
        }
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? VisitId { get; set; }
        public string VisitTypeGroupCode { get; set; }
        public string FirstTotal { get; set; }
        public string DoctorOpinion { get; set; }
        public string ClinicalPharmacistReview { get; set; }
        public bool? StorageDrugsAtPharmacy { get; set; }
        public string SecondTotal { get; set; }
        public string Upload { get; set; }
        public DateTime? HeadOfPharmacyTime { get; set; }
        public Guid? HeadOfPharmacyId { get; set; }
        [ForeignKey("HeadOfPharmacyId")]
        public virtual User HeadOfPharmacy { get; set; }
        public DateTime? HeadOfDepartmentTime { get; set; }
        public Guid? HeadOfDepartmentId { get; set; }
        [ForeignKey("HeadOfDepartmentId")]
        public virtual User HeadOfDepartment { get; set; }
        public DateTime? DoctorTime { get; set; }
        public Guid? DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public virtual User Doctor { get; set; }
        public virtual ICollection<EIOPatientOwnMedicationsChartData> EIOPatientOwnMedicationsChartDatas { get; set; }
    }
}
