using EMRModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EForm.Models.MedicationAdministrationRecordModels
{
    public class CustomerMedicationInfoModel
    {
        public string Height { get; set; } //Chiều cao
        public string Weight { get; set; } //Cân nặng
        private string bmi;
        public string BMI
        {
            get
            {
                try
                {
                    if (Height != null && Height != "" && Weight != null && Weight != "")
                    {
                        var height = Convert.ToDouble(Height);
                        var weight = Convert.ToDouble(Weight);
                        var doubleBmi = weight / (Math.Pow(height / 100, 2));
                        if (doubleBmi < 16)
                        {
                            return $"BMI: {doubleBmi:0.00} - Tình trạng dinh dưỡng: Suy dinh dưỡng nặng.";
                        }
                        else if (doubleBmi >= 16 && doubleBmi <= 16.99)
                        {
                            return $"BMI: {doubleBmi:0.00} - Tình trạng dinh dưỡng: Suy dinh dưỡng vừa.";
                        }
                        else if (doubleBmi >= 17 && doubleBmi <= 18.49)
                        {
                            return $"BMI: {doubleBmi:0.00} - Tình trạng dinh dưỡng: Suy dinh dưỡng nhẹ.";
                        }
                        else if (doubleBmi >= 18.5 && doubleBmi <= 24.99)
                        {
                            return $"BMI: {doubleBmi:0.00} - Tình trạng dinh dưỡng: Bình thường.";
                        }
                        else if (doubleBmi >= 25 && doubleBmi <= 29.99)
                        {
                            return $"BMI: {doubleBmi:0.00} - Tình trạng dinh dưỡng: Thừa cân.";
                        }
                        else
                        {
                            return $"BMI: {doubleBmi:0.00} - Tình trạng dinh dưỡng: Béo phì.";
                        }
                    }
                    else
                    {
                        return "BMI:";
                    }
                }
                catch (Exception)
                {
                    return "BMI:";
                }
            }
            set 
            {
                bmi = value;
            } 
        }
        public string Allergy { get; set; } //Dị ứng
        public string InsuranceCardNo { get; set; } //Số thẻ BHYT
        public string ExpireDate { get; set; } //Ngày hết hạn
        public DiagnosisAndICDModel DiagnosisInfo { get; set; } //Thông tin chẩn đoán
        public string DateOfBirth { get; set; } //Ngày tháng năm sinh
        public bool IsOver18Age { get; set; } //Kiểm tra xem bệnh nhân có phải trên 18 tuổi hay ko
        public string DiagnosisFromViHC { get; set; } // Chẩn đoán từ ViHC
    }
}
