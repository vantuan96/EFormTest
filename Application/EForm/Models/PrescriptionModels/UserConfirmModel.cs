using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EForm.Models.PrescriptionModels
{
    public class UserConfirmModel
    {
        // Bác sỹ
        public Guid? DoctorConfirmId { get; set; }
        public string UserNameDoctor { get; set; }
        // Dược sỹ
        public Guid? PharmacistConfirmId { get; set; }
        public string UserNamePharmacist { get; set; }
        // Trưởng khoa
        public Guid? HeadOfDepartmentConfirmId { get; set; }
        public string UserNameHeadOfDepartment { get; set; }
        // 
        public Guid? PhysicianConfirmId { get; set; }
        public string UserNamePhysician { get; set; }
        // Giám đốc
        public Guid? DirectorId { get; set; }
        public string UserNameDirector { get; set; }
        // Y tá
        public Guid? NurseId { get; set; }
        public string UserNameNurse { get; set; }
    }
}