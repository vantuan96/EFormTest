using DataAccess.Model.BaseModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.EDModel
{
    public class EDPatientProgressNoteData:IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DateTime { get; set; }
        public string Note { get; set; }
        public string Interventions { get; set; }
        public Nullable<DateTime> NoteAt { get; set; }
        public Nullable<Guid> PatientProgressNoteId { get; set; }
        [ForeignKey("PatientProgressNoteId")]
        public virtual EDPatientProgressNote PatientProgressNote { get; set; }
    }
}
