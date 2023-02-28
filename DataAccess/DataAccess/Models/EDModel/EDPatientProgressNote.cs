using DataAccess.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.EDModel
{
    public class EDPatientProgressNote: IEntity
    {
        public EDPatientProgressNote()
        {
            this.PatientProgressNoteDatas = new HashSet<EDPatientProgressNoteData>();
        }
        [Key]
        public Guid Id { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<DateTime> NoteAt { get; set; }
        public virtual ICollection<EDPatientProgressNoteData> PatientProgressNoteDatas { get; set; }
    }
}
