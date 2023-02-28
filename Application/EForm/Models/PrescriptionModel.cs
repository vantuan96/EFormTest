using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EForm.Models
{
    public class PrescriptionModel
    {
        public string PrescriberAD { get; set; }
        public string PrescriptionCode { get; set; }
        public string PrescriptionId { get; set; }
        public DateTime CreatedDate { get; set; }    
        public string PID { get; set; }
        public string VisitCode { get; set; }
        public string PrescriberName { get; set; }
        public string PrimaryDoctorAD { get; set; }
        public string VisitType { get; set; }
    }
}
