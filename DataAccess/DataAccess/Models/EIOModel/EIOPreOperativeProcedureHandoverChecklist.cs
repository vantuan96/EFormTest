using DataAccess.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.EIOModel
{
    public class EIOPreOperativeProcedureHandoverChecklist:IEntity 
    {
        public EIOPreOperativeProcedureHandoverChecklist()
        {
            this.PreOperativeProcedureHandoverChecklistDatas = new HashSet<EIOPreOperativeProcedureHandoverChecklistData>();
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
        public Nullable<DateTime> DateTimeHandover { get; set; }
        public string WardNurse { get; set; }
        public string EscortNurse { get; set; }
        public string ReceivingNurse { get; set; }
        public Guid? VisitId { get; set; }
        public string VisitTypeGroupCode { get; set; }
        public virtual ICollection<EIOPreOperativeProcedureHandoverChecklistData> PreOperativeProcedureHandoverChecklistDatas { get; set; }
    }
}
