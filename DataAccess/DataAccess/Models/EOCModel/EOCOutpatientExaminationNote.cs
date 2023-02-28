using DataAccess.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.EOCModel
{
    public class EOCOutpatientExaminationNote : EOCBase
    {
       
        public DateTime? ExaminationTime { get; set; }

        public bool IsAuthorizeDoctorChangeForm { get; set; }
        public bool IsDoctorChangeForm { get; set; }
    }
}
