using System;

namespace DataAccess.Model.BaseModel
{
    public interface IEntity
    {
        Guid Id { get; set; }
        bool IsDeleted { get; set; }
        string DeletedBy { get; set; }
        Nullable<DateTime> DeletedAt { get; set; }
        string CreatedBy { get; set; }
        Nullable<DateTime> CreatedAt { get; set; }
        string UpdatedBy { get; set; }
        Nullable<DateTime> UpdatedAt { get; set; }
    }
}
