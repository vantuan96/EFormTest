using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EForm.Models.MedicationAdministrationRecordModels
{
    public class IPDPrescriptionModel
    {
        public string SeqNum { get; set; }
        public string Usage { get; set; }
        public string AdministrationUserName { get; set; }
        public DateTime VisitCreatedDate { get; set; }
        public string PatientArea { get; set; }
        public DateTime ActualVisitDate { get; set; }
        public string PrescriptionId { get; set; } // Mã đơn thuốc
        public string PID { get; set; }
        public string AreaShortCode { get; set; }
        public string VisitCode { get; set; }
        public string ItemId { get; set; } // Mã thuốc
        public string PharmacyName { get; set; }
        public DateTime? AdministrationDateTime { get; set; }
        public string StartDate { get; set; }
        public string PrimaryDoctorAD { get; set; }
        private string pharmacySource;
        public string PharmacySource
        {
            get
            {
                if (PharmacyApprovalStatus == null && !pharmacySource.Contains("New"))
                {
                    return $"New-{pharmacySource}";
                }
                return pharmacySource;
            }
            set { pharmacySource = value; }
        }
        public string PrescriptionCode { get; set; }
        public object Note { get; set; }
        public string CreatedDate { get; set; }
        public string StopDate { get; set; }
        public string VisitType { get; set; }
        public DateTime? LastAdministrationDateTime { get; set; }
        public string OrderReferenceNumber { get; set; }
        public string PharmacyApprovalStatus { get; set; }

        private string caSang;
        public string CaSang
        {
            get
            {
                if (PlannedAdministrationDateTime != null && AdministrationDateTime != null)
                {
                    TimeSpan time = Convert.ToDateTime(AdministrationDateTime).TimeOfDay;
                    if (time >= new TimeSpan(09, 00, 00) && time <= new TimeSpan(21, 29, 59))
                    {
                        return $"{time:hh\\:mm\\:ss} {AdministrationUserName}";
                    }
                    else
                    {
                        caToi = $"{time:hh\\:mm\\:ss} {AdministrationUserName}";
                        return "";
                    }
                }
                else if (PlannedAdministrationDateTime != null && AdministrationDateTime == null)
                {
                    TimeSpan plannedTime = Convert.ToDateTime(PlannedAdministrationDateTime).TimeOfDay;
                    if (plannedTime >= new TimeSpan(09, 00, 00) && plannedTime <= new TimeSpan(21, 29, 59))
                    {
                        return $"{plannedTime:hh\\:mm\\:ss} {AdministrationUserName}";
                    }
                    else
                    {
                        caToi = $"{plannedTime:hh\\:mm\\:ss} {AdministrationUserName}";
                        return "";
                    }
                }
                else if (AdministrationDateTime != null && PlannedAdministrationDateTime == null)
                {
                    TimeSpan time = Convert.ToDateTime(AdministrationDateTime).TimeOfDay;
                    if (time >= new TimeSpan(09, 00, 00) && time <= new TimeSpan(21, 29, 59))
                    {
                        return $"{time:hh\\:mm\\:ss} {AdministrationUserName}";
                    }
                    else
                    {
                        caToi = $"{time:hh\\:mm\\:ss} {AdministrationUserName}";
                        return "";
                    }
                }
                else
                {
                    return null;
                }
            }
            set { caSang = value; }
        }
        private string caToi;
        public string CaToi
        {
            get { return caToi; }
            set { caToi = value;}
        }

        public string Shift01 { get; set; }
        public string Shift02 { get; set; }
        public string HospitalCode { get; set; }
        public string RecurrenceType { get; set; }
        public string RecurrencePeriod { get; set; }

        private string nameForGroup;
        public string NameForGroup
        {
            get
            {
                return $"{ItemId}{Usage}{PrescriptionCode}";
            }
            set { nameForGroup = value; }
        }

        public DateTime? PlannedAdministrationDateTime { get; set; }

        public Site HospitalInfo { get; set; } //Thông tin bệnh viện
        public string RecurrenceCode { get; set; }
        public double Duration { get; set; }
        public string DType { get; set; } //Loại thuốc: TP - Thực phẩm chức năng, N - Thuốc gây nghiện, H - Thuốc hướng thần, A - Thuốc thường
        public string PrescriberAD { get; set; }
    }
}
