using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EForm.Models
{
    public class EIOFormConfirmModel
    {
        
        public string FormCode { get; set; }
        public Guid Id { get; set; }
        public Guid? FormId { get; set; }
        public DateTime? ConfirmAt { get; set; }
        public string ConfirmBy { get; set; }
        public string Fullname { get; set; }
        public string Title { get; set; }

        public string Note { get; set; } // bỏ
        public string ConfirmType { get; set; } // Chân ký 1, chân ký 2
        public string Department { get; set; }
    }
}