using DataAccess.Model.BaseModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.EDModel
{
    public class EDPointOfCareTestingMasterData: IEntity
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
        public string Form { get; set; }
        public int Order { get; set; }
        public string Name { get; set; }
        public string ViAge { get; set; }
        public string EnAge { get; set; }
        public float? HigherLimit { get; set; }
        public float? LowerLimit { get; set; }
        public float? HigherAlert { get; set; }
        public float? LowerAlert { get; set; }
        public float? Result { get; set; }
        public string Unit { get; set; }
        public int Version { get; set; }
    }
}
