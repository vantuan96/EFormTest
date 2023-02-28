using DataAccess.Models;
using DataAccess.Models.GeneralModel;
using DataAccess.Repository;
using EForm.Common;
using EForm.Models;
using EMRModels;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Xml.Linq;

namespace EForm.Client
{
    public class OHClient : HISClient
    {
        #region Đồng bộ DataHC
        public static List<DataHCModel> SyncDataPathologyMicrobiologyHC(string from, string to)
        {
            string url_postfix = string.Format("/EMRVinmecCom/1.0.0/getDataHC_V2?from_date={0}&to_date={1}", from, to);
            var response = RequestAPI(url_postfix, "Entries", "Entry");
            if (response != null)
                return JsonConvert.DeserializeObject<List<DataHCModel>>(response.ToString());
            return new List<DataHCModel>();
        }
        public static List<DataHCModel> SyncDataHc(string from, string to)
        {
            string url_postfix = string.Format("/EMRVinmecCom/1.0.0/getDataHC?from_date={0}&to_date={1}", from, to);
            var response = RequestAPI(url_postfix, "Entries", "Entry");
            if (response != null)
                return JsonConvert.DeserializeObject<List<DataHCModel>>(response.ToString());
            return new List<DataHCModel>();
        }
        #endregion

        public static List<LISInfor> getLISInforByPID(string pId, string from = null, string to = null)
        {
            string url_postfix = "";
            if (!string.IsNullOrEmpty(pId))
                url_postfix = string.Format("/EMRVinmecCom/1.0.0/getLISInforByPID?PID={0}", pId);
            //url_postfix = string.Format("/LABCONN_EMR/1.0.0/getLISInforByPID?PID={0}", pId);
            var dataFromOH = RequestAPI(url_postfix, "Entries", "Entry");

            var result = new List<LISInfor>();
            if (dataFromOH == null)
                return result;

            return new JavaScriptSerializer().Deserialize<List<LISInfor>>(dataFromOH.ToString());
        }
        public static List<LISInfor> getLISResultByPID(string pId)
        {
            string url_postfix = "";
            if (!string.IsNullOrEmpty(pId))
                url_postfix = string.Format("/LABCONN_EMR/1.0.0/getLISResultByPID?PID={0}", pId);
            var dataFromOH = RequestAPI(url_postfix, "Entries", "Entry");

            var result = new List<LISInfor>();
            if (dataFromOH == null)
                return result;

            return new JavaScriptSerializer().Deserialize<List<LISInfor>>(dataFromOH.ToString());
        }
        #region Get dịch vụ chung từ OH
        public static List<AlliedService> GetAlliedServiceFromOH(string pId)
        {
            string url_postfix = "";
            if (!string.IsNullOrEmpty(pId))
                url_postfix = string.Format("/EMRVinmecCom/1.0.0/getASInfor?PID={0}", pId);
            var res = RequestAPI(url_postfix, "Entries", "Entry");
            var results = new List<AlliedService>();
            // Ha xem ham ben trên parser json cho nhanh nhe
            if(res == null)
                return results;
            return new JavaScriptSerializer().Deserialize<List<AlliedService>>(res.ToString());
        }
        #endregion
        #region Get Chẩn đoán hình ảnh từ OH
        public static List<RadiologyResult> GetRadiologyFromOH(string pId)
        {
            string url_postfix = "";
            if (!string.IsNullOrEmpty(pId))
                url_postfix = string.Format("/EMRVinmecCom/1.0.0/getRISReportByPIDV2?PID={0}", pId);

            var res = RequestAPI(url_postfix, "DSChanDoan", "ChanDoan");          
            if (res == null)
                return new List<RadiologyResult>();

            return new JavaScriptSerializer().Deserialize<List<RadiologyResult>>(res.ToString());
        }
        #endregion

        #region Get Relationships Of Customer By PID
        public static List<dynamic> GetRelationshipOfCustormerByPid(string pId)
        {
            string url_postfix = "";
            if (!string.IsNullOrEmpty(pId))
                url_postfix = string.Format("/EMRVinmecCom/1.0.0/getPatientContact?pid={0}", pId);

            var res = RequestAPI(url_postfix, "contacts", "contact");
            if (res != null)
                return BuilRelationshipCustomer(res);

            return new List<dynamic>();
        }
        #endregion

        #region Search Customer By PID + Name

        public static List<dynamic> searchPatienteOh(SearchParameter search_parameter)
        {
            string url_postfix = "";
            if (!string.IsNullOrEmpty(search_parameter.PID))
            {
                url_postfix = string.Format("/EMRVinmecCom/1.0.0/searchPatientByPID?PID={0}", search_parameter.PID);
            }
            else if (!string.IsNullOrEmpty(search_parameter.TenBenhNhan))
            {
                url_postfix = string.Format("/EMRVinmecCom/1.0.0/searchPatientOH?TenBenhNhan={0}&NgaySinh={1}&GioiTinh={2}", search_parameter.TenBenhNhan, search_parameter.ConvertedNgaySinh, search_parameter.ConveredGioiTinh);
            }
            var response = RequestAPI(url_postfix, "Entries", "Entry");
            if (response != null)
                return BuildShortResultV2(response);
            return new List<dynamic>();
        }
        public static List<HisCustomer> searchPatienteOhByPidV3(string pid)
        {
            var url_postfix = string.Format("/EMRVinmecCom/1.0.0/searchPatientByPID?PID={0}", pid);
            var response = RequestAPI(url_postfix, "Entries", "Entry");
            if (response != null)
                return BuildShortResultV3(response);
            return new List<HisCustomer>();
        }
        public static List<dynamic> searchPatienteOhByPid(string pid)
        {
            var url_postfix = string.Format("/EMRVinmecCom/1.0.0/searchPatientByPID?PID={0}", pid);
            var response = RequestAPI(url_postfix, "Entries", "Entry");
            if (response != null)
                return BuildShortResultV2(response);
            return new List<dynamic>();
        }
        #endregion

        #region Get Visitcode
        public static List<dynamic> GetVisitCode(string pid, string visit_type_group = "ED")
        {
            string url_postfix = string.Format("/EMRVinmecCom/1.0.0/getVisitList?PID={0}", pid);
            var response = RequestAPI(url_postfix, "VisitSyncs", "VisitSync");
            if (response != null)
            {
                if (visit_type_group == "ED")
                    return BuildVisitEDCodeResult(response);
                if (visit_type_group == "OPD")
                    return BuildVisitOPDCodeResult(response);
                if (visit_type_group == "IPD")
                    return BuildVisitIPDCodeResult(response);
            }
            return new List<dynamic>();
        }
        public static string GetBsIPD(string pid, string visit_code)
        {
            string url_postfix = string.Format("/EMRVinmecCom/1.0.0/getBsIPDOH?PID={0}&VisitCode={1}", pid, visit_code);
            var response = RequestAPI(url_postfix, "Entries", "Entry");
            if (response == null)
                return null;
            var TaiKhoanBS = GetDoctorUsernameFromRes(response);
            return TaiKhoanBS;
        }

        private static List<dynamic> BuilRelationshipCustomer(JToken relationshiips)
        {
            List<dynamic> result = new List<dynamic>();
            foreach (JToken item in relationshiips)
            {
                result.Add(new
                {
                    ContactName = item.Value<string>("ContactName"),
                    RelationshipCode = item.Value<string>("RelationshipCode"),
                    LastUpdated = DateTime.ParseExact(item.Value<string>("LastUpdated"), "yyyy-MM-dd HH:mm:ss", null),
                    ContactAddress = item.Value<string>("ContactAddress")?.Trim().Trim(','),
                    ContactPhoneNumber = item.Value<string>("ContactPhoneNumber"),
                    ContactGender = item.Value<string>("ContactGender"),
                    ContactDoB = item.Value<string>("ContactDoB"),
                    ContactOccupation = item.Value<string>("ContactOccupation"),
                    ContactOccupationType = item.Value<string>("ContactOccupationType"),
                    ContactOccupationStatus = item.Value<string>("ContactOccupationStatus"),
                    ContactType = item.Value<string>("ContactType"),
                    RelationshipDesc = item.Value<string>("RelationshipDesc")
                });
            }

            return result;
        }

        private static dynamic BuildVisitEDCodeResult(JToken visit_data)
        {
            List<dynamic> result = new List<dynamic>();
            var time = DateTime.Now.AddDays(-7);

            foreach (JToken data in visit_data)
            {
                var visit_code = data.Value<string>("VISIT_CODE");
                var ten_khoa = data.Value<string>("TEN_KHOA");
                var ma_loai_kcb = data.Value<string>("MA_LOAI_KCB");
                var ngay_vao = data.Value<string>("NGAY_VAO");

                var visit_type_code = data.Value<string>("VisitType");
                var visit_type = FormatVisitTypeCodeOH(visit_type_code);
                var ngay_ra = data.Value<string>("NGAY_RA");
                if (ngay_ra == "null") ngay_ra = null;
                if (!string.IsNullOrEmpty(ngay_ra))
                    continue;
                if (string.IsNullOrEmpty(ngay_vao) || string.IsNullOrEmpty(ma_loai_kcb) || string.IsNullOrEmpty(visit_code))
                    continue;

                var date = DateTime.ParseExact(ngay_vao, "MM/dd/yyyy HH:mm:ss", null);
                if (date < time)
                    continue;

                if ((!string.IsNullOrEmpty(ten_khoa) && ten_khoa.ToLower().Contains("cấp cứu")) || (!string.IsNullOrEmpty(visit_type_code) && (visit_type_code.Contains("ED") || visit_type_code.Contains("VMIPD"))))
                    result.Add(new
                    {
                        VisitCode = visit_code,
                        VisitTypeName = visit_type,
                        NgayVao = FormatDateVisitCode(ngay_vao),
                        TenKhoa = data.Value<string>("TEN_KHOA"),
                        MaKhoa = data.Value<string>("MA_KHOA"),
                        MaLoaiKCB = ma_loai_kcb,
                        BenhVien = data.Value<string>("BenhVien"),
                        BacSi = data.Value<string>("BacSi"),
                        BacSiAD = data.Value<string>("BacSiAD")
                    });
            }
            return result;
        }
        private static dynamic BuildVisitOPDCodeResult(JToken visit_data)
        {
            List<dynamic> result = new List<dynamic>();
            var now = DateTime.Now;
            List<string> visit_opd_code = GetAppConfig("OPD_VISIT_TYPE_CODE").ToString().Split(',').ToList();
            List<string> visit_special_code = GetAppConfig("SPECIAL_VISIT_TYPE_CODE").Split(',').ToList(); // "VMHC,VMIPD"
            foreach (JToken data in visit_data)
            {
                var visit_code = data.Value<string>("VISIT_CODE");
                var ma_loai_kcb = data.Value<string>("MA_LOAI_KCB");
                var ngay_vao = data.Value<string>("NGAY_VAO");
                var visit_type_code = data.Value<string>("VisitType");
                var visit_type = FormatVisitTypeCodeOH(visit_type_code);

                var ngay_ra = data.Value<string>("NGAY_RA");
                if (ngay_ra == "null") ngay_ra = null;
                if (!string.IsNullOrEmpty(ngay_ra))
                    continue;

                if (string.IsNullOrEmpty(ngay_vao) || string.IsNullOrEmpty(ma_loai_kcb) || string.IsNullOrEmpty(visit_code) || string.IsNullOrEmpty(visit_type_code))
                    continue;
                // "OPD,VMPK,KBTX,VMDC,PKIPD,VMHC"
                
                var date = DateTime.ParseExact(ngay_vao, "MM/dd/yyyy HH:mm:ss", null);

                visit_type_code = visit_type_code.Trim();
                // "VMHC"
                if (visit_special_code.Contains(visit_type_code) || (date.Date == now.Date && visit_opd_code.Contains(visit_type_code)))
                    result.Add(new
                    {
                        VisitCode = visit_code,
                        VisitTypeName = visit_type,
                        NgayVao = FormatDateVisitCode(ngay_vao),
                        TenKhoa = data.Value<string>("TEN_KHOA"),
                        MaKhoa = data.Value<string>("MA_KHOA"),
                        MaLoaiKCB = ma_loai_kcb,
                        BenhVien = data.Value<string>("BenhVien"),
                        BacSi = data.Value<string>("BacSi"),
                        VisitTypeCode = visit_type_code
                    });
            }
            return result;
        }
        private static dynamic BuildVisitIPDCodeResult(JToken visit_data)
        {
            List<dynamic> result = new List<dynamic>();
            var now = DateTime.Now;

            foreach (JToken data in visit_data)
            {
                var visit_code = data.Value<string>("VISIT_CODE");
                var ma_loai_kcb = data.Value<string>("MA_LOAI_KCB");
                var ngay_vao = data.Value<string>("NGAY_VAO");
                var visit_type_code = data.Value<string>("VisitType");
                var visit_type = FormatVisitTypeCodeOH(visit_type_code);

                var ngay_ra = data.Value<string>("NGAY_RA");
                if (ngay_ra == "null") ngay_ra = null;
                if (!string.IsNullOrEmpty(ngay_ra))
                    continue;

                if (string.IsNullOrEmpty(ngay_vao) || string.IsNullOrEmpty(ma_loai_kcb) || string.IsNullOrEmpty(visit_code))
                    continue;

                var date = DateTime.ParseExact(ngay_vao, "MM/dd/yyyy HH:mm:ss", null);
                if (!string.IsNullOrEmpty(visit_type_code) && (visit_type_code.Contains("IPD") || visit_type_code.Trim() == "VMEDO") && date.Year >= now.AddYears(-1).Year)
                    result.Add(new
                    {
                        VisitCode = visit_code,
                        VisitTypeName = visit_type,
                        NgayVao = FormatDateVisitCode(ngay_vao),
                        TenKhoa = data.Value<string>("TEN_KHOA"),
                        MaKhoa = data.Value<string>("MA_KHOA"),
                        MaLoaiKCB = ma_loai_kcb,
                        BenhVien = data.Value<string>("BenhVien"),
                        BacSi = data.Value<string>("BacSi"),
                    });
            }
            return result.Take(3).ToList();
        }
        #endregion

        #region Get Lab Results by PID
        public static dynamic GetLabResults(string pid, string code)
        {
            //if (string.IsNullOrEmpty(pid) || !IsPID(pid))
            //    return new List<dynamic>();

            //string url_postfix = $"/EMRVinmecCom/1.0.0/getLabResultByPID_{code}?PID={pid}";
            //var response = RequestAPI(url_postfix, "DSXetNghiem", "XetNghiem");
            //if (response != null)
            //    return BuildLabResultsByPID(JsonConvert.DeserializeObject<List<LabResultDataModel>>(response.ToString()));
            //return new List<dynamic>();
            return GetLabResultsV3(pid, code);
        }
        public static dynamic GetLabResultsV4(string pid, string code)
        {
            return GetLabResultsSurgrey(pid, code);
        }
        public static List<ResultModel> GetLabResultsV2(string pid, string code)
        {
            if (string.IsNullOrEmpty(pid) || !IsPID(pid))
                return new List<ResultModel>();

            //string url_postfix = $"/EMRVinmecCom/1.0.0/getLabResultByPIDV2_{code}?PID={pid}";
            string url_postfix = $"/LABCONN_EMR/1.0.0/getLabResultByPIDV2_{code}?PID={pid}";

            var response = RequestAPI(url_postfix, "DSXetNghiem", "XetNghiem");

            if (response != null)
                return JsonConvert.DeserializeObject<List<ResultModel>>(response.ToString());
            return new List<ResultModel>();
        }
        public static List<LabResultModelByPID> GetLabResultsV3(string pid, string code)
        {
            if (string.IsNullOrEmpty(pid) || !IsPID(pid))
                return new List<LabResultModelByPID>();

            //string url_postfix = $"/EMRVinmecCom/1.0.0/getLabResultByPID_{code}?PID={pid}";
            string url_postfix = $"/LABCONN_EMR/1.0.0/getLabResultByPID_{code}?PID={pid}";

            var response = RequestAPI(url_postfix, "DSXetNghiem", "XetNghiem");
            if (response != null)
                return BuildLabResultsByPIDV3(JsonConvert.DeserializeObject<List<LabResultDataModel>>(response.ToString()));
            return new List<LabResultModelByPID>();
        }
        //phần biên bản hội chẩn thông qua mổ ver4
        public static List<LabReultModel> GetLabResultsSurgrey(string pid, string code)
        {
            if (string.IsNullOrEmpty(pid) || !IsPID(pid))
                return new List<LabReultModel>();

            //string url_postfix = $"/EMRVinmecCom/1.0.0/getLabResultByPID_{code}?PID={pid}";
            string url_postfix = $"/LABCONN_EMR/1.0.0/getLabResultByPID_{code}?PID={pid}";

            var response = RequestAPI(url_postfix, "DSXetNghiem", "XetNghiem");
            var result = new List<LabReultModel>();
            if (response == null)
                return result;
            return new JavaScriptSerializer().Deserialize<List<LabReultModel>>(response.ToString());
        }
       
        private static List<LabResultModelByPID> BuildLabResultsByPIDV3(List<LabResultDataModel> specimen_datas)
        {


            int i = 0;
            foreach (var data in specimen_datas)
            {
                data.Index = i;
                i++;
            }
            List<LabResultDataModel> specimen_data = specimen_datas.OrderByDescending(e => e.UpdateTime).ToList();
            List<LabResultModelByPID> result = new List<LabResultModelByPID>();
            List<string> list_sid = new List<string>();
            foreach (var data in specimen_data)
            {
                string sid = data.SID.Trim();
                string test_name = data.TestNameE?.ToUpper();
                var date_time = data.UpdateTime;
                if (date_time == null)
                    continue;
                if (list_sid.Where(e => e.ToString() == sid).Count() > 0)
                {
                    continue;
                }
                list_sid.Add(sid);
                var date = data.UpdateTime?.Date;
                var date_str = date.Value.ToString(Constant.DATE_FORMAT);
                var date_item = result.FirstOrDefault(e => e.Date == date_str);
                if (date_item == null)
                {
                    date_item = new LabResultModelByPID()
                    {
                        Date = date_str,
                        RawDate = (DateTime)date,
                        Categories = new List<CategoriesLabResultModel>(),
                    };
                    result.Add(date_item);
                }
                string category_name = data.CategoryName?.ToUpper();
                var category_item = date_item.Categories.FirstOrDefault(e => e.Name == category_name);
                if (category_item == null)
                {
                    category_item = new CategoriesLabResultModel()
                    {
                        Name = category_name,
                        Profiles = new List<ProfileLabResult>(),
                    };
                    date_item.Categories.Add(category_item);
                }

                string profile_name = data.ProfileName?.ToLower();
                string profile_code = data.ProfileID?.ToLower();
                if (string.IsNullOrEmpty(profile_name))
                    profile_name = string.Empty;
                var profile_item = category_item.Profiles.FirstOrDefault(e => e.Name == profile_name);
                if (profile_item == null)
                {
                    profile_item = new ProfileLabResult()
                    {
                        Name = profile_name,
                        Code = profile_code,
                        Datas = new List<ItemLabResult>(),
                    };
                    category_item.Profiles.Add(profile_item);
                }
                category_item.Profiles.OrderBy(e => e.Code);
                List<ItemLabResult> datas = new List<ItemLabResult>();
                datas.Add(new ItemLabResult()
                {
                    SID = sid,
                    TestName = data.TestName,
                    TestCode = data.TestCode,
                    Result = data.Result,
                    LowerLimit = FormatLimitNumber(data.LowerLimit),
                    HigherLimit = FormatLimitNumber(data.HigherLimit),
                    NormalRange = data.NormalRange,
                    Status = FormatStatusLabResultByVisitCode(data),
                    Unit = data.Unit?.Trim(),
                    UpdateTime = data.UpdateTime?.ToString(Constant.TIME_DATE_FORMAT),
                    RawUpdateTime = (DateTime)data.UpdateTime,
                    Copyed = false,
                    Index = data.Index
                });
                var cc = FilterInSpecimenDataV3(specimen_datas, sid, profile_name, date_str);
                datas.AddRange(cc);
                profile_item.Datas = datas.GroupBy(e => e.ObjKey).Select(e => e.FirstOrDefault()).ToList().OrderBy(e => e.Index).ToList();
            }
            return result.OrderByDescending(e => e.RawDate).ToList();
        }
        private static List<LabResultModelByPID> SortByLab(List<LabResultModelByPID> results)
        {
            foreach (var labresult in results)
            {
                foreach (var cat in labresult.Categories)
                {
                    foreach (var prof in cat.Profiles)
                    {
                        var datas = prof.Datas.OrderBy(e => e.SID).GroupBy(e => e.SID).ToList();
                        var data_ordered = new List<ItemLabResult>();
                        foreach (var it in datas)
                        {
                            data_ordered.AddRange(it.OrderBy(e => e.RawUpdateTime).ToList());
                        }
                        prof.Datas = data_ordered.GroupBy(e => e.ObjKey).Select(e => e.FirstOrDefault()).ToList();
                    }
                }
            }
            return results;
        }
        private static List<LabResultModel> BuildLabResultsByPID(List<LabResultDataModel> specimen_datas)
        {
            List<LabResultDataModel> specimen_data = specimen_datas.OrderByDescending(e => e.UpdateTime).ToList();
            List<LabResultModel> result = new List<LabResultModel>();
            List<string> list_sid = new List<string>();
            foreach (var data in specimen_data)
            {
                string sid = data.SID.Trim();
                string test_name = data.TestNameE?.ToUpper();
                var date_time = data.UpdateTime;
                if (date_time == null)
                    continue;
                if (list_sid.Where(e => e.ToString() == sid).Count() > 0)
                {
                    continue;
                }
                list_sid.Add(sid);
                var date = data.UpdateTime?.Date;
                var date_str = date.Value.ToString(Constant.DATE_FORMAT);
                var date_item = result.FirstOrDefault(e => e.Date == date_str);
                if (date_item == null)
                {
                    date_item = new LabResultModel()
                    {
                        Date = date_str,
                        RawDate = (DateTime)date,
                        Categories = new List<CategoryLabResultModel>(),
                    };
                    result.Add(date_item);
                }
                string category_name = data.CategoryName?.ToUpper();
                var category_item = date_item.Categories.FirstOrDefault(e => e.Name == category_name);
                if (category_item == null)
                {
                    category_item = new CategoryLabResultModel()
                    {
                        Name = category_name,
                        Profiles = new List<ProfileLabResultModel>(),
                    };
                    date_item.Categories.Add(category_item);
                }

                string profile_name = data.ProfileName?.ToLower();
                string profile_code = data.ProfileID?.ToLower();
                if (string.IsNullOrEmpty(profile_name))
                    profile_name = string.Empty;
                var profile_item = category_item.Profiles.FirstOrDefault(e => e.Name == profile_name);
                if (profile_item == null)
                {
                    profile_item = new ProfileLabResultModel()
                    {
                        Name = profile_name,
                        Code = profile_code,
                        Datas = new List<dynamic>(),
                    };
                    category_item.Profiles.Add(profile_item);
                }
                category_item.Profiles.OrderBy(e => e.Code);
                List<dynamic> datas = new List<dynamic>();
                datas.Add(new
                {
                    SID = sid,
                    TestName = data.TestName,
                    TestCode = data.TestCode?.Trim(),
                    Result = data.Result,
                    LowerLimit = FormatLimitNumber(data.LowerLimit),
                    HigherLimit = FormatLimitNumber(data.HigherLimit),
                    NormalRange = data.NormalRange,
                    Status = FormatStatusLabResultByVisitCode(data),
                    Unit = data.Unit?.Trim(),
                    UpdateTime = data.UpdateTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                    Copyed = false
                });
                // TIME_DATE_FORMAT_WITHOUT_SECOND
                var cc = FilterInSpecimenData(specimen_datas, sid, profile_name, date_str);
                datas.AddRange(cc);
                datas = datas.Distinct().ToList();
                profile_item.Datas = datas.OrderBy(e => e.TestCode).ToList();
                //break;
            }
            return result.OrderByDescending(e => e.RawDate).ToList();
        }
        protected static int FormatStatusLabResultByVisitCode(LabResultDataModel rawData)
        {
            try
            {
                var result = Convert.ToDouble(rawData.Result);
                double low = 0;
                low = Convert.ToDouble(rawData.LowerLimit);
                double high = 0;
                high = Convert.ToDouble(rawData.HigherLimit);
                if (result < low)
                {
                    return 0;
                }
                else if (result > high)
                {
                    return 2;
                }
                return 1;
            }
            catch
            {
                return -1;
            }
        }
        private static List<ItemLabResult> FilterInSpecimenDataV3(List<LabResultDataModel> specimen_data, string sid, string profile_name, string date_str)
        {
            var datas = new List<ItemLabResult>();
            foreach (var data in specimen_data)
            {
                string sid_in_list = data.SID?.ToUpper();
                var date_time = data.UpdateTime;
                string profilename = data.ProfileName?.ToLower();

                if (date_time == null)
                    continue;
                var date = data.UpdateTime?.Date;
                var datestr = date.Value.ToString(Constant.DATE_FORMAT);

                if (sid.Trim() == sid_in_list.Trim() || (profilename == profile_name && datestr == date_str))
                {
                    datas.Add(new ItemLabResult()
                    {
                        SID = sid_in_list.Trim(),
                        TestName = data.TestName,
                        TestCode = data.TestCode,
                        Result = data.Result,
                        LowerLimit = FormatLimitNumber(data.LowerLimit),
                        HigherLimit = FormatLimitNumber(data.HigherLimit),
                        NormalRange = data.NormalRange,
                        Status = FormatStatusLabResultByVisitCode(data),
                        Unit = data.Unit?.Trim(),
                        UpdateTime = data.UpdateTime?.ToString(Constant.TIME_DATE_FORMAT),
                        RawUpdateTime = (DateTime)data.UpdateTime,
                        Copyed = false,
                        Index = data.Index
                    });
                }
            }
            return datas;
        }
        private static List<dynamic> FilterInSpecimenData(List<LabResultDataModel> specimen_data, string sid, string profile_name, string date_str)
        {
            var datas = new List<dynamic>();
            foreach (var data in specimen_data)
            {
                string sid_in_list = data.SID?.ToUpper();
                var date_time = data.UpdateTime;
                string profilename = data.ProfileName?.ToLower();

                if (date_time == null)
                    continue;
                var date = data.UpdateTime?.Date;
                var datestr = date.Value.ToString(Constant.DATE_FORMAT);

                if (sid.Trim() == sid_in_list.Trim() || (profilename == profile_name && datestr == date_str))
                {
                    datas.Add(new
                    {
                        SID = sid_in_list.Trim(),
                        TestName = data.TestName,
                        TestCode = data.TestCode?.Trim(),
                        Result = data.Result,
                        LowerLimit = FormatLimitNumber(data.LowerLimit),
                        HigherLimit = FormatLimitNumber(data.HigherLimit),
                        NormalRange = data.NormalRange,
                        Status = FormatStatusLabResultByVisitCode(data),
                        Unit = data.Unit?.Trim(),
                        UpdateTime = data.UpdateTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                        Copyed = false
                    });
                }
            }
            return datas;
        }
        #endregion

        #region Get Xray Results by PID
        private static string RequestXrayOHAPI(string url_postfix)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "e2d94890-8c94-3d9e-bb08-1baadf3c1917");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true };
                client.Timeout = TimeSpan.FromSeconds(int.Parse(ConfigurationManager.AppSettings["HIS_API_TIMEOUT"].ToString()));
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                string url = string.Format("{0}{1}", ConfigurationManager.AppSettings["HIS_API_SERVER_URL"], url_postfix);
                var log_uri = url;
                string raw_data = string.Empty;
                try
                {
                    var response = client.PostAsync(url, null);
                    raw_data = response.Result.Content.ReadAsStringAsync().Result;
                    var log_response = raw_data;
                    CustomLog.apigwlog.Info(new
                    {
                        URI = log_uri,
                        Response = log_response
                    });
                    return raw_data;
                }
                catch (Exception ex)
                {
                    var log_response = string.Format("{0}\n{1}", ex.ToString(), raw_data);
                    CustomLog.apigwlog.Error(new
                    {
                        URI = log_uri,
                        Response = log_response
                    });
                    return null;
                }
            }
        }
        public static List<XRayResultModel> GetXrayResultsByPID(string pid)
        {
            string url_postfix = string.Format("/EMRVinmecCom/1.0.0/getRISReportByPIDV2?PID={0}", pid);
            var response = RequestAPI(url_postfix, "DSChanDoan", "ChanDoan");
            if (response == null)
                return new List<XRayResultModel>();
            try
            {
                return BuildXrayReportByPIDV2(JsonConvert.DeserializeObject<List<RISReportByPIDModel>>(response.ToString()));
            }
            catch
            {
                return null;
            }
            
        }
        private static List<XRayResultModel> BuildXrayReportByPIDV2(List<RISReportByPIDModel> xray_data)
        {
            
            List<XRayResultModel> result = new List<XRayResultModel>();
            foreach (RISReportByPIDModel data in xray_data)
            {
                var date_time = data.ResultAt;
                if (date_time == null)
                    continue;
                var date = data.ResultAt.Value.Date;
                var date_str = date.ToString(Constant.DATE_FORMAT);
                var date_item = result.FirstOrDefault(e => e.Date == date_str);
                if (date_item == null)
                {
                    date_item = new XRayResultModel()
                    {
                        Id = Guid.NewGuid(),
                        Date = date_str,
                        RawDate = date,
                        Datas = new List<dynamic>(),
                    };
                    result.Add(date_item);
                }
                string ket_luan = "";
                var report_id = data.ReportId;
                date_item.Datas.Add(new
                {
                    TenDichVu = data.ItemNameV,
                    KetLuan = ket_luan,
                    MoTa = "",
                    ReportId = report_id,
                    LoadingData = false,
                    Copyed = false,
                    NguoiTraKQ = data?.ResultBy
                });
            }
            return result.OrderByDescending(e => e.RawDate).ToList();
        }
        private static dynamic BuildXrayReportByPID(JToken xray_data)
        {
            List<XRayResultModel> result = new List<XRayResultModel>();
            foreach (JToken data in xray_data)
            {
                var date_time = data.Value<string>("NgayThucHien");
                if (string.IsNullOrEmpty(date_time))
                    continue;
                var date = data.Value<DateTime>("NgayThucHien").Date;
                var date_str = date.ToString(Constant.DATE_FORMAT);
                var date_item = result.FirstOrDefault(e => e.Date == date_str);
                if (date_item == null)
                {
                    date_item = new XRayResultModel()
                    {
                        Id = Guid.NewGuid(),
                        Date = date_str,
                        RawDate = date,
                        Datas = new List<dynamic>(),
                    };
                    result.Add(date_item);
                }

                string ket_luan = "";
                var report_id = data.Value<string>("ReportId");
                date_item.Datas.Add(new
                {
                    TenDichVu = data.Value<string>("TenDichVu"),
                    KetLuan = ket_luan,
                    MoTa = "",
                    ReportId = report_id,
                    LoadingData = false,
                    Copyed = false
                });
            }
            return result.OrderByDescending(e => e.RawDate).ToList();
        }
        public static dynamic getRISReport(JToken report_ids)
        {
            var data = new List<dynamic>();
            foreach (JToken report_id in report_ids)
            {
                string MoTa = "";
                string ket_luan = "";
                string html = "";
                string url_postfix = string.Format("/EMRVinmecCom/1.0.0/getRISReportById?reportId={0}", report_id);
                var response = RequestAPI(url_postfix, "reports", "report");
                if (response != null)
                {
                    var Res = BuildXrayResultsByReport(response);
                    ket_luan = Res.ket_luan;
                    MoTa = Res.Mota;
                    html = Res.html;
                }
                data.Add(new
                {
                    report_id,
                    ket_luan,
                    MoTa,
                    html
                });
            }
            return data;
        }
        public static string TrimStart(string target, string trimString)
        {
            if (string.IsNullOrEmpty(trimString)) return target;

            string result = target;
            while (result.StartsWith(trimString))
            {
                var l = result.Length;
                result = result.Substring(trimString.Length);
            }

            return result;
        }
        private static RISResultModel BuildXrayResultsByReport(JToken response)
        {
            List<string> trimChars = new List<string>(){
                "KẾT LUẬN :",
                "KẾT LUẬN:",

                "Kết Luận:",
                "Kết Luận :",

                "Kết luận :",
                "Kết luận:",

                "kết luận :",
                "kết luận:",

                "KẾT LUẬN :",
                "KẾT LUẬN:"
            };
            foreach (JToken data in response)
            {
                string ket_luan = data.Value<string>("conclusion");
                string fulldata = data.Value<string>("fulldata");
                string htmlData = data.Value<string>("htmlData");
                var output = !string.IsNullOrEmpty(ket_luan) ? Regex.Replace(ket_luan, @"\s+", " ") : "";
                foreach (string trimChar in trimChars)
                    output = TrimStart(output, trimChar);
                output = TrimStart(output, " ");
                return new RISResultModel()
                {
                    ket_luan = output,
                    Mota = converFullData(fulldata),
                    html = htmlData
                };
            }
            return new RISResultModel()
            {
            };
        }
        private static string converFullData(string fulldata)
        {
            if (string.IsNullOrEmpty(fulldata)) return "";
            XDocument xDoc = XDocument.Parse(fulldata);
            List<string> res = new List<string>();
            foreach (XElement element in xDoc.Descendants().Where(x => x.Name.LocalName.Contains("Region")))
            {
                if (element.FirstAttribute.Value == "region3")
                {
                    IEnumerable<XElement> elemsLv1 = element.Elements();
                    if (elemsLv1.Count() > 1)
                    {
                        foreach (XElement item in elemsLv1)
                        {
                            if (string.IsNullOrEmpty(item.Value)) res.Add(item.Value);
                        }
                    }
                    else
                    {
                        res.Add(CrawlHtml(element.FirstNode?.ToString()));
                    }
                }
            }
            return string.Join("\n", res);
        }
        protected static string FormatStringCDHA(string rawtext)
        {
            string test = rawtext.Replace("&nbsp;&nbsp;", "\n");
            string test1 = rawtext.Replace("        ", "\n");
            return test1;
        }
        protected static string CrawlHtml(string htmlData)
        {
            if (string.IsNullOrEmpty(htmlData)) return "";
            string finalString = "";

            try
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(htmlData);

                //var listFontNodes = doc.DocumentNode.SelectNodes("//font[@xmlns='http://www.w3.org/TR/xhtml1/strict']").ToList();
                List<string> listChanDoan = new List<string>();
                var listPNodes = doc.DocumentNode.SelectNodes("//p").ToList();

                foreach (var pNode in listPNodes)
                {
                    string chanDoan = "";
                    if (pNode.InnerText.Contains("\\n") || pNode.InnerText.Contains("\\r"))
                    {
                        chanDoan = "- " + pNode.InnerText.Replace("\\n", "").Replace("\\r", "").Trim();
                    }
                    else
                    {
                        chanDoan = "- " + pNode.InnerText.Replace("\n", "").Replace("\r", "").Trim();
                    }
                    Regex trimmer = new Regex(@"\s\s+");
                    chanDoan = trimmer.Replace(chanDoan, " ");
                    listChanDoan.Add(chanDoan);
                }
                foreach (var item in listChanDoan)
                {
                    finalString += $"{item}\n";
                }
                return finalString;
            }
            catch
            {
                return htmlData;
            }
        }
        private static string BuildXrayResultsByReportResponse(JToken response)
        {
            List<string> trimChars = new List<string>(){
                "Kết Luận:",
                "Kết Luận :",

                "Kết luận :",
                "Kết luận:",

                "kết luận :",
                "kết luận:",

                "KẾT LUẬN :",
                "KẾT LUẬN:"
            };
            string ket_luan = "";
            foreach (JToken data in response)
            {
                ket_luan = data.Value<string>("conclusion");
            }
            var output = Regex.Replace(ket_luan, @"\s+", " ");
            foreach (string trimChar in trimChars)
                output = TrimStart(output, trimChar);
            output = TrimStart(output, " ");
            return output;//ket_luan.Replace("\u00A0", "");
        }
        #endregion

        #region Get HIS doctor
        public static List<dynamic> GetHISDoctor(string pid, string visit_code)
        {
            string url_postfix = string.Format("/EMRVinmecCom/1.0.0/getBacSiByVisit?PID={0}&VisitCode={1}", pid, visit_code);
            var response = RequestAPI(url_postfix, "DSBacSi", "BacSi");
            if (response != null)
            {
                return BuildHISDoctorResult(response);
            }
            return new List<dynamic>();
        }
        private static dynamic BuildHISDoctorResult(JToken doctor_data)
        {
            using (IUnitOfWork unitOfWork = new EfUnitOfWork())
            {
                List<dynamic> result = new List<dynamic>();
                foreach (JToken data in doctor_data)
                {
                    var username = data.Value<string>("UserAD")?.ToLower();
                    var user = unitOfWork.UserRepository.FirstOrDefault(e => !e.IsDeleted && !string.IsNullOrEmpty(e.Username) && e.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
                    result.Add(new
                    {
                        user?.Id,
                        Fullname = data.Value<string>("BacSi"),
                        Department = data.Value<string>("PhongBan"),
                        EHOSAccount = "",
                        IsBooked = FormatIsBooked(data.Value<string>("CoHen")),
                        BookingTime = FormatDateTimeLongToTimeDate(data.Value<DateTime?>("GioHen")),
                        Username = username,
                    });
                }
                return result;
            }
        }
        #endregion

        #region Get Significant Medication By PID & VisitCode
        public static dynamic GetSignificantMedications(string pid, string visit_code)
        {
            var results = new List<dynamic>();
            foreach (var vs in visit_code.Split(','))
            {
                string url_postfix = string.Format("/EMRVinmecCom/1.0.0/getYLenhByVisit?PID={0}&VisitCode={1}", pid, vs);
                var response = RequestAPI(url_postfix, "DanhSachThuoc", "DanhSach");
                if (response != null)
                {
                    foreach (var x in BuildSignificantMedications(response))
                    {
                        results.Add(x);
                    }
                }
            }
            return results;
        }
        private static dynamic BuildSignificantMedications(JToken medicine_data)
        {
            List<dynamic> result = new List<dynamic>();
            foreach (JToken data in medicine_data)
            {
                result.Add(new
                {
                    TenThuoc = data.Value<string>("TenThuoc"),
                    SoLuong = FormatQuality(data.Value<string>("SoLuong")),
                    DonViTinh = data.Value<string>("DonViTinh"),
                    LieuDung = FormatDosageOH(data.Value<string>("LieuDung")),
                    DuongDung = data.Value<string>("DuongDung"),
                    NgayDung = FormatDateVisitCode(data.Value<string>("NgayDung")),
                });
            }
            return result;
        }
        #endregion

        #region Get Diagnosis by PID and VisitCode
        public static dynamic GetDiagnosisAndICD(string pid, string visit_code, string ehos_acc)
        {
            if (string.IsNullOrEmpty(pid) || !IsPID(pid))
                return new List<dynamic>();

            foreach (var vs in visit_code.Split(','))
            {
                string url_postfix = string.Format("/EMRVinmecCom/1.0.0/getLichSuKhamByPID?PID={0}&VisitCode={1}", pid, vs);
                var response = RequestAPI(url_postfix, "DSLichSuKhamBenh", "LanKham");
                if (response != null)
                    return BuildDiagnosisAndICDResult(response, ehos_acc);
            }
            return new List<dynamic>();
        }
        private static dynamic BuildDiagnosisAndICDResult(JToken diagnosis_data, string ad_acc)
        {
            using (IUnitOfWork unitOfWork = new EfUnitOfWork())
            {
                foreach (var data in diagnosis_data)
                {
                    string[] ehos_acc_list = ad_acc.ToLower().Split(',');
                    var ehos_account = data.Value<string>("UsereHOS")?.ToLower();
                    if (!string.IsNullOrEmpty(ehos_account) && ehos_acc_list.Contains(ehos_account))
                    {
                        string pri_icd_str = data["ICD"]?.ToString();
                        string opt_icd_str = data["MaICDPhu"]?.ToString();

                        List<string> icd = new List<string>();
                        List<string> pri_icd = new List<string>();

                        List<string> opt_icd = new List<string>();

                        if (!string.IsNullOrEmpty(pri_icd_str))
                        {
                            pri_icd = pri_icd_str.Trim().Replace(" ", "").Split('/').ToList();
                            icd.AddRange(pri_icd);
                        }

                        if (!string.IsNullOrEmpty(opt_icd_str))
                        {
                            opt_icd = opt_icd_str.Trim().Replace(" ", "").Split('/').ToList();
                            icd.AddRange(opt_icd);
                        }

                        dynamic recommend = new List<dynamic>();
                        if (icd.Count > 0)
                            recommend = unitOfWork.MasterDataRepository.Find(
                                e => !e.IsDeleted &&
                                !string.IsNullOrEmpty(e.Code) &&
                                icd.Contains(e.Code))
                            .Select(e => new { e.ViName, e.EnName, e.Note });

                        return new
                        {
                            InitialDiagnosis = data.Value<string>("ChanDoanSoBo"),
                            Diagnosis = data.Value<string>("ChanDoan"),
                            PrimaryICD = FormatICD10(pri_icd),
                            OptionICD = FormatICD10(opt_icd),
                            Recommends = recommend
                        };
                    }
                }
                return new List<dynamic>();
            }
        }
        #endregion

        #region Get Lab Results by PID and VisitCode
        public static dynamic GetFinalLabResultsByPIDVisitCodeAndAPICode(string pid, string visit_code, string api_code)
        {
            var specimen = GetSpecimenNumberByPIDVisitCodeAndAPICode(pid, visit_code, api_code);
            return GetLabResultsByPIDSpecimenAndAPICode(pid, specimen, api_code);
        }
        private static List<dynamic> GetSpecimenNumberByPIDVisitCodeAndAPICode(string pid, string visit_code, string api_code)
        {
            var results = new List<dynamic>();
            foreach (var vs in visit_code.Split(','))
            {
                string url_postfix = string.Format("/EMRVinmecCom/1.0.0/getSpecimenByVisit?PID={0}&sotiepnhan={1}&hospital={2}", pid, vs, api_code);
                var response = RequestAPI(url_postfix, "DSSpecimen", "Specimen");
                if (response != null)
                {
                    foreach (var x in BuildSpecimenNumberResultByPIDVisitCodeAndAPICode(response))
                    {
                        results.Add(x);
                    }
                }
            }
            return results;
        }
        private static List<dynamic> BuildSpecimenNumberResultByPIDVisitCodeAndAPICode(JToken specimen_data)
        {
            List<dynamic> result = new List<dynamic>();
            foreach (JToken data in specimen_data)
            {
                result.Add(data.Value<string>("specimen_number"));
            }
            return result;
        }
        private static dynamic GetLabResultsByPIDSpecimenAndAPICode(string pid, List<dynamic> specimen, string api_code)
        {
            string specimen_param = string.Join(",", specimen);
            //string url_postfix = string.Format("/EMRVinmecCom/1.0.0/getLabResult_{2}?PID={0}&specimen_number={1}", pid, specimen_param, api_code);
            string url_postfix = string.Format("/LABCONN_EMR/1.0.0/getLabResult_{2}?PID={0}&specimen_number={1}", pid, specimen_param, api_code);
            var response = RequestAPI(url_postfix, "DSXetNghiem", "XetNghiem");
            if (response != null)
            {
                return BuildLabResultsByPIDSpecimenAndAPICode(response);
            }
            return new List<dynamic>();
        }
        private static dynamic BuildLabResultsByPIDSpecimenAndAPICode(JToken specimen_data)
        {
            List<dynamic> result = new List<dynamic>();
            foreach (JToken data in specimen_data)
            {
                var UpdateTimeRaw = data.Value<string>("UpdateTime") != "" ? DateTime.Parse(data.Value<string>("UpdateTime")) : (DateTime?)null;
                if (UpdateTimeRaw == null) continue;
                result.Add(new
                {
                    TestName = data.Value<string>("TestName")?.Trim(),
                    TestCode = data.Value<string>("TestCode")?.Trim(),
                    Result = data.Value<string>("Result"),
                    LowerLimit = FormatLimitNumber(data.Value<string>("LowerLimit")),
                    HigherLimit = FormatLimitNumber(data.Value<string>("HigherLimit")),
                    Status = FormatStatusLabResultByVisitCode(data),
                    Unit = data.Value<string>("Unit")?.Trim(),
                    UpdateTime = FormatDateVisitCode(data.Value<string>("UpdateTime")),
                    UpdateTimeRaw
                });
            }
            return result.OrderBy(e => e.UpdateTimeRaw).ToList();
        }
        #endregion

        #region Get Xray by PID and VisitCode
        public static dynamic GetFinalXrayResultsByPIDAndVisitCode(string pid, string visit_code)
        {
            List<RISResultModelV2> report_list = GetReportIDByPIDAndVisitCode(pid, visit_code);
            return GetXrayResultsByReportV2(report_list);
        }
        private static List<RISResultModelV2> GetReportIDByPIDAndVisitCode(string pid, string visit_code)
        {
            var results = new List<RISResultModelV2>();
            foreach (var vs in visit_code.Split(','))
            {
                string url_postfix = string.Format("/EMRVinmecCom/1.0.0/getRISReportByVisit?PID={0}&visit_code={1}", pid, vs);
                var response = RequestAPI(url_postfix, "RISReportList", "RISReport");
                if (response != null)
                {
                    foreach (var x in BuildReportIDByPIDAndVisitCode(response))
                    {
                        results.Add(x);
                    }
                }

            }
            return results.Distinct().ToList();
        }
        private static List<RISResultModelV2> BuildReportIDByPIDAndVisitCode(JToken xray_data)
        {
            List<RISResultModelV2> result = new List<RISResultModelV2>();
            foreach (JToken data in xray_data)
            {
                if (result.Find(e => e.ReportID == data.Value<string>("ReportID")) == null)
                {
                    result.Add(new RISResultModelV2()
                    {
                        ReportID = data.Value<string>("ReportID"),
                        TenDichVu = data.Value<string>("ReportType"),
                        KetLuan = "",
                        DoctorAD = data.Value<string>("DoctorAD"),
                        ReportDate = data.Value<string>("ReportDate")
                    });
                }
            }
            return result.Distinct().ToList();
        }
        private static dynamic GetXrayResultsByReportV2(List<RISResultModelV2> report_list)
        {
            List<dynamic> result = new List<dynamic>();
            foreach (var report in report_list)
            {
                string MoTa = "";
                string ket_luan = "";
                string html = "";
                string NguoiTraKQ = "";
                string reportdate = "";
                string url_postfix = string.Format("/EMRVinmecCom/1.0.0/getRISReportById?reportId={0}", report.ReportID);
                var response = RequestAPI(url_postfix, "reports", "report");
                if (response != null)
                {
                    var Res = BuildXrayResultsByReport(response);
                    ket_luan = Res.ket_luan;
                    MoTa = Res.Mota;
                    html = Res.html;
                    NguoiTraKQ = report.DoctorAD;
                    reportdate = report.ReportDate;
                }
                result.Add(new
                {
                    report.TenDichVu,
                    KetLuan = ket_luan,
                    MoTa,
                    html,
                    NguoiTraKQ,
                    reportdate
                });
            }
            return result;
        }
        private static dynamic GetXrayResultsByReport(List<dynamic> report_list)
        {
            List<dynamic> result = new List<dynamic>();
            foreach (var report in report_list)
            {
                //string url_postfix = string.Format("/VinmecSharedServices/1.0.0/OH_PRODUCTION/getRISReport?reportId={0}", report.ReportID);
                //var response = RequestXrayOHAPI(url_postfix);
                //var ket_luan = "";
                //if (response != null)
                //{
                //    ket_luan = BuildXrayResultsByReport(response);
                //}
                //result.Add(new
                //{
                //    TenDichVu = report.TenDichVu,
                //    MaDichVu = "ABCXYZ",
                //    KetLuan = ket_luan
                //});

                string MoTa = "";
                string ket_luan = "";
                string html = "";
                string url_postfix = string.Format("/EMRVinmecCom/1.0.0/getRISReportById?reportId={0}", report.ReportID);
                var response = RequestAPI(url_postfix, "reports", "report");
                if (response != null)
                {
                    var Res = BuildXrayResultsByReport(response);
                    ket_luan = Res.ket_luan;
                    MoTa = Res.Mota;
                    html = Res.html;
                }
                result.Add(new
                {
                    report.TenDichVu,
                    KetLuan = ket_luan,
                    MoTa,
                    html
                });
            }
            return result;
        }
        private static dynamic BuildXrayResultsByReport(string xray_data)
        {
            Match match = Regex.Match(xray_data, @"<jsonObject><data>(.*)</data></jsonObject>");
            if (match.Success)
            {
                xray_data = match.Groups[1].Value.Replace("&lt;", "<");
                return Regex.Replace(xray_data, "<.*?>", string.Empty);
            }

            match = Regex.Match(xray_data, @"<jsonObject><Data>(.*)</Data></jsonObject>");
            if (match.Success)
            {
                xray_data = match.Groups[1].Value.Replace("&lt;", "<");
                return Regex.Replace(xray_data, "<.*?>", string.Empty);
            }
            return "";
        }
        #endregion

        #region Get Visit History by PID
        public static List<VisitModel> GetVisitHistory(string pid, DateTime admitted_date)
        {
            if (string.IsNullOrEmpty(pid) || !IsPID(pid))
                return new List<VisitModel>();

            string url_postfix = $"/EMRVinmecCom/1.0.0/getLichSuKhamByPID?PID={pid}";
            var response = RequestAPI(url_postfix, "DSLichSuKhamBenh", "LanKham");
            if (response != null)
                return BuildVisitResults(response, admitted_date);
            return new List<VisitModel>();
        }
        private static List<VisitModel> BuildVisitResults(JToken visit_data, DateTime admitted_date)
        {
            List<VisitModel> result = new List<VisitModel>();
            foreach (JToken data in visit_data)
            {
                var user_ehos = data.Value<string>("UsereHOS");
                if (!string.IsNullOrEmpty(user_ehos))
                    user_ehos = user_ehos.ToLower();

                var examination_time = data.Value<DateTime>("NgayKham");
                if (examination_time > admitted_date) continue;

                result.Add(new VisitModel()
                {
                    ExaminationTime = examination_time,
                    Fullname = data.Value<string>("TenBacSiKham"),
                    Assessment = data.Value<string>("NoiDungKham"),
                    ViName = data.Value<string>("TenPhongBan"),
                    EnName = data.Value<string>("TenPhongBan"),
                    PastMedicalHistory = data.Value<string>("TienSuBenh"),
                    FamilyMedicalHistory = "",
                    HistoryOfAllergies = data.Value<string>("TienSuDiUng"),
                    HistoryOfPresentIllness = data.Value<string>("BenhSu"),
                    Tests = data.Value<string>("XNThamDoChinh"),
                    Diagnosis = data.Value<string>("ChanDoan"),
                    ClinicalSymptoms = data.Value<string>("TrieuChungLamSang"),
                    ICD = data.Value<string>("ICD"),
                    ICDName = data.Value<string>("TenICD"),
                    VisitCode = data.Value<string>("VisitCode"),
                    Username = user_ehos,
                    EHOSVisitCode = $"{user_ehos}{data.Value<string>("VisitCode")}",
                    Type = "OH",
                    ChiefComplain = data.Value<string>("LyDoKham"),
                    InitialDiagnosis = data.Value<string>("ChanDoanSoBo"),
                    TreatmentPlans = data.Value<string>("HuongDieuTri"),
                    DoctorRecommendations = data.Value<string>("LoiDanVaTheoDoi"),
                    ICDOption = data.Value<string>("MaICDPhu")
                });
            }
            return result.ToList();
        }
        #endregion


        public static List<dynamic> GetVisitDetails(Guid patientVisitId)
        {
            string url_postfix = string.Format("/EMRVinmecCom/1.0.0/getVisitDetails?PatientVisitId={0}", patientVisitId);
            var response = RequestAPI(url_postfix, "VisitSyncs", "VisitSync");
            if (response != null)
            {
                return BuildVisitDetailsResult(response);
            }
            return new List<dynamic>();
        }
        

        
        internal static Guid? getServiceDepartmentId(Guid patientLocationId, Guid HISId)
        {
            string url_postfix = string.Format("/EMRVinmecCom/1.0.0/getServiceDepartment?PatientLocationId={0}&ServiceId={1}", patientLocationId, HISId.ToString());
            var response = RequestAPI(url_postfix, "Entries", "Entry");
            if (response != null)
            {
                foreach (JToken item in response)
                {
                    var ServiceDepartmentId = item.Value<string>("ServiceDepartmentId");
                    return new Guid(ServiceDepartmentId);
                }
            }
            return null;
        }

        

        public static List<HISServiceModel> getServicePrice(string service_codes, Guid patient_visit_id)
        {
            string url_postfix = string.Format(
                "/EMRVinmecCom/1.0.0/getServicePrice?ServiceCodes={0}&PatientVisitId={1}",
                service_codes,
                patient_visit_id
            );
            var response = RequestAPI(url_postfix, "Entries", "Entry");
            if (response != null)
            {
                var list_price_service = response.Select(e => new HISServiceModel
                {
                    ServiceCode = e["ItemCode"]?.ToString(),
                    ItemHospital = e["ItemHospital"]?.ToString(),
                    CostCentreId = e["CostCentreId"]?.ToString(),
                    GLAcc = e["GLAcc"]?.ToString(),
                    Price = TryFormatDecimal(e["Price"].ToString()),
                    EffectFrom = e["EffectFrom"]?.ToString() != "" ? DateTime.Parse(e["EffectFrom"]?.ToString()) : (DateTime?)null,
                    EffectTo = e["EffectTo"]?.ToString() != "" ? DateTime.Parse(e["EffectTo"]?.ToString()) : (DateTime?)null,
                    Type = e["Status"]?.ToString()
                }).ToList();
                var list_current_price = list_price_service.Where(e => e.Type == "CURRENT_PRICE").ToList();
                foreach (HISServiceModel data in list_current_price)
                {
                    HISServiceModel hasEffect = getEffectService(list_price_service, data.ServiceCode);
                    if (hasEffect != null)
                    {
                        try
                        {
                            data.PriceWarMsg = new
                            {
                                ViName = string.Format("Từ {0} giá dịch vụ là {1} VNĐ", hasEffect.EffectFrom?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND), hasEffect.Price?.ToString("#,##0")),
                                EnName = string.Format("From {0} the service price is {1} VNĐ", hasEffect.EffectFrom?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND), hasEffect.Price?.ToString("#,##0")),
                            };
                        }
                        catch (Exception ex)
                        {
                            var log_response = string.Format("#getServicePrice-{0}", ex.ToString());
                            CustomLog.apigwlog.Error(new
                            {
                                URI = url_postfix,
                                Response = log_response
                            });
                        }
                    }
                }

                return list_current_price;
            }

            return new List<HISServiceModel>();
        }

        private static HISServiceModel getEffectService(List<HISServiceModel> list_price_service, string service_code)
        {
            var now = DateTime.Now;
            var currebt = list_price_service.Where(e => e.ServiceCode == service_code && e.Type == "FUTURE_PRICE" && e.EffectFrom != null && e.EffectFrom > now).OrderBy(e => e.EffectFrom).FirstOrDefault();
            return currebt;
        }

        public static decimal? TryFormatDecimal(string price)
        {
            try
            {
                return Decimal.Parse(price);
            }
            catch
            {
                return null;
            }
        }
        public static List<HISServiceGroupModel> GetServiceGroup(DateTime from)
        {
            string url_postfix = string.Format(
                "/EMRVinmecCom/1.0.0/getServiceGroupList?TuNgay={0}",
                from.ToString(Constant.DATETIME_SQL)
            );
            var response = RequestAPI(url_postfix, "Entries", "Entry");
            if (response != null)
                return response.Select(e => new HISServiceGroupModel
                {
                    ParentServiceGroupId = e["ParentServiceGroupId"].ToString() != "" ? new Guid(e["ParentServiceGroupId"].ToString()) : (Guid?)null,
                    ServiceGroupId = new Guid(e["ServiceGroupId"]?.ToString()),
                    ServiceGroupCode = e["ServiceGroupCode"]?.ToString(),
                    ServiceGroupViName = e["ServiceGroupName"]?.ToString(),
                    ServiceGroupEnName = e["ServiceGroupNameE"]?.ToString(),
                    ServiceType = e["ServiceGroupType"]?.ToString(),
                    KeyStruct = e["KeyStruct"]?.ToString(),
                    HISLastUpdated = DateTime.Parse(e["LastUpdated"]?.ToString()),
                    IsActive = e["ActiveFlag"] != null ? Convert.ToBoolean(e["ActiveFlag"].ToString()) : false
                }).ToList();
            return new List<HISServiceGroupModel>();
        }
        public static List<HISServiceModel> GetService(DateTime from, DateTime to)
        {
            string url_postfix = string.Format(
                "/EMRVinmecCom/1.0.0/getServiceListOH?from={0}&to={1}",
                from.ToString(Constant.DATETIME_SQL),
                to.ToString(Constant.DATETIME_SQL)
            );
            var response = RequestAPI(url_postfix, "ServiceList", "Service");
            if (response != null)
                return response.Select(e => new HISServiceModel
                {
                    ParentServiceGroupId = e["ParentServiceGroupId"].ToString() != "" ? new Guid(e["ParentServiceGroupId"].ToString()) : (Guid?)null,
                    ServiceGroupId = new Guid(e["ServiceGroupId"]?.ToString()),
                    ServiceId = new Guid(e["ServiceId"]?.ToString()),
                    ServiceGroupCode = e["ServiceGroupCode"]?.ToString(),
                    ServiceGroupViName = e["ServiceGroupName"]?.ToString(),
                    ServiceGroupEnName = e["ServiceGroupNameE"]?.ToString(),
                    ServiceCode = e["ServiceCode"]?.ToString(),
                    ServiceViName = e["ServiceName"]?.ToString(),
                    ServiceEnName = e["ServiceNameE"]?.ToString(),
                    ServiceType = e["ServiceType"]?.ToString(),
                    HISLastUpdated = DateTime.Parse(e["LastUpdated"]?.ToString()),
                    HISCode = Constant.HIS_CODE["OH"],
                    IsActive = e["ActiveFlag"] != null ? Convert.ToBoolean(e["ActiveFlag"].ToString()) : false
                }).ToList();
            return new List<HISServiceModel>();
        }
        public static List<CpoeOrderable> GetCpoeOrderable(DateTime from)
        {
            string url_postfix = string.Format(
                "/EMRVinmecCom/1.0.0/getCpoeOrderable?From={0}",
                from.ToString(Constant.DATETIME_SQL)
            );
            var response = RequestAPI(url_postfix, "Entries", "Entry");
            if (response != null)
                return response.Select(e => new CpoeOrderable
                {
                    CpoeOrderableId = new Guid(e["CpoeOrderableId"]?.ToString()),
                    PhGenericDrugId = e["PhGenericDrugId"].ToString() != "" ? new Guid(e["PhGenericDrugId"].ToString()) : (Guid?)null,
                    ServiceCategoryRcd = e["ServiceCategoryRcd"]?.ToString(),
                    LabOrderableRid = e["LabOrderableRid"].ToString() != "" ? new Guid(e["LabOrderableRid"].ToString()) : (Guid?)null,
                    GenericOrderableServiceCodeRid = e["GenericOrderableServiceCodeRid"].ToString() != "" ? new Guid(e["GenericOrderableServiceCodeRid"].ToString()) : (Guid?)null,
                    RadiologyProcedurePlanRid = e["RadiologyProcedurePlanRid"].ToString() != "" ? new Guid(e["RadiologyProcedurePlanRid"].ToString()) : (Guid?)null,
                    PhPharmacyProductId = e["PhPharmacyProductId"].ToString() != "" ? new Guid(e["PhPharmacyProductId"].ToString()) : (Guid?)null,
                    PackageItemId = e["PackageItemId"].ToString() != "" ? new Guid(e["PackageItemId"].ToString()) : (Guid?)null,
                    CpoeOrderableTypeRcd = e["CpoeOrderableTypeRcd"]?.ToString(),
                    SeqNum = e["SeqNum"]?.ToString(),
                    Comments = e["Comments"]?.ToString(),
                    OverrideNameE = e["OverrideNameE"]?.ToString(),
                    OverrideNameL = e["OverrideNameL"]?.ToString(),
                    FillerNameE = e["FillerNameE"]?.ToString(),
                    FillerNameL = e["FillerNameL"]?.ToString(),
                    EffectiveFromDateTime = DateTime.Parse(e["EffectiveFromDateTime"]?.ToString()),
                    EffectiveToDateTime = e["EffectiveFromDateTime"]?.ToString() != "" ? DateTime.Parse(e["EffectiveFromDateTime"]?.ToString()) : (DateTime?)null,
                    LuUserId = e["LuUserId"].ToString() != "" ? new Guid(e["LuUserId"].ToString()) : (Guid?)null,
                    LuUpdated = DateTime.Parse(e["LuUpdated"]?.ToString()),
                }).ToList();
            return new List<CpoeOrderable>();
        }

        #region Lab Orderable Ref
        public static List<LabOrderableRef> GetLabOrderableRef(DateTime from)
        {
            string url_postfix = string.Format(
                "/EMRVinmecCom/1.0.0/getLabOrderableRef?From={0}",
                from.ToString(Constant.DATETIME_SQL)
            );
            var response = RequestAPI(url_postfix, "Entries", "Entry");
            if (response != null)
                return response.Select(e => new LabOrderableRef
                {
                    LabOrderableRid = new Guid(e["LabOrderableRid"]?.ToString()),
                    ItemId = e["ItemId"].ToString() != "" ? new Guid(e["ItemId"].ToString()) : (Guid?)null,
                    LabSpecialProcessingGroupRcd = e["LabSpecialProcessingGroupRcd"]?.ToString(),
                    LabOrderableCode = e["LabOrderableCode"]?.ToString(),
                    ServiceCategoryRcd = e["ServiceCategoryRcd"]?.ToString(),
                    NameE = e["NameE"]?.ToString(),
                    NameL = e["NameL"]?.ToString(),
                    SeqNum = e["SeqNum"]?.ToString(),
                    ActiveStatus = e["ActiveStatus"]?.ToString(),
                    LuUserId = e["LuUserId"].ToString() != "" ? new Guid(e["LuUserId"].ToString()) : (Guid?)null,
                    LuUpdated = DateTime.Parse(e["LuUpdated"]?.ToString())
                }).ToList();
            return new List<LabOrderableRef>();
        }
        #endregion

        #region Radiology Procedure
        public static List<RadiologyProcedurePlanRef> GetRadiologyProcedure(DateTime from)
        {
            string url_postfix = string.Format(
                "/EMRVinmecCom/1.0.0/getRadiologyProcedurePlan?From={0}",
                from.ToString(Constant.DATETIME_SQL)
            );
            var response = RequestAPI(url_postfix, "Entries", "Entry");
            if (response != null)
                return response.Select(e => new RadiologyProcedurePlanRef
                {
                    RadiologyProcedurePlanRid = new Guid(e["RadiologyProcedurePlanRid"]?.ToString()),
                    ShortCode = e["ShortCode"]?.ToString(),
                    RadiologyProcedureNameE = e["RadiologyProcedureNameE"]?.ToString(),
                    RadiologyProcedureNameL = e["RadiologyProcedureNameL"]?.ToString(),
                    ActiveStatus = e["ActiveStatus"]?.ToString(),
                    ServiceCategoryCode = e["ServiceCategoryCode"]?.ToString(),
                    ServiceCategoryNameL = e["ServiceCategoryNameL"]?.ToString(),
                    ServiceCategoryNameE = e["ServiceCategoryNameE"]?.ToString(),
                    DicomModality = e["DicomModality"]?.ToString(),
                    LuUpdated = DateTime.Parse(e["LuUpdated"]?.ToString())
                }).ToList();
            return new List<RadiologyProcedurePlanRef>();
        }
        #endregion
       
        

        private static dynamic BuildVisitDetailsResult(JToken visit_data)
        {
            List<dynamic> result = new List<dynamic>();

            foreach (JToken data in visit_data)
            {
                result.Add(new
                {
                    PatientId = data.Value<string>("PatientId"),
                    PatientVisitId = data.Value<string>("PatientVisitId"),
                    VisitCode = data.Value<string>("VisitCode"),
                    VisitType = data.Value<string>("VisitType"),
                    ActualVisitDate = data.Value<string>("ActualVisitDate"),
                    ClosureDate = data.Value<string>("ClosureDate")
                });
            }
            return result.ToList();
        }
        protected static dynamic FormatDosageOH(string rawData)
        {
            var result = "";
            if (!string.IsNullOrEmpty(rawData))
            {
                Match vi_match = Regex.Match(rawData, @"^(.*),");
                if (vi_match.Success)
                {
                    result = vi_match.Groups[1].Value;
                }
            }
            return result;
        }
        protected static dynamic FormatVisitTypeCodeOH(string rawData)
        {
            var result = "";
            if (!string.IsNullOrEmpty(rawData))
            {
                rawData = rawData.Trim();
                switch (rawData)
                {
                    case "VMED":
                        result = "Cấp cứu";
                        break;
                    case "VMEDO":
                        result = "Cấu cứu lưu";
                        break;
                    case "VMOPD":
                        result = "Ngoại trú";
                        break;
                    case "VMPK":
                        result = "Ngoại trú (Gói)";
                        break;
                    case "KBTX1":
                        result = "Ngoại trú (Telehealth)";
                        break;
                    case "VMIPD":
                        result = "Nội trú";
                        break;
                    default:
                        break;
                }
            }
            return result;
        }
        public static dynamic getProblemListOH(dynamic pid, string doctorname)
        {
            string url_postfix = string.Format("/EMRVinmecCom/1.0.0/getProblemListOH?Pid={0}{1}", pid, !string.IsNullOrEmpty(doctorname) ? string.Format("&DoctorAD={0}", doctorname) : "");
            var response = RequestAPI(url_postfix, "ArrayOfProblem", "Problem");
            if (response != null)
            {
                return response;
            }
            return new List<dynamic>();
        }
        public static dynamic getGetHeightAndWeight(dynamic pid)
        {
            string url_postfix = string.Format("/EMRVinmecCom/1.0.0/getHeightWeightOH?PID={0}", pid);
            var response = RequestAPI(url_postfix, "DSBenhNhan", "BenhNhan");
            if (response != null)
            {
                List<dynamic> result = new List<dynamic>();
                foreach (JToken data in response)
                {
                    result.Add(new
                    {
                        ChieuCao = StrToInt(data.Value<string>("ChieuCao")) * 100, // convert to cm
                        CanNang = StrToInt(data.Value<string>("CanNang")),
                        LastUpdated = FormatDateVisitCode(data.Value<string>("LastUpdated"))
                    });
                }
                return result;
            }
            return new List<dynamic>();
        }
        
        public static List<HisCustomer> searchPatientByPid(string pid)
        {
            var url_postfix = string.Format("/EMRVinmecCom/1.0.0/searchPatientByPID?PID={0}", pid);
            var response = RequestAPI(url_postfix, "Entries", "Entry");
            if (response != null)
                return BuildHisCustomerResult(response);
            return new List<HisCustomer>();
        }
        
    }

    

    public class SpecimenData
    {
        public string TestName { get; set; }
        public string Result { get; set; }
        public string LowerLimit { get; set; }
        public string HigherLimit { get; set; }
        public string NormalRange { get; set; }
        public int Status { get; set; }
        public string Unit { get; set; }
        public string UpdateTime { get; set; }
        public bool Copyed { get; set; }
    }

    public class RadiologyResult
    {
        public string CategoryCode { get; set; }
        public string ItemNameV { get; set; }
        public string ItemCode { get; set; }
        public string ResultBy { get; set; }
        public string SiteCode { get; set; }
        public string CategoryNameE { get; set; }
        public DateTime? ResultAt { get; set; }
        public string LocationNameV { get; set; }
        public string ReportId { get; set; }
        public string PID { get; set; }
        public string VisitCode { get; set; }
        public DateTime? ServiceDate { get; set; }
        public string CategoryNameV { get; set; }
        public string LocationNameE { get; set; }
        public string ItemNameE { get; set; }
        public string ServiceByAD { get; set; }         
    }
    
    public class AlliedService
    {
        public string ItemCode { get; set; }
        public Guid? ServiceGroupId { get; set; }
        public string ItemNameV { get; set; }       
        public DateTime? ServiceDate { get; set; }
        public string LocationNameE { get; set; }
        public string SiteCode { get; set; }
        public string LocationNameV { get; set; }
        public string PID { get; set; }
        public string VisitCode { get; set; }
        public string ItemNameE { get; set; }
        public string ServiceByAD { get; set; }
      
    }
}
