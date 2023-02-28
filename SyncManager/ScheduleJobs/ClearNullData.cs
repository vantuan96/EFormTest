using Common;
using DataAccess.Dapper.Repository;
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
    public class ClearNullData : IJob
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
            CustomLogs.intervaljoblog.Info($"<ClearNullData> Start!");
            if (ConfigHelper.CF_NullData_CS_is_off)
            {
                CustomLogs.intervaljoblog.Info($"<ClearNullData> is Off!");
                return;
            }
            try
            {
                //unitOfWork.FormDatasRepository.HardDeleteRange(
                //    unitOfWork.FormDatasRepository.AsQueryable().Where(e => string.IsNullOrEmpty(e.Value)).Take(1000)
                //);
                //unitOfWork.EmergencyTriageRecordDataRepository.HardDeleteRange(
                //    unitOfWork.EmergencyTriageRecordDataRepository.AsQueryable().Where(e => string.IsNullOrEmpty(e.Value)).Take(1000)
                //);
                //unitOfWork.OPDInitialAssessmentForShortTermDataRepository.HardDeleteRange(
                //    unitOfWork.OPDInitialAssessmentForShortTermDataRepository.AsQueryable().Where(e => string.IsNullOrEmpty(e.Value)).Take(10000)
                //);
                //unitOfWork.OPDOutpatientExaminationNoteDataRepository.HardDeleteRange(
                //    unitOfWork.OPDOutpatientExaminationNoteDataRepository.AsQueryable().Where(e => string.IsNullOrEmpty(e.Value)).Take(10000)
                //);
                //unitOfWork.IPDMedicalRecordDataRepository.HardDeleteRange(
                //    unitOfWork.IPDMedicalRecordDataRepository.AsQueryable().Where(e => string.IsNullOrEmpty(e.Value)).Take(10000)
                //);
                //unitOfWork.IPDMedicalRecordPart1DataRepository.HardDeleteRange(
                //    unitOfWork.IPDMedicalRecordPart1DataRepository.AsQueryable().Where(e => string.IsNullOrEmpty(e.Value)).Take(10000)
                //);
                //unitOfWork.IPDMedicalRecordPart2DataRepository.HardDeleteRange(
                //    unitOfWork.IPDMedicalRecordPart2DataRepository.AsQueryable().Where(e => string.IsNullOrEmpty(e.Value)).Take(10000)
                //);
                //unitOfWork.IPDMedicalRecordPart3DataRepository.HardDeleteRange(
                //    unitOfWork.IPDMedicalRecordPart3DataRepository.AsQueryable().Where(e => string.IsNullOrEmpty(e.Value)).Take(10000)
                //);
                //unitOfWork.Commit();

                var count = ConfigurationManager.AppSettings["MaximumNumberOfItemPerRequest"] != null ? int.Parse(ConfigurationManager.AppSettings["MaximumNumberOfItemPerRequest"].ToString()) : 1000;
                var h = DateTime.Now.Hour;
                if (h > 7 & h < 19)
                {
                    count = 500;
                }
                else
                {
                    // count = count * 2;
                }
                var param = new
                {
                    takeRowNumber = count
                };
                ExecStoProcedure.NoResult("ClearNullValue", param);

                CustomLogs.intervaljoblog.Info($"<ClearNullData> Success!");
            }
            catch (Exception ex)
            {
                CustomLogs.intervaljoblog.Info(string.Format("<ClearNullData> Error: {0}", ex));
            }
        }
    }
}