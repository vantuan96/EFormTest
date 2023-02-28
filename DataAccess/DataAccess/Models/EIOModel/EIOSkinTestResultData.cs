using DataAccess.Model.BaseModel;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.EIOModel
{
    public class EIOSkinTestResultData: IEntity
    {
        public Guid Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string DeletedBy { get; set; }
        public Guid? EDSkinTestResultId { get; set; }
        [ForeignKey("EDSkinTestResultId")]
        public virtual EIOSkinTestResult EIOSkinTestResult { get; set; }
        public string Drug { get; set; }
        public string SkinDilutionConcentration { get; set; }
        public string SkinResult { get; set; }
        public string SkinPositive { get; set; }
        public string SkinNegative { get; set; }
        public string EndodermDilutionConcentration { get; set; }
        public string EndodermResult { get; set; }
        public string EndodermNegative { get; set; }
    }
}
