using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace EMRModels
{
    public class EkipFromOrModel
    {
        public string FullName_Nurse_Runout_5 { get; set; }
        public string FullName_Nurse_Runout_6 { get; set; }
        public string FullName_KTV_CDHA { get; set; }
        public string UserName_Nurse_HT { get; set; }
        public string UserName_Nurse_HS { get; set; }
        public string FullName_Nurse_PhuMe_1 { get; set; }
        public string FullName_Nurse_PhuMe_2 { get; set; }
        public string UserName_PTV_Phu_2 { get; set; }
        public string UserName_KTV_PhuMo { get; set; }
        public string UserName_PTV_Phu_3 { get; set; }
        public string UserName_PTV_Phu_1 { get; set; }
        public string UserName_PTV_Phu_6 { get; set; }
        public DateTime? ThoiGianThucHien { get; set; }
        public string UserName_PTV_Phu_7 { get; set; }
        public string ServiceType { get; set; }
        public string UserName_PTV_Phu_4 { get; set; }
        public string UserName_PTV_Phu_5 { get; set; }
        public string UserName_Bs_CDHA { get; set; }
        public string UserName_Bs_GayMe { get; set; }
        public string FullName_PTV_CEC { get; set; }
        public bool IsDeleted { get; set; }
        public string RoomName { get; set; }
        public string FullName_Bs_SS { get; set; }
        public string FullName_Bs_PhuMe_1 { get; set; }
        public string FullName_Bs_PhuMe_2 { get; set; }
        public string UserName_PTV_CEC { get; set; }
        public string UserName_Nurse_Tool_1 { get; set; }
        public string UserName_Bs_PhuMe_2 { get; set; }
        public string UserName_Bs_PhuMe_1 { get; set; }
        public string UserName_Nurse_Tool_2 { get; set; }
        public string HospitalCode { get; set; }
        public string FullName_PTV_Phu_2 { get; set; }
        public string FullName_PTVChinh { get; set; }
        public string FullName_PTV_Phu_3 { get; set; }
        public string FullName_PTV_Phu_1 { get; set; }
        public string UserName_Nurse_Runout_6 { get; set; }
        public string UserName_Nurse_Runout_5 { get; set; }
        public string UserName_Nurse_Runout_4 { get; set; }
        public string UserName_Nurse_PhuMe_2 { get; set; }
        public string UserName_Nurse_Runout_3 { get; set; }
        public string UserName_Nurse_PhuMe_1 { get; set; }
        public string UserName_Nurse_Runout_2 { get; set; }
        public string UserName_Nurse_Runout_1 { get; set; }
        public string ItemCode { get; set; }
        public string FullName_PTV_Phu_6 { get; set; }
        public string FullName_Bs_GayMe { get; set; }
        public string FullName_Nurse_HS { get; set; }
        public string FullName_PTV_Phu_7 { get; set; }
        public string FullName_Nurse_HT { get; set; }
        public string FullName_PTV_Phu_4 { get; set; }
        public string FullName_PTV_Phu_5 { get; set; }
        public string FullName_PTV_Phu_8 { get; set; }
        public string UserName_KTV_CDHA { get; set; }
        public string FullName_Bs_CDHA { get; set; }
        public string ItemName { get; set; }
        public string FullName_KTV_CEC { get; set; }
        public string FullName_Bs_KhamMe { get; set; }
        public string UserName_Bs_SS { get; set; }
        public string FullName_Nurse_Tool_1 { get; set; }
        public string FullName_Nurse_Tool_2 { get; set; }
        public string PID { get; set; }
        public string UserName_PTV_Phu_8 { get; set; }
        public string UserName_KTV_CEC { get; set; }
        public string UserName_Bs_KhamMe { get; set; }
        public string UserName_PTVChinh { get; set; }
        public string FullName_Nurse_Runout_3 { get; set; }
        public string FullName_Nurse_Runout_4 { get; set; }
        public string FullName_Nurse_Runout_1 { get; set; }
        public string FullName_Nurse_Runout_2 { get; set; }
        public string ChargeId { get; set; }
        public string FullName_KTV_PhuMo { get; set; }
    }
    public class ListItemEkipFromOrModel
    {
        public string ThoiGianThucHien { get; set; }     
        public string ItemCode { get; set; }        
        public string ItemName { get; set; }
        public List<EkipFromOrModel> ListItem { get; set; }       
    }
}