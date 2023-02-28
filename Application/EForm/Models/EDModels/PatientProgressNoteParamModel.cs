using EForm.Common;
using System;

namespace EForm.Models.EDModels
{
    public class PatientProgressNoteParamModel
    {
        public string Id { get; set; }
        public string Note { get; set; }
        public string Interventions { get; set; }
        public string NoteAt { get; set; }

        public DateTime ConvertedNoteAt
        {
            get
            {
                if(string.IsNullOrEmpty(this.NoteAt))
                {
                    return DateTime.Now;
                }
                return DateTime.ParseExact(this.NoteAt, Constant.DATE_TIME_FORMAT, null);
            }
        }

        public Guid ConvertedId
        {
            get
            {
                if (string.IsNullOrEmpty(this.Id))
                {
                    return Guid.Empty;
                }
                return new Guid(this.Id);
            }
        }

        public bool Validate()
        {
            if (!string.IsNullOrEmpty(this.Id) && !Validator.ValidateGuid(this.Id))
            {
                return false;
            }
            if (!string.IsNullOrEmpty(this.NoteAt) && !Validator.ValidateDateTime(this.NoteAt))
            {
                return false;
            }
            return true;
        }
    }
}