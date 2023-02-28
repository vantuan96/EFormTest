using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EForm.Models
{
    public class PatientManagementQueryModel
    {
        public Guid Id { get; set; }
        public DateTime AdmittedDate { get; set; }
        public string Bed { get; set; }
        public string Reason { get; set; }
        public Guid? CurrentDoctorId { get; set; }
        public string CurrentDoctorUsername { get; set; }
        public string CurrentDoctorFullname { get; set; }
        public string CurrentDoctorFullShort { get; set; }
        public Guid? CurrentNurseId { get; set; }
        public string CurrentNurseUsername { get; set; }
        public string CurrentNurseFullname { get; set; }
        public string CurrentNurseFullShort { get; set; }
        public string ATSScaleCode { get; set; }
        public string ATSScaleViName { get; set; }
        public string ATSScaleEnName { get; set; }
        public string ATSScaleNote { get; set; }
    }

    public class PatientManagementViewModel
    {
        public Guid Id { get; set; }
        public string AdmittedDate { get; set; }
        public string Bed { get; set; }
        public string Reason { get; set; }
        public dynamic CurrentDoctor { get; set; }
        public dynamic CurrentNurse { get; set; }
        public dynamic ATSScale { get; set; }
    }
}
