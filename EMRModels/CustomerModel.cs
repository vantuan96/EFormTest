using Common;
using System;
using System.Collections.Generic;

namespace EMRModels
{
    public class CustomerViewModel
    {
        public string Fullname { get; set; }
        public string PID { get; set; }
        public string Gender { get; set; }
        public string DateOfBirth { get; set; }
        public string EthnicGroup { get; set; }
        public string Profession { get; set; }
    }

    public class CustomerRequetDetailModel
    {
        public bool? readonlyview { get; set; }
    }
    public class CustomerModel
    {
        public Guid Id { get; set; }
        public string ExaminationTime { get; set; }
        public string VisitCode { get; set; }
        public string RecordCode { get; set; }
        public dynamic Status { get; set; }
        public string VisitTypeGroupCode { get; set; }
        public dynamic Specialty { get; set; }
        public dynamic Site { get; set; }
        public string PrimaryDoctor { get; set; }

        public bool IsAllergy { get; set; }
        public bool IsAllergyNone { get; set; }
        public string Allergy { get; set; }
        public string KindOfAllergy { get; set; }


        public string Pulse { get; set; }
        public string BP { get; set; }
        public string T { get; set; }
        public string SpO2 { get; set; }
        public string RR { get; set; }
        public string VitalSignsNote { get; set; }

        public string Weight { get; set; }
        public string Height { get; set; }
        public string Fullname { get; set; }
        public string PID { get; set; }
        public int? Gender { get; set; }
        public string DateOfBirth { get; set; }
        public string Diagnosis { get; set; }
        public string ICD10 { get; set; }
        public string ICDOptions{get;set;}

        public List<MasterDataValue> MasterDataValue { get; set; }
    }

    public class QueryCustomerModel
    {
        public Guid Id { get; set; }
        public DateTime? ExaminationTime { get; set; }
        public string VisitCode { get; set; }
        public string RecordCode { get; set; }
        public Guid? StatusId { get; set; }
        public dynamic Status { get; set; }
        public string VisitTypeGroupCode { get; set; }
        public Guid? SpecialtyId { get; set; }
        public dynamic Specialty { get; set; }
        public string PrimaryDoctor { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CustomerParameterModel : PagingParameterModel
    {
        public string Search { get; set; }
        public string ConvertedSearch
        {
            get
            {
                return this.Search.Trim().ToLower();
            }
        }
    }

    public class CustomerDetailParameterModel : PagingParameterModel
    {
        public string Status { get; set; }
        public string VisitCode { get; set; }
        public string RecordCode { get; set; }
        public string VisitTypeGroupCode { get; set; }
        public string Specialty { get; set; }
        public string StartExaminationTime { get; set; }
        public string EndExaminationTime { get; set; }

        public List<Guid?> ConvertedStatus
        {
            get
            {
                return ConvertGuid(this.Status);
            }
        }

        public string[] ConvertedVisitCode
        {
            get
            {
                return this.VisitCode.Split(',');
            }
        }
        public string[] ConvertedRecordCode
        {
            get
            {
                return this.RecordCode.Split(',');
            }
        }
        public string[] ConvertedVisitTypeGroupCode
        {
            get
            {
                return this.VisitTypeGroupCode.Split(',');
            }
        }

        public List<Guid?> ConvertedSpecialty
        {
            get
            {
                return ConvertGuid(this.Specialty);
            }
        }

        public DateTime? ConvertedStartExaminationTime
        {
            get
            {
                if (string.IsNullOrEmpty(this.StartExaminationTime))
                {
                    return null;
                }
                return DateTime.ParseExact(this.StartExaminationTime, Constants.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            }
        }
        public DateTime? ConvertedEndExaminationTime
        {
            get
            {
                if (string.IsNullOrEmpty(this.EndExaminationTime))
                {
                    return null;
                }
                return DateTime.ParseExact(this.EndExaminationTime, Constants.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            }
        }

        public bool Validate()
        {
            if (!string.IsNullOrEmpty(this.StartExaminationTime) && !Validators.ValidateTimeDateWithoutSecond(this.StartExaminationTime))
            {
                return false;
            }
            if (!string.IsNullOrEmpty(this.EndExaminationTime) && !Validators.ValidateTimeDateWithoutSecond(this.EndExaminationTime))
            {
                return false;
            }
            return true;
        }

        private dynamic ConvertGuid(string list_guid)
        {
            string[] id = list_guid.Split(',');
            List<Guid?> guid = new List<Guid?>();
            foreach (var i in id)
            {
                guid.Add(new Guid(i));
            }
            return guid;
        }
    }
    public class UpdateMedicalRecordStatus
    {
        public string Note { get; set; }
        public string VisitType { get; set; }
        public Guid? StatusId { get; set; }
    }
    public class DeleteMedicalRecord
    {
        public string Note { get; set; }
        public string VisitType { get; set; }
    }
    public class SetConsultationRecord
    {
        public bool? IsConsultation { get; set; } = false;
    }
    public class HisCustomer
    {
        public string PID { get; set; }

        public string Fullname { get; set; }
        public string SoNha { get; set; }
        public string Phone { get; set; }
        public string Fork { get; set; }
        public int Gender { get; set; }
        public string Address { get; set; }
        public bool IsVip { get; set; }
        public string Object { get; set; }
        public string District { get; set; }
        public string Province { get; set; }
        public string IssuePlace { get; set; }
        public string IssueDate { get; set; }
        public string Nationality { get; set; }
        public string Job { get; set; }
        public string WorkPlace { get; set; }
        public string Relationship { get; set; }
        public string RelationshipContact { get; set; }
        public string RelationshipAddress { get; set; }
        public string RelationshipKind { get; set; }
        public string RelationshipID { get; set; }
        public string HealthInsuranceNumber { get; set; }
        public string ExpireHealthInsuranceDate { get; set; }
        public string StartHealthInsuranceDate { get; set; }
        public string IdentificationCard { get; set; }
        public string DateOfBirth { get; set; }

        public DateTime? ConvertedDateOfBirth
        {
            get
            {
                if (string.IsNullOrEmpty(this.DateOfBirth))
                {
                    return null;
                }
                return DateTime.ParseExact(this.DateOfBirth, Constants.DATE_FORMAT, null);
            }
        }
        public dynamic ConvertedGender
        {
            get
            {
                if (this.Gender == -9)
                {
                    return null;
                }
                return this.Gender;
            }
        }
    }
    public class OhCustomer {
        public string SoBHYT { get; set; }
        public string PatientIndicatorName { get; set; }
        public string BHYTDenNgay { get; set; }
        public string TenBenhNhan { get; set; }
        public string DanToc { get; set; }
        public string NguoiNha_SoDienThoai { get; set; }
        public string NguoiNha_DiaChi { get; set; }
        public string SoNha { get; set; }
        public string NguoiNha_QuanHe { get; set; }
        public string GioiTinh { get; set; }
        public string PatientIndicatorType { get; set; }
        public string NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public string TinhThanh { get; set; }
        public string NgayHetHan_HoChieu { get; set; }
        public string PatientIndicatorNameE { get; set; }
        public string HoChieu { get; set; }
        public string QuanHuyen { get; set; }
        public string DoiTuong { get; set; }
        public string QuocTich { get; set; }
        public string SoVaoVien { get; set; }
        public string NoiLamViec { get; set; }
        public string NguoiNha_HoTen { get; set; }
        public string NgheNghiep { get; set; }
        public string NguoiNha_CMND { get; set; }
        public string CMND_NoiCap { get; set; }
        public string BHYTTuNgay { get; set; }
        public string CMND { get; set; }
        public string CMND_NgayCap { get; set; }
    }
}