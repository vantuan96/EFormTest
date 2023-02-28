using DataAccess.Models;
using DataAccess.Repository;
using EForm.Common;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EForm.ScheduleJobs
{
    public class NotifyBlock24h : IJob
    {
        private IUnitOfWork unitOfWork = new EfUnitOfWork();

        public void Execute(IJobExecutionContext context)
        {
            CustomLog.intervaljoblog.Info($"<Notify> Start job");
            var start = DateTime.Now.AddHours(-22);
            var end = start.AddMinutes(20);
            CustomLog.intervaljoblog.Info($"<Notify> Range: {start.ToString()} = {end.ToString()}");
            //var warning_ed = GetWarningED(start, end);
            //foreach (var warn in warning_ed)
            //{
            //    CreateNotification(warn.Nurse, "ED", warn.SpecialtyName, warn.CustomerName, warn.SpecialtyId, warn.Id, "ETR");
            //    CreateNotification(warn.Doctor, "ED", warn.SpecialtyName, warn.CustomerName, warn.SpecialtyId, warn.Id, "DI0");
            //}

            var warning_opd = GetWarningOPD(start, end);
            CustomLog.intervaljoblog.Info($"<Notify> Visit: {warning_opd.Count}");
            foreach (var warn in warning_opd)
            {
                if (warn.IsNurse)
                    CreateNotification(warn.User, "OPD", warn.SpecialtyName, warn.CustomerName, warn.SpecialtyId, warn.Id, "InitialAssessment");
                else
                    CreateNotification(warn.User, "OPD", warn.SpecialtyName, warn.CustomerName, warn.SpecialtyId, warn.Id, "OutpatientExaminationNote");
            }
            CustomLog.intervaljoblog.Info("<Notify> End job!");
        }

        private dynamic GetWarningED(DateTime start, DateTime end)
        {
            return unitOfWork.EDRepository.Find(
                e => !e.IsDeleted &&
                e.CreatedAt != null &&
                e.CreatedAt >= start &&
                e.CreatedAt <= end &&
                e.CustomerId != null &&
                e.SpecialtyId != null &&
                e.EDStatusId != null &&
                Constant.InHospital.Contains(e.EDStatus.Code) &&
                e.EmergencyTriageRecordId != null &&
                e.DischargeInformationId != null
            ).Select(e => new {
                e.Id,
                SpecialtyId = e.Specialty.Id,
                SpecialtyName = e.Specialty.ViName,
                CustomerName = e.Customer.Fullname,
                Nurse = e.EmergencyTriageRecord.UpdatedBy,
                Doctor = e.DischargeInformation.UpdatedBy
            }).ToList();
        }
        private dynamic GetWarningOPD(DateTime start, DateTime end)
        {
            List<dynamic> visit = new List<dynamic>();
            visit.AddRange(GetWarningOPDOngoing(start, end));
            visit.AddRange(GetWarningOPDShortTerm(start, end));
            visit.AddRange(GetWarningOPDTelehealth(start, end));
            visit.AddRange(GetWarningOPDOutPatient(start, end));
            return visit.Distinct().ToList();
        }
        private dynamic GetWarningOPDOngoing(DateTime start, DateTime end)
        {
            return unitOfWork.OPDRepository.Find(
                e => !e.IsDeleted &&
                e.CreatedAt != null &&
                e.CreatedAt >= start &&
                e.CreatedAt <= end &&
                e.CustomerId != null &&
                e.SpecialtyId != null &&
                e.EDStatusId != null &&
                Constant.InHospital.Contains(e.EDStatus.Code) &&
                e.OPDInitialAssessmentForOnGoingId != null
            ).Select(e => new {
                e.Id,
                SpecialtyId = e.Specialty.Id,
                SpecialtyName = e.Specialty.ViName,
                CustomerName = e.Customer.Fullname,
                User = e.OPDInitialAssessmentForOnGoing.UpdatedBy,
                IsNurse = true
            }).ToList();
        }
        private dynamic GetWarningOPDShortTerm(DateTime start, DateTime end)
        { 
            return unitOfWork.OPDRepository.Find(
                e => !e.IsDeleted &&
                e.CreatedAt != null &&
                e.CreatedAt >= start &&
                e.CreatedAt <= end &&
                e.CustomerId != null &&
                e.SpecialtyId != null &&
                Constant.InHospital.Contains(e.EDStatus.Code) &&
                e.OPDInitialAssessmentForShortTermId != null
            ).Select(e => new {
                e.Id,
                SpecialtyId = e.Specialty.Id,
                SpecialtyName = e.Specialty.ViName,
                CustomerName = e.Customer.Fullname,
                User = e.OPDInitialAssessmentForShortTerm.UpdatedBy,
                IsNurse = true
            }).ToList();
        }
        private dynamic GetWarningOPDTelehealth(DateTime start, DateTime end)
        {
            return unitOfWork.OPDRepository.Find(
                e => !e.IsDeleted &&
                e.CreatedAt != null &&
                e.CreatedAt >= start &&
                e.CreatedAt <= end &&
                e.CustomerId != null &&
                e.SpecialtyId != null &&
                Constant.InHospital.Contains(e.EDStatus.Code) &&
                e.OPDInitialAssessmentForTelehealthId != null
            ).Select(e => new {
                e.Id,
                SpecialtyId = e.Specialty.Id,
                SpecialtyName = e.Specialty.ViName,
                CustomerName = e.Customer.Fullname,
                User = e.OPDInitialAssessmentForTelehealth.UpdatedBy,
                IsNurse = true
            }).ToList();
        }
        private dynamic GetWarningOPDOutPatient(DateTime start, DateTime end)
        {
            return unitOfWork.OPDRepository.Find(
                e => !e.IsDeleted &&
                e.CreatedAt != null &&
                e.CreatedAt >= start &&
                e.CreatedAt <= end &&
                e.CustomerId != null &&
                e.SpecialtyId != null &&
                Constant.InHospital.Contains(e.EDStatus.Code) &&
                e.PrimaryDoctorId != null
            ).Select(e => new {
                e.Id,
                SpecialtyId = e.Specialty.Id,
                SpecialtyName = e.Specialty.ViName,
                CustomerName = e.Customer.Fullname,
                User = e.PrimaryDoctor.Username,
                IsNurse = false
            }).ToList();
        }


        private void CreateNotification(string to_user, string group_code, string spec_name, string customer_name, Guid spec_id, Guid visit_id, string form)
        {
            var vi_message = $"<b>[{group_code}- {spec_name}]</b> Hồ sơ của bệnh nhân <b>{customer_name}</b> sẽ được khóa sau 2 tiếng kể từ thời điểm thông báo này";
            var en_message = $"<b>[{group_code}- {spec_name}]</b> Hồ sơ của bệnh nhân <b>{customer_name}</b> sẽ được khóa sau 2 tiếng kể từ thời điểm thông báo này";
            var old_noti = unitOfWork.NotificationRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                !string.IsNullOrEmpty(e.ViMessage) &&
                e.ViMessage.Equals(vi_message) &&
                !string.IsNullOrEmpty(e.ToUser) &&
                e.ToUser == to_user &&
                e.VisitId != null &&
                e.VisitId == visit_id
            );
            if (old_noti != null)
            {
                old_noti.CreatedAt = DateTime.Now;
                unitOfWork.NotificationRepository.Update(old_noti);
            }
            else
            {
                Notification noti = new Notification()
                {
                    ToUser = to_user,
                    Priority = 2,
                    ViMessage = vi_message,
                    EnMessage = en_message,
                    SpecialtyId = spec_id,
                    VisitId = visit_id,
                    VisitTypeGroupCode = group_code,
                    Form = form,
                };
                unitOfWork.NotificationRepository.Add(noti);
            }
            unitOfWork.Commit();
        }
    }
}