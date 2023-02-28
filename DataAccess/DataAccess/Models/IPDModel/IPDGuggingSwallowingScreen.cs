using DataAccess.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.IPDModel
{
    public class IPDGuggingSwallowingScreen : IEntity
    {
        public IPDGuggingSwallowingScreen()
        {
            this.IPDGuggingSwallowingScreenDatas = new HashSet<IPDGuggingSwallowingScreenData>();
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
        public Guid? IPDId { get; set; }
        [ForeignKey("IPDId")]
        public virtual IPD IPD { get; set; }
        public virtual ICollection<IPDGuggingSwallowingScreenData> IPDGuggingSwallowingScreenDatas { get; set; }
    }
}
