using Dapper.Contrib.Extensions;
using DataAccess.Models.BaseModel;
using DataAccess.Models.GeneralModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class ChargeItemDto : VBaseModel
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
        public string DoctorAD { get; set; }
        public string Priority { get; set; }
        public string Date { get; set; }
        public string Comments { get; set; }
        public string Instructions { get; set; }
        public string InitialDiagnosis { get; set; }
        public string Reason { get; set; }
        public string AdditionalInformation { get; set; }
        public string Status { get; set; }
        public string CancelComment { get; set; }
        public string CancelBy { get; set; }
        public string ChargeItemType { get; set; } // Lab, Rad, Allies
        public string FailedReason { get; set; }
        public string CancelFailedReason { get; set; }
        public Guid? ChargeItemPathologyId { get; set; }

        public Guid? ChargeItemMicrobiologyId { get; set; }
        public Guid? RadiologyProcedureId { get; set; }
        public string PaymentStatus { get; set; }
        public string RadiologyScheduledStatus { get; set; }
        public string PlacerOrderStatus { get; set; }
        public string SpecimenStatus { get; set; }

        public decimal? Price { get; set; }

        //[Computed]
        //public bool DiagnosticReported { get; set; } = false;
    }
}
