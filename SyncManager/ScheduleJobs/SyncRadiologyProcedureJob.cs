using Common;
using DataAccess.Models.GeneralModel;
using DataAccess.Repository;
using EForm.Client;
using EForm.Models;
using Quartz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncManager.ScheduleJobs
{
    public class SyncRadiologyProcedureJob
    {
        protected IUnitOfWork unitOfWork = new EfUnitOfWork();
        protected RadiologyProcedurePlanRef GetOrCreateRadiologyProcedurePlanRef(RadiologyProcedurePlanRef item, out Service service)
        {
            try
            {
                var objRadiologyProcedure = unitOfWork.RadiologyProcedurePlanRefRepository.FirstOrDefault(e => e.RadiologyProcedurePlanRid == item.RadiologyProcedurePlanRid);
                if (objRadiologyProcedure != null)
                {
                    if(objRadiologyProcedure.LuUpdated < item.LuUpdated)
                    //if (true)
                    {
                        objRadiologyProcedure.LuUpdated = item.LuUpdated;
                        objRadiologyProcedure.ShortCode = item.ShortCode;
                        objRadiologyProcedure.RadiologyProcedureNameE = item.RadiologyProcedureNameE;
                        objRadiologyProcedure.RadiologyProcedureNameL = item.RadiologyProcedureNameL;
                        objRadiologyProcedure.ActiveStatus = item.ActiveStatus;
                        objRadiologyProcedure.ServiceCategoryCode = item.ServiceCategoryCode;
                        objRadiologyProcedure.DicomModality = item.DicomModality;
                        objRadiologyProcedure.ServiceCategoryNameL = item.ServiceCategoryNameL;
                        objRadiologyProcedure.ServiceCategoryNameE = item.ServiceCategoryNameE;
                        unitOfWork.RadiologyProcedurePlanRefRepository.Update(objRadiologyProcedure);
                        service = updateServiceType(objRadiologyProcedure);
                        return objRadiologyProcedure;
                    } 
                    else
                    {
                        service = null;
                        return null;
                    }
                }

                unitOfWork.RadiologyProcedurePlanRefRepository.Add(item);
                service = updateServiceType(item);
                return item;
            }
            catch (Exception ex)
            {
                CustomLogs.intervaljoblog.Info(string.Format("<Sync OH RadiologyProcedurePlanRefRepository> Error: {0}, {1}", ex, item.ShortCode));
                service = null;
                return null;
            }
        }
        protected Service updateServiceType(RadiologyProcedurePlanRef objRadiologyProcedure)
        {
            var findItem = unitOfWork.ServiceRepository.AsQueryable().Where(i => objRadiologyProcedure.ShortCode.StartsWith(i.Code)).FirstOrDefault();
            if (findItem != null)
            {
                if (objRadiologyProcedure.ActiveStatus == "A")
                {
                    findItem.ServiceType = Constants.ChargeItemType.Rad;
                }
                else
                {
                    findItem.ServiceType = Constants.ChargeItemType.Allies;
                }
                unitOfWork.ServiceRepository.Update(findItem);
                return findItem;
            }
            else
            {
                return null;
            }
        }
    }

    public class SyncOHRadiologyProcedureJob : SyncRadiologyProcedureJob, IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            SyncRadiologyProcedureJob();
        }

        public void Execute()
        {
            SyncRadiologyProcedureJob();
            unitOfWork.Dispose();
        }

        private void SyncRadiologyProcedureJob()
        {
            var lastRecord = unitOfWork.RadiologyProcedurePlanRefRepository.AsQueryable().OrderByDescending(s => s.LuUpdated).FirstOrDefault();
            DateTime last_updated = lastRecord != null ? lastRecord.LuUpdated : new DateTime(2012, 01, 01);
            //last_updated = new DateTime(2012, 01, 01);
            CustomLogs.intervaljoblog.Info($"<Sync OH Radiology Procedure> Start!");
            try
            {
                var results = OHClient.GetRadiologyProcedure(last_updated);
                CustomLogs.intervaljoblog.Info(string.Format("<Sync OH Radiology Procedure> Total item: {0}", results?.Count()));
                int countItem = 0;
                List<Guid> listServiceGroupIds = new List<Guid>();
                foreach (RadiologyProcedurePlanRef item in results)
                {
                    Service service;
                    var radiologyProcedure = GetOrCreateRadiologyProcedurePlanRef(item, out service);

                    if (radiologyProcedure != null)
                    {
                        countItem++;
                        CustomLogs.intervaljoblog.Info(string.Format("<Sync OH radiology Procedure> Info {0}: Name: {1}]", countItem, item.RadiologyProcedureNameL));
                    }
                    if (service != null && !listServiceGroupIds.Contains(service.ServiceGroupId.Value))
                    {
                        listServiceGroupIds.Add(service.ServiceGroupId.Value);
                    }
                }
                
                UpdateServiceGroupType(listServiceGroupIds);
                unitOfWork.Commit();
                CustomLogs.intervaljoblog.Info($"<Sync Radiology Procedure> Success!");
            }
            catch (Exception ex)
            {
                CustomLogs.intervaljoblog.Info(string.Format("<Sync Radiology Procedure> Error: {0}", ex));
            }
        }

        private void UpdateServiceGroupType(List<Guid> listServiceGroupIds)
        {
            var listSVGroup = unitOfWork.ServiceGroupRepository.AsQueryable().Where(s => listServiceGroupIds.Contains(s.Id)).ToList();
            if (listSVGroup.Count > 0)
            {
                foreach (ServiceGroup group in listSVGroup)
                {
                    var serviceType = unitOfWork.ServiceRepository.AsQueryable().Where(s => s.IsActive && !s.IsDeleted & s.ServiceGroupId == group.Id).Select(s => s.ServiceType).Distinct().ToList();
                    if (serviceType.Count > 0)
                    {
                        group.ServiceType = string.Join(";", serviceType);
                        unitOfWork.ServiceGroupRepository.Update(group);
                    }
                }
            }
        }
    }
}
