using DataAccess.Model.BaseModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.EIOModel
{
    public class EIOBloodProductData: IEntity
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
        public string Code { get; set; }
        public string Note { get; set; }
        public int Quanlity { get; set; }
        public int Capacity { get; set; }
        public bool IsConfirm { get; set; }
        public string BloodTypeABO { get; set; }
        public string BloodTypeRH { get; set; }
        public DateTime? Time { get; set; }
        public Guid? FormId { get; set; }
        public Guid? GroupId { get; set; }
        public string FormName { get; set; }
        public DateTime? TransmissionTime { get; set; }
    }
}
