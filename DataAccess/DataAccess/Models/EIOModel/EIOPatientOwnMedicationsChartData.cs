using DataAccess.Model.BaseModel;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.EIOModel
{
    public class EIOPatientOwnMedicationsChartData: IEntity
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string DrugName { get; set; }
        public string Unit { get; set; }
        public string Quantity { get; set; }
        public string LotNo { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string StorageOrigin { get; set; }
        public string Note { get; set; }
        public Guid? EIOPatientOwnMedicationsChartId { get; set; }
        [ForeignKey("EIOPatientOwnMedicationsChartId")]
        public virtual EIOPatientOwnMedicationsChart EIOPatientOwnMedicationsChart { get; set; }
    }
}
