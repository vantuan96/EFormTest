using DataAccess.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.IPDModel
{
    public class IPDCoronaryIntervention: VBaseModel
    {
        public Guid? VisitId { get; set; }
        public Guid? DoctorConfirmId { get; set; }
        public DateTime? DoctorConfirmAt { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Order { get; set; }
        public int Version { get; set; }
    }
}
