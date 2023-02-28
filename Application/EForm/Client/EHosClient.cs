using DataAccess.Repository;
using EForm.Common;
using EForm.Models;
using EForm.Utils;
using EMRModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
namespace EForm.Client
{
    public class EHosClient:HISClient
    {
        #region Search Customer By PID + Name

        //EMRVinmecCom/1.0.0/searchPatienteHos
        public static List<dynamic> searchPatienteHos(SearchParameter search_parameter)
        {
            string url_postfix = "";
            if (!string.IsNullOrEmpty(search_parameter.PID)) {
                url_postfix = string.Format("/EMRVinmecCom/1.0.0/searchPatienteHos?PID={0}", search_parameter.PID);
            } else if (!string.IsNullOrEmpty(search_parameter.TenBenhNhan)) {
               url_postfix = string.Format("/EMRVinmecCom/1.0.0/searchPatienteHos?TenBenhNhan={0}&NgaySinh={1}&GioiTinh={2}", search_parameter.TenBenhNhan, search_parameter.ConvertedNgaySinh, search_parameter.ConveredGioiTinh);
            }
            var response = RequestAPI(url_postfix, "Entries", "Entry");
            if (response != null)
                return BuildShortResultV2(response);
            return new List<dynamic>();
        }
        public static List<dynamic> SearchCustomerByPID(string pid)
        {
            if(string.IsNullOrEmpty(pid) || !IsPID(pid))
                return new List<dynamic>();

            string url_postfix = string.Format("/eHos_Production/1.0.0/searchPatientByPID?PID={0}", pid);
            var response = RequestAPI(url_postfix, "Entries", "Entry");
            if(response != null)
                return BuildShortResult(response);
            return new List<dynamic>();
        }
        public static List<dynamic> SearchCustomerByName(string name)
        {
            string url_postfix = string.Format("/eHos_Production/1.0.0/searchPatientByName?HO_TEN={0}", name);
            var response = RequestAPI(url_postfix, "Entries", "Entry");
            if (response != null)
                return BuildShortResult(response);
            return new List<dynamic>();
        }

        public static dynamic GetGetHeightAndWeight(dynamic pid)
        {
            string url_postfix = string.Format("/EMRVinmecCom/1.0.0/getHeightWeighteHos?PID={0}", pid);
            var response = RequestAPI(url_postfix, "DSBenhNhan", "BenhNhan");
            if (response != null)
            {
                List<dynamic> result = new List<dynamic>();
                foreach (JToken data in response)
                {
                    result.Add(new
                    {
                        ChieuCao = StrToInt(data.Value<string>("ChieuCao")),
                        CanNang = StrToInt(data.Value<string>("CanNang")),
                        LastUpdated = FormatDateVisitCode(data.Value<string>("LastUpdated"))
                    });
                }
                return result;
            }
            return new List<dynamic>();
        }
        
        private static List<dynamic> BuildShortResult(JToken customer_data)
        {
            List<dynamic> result = new List<dynamic>();
            foreach (JToken customer in customer_data)
            {
                result.Add(new
                {
                    Fullname = customer.Value<string>("TenBenhNhan"),
                    PID = customer.Value<string>("SoVaoVien"),
                    Address = customer.Value<string>("DiaChi"),
                    Phone = customer.Value<string>("SoDienThoai"),
                    Gender = FormatGender(customer.Value<string>("GioiTinh")),
                    Job = customer.Value<string>("NgheNghiep"),
                    WorkPlace = customer.Value<string>("TenCongTy"),
                    Relationship = customer.Value<string>("HoTenNguoiNha"),
                    RelationshipContact = customer.Value<string>("SoDienThoaiNguoiNha"),
                    DateOfBirth = FormatDOBLong(customer.Value<DateTime>("NgaySinh")),
                    IdentificationCard = customer.Value<string>("CMND"),
                    HealthInsuranceNumber = customer.Value<string>("TheBHYT"),
                });
            }
            return result;
        }
        #endregion

        #region Search Customer By Name Gender And DOB
        public static List<dynamic> SearchCustomerByNameGenderAndDOB(string name, int gender, string dob)
        {
            DateTime date = DateTime.ParseExact(dob, Constant.DATE_FORMAT, null);
            string date_of_birth = date.ToString("yyyy-MM-dd");
            string gender_param = "";
            if(gender == 0)
                gender_param = "F";
            else if(gender == 1)
                gender_param = "M";
            string url_postfix = string.Format("/eHos_Production/1.0.0/searchPatient_v2?name={0}&dob={1}&sex={2}", name, date_of_birth, gender_param);
            var response = RequestAPI(url_postfix, "DSBenhNhan", "BenhNhan");
            if (response != null)
                return BuildSearchCustomerResult(response);
            return new List<dynamic>();
        }
        private static List<dynamic> BuildSearchCustomerResult(JToken customer_data)
        {
            List<dynamic> result = new List<dynamic>();
            foreach (JToken customer in customer_data)
            {
                result.Add(new
                {
                    Fullname = customer.Value<string>("TenBenhNhan"),
                    PID = customer.Value<string>("PID"),
                    Address = customer.Value<string>("DiaChi"),
                    Phone = customer.Value<string>("SoDienThoai"),
                    Gender = FormatGender(customer.Value<string>("GioiTinh")),
                    Job = customer.Value<string>("NgheNghiep"),
                    Email = customer.Value<string>("Email"),
                    DateOfBirth = FormatDOBShort(customer.Value<string>("NgaySinh")),
                    Nationality = customer.Value<string>("QuocTich"),
                    WorkPlace = customer.Value<string>("TenCongTy"),
                    Relationship = customer.Value<string>("HoTenNguoiNha"),
                    RelationshipContact = customer.Value<string>("SoDienThoaiNguoiNha"),
                });
            }
            return result;
        }
        #endregion

        #region Get VisitCode
        public static dynamic GetVisitCode(string pid, string visit_type_group = "ED")
        {
            string url_postfix = string.Format("/eHos_Production/1.0.0/patientVisitList_v3?PID={0}&pagenum=1&pagesize=100", pid);
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
            string url_postfix = string.Format("/EMRVinmecCom/1.0.0/getBsIPDeHos?PID={0}&VisitCode={1}", pid, visit_code);
            var response = RequestAPI(url_postfix, "Entries", "Entry");
            if (response == null)
                return null;
            var TaiKhoanBS = GetDoctorUsernameFromRes(response);
            return TaiKhoanBS;
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
                var visit_type = FormatVisitType(ma_loai_kcb, visit_code);

                if (string.IsNullOrEmpty(ngay_vao) || string.IsNullOrEmpty(ma_loai_kcb) || string.IsNullOrEmpty(visit_code))
                    continue;

                var date = DateTime.ParseExact(ngay_vao, "MM/dd/yyyy HH:mm:ss", null);
                if (date < time)
                    continue;

                if (visit_code.Contains("/CC") || (!string.IsNullOrEmpty(visit_code) && ten_khoa.ToLower().Contains("cấp cứu")))
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
            return result;

            /* Chờ sửa API
            List<dynamic> result = new List<dynamic>();
            var time = DateTime.Now.AddDays(-7);

            foreach (JToken data in visit_data)
            {
                var visit_code = data.Value<string>("VISIT_CODE");
                var loai_kcb = data.Value<string>("LOAI_KCB");
                var ngay_vao = data.Value<string>("NGAY_VAO");

                if (string.IsNullOrEmpty(ngay_vao) || string.IsNullOrEmpty(loai_kcb) || string.IsNullOrEmpty(visit_code))
                    continue;

                var date = DateTime.ParseExact(ngay_vao, "MM/dd/yyyy HH:mm:ss", null);
                if (date < time)
                    continue;

                if (Constant.ML_KCB_CAP_CUU.Contains(loai_kcb))
                    result.Add(new
                    {
                        VisitCode = visit_code,
                        VisitTypeName = "Cấp cứu",
                        NgayVao = FormatDateVisitCode(ngay_vao),
                        TenKhoa = data.Value<string>("TEN_KHOA"),
                        MaKhoa = data.Value<string>("MA_KHOA"),
                        BenhVien = data.Value<string>("BenhVien"),
                        BacSi = data.Value<string>("BacSi"),
                    });
            }
            return result;
            */
        }
        private static dynamic BuildVisitOPDCodeResult(JToken visit_data)
        {
            List<dynamic> result = new List<dynamic>();
            var now = DateTime.Now;
            int cnt_ipd = 0;
            foreach (JToken data in visit_data)
            {
                var visit_code = data.Value<string>("VISIT_CODE");
                var ten_khoa = data.Value<string>("TEN_KHOA");
                var ma_loai_kcb = data.Value<string>("MA_LOAI_KCB");
                var ngay_vao = data.Value<string>("NGAY_VAO");
                var visit_type = FormatVisitType(ma_loai_kcb, visit_code);

                if (string.IsNullOrEmpty(ngay_vao) || string.IsNullOrEmpty(ma_loai_kcb) || string.IsNullOrEmpty(visit_code))
                    continue;

                var date = DateTime.ParseExact(ngay_vao, "MM/dd/yyyy HH:mm:ss", null);
                if (ma_loai_kcb == "1" && date.Date == now.Date)
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
                else if (ma_loai_kcb == "2" && !visit_code.Contains("/CC") && date.Year >= now.AddYears(-1).Year && cnt_ipd < 3)
                {
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
                    cnt_ipd += 1;
                }
            }
            return result;

            /* chờ API
            List<dynamic> result = new List<dynamic>();
            var now = DateTime.Now;
            int cnt_ipd = 0;
            int cnt_hc = 0;
            foreach (JToken data in visit_data)
            {
                var visit_code = data.Value<string>("VISIT_CODE");
                var loai_kcb = data.Value<string>("LOAI_KCB");
                var ngay_vao = data.Value<string>("NGAY_VAO");

                if (string.IsNullOrEmpty(ngay_vao) || string.IsNullOrEmpty(loai_kcb) || string.IsNullOrEmpty(visit_code))
                    continue;

                var date = DateTime.ParseExact(ngay_vao, "MM/dd/yyyy HH:mm:ss", null);
                if (Constant.ML_KCB_NGOAI_TRU.Contains(loai_kcb) && date.Date == now.Date)
                {
                    result.Add(new
                    {
                        VisitCode = visit_code,
                        VisitTypeName = "Ngoại trú",
                        NgayVao = FormatDateVisitCode(ngay_vao),
                        TenKhoa = data.Value<string>("TEN_KHOA"),
                        MaKhoa = data.Value<string>("MA_KHOA"),
                        BenhVien = data.Value<string>("BenhVien"),
                        BacSi = data.Value<string>("BacSi"),
                    });
                }
                else if (Constant.ML_KCB_NOI_TRU.Contains(loai_kcb) && date.Year >= now.AddYears(-1).Year && cnt_ipd < 3)
                {
                    result.Add(new
                    {
                        VisitCode = visit_code,
                        VisitTypeName = "Nội trú",
                        NgayVao = FormatDateVisitCode(ngay_vao),
                        TenKhoa = data.Value<string>("TEN_KHOA"),
                        MaKhoa = data.Value<string>("MA_KHOA"),
                        BenhVien = data.Value<string>("BenhVien"),
                        BacSi = data.Value<string>("BacSi"),
                    });
                    cnt_ipd += 1;
                }
                else if (Constant.ML_KCB_HC.Contains(loai_kcb) && date.Year >= now.AddYears(-1).Year && cnt_ipd < 1)
                {
                    result.Add(new
                    {
                        VisitCode = visit_code,
                        VisitTypeName = "Khám sức khỏe tổng quát",
                        NgayVao = FormatDateVisitCode(ngay_vao),
                        TenKhoa = data.Value<string>("TEN_KHOA"),
                        MaKhoa = data.Value<string>("MA_KHOA"),
                        BenhVien = data.Value<string>("BenhVien"),
                        BacSi = data.Value<string>("BacSi"),
                    });
                    cnt_hc += 1;
                }
            }
            return result;
            */
        }
        private static dynamic BuildVisitIPDCodeResult(JToken visit_data)
        {
            List<dynamic> result = new List<dynamic>();
            var now = DateTime.Now;

            foreach (JToken data in visit_data)
            {
                var visit_code = data.Value<string>("VISIT_CODE");
                var ten_khoa = data.Value<string>("TEN_KHOA");
                var ma_loai_kcb = data.Value<string>("MA_LOAI_KCB");
                var ngay_vao = data.Value<string>("NGAY_VAO");
                var visit_type = FormatVisitType(ma_loai_kcb, visit_code);

                if (string.IsNullOrEmpty(ngay_vao) || string.IsNullOrEmpty(ma_loai_kcb) || string.IsNullOrEmpty(visit_code))
                    continue;

                var date = DateTime.ParseExact(ngay_vao, "MM/dd/yyyy HH:mm:ss", null);
                if ((ma_loai_kcb == "2" && !visit_code.Contains("/CC") && date.Year >= now.AddYears(-1).Year))
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

            /* chờ API
            List<dynamic> result = new List<dynamic>();
            var now = DateTime.Now;

            foreach (JToken data in visit_data)
            {
                var visit_code = data.Value<string>("VISIT_CODE");
                var loai_kcb = data.Value<string>("MA_LOAI_KCB");
                var ngay_vao = data.Value<string>("NGAY_VAO");

                if (string.IsNullOrEmpty(ngay_vao) || string.IsNullOrEmpty(loai_kcb) || string.IsNullOrEmpty(visit_code))
                    continue;

                var date = DateTime.ParseExact(ngay_vao, "MM/dd/yyyy HH:mm:ss", null);
                if (Constant.ML_KCB_NOI_TRU.Contains(loai_kcb) && date.Year >= now.AddYears(-1).Year)
                    result.Add(new
                    {
                        VisitCode = visit_code,
                        VisitTypeName = "Nội trú",
                        NgayVao = FormatDateVisitCode(ngay_vao),
                        TenKhoa = data.Value<string>("TEN_KHOA"),
                        MaKhoa = data.Value<string>("MA_KHOA"),
                        BenhVien = data.Value<string>("BenhVien"),
                        BacSi = data.Value<string>("BacSi"),
                    });
            }
            return result.Take(3).ToList();
            */
        }
        #endregion

        #region Get Lab Results by PID
        public static dynamic GetLabResults(string pid)
        {
            if (string.IsNullOrEmpty(pid) || !IsPID(pid))
                return new List<dynamic>();

            string url_postfix = $"/eHos_Production/1.0.0/getLabResult_byPID?PID={pid}";
            var response = RequestAPI(url_postfix, "ListResult", "Result");
            if (response != null)
                return BuildLabResults(response);
            return new List<dynamic>();
        }
        private static dynamic BuildLabResults(dynamic specimen_data)
        {
            List<LabResultModel> result = new List<LabResultModel>();
            foreach (var data in specimen_data)
            {
                var date_time = data.Value<string>("updatetime");
                if (string.IsNullOrEmpty(date_time))
                    continue;
                var date = data.Value<DateTime>("updatetime").Date;
                var date_str = date.ToString(Constant.DATE_FORMAT);
                var date_item = result.FirstOrDefault(e => e.Date == date_str);
                if (date_item == null)
                {
                    date_item = new LabResultModel()
                    {
                        Date = date_str,
                        RawDate = date,
                        Categories = new List<CategoryLabResultModel>(),
                    };
                    result.Add(date_item);
                }

                string category_name = data.Value<string>("CategoryName")?.ToUpper();
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

                string profile_name = data.Value<string>("ProfileName")?.ToLower();
                if (string.IsNullOrEmpty(profile_name))
                    profile_name = string.Empty;
                var profile_item = category_item.Profiles.FirstOrDefault(e => e.Name == profile_name);
                if (profile_item == null)
                {
                    profile_item = new ProfileLabResultModel()
                    {
                        Name = profile_name,
                        Datas = new List<dynamic>(),
                    };
                    category_item.Profiles.Add(profile_item);
                }
                category_item.Profiles.OrderBy(e => e.Name);

                string profile_level = data.Value<string>("Profile");
                if (!string.IsNullOrEmpty(profile_level) && profile_level.Equals("0"))
                    profile_item.Datas.Add(new
                    {
                        TestName = data.Value<string>("TestName")?.Trim(),
                        Result = data.Value<string>("Result"),
                        LowerLimit = FormatLimitNumber(data.Value<string>("lowerlimit")),
                        HigherLimit = FormatLimitNumber(data.Value<string>("higherlimit")),
                        NormalRange = data.Value<string>("normalrange"),
                        Status = FormatStatusLabResult(data),
                        Unit = data.Value<string>("Unit")?.Trim(),
                        UpdateTime = FormatDateVisitCode(data.Value<string>("updatetime")),
                        Copyed = false
                    });
            }
            return result.OrderByDescending(e => e.RawDate).ToList();
        }
        #endregion

        #region Get Xray Results by PID
        public static dynamic GetXrayResults(string pid)
        {
            if (string.IsNullOrEmpty(pid) || !IsPID(pid))
                return new List<dynamic>();

            string url_postfix = $"/eHos_Production/1.0.0/getKetQua_CDHA_VBR?PID={pid}";
            var response = RequestAPI(url_postfix, "DSChanDoan", "ChanDoan");
            if (response != null)
                return BuildXrayResults(response);
            return new List<dynamic>();
        }
        private static dynamic BuildXrayResults(JToken xray_data)
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
                        Date = date_str,
                        RawDate = date,
                        Datas = new List<dynamic>(),
                        Id = Guid.NewGuid(),
                    };
                    result.Add(date_item);
                }
                date_item.Datas.Add(new
                {
                    TenDichVu = data.Value<string>("TenDichVu"),
                    KetLuan = RTFDecoder.Decode(data.Value<string>("KetLuan_Decode")),
                    MoTa = RTFDecoder.Decode(data.Value<string>("Mota_Decode")),
                    Copyed = false
                });
            }
            return result.OrderByDescending(e => e.RawDate).ToList();
        }
        #endregion

        #region Get HIS Doctor
        public static List<dynamic> GetHISDoctor(string pid, string visit_code)
        {
            if (string.IsNullOrEmpty(pid) || !IsPID(pid))
                return new List<dynamic>();

            var result = new List<dynamic>();
            foreach (var vs in visit_code.Split(','))
            {
                string url_postfix = string.Format("/eHos_Production/1.0.0/getBacSi_byVisit?PID={0}&visit_code={1}", pid, vs);
                var response = RequestAPI(url_postfix, "DSBacSi", "BacSi");
                if (response != null)
                    result.AddRange(BuildHISDoctorResult(response));
            }
            return result;
        }
        private static dynamic BuildHISDoctorResult(JToken doctor_data)
        {
            using (IUnitOfWork unitOfWork = new EfUnitOfWork()) { 

                List<dynamic> result = new List<dynamic>();
                foreach (JToken data in doctor_data)
                {
                    var ehos_account = data.Value<string>("MaBacSiThucHien")?.ToLower();
                    var user = unitOfWork.UserRepository.FirstOrDefault(
                        e => !e.IsDeleted &&
                        !string.IsNullOrEmpty(e.EHOSAccount) &&
                        e.EHOSAccount.ToLower().Contains(ehos_account.ToLower())
                    );
                    result.Add(new
                    {
                        user?.Id,
                        Fullname = data.Value<string>("BacSiThucHien"),
                        Department = data.Value<string>("PhongBanThucHien"),
                        EHOSAccount = ehos_account,
                        IsBooked = FormatIsBooked(data.Value<string>("CoHen")),
                        BookingTime = FormatDateTimeLongToTimeDate(data.Value<DateTime?>("GioHen")),
                        Username = "",
                    });
                }
                return result.Distinct().ToList();
            }
        }
        #endregion

        #region Get Significant Medications by PID and VisitCode
        public static dynamic GetSignificantMedications(string pid, string visit_code)
        {
            if (string.IsNullOrEmpty(pid) || !IsPID(pid))
                return new List<dynamic>();

            var results = new List<dynamic>();
            foreach (var vs in visit_code.Split(','))
            {
                string url_postfix = string.Format("/eHos_Production/1.0.0/getYLenh_byPID_VisitCode?PID={0}&VisitCode={1}", pid, vs);
                var response = RequestAPI(url_postfix, "DanhSachThuoc", "DanhSach");
                if (response != null)
                    foreach (var x in BuildSignificantMedications(response))
                        results.Add(x);
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
                    LieuDung = data.Value<string>("LieuDung"),
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
                string url_postfix = string.Format("/eHos_Production/1.0.0/getInfoKhamBenh?PID={0}&VisitCode={1}", pid, vs);
                var response = RequestAPI(url_postfix, "LSKhamBenh", "KhamBenh");
                if (response != null)
                    return BuildDiagnosisAndICDResult(response, ehos_acc);
            }
            return new List<dynamic>();
        }
        private static dynamic BuildDiagnosisAndICDResult(JToken diagnosis_data, string ehos_acc)
        {
            using (IUnitOfWork unitOfWork = new EfUnitOfWork())
            {
                foreach (var data in diagnosis_data)
                {
                    string[] ehos_acc_list = ehos_acc.ToLower().Split(',');
                    var ehos_account = data.Value<string>("UserBS")?.ToLower();
                    if (!string.IsNullOrEmpty(ehos_account) && ehos_acc_list.Contains(ehos_account))
                    {
                        var pri_icd = data.Value<string>("MaICD");
                        var opt_icd = data.Value<string>("MaICDPhu");

                        string icd = "";
                        if (!string.IsNullOrEmpty(pri_icd))
                            icd += pri_icd;
                        if (!string.IsNullOrEmpty(opt_icd))
                            icd += opt_icd;

                        dynamic recommend = new List<dynamic>();
                        if (!string.IsNullOrEmpty(icd))
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

        #region Get Diagnosis IPD by PID and VisitCode
        public static dynamic GetDiagnosisAndICDIPD(string pid, string visit_code)
        {
            if (string.IsNullOrEmpty(pid) || !IsPID(pid))
                return new List<dynamic>();

            foreach (var vs in visit_code.Split(','))
            {
                string url_postfix = string.Format("/eHos_Production/1.0.0/getInfoKhamBenhIPD?PID={0}&VisitCode={1}", pid, vs);
                var response = RequestAPI(url_postfix, "LSKhamBenh", "KhamBenh");
                if (response != null)
                    return BuildDiagnosisAndICDResultIPD(response);
            }
            return new List<dynamic>();
        }
        private static dynamic BuildDiagnosisAndICDResultIPD(JToken diagnosis_data)
        {
            foreach (var data in diagnosis_data)
            {
                return new
                {
                    InPrimaryDiagnosis = data.Value<string>("ChanDoanVaoVien"),
                    InPrimaryICD = FormatICD10(data.Value<string>("MaICDVaoVien")),
                    OutPrimaryDiagnosis = data.Value<string>("ChanDoanRaVien"),
                    OutPrimaryICD = FormatICD10(data.Value<string>("MaICDRaVien")),
                    OutOptionDiagnosis = data.Value<string>("Chandoanravienphu"),
                    OutOptionICD = FormatICD10(data.Value<string>("MaICDRaVienphu")),
                };
            }
            return new List<dynamic>();
        }
        #endregion

        #region Get Xray Results by PID and VisitCode
        public static dynamic GetXrayResultsByPIDAndVisitCode(string pid, string visit_code)
        {
            if (string.IsNullOrEmpty(pid) || !IsPID(pid))
                return new List<dynamic>();

            var results = new List<dynamic>();
            foreach (var vs in visit_code.Split(','))
            {
                string url_postfix = string.Format("/eHos_Production/1.0.0/getKetQua_XN_CDHA?PID={0}&sotiepnhan={1}", pid, vs);
                var response = RequestAPI(url_postfix, "DSDichVuSuDung", "DichVuSuDung");
                if (response != null)
                    foreach (var x in BuildXrayResultsByPIDAndVisitCode(response))
                        results.Add(x);
            }
            return results;
        }
        private static dynamic BuildXrayResultsByPIDAndVisitCode(JToken xray_data)
        {
            List<dynamic> result = new List<dynamic>();
            foreach (JToken data in xray_data)
            {
                result.Add(new
                {
                    TenDichVu = data.Value<string>("TenDichVu"),
                    KetLuan = RTFDecoder.Decode(data.Value<string>("KetLuan_Decode")),
                    MaDichVu = data.Value<string>("MaDichVu"),
                    MoTa = "",
                });
            }
            return result;
        }
        #endregion

        #region Get Lab Results by PID and VisitCode
        public static List<dynamic> GetSpecimenNumber(string pid, string visit_code)
        {
            if (string.IsNullOrEmpty(pid) || !IsPID(pid))
                return new List<dynamic>();

            var results = new List<dynamic>();
            foreach (var vs in visit_code.Split(','))
            {
                string url_postfix = string.Format("/eHos_Production/1.0.0/getSpecimenByVisit?PID={0}&sotiepnhan={1}", pid, vs);
                var response = RequestAPI(url_postfix, "DSSpecimen", "Specimen");
                if (response != null)
                    foreach (var x in BuildSpecimenNumberResult(response))
                        results.Add(x);
            }
            return results;
        }
        private static List<dynamic> BuildSpecimenNumberResult(JToken specimen_data)
        {
            List<dynamic> result = new List<dynamic>();
            foreach (JToken data in specimen_data)
            {
                result.Add(data.Value<string>("specimen_number"));
            }
            return result;
        }
        public static dynamic GetLabResultsByPIDAndSpecimen(string pid, List<dynamic> specimen)
        {
            string specimen_param = "&specimen_number=" + string.Join("&specimen_number=", specimen);
            string url_postfix = string.Format("/eHos_Production/1.0.0/getLabResult_HTC?PID={0}{1}", pid, specimen_param);
            var response = RequestAPI(url_postfix, "DSXetNghiem", "XetNghiem");
            if (response != null)
                return BuildLabResultsByPIDAndSpecimen(response);
            return new List<dynamic>();
        }
        private static dynamic BuildLabResultsByPIDAndSpecimen(JToken specimen_data)
        {
            List<dynamic> result = new List<dynamic>();
            foreach (JToken data in specimen_data)
            {
                result.Add(new
                {
                    TestName = data.Value<string>("TestName")?.Trim(),
                    TestCode = data.Value<string>("TestCode")?.Trim(),
                    Result = data.Value<string>("Result"),
                    LowerLimit = FormatLimitNumber(data.Value<string>("LowerLimit")),
                    HigherLimit = FormatLimitNumber(data.Value<string>("HigherLimit")),
                    NormalRange = data.Value<string>("NormalRange"),
                    Status = FormatStatusLabResult(data),
                    Unit = data.Value<string>("Unit")?.Trim(),
                    UpdateTime = FormatDateVisitCode(data.Value<string>("UpdateTime")),
                });
            }
            return result;
        }
        public static dynamic GetFinalLabResultsByPIDAndVisitCode(string pid, string visit_code)
        {
            var specimen = GetSpecimenNumber(pid, visit_code);
            return GetLabResultsByPIDAndSpecimen(pid, specimen);
        }
        #endregion

        #region Get Visit History by PID
        public static List<VisitModel> GetVisitHistory(string pid, DateTime admitted_date)
        {
            if (string.IsNullOrEmpty(pid) || !IsPID(pid))
                return new List<VisitModel>();

            string url_postfix = $"/eHos_Production/1.0.0/getLichSuKham_byPID?PID={pid}";
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
                    Type = "EHOS",
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

        private static string FormatVisitType(string rawData, string visit_code)
        {
            if (!string.IsNullOrEmpty(rawData))
            {
                if (rawData == "1")
                    return "Ngoại trú";
                else if (rawData == "2")
                {
                    if (visit_code.Contains("CC"))
                        return "Cấp cứu";
                    return "Nội trú";
                }
            }
            return string.Empty;
        }
    }

    public class SearchParameter
    {
        public bool? IsGetAgeByMonth { get; set; }
        public string PID { get; set; }
        public string TenBenhNhan { get; set; }
        public string NgaySinh { get; set; }
        public string GioiTinh { get; set; }

        public string ConvertedNgaySinh
        {
            get
            {
                if (string.IsNullOrEmpty(this.NgaySinh)) return "";
                DateTime date = DateTime.ParseExact(this.NgaySinh, Constant.DATE_FORMAT, null);
                string date_of_birth = date.ToString("yyyy-MM-dd");
                return date_of_birth;
            }
        }
        public string ConveredGioiTinh
        {
            get
            {
                return "";
                //if (this.GioiTinh == "0")
                //    return "F";
                //else if (this.GioiTinh == "1")
                //    return  "M";
                //return "";
            }
        }
    }
}
 