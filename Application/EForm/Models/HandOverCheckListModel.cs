using EForm.Common;
using System;
using System.Collections.Generic;

namespace EForm.Models
{
    public class HandOverCheckListModel
    {
        public Guid Id { get; set; }
        public string PID { get; set; }
        public string Phone { get; set; }
        public string Fullname { get; set; }
        public string DateOfBirth { get; set; }
        public string VisitCode { get; set; }
        public string TransferDate { get; set; }
        public bool IsAcceptNurse { get; set; }
        public string HandOverNurseUsername { get; set; }
        public string ReceivingNurseUsername { get; set; }
        public bool IsAcceptPhysician { get; set; }
        public string HandOverPhysicianUsername { get; set; } 
        public string ReceivingPhysicianUsername { get; set; }
        public string HandOverUnit { get; set; }
        public string VisitTypeGroupCode { get; set; }
        public Guid VisitId { get; set; }
        public bool IsUseHandOverCheckList { get; set; }
    }

    public class HandOverCheckListQueryModel
    {
        public Guid Id { get; set; }
        public DateTime? TransferDate { get; set; }
        public Guid? HandOverNurseId { get; set; }
        public string HandOverNurseUsername { get; set; }
        public Guid? ReceivingNurseId { get; set; }
        public string ReceivingNurseUsername { get; set; }
        public Guid? HandOverPhysicianId { get; set; }
        public string HandOverPhysicianUsername { get; set; }
        public Guid? ReceivingPhysicianId { get; set; }
        public string ReceivingPhysicianUsername { get; set; }
        public string HandOverUnit { get; set; }
        public bool IsAcceptNurse { get; set; }
        public bool IsAcceptPhysician { get; set; }
        public Guid? SiteId { get; set; }
        public string PID { get; set; }
        public string Fullname { get; set; }
        public string Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string VisitCode { get; set; }
        public string VisitTypeGroupCode { get; set; }
        public Guid VisitId { get; set; }
        public bool IsUseHandOverCheckList { get; set; }
    }

    public class HandOverCheckListParameterModel : PagingParameterModel
    {
        
        public string isCheckTranfer { get; set; }
        public string Search { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int? Status { get; set; }
        public string UserAccept { get; set; }

        public string Type { get; set; }

        public string ConvertedSearch
        {
            get
            {
                return this.Search.Trim().ToLower();
            }
        }
        public DateTime? ConvertedStartDate
        {
            get
            {
                if (string.IsNullOrEmpty(this.StartDate))
                {
                    return null;
                }
                return DateTime.ParseExact(this.StartDate, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            }
        }
        public DateTime? ConvertedEndDate
        {
            get
            {
                if (string.IsNullOrEmpty(this.EndDate))
                {
                    return null;
                }
                return DateTime.ParseExact(this.EndDate, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            }
        }
        public List<Guid?> ConvertedUserAccept
        {
            get
            {
                string[] id = this.UserAccept.Split(',');
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
            if (!string.IsNullOrEmpty(this.StartDate) && !Validator.ValidateTimeDateWithoutSecond(this.StartDate))
            {
                return false;
            }
            if (!string.IsNullOrEmpty(this.EndDate) && !Validator.ValidateTimeDateWithoutSecond(this.EndDate))
            {
                return false;
            }
            return true;
        }
    }
}