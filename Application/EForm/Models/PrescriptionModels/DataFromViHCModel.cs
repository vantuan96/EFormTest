using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EForm.Models.PrescriptionModels
{
    public class DataFromViHCModel
    {
        public class ICD10
        {
            public string NameVN { get; set; }
            public string NameEN { get; set; }
            public string DoctorAD { get; set; }
            public string Code { get; set; }
            public string SpecialtyCode { get; set; }
        }

        public class ICD10s
        {
            public List<ICD10> ICD10 { get; set; }
        }

        public class Result
        {
            public string RR { get; set; }
            public DateTime ExaminedDate { get; set; }
            public string Pulse { get; set; }
            public string DoctorAD { get; set; }
            public DateTime? GPCompletedTime { get; set; }
            public string PID { get; set; }
            public string VisitCode { get; set; }
            public string Weight { get; set; }
            public string BP { get; set; }
            public string HospitalCode { get; set; }
            public string T { get; set; }
            public string FullName { get; set; }
            public ICD10s ICD10s { get; set; }
            public string Height { get; set; }
        }

        public class Root
        {
            public Result Result { get; set; }
        }

    }
}
