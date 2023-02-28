using DataAccess.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.IPDModel
{
    public class StillBirth : VBaseModel
    {
        public Guid? VisitId { get; set; }
        public string VisitTypeGroupCode { get; set; }
        public Guid? HospitalLeadershipConfirmId { get; set; }
        public DateTime? HospitalLeadershipConfirmAt { get; set; }
        public Guid? HeadOfDepartmentOrLeaderOfOnDutyTeamConfirmId { get; set; }
        public DateTime? HeadOfDepartmentOrLeaderOfOnDutyTeamConfirmAt { get; set; }
        public Guid? PatientOrPatientIsFamilyConfirmId { get; set; }
        public DateTime? PatientOrPatientIsFamilyConfirmAt { get; set; }

    }
}
