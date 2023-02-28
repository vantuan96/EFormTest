using DataAccess.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.EIOModel
{
    public class EIOSkinTestResult: IEntity
    {
        public EIOSkinTestResult()
        {
            this.EIOSkinTestResultDatas = new HashSet<EIOSkinTestResultData>();
        }
        public Guid Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string DeletedBy { get; set; }
        public Guid? VisitId { get; set; }
        public string VisitTypeGroupCode { get; set; }
        public string Conclusion { get; set; }
        public DateTime? ConfirmDate { get; set; }
        public Guid? ConfirmDoctorId { get; set; }
        [ForeignKey("ConfirmDoctorId")]
        public virtual User ConfirmDoctor { get; set; }
        public virtual ICollection<EIOSkinTestResultData> EIOSkinTestResultDatas { get; set; }
    }
}
