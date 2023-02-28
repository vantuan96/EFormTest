using DataAccess.Models;
using DataAccess.Models.GeneralModel;
using EForm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EForm.Models
{
    public class HumanResourceListQueryModel
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public DateTime? ConvertedStartDate
        {
            get
            {
                if (string.IsNullOrEmpty(this.StartDate)) return null;

                return DateTime.ParseExact(this.StartDate, Constant.DATE_FORMAT, null);
            }
        }
        public DateTime? ConvertedEndDate
        {
            get
            {
                if (string.IsNullOrEmpty(this.EndDate)) return null;

                return DateTime.ParseExact(this.EndDate, Constant.DATE_FORMAT, null);
            }
        }

        public bool Validate()
        {
            if (string.IsNullOrEmpty(this.StartDate) || !Validator.ValidateDate(this.StartDate))
                return false;

            if (string.IsNullOrEmpty(this.EndDate) || !Validator.ValidateDate(this.EndDate))
                return false;

            return true;
        }
    }

    public class HumanResourceQueryModel
    {
        public string Name { get; set; }
        public string Now { get; set; }
        public string Date { get; set; }
        public DateTime? ConvertedNow
        {
            get
            {
                if (string.IsNullOrEmpty(this.Now)) return null;

                return DateTime.ParseExact(this.Now, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            }
        }
        public DateTime? ConvertedDate
        {
            get
            {
                if (string.IsNullOrEmpty(this.Date)) return null;

                return DateTime.ParseExact(this.Date, Constant.DATE_FORMAT, null);
            }
        }
    }
    public class HumanResourceAssessmentStaffModel
    {
        public string EnName { get; set; }
        public string ViName { get; set; }
        public int Order { get; set; }
        public string Type { get; set; }
        public Guid? UserId { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string FullShort { get; set; }
    }

    public class HumanResourceItem
    {
        public string ViName { get; set; }
        public string EnName { get; set; }
        public int Order { get; set; }
        public List<HumanResourceItemGroupStaff> GroupStaffs { get; set; }
    }
    public class HumanResourceItemGroupStaff
    {
        public string Type { get; set; }
        public List<dynamic> Users { get; set; }
    }
}