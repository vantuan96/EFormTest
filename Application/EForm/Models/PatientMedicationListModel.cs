using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EForm.Models
{
    public class PatientMedicationListModel
    {
        public string Drug { get; set; }
        public string Dosage { get; set; }
        public string Route { get; set; }
        public string LastDoseDate { get; set; }
    }
}