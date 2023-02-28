using EForm.Common;
using System;

namespace EForm.Models
{
    public class CustomerEDParameterModel
    {
        public string PID { get; set; }
        public string Fullname { get; set; }
        public string DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Nationality { get; set; }
        public string Job { get; set; }
        public string WorkPlace { get; set; }
        public string Relationship { get; set; }
        public string RelationshipContact { get; set; }
        public int Gender { get; set; } = -9;
        public string IdentificationCard { get; set; }
        public string HealthInsuranceNumber { get; set; }
        public string StartHealthInsuranceDate { get; set; }
        public string ExpireHealthInsuranceDate { get; set; }
        public string Fork { get; set; }
        public string VisitCode { get; set; }
        public string MOHJob { get; set; }
        public string MOHJobCode { get; set; }
        public string MOHEthnic { get; set; }
        public string MOHEthnicCode { get; set; }
        public string MOHNationality { get; set; }
        public string MOHNationalityCode { get; set; }
        public string MOHProvince { get; set; }
        public string MOHProvinceCode { get; set; }
        public string MOHDistrict { get; set; }
        public string MOHDistrictCode { get; set; }
        public string MOHObject { get; set; }
        public string MOHObjectOther { get; set; }
        public bool IsVip { get; set; }
        public string IssueDate { get; set; }
        public string IssuePlace { get; set; }
        public dynamic ConvertedIssueDate
        {
            get
            {
                if (string.IsNullOrEmpty(this.IssueDate))
                {
                    return null;
                }
                return DateTime.ParseExact(this.IssueDate, Constant.DATE_FORMAT, null);
            }
        }
        public DateTime? ConvertedDateOfBirth
        {
            get
            {
                if (string.IsNullOrEmpty(this.DateOfBirth))
                {
                    return null;
                }
                return DateTime.ParseExact(this.DateOfBirth, Constant.DATE_FORMAT, null);
            }
        }
        public DateTime? ConvertedStartHealthInsuranceDate
        {
            get
            {
                if (string.IsNullOrEmpty(this.StartHealthInsuranceDate))
                {
                    return null;
                }
                return DateTime.ParseExact(this.StartHealthInsuranceDate, Constant.DATE_FORMAT, null);
            }
        }
        public DateTime? ConvertedExpireHealthInsuranceDate
        {
            get
            {
                if (string.IsNullOrEmpty(this.ExpireHealthInsuranceDate))
                {
                    return null;
                }
                return DateTime.ParseExact(this.ExpireHealthInsuranceDate, Constant.DATE_FORMAT, null);
            }
        }
        public dynamic ConvertedGender
        {
            get
            {
                if(this.Gender == -9)
                {
                    return null;
                }
                return this.Gender;
            }
        }

        public bool Validate()
        {
            if (!string.IsNullOrEmpty(this.Phone) && !Validator.ValidatePhone(this.Phone))
                return false;
            if (!string.IsNullOrEmpty(this.PID) && !Validator.ValidatePID (this.PID))
                return false;
            if (!string.IsNullOrEmpty(this.DateOfBirth) && !Validator.ValidateDate(this.DateOfBirth))
                return false;
            if (this.Gender != -9 && (this.Gender < 0 || this.Gender > 2))
                return false;
            return true;
        }

        public string VisitType { get; set; }
    }
}