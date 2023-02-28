using DataAccess.Models.BaseModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.IPDModel
{
    public class IPDConfirmDischarge : VBaseModel
    {

        public Guid? VisitId { get; set; }
        [ForeignKey("VisitId")]
        [JsonIgnore]
        public virtual IPD Visit { get; set; }

        public string CustomerName { get; set; } //Họ tên người bệnh/người nhà
        public bool IsSignToConfirm { get; set; } //NB/ Người giám hộ có kí xác nhận hay không
 
        public string ImageUrl { get; set; } //Đường dẫn ảnh Giấy xác nhận ra viện đã ký
        public string Witness { get; set; } //Người làm chứng

        public string DoctorUsername { get; set; } //Username của bác sĩ
        public string DoctorFullName { get; set; } //Tên đầy đủ của bác sĩ
        public string ReasonDischarge { get; set; } //Lý do ra viện
        
    }
}
