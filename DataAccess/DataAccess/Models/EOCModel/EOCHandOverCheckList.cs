using DataAccess.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.EOCModel
{
    public class EOCHandOverCheckList: EOCBase
    {
        public string ReasonForTransfer { get; set; }
        public Nullable<DateTime> HandOverTimePhysician { get; set; }
        public Guid? HandOverPhysicianId { get; set; }
        [ForeignKey("HandOverPhysicianId")]
        public virtual User HandOverPhysician { get; set; }
        public Guid? HandOverUnitPhysicianId { get; set; }
        [ForeignKey("HandOverUnitPhysicianId")]
        public virtual Specialty HandOverUnitPhysician { get; set; }
        public Guid? ReceivingPhysicianId { get; set; }
        [ForeignKey("ReceivingPhysicianId")]
        public virtual User ReceivingPhysician { get; set; }
        public Guid? ReceivingUnitPhysicianId { get; set; }
        [ForeignKey("ReceivingUnitPhysicianId")]
        public virtual Specialty ReceivingUnitPhysician { get; set; }
        public bool IsAcceptPhysician { get; set; }
        public Nullable<DateTime> HandOverTimeNurse { get; set; }
        public Guid? HandOverNurseId { get; set; }
        [ForeignKey("HandOverNurseId")]
        public virtual User HandOverNurse { get; set; }
        public Guid? HandOverUnitNurseId { get; set; }
        [ForeignKey("HandOverUnitNurseId")]
        public virtual Specialty HandOverUnitNurse { get; set; }
        public Guid? ReceivingNurseId { get; set; }
        [ForeignKey("ReceivingNurseId")]
        public virtual User ReceivingNurse { get; set; }
        public Guid? ReceivingUnitNurseId { get; set; }
        [ForeignKey("ReceivingUnitNurseId")]
        public virtual Specialty ReceivingUnitNurse { get; set; }
        public bool IsAcceptNurse { get; set; }
        public bool IsUseHandOverCheckList { get; set; } = true; // Có sử dụng Biên bản bàn giao người bệnh chuyển khoa hay không
        public DateTime? NurseAcceptTime { get; set; } // thời gian xác nhận của điều dưỡng (chính là thời gian CreatedAt của visit)
        public DateTime? PhysicianAcceptTime { get; set; } // thời gian bác sĩ Xác nhận bệnh nhân chuyển khoa
    }
}
