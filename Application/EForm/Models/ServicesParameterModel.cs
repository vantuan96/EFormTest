using DataAccess.Models.GeneralModel;
using System;
using System.Collections.Generic;

namespace EForm.Models
{
    public class ServicesParameterModel : PagingParameterModel
    {
        public string Ids { get; set; }
        public Guid? GroupId { get; set; }
        public string GroupCode { get; set; }
        public string PID { get; set; }
        public int? Type { get; set; }
        public int? FilterType { get; set; } // 1 cá nhân 2 được chia sẻ
        public string Search { get; set; } = "";
        public string ConvertedSearch
        {
            get
            {
                return this.Search.Trim().ToLower();
            }
        }
        public string ConvertedSearch2
        {
            get
            {
                return this.Search.Trim();
            }
        }
    }
    public class OrderSetsModel
    {
        public Guid? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public List<Guid> ServiceIds { get; set; }
        public List<Guid> UserIds { get; set; }
        public List<Service> Services { get; set; }
    }
    public class ServicesGetPriceParameterModel
    {
        public List<Guid> Ids { get; set; }
        public Guid PatientVisitId { get; set; }
        public Guid PatientLocationId { get; set; }
        public string Code { get; set; }

    }
    public class ChargeModel
    {
        public string PID { get; set; }
        public List<ChargeItem> ChargeItems { get; set; }
        public List<Guid> Ids { get; set; }
        public Guid ChargeId { get; set; }
        public Charge Charge { get; set; }
        public ChargeVisit Visit { get; set; }
    }

    public class ChargeUpdateModel
    {
        public Guid ChargeId { get; set; }
        public string Status { get; set; }
        public string FailedReason { get; set; }
    }

    public class ServicesModel
    {
        public List<RadiologyProcedurePlanRef> RadiologyProcedurePlanRef;

        public Guid Id { get; set; }
        public bool hasLabDepartment { get; set; }
        public Guid? GroupId { get; set; }
        public Guid HISId { get; set; }
        
        public string ViName { get; set; } = "";
        public int ItemType { get; set; }
        public int ServiceType { get; set; }
        
        public string EnName { get; set; } = "";
        public string Code { get; set; } = "";
        public string CombinedName { get; set; } = "";
        public string ServiceGroupCode { get; set; } = "";

        public decimal? Price { get; set; }

        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; } = "";

        public string RootServiceGroupCode { get; set; }
        public Guid? RootServiceGroupId { get; set; }

        public bool AllowSetupQty { get; set; }

        public int Qty { get; set; } = 1;
        public string Status { get; set; }
    }
    public class DataHcModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
}