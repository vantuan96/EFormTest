using Common;
using DataAccess.Models.GeneralModel;
using DataAccess.Repository;
using EForm.Client;
using EForm.Models;
using Quartz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncManager.ScheduleJobs
{
    public class SyncServiceJob
    {
        protected IUnitOfWork unitOfWork = new EfUnitOfWork();
        protected Service GetOrCreateService(HISServiceModel item, ServiceGroup group)
        {
            try
            {
                var service_group = GetAppConfig("SERVICE_GROUP_DIAGNOSTICREPORTING");
                var service_code = GetAppConfig("SERVICE_CODE_DIAGNOSTICREPORTING");

                List<string> service_groups = service_group.Split(',').ToList();
                List<string> service_codes = service_code.Split(',').ToList();

                var service = unitOfWork.ServiceRepository.FirstOrDefault(e => e.Code == item.ServiceCode);
                
                var serviceType = DetectItemType(item);
                if(group == null)
                {
                    CustomLogs.intervaljoblog.Info(string.Format("<Sync OH Service> Group not found: {0}", item.ServiceGroupId.ToString()));
                }
                var groupIds = group != null && !string.IsNullOrEmpty(group.KeyStruct) ? group.KeyStruct.Split('\\').ToList() : null;
                var rootServiceGroupCode = string.Empty;
                if(groupIds != null)
                {
                    var listGroups = unitOfWork.ServiceGroupRepository.AsQueryable().Where(g => groupIds.Contains(g.HISId.ToString())).OrderBy(g => g.Code).ToList();
                    rootServiceGroupCode = string.Join("\\", listGroups.Select(g => g.Code).ToList()) + "\\" + group.Code;
                }
                if (service != null)
                {
                    if (service.HISLastUpdated < item.HISLastUpdated)
                    //if (true)
                    {
                        service.HISId = item.ServiceId;
                        service.HISCode = item.HISCode;
                        service.Code = item.ServiceCode;
                        service.ViName = item.ServiceViName;
                        service.EnName = item.ServiceEnName;
                        service.IsActive = item.IsActive;
                        service.HISLastUpdated = item.HISLastUpdated;
                        service.CombinedName = item.ServiceCode + "-" + item.ServiceViName + "-" + item.ServiceEnName;
                        service.RootServiceGroupCode = rootServiceGroupCode;
                        service.ServiceGroupId = group?.Id;
                        service.ServiceType = serviceType;
                        service.IsDiagnosticReporting = service_groups.Contains(group?.Code) || service_codes.Contains(item.ServiceCode) || rootServiceGroupCode.StartsWith("F\\FE");
                        unitOfWork.ServiceRepository.Update(service);
                        unitOfWork.Commit();
                        return service;
                    } 
                    else
                    {
                        return null;
                    }
                }
                
                service = new Service
                {
                    HISId = item.ServiceId,
                    Type = item.ServiceType,
                    Code = item.ServiceCode,
                    ViName = item.ServiceViName,
                    EnName = item.ServiceEnName,
                    HISCode = item.HISCode,
                    ServiceGroupId = group.Id,
                    HISLastUpdated = item.HISLastUpdated,
                    CombinedName = item.ServiceCode + "-" + item.ServiceViName + "-" + item.ServiceEnName,
                    RootServiceGroupCode = rootServiceGroupCode,
                    IsActive = item.IsActive,
                    IsDiagnosticReporting = service_groups.Contains(group.Code) || service_codes.Contains(item.ServiceCode) || rootServiceGroupCode.StartsWith("F\\FE"),
                    ServiceType = serviceType
                };
                unitOfWork.ServiceRepository.Add(service);
                unitOfWork.Commit();
                return service;

            }
            catch (Exception ex)
            {
                CustomLogs.intervaljoblog.Info(string.Format("<Sync OH Service> Error: {0}", ex));
                return null;
            }

        }

        protected string DetectItemType(HISServiceModel item)
        {
            //Check if Service is Rad
            var radiology = unitOfWork.RadiologyProcedurePlanRefRepository.Find(e => e.ActiveStatus == "A" && e.ShortCode.StartsWith(item.ServiceCode)).ToList();
            if(radiology.Count > 0)
            {
                return Constants.ChargeItemType.Rad;
            } else
            {
                //check if service is Lab
                var query = (from sql in unitOfWork.LabOrderableRefRepository.AsQueryable().Where(
                       e => !e.IsDeleted && e.ActiveStatus == "A" && e.ItemId == item.ServiceId
                       )
                             join cpoeOrderables_tbl in unitOfWork.CpoeOrderableRepository.AsQueryable()
                             on sql.LabOrderableRid equals cpoeOrderables_tbl.LabOrderableRid

                             select new CpoeOrderableModel
                             {
                                 CpoeOrderableId = cpoeOrderables_tbl.CpoeOrderableId

                             });
                if(query.FirstOrDefault() != null)
                {
                    return Constants.ChargeItemType.Lab;
                }
                else
                {
                    return Constants.ChargeItemType.Allies;
                }
            }
        }
        protected void UpdateServiceGroupType(List<ServiceGroup> listSVGroup)
        {
            if(listSVGroup.Count > 0)
            {
                foreach(ServiceGroup group in listSVGroup)
                {
                    var serviceType = unitOfWork.ServiceRepository.AsQueryable().Where(s => s.IsActive && !s.IsDeleted & s.ServiceGroupId == group.Id).Select(s => s.ServiceType).Distinct().ToList();
                    if(serviceType.Count > 0)
                    {
                        group.ServiceType = string.Join(";", serviceType);
                        unitOfWork.ServiceGroupRepository.Update(group);
                    }
                }
            }
        }
        protected ServiceGroup GetOrCreateServiceGroup(HISServiceGroupModel item)
        {
            var group = unitOfWork.ServiceGroupRepository.FirstOrDefault(e => e.HISId == item.ServiceGroupId);
            if (group != null)
            {
                if(group.HISLastUpdated < item.HISLastUpdated)
                {
                    group.HISParentId = item.ParentServiceGroupId;
                    group.HISId = item.ServiceGroupId;
                    group.Code = item.ServiceGroupCode;
                    group.ViName = item.ServiceGroupViName;
                    group.EnName = item.ServiceGroupEnName;
                    group.IsActive = item.IsActive;
                    group.KeyStruct = item.KeyStruct;
                    group.HISLastUpdated = item.HISLastUpdated;
                    group.Type = item.ServiceType;
                    unitOfWork.ServiceGroupRepository.Update(group);
                    unitOfWork.Commit();
                    return group;
                }
                else
                {
                    return null;
                }
            }
                

            group = new ServiceGroup
            {
                HISParentId = item.ParentServiceGroupId,
                HISId = item.ServiceGroupId,
                Code = item.ServiceGroupCode,
                ViName = item.ServiceGroupViName,
                EnName = item.ServiceGroupEnName,
                IsActive = item.IsActive,
                KeyStruct = item.KeyStruct,
                HISLastUpdated = item.HISLastUpdated,
                Type = item.ServiceType
            };
            unitOfWork.ServiceGroupRepository.Add(group);
            unitOfWork.Commit();
            return group;
        }
        private string GetAppConfig(string key)
        {
            var config_in_db = unitOfWork.AppConfigRepository.Find(e => !e.IsDeleted && e.Key == key).FirstOrDefault();
            var isTest = (config_in_db != null && !string.IsNullOrEmpty(config_in_db?.Value));
            var config = ConfigurationManager.AppSettings[key]?.ToString();
            return isTest ? config_in_db?.Value : (string.IsNullOrEmpty(config) ? "" : config);
        }
    }

    public class SyncOHServiceJob : SyncServiceJob, IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            DoJob();
        }

        public void Execute()
        {
            DoJob();
        }

        private void DoJob()
        {
            CustomLogs.intervaljoblog.Info($"<Sync OH Service> Start!");
            if (ConfigHelper.CF_SyncOHService_CS_is_off)
            {
                CustomLogs.intervaljoblog.Info($"<Sync OH Service> is Off!");
                return;
            }

            var lastService = unitOfWork.ServiceRepository.AsQueryable().OrderByDescending(s => s.HISLastUpdated).FirstOrDefault();
            var lastServiceGroup = unitOfWork.ServiceGroupRepository.AsQueryable().OrderByDescending(s => s.HISLastUpdated).FirstOrDefault();
            DateTime last_updated_service_group = lastServiceGroup != null && lastServiceGroup.HISLastUpdated  != null ? lastServiceGroup.HISLastUpdated.Value : new DateTime(2012, 01, 01);
            DateTime last_updated_service = lastService != null ? lastService.HISLastUpdated : new DateTime(2012, 01, 01);
            
            DateTime now = DateTime.Now;
            try
            {

                SyncOHCpoeOrderableJob objLab = new SyncOHCpoeOrderableJob();
                objLab.Execute();

                SyncOHRadiologyProcedureJob objRad = new SyncOHRadiologyProcedureJob();
                objRad.Execute();

                unitOfWork.Commit();

                var resultServiceGroups = OHClient.GetServiceGroup(last_updated_service_group);
                var results = OHClient.GetService(last_updated_service, now);
                CustomLogs.intervaljoblog.Info(string.Format("<Sync OH Service> Total item: {0}", results?.Count()));
                int countItem = 0;
                List<ServiceGroup> listSVGroup = new List<ServiceGroup>();
                List<Guid> listSVGroupId = new List<Guid>();
                foreach (var g in resultServiceGroups)
                {
                    GetOrCreateServiceGroup(g);
                }
                foreach (HISServiceModel item in results)
                {
                    var group = unitOfWork.ServiceGroupRepository.FirstOrDefault(e => e.HISId == item.ServiceGroupId);
                    var service = GetOrCreateService(item, group);
                    if (service != null)
                    {
                        if(service.ServiceGroupId.HasValue && !listSVGroupId.Contains(service.ServiceGroupId.Value))
                        {
                            listSVGroupId.Add(service.ServiceGroupId.Value);
                            listSVGroup.Add(group);
                        }
                        countItem++;
                        CustomLogs.intervaljoblog.Info(string.Format("<Sync OH Service> Info {0}: [Code: {1} - Name: {2}]", countItem, item.ServiceCode, item.ServiceEnName));
                    }
                }
                UpdateServiceGroupType(listSVGroup);
                unitOfWork.Commit();
                CustomLogs.intervaljoblog.Info($"<Sync OH Service> Success!");
            }
            catch (Exception ex)
            {
                CustomLogs.intervaljoblog.Info(string.Format("<Sync OH Service> Error: {0}", ex));
                unitOfWork.Dispose();
            }
            unitOfWork.Dispose();
        }
    }
}
