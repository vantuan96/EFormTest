using DataAccess.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.EDModel
{
    public class EDMonitoringChartAndHandoverForm: IEntity
    {
        public EDMonitoringChartAndHandoverForm()
        {
            this.MonitoringChartAndHandoverFormDatas = new HashSet<EDMonitoringChartAndHandoverFormData>();
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
        public string Note { get; set; }
        public virtual ICollection<EDMonitoringChartAndHandoverFormData> MonitoringChartAndHandoverFormDatas { get; set; }
    }
}
