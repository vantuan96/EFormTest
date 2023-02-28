using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EForm.Models.MedicationAdministrationRecordModels
{
    public class Age
    {
        public readonly int Years;
        public readonly int Months;
        public readonly int Days;

        public Age()
        {
        }

        public Age(int y, int m, int d) : this()
        {
            Years = y;
            Months = m;
            Days = d;
        }
    }

}
