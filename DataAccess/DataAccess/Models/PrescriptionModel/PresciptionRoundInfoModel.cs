using DataAccess.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.PrescriptionModel
{
    public class PresciptionRoundInfoModel: VBaseModel
    {
        public Guid PrescriptionId { get; set; }
        public string PrescriptionType { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? Round { get; set; }
    }
}
