using DataAccess.Model.BaseModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.IPDModel
{
    public class IPDSurgeryCertificate : IEntity
    {
        public IPDSurgeryCertificate()
        {
            this.IPDSurgeryCertificateDatas = new HashSet<IPDSurgeryCertificateData>();
;       }
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? ProcedureTime { get; set; }
        public Guid? ProcedureDoctorId { get; set; }
        [ForeignKey("ProcedureDoctorId")]
        public virtual User ProcedureDoctor { get; set; } //Phẫu thuật viên xác nhận

        public DateTime? DeanConfirmTime { get; set; }
        public Guid? DeanId { get; set; }
        [ForeignKey("DeanId")]
        public virtual User Dean { get; set; } //Trưởng khoa xác nhận

        public DateTime? DirectorTime { get; set; }
        public Guid? DirectorId { get; set; }
        [ForeignKey("DirectorId")]
        public virtual User Director { get; set; } //Giám đốc xác nhận

        public Guid? VisitId { get; set; }
        public string VisitTypeGroupCode { get; set; }

        [JsonIgnore]
        public virtual ICollection<IPDSurgeryCertificateData> IPDSurgeryCertificateDatas { get; set; }
        public Guid? FormId { get; set; }
        public string Version { get; set; }
    }
}
