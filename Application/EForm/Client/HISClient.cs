using Clients.HisClient;
using DataAccess.Models;
using DataAccess.Models.GeneralModel;
using DataAccess.Repository;
using EForm.Common;
using EForm.Models;
using EMRModels;
using Helper;
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
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;


namespace EForm.Client
{
    public class HISClient
    {
        // protected static IUnitOfWork unitOfWork = new EfUnitOfWork();
        public static JToken RequestAPI(string url_postfix, string json_collection, string json_item, int? timeout = null)
        {
            return ApiGWRequest.GetNoAsync(url_postfix, json_collection, json_item, timeout);
        }

        public static JToken RequestAsyncAPIDauHieuSinhTon(string url_postfix, string json_collection, string json_item, int? timeout = null)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ConfigurationManager.AppSettings["HIS_API_SERVER_TOKEN"]);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true };
                client.Timeout = timeout == null ? TimeSpan.FromSeconds(int.Parse(ConfigurationManager.AppSettings["HIS_API_TIMEOUT"].ToString())) : TimeSpan.FromSeconds((int)timeout);
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                string url = string.Format("{0}{1}", ConfigurationManager.AppSettings["HIS_API_SERVER_URL"], url_postfix);
                string raw_data = string.Empty;
                var start_time = DateTime.Now;
                var loger = new LogModel()
                {
                    URI = url,
                    StartAt = start_time
                };
                try
                {
                    var results = AsyncHelper.RunSync(() => client.GetAsync(url));

                    raw_data = results.Content.ReadAsStringAsync().Result;

                    JObject json_data = JObject.Parse(raw_data);
                    if (results.StatusCode != HttpStatusCode.OK)
                        HandleError(url, raw_data);
                    else
                        HandleSuccess(url);
                    var log_response = json_data.ToString();
                    loger.Response = log_response;
                    var end_time = DateTime.Now;
                    loger.EndAt = end_time;
                    loger.RequestTime = (end_time - start_time).ToString();
                    CustomLog.apigwlog.Info(JsonConvert.SerializeObject(loger));
                    return json_data;
                }
                catch (Exception ex)
                {
                    var log_response = string.Format("{0}\n{1}", ex.ToString(), raw_data);
                    HandleError(url, log_response);
                    var end_time = DateTime.Now;
                    loger.EndAt = end_time;
                    loger.Response = log_response;
                    loger.RequestTime = (end_time - start_time).ToString();
                    CustomLog.apigwlog.Info(JsonConvert.SerializeObject(loger));
                    return null;
                }
            }
        }
        public static string RequestPostAsyncAPI(string url, string auth_token, int? timeout = null)

        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ConfigurationManager.AppSettings["HIS_API_SERVER_TOKEN"]);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true };
                client.Timeout = timeout == null ? TimeSpan.FromSeconds(int.Parse(ConfigurationManager.AppSettings["HIS_API_TIMEOUT"].ToString())) : TimeSpan.FromSeconds((int)timeout);
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(url),
                };
                if (!string.IsNullOrWhiteSpace(auth_token))
                {
                    request.Headers.Add("Authorization", auth_token);
                }
                string raw_data = string.Empty;
                try
                {
                    var results = AsyncHelper.RunSync(() => client.SendAsync(request));
                    try
                    {
                        raw_data = results.Content.ReadAsStringAsync().Result;
                        return raw_data.ToString();
                    }
                    catch
                    {
                        return null;
                    }

                }
                catch (Exception ex)
                {
                    var log_response = string.Format("{0}\n{1}", ex.ToString(), raw_data);
                    return null;
                }
            }
        }
        
        protected static List<HisCustomer> BuildHisCustomerResult(JToken customer_data)
        {
            List<HisCustomer> result = new List<HisCustomer>();
            foreach (JToken customer in customer_data) 
            {
                var DateOfBirth = FormatDOBShort(customer.Value<string>("NgaySinh"));
                var IdentificationCard = string.IsNullOrEmpty(customer.Value<string>("CMND")) ? customer.Value<string>("HoChieu") : customer.Value<string>("CMND");
                var StartHealthInsuranceDate = FormatDOBShort(customer.Value<string>("BHYTTuNgay"));
                var ExpireHealthInsuranceDate = FormatDOBShort(customer.Value<string>("BHYTDenNgay"));
                var IssueDate = FormatDateIssueDate(customer.Value<string>("CMND_NgayCap"));
                var listAdd = new List<string> {
                    customer.Value<string>("NguoiNha_QuanHe"),
                    customer.Value<string>("NguoiNha_HoTen"),
                    customer.Value<string>("NguoiNha_DiaChi")
                }.Where(item => !string.IsNullOrEmpty(item));
                result.Add(new HisCustomer()
                {
                    Fullname = customer.Value<string>("TenBenhNhan"),
                    PID = customer.Value<string>("SoVaoVien"),
                    SoNha = customer.Value<string>("SoNha"),
                    Address = customer.Value<string>("DiaChi"),
                    Phone = customer.Value<string>("SoDienThoai"),
                    Fork = customer.Value<string>("DanToc"),
                    Gender = FormatGender(customer.Value<string>("GioiTinh")),
                    Job = customer.Value<string>("NgheNghiep"),
                    WorkPlace = customer.Value<string>("NoiLamViec"),
                    Relationship = String.Join(" - ", listAdd.ToArray()),
                    RelationshipContact = customer.Value<string>("NguoiNha_SoDienThoai"),
                    RelationshipAddress = customer.Value<string>("NguoiNha_DiaChi"),
                    RelationshipKind = customer.Value<string>("NguoiNha_QuanHe"),
                    RelationshipID = customer.Value<string>("NguoiNha_CMND"),
                    DateOfBirth = DateOfBirth,
                    IdentificationCard = IdentificationCard,
                    HealthInsuranceNumber = customer.Value<string>("SoBHYT"),
                    StartHealthInsuranceDate = StartHealthInsuranceDate,
                    ExpireHealthInsuranceDate = ExpireHealthInsuranceDate,
                    Nationality = customer.Value<string>("QuocTich"),
                    IssueDate = IssueDate,
                    IssuePlace = customer.Value<string>("CMND_NoiCap"),
                    Province = customer.Value<string>("TinhThanh"),
                    District = customer.Value<string>("QuanHuyen"),
                    Object = customer.Value<string>("DoiTuong"),
                    IsVip = customer.Value<string>("PatientIndicatorType") == "VIP"
                });
            }
            return result;
        }
        protected static List<dynamic> BuildShortResultV2(JToken customer_data)
        {
            List<dynamic> result = new List<dynamic>();
            foreach (JToken customer in customer_data)
            {
                var DateOfBirth = FormatDOBShort(customer.Value<string>("NgaySinh"));
                var IdentificationCard = string.IsNullOrEmpty(customer.Value<string>("CMND")) ? customer.Value<string>("HoChieu") : customer.Value<string>("CMND");
                var StartHealthInsuranceDate = FormatDOBShort(customer.Value<string>("BHYTTuNgay"));
                var ExpireHealthInsuranceDate = FormatDOBShort(customer.Value<string>("BHYTDenNgay"));
                var IssueDate = FormatDateIssueDate(customer.Value<string>("CMND_NgayCap"));
                var listAdd = new List<string> {
                    customer.Value<string>("NguoiNha_QuanHe"),
                    customer.Value<string>("NguoiNha_HoTen"),
                    customer.Value<string>("NguoiNha_DiaChi")
                }.Where(item => !string.IsNullOrEmpty(item));
                result.Add(new
                {
                    Fullname = customer.Value<string>("TenBenhNhan"),
                    PID = customer.Value<string>("SoVaoVien"),
                    SoNha = customer.Value<string>("SoNha"),
                    Address = customer.Value<string>("DiaChi"),
                    Phone = customer.Value<string>("SoDienThoai"),
                    Fork = customer.Value<string>("DanToc"),
                    Gender = FormatGender(customer.Value<string>("GioiTinh")),
                    Job = customer.Value<string>("NgheNghiep"),
                    WorkPlace = customer.Value<string>("NoiLamViec"),
                    Relationship = String.Join(" - ", listAdd.ToArray()),
                    RelationshipContact = customer.Value<string>("NguoiNha_SoDienThoai"),
                    RelationshipAddress = customer.Value<string>("NguoiNha_DiaChi"),
                    RelationshipKind = customer.Value<string>("NguoiNha_QuanHe"),
                    RelationshipID = customer.Value<string>("NguoiNha_CMND"),
                    DateOfBirth,
                    IdentificationCard,
                    HealthInsuranceNumber = customer.Value<string>("SoBHYT"),
                    StartHealthInsuranceDate,
                    ExpireHealthInsuranceDate,
                    Nationality = customer.Value<string>("QuocTich"),
                    IssueDate = IssueDate,
                    IssuePlace = customer.Value<string>("CMND_NoiCap"),
                    Province = customer.Value<string>("TinhThanh"),
                    District = customer.Value<string>("QuanHuyen"),
                    Object = customer.Value<string>("DoiTuong"),
                    IsVip = customer.Value<string>("PatientIndicatorType") == "VIP"

                });
            }
            return result;
        }
        protected static List<HisCustomer> BuildShortResultV3(JToken customer_data)
        {
            List<HisCustomer> result = new List<HisCustomer>();
            foreach (JToken customer in customer_data)
            {
                var DateOfBirth = FormatDOBShort(customer.Value<string>("NgaySinh"));
                var IdentificationCard = string.IsNullOrEmpty(customer.Value<string>("CMND")) ? customer.Value<string>("HoChieu") : customer.Value<string>("CMND");
                var StartHealthInsuranceDate = FormatDOBShort(customer.Value<string>("BHYTTuNgay"));
                var ExpireHealthInsuranceDate = FormatDOBShort(customer.Value<string>("BHYTDenNgay"));
                var IssueDate = FormatDateIssueDate(customer.Value<string>("CMND_NgayCap"));
                var listAdd = new List<string> {
                    customer.Value<string>("NguoiNha_QuanHe"),
                    customer.Value<string>("NguoiNha_HoTen"),
                    customer.Value<string>("NguoiNha_DiaChi")
                }.Where(item => !string.IsNullOrEmpty(item));
                result.Add(new HisCustomer()
                {
                    Fullname = customer.Value<string>("TenBenhNhan"),
                    PID = customer.Value<string>("SoVaoVien"),
                    SoNha = customer.Value<string>("SoNha"),
                    Address = customer.Value<string>("DiaChi"),
                    Phone = customer.Value<string>("SoDienThoai"),
                    Fork = customer.Value<string>("DanToc"),
                    Gender = FormatGender(customer.Value<string>("GioiTinh")),
                    Job = customer.Value<string>("NgheNghiep"),
                    WorkPlace = customer.Value<string>("NoiLamViec"),
                    Relationship = String.Join(" - ", listAdd.ToArray()),
                    RelationshipContact = customer.Value<string>("NguoiNha_SoDienThoai"),
                    RelationshipAddress = customer.Value<string>("NguoiNha_DiaChi"),
                    RelationshipKind = customer.Value<string>("NguoiNha_QuanHe"),
                    RelationshipID = customer.Value<string>("NguoiNha_CMND"),
                    DateOfBirth = DateOfBirth,
                    IdentificationCard = IdentificationCard,
                    HealthInsuranceNumber = customer.Value<string>("SoBHYT"),
                    StartHealthInsuranceDate = StartHealthInsuranceDate,
                    ExpireHealthInsuranceDate = ExpireHealthInsuranceDate,
                    Nationality = customer.Value<string>("QuocTich"),
                    IssueDate = IssueDate,
                    IssuePlace = customer.Value<string>("CMND_NoiCap"),
                    Province = customer.Value<string>("TinhThanh"),
                    District = customer.Value<string>("QuanHuyen"),
                    Object = customer.Value<string>("DoiTuong"),
                    IsVip = customer.Value<string>("PatientIndicatorType") == "VIP"
                });
            }
            return result;
        }
        protected static void HandleSuccess(string url)
        {
            //try
            //{
            //    var scope = url.Split('?')[0];
            //    var noti = GetNotification(scope);
            //    if (noti == null || noti.Status == 2) return;

            //    if (noti.Status == 0)
            //    {
            //        unitOfWork.SystemNotificationRepository.Delete(noti);
            //        unitOfWork.Commit();
            //        return;
            //    }

            //    noti.Status = 2;
            //    unitOfWork.SystemNotificationRepository.Update(noti);
            //    unitOfWork.Commit();
            //}
            //catch { }
        }
        public static string GetDoctorUsernameFromRes(JToken response)
        {
            foreach (JToken data in response)
            {
                return data.Value<string>("TaiKhoanBS");
            }
            return "";
        }
        protected static void HandleError(string url, string error)
        {
            HandleApiILog.Error(url, error);
        }

        protected static string FormatDOBShort(string rawData)
        {
            if (!string.IsNullOrEmpty(rawData))
            {
                DateTime date = DateTime.ParseExact(rawData, "dd-MM-yyyy", null);
                return date.ToString(Constant.DATE_FORMAT);
            }
            return rawData;
        }
        protected static string FormatDOBLong(string rawData)
        {
            if (!string.IsNullOrEmpty(rawData))
            {
                DateTime date = DateTime.ParseExact(rawData, "yyyy-MM-dd HH:mm:ss", null);
                return date.ToString(Constant.DATE_FORMAT);
            }
            return rawData;
        }
        protected static string FormatDOBLong(DateTime rawData)
        {
            if (rawData != null)
            {
                return rawData.ToString(Constant.DATE_FORMAT);
            }
            return "";
        }
        protected static string FormatDateTimeLongToDate(DateTime? rawData)
        {
            if (rawData != null)
            {
                return rawData?.ToString(Constant.DATE_FORMAT);
            }
            return "";
        }
        protected static string FormatDateTimeLongToTimeDate(DateTime? rawData)
        {
            if (rawData != null)
            {
                return rawData?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND);
            }
            return "";
        }
        protected static int FormatGender(string rawData)
        {
            foreach (string gender in Constant.MALE_SAMPLE)
            {
                if (rawData == gender)
                {
                    return 1;
                }
            }
            foreach (string gender in Constant.FEMALE_SAMPLE)
            {
                if (rawData == gender)
                {
                    return 0;
                }
            }
            return 2;
        }
        protected static int FormatQuality(string rawData)
        {
            double i = 0;
            if (rawData != null)
            {
                if (!Double.TryParse(rawData, out i))
                {
                    i = 0;
                }
            }
            return Convert.ToInt32(i);
        }
        public static Double? StrToInt(string str)
        {
            if (string.IsNullOrEmpty(str)) return null;
            return Double.Parse(str);
        }
        protected static string FormatDateVisitCode(string rawData)
        {
            if (!string.IsNullOrEmpty(rawData))
            {
                DateTime date = DateTime.ParseExact(rawData, "MM/dd/yyyy HH:mm:ss", null);
                return date.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND);
            }
            return rawData;
        }
        protected static string FormatDateIssueDate(string rawData)
        {
            if (!string.IsNullOrEmpty(rawData))
            {
                DateTime date = DateTime.ParseExact(rawData, "MM/dd/yyyy HH:mm:ss", null);
                return date.ToString(Constant.DATE_FORMAT);
            }
            return rawData;
        }
        protected static int FormatStatusLabResult(JToken rawData)
        {
            try
            {
                var result = rawData.Value<double>("Result");
                double low = 0;
                low = rawData.Value<double>("lowerlimit");
                double high = 0;
                high = rawData.Value<double>("higherlimit");
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
        protected static int FormatStatusLabResultByVisitCode(JToken rawData)
        {
            try
            {
                var result = rawData.Value<double>("Result");
                double low = 0;
                low = rawData.Value<double>("LowerLimit");
                double high = 0;
                high = rawData.Value<double>("HigherLimit");
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
        protected static string FormatLimitNumber(string rawData)
        {
            if (string.IsNullOrEmpty(rawData))
            {
                return "0";
            }
            return rawData;
        }

        protected static bool FormatIsBooked(string rawData)
        {
            if (!string.IsNullOrEmpty(rawData))
            {
                return "yes;co;có".Contains(rawData.Trim().ToLower());
            }
            return false;
        }

        protected static List<dynamic> FormatICD10(string rawData)
        {
            using (IUnitOfWork unitOfWork = new EfUnitOfWork()) {
                if (!string.IsNullOrEmpty(rawData))
                {
                    var icd = unitOfWork.ICD10Repository.FirstOrDefault(
                        e => !e.IsDeleted &&
                        !string.IsNullOrEmpty(e.Code) &&
                        e.Code == rawData
                    );
                    if (icd != null)
                    {
                        return new List<dynamic>() { new { code = icd.Code, label = $"{icd.Code}, {icd.ViName}" }, };
                    }
                }
                return new List<dynamic>() { new { code = rawData, label = "" }, };
            }
        }
        protected static dynamic FormatICD10(List<string> rawData)
        {
            using (IUnitOfWork unitOfWork = new EfUnitOfWork()) {
                if (rawData.Count > 0)
                {
                    return unitOfWork.ICD10Repository.Find(
                        e => !e.IsDeleted &&
                        !string.IsNullOrEmpty(e.Code) &&
                        rawData.Contains(e.Code)
                    ).Select(e => new { code = e.Code, label = $"{e.Code}, {e.ViName}" }).ToList();
                }
                return rawData.Select(e => new { code = e, label = "" });
            }
        }

        protected static bool IsPID(string input)
        {
            Regex regex = new Regex(@"^\d+$");
            Match match = regex.Match(input);
            return match.Success;
        }
        protected static string GetAppConfig(string key)
        {
            using (IUnitOfWork unitOfWork = new EfUnitOfWork())
            {
                var config_in_db = unitOfWork.AppConfigRepository.Find(e => !e.IsDeleted && e.Key == key).FirstOrDefault();
                var isTest = (config_in_db != null && !string.IsNullOrEmpty(config_in_db?.Value));
                var config = ConfigurationManager.AppSettings[key].ToString();
                return isTest ? config_in_db?.Value : config;
            }
        }
    }

    public class AsyncHelper
    {
        private static readonly TaskFactory _taskFactory = new
            TaskFactory(CancellationToken.None,
                        TaskCreationOptions.None,
                        TaskContinuationOptions.None,
                        TaskScheduler.Default);

        public static TReturn RunSync<TReturn>(Func<Task<TReturn>> task)
        {
            return _taskFactory.StartNew(task)
                                .Unwrap()
                                .GetAwaiter()
                                .GetResult();
        }
    }
}