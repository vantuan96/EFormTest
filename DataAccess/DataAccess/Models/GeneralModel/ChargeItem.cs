using DataAccess.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.GeneralModel
{
    public class ChargeItem: VBaseModel
    {
        public string HospitalCode { get; set; }
        public string PatientLocationCode { get; set; }
        public string VisitGroupType { get; set; }

        public Guid CustomerId { get; set; }
        public Guid PatientVisitId { get; set; }// Lấy từ API get Visit
        public string VisitCode { get; set; }// Lấy từ API get Visit
        public string VisitType { get; set; }// Lấy từ API get Visit
        public Guid ChargeId { get; set; } // Charge.Id
        public Guid ItemId { get; set; } // Service.HISId
        public string ServiceCode { get; set; }
        public Guid ServiceId { get; set; }
        [ForeignKey("ServiceId")]
        public virtual Service Service { get; set; }
        public int ItemType { get; set; } // 0 VS, 1 GPB
        public int ServiceType { get; set; } // //0  Lab,1 Rad,2 Allign
        public Guid? ChargeSessionId { get; set; }//Lấy sau khi tạo Charge lên OH thành công
        public Guid? ChargeDetailId { get; set; }//Lấy sau khi tạo Charge lên OH thành công
        public string Filler { get; set; }//Lấy sau khi tạo Charge lên OH thành công
        public string FillerGroup { get; set; }//Lấy sau khi tạo Charge lên OH thành công
        public string Quantity { get; set; } // Mặc định là 1
        public string PatientId { get; set; }// PID
        
        public Guid? CostCentreId { get; set; }// Lấy từ API get Visit
        public Guid PatientLocationId { get; set; }// Lấy từ API get Visit
        
        public Guid? PlacerOrderableId { get; set; }
        public Guid? ServiceDepartmentId { get; set; }
        //R Routine
        //S STAT
        //W ASAP
        public string DoctorAD { get; set; }
        public string Priority { get; set; }
        public string Date { get; set; }
        public string Comments { get; set; }
        public string Instructions { get; set; }
        public string InitialDiagnosis { get; set; }
        public string Reason { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlacerIdentifyNumber { get; set; } // 1000
        public string AdditionalInformation { get; set; }
        public string Status { get; set; }
        public string CancelComment { get; set; }
        public string CancelBy { get; set; }
        public string ChargeItemType { get; set; } // Lab, Rad, Allies
        public string FailedReason { get; set; }
        public string CancelFailedReason { get; set; }

        public Guid? ChargeItemPathologyId { get; set; }
        [ForeignKey("ChargeItemPathologyId")]
        public virtual ChargeItemPathology Pathology { get; set; }

        public Guid? ChargeItemMicrobiologyId { get; set; }
        [ForeignKey("ChargeItemMicrobiologyId")]
        public virtual ChargeItemMicrobiology Microbiology { get; set; }

        
        public Guid? RadiologyProcedureId { get; set; }
        [ForeignKey("RadiologyProcedureId")]
        public virtual RadiologyProcedurePlanRef RadiologyProcedurePlan { get; set; }
        public string PaymentStatus { get; set; }
        public string RadiologyScheduledStatus { get; set; }
        public string PlacerOrderStatus { get; set; }
        public string SpecimenStatus { get; set; }
        [NotMapped]
        public bool Selected { get; set; } = false;
        [NotMapped]
        public bool DiagnosticReported { get; set; } = false;
        [NotMapped]
        public bool AllowCancel
        {
            get
            {
                var allowCancelStatus = new List<string>() { "Placed" };
                var allowCancelRadiologyScheduledStatus = new List<string>() { RadiologyScheduledProcedureStatus.Scheduled };
                var notAllowCancelPlacerOrderStatus = new List<string>() { CpoePlacerOrderStatus.Completed, CpoePlacerOrderStatus.Cancelled };
                var allowCancelPaymentStatus = new List<string>() { ChargePaymentStatus.Voided, ChargePaymentStatus.Unknown, ChargePaymentStatus.Incomplete };
                var allowCancelSpecimenStatus = new List<string>() { SpecimenStatuses.Created, SpecimenStatuses.Deleted, SpecimenStatuses.Discarded, SpecimenStatuses.Rejected, SpecimenStatuses.MarkedForRecollection };
                if (!string.IsNullOrEmpty(this.Status) && !allowCancelStatus.Contains(this.Status))
                    return false;
                if (!string.IsNullOrEmpty(this.PaymentStatus) && !allowCancelPaymentStatus.Contains(this.PaymentStatus))
                    return false;
                if (!string.IsNullOrEmpty(this.RadiologyScheduledStatus) && !allowCancelRadiologyScheduledStatus.Contains(this.RadiologyScheduledStatus))
                    return false;
                if (!string.IsNullOrEmpty(this.PlacerOrderStatus) && notAllowCancelPlacerOrderStatus.Contains(this.PlacerOrderStatus))
                    return false;
                if (!string.IsNullOrEmpty(this.SpecimenStatus) && !allowCancelSpecimenStatus.Contains(this.SpecimenStatus))
                    return false;
                return true;
            }
        }
        [NotMapped]
        public bool AllowEditWithSpecimenStatus
        {
            get
            {
                var allowCancelSpecimenStatus = new List<string>() { SpecimenStatuses.Created, SpecimenStatuses.Deleted, SpecimenStatuses.Discarded, SpecimenStatuses.Rejected, SpecimenStatuses.MarkedForRecollection };
                if (!string.IsNullOrEmpty(this.SpecimenStatus) && !allowCancelSpecimenStatus.Contains(this.SpecimenStatus))
                    return false;
                return true;
            }
        }
        [NotMapped]
        public int? Qty { get; set; } = 1;
        public decimal? Price { get; set; }
        public DateTime? SyncAt { get; set; }
    }
    public class ChargePackage : VBaseModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public virtual ICollection<ChargePackageService> ChargePackageService { get; set; }
        public virtual ICollection<ChargePackageUser> ChargePackageUser { get; set; }
    }
    public class ChargePackageService : VBaseModel
    {
        public Guid ServiceId { get; set; }
        [ForeignKey("ServiceId")]
        public virtual Service Service { get; set; }
        public Guid ChargePackageId { get; set; }
        [ForeignKey("ChargePackageId")]
        public virtual ChargePackage ChargePackage { get; set; }
    }
    public class ChargePackageUser : VBaseModel
    {
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public Guid ChargePackageId { get; set; }
        [ForeignKey("ChargePackageId")]
        public virtual ChargePackage ChargePackage { get; set; }
    }
    public class ChargePaymentStatus
    {
        public static string Complete = "COM";
        public static string Incomplete = "INC";
        public static string PostedToGL = "POS";
        public static string PostedToSubLedger = "PSL";
        public static string Temporary = "TEMP";
        public static string Unknown = "UNK";
        public static string Voided = "VOI";
    };

    public class RadiologyScheduledProcedureStatus
    {
        public static string Arrived = "ARRIVED";
        public static string Cancelled = "CANCELLED";
        public static string Processed = "PROCESSED";
        public static string Scheduled = "SCHEDULED";
    };

    public class CpoePlacerOrderStatus
    {
        public static string Aborted = "ABORT";
        public static string Verified = "ACTIV";
        public static string Cancelled = "CANCL";
        public static string Completed = "COMPL";
        public static string CollectedAcquired = "CON";
        public static string Held = "HELD";
        public static string New = "NEW";
        public static string Nullified = "NULLI";
        public static string Obsolete = "OBSOL";
        public static string ResultsPublished = "REPUB";
        public static string Suspended = "SUSPE";
        public static string FutureVisitOrder = "UNCON";
    };

    public class SpecimenStatuses
    {
        public static string Collected = "COL";
        public static string Deleted = "DEL";
        public static string Discarded = "DIS";
        public static string OnCollectionList = "LST";
        public static string Created = "ORD";
        public static string Quarantined = "QRT";
        public static string MarkedForRecollection = "RCL";
        public static string LabReceived = "REC";
        public static string Rejected = "RJT";
        public static string RetrievedFromRack = "RTV";
        public static string SentOut = "SOT";
        public static string StoredInRack = "STR";
    };
}
