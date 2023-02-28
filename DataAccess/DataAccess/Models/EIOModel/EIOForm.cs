using DataAccess.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.EIOModel
{
    public class EIOForm : VBaseModel
    {
        public Guid? VisitId { get; set; }
        public Guid? FormId { get; set; } // nếu gắn với 1 form nào đó đã có, ví dụ bệnh án
        public string FormCode { get; set; }
        public string Note { get; set; } // bỏ
        public string Comment { get; set; } // bỏ
        public string VisitTypeGroupCode { get; set; }
        public string ConfirmBy { get; set; } // bỏ
        public DateTime? ConfirmAt { get; set; } // bỏ
        public int? Version { get; set; } // bỏ
    }
    public class EIOFormConfirm : VBaseModel
    {
        public Guid? FormId { get; set; } // FK với Id của EIOForm
        public string Note { get; set; } // bỏ
        public string ConfirmType { get; set; } // Chân ký 1, chân ký 2
        public string ConfirmBy { get; set; } // username
        public DateTime? ConfirmAt { get; set; }
    }
}
