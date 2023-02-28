using DataAccess.Model.BaseModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.IPDModel
{
    public class IPDPatientProgressNoteData : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DateTime { get; set; }
        public string Note { get; set; }
        public string Interventions { get; set; }
        public DateTime? NoteAt { get; set; }
        public Guid? IPDPatientProgressNoteId { get; set; }
        [ForeignKey("IPDPatientProgressNoteId")]
        public virtual IPDPatientProgressNote IPDPatientProgressNote { get; set; }
    }
}