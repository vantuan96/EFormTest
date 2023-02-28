using DataAccess.Models.BaseModel;
using System;

namespace DataAccess.Models.PrescriptionModel
{
    public class PrescriptionNoteModel: VBaseModel
    {
        public Guid PrescriptionId { get; set; }
        public string PrescriptionType { get; set; }
        public string Note { get; set; }
    }
}
