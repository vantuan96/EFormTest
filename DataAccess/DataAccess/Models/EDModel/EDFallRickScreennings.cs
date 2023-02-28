using DataAccess.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.EDModel
{
    public class EDFallRickScreennings : VBaseModel
    {
        public Guid VisitId { get; set; }
        public DateTime TransessionDate { get; set; }
        public string ClinicName { get; set; }
    }
}
