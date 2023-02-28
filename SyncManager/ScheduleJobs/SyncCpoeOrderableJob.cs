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
    public class SyncCpoeOrderableJob
    {
        protected IUnitOfWork unitOfWork = new EfUnitOfWork();
        protected CpoeOrderable GetOrCreateCpoeOrderable(CpoeOrderable item, out Service service)
        {
            try
            {
                var cpoeOrderable = unitOfWork.CpoeOrderableRepository.FirstOrDefault(e => e.CpoeOrderableId == item.CpoeOrderableId);
                if (cpoeOrderable != null)
                {
                    //if (true)
                    if (cpoeOrderable.LuUpdated < item.LuUpdated)
                    {
                        cpoeOrderable.LuUpdated = item.LuUpdated;
                        cpoeOrderable.PhGenericDrugId = item.PhGenericDrugId;
                        cpoeOrderable.ServiceCategoryRcd = item.ServiceCategoryRcd;
                        cpoeOrderable.LabOrderableRid = item.LabOrderableRid;
                        cpoeOrderable.GenericOrderableServiceCodeRid = item.GenericOrderableServiceCodeRid;
                        cpoeOrderable.RadiologyProcedurePlanRid = item.RadiologyProcedurePlanRid;
                        cpoeOrderable.PhPharmacyProductId = item.PhPharmacyProductId;
                        cpoeOrderable.PackageItemId = item.PackageItemId;
                        cpoeOrderable.CpoeOrderableTypeRcd = item.CpoeOrderableTypeRcd;
                        cpoeOrderable.OverrideNameE = item.OverrideNameE;
                        cpoeOrderable.OverrideNameL = item.OverrideNameL;
                        cpoeOrderable.EffectiveFromDateTime = item.EffectiveFromDateTime;
                        cpoeOrderable.EffectiveToDateTime = item.EffectiveToDateTime;
                        cpoeOrderable.FillerNameE = item.FillerNameE;
                        cpoeOrderable.FillerNameL = item.FillerNameL;
                        cpoeOrderable.SeqNum = item.SeqNum;
                        unitOfWork.CpoeOrderableRepository.Update(cpoeOrderable);
                        
                        service = updateServiceType(cpoeOrderable);
                        unitOfWork.Commit();
                        return cpoeOrderable;
                    } 
                    else
                    {
                        service = null;
                        return null;
                    }
                }

                unitOfWork.CpoeOrderableRepository.Add(item);
                service = updateServiceType(item);
                unitOfWork.Commit();
                return item;
            }
            catch (Exception ex)
            {
                CustomLogs.intervaljoblog.Info(string.Format("<Sync OH CpoeOrderable> Error: {0}", ex));
                service = null;
                return null;
            }
        }

        protected LabOrderableRef GetLabOrderableRef(LabOrderableRef item, out Service service)
        {
            try
            {
                var labOrderableRef = unitOfWork.LabOrderableRefRepository.FirstOrDefault(e => e.LabOrderableRid == item.LabOrderableRid);
                if (labOrderableRef != null)
                {
                    if (labOrderableRef.LuUpdated < item.LuUpdated)
                    //if (true)
                    {
                        labOrderableRef.LuUpdated = item.LuUpdated;
                        labOrderableRef.ItemId = item.ItemId;
                        labOrderableRef.LabSpecialProcessingGroupRcd = item.LabSpecialProcessingGroupRcd;
                        labOrderableRef.LabOrderableCode = item.LabOrderableCode;
                        labOrderableRef.ServiceCategoryRcd = item.ServiceCategoryRcd;
                        labOrderableRef.NameE = item.NameE;
                        labOrderableRef.NameL = item.NameL;
                        labOrderableRef.ActiveStatus = item.ActiveStatus;
                        unitOfWork.LabOrderableRefRepository.Update(labOrderableRef);
                        service = updateServiceType(labOrderableRef);
                        return labOrderableRef;
                    }
                    else
                    {
                        service = null;
                        return null;
                    }
                }

                unitOfWork.LabOrderableRefRepository.Add(item);
                service = updateServiceType(item);
                return item;
            }
            catch (Exception ex)
            {
                CustomLogs.intervaljoblog.Info(string.Format("<Sync OH LabOrderableRef> Error: {0}", ex));
                service = null;
                return null;
            }
        }

        protected Service updateServiceType(LabOrderableRef labOrderableRef)
        {
            var findItem = unitOfWork.ServiceRepository.AsQueryable().Where(i => i.HISId == labOrderableRef.ItemId).FirstOrDefault();
            var findCpoeOrderable = unitOfWork.CpoeOrderableRepository.AsQueryable().Where(c => c.LabOrderableRid == labOrderableRef.LabOrderableRid).FirstOrDefault();
            if(findItem != null)
            {
                if(labOrderableRef.ActiveStatus == "A" && findCpoeOrderable != null)
                {
                    findItem.ServiceType = Constants.ChargeItemType.Lab;
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

        protected Service updateServiceType(CpoeOrderable cpoeOrderable)
        {
            var findLabOrderable = unitOfWork.LabOrderableRefRepository.AsQueryable().Where(l => l.LabOrderableRid == cpoeOrderable.LabOrderableRid).FirstOrDefault();
            if(findLabOrderable != null)
            {
                var findItem = unitOfWork.ServiceRepository.AsQueryable().Where(i => i.HISId == findLabOrderable.ItemId).FirstOrDefault();
                if(findItem != null)
                {
                    if (findLabOrderable.ActiveStatus == "A")
                    {
                        findItem.ServiceType = Constants.ChargeItemType.Lab;
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
            else
            {
                return null;
            }
        }
    }

    public class SyncOHCpoeOrderableJob : SyncCpoeOrderableJob, IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            SyncCpoeOrderableJob();
            SyncLabOrderableRefJob();
        }

        public void Execute()
        {
            SyncCpoeOrderableJob();
            SyncLabOrderableRefJob();
            unitOfWork.Dispose();
        }

        private void SyncCpoeOrderableJob()
        {
            var lastRecord = unitOfWork.CpoeOrderableRepository.AsQueryable().OrderByDescending(s => s.LuUpdated).FirstOrDefault();
            DateTime last_updated = lastRecord != null ? lastRecord.LuUpdated : new DateTime(2012, 01, 01);
            //DateTime last_updated = new DateTime(2012, 01, 01);
            CustomLogs.intervaljoblog.Info($"<Sync OH Cpoe Orderable> Start!");
            try
            {
                var results = OHClient.GetCpoeOrderable(last_updated);
                CustomLogs.intervaljoblog.Info(string.Format("<Sync OH Service> Total item: {0}", results?.Count()));
                int countItem = 0;
                List<Guid> listServiceGroupIds = new List<Guid>();
                foreach (CpoeOrderable item in results)
                {
                    Service service;
                    var cpoeOrderable = GetOrCreateCpoeOrderable(item, out service);

                    if (cpoeOrderable != null)
                    {
                        countItem++;
                        CustomLogs.intervaljoblog.Info(string.Format("<Sync OH Cpoe Orderable> Info {0}: Name: {1}]", countItem, item.FillerNameL));
                    }
                    if (service != null && !listServiceGroupIds.Contains(service.ServiceGroupId.Value))
                    {
                        listServiceGroupIds.Add(service.ServiceGroupId.Value);
                    }
                }
                UpdateServiceGroupType(listServiceGroupIds);
                unitOfWork.Commit();
                CustomLogs.intervaljoblog.Info($"<Sync OH Orderable> Success!");
            }
            catch (Exception ex)
            {
                CustomLogs.intervaljoblog.Info(string.Format("<Sync OH Orderable> Error: {0}", ex));
            }
            unitOfWork.Dispose();
        }

        private void SyncLabOrderableRefJob()
        {
            var lastRecord = unitOfWork.LabOrderableRefRepository.AsQueryable().OrderByDescending(s => s.LuUpdated).FirstOrDefault();
            DateTime last_updated = lastRecord != null ? lastRecord.LuUpdated : new DateTime(2012, 01, 01);
            //last_updated = new DateTime(2012, 01, 01);
            CustomLogs.intervaljoblog.Info($"<Sync OH Lab Orderable Ref> Start!");
            try
            {
                var results = OHClient.GetLabOrderableRef(last_updated);
                CustomLogs.intervaljoblog.Info(string.Format("<Sync OH labOrderableRef> Total item: {0}", results?.Count()));
                int countItem = 0;
                List<Guid> listServiceGroupIds = new List<Guid>();
                foreach (LabOrderableRef item in results)
                {
                    Service service;
                    var labOrderableRef = GetLabOrderableRef(item, out service);

                    if (labOrderableRef != null)
                    {
                        countItem++;
                        CustomLogs.intervaljoblog.Info(string.Format("<Sync OH Lab Orderable Ref> Info {0}: Name: {1}]", countItem, item.NameL));
                    }
                    if(service != null && !listServiceGroupIds.Contains(service.ServiceGroupId.Value))
                    {
                        listServiceGroupIds.Add(service.ServiceGroupId.Value);
                    }
                }
                UpdateServiceGroupType(listServiceGroupIds);
                unitOfWork.Commit();
                CustomLogs.intervaljoblog.Info($"<Sync OH Lab Orderable Ref> Success!");
            }
            catch (Exception ex)
            {
                CustomLogs.intervaljoblog.Info(string.Format("<Sync OH Lab Orderable Ref> Error: {0}", ex));
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
