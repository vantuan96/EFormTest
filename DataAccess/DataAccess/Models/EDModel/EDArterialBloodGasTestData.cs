using DataAccess.Model.BaseModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.EDModel
{
    public class EDArterialBloodGasTestData: IEntity
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
        public Guid? EDArterialBloodGasTestId { get; set; }
        [ForeignKey("EDArterialBloodGasTestId")]
        public virtual EDArterialBloodGasTest EDArterialBloodGasTest { get; set; }
    }
}
