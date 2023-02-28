using DataAccess.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.IPDModel
{
    public class IPDPatientProgressNote : IEntity
    {
        public IPDPatientProgressNote()
        {
            this.IPDPatientProgressNoteDatas = new HashSet<IPDPatientProgressNoteData>();
        }
        [Key]
        public Guid Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? NoteAt { get; set; }
        public virtual ICollection<IPDPatientProgressNoteData> IPDPatientProgressNoteDatas { get; set; }
    }
}
