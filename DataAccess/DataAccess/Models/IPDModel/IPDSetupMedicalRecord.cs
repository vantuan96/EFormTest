using DataAccess.Model.BaseModel;
using DataAccess.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.IPDModel
{
    public class IPDSetupMedicalRecord : VBaseModel
    {
        public Guid SpecialityId { get; set; }
        [MaxLength(50)]
        public string Formcode { get; set; }
        public string ViName { get; set; }
        public string EnName { get; set; }
        public bool IsDeploy { get; set; }
        public string Type { get; set; }
        public string FormType { get; set; }
    }
}
