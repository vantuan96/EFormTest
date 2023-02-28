using DataAccess.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.EIOModel
{
    public class PreAnesthesiaConsultation: VBaseModel
    {
        public Guid VisitId { get; set; }
        public string VisitTypeGroupCode { get; set; }
    }
}
