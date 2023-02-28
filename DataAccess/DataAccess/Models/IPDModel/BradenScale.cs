using DataAccess.Models.BaseModel;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.IPDModel
{
    public class BradenScaleContent
    {
        public string ViName { get; set; }
        public string EnName { get; set; }
    }

    /// <summary>
    /// BRADEN SCALE FOR PREDICTING PRESSURE SCORE RISK
    /// Bảng điểm Braden đánh giá nguy cơ loét (A02_056_050919_VE)
    /// </summary>
    [Table("IPD_BRADEN_SCALE")]
    public class BradenScale : VBaseModel
    {
        [NotMapped]
        public int RowNum { get; set; }

        [Column("VISIT_ID")]
        public Guid? VisitId { get; set; }

        /// <summary>
        /// Tổng điểm
        /// </summary>
        [NotMapped]
        public string TotalScore { get; set; }

        /// <summary>
        /// Phân loại nguy cơ
        /// </summary>
        [NotMapped]
        public string Classify { get; set; }

        /// <summary>
        /// Can thiệp
        /// </summary>
        [NotMapped]
        public string Intervention { get; set; }


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
