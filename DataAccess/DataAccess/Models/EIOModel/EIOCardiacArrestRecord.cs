using DataAccess.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.EIOModel
{
    public class EIOCardiacArrestRecord : IEntity
    {
        public EIOCardiacArrestRecord()
        {
            this.EIOCardiacArrestRecordTables = new HashSet<EIOCardiacArrestRecordTable>();
            this.EIOCardiacArrestRecordDatas = new HashSet<EIOCardiacArrestRecordData>();
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
        public string Note { get; set; }
        public int Version { get; set; }
        public DateTime? DoctorComfirm { get; set; }
        public Guid? DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public virtual User Doctor { get; set; }
        public DateTime? TeamLeaderTime  { get; set; }
        public Guid? TeamLeaderId { get; set; }
        [ForeignKey("TeamLeaderId")]
        public virtual User TeamLeader { get; set; }
        public DateTime? FormCompletedTime { get; set; }
        public Guid? FormCompletedId { get; set; }
        [ForeignKey("FormCompletedId")]
        public virtual User FormCompleted { get; set; }
        public virtual ICollection<EIOCardiacArrestRecordData> EIOCardiacArrestRecordDatas { get; set; }
        public virtual ICollection<EIOCardiacArrestRecordTable> EIOCardiacArrestRecordTables { get; set; }
    }
}
