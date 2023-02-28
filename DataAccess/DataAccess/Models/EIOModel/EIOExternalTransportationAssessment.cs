using DataAccess.Model.BaseModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.EIOModel
{
    public class EIOExternalTransportationAssessment: IEntity
    {
        public EIOExternalTransportationAssessment()
        {
            this.EIOExternalTransportationAssessmentDatas = new HashSet<EIOExternalTransportationAssessmentData>();
            this.EIOExternalTransportationAssessmentEquipments = new HashSet<EIOExternalTransportationAssessmentEquipment>();
        }
        [Key]
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? VisitId { get; set; }
        public string VisitTypeGroupCode { get; set; }
        public DateTime? NurseTime { get; set; }
        public Guid? NurseId { get; set; }
        [ForeignKey("NurseId")]
        [JsonIgnore]
        public virtual User Nurse { get; set; }
        public DateTime? DoctorTime { get; set; }
        public Guid? DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        [JsonIgnore]
        public virtual User Doctor { get; set; }
        [JsonIgnore]
        public virtual ICollection<EIOExternalTransportationAssessmentData> EIOExternalTransportationAssessmentDatas { get; set; }
        [JsonIgnore]
        public virtual ICollection<EIOExternalTransportationAssessmentEquipment> EIOExternalTransportationAssessmentEquipments { get; set; }
        //[NotMapped]
        //public bool IsLocked { get; set; }
    }
}
