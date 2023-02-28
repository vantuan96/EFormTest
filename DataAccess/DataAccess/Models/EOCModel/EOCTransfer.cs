using DataAccess.Model.BaseModel;
using DataAccess.Models.BaseModel;
using DataAccess.Models.EIOModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.EOCModel
{
    public class EOCTransfer : VBaseModel
    {
        public string FromVisitType { get; set; }
        public Guid? FromVisitId { get; set; }
        public Guid? ToVisitId { get; set; }
        public string TransferBy { get; set; }
        public DateTime? TransferAt { get; set; }
        public string AcceptBy { get; set; }
        public DateTime? AcceptAt { get; set; }
        public Guid? SpecialtyId { get; set; }
        public Guid? SiteId { get; set; }
        public Guid? CustomerId { get; set; }
    }
}
