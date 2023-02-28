using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EForm.Models.EIOModels
{
    public class EIOCardiacArrestRecordTableModel
    {
        public Guid Id { get; set; }
        public string Time { get; set; }
        public bool Remove { get; set; }
        public dynamic VitalSign { get; set; }
        public dynamic DefibEnergy { get; set; }
        public dynamic MedicationBolus { get; set; }
        public dynamic MedicationInfusion { get; set; }
        public dynamic Other { get; set; }
        public string CreatedBy { get; set; }
    }
}