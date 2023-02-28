using DataAccess.Models.BaseModel;
using System;

namespace DataAccess.Models.MedicationAdministrationRecordModel
{
    public class MedicationAdministrationRecordModel : VBaseModel
    {
        public Guid? VisitId { get; set; }
        public string PID { get; set; }
        public string VisitCode { get; set; }
        public string PrescriptionCode { get; set; }
        public string DietCode { get; set; }
    }
}
