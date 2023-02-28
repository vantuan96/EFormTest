using DataAccess.Model.BaseModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.EDModel
{
    public class EDChemicalBiologyTestData: IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string DeletedBy { get; set; }
        public string Result { get; set; }
        public Guid? EDPointOfCareTestingMasterDataId { get; set; }
        [ForeignKey("EDPointOfCareTestingMasterDataId")]
        public virtual EDPointOfCareTestingMasterData EDPointOfCareTestingMasterData { get; set; }
        public Guid? EDChemicalBiologyTestId { get; set; }
        [ForeignKey("EDChemicalBiologyTestId")]
        public virtual EDChemicalBiologyTest EDChemicalBiologyTest { get; set; }
    }
}
