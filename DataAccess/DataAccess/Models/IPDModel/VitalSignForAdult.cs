using DataAccess.Models.BaseModel;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.IPDModel
{

    public class VitalSignForAdultContent
    {
        public string ViName { get; set; }
        public string EnName { get; set; }
    }

    [Table("IPD_VITALSIGN_ADULT")]
    public class VitalSignForAdult : VBaseModel
    {
        [NotMapped]
        public int RowNum { get; set; }

        [Column("VISIT_ID")]
        public Guid? VisitId { get; set; }

        /// <summary>
        /// Nhịp thở
        /// </summary>
        [Column("BREATH_RATE")]
        [Description("Nhip tho")]
        [NotMapped]
        public decimal? BreathRate { get; set; }

        [NotMapped]
        public int? BreathMEWS
        {
            get
            {
                if (BreathRate <= 5 || BreathRate >= 35) return 3;
                if ((BreathRate >= 25 && BreathRate <= 34) || (BreathRate >= 6 && BreathRate <= 9)) return 2;
                if ((BreathRate >= 20 && BreathRate <= 24) || (BreathRate >= 10 && BreathRate <= 14)) return 1;
                return 0;
            }
        }

        /// <summary>
        /// COPD
        /// </summary>
        [Column("COPD")]
        public bool COPD { get; set; }

        /// <summary>
        /// Chỉ số spo2
        /// </summary>
        [Column("SPO2")]
        [NotMapped]
        public decimal? SPO2 { get; set; }

        [NotMapped]
        public int? SPO2MEWS
        {
            get
            {
                if (SPO2 <= 84 || (SPO2 <= 83 && COPD)) return 3;
                if ((SPO2 >= 85 && SPO2 <= 89) || (SPO2 >= 84 && SPO2 <= 85 && COPD)) return 2;
                if ((SPO2 >= 90 && SPO2 <= 94) || (SPO2 >= 86 && SPO2 <= 87 && COPD)) return 1;
                return 0;
            }
        }

        /// <summary>
        /// Huyết áp tối thiểu
        /// </summary>
        [Column("LOW_BP")]
        [NotMapped]
        public decimal? LowBP { get; set; }

        /// <summary>
        /// Huyết áp tối đa
        /// </summary>
        [Column("HIGH_BP")]
        [NotMapped]
        public decimal? HighBP { get; set; }

        [NotMapped]
        public int? BPMEWS
        {
            get
            {
                if (HighBP <= 79 || HighBP >= 180) return 3;
                if ((HighBP >= 160 && HighBP <= 179) || (HighBP >= 80 && HighBP <= 89)) return 2;
                if (HighBP >= 150 && HighBP <= 159) return 1;
                return 0;
            }
        }

        /// <summary>
        /// Mạch
        /// </summary>
        [Column("PULSE")]
        [NotMapped]
        public decimal? Pulse { get; set; }

        [NotMapped]
        public int? PulseMEWS
        {
            get
            {
                if (Pulse < 39 || Pulse >= 140) return 3;
                if ((Pulse >= 120 && Pulse <= 139) || (Pulse >= 40 && Pulse <= 49)) return 2;
                if ((Pulse >= 50 && Pulse <= 59) || (Pulse >= 100 && Pulse <= 119)) return 1;
                return 0;
            }
        }

        [NotMapped]
        public string VIPScore { get; set; }

        [NotMapped]
        public string VIPScore_Vi
        {
            get
            {
                if (VIPScore == "00") return "Không có dấu hiệu viêm tĩnh mạch.\n Tiếp tục THEO DÕI vị trí đặt Catheter";
                if (VIPScore == "01") return "Có thể là dấu hiệu KHỞI ĐẦU viêm tĩnh mạch.\n Tiếp tục THEO DÕI vị trí đặt Catheter";
                if (VIPScore == "02") return "Viêm tĩnh mạch giai đoạn SỚM.\n RÚT Catheter";
                if (VIPScore == "03") return "Viêm tĩnh mạch giai đoạn TRUNG BÌNH.\n RÚT Catheter.\n Cân nhắc điều trị";
                if (VIPScore == "04") return "Viêm tĩnh mạch tiến triển hoặc KHỞI ĐẦU viêm tĩnh mạch huyết khối.\n RÚT Catheter.\n Cân nhắc điều trị";
                if (VIPScore == "05") return "Viêm tĩnh mạch huyết khối tiến triển.\n RÚT Catheter.\n Điều trị ngay";
                return "";
            }
        }

        [NotMapped]
        public string VIPScore_En
        {
            get
            {
                if (VIPScore == "00") return "No signs of phlebitis\nOBSERVE CANNULA";
                if (VIPScore == "01") return "Possisbly first signs of phlebitis\nOBSERVE CANNULA";
                if (VIPScore == "02") return "Early stage of phlebitis\nRESITE CANNULA";
                if (VIPScore == "03") return "Medium stage of phlebitis\nRESITE CANNULA\nCONSIDER TREATMENT";
                if (VIPScore == "04") return "Advanced stage of phlebitis or the start of thrombophlebitis\nRESITE CANNULA\nCONSIDER TREATMENT";
                if (VIPScore == "05") return "Advanced stage of thrombophlebitis\nINITIATE TREATMENT\nRESITE CANNULA";
                return "";
            }
        }


        /// <summary>
        /// Thân nhiệt
        /// </summary>
        [Column("TEMPERATURE")]
        [NotMapped]
        public decimal? Temperature { get; set; }

        [NotMapped]
        public int? TemperatureMEWS
        {
            get
            {
                if (Temperature < 35 || Temperature >= 41) return 3;
                if ((Temperature >= (decimal)38.5 && Temperature <= (decimal)40.9) || (Temperature >= 35 && Temperature <= (decimal)35.4)) return 2;
                if ((Temperature >= (decimal)35.5 && Temperature <= (decimal)35.9) || (Temperature >= 38 && Temperature <= (decimal)38.4)) return 1;
                return 0;
            }
        }

        /// <summary>
        /// Tri giác
        /// </summary>
        [Column("SENSE")]
        [NotMapped]
        public string Sense { get; set; }

        [NotMapped]
        public int SenseMews
        {
            get
            {
                if (Sense == "U" || Sense == "P") return 3;
                if (Sense == "V") return 2;
                return 0;
            }

        }

        /// <summary>
        /// Hỗ trợ hô hấp
        /// </summary>
        [NotMapped]
        public string RespiratorySupport { get; set; }

        [NotMapped]
        public int RespiratotySupportMews
        {
            get
            {
                if (Sense == "Oxy mark" || Sense == "Oxy kính") return 2;
                return 0;
            }
        }

        [NotMapped]
        public string Content { get; set; }

        [NotMapped]
        public string PainScore { get; set; }

        [NotMapped]
        public string CapillaryBlood { get; set; }

        [NotMapped]
        public string FluidIn { get; set; }

        [NotMapped]
        public string TotalFluidIn { get; set; }

        [NotMapped]
        public string FluidOut { get; set; }

        [NotMapped]
        public string TotalFluidOut { get; set; }

        [NotMapped]
        public string TotalBilan { get; set; }

        [NotMapped]
        public string TotalMews { get; set; }

        [Column("NOTE")]
        [MaxLength(100)]
        public string Note { get; set; }

        [NotMapped]
        public string DateFrom { get; set; }

        [NotMapped]
        public string DateTo { get; set; }

        [NotMapped]
        public int PageIndex { get; set; }

        [NotMapped]
        public int PageSize { get; set; }

        [NotMapped]
        public int TotalRow { get; set; }

        [Column("TRANS_DATE")]
        public DateTime TransactionDate { get; set; }

        [NotMapped]
        public string TransDate { get; set; }
    }
}