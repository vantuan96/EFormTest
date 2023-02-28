using Common;
using DataAccess.Models.EDModel;
using DataAccess.Models.EOCModel;
using DataAccess.Models.GeneralModel;
using DataAccess.Models.IPDModel;
using DataAccess.Models.OPDModel;
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
    public class LockVipMedicalRecord : IJob
    {
        protected IUnitOfWork unitOfWork = new EfUnitOfWork();
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

            CustomLogs.intervaljoblog.Info($"<LockVipMedicalRecordJob> Start!");
            if (ConfigHelper.CF_LockVipPatientService_CS_is_off)
            {
                CustomLogs.intervaljoblog.Info($"<LockVipMedicalRecordJob> is Off!");
                return;
            }
            try
            {
                DateTime? last_update_time = null;
                if (ConfigurationManager.AppSettings["CF_LockVipPatientService_CS"] != null)
                {
                    last_update_time = DateTime.Now.AddDays(-2);
                    CustomLogs.intervaljoblog.Info($"<LockVipMedicalRecordJob> Start with {last_update_time.ToString()}");
                }

                var ed_count = LockEDVipMedicalRecord(last_update_time);
                var eoc_count = LockEOCVipMedicalRecord(last_update_time);
                var opd_count = LockOPDVipMedicalRecord(last_update_time);
                var ipd_count = LockIPDVipMedicalRecord(last_update_time);

                CustomLogs.intervaljoblog.Info($"<LockVipMedicalRecordJob> Success with ED {ed_count} + OPD {opd_count} + EDC {eoc_count} + IPD {ipd_count}");
            }
            catch (Exception ex)
            {
                CustomLogs.intervaljoblog.Info(string.Format("<LockVipMedicalRecordJob> Error: {0}", ex));
            }
        }
        protected int LockEDVipMedicalRecord(DateTime? last_update_time = null)
        {
            var visits = unitOfWork.EDRepository.Find(e => !e.IsDeleted && e.Customer.IsVip && (last_update_time == null || e.UpdatedAt >= last_update_time)).ToList();
            var i = 0;
            foreach (ED visit in visits)
            {
                if (visit.DischargeDate != null && IsBlockAfter24h(visit.DischargeDate))
                {
                    visit.UnlockFor = GetUnlockFor(visit.Id, visit.Customer.Id, visit.PrimaryDoctor?.Username);
                    i++;
                }
            }
            unitOfWork.Commit();
            return i;
        }
        protected int LockOPDVipMedicalRecord(DateTime? last_update_time = null)
        {
            var visits = unitOfWork.OPDRepository.Find(e => !e.IsDeleted && e.Customer.IsVip && (last_update_time == null || e.UpdatedAt >= last_update_time)).ToList();
            var i = 0;
            foreach (OPD visit in visits)
            {
                var list_status = new List<string> { "OPDIH", "OPDWR" };
                if (!list_status.Contains(visit.EDStatus.Code) && IsBlockAfter24h(visit.DischargeDate))
                {
                    var list_doctor = new List<string>();
                    if (!string.IsNullOrEmpty(visit.PrimaryDoctor?.Username)) list_doctor.Add(visit.PrimaryDoctor?.Username);
                    if (!string.IsNullOrEmpty(visit.AuthorizedDoctor?.Username)) list_doctor.Add(visit.AuthorizedDoctor?.Username);
                    visit.UnlockFor = GetUnlockFor(visit.Id, visit.Customer.Id, string.Join(",", list_doctor));
                    i++;
                }
            }
            unitOfWork.Commit();
            return i;
        }
        protected int LockIPDVipMedicalRecord(DateTime? last_update_time = null)
        {
            var visits = unitOfWork.IPDRepository.Find(e => !e.IsDeleted && e.Customer.IsVip && (last_update_time == null || e.UpdatedAt >= last_update_time)).ToList();
            var i = 0;
            foreach (IPD visit in visits)
            {
                if (visit.DischargeDate != null && IsBlockAfter24h(visit.DischargeDate))
                {
                    visit.UnlockFor = GetUnlockFor(visit.Id, visit.Customer.Id, visit.PrimaryDoctor?.Username);
                    i++;
                }
            }
            unitOfWork.Commit();
            return i;
        }
        protected int LockEOCVipMedicalRecord(DateTime? last_update_time = null)
        {
            var visits = unitOfWork.EOCRepository.Find(e => !e.IsDeleted && e.Customer.IsVip && (last_update_time == null || e.UpdatedAt >= last_update_time)).ToList();
            var i = 0;
            foreach (EOC visit in visits)
            {
                if (visit.DischargeDate != null && IsBlockAfter24h(visit.DischargeDate))
                {
                    var list_doctor = new List<string>();
                    if (!string.IsNullOrEmpty(visit.PrimaryDoctor?.Username)) list_doctor.Add(visit.PrimaryDoctor?.Username);
                    if (!string.IsNullOrEmpty(visit.AuthorizedDoctor?.Username)) list_doctor.Add(visit.AuthorizedDoctor?.Username);
                    visit.UnlockFor = GetUnlockFor(visit.Id, visit.Customer.Id, string.Join(",", list_doctor));
                    i++;
                }
            }
            unitOfWork.Commit();
            return i;
        }
        protected string GetUnlockFor(Guid visit_id, Guid custome_id, string primary_doctor = null) {
            var now = DateTime.Now;
            var unlockfor = unitOfWork.UnlockVipRepository.Find(e => !e.IsDeleted && e.VisitId == visit_id && e.CustomerId == custome_id && e.ExpiredAt >= now).Select(e => e.Username).ToList();
            if (primary_doctor != null)
            {
                unlockfor.Add(primary_doctor);
            }
            if (unlockfor.Count() > 0) {
                var usernames = string.Join(",", unlockfor);
                return usernames;
            }
            return null;
        }
        protected bool IsBlockAfter24h(DateTime? created_at)
        {
            var now = DateTime.Now;
            double timeToBlock = 1;
            return created_at?.AddDays(timeToBlock) <= now;
        }
    }
}