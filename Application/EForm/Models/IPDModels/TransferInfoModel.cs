using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EForm.Models.IPDModels
{
    public class TransferInfoModel
    {
        public string TransferType { get; set; }
        public string TransferDate { get; set; }
        public DateTime? TransferRawDate { get; set; }
        public dynamic TransferSpecialty { get; set; }
        public dynamic TransferDoctor { get; set; }
        public Guid? CurrentTransferFromId { get; set; }
        public string CurrentType { get; set; }
        public string CurrentDate { get; set; }
        public DateTime? CurrentRawDate { get; set; }
        public dynamic CurrentSpecialty { get; set; }
        public dynamic CurrentDoctor { get; set; }
        public string CurrentDiagnosis { get; set; }
        public string CurrentICD { get; set; }

        public DateTime? DischargeDate { get; set; }
        public string TransferSpecialtyCode { get; set; }
        public string CurrentSpecialtyCode { get; set; }
    }

    public class TransferInfoView
    {
        public dynamic Specialty { get; set; }
        public dynamic PrimaryDoctor { get; set; }
        public string AdmittedDate { get; set; }
        public string VisitType { get; set; }
        public string TransferType { get; set; }
        public string Diagnosis { get; set; }
        public string ICD { get; set; }
        public DateTime? DischargeDate { get; set; }
    }
}