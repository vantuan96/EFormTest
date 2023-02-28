using DataAccess.Model.BaseModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.EIOModel
{
    public class EIOBloodSupplyData: IEntity
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
        public string Name { get; set; }
        public int Quanlity { get; set; }
        public string NurseUser { get; set; }
        public DateTime? NurseTime { get; set; }
        public string CuratorUser { get; set; }
        public DateTime? CuratorTime { get; set; }
        public string ProviderUser { get; set; }
        public DateTime? ProviderTime { get; set; }
        public Guid? EIOBloodRequestSupplyAndConfirmationId { get; set; }
        [ForeignKey("EIOBloodRequestSupplyAndConfirmationId")]
        public virtual EIOBloodRequestSupplyAndConfirmation EIOBloodRequestSupplyAndConfirmation { get; set; }
    }
}
