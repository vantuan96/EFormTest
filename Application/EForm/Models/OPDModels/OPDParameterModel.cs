using EForm.Common;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace EForm.Models.OPDModels
{
    public class OPDParameterModel:PagingParameterModel
    {
        public string Search { get; set; }
        public string StartAdmittedDate { get; set; }
        public string EndAdmittedDate { get; set; }
        public string EmergencyStatus { get; set; }
        public string User { get; set; }
        public string Clinic { get; set; }
        public string ClinicId { get; set; }
        public bool? IsTelehealth { get; set; }
        public bool? IsRetailService { get; set; }
        public Guid? CustomerId { get; set; }
        public string ConvertedSearch
        {
            get
            {
                return this.Search.Trim().ToLower();
            }
        }
        public bool IsPid
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.Search)) return false;
                Regex regex = new Regex(@"^\d+$");
                Match match = regex.Match(this.Search.Trim().ToLower());
                return match.Success;
            }
        }
        public DateTime? ConvertedStartAdmittedDate
        {
            get
            {
                if (string.IsNullOrEmpty(this.StartAdmittedDate))
                {
                    return null;
                }
                return DateTime.ParseExact(this.StartAdmittedDate, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            }
        }
        public DateTime? ConvertedEndAdmittedDate
        {
            get
            {
                if (string.IsNullOrEmpty(this.EndAdmittedDate))
                {
                    return null;
                }
                return DateTime.ParseExact(this.EndAdmittedDate, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            }
        }
        public List<Guid?> ConvertedEmergencyStatus
        {
            get
            {
                string[] id = this.EmergencyStatus.Split(',');
                List<Guid?> guid = new List<Guid?>();
                foreach (var i in id)
                {
                    guid.Add(new Guid(i));
                }
                return guid;
            }
        }

        public List<Guid?> ConvertedUser
        {
            get
            {
                string[] id = this.User.Split(',');
                List<Guid?> guid = new List<Guid?>();
                foreach (var i in id)
                {
                    guid.Add(new Guid(i));
                }
                return guid;
            }
        }
        public List<Guid?> ConvertedClinic
        {
            get
            {
                string[] id = this.Clinic.Split(',');
                List<Guid?> guid = new List<Guid?>();
                foreach (var i in id)
                {
                    guid.Add(new Guid(i));
                }
                return guid;
            }
        }

        public bool Validate()
        {
            if (!string.IsNullOrEmpty(this.StartAdmittedDate) && !Validator.ValidateTimeDateWithoutSecond(this.StartAdmittedDate))
            {
                return false;
            }
            if (!string.IsNullOrEmpty(this.EndAdmittedDate) && !Validator.ValidateTimeDateWithoutSecond(this.EndAdmittedDate))
            {
                return false;
            }
            if (!string.IsNullOrEmpty(this.EmergencyStatus) && !Validator.ValidateGuid(this.EmergencyStatus))
            {
                return false;
            }
            return true;
        }
    }
}