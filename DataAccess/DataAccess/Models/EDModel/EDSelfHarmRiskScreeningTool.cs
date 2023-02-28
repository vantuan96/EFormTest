using System;

namespace DataAccess.Models.EDModel
{
    public class EDSelfHarmRiskScreeningTool : EDBase
    {
        public DateTime ScreeningTime { get; set; }

        public DateTime? ConfirmAt { get; set; }
        public string ConfirmBy { get; set; }
    }
}
