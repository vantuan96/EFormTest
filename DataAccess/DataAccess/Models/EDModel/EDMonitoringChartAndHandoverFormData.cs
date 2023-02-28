using DataAccess.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.EDModel
{
    public class EDMonitoringChartAndHandoverFormData: IEntity
    {
        public Guid Id { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }
        public string DeletedBy { get; set; }
        public string Path { get; set; }
        public Nullable<Guid> MonitoringChartAndHandoverFormId { get; set; }
        [ForeignKey("MonitoringChartAndHandoverFormId")]
        public virtual EDMonitoringChartAndHandoverForm MonitoringChartAndHandoverForm { get; set; }
    }
}
