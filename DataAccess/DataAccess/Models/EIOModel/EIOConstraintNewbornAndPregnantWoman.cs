using DataAccess.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.EIOModel
{
    public class EIOConstraintNewbornAndPregnantWoman : VBaseModel
    {
        public Guid VisitId { get; set; }
        public Guid? NewbornCustomerId { get; set; }
        [ForeignKey("NewbornCustomerId")]
        virtual public Customer NewbornCustomer { get; set; }
        public string VisitTypeCode { get; set; }
        public Guid PregnantWomanCustomerId { get; set; }
        public string FormCode { get; set; }
        public string Room { get; set; }
        public string Bed { get; set; }
    }
}
