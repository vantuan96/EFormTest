using DataAccess.Model.BaseModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.EIOModel
{
    public class EIOBloodTransfusionChecklistData: IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? EIOBloodTransfusionChecklistId { get; set; }
        [ForeignKey("EIOBloodTransfusionChecklistId")]
        public virtual EIOBloodTransfusionChecklist EIOBloodTransfusionChecklist { get; set; }
        public DateTime? Time { get; set; }
        public string TransfusionSpeed { get; set; }
        public string ColorOfSkin { get; set; }
        public string BreathsPerMinute { get; set; }
        public string PulsePerMinute { get; set; }
        public string Temp { get; set; }
        public string Other { get; set; }
        public int? Period { get; set; }
    }
}
