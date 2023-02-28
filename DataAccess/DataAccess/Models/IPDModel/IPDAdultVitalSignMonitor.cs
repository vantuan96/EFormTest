using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.IPDModel
{
    public class IPDAdultVitalSignMonitor : IPDBase
    {
        public IPDAdultVitalSignMonitor()
        {
            this.IPDAdultVitalSignMonitorDatas = new HashSet<IPDAdultVitalSignMonitorData>();
        }
        public decimal? HeartRate { get; set; }
        public bool COPD { get; set; }
        public decimal? SPO2 { get; set; }
        public decimal? LowBP { get; set; }
        public decimal? HighBP { get; set; }
        public decimal? Pulse { get; set; }
        public decimal? Temperature { get; set; }
        public decimal? PainScore { get; set; }
        public decimal? CapillaryBloodGlucose { get; set; }
        public decimal? FluidInT { get; set; }
        public decimal? FluidInP { get; set; }
        public decimal? FluidInM { get; set; }
        public decimal? FluidInS { get; set; }
        public decimal? FluidInAN { get; set; }
        public decimal? FluidInD { get; set; }
        public decimal? FluidOutN { get; set; }
        public decimal? FluidOutPh { get; set; }
        public decimal? FluidOutNT { get; set; }
        public decimal? FluidOutDL { get; set; }
        public virtual ICollection<IPDAdultVitalSignMonitorData> IPDAdultVitalSignMonitorDatas { get; set; }

        [NotMapped]
        public int HeartRateMEWS
        {
            get
            {
                if (HeartRate <= 5 || HeartRate >= 35) return 3;
                if (HeartRate <= 9 || HeartRate >= 25) return 2;
                if (HeartRate <= 14 || HeartRate >= 20) return 1;
                return 0;
            }
        }

        [NotMapped]
        public int SPO2MEWS
        {
            get
            {
                if (SPO2 <= 84 || (SPO2 <= 83 && COPD)) return 3;
                if (SPO2 <= 89 || (SPO2 <= 85 && COPD)) return 2;
                if (SPO2 <= 94 || (SPO2 <= 87 && COPD)) return 1;
                return 0;
            }
        }

        [NotMapped]
        public int BloodPressureMEWS
        {
            get
            {
                if (HighBP <= 79 || HighBP >= 180) return 3;
                if (HighBP <= 89 || HighBP >= 160) return 2;
                if (HighBP >= 150 && HighBP <= 159) return 1;
                return 0;
            }
        }

        [NotMapped]
        public int PulseMEWS
        {
            get
            {
                if (Pulse <= 39 || Pulse >= 140) return 3;
                if (Pulse <= 49 || Pulse >= 120) return 2;
                if (Pulse <= 59 || Pulse >= 100) return 1;
                return 0;
            }
        }

        [NotMapped]
        public int TempMEWS
        {
            get
            {
                if (Temperature < 35 || Temperature >= 41) return 3;
                if (Temperature < (decimal)35.5 || Temperature > (decimal)38.4) return 2;
                if (Temperature < 36 || Temperature > 38) return 1;
                return 0;
            }
        }
    }
}
