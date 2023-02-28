using DataAccess.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    public class Translation : IEntity
    {
        public Translation()
        {
            this.TranslationDatas = new HashSet<TranslationData>();
        }
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
        public string TranslatedBy { get; set; }
        public string ToLanguage { get; set; }
        public string FromLanguage { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }
        public string PID { get; set; }
        public string VisitCode { get; set; }
        public string CustomerName { get; set; }
        public Guid? CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
        public Guid? VisitId { get; set; }
        public string VisitTypeGroupCode { get; set; }
        public Guid? SiteId { get; set; }
        public Guid? SpecialtyId { get; set; }
        public virtual ICollection<TranslationData> TranslationDatas { get; set; }
        public Guid? FormId { get; set; }
        public string FormCode { get; set; }

    }
}
