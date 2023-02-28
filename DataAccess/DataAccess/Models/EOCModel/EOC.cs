using DataAccess.Model.BaseModel;
using DataAccess.Models.BaseModel;
using DataAccess.Models.EIOModel;
using DataAccess.Models.OPDModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.EOCModel
{
    public class EOC : VisitInfo
    {
        public DateTime? DischargeDate { get; set; }
        public string HealthInsuranceNumber { get; set; }
        public DateTime? StartHealthInsuranceDate { get; set; }
        public DateTime? ExpireHealthInsuranceDate { get; set; }
        public string TransferFromType { get; set; }
        public Guid? StatusId { get; set; }
        [ForeignKey("StatusId")]
        public virtual EDStatus Status { get; set; }
        
        public Guid? AuthorizedDoctorId { get; set; }
        [ForeignKey("AuthorizedDoctorId")]
        public virtual User AuthorizedDoctor { get; set; }

        public Guid? EFormVisitId { get; set; }

        public DateTime? LastUpdatedAtByDoctor { get; set; }

        public Guid? OPDPatientProgressNoteId { get; set; }
        [ForeignKey("OPDPatientProgressNoteId")]
        public virtual OPDPatientProgressNote OPDPatientProgressNote { get; set; }

        public Guid? OPDObservationChartId { get; set; }
        [ForeignKey("OPDObservationChartId")]
        public virtual OPDObservationChart OPDObservationChart { get; set; }
        public int Version { get; set; }
        public bool IsHasFallRiskScreening { get; set; }
    }
}
