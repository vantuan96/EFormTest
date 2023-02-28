using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Sync
{
    public class OHArea
    {
        protected IUnitOfWork unitOfWork = new EfUnitOfWork();
        protected string OH_MD_CODE = "OHAREA";
        public void RunSync()
        {
            var all_item = unitOfWork.MasterDataRepository.Find(e => !e.IsDeleted && e.Form == OH_MD_CODE).OrderByDescending(s => s.UpdatedAt).ToList();
            var first_item = all_item.FirstOrDefault();

            var from = first_item != null ? (DateTime)first_item.UpdatedAt : DateTime.ParseExact("17:30 13/10/2010", Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);

            var to = DateTime.Now;

            var resultOhAreas = await OHClient.SyncOHAreaAsync(from.ToString(Constant.DATE_SQL), to.ToString(Constant.DATE_SQL));


            var ind = 0;
            foreach (var oh_area in resultOhAreas)
            {
                var group_item = getOrCreateGroup(oh_area, all_item, to, ind);
                var chil_item = getOrCreateChilItem(group_item.Code, oh_area, all_item, to, ind);
                ind++;
            }
            unitOfWork.Commit();
        }
        private MasterData getOrCreateChilItem(string code, SyncAreasModel oh_area, List<MasterData> all_item, DateTime to, int ind)
        {
            var exit_item = all_item.FirstOrDefault(e => e.Code == oh_area.area_id && e.Level == 2);
            if (exit_item == null)
            {
                exit_item = new MasterData()
                {
                    Code = oh_area.area_id,
                    ViName = oh_area.areaNameV.Trim(),
                    EnName = oh_area.areaNameE.Trim(),
                    CreatedAt = to,
                    UpdatedAt = to,
                    Form = OH_MD_CODE,
                    Group = code,
                    Clinic = oh_area.SiteCode.Trim(),
                    Order = ind,
                    Level = 2,
                    DataType = oh_area.Type,
                    Note = "",
                    IsReadOnly = true,
                    Data = oh_area.area_code.Trim(),
                    Version = "1"
                };
                unitOfWork.MasterDataRepository.Add(exit_item);
            }
            else
            {
                exit_item.ViName = oh_area.CostCentreNameV;
                exit_item.EnName = oh_area.CostCentreNameE;
                exit_item.UpdatedAt = to;
            }
            return exit_item;
        }

        private MasterData getOrCreateGroup(SyncAreasModel oh_area, List<MasterData> all_item, DateTime? to, int ind)
        {
            var exit_item = all_item.FirstOrDefault(e => e.Code == oh_area.costcentre_code && e.Level == 1);

            if (exit_item == null)
            {
                exit_item = new MasterData()
                {
                    Code = oh_area.costcentre_code.Trim(),
                    ViName = oh_area.CostCentreNameV.Trim(),
                    EnName = oh_area.CostCentreNameE.Trim(),
                    CreatedAt = to,
                    UpdatedAt = to,
                    Form = OH_MD_CODE,
                    Group = OH_MD_CODE,
                    Clinic = oh_area.SiteCode.Trim(),
                    Order = ind,
                    Level = 1,
                    DataType = oh_area.Type,
                    Note = "",
                    IsReadOnly = true,
                    Data = oh_area.costcentre_code.Trim(),
                    Version = "1"
                };
                unitOfWork.MasterDataRepository.Add(exit_item);
            }
            else
            {
                exit_item.ViName = oh_area.CostCentreNameV;
                exit_item.EnName = oh_area.CostCentreNameE;
                exit_item.UpdatedAt = to;
            }
            return exit_item;
        }
    }
}
