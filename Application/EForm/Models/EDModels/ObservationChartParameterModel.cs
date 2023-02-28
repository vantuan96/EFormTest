using EForm.Common;
using System;

namespace EForm.Models.EDModels
{
    public class ObservationChartParameterModel
    {
        public string Id { get; set; }
        public string SysBP { get; set; }
        public string DiaBP { get; set; }
        public string Pulse { get; set; }
        public string Temperature { get; set; }
        public string Resp { get; set; }
        public string SpO2 { get; set; }
        public string RestPainScore { get; set; }
        public string MovePainScore { get; set; }
        public string Other { get; set; }
        public string NoteAt { get; set; }

        public DateTime ConvertedNoteAt
        {
            get
            {
                if (string.IsNullOrEmpty(this.NoteAt))
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