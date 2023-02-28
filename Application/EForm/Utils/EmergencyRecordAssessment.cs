using DataAccess.Repository;
using EForm.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EForm.Utils
{
    public class EmergencyRecordAssessment
    {
        private Guid? EmergencyRecordId;

        public EmergencyRecordAssessment(Guid emer_id)
        {
            this.EmergencyRecordId = emer_id;
        }

        public List<DataClinicalFinding> GetList()
        {
            using (IUnitOfWork unitOfWork = new EfUnitOfWork())
            {
                return (from data in unitOfWork.EmergencyRecordDataRepository.AsQueryable()
                        .Where(
                            i => !i.IsDeleted &&
                            i.EmergencyRecordId == this.EmergencyRecordId &&
                            !string.IsNullOrEmpty(i.Code) &&
                            Constant.ED_ER0_ASS_CODE.Contains(i.Code)
                        )
                        join master in unitOfWork.MasterDataRepository.AsQueryable() on data.Code equals master.Code into ulist
                        from master in ulist.DefaultIfEmpty()
                        select new { master.ViName, master.EnName, data.Value, master.Order, master.Code, master.DefaultValue })
                        .OrderBy(e => e.Order)
                        .Select(e => new DataClinicalFinding { ViName = e.ViName, EnName = e.EnName, Value = e.Value, Code = e.Code, CodeOther = e.DefaultValue })
                        .ToList();
            }
        }

        public string GetString()
        {
            var assess_lst = GetList();
            string result = string.Empty;
            foreach (var item in assess_lst)
                result += $" + {item.ViName}: \n{item.Value}\n";
            return result;
        }
    }
}