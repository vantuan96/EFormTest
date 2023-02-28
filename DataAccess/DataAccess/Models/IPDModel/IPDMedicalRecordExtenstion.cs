using DataAccess.Model.BaseModel;
using DataAccess.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.IPDModel
{
    public class IPDMedicalRecordExtenstion : VBaseModel
    {
        public string Name { get; set; }
        public Guid? VisitId { get; set; }
        public Guid? DoctorConfirmId { get; set; }
        public DateTime? DoctorConfirmAt { get; set; }
        public string FormCode { get; set; }
        public int Version { get; set; }
    }
}
