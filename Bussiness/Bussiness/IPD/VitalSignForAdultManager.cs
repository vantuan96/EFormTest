using Common;
using DataAccess.Models.IPDModel;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Data;

namespace Bussiness.IPD
{
    public class VitalSignForAdultManager
    {
        protected static IUnitOfWork unitOfWork = new EfUnitOfWork();

        public class MewsData
        {
            public Guid? VisitId { get; set; }
            public string CreatedAt { get; set; }
            public string CreatedBy { get; set; }
        }

        public class BreathData : MewsData
        {
            public decimal? BreathRate { get; set; }
            public int? BreathMEWS { get; set; }
        }

        public class SPO2Data : MewsData
        {
            public bool COPD { get; set; }
            public decimal? SPO2 { get; set; }
            public int? SPO2MEWS { get; set; }
        }

        public class BPData : MewsData
        {
            public decimal? HighBP { get; set; }
            public int? BPMEWS { get; set; }
        }

        public class PulseData : MewsData
        {
            public decimal? Pulse { get; set; }
            public int? PulseMEWS { get; set; }
        }

        public class TemperatureData : MewsData
        {
            public decimal? Temperature { get; set; }
            public int? TemperatureMEWS { get; set; }
        }

        public class SenseData : MewsData
        {
            public string Sense { get; set; }
            public int SenseMews { get; set; }
        }


        public class MewsChart
        {
            public IList<BreathData> LstBreathRate { get; set; }
            public List<SPO2Data> LstSPO2 { get; set; }
            public List<BPData> LstBP { get; set; }
            public List<PulseData> LstPulse { get; set; }
            public List<TemperatureData> LstTemperature { get; set; }
            public List<SenseData> LstSense { get; set; }

        }

        /// <summary>
        /// Get vital sign for adult by primary key id
        /// </summary>
        /// <param name="vitalsignId"></param>
        /// <returns></returns>
        public static VitalSignForAdult GetById(Guid vitalsignId)
        {
            return unitOfWork.IPDVitalSignForAdultRespository.GetById(vitalsignId);
        }


        public static DataSet GetByVisitId(VitalSignForAdult vitalSign)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("VISIT_ID", vitalSign.VisitId);
            dict.Add("USER_NAME", vitalSign.CreatedBy);
            dict.Add("FROM_DATE", vitalSign.DateFrom);
            dict.Add("TO_DATE", vitalSign.DateTo);
            dict.Add("PAGE_INDEX", vitalSign.PageIndex);
            dict.Add("PAGE_SIZE", vitalSign.PageSize);
            dict.Add("TOTAL_ROW", 0);

            EfUnitOfWork unitOfWork = new EfUnitOfWork();
            var ds = unitOfWork.ExecuteDataSet("prc_VitalSignAdult_SearchByVisitId", dict);

            return ds;
        }


        public static DataSet GetChartByVisitId(VitalSignForAdult vitalSign)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("VISIT_ID", vitalSign.VisitId);
            dict.Add("USER_NAME", vitalSign.CreatedBy);
            dict.Add("FROM_DATE", vitalSign.DateFrom);
            dict.Add("TO_DATE", vitalSign.DateTo);
            dict.Add("PAGE_INDEX", vitalSign.PageIndex);
            dict.Add("PAGE_SIZE", vitalSign.PageSize);
            dict.Add("ORDER_BY", "TRANS_DATE");
            dict.Add("DIRECTION_SORT", "ASC");
            dict.Add("TOTAL_ROW", 0);

            EfUnitOfWork unitOfWork = new EfUnitOfWork();
            var ds = unitOfWork.ExecuteDataSet("prc_VitalSignAdult_SearchByVisitId", dict);

            return ds;
        }

        public static int Insert(VitalSignForAdult entity)
        {
            try
            {
                entity.IsDeleted = false;
                unitOfWork.IPDVitalSignForAdultRespository.Add(entity);
                unitOfWork.Commit();
                return 1;
            }
            catch
            {
                return -1;
            }
        }

        public static int Update(VitalSignForAdult entity)
        {
            try
            {
                unitOfWork.IPDVitalSignForAdultRespository.Update(entity);
                unitOfWork.Commit();
                return 1;
            }
            catch
            {
                return -1;
            }
        }


        public static MewsChart GetChartData(VitalSignForAdult vitalSign)
        {
            MewsChart mewsChart = new MewsChart();

            List<VitalSignForAdult> lstMewsData = new List<VitalSignForAdult>();
            DataTable dt = GetChartByVisitId(vitalSign).Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    VitalSignForAdult vitalInfo = new VitalSignForAdult();
                    vitalInfo.Id = (Guid)item["Id"];
                    vitalInfo.VisitId = (Guid?)item["VISIT_ID"];
                    vitalInfo.CreatedAt = (DateTime?)item["CreatedAt"];
                    vitalInfo.CreatedBy = (string)item["CreatedBy"];
                    vitalInfo.TransactionDate = (DateTime)item["TransactionDate"];
                    vitalInfo.BreathRate = item["BreathRate"] == DBNull.Value || item["BreathRate"].ToString() == string.Empty ? -1 : decimal.Parse(item["BreathRate"].ToString());
                    vitalInfo.SPO2 = item["SPO2"] == DBNull.Value || item["SPO2"].ToString() == string.Empty ? -1 : decimal.Parse(item["SPO2"].ToString());
                    vitalInfo.HighBP = item["HighBP"] == DBNull.Value || item["HighBP"].ToString() == string.Empty ? -1 : decimal.Parse(item["HighBP"].ToString());
                    vitalInfo.Pulse = item["Pulse"] == DBNull.Value || item["Pulse"].ToString() == string.Empty ? -1 : decimal.Parse(item["Pulse"].ToString());
                    vitalInfo.Temperature = item["Temperature"] == DBNull.Value || item["Temperature"].ToString() == string.Empty ? -1 : decimal.Parse(item["Temperature"].ToString());
                    vitalInfo.Sense = item["Sense"].ToString();
                    lstMewsData.Add(vitalInfo);
                }
            }

            List<BreathData> lstBreathData = new List<BreathData>();
            foreach (var item in lstMewsData)
            {
                BreathData entity = new BreathData();
                entity.VisitId = item.VisitId;
                entity.CreatedAt = item.TransactionDate.ToString(Constants.TIME_DATE_FORMAT_WITHOUT_SECOND);
                entity.BreathRate = item.BreathRate == -1 ? null : item.BreathRate;
                entity.BreathMEWS = item.BreathRate == -1 ? null : item.BreathMEWS;
                entity.CreatedBy = item.CreatedBy;
                lstBreathData.Add(entity);
            }
            mewsChart.LstBreathRate = lstBreathData;

            List<SPO2Data> lstSPO2Data = new List<SPO2Data>();
            foreach (var item in lstMewsData)
            {
                SPO2Data entity = new SPO2Data();
                entity.VisitId = item.VisitId;
                entity.CreatedAt = item.TransactionDate.ToString(Constants.TIME_DATE_FORMAT_WITHOUT_SECOND);
                entity.SPO2 = item.SPO2 == -1 ? null : item.SPO2;
                entity.SPO2MEWS = item.SPO2 == -1 ? null : item.SPO2MEWS;
                entity.COPD = item.COPD;
                entity.CreatedBy = item.CreatedBy;
                lstSPO2Data.Add(entity);
            }
            mewsChart.LstSPO2 = lstSPO2Data;

            List<BPData> lstBPData = new List<BPData>();
            foreach (var item in lstMewsData)
            {
                BPData entity = new BPData();
                entity.VisitId = item.VisitId;
                entity.CreatedAt = item.TransactionDate.ToString(Constants.TIME_DATE_FORMAT_WITHOUT_SECOND);
                entity.HighBP = item.HighBP == -1 ? null : item.HighBP;
                entity.BPMEWS = item.HighBP == -1 ? null : item.BPMEWS;
                entity.CreatedBy = item.CreatedBy;
                lstBPData.Add(entity);
            }
            mewsChart.LstBP = lstBPData;

            List<PulseData> lstPulseData = new List<PulseData>();
            foreach (var item in lstMewsData)
            {
                PulseData entity = new PulseData();
                entity.VisitId = item.VisitId;
                entity.CreatedAt = item.TransactionDate.ToString(Constants.TIME_DATE_FORMAT_WITHOUT_SECOND);
                entity.Pulse = item.Pulse == -1 ? null : item.Pulse;
                entity.PulseMEWS = item.Pulse == -1 ? null : item.PulseMEWS;
                entity.CreatedBy = item.CreatedBy;
                lstPulseData.Add(entity);
            }
            mewsChart.LstPulse = lstPulseData;

            List<TemperatureData> lstTemperatureData = new List<TemperatureData>();
            foreach (var item in lstMewsData)
            {
                TemperatureData entity = new TemperatureData();
                entity.VisitId = item.VisitId;
                entity.CreatedAt = item.TransactionDate.ToString(Constants.TIME_DATE_FORMAT_WITHOUT_SECOND);
                entity.Temperature = item.Temperature == -1 ? null : item.Temperature;
                entity.TemperatureMEWS = item.Temperature == -1 ? null : item.TemperatureMEWS;
                entity.CreatedBy = item.CreatedBy;
                lstTemperatureData.Add(entity);
            }
            mewsChart.LstTemperature = lstTemperatureData;

            List<SenseData> lstSenseData = new List<SenseData>();
            foreach (var item in lstMewsData)
            {
                SenseData entity = new SenseData();
                entity.VisitId = item.VisitId;
                entity.CreatedAt = item.TransactionDate.ToString(Constants.TIME_DATE_FORMAT_WITHOUT_SECOND);
                entity.Sense = item.Sense;
                entity.SenseMews = item.SenseMews;
                entity.CreatedBy = item.CreatedBy;
                lstSenseData.Add(entity);
            }
            mewsChart.LstSense = lstSenseData;

            return mewsChart;
        }
    }
}
