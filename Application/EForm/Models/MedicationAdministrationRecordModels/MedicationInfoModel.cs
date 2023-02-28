using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EForm.Models.MedicationAdministrationRecordModels
{
    public class MedicationInfoModel
    {
        public string HeightMessage { get; set; } //Thông báo nếu thiếu chiều cao
        public string WeightMessage { get; set; } //Thông báo nếu thiếu cân nặng
        public string DiagnosisMessage { get; set; } //Thông báo nếu thiếu chẩn đoán
        public string VisitCodeMessage { get; set; } //Thông báo nếu thiếu visit code
        public string AllergyMessage { get; set; }  //Thông báo nếu thiếu thông tin dị ứng
        public Site HospitalInfo { get; set; } //Thông tin bệnh viện
        public CustomerMedicationInfoModel CustomerMedicationInfo { get; set; } = new CustomerMedicationInfoModel(); //Các thông tin về bệnh nhân
        public string Specialty { get; set; }
        public string NoteOfDoctor { get; set; } // Lời dặn của bác sĩ
        public Customer CustomerInfo { get; set; }
    }
}
