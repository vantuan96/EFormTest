using DataAccess.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.IPDModel
{
    public class IPDSurgeryCertificateData : IEntity
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

        public string Code { get; set; }
        public string Value { get; set; }
        public string EnValue { get; set; }
        public Guid? IPDSurgeryCertificateId { get; set; }
        [ForeignKey("IPDSurgeryCertificateId")]
        public virtual IPDSurgeryCertificate IPDSurgeryCertificate { get; set; }
    }
}
