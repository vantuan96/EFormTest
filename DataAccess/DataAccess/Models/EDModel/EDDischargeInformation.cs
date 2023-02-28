using DataAccess.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.EDModel
{
    public class EDDischargeInformation : IEntity
    {
        public EDDischargeInformation()
        {
            this.DischargeInformationDatas = new HashSet<EDDischargeInformationData>();
        }
        [Key]
        public Guid Id { get; set; }
        public bool IsDeleted { get ; set ; }
        public string DeletedBy { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public Nullable<DateTime> AssessmentAt { get; set; }

        public virtual ICollection<EDDischargeInformationData> DischargeInformationDatas { get; set; }
    }
}
