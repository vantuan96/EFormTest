using EForm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EForm.Models.IPDModels
{
    public class IPDParameterModel: PagingParameterModel
    {
        public string Search { get; set; }
        public string StartAdmittedDate { get; set; }
        public string EndAdmittedDate { get; set; }
        public string EmergencyStatus { get; set; }
        public string User { get; set; }
        public string Clinic { get; set; }
        public bool? IsTelehealth { get; set; }
        public bool? IsDraft { get; set; }

        public string ConvertedSearch
        {
            get
            {
                return this.Search.Trim().ToLower();
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