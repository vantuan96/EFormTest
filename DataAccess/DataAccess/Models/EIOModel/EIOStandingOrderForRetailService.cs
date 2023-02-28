using DataAccess.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.EIOModel
{
    public class EIOStandingOrderForRetailService: IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string DeletedBy { get; set; }
        public string Doctor { get; set; }
        public string Pulse { get; set; }
        public string Temparature { get; set; }
        public string BloodPressure { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Diagnosis { get; set; }
    }
}
