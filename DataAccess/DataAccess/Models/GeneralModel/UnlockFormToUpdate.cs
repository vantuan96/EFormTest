using DataAccess.Model.BaseModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class UnlockFormToUpdate: IEntity
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
        public string Username { get; set; }
        public Guid? VisitId { get; set; }       
        public string RecordCode { get; set; }
        public string FormCode { get; set; }
        public string FormName { get; set; }
        public DateTime? ExpiredAt { get; set; }
        public Guid? FormId { get; set; }
    }
}
