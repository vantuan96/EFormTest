using EForm.Common;
using System;

namespace EForm.Models.EDModels
{
    public class PatientHandOverRecordParameterModel
    {
        public string ReasonForTransfer { get; set; }
        public string HandOverTimePhysician { get; set; }
        public string HandOverPhysician { get; set; }
        public string HandOverUnitPhysician { get; set; }
        public string ReceivingPhysician { get; set; }
        public string ReceivingUnitPhysician { get; set; }
        public dynamic ConvertedHandOverTimePhysician
        {
            get
            {
                if (string.IsNullOrEmpty(this.HandOverTimePhysician)){
                    return null;
                }
                return DateTime.ParseExact(this.HandOverTimePhysician, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            }
        }
        public bool Validate()
        {
            if (!string.IsNullOrEmpty(this.HandOverTimePhysician) && !Validator.ValidateTimeDateWithoutSecond(this.HandOverTimePhysician))
            {
                return false;
            }
            return true;
        }
    }
}