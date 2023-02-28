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
    public class IPDMedicalRecordOfPatients : VBaseModel
    {
        public Guid VisitId { get; set; }
        [MaxLength(50)]
        public string FormCode { get; set; } 
        
        public Guid? FormId { get; set; }
        public int Version { get; set; }
    }
}
