using DataAccess.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.GeneralModel
{
    public class AppVersion : VBaseModel
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Order { get; set; }
        [Required]
        public string Lable { get; set; }
        [Required]
        public string Description { get; set; }
        public int Version { get; set; }
    }
}
