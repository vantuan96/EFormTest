using DataAccess.Model.BaseModel;
using DataAccess.Models.BaseModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    public class UnlockVip : VBaseModel
    {
        public Guid GroupId { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid? VisitId { get; set; }
        public string VisitCode { get; set; }
        public string RecodeCode { get; set; }
        public string Username { get; set; }
        public string PID { get; set; }
        public string Type { get; set; } // 1 HSBA // 2 CHI DINH // 3 DON THUOC // 4 Y LENH // 5 CĐHA
        public DateTime? ExpiredAt { get; set; }
        public string Note { get; set; }
        public string Files { get; set; }
        [NotMapped]
        public string StringExpiredAt { get; set; }

    }
}
