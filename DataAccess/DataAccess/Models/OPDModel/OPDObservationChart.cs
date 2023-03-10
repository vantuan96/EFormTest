using DataAccess.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.OPDModel
{
    public class OPDObservationChart: IEntity
    {
        public OPDObservationChart()
        {
            this.ObservationChartDatas = new HashSet<OPDObservationChartData>();
        }
        [Key]
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual ICollection<OPDObservationChartData> ObservationChartDatas { get; set; }
    }
}
