using DataAccess.Model.BaseModel;
using DataAccess.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.IPDModel
{
    public class OPDRiskAssessmentForCancer : VBaseModel
    {
        public Guid? VisitId { get; set; }
        public Guid? DoctorConfirmId { get; set; }
        public DateTime? DoctorConfirmAt { get; set; }
    }
}
