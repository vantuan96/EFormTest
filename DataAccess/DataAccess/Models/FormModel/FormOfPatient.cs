using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.FormModel
{
    public class FormOfPatient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string ViName { get; set; }
        [Required]
        public string EnName { get; set; }
        [Required]
        public string TypeName { get; set; }
        [Required]
        public string Area { get; set; }
        public string ViStatusPatient { get; set; }
        public string EnStatusPatient { get; set; }
        public int Order { get; set; }
    }
}
