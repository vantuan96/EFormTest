using DataAccess.Model.BaseModel;
using System;
using System.Collections.Generic;

namespace DataAccess.Models.OPDModel
{
    public class OPDFallRiskScreening : IEntity
    {
        public OPDFallRiskScreening()
        {
            this.OPDFallRiskScreeningDatas = new HashSet<OPDFallRiskScreeningData>();
        }

        public Guid Id { get; set; }
        public Guid VisitId { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }
        public string DeletedBy { get; set; }
        public int Version { get; set; }
        public virtual ICollection<OPDFallRiskScreeningData> OPDFallRiskScreeningDatas { get; set; }
    }
}
