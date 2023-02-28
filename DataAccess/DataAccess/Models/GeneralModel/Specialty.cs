using DataAccess.Model.BaseModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Specialty: IEntity
    {
        public Specialty()
        {
            this.RoleSpecialties = new HashSet<RoleSpecialty>();
        }
        [Key]
        public Guid Id { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }
        public string DeletedBy { get; set; }
        public string ViName { get; set; }
        public string EnName { get; set; }
        public string Code { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SpecialtyNo { get; set; }
        public bool IsPublish { get; set; }
        public Nullable<Guid> SiteId { get; set; }
        [ForeignKey("SiteId")]
        public virtual Site Site { get; set; }
        public Nullable<Guid> VisitTypeGroupId { get; set; }
        [ForeignKey("VisitTypeGroupId")]
        public virtual VisitTypeGroup VisitTypeGroup { get; set; }
        [JsonIgnore]
        public virtual ICollection<RoleSpecialty> RoleSpecialties { get; set; }
        public string LocationCode { get; set; }
        public bool IsAnesthesia { get; set; }
    }
}
