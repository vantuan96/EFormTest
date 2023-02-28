using DataAccess.Model.BaseModel;
using DataAccess.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.GeneralModel
{
    // [Table("SendNotificationLog")]
    public class SendMailNotification : VBaseModel
    {
        public Guid? FormId { get; set; }
        public Guid? ReceiverId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string To { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string ErrorMessenge { get; set; }
        public int ErrorCount { get; set; } = 0;
    }
}