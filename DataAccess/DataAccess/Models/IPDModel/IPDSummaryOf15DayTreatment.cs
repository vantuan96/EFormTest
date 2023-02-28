using DataAccess.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.IPDModel
{
    public class IPDSummaryOf15DayTreatment: VBaseModel
    {
        public Guid? VisitId { get; set; }
        public string RoomId { get; set; }
        public Guid? HeadOfDepartmentConfirmId { get; set; }
        public DateTime? HeadOfDepartmentConfirmAt { get; set; }
        public Guid? PhysicianConfirmId { get; set; }
        public DateTime? PhysicianConfirmAt { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Order { get; set; }

    }
}
