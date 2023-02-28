using DataAccess.Model.BaseModel;
using DataAccess.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.OPDModel
{
    public class OPDOutpatientExaminationNote : VBaseModel
    {
        public OPDOutpatientExaminationNote()
        {
            this.OPDOutpatientExaminationNoteDatas = new HashSet<OPDOutpatientExaminationNoteData>();
        }
        public DateTime? ExaminationTime { get; set; }
        public string Service { get; set; }
        public DateTime? BlockTime { get; set; }
        public bool IsAuthorizeDoctorChangeForm { get; set; }
        public bool IsDoctorChangeForm { get; set; }
        public virtual ICollection<OPDOutpatientExaminationNoteData> OPDOutpatientExaminationNoteDatas { get; set; }
        public bool? IsConsultation { get; set; } = false;
        public DateTime? AppointmentDateResult { get; set; }
        public int Version { get; set; } = 2; // tạo luôn ver 2 khi tạo hồ sơ mới
    }
}
