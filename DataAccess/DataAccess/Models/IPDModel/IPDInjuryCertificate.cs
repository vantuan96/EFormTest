using DataAccess.Model.BaseModel;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.IPDModel
{
    public class IPDInjuryCertificate : IEntity
    {
        public Guid Id { get; set; }

        [MaxLength(10)]
        public string PID { get; set; }

        public Guid? VisitId { get; set; }

        public DateTime? DoctorTime { get; set; }

        public Guid? DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        [JsonIgnore]
        public virtual User Doctor { get; set; }

        public DateTime? HeadOfDeptTime { get; set; }

        public Guid? HeadOfDeptId { get; set; }
        [ForeignKey("HeadOfDeptId")]
        [JsonIgnore]
        public virtual User HeadOfDept { get; set; }

        public DateTime? DirectorTime { get; set; }

        public Guid? DirectorId { get; set; }
        [ForeignKey("DirectorId")]
        [JsonIgnore]
        public virtual User Director { get; set; }

        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }

    }
}
