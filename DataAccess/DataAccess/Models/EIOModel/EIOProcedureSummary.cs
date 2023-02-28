using DataAccess.Model.BaseModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.EIOModel
{
    public class EIOProcedureSummary : IEntity
    {
        public EIOProcedureSummary()
        {
            this.EIOProcedureSummaryDatas = new HashSet<EIOProcedureSummaryData>();
        }
        [Key]
        public Guid Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? ProcedureTime { get; set; }
        public Guid? ProcedureDoctorId { get; set; }
        [ForeignKey("ProcedureDoctorId")]
        [JsonIgnore]
        public virtual User ProcedureDoctor { get; set; }
        public DateTime? HeadOfDepartmentTime { get; set; }
        public Guid? HeadOfDepartmentId { get; set; }
        [ForeignKey("HeadOfDepartmentId")]
        [JsonIgnore]
        public virtual User HeadOfDepartment { get; set; }
        public DateTime? DirectorTime { get; set; }
        public Guid? DirectorId { get; set; }
        [ForeignKey("DirectorId")]
        [JsonIgnore]
        public virtual User Director { get; set; }
        public Guid? VisitId { get; set; }
        public string VisitTypeGroupCode { get; set; }
        [JsonIgnore]
        public virtual ICollection<EIOProcedureSummaryData> EIOProcedureSummaryDatas { get; set; }
        public string Version { get; set; } //Version của Phiếu phẫu thuật/thủ thuật
    }
}
