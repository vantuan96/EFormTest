using EForm.Common;
using System;
using System.Collections.Generic;

namespace EForm.Models.EDModels
{
    public class EDParameterModel : PagingParameterModel
    {
        public string Search { get; set; }
        public string StartAdmittedDate { get; set; }
        public string EndAdmittedDate { get; set; }
        public string ATSScale { get; set; }
        public string RickGroups { get; set; }
        public int MetDoctor { get; set; } = -9;
        public string EmergencyStatus { get; set; }
        public bool? IsRetailService { get; set; }
        public int? Sort { get; set; }
        public Guid? FormIdNewborn { get; set; }
        public string ConvertedSearch {
            get {
                return this.Search.Trim().ToLower();
            }
        }
        
        public string CreatedBy { get; set; }
        public string StartAt { get; set; }
        public DateTime? ConvertedStartAt
        {
            get
            {
                if (string.IsNullOrEmpty(this.StartAt))
                {
                    return null;
                }
                return DateTime.ParseExact(this.StartAt, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            }
        }
        public DateTime? ConvertedEndAt
        {
            get
            {
                if (string.IsNullOrEmpty(this.EndAt))
                {
                    return null;
                }
                return DateTime.ParseExact(this.EndAt, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            }
        }
        public string EndAt { get; set; }
        public DateTime? ConvertedStartAdmittedDate
        {
            get
            {
                if (string.IsNullOrEmpty(this.StartAdmittedDate)){
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
        public string[] ConvertedATSScale
        {
            get
            {
                return this.ATSScale.Split(',');
            }
        }

        public bool ConvertedMetDoctor
        {
            get
            {
                if(this.MetDoctor == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public List<int?> ConvertedRickGroups
        {
            get
            {
                string[] id = this.RickGroups.Split(',');
                List<int?> guid = new List<int?>();
                foreach (var i in id)
                {
                    guid.Add(int.Parse(i));
                }
                return guid;
            }
        }
        public List<Guid?> ConvertedEmergencyStatus
        {
            get
            {
                string[] id = this.EmergencyStatus.Split(',');
                List<Guid?> guid = new List<Guid?>();
                foreach(var i in id)
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
            if(this.MetDoctor != -9 && (this.MetDoctor < 0 || this.MetDoctor > 1))
            {
                return false;
            }
            if(!string.IsNullOrEmpty(this.EmergencyStatus) && !Validator.ValidateGuid(this.EmergencyStatus))
            {
                return false;
            }
            return true;
        }
    }
}