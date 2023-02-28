using DataAccess.Model.BaseModel;
using DataAccess.Models.BaseModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.OPDModel
{
    public class OPDClinicalBreastExamNote : VBaseModel
    {

        public Guid? VisitId { get; set; }
        public Guid? DoctorConfirmId { get; set; }
        public DateTime? DoctorConfirmAt { get; set; }       
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Order { get; set; }

    }
}
