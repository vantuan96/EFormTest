using System;

namespace EForm.Models.IPDModels
{
    public class VitalSignsForPregnantWomanParams: PagingParameterModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public Guid IPDId { get; set; }
        public string Assessor { get; set; }
    }
}
