using DataAccess.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.IPDModel
{
    public class OPDGENBRCA : VBaseModel
    {
        public Guid? VisitId { get; set; }
        public Guid? UserConfirmId { get; set; }
        public DateTime? UserConfirmAt { get; set; }
        public string TypeConfirm { get; set; }
    }
}
