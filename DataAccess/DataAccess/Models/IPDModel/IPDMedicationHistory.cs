using DataAccess.Model.BaseModel;
using DataAccess.Models.BaseModel;
using System;
using System.Collections.Generic;

namespace DataAccess.Models.IPDModel
{
    public class IPDMedicationHistory : VBaseModel
    {

        public Guid? VisitId { get; set; }
        public string RoomId { get; set; }
        public Guid? PharmacistConfirmId { get; set; }
        public DateTime? PharmacistConfirmAt { get; set; }
        public Guid? DoctorConfirmId { get; set; }
        public DateTime? DoctorConfirmAt { get; set; }
        public string FormCode { get; set; }
        public int Version { get; set; }
    }
}