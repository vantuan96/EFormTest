using DataAccess.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.IPDModel
{
    public class IPDPainRecord : VBaseModel
    {
        public Guid? VisitId { get; set; }
        public string DataType { get; set; }
       
    }
}
