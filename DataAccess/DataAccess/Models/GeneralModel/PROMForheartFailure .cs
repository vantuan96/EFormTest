using DataAccess.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.GeneralModel
{
    public class PROMForheartFailure : VBaseModel
    {
        public Guid? VisitId { get; set; }
        public string VisitType { get; set; }
        public Guid? UserConfirmId { get; set; }
        public DateTime? UserConfirmAt { get; set; }
        public string Version { get; set; }

    }
}
