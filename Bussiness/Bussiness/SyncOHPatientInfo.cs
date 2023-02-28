using Clients.HisClient;
using Common;
using DataAccess.Models;
using DataAccess.Repository;
using EMRModels;
using Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bussiness
{
    public class SyncOHPatientInfo
    {
        private static IUnitOfWork _unitOfWork;

        public SyncOHPatientInfo(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public static async Task<Customer> GetOHPatientInfoByPidAsync(string pid)
        {
            var response = await HisClient.SearchPatientByPID(pid);
            if (response != null)
                return BuildOhCustomerResult(JsonConvert.DeserializeObject<List<OhCustomer>>(response.ToString()).FirstOrDefault());
            return null;
        }
        private static Customer BuildOhCustomerResult(OhCustomer oh_customer)
        {
            var dob = FormatDOBShort(oh_customer.NgaySinh);
            var IdentificationCard = string.IsNullOrEmpty(oh_customer.CMND) ? oh_customer.HoChieu : oh_customer.CMND;
            var listAdd = new List<string> {
                    oh_customer.NguoiNha_QuanHe,
                    oh_customer.NguoiNha_HoTen,
                    oh_customer.NguoiNha_DiaChi
                }.Where(item => !string.IsNullOrEmpty(item));

            var Province = MapStringFromHis(oh_customer.TinhThanh, "GENLPCG");
            var District = MapDistrictFromHis(oh_customer.QuanHuyen, Province);
            var Ethnic = MapStringFromHis(oh_customer.DanToc, "GENETHN2");
            var Job = MapStringFromHis(oh_customer.NgheNghiep, "GENJOBB");

            return new Customer()
            {
                PID = oh_customer.SoVaoVien.Trim(),
                DateOfBirth = stringToDatetime(oh_customer.NgaySinh, "dd-MM-yyyy"),
                Fullname = oh_customer.TenBenhNhan.Trim(),
                Gender = FormatGender(oh_customer.GioiTinh),
                Address = oh_customer.DiaChi,
                Phone = oh_customer.SoDienThoai,
                Nationality = oh_customer.QuocTich,

                WorkPlace = oh_customer.NoiLamViec,

                Relationship = String.Join(" - ", listAdd.ToArray()),
                RelationshipContact = oh_customer.NguoiNha_SoDienThoai,
                RelationshipAddress = oh_customer.NguoiNha_DiaChi,
                RelationshipKind = oh_customer.NguoiNha_QuanHe,
                RelationshipID = oh_customer.NguoiNha_CMND,

                IdentificationCard = IdentificationCard,
                IssueDate = stringToDatetime(oh_customer.CMND_NgayCap),
                IssuePlace = oh_customer.CMND_NoiCap,

                MOHAddress = oh_customer.SoNha,

                MOHProvince = Province.ViName,
                MOHProvinceCode = Province.Note,

                MOHDistrict = District.ViName,
                MOHDistrictCode = District.Note,

                MOHEthnic = Ethnic?.ViName,
                MOHEthnicCode = Ethnic?.Note,
                Fork = Ethnic?.ViName,

                MOHJob = Job?.ViName,
                MOHJobCode = Job?.Note,
                Job = Job.ViName,
                //MOHObject = Job?.ViName,
                //MOHObjectOther = Job?.Note,

                //MOHNationality = Province.ViName,
                //MOHNationalityCode = Province.Note,

                MOHObject = !string.IsNullOrEmpty(oh_customer.SoBHYT) ? "1" : oh_customer.DoiTuong,
                IsVip = oh_customer.PatientIndicatorType == "VIP",

                // [NotMapped]
                HealthInsuranceNumber = oh_customer.SoBHYT,
                Province = oh_customer.TinhThanh,
                District = oh_customer.QuanHuyen,
                Object = oh_customer.DoiTuong,
            };
        }
        private static MasterDataValue MapDistrictFromHis(string district, MasterDataValue province)
        {
            var result = new MasterDataValue()
            {
                ViName = district,
                Code = "",
                Note = ""
            };

            if (string.IsNullOrWhiteSpace(district)) return result;

            if (province == null || string.IsNullOrWhiteSpace(province.Code)) return result;

            var listDistrict = _unitOfWork.MasterDataRepository.Find(s => s.Level == 2 && s.Form == "GENLPCG").ToList();
            var District = listDistrict.Where(s => s.Group == province.Code).FirstOrDefault(s => FormatUnicodeString(s.Data).Contains(FormatUnicodeString(district)));
            if (District == null) return result;

            result.ViName = District.ViName;
            result.Code = District.Code;
            result.Note = District.Note;

            return result;
        }
        private static DateTime? stringToDatetime(string str_date, string format)
        {
            try
            {
                return DateTime.ParseExact(str_date, format, null);
            }
            catch (Exception)
            {
                return null;
            }
        }
        private static DateTime? stringToDatetime(string str_date)
        {
            try
            {
                return DateTime.Parse(str_date);
            }
            catch (Exception)
            {
                return null;
            }
        }
        private static MasterDataValue MapStringFromHis(string str, string masterDataCode)
        {
            var result = new MasterDataValue()
            {
                ViName = str,
                Code = "",
                Note = ""
            };
            if (string.IsNullOrWhiteSpace(str)) return result;

            var list_data = _unitOfWork.MasterDataRepository.Find(s => s.Level == 1 && s.Form == masterDataCode).OrderByDescending(e => e.CreatedAt).ToList();
            var finded_value = list_data.FirstOrDefault(s => FormatUnicodeString(s.Data).Contains(FormatUnicodeString(str)));
            if (finded_value == null) return result;

            result.ViName = finded_value.ViName;
            result.Code = finded_value.Code;
            result.Note = finded_value.Note;

            return result;
        }
        private static string FormatUnicodeString(string s_input)
        {
            return StringHelper.FormatUnicodeString(s_input);
        }
        public async Task<Customer> SyncCustomerAsync(string pid)
        {
            var oh_customer = await GetOHPatientInfoByPidAsync(pid);
            var local_customer = _unitOfWork.CustomerRepository.Find(e => !e.IsDeleted && e.PID == pid).FirstOrDefault();
            var is_new_customer = local_customer == null;
            if (oh_customer != null)
            {
                if (is_new_customer)
                {
                    local_customer = new Customer();
                    local_customer.PID = pid;
                }
                local_customer.DateOfBirth = oh_customer.DateOfBirth;
                local_customer.Fullname = oh_customer.Fullname;
                local_customer.Gender = oh_customer.Gender;
                local_customer.Address = oh_customer.Address;
                local_customer.Phone = oh_customer.Phone;
                local_customer.WorkPlace = oh_customer.WorkPlace;
                local_customer.Relationship = oh_customer.Relationship;
                local_customer.RelationshipContact = oh_customer.RelationshipContact;
                local_customer.RelationshipAddress = oh_customer.RelationshipAddress;
                local_customer.RelationshipKind = oh_customer.RelationshipKind;
                local_customer.RelationshipID = oh_customer.RelationshipID;
                local_customer.IdentificationCard = oh_customer.IdentificationCard;
                local_customer.IssueDate = oh_customer.IssueDate;
                local_customer.IssuePlace = oh_customer.IssuePlace;
                local_customer.MOHAddress = oh_customer.MOHAddress;
                local_customer.MOHProvince = oh_customer.MOHProvince;
                local_customer.MOHProvinceCode = oh_customer.MOHProvinceCode;
                local_customer.MOHDistrict = oh_customer.MOHDistrict;
                local_customer.MOHDistrictCode = oh_customer.MOHDistrictCode;
                local_customer.MOHEthnic = oh_customer.MOHEthnic;
                local_customer.MOHEthnicCode = oh_customer.MOHEthnicCode;
                local_customer.Fork = oh_customer.Fork;
                local_customer.MOHJob = oh_customer.MOHJob;
                local_customer.MOHJobCode = oh_customer.MOHJobCode;
                local_customer.Job = oh_customer.Job;
                local_customer.MOHObject = oh_customer.MOHObject;
                local_customer.IsVip = oh_customer.IsVip;
                if (is_new_customer)
                {
                    _unitOfWork.CustomerRepository.Add(local_customer);
                }
                else
                {
                    _unitOfWork.CustomerRepository.Update(local_customer);
                }
                _unitOfWork.Commit();
            }

            return local_customer;
        }
        protected static string FormatDOBShort(string rawData)
        {
            if (!string.IsNullOrEmpty(rawData))
            {
                DateTime date = DateTime.ParseExact(rawData, "dd-MM-yyyy", null);
                return date.ToString(Constants.DATE_FORMAT);
            }
            return rawData;
        }
        protected static int FormatGender(string rawData)
        {
            foreach (string gender in Constants.MALE_SAMPLE)
            {
                if (rawData == gender)
                {
                    return 1;
                }
            }
            foreach (string gender in Constants.FEMALE_SAMPLE)
            {
                if (rawData == gender)
                {
                    return 0;
                }
            }
            return 2;
        }
    }
}
