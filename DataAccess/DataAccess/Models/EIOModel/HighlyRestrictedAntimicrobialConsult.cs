using DataAccess.Model.BaseModel;
using DataAccess.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.EIOModel
{
    public class HighlyRestrictedAntimicrobialConsult : VBaseModel
    {
        public Guid VisitId { get; set; }
        public string VisitTypeGroupCode { get; set; }
        public DateTime? ConfirmDate { get; set; }
        public Guid? ConfirmDoctorId { get; set; }
        [ForeignKey("ConfirmDoctorId")]
        public virtual User ConfirmDoctor { get; set; }
        public virtual ICollection<HighlyRestrictedAntimicrobialConsultMicrobiologicalResults> HighlyRestrictedAntimicrobialConsultMicrobiologicalResults { get; set; }
        public virtual ICollection<HighlyRestrictedAntimicrobialConsultAntimicrobialOrder> HighlyRestrictedAntimicrobialConsultAntimicrobialOrder { get; set; }
    }
}
