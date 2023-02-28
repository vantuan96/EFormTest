using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EForm.Models
{
    public class LabResultModel
    {
        public string Date { get; set; }
        public DateTime RawDate { get; set; }
        public List<CategoryLabResultModel> Categories { get; set; }
    }

    public class CategoryLabResultModel
    {
        public string Name { get; set; }
        public List<ProfileLabResultModel> Profiles { get; set; }
    }

    public class ProfileLabResultModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public List<dynamic> Datas { get; set; }
    }

    public class LabResultModelByPID
    {
        public string Date { get; set; }
        public DateTime RawDate { get; set; }
        public List<CategoriesLabResultModel> Categories { get; set; }
    }

    public class CategoriesLabResultModel
    {
        public string Name { get; set; }
        public List<ProfileLabResult> Profiles { get; set; }
    }

    public class ProfileLabResult
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public List<ItemLabResult> Datas { get; set; }
    }
    public class ItemLabResult
    {
        public string SID { get; set; }
        public string TestName { get; set; }
        public string TestCode { get; set; }
        public string Result { get; set; }
        public string LowerLimit { get; set; }
        public string HigherLimit { get; set; }
        public string NormalRange { get; set; }
        public int Status { get; set; }
        public string Unit { get; set; }
        public string UpdateTime { get; set; }
        public DateTime RawUpdateTime { get; set; }
        public bool Copyed { get; set; }
        public string ObjKey
        {
            get
            {
                return $"{SID}-{TestCode}-{Result}-{Status.ToString()}-{UpdateTime}";
            }
        }
        public string SortKey
        {
            get
            {
                return $"{RawUpdateTime.ToString()}-{TestCode}";
            }
        }
        public int Index { get; set; } = 0;
    }

    public class XRayResultModel
    {
        public Guid? Id { get; set; }
        public string Date { get; set; }
        public DateTime RawDate { get; set; }
        public List<dynamic> Datas { get; set; }
    }
    public class RISResultModel
    {
        public Guid? Id { get; set; }
        public string Mota { get; set; }
        public string ket_luan { get; set; }
        public string html { get; set; }
    }
    public class RISResultModelV2
    {
        public string ReportID { get; set; }
        public string TenDichVu { get; set; }
        public string KetLuan { get; set; }
        public string html { get; set; }
        public string DoctorAD { get; set; }
        public string ReportDate { get; set; }
    }
    public class LISResult
    {
        public string Id { get; set; }
        public bool IsHeader { get; set; }
        public string ViName { get; set; }
        public string EnName { get; set; }
        public string Code { get; set; }
        public List<ResultModel> Datas { get; set; }
        public int Count { get; set; }
        public List<string> ResultList { get; set; }
    }
    public class LISResultCols
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime? ResultAt { get; set; }
        public string Ad { get; set; }
        public string SID { get; set; }
        public string VisitCode { get; set; }
        public ResultModel Item { get; set; }
    }
    public class LabResultDataModel
    {
        public string CategoryID { get; set; }
        public string ProfileID { get; set; }
        public string TestName { get; set; }
        public string CategoryNameE { get; set; }
        public string TestCode { get; set; }
        public string TestNameE { get; set; }
        public string ProfileName { get; set; }
        public string Unit { get; set; }
        public string IsProfile { get; set; }
        public string SID { get; set; }
        public string Result { get; set; }
        public string NormalRange { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string CategoryName { get; set; }
        public string LowerLimit { get; set; }
        public string HigherLimit { get; set; }
        public bool IsCurrentResult { get; set; }
        public int Index { get; set; } = 0;
    }
    public class LISInfor
    {
        public string CategoryCode { get; set; }
        public string ItemNameV { get; set; }
        public string ItemCode { get; set; }
        public string ResultBy { get; set; }
        public string SiteCode { get; set; }
        public string CategoryNameE { get; set; }
        public DateTime? ResultAt { get; set; }
        public string LocationNameV { get; set; }
        public string PID { get; set; }
        public string VisitCode { get; set; }
        public DateTime? ServiceDate { get; set; }
        public string CategoryNameV { get; set; }
        public string LocationNameE { get; set; }
        public string ItemNameE { get; set; }
        public string ServiceByAD { get; set; }
        public string SID { get; set; }
    }
    public class RISReportByPIDModel
    {
        public string CategoryCode { get; set; }
        public string ItemNameV { get; set; }
        public string ItemCode { get; set; }
        public string ResultBy { get; set; }
        public string SiteCode { get; set; }
        public string CategoryNameE { get; set; }
        public DateTime? ResultAt { get; set; }
        public string LocationNameV { get; set; }
        public Guid? ReportId { get; set; }
        public string PID { get; set; }
        public string VisitCode { get; set; }
        public DateTime? ServiceDate { get; set; }
        public string CategoryNameV { get; set; }
        public string LocationNameE { get; set; }
        public string ItemNameE { get; set; }
        public string ServiceByAD { get; set; }
    }
    public class ResultModel
    {
        public string CategoryCode { get; set; }
        public Object ResultBy { get; set; }
        public Object ItemCode { get; set; }
        public Object ItemNameV { get; set; }
        public string CategoryNameE { get; set; }
        public string SiteCode { get; set; }
        public DateTime? ResultAt { get; set; }
        public string TestCode { get; set; }
        public string CollectedBy { get; set; }
        public string TestNameE { get; set; }
        public string PID { get; set; }
        public string Unit { get; set; }
        public string SID { get; set; }
        public string Result { get; set; }
        public string NormalRange { get; set; }
        public string LowerLimit { get; set; }
        public string TestNameV { get; set; }
        public DateTime? CollectedAt { get; set; }
        public Object ItemNameE { get; set; }
        public string HigherLimit { get; set; }
        public string ServiceByAD { get; set; }
        public string Sub_SID { get; set; }
        public string itemCode { get; set; }
        public string VisitCode { get; set; }
    }
    public class ParamModel
    {
        public string itemcode { get; set; }       
    }
    public class LabReultModel
    {
        public string TestName { get; set; }
        public string TestCode { get; set; }
        public string Result { get; set; }
        public string LowerLimit { get; set; }
        public string HigherLimit { get; set; }
        public bool IsProfile { get; set; }
        public string Unit { get; set; }
        public DateTime UpdateTime { get; set; }
        public string ProfileName { get; set; }
        public string ProfileID { get; set; }
        public string UpdateTimeRaw {
            get
            {
                return UpdateTime.ToString("MM/dd/yyyy");
            }
        }
        public string CategoryName { get; set; }
        public string CategoryNameE { get; set; }
        public string CategoryID { get; set; }
    }
}