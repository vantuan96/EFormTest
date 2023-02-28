using DataAccess.Model.BaseModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    public class MasterData : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }
        public string DeletedBy { get; set; }
        public string ViName { get; set; }
        public string EnName { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        [Index]
        public string Code { get; set; }
        public string DataType { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        [Index]
        public string Group { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        [Index]
        public string Form { get; set; }
        [Index]
        public Nullable<int> Order { get; set; }
        public Nullable<int> Level { get; set; }
        public string Note { get; set; }
        public string Data { get; set; }
        public bool IsReadOnly { get; set; }
        public string Clinic { get; set; }
        public string Version { get; set; } = "1";
        public string DefaultValue { get; set; }
    }
}
