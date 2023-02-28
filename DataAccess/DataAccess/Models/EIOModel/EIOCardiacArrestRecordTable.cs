using DataAccess.Model.BaseModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.EIOModel
{
    public class EIOCardiacArrestRecordTable : IEntity
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
        public DateTime? Time { get; set; }
        public string Rhythm { get; set; }
        public string Defib { get; set; }
        public string Andrenalin { get; set; }
        public string Amiodarone { get; set; }
        public string Other { get; set; }
        public Guid? EIOCardiacArrestRecordId { get; set; }
        public virtual EIOCardiacArrestRecord EIOCardiacArrestRecord { get; set; }
    }
}
