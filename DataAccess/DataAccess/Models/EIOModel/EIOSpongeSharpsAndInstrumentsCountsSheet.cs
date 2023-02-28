using DataAccess.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.EIOModel
{
    public class EIOSpongeSharpsAndInstrumentsCountsSheet: IEntity
    {
        public EIOSpongeSharpsAndInstrumentsCountsSheet()
        {
            this.SpongeSharpsAndInstrumentsCountsSheetDatas = new HashSet<EIOSpongeSharpsAndInstrumentsCountsSheetData>();
        }
        [Key]
        public Guid Id { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<DateTime> DateTimeSheet { get; set; }
        public string ScrubNurse { get; set; }
        public string CirculatingNurse { get; set; }
        public string Surgeon { get; set; }
        public Guid? VisitId { get; set; }
        public string VisitTypeGroupCode { get; set; }
        public virtual ICollection<EIOSpongeSharpsAndInstrumentsCountsSheetData> SpongeSharpsAndInstrumentsCountsSheetDatas { get; set; }
    }
}
