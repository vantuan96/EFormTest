using DataAccess.Models.BaseModel;
using System;

namespace DataAccess.Models.IPDModel
{
    /// <summary>
    /// Bảng theo dõi DHST dành cho trẻ sơ sinh (Neonatal Observation Chart)
    /// </summary>
    public class IPDNeonatalObservationChart: VBaseModel
    {
        public Guid? VisitId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Note { get; set; }
    }
}
