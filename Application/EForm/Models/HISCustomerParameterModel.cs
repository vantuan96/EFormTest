using EForm.Common;
using System;

namespace EForm.Models
{
    public class HISCustomerParameterModel
    {
        
        public Guid? VisitId { get; set; }
        public string VisitType { get; set; }
        public string Fullname { get; set; }
        public string DateOfBirth { get; set; }
        public int Gender { get; set; } = 2;
        public string PID { get; set; }
        public string VisitCode { get; set; }
        public string PatientLocationCode { get; set; }
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

        public bool Validate()
        {
            if (!string.IsNullOrEmpty(this.DateOfBirth) && !Validator.ValidateDate(this.DateOfBirth))
            {
                return false;
            }
            if (this.Gender < 0 || this.Gender > 2)
            {
                return false;
            }
            return true;
        }
    }
}