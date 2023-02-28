using DataAccess.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.IPDModel
{
    public class IPDMonitoringSheetForPatientsWithExtravasationOfCancerDrugs: VBaseModel
    {
        public Guid? VisitId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Order { get; set; }

    }
}
