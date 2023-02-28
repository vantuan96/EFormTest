using DataAccess.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.IPDModel
{
    public class IPDInitialAssessmentForNewborns: VBaseModel
    {
        public Guid? VisitId { get; set; }
        public string RoomId { get; set; }
        public string DataType { get; set; }
        public Guid? MedicalStaff1ConfirmId { get; set; }
        public DateTime? MedicalStaff1ConfirmAt { get; set; }
        public Guid? MedicalStaff2ConfirmId { get; set; }
        public DateTime? MedicalStaff2ConfirmAt { get; set; }
        public DateTime? DateOfAdmission { get; set; }
        public Guid? EIOConstraintNewbornAndPregnantWomanId { get; set; }
        public int Version { get; set; }
    }
}
