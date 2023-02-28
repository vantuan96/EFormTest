using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EForm.Models.MedicationAdministrationRecordModels
{
    public class MedicationGroupByVisitCode
    {
        public string VisitCode { get; set; }
        public string PatientArea { get; set; }
        public DateTime VisitCreatedDate { get; set; }
        public string PrimaryDoctorAD { get; set; }
    }
}
