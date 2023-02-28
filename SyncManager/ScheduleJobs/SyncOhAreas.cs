using Bussiness.HisService;
using Clients.HisClient;
using Common;
using DataAccess.Models;
using DataAccess.Repository;
using EMRModels;
using Quartz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;


namespace SyncManager.ScheduleJobs
{
    public class SyncOhAreas : IJob
    {
        protected IUnitOfWork unitOfWork = new EfUnitOfWork();
        protected string OH_MD_CODE = "OHAREA";
        public void Execute(IJobExecutionContext context)
        {
            _ = DoJobAsync();
        }

        public void Execute()
        {
            _ = DoJobAsync();
        }

        private async Task DoJobAsync()
        {
            CustomLogs.intervaljoblog.Info($"<SyncOhAreas> Start!");
            if (ConfigHelper.CF_SyncOHArea_is_off)
            {
                CustomLogs.intervaljoblog.Info($"<SyncOhAreas> is Off!");
                return;
            }
            try
            {
                var all_item = unitOfWork.MasterDataRepository.Find(e => !e.IsDeleted && e.Form == OH_MD_CODE).OrderByDescending(s => s.UpdatedAt).ToList();
                var first_item = all_item.FirstOrDefault();
                var is_first = first_item == null;
                var from = first_item != null ? (DateTime)first_item.UpdatedAt : DateTime.ParseExact("17:30 13/10/2010", Constants.TIME_DATE_FORMAT_WITHOUT_SECOND, null);

                var to = DateTime.Now;

                var resultOhAreas = await OHAPIService.SyncOHAreaAsync(from.ToString(Constants.DATE_SQL), to.ToString(Constants.DATE_SQL));

                var ind = 0;
                foreach (var oh_area in resultOhAreas)
                {
                    var group_item = getOrCreateGroup(oh_area, all_item, to, ind);
                    var chil_item = getOrCreateChilItem(group_item.Code, oh_area, all_item, to, ind, is_first);
                    ind++;
                }
                unitOfWork.Commit();
                CustomLogs.intervaljoblog.Info($"<SyncOhAreas> Success!");
            }
            catch (Exception ex)
            {
                CustomLogs.intervaljoblog.Info(string.Format("<SyncOhAreas> Error: {0}", ex));
            }
        }

        private MasterData getOrCreateChilItem(string code, SyncAreasModel oh_area, List<MasterData> all_item, DateTime to, int ind, bool is_first)
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
                if (is_first) updateDiagnosticReporting(oh_area.area_code, oh_area.area_id);
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
        private void updateDiagnosticReporting(string area_code, string area_id)
        {
            var dataof_DiagnosticReporting = unitOfWork.DiagnosticReportingRepository.Find(e => e.Area == area_code).ToList();
            foreach (var dataof in dataof_DiagnosticReporting)
            {
                dataof.Area = area_id;
            }
            unitOfWork.Commit();
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
            } else
            {
                exit_item.ViName = oh_area.CostCentreNameV;
                exit_item.EnName = oh_area.CostCentreNameE;
                exit_item.UpdatedAt = to;
            }
            return exit_item;
        }
    }
}