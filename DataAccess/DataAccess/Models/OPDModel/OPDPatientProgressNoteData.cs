using DataAccess.Model.BaseModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.OPDModel
{
    public class OPDPatientProgressNoteData : IEntity
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
        public Nullable<DateTime> DateTime { get; set; }
        public string Note { get; set; }
        public string Interventions { get; set; }
        public Nullable<DateTime> NoteAt { get; set; }
        public Nullable<Guid> OPDPatientProgressNoteId { get; set; }
        [ForeignKey("OPDPatientProgressNoteId")]
        public virtual OPDPatientProgressNote OPDPatientProgressNote { get; set; }
    }
}