using DataAccess.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.IPDModel
{
    public class IPDGlamorgan : VBaseModel
    {
        public Guid? VisitId { get; set; }
        public Guid? NurseConfirmId { get; set; }
        public DateTime? NurseConfirmAt { get; set; }
        public string Total { get; set; }
        public string Level { get; set; }
        public string Intervention { get; set; }
        public string InterventionOther { get; set; }
        public DateTime? StartingAssessment { get; set; }
        public int Number { get; set; }
    }
}
