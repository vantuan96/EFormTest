using DataAccess.Model.BaseModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class MedicationMasterdata: IEntity
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
        public string Content { get; set; }
        public bool IsStop { get; set; }
        public string Manufactory { get; set; }
        public string Name { get; set; }
        public string AutoComplete { get; set; }
        public string ActiveMaterial { get; set; }
        public string System { get; set; }
    }
}