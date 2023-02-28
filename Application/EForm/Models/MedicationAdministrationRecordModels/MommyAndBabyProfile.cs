using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EForm.Models.MedicationAdministrationRecordModels
{
    public class MommyAndBabyProfile
    {
        public Guid Id { get; set; }
        public string PID { get; set; }
        public string Fullname { get; set; }
        public DateTime? DOB { get; set; }
        public string VisitCode { get; set; }
        public string PhoneNumber { get; set; }
        public string PrimaryDoctor { get; set; }
        public string Receiver { get; set; }
        public string Department { get; set; }
        public DateTime ExaminationTime { get; set; }
        public string Status { get; set; }
        public bool IsMommy { get; set; }
        public string RecordCode { get; set; }
    }
}
