using DataAccess.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.EIOModel
{
    public class EIOSurgicalProcedureSafetyChecklist: IEntity
    {
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
        public Guid? EIOSurgicalProcedureSafetyChecklistSignInId { get; set; }
        [ForeignKey("EIOSurgicalProcedureSafetyChecklistSignInId")]
        public virtual EIOSurgicalProcedureSafetyChecklistSignIn EIOSurgicalProcedureSafetyChecklistSignIn { get; set; }
        public Guid? EIOSurgicalProcedureSafetyChecklistTimeOutId { get; set; }
        [ForeignKey("EIOSurgicalProcedureSafetyChecklistTimeOutId")]
        public virtual EIOSurgicalProcedureSafetyChecklistTimeOut EIOSurgicalProcedureSafetyChecklistTimeOut { get; set; }
        public Guid? EIOSurgicalProcedureSafetyChecklistSignOutId { get; set; }
        [ForeignKey("EIOSurgicalProcedureSafetyChecklistSignOutId")]
        public virtual EIOSurgicalProcedureSafetyChecklistSignOut EIOSurgicalProcedureSafetyChecklistSignOut { get; set; }
    }
}
