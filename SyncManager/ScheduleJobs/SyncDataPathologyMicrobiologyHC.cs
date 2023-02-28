using Common;
using DataAccess.Models;
using DataAccess.Models.GeneralModel;
using DataAccess.Repository;
using EForm.Client;
using EForm.Models;
using EMRModels;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SyncManager.ScheduleJobs
{
    public class SyncDataPathologyMicrobiologyHC : IJob
    {
        protected IUnitOfWork unitOfWork = new EfUnitOfWork();
        protected string VMHC_CODE = Constants.VMHC_CODE;
        public void Execute(IJobExecutionContext context)
        {
            DoJob();
        }
        public void Execute()
        {
            DoJob();
        }
        public void DoJob()
        {

            CustomLogs.intervaljoblog.Info($"<SyncDataPathologyMicrobiologyHC> Start!");
            if (ConfigHelper.CF_SyncOHHCPathologyMicrobiologyService_CS_is_off)
            {
                CustomLogs.intervaljoblog.Info($"<SyncDataPathologyMicrobiologyHC> is Off!");
                return;
            }
            try
            {
                int MinuteOfJobSyncDataPathologyMicrobiologyHC = ConfigurationManager.AppSettings["MinuteOfJobSyncDataPathologyMicrobiologyHC"] != null ? int.Parse(ConfigurationManager.AppSettings["MinuteOfJobSyncDataPathologyMicrobiologyHC"].ToString()) : -45;
                var base_start_time = DateTime.ParseExact("17:30 01/08/2022", Constants.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
                bool DrsHCSyncIsOn = ConfigurationManager.AppSettings["DrsHCSyncIsOn"] != null && ConfigurationManager.AppSettings["DrsHCSyncIsOn"].ToString() == "True";
                if (!DrsHCSyncIsOn)
                {
                    CustomLogs.intervaljoblog.Info($"<SyncDataPathologyMicrobiologyHC> END with DrsHCSyncIsOff");
                    return;
                }
                string StartOfHCSync = ConfigurationManager.AppSettings["StartOfHCSync"] == null ? "" : ConfigurationManager.AppSettings["StartOfHCSync"].ToString(); // "17:30 22/04/2022"

                var to = DateTime.Now;

                var from = to.AddMinutes(MinuteOfJobSyncDataPathologyMicrobiologyHC);

                var start_in_config = ConfigurationManager.AppSettings["StartOfJobSyncDataPathologyMicrobiologyHC"];
                var end_in_config = ConfigurationManager.AppSettings["EndOfJobSyncDataPathologyMicrobiologyHC"];
                ChargeItem last_item = null;
                if (start_in_config != null && end_in_config != null)
                {
                    from = DateTime.ParseExact(start_in_config, Constants.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
                    to = DateTime.ParseExact(end_in_config, Constants.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
                }
                var is_get_by_last_sync = ConfigurationManager.AppSettings["IsSyncWithLastOfJobSyncDataPathologyMicrobiologyHC"];
                if (is_get_by_last_sync != null)
                {
                    last_item = unitOfWork.ChargeItemRepository.Find(e => e.VisitGroupType == VMHC_CODE && e.SyncAt != null && e.ChargeItemType == Constants.ChargeItemType.Lab).OrderByDescending(s => s.SyncAt).FirstOrDefault();
                    from = last_item != null ? (DateTime)last_item.SyncAt : base_start_time;
                    to = from.AddHours(MinuteOfJobSyncDataPathologyMicrobiologyHC);
                    if (to >= DateTime.Now) to = DateTime.Now;
                }

                var resultFromDataHc = OHClient.SyncDataPathologyMicrobiologyHC(from.ToString(Constants.DATETIME_SQL), to.ToString(Constants.DATETIME_SQL));
                resultFromDataHc = resultFromDataHc.OrderBy(e => e.CreationDateTime).ToList();
                CustomLogs.intervaljoblog.Info($"<GET SyncDataPathologyMicrobiologyHC> " + resultFromDataHc.Count().ToString() + " from " + from.ToString(Constants.DATETIME_SQL) + " to " + to.ToString(Constants.DATETIME_SQL));
                if (resultFromDataHc.Count() > 0)
                {
                    CustomLogs.intervaljoblog.Info($"<GET SyncDataPathologyMicrobiologyHC> Start" + resultFromDataHc.FirstOrDefault().CreationDateTime);
                    CustomLogs.intervaljoblog.Info($"<GET SyncDataPathologyMicrobiologyHC> End" + resultFromDataHc.LastOrDefault().CreationDateTime);

                    var count_new = 0;
                    foreach (var item in resultFromDataHc)
                    {
                        var service = unitOfWork.ServiceRepository.FirstOrDefault(e => !e.IsDeleted && e.Code == item.ServiceCode);
                        if (service == null) continue;
                        var itemType = GetItemType(service.ServiceGroup?.Code);
                        if (itemType == 2) continue;

                        var chargeItem = unitOfWork.ChargeItemRepository.FirstOrDefault(e => e.ChargeDetailId == item.ChargeDetaiId);
                        if (chargeItem != null)
                        {
                            chargeItem.SyncAt = item.CreationDateTime;
                            unitOfWork.Commit();
                            continue;
                        }
                        count_new++;
                        if (!string.IsNullOrEmpty(item.PatientId))
                        {
                            var cus = unitOfWork.CustomerRepository.FirstOrDefault(e => e.PID == item.PatientId);
                            if (cus == null)
                            {
                                cus = GetCreateHisCustomerByPid(item.PatientId);
                            }
                            CreateOrUpdateChargeVisit(item, cus.Id, service);
                        }
                    }
                    if (count_new == 0 && last_item != null)
                    {
                        last_item.SyncAt = to;
                        unitOfWork.Commit();
                    }
                    CustomLogs.intervaljoblog.Info($"<Sync SyncDataPathologyMicrobiologyHC> Success with " + count_new.ToString());
                }
                else
                {
                    if (last_item != null && is_get_by_last_sync != null)
                    {
                        last_item.SyncAt = to;
                        unitOfWork.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                CustomLogs.intervaljoblog.Error(string.Format("<Sync SyncDataPathologyMicrobiologyHC Error: {0}, Job is stoped", ex.ToString()));
                unitOfWork.Dispose();
            }
            unitOfWork.Dispose();
        }
        private Customer GetCreateHisCustomerByPid(string pid)
        {
            var hisCustomers = OHClient.searchPatienteOhByPidV3(pid);
            if (hisCustomers.Count == 0)
            {
                return null;
            }
            var his_customer = hisCustomers.First();
            var customerLocal = unitOfWork.CustomerRepository.Find(e => !e.IsDeleted && e.PID == pid).FirstOrDefault();
            if (customerLocal != null)
            {
                return customerLocal;
            }
            else
            {
                return CreateNewCustomer(his_customer);
            }
        }
        private int GetItemType(string group_code)
        {
            if (group_code.StartsWith("FB.05")) return 0;
            if (group_code.StartsWith("FB.01")) return 1;
            return 2;
        }
        private Customer CreateNewCustomer(HisCustomer his_customer)
        {
            DateTime? dob = null;
            if (!string.IsNullOrEmpty(his_customer.DateOfBirth))
            {
                dob = DateTime.ParseExact(his_customer.DateOfBirth, Constants.DATE_FORMAT, null);
            }
            Customer customer = new Customer
            {
                PID = his_customer.PID,
                Fullname = his_customer.Fullname,
                DateOfBirth = dob,
                Phone = his_customer.Phone,
                Gender = his_customer.Gender,
                Job = his_customer.Job,
                WorkPlace = his_customer.WorkPlace,
                Relationship = his_customer.Relationship,
                RelationshipContact = his_customer.RelationshipContact,
                Address = his_customer.Address,
                Nationality = his_customer.Nationality,
                Fork = his_customer.Fork,
                IdentificationCard = his_customer.IdentificationCard,
                RelationshipID = his_customer.RelationshipID,
                IsVip = his_customer.IsVip,
                HealthInsuranceNumber = his_customer.HealthInsuranceNumber
            };
            unitOfWork.CustomerRepository.Add(customer);
            unitOfWork.Commit();
            return customer;
        }
        private void CreateOrUpdateChargeVisit(DataHCModel data, Guid CustomerId, Service service)
        {
            data.VisitGroupType = VMHC_CODE; // các chỉ định qua upload tool
            data.VisitType = VMHC_CODE; // các chỉ định qua upload tool

            var checkitem = unitOfWork.ChargeVistRepository.FirstOrDefault(e => e.PatientVisitId == data.PatientVisitId &&
            e.VisitCode == data.VisitCode && e.VisitType == data.VisitType && e.HospitalCode == data.HospitalCode &&
            e.CustomerId == CustomerId && e.VisitGroupType == data.VisitGroupType);
            if (checkitem == null)
            {
                var chargeVisit = new ChargeVisit
                {
                    PatientLocationCode = data?.PatientLocationCode,
                    VisitGroupType = data.VisitGroupType,
                    VisitCode = data.VisitCode,
                    AreaName = data.AreaName,
                    VisitType = data.VisitType,
                    HospitalCode = data.HospitalCode,
                    DoctorAD = data.DoctorAD,
                    PatientLocationId = data?.PatientLocationId,
                    PatientVisitId = data.PatientVisitId,
                    CustomerId = CustomerId
                };
                unitOfWork.ChargeVistRepository.Add(chargeVisit);
                unitOfWork.Commit();
                CreateOrUpdateCharge(data, CustomerId, chargeVisit.Id, service);
            }
            else
            {
                checkitem.PatientLocationCode = data?.PatientLocationCode;
                checkitem.AreaName = data.AreaName;
                checkitem.DoctorAD = data.DoctorAD;
                checkitem.PatientLocationId = data?.PatientLocationId;
                unitOfWork.ChargeVistRepository.Update(checkitem);
                unitOfWork.Commit();
                CreateOrUpdateCharge(data, CustomerId, checkitem.Id, service);
            }
        }
        private void CreateOrUpdateCharge(DataHCModel data, Guid CustomerId, Guid chargevisitId, Service service)
        {
            var checkcharge = unitOfWork.ChargeRepository.FirstOrDefault(e => e.PatientVisitId == data.PatientVisitId &&
             e.VisitCode == data.VisitCode && e.VisitType == data.VisitType && e.HospitalCode == data.HospitalCode
             && e.ChargeVisitId == chargevisitId);
            if (checkcharge != null)
            {
                checkcharge.DoctorAD = data.DoctorAD;
                checkcharge.PatientVisitId = data.PatientVisitId;
                checkcharge.PatientLocationId = data?.PatientLocationId;
                checkcharge.VisitCode = data.VisitCode;
                checkcharge.VisitType = data.VisitType;
                checkcharge.HospitalCode = data.HospitalCode;
                unitOfWork.ChargeRepository.Update(checkcharge);
                unitOfWork.Commit();
                CreateOrUpdateChargeItem(data, CustomerId, checkcharge.Id, service);
            }
            else
            {
                var chargedata = new Charge
                {
                    DoctorAD = data.DoctorAD,
                    PatientVisitId = data.PatientVisitId,
                    PatientLocationId = data?.PatientLocationId,
                    VisitCode = data.VisitCode,
                    VisitType = data.VisitType,
                    HospitalCode = data.HospitalCode,
                    ChargeVisitId = chargevisitId
                };
                unitOfWork.ChargeRepository.Add(chargedata);
                unitOfWork.Commit();
                CreateOrUpdateChargeItem(data, CustomerId, chargedata.Id, service);
            }

        }
        private void CreateOrUpdateChargeItem(DataHCModel data, Guid CustomerId, Guid chargeId, Service service)
        {
            var chargeItem = unitOfWork.ChargeItemRepository.FirstOrDefault(e => e.ChargeDetailId == data.ChargeDetaiId);
            if (chargeItem == null)
            {
                if (data.DeleteDateTime == null)
                {
                    var chargedata = new ChargeItem
                    {
                        DoctorAD = data.DoctorAD,
                        CreatedBy = data.DoctorAD,
                        CreatedAt = data.ChargeDateTime,
                        UpdatedAt = data.ChargeDateTime,
                        UpdatedBy = data.DoctorAD,
                        CustomerId = CustomerId,
                        PatientVisitId = data.PatientVisitId.Value,
                        VisitCode = data.VisitCode,
                        VisitType = data.VisitType,
                        HospitalCode = data.HospitalCode,
                        ChargeId = chargeId,
                        VisitGroupType = data.VisitGroupType,
                        Status = data.Status,
                        CostCentreId = data.CostCentreId,
                        PatientId = data.PatientId,
                        ServiceId = service.Id,
                        ChargeDetailId = data.ChargeDetaiId,
                        ServiceCode = data.ServiceCode,
                        ItemId = service.HISId,
                        ItemType = GetItemType(service.ServiceGroup?.Code),
                        ChargeItemType = Constants.ChargeItemType.Lab,
                        SyncAt = data.CreationDateTime
                    };
                    unitOfWork.ChargeItemRepository.Add(chargedata);
                    unitOfWork.Commit();
                }
            }
            else
            {
                if (data.DeleteDateTime != null && chargeItem.VisitType == VMHC_CODE)
                {
                    chargeItem.Status = "Cancelled";
                    chargeItem.CancelFailedReason = "Cancelled form HC SYNC";
                    unitOfWork.ChargeItemRepository.Update(chargeItem);
                    unitOfWork.Commit();
                }
            }

        }
    }
}
