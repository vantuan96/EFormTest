using DataAccess.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.OPDModel
{
    public class OPDPatientProgressNote: IEntity
    {
        public OPDPatientProgressNote()
        {
            this.OPDPatientProgressNoteDatas = new HashSet<OPDPatientProgressNoteData>();
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
        public virtual ICollection<OPDPatientProgressNoteData> OPDPatientProgressNoteDatas { get; set; }
    }
}
