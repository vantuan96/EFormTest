using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EForm.Models.MedicationAdministrationRecordModels
{
    public class DependentProfileModel
    {
        public bool IsDependentProfile { get; set; }
        public string MommyPID { get; set; }
        public Customer Baby { get; set; }
    }
}
