using DataAccess.Model.BaseModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.OPDModel
{
    public class OPDHandOverCheckListData: IEntity
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
        public string Code { get; set; }
        public string Value { get; set; }
        public string EnValue { get; set; }
        public Nullable<Guid> OPDHandOverCheckListId { get; set; }
        [ForeignKey("OPDHandOverCheckListId")]
        public virtual OPDHandOverCheckList OPDHandOverCheckList { get; set; }
    }
}
