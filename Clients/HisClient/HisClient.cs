using DataAccess.Models;
using EMRModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using Helper;
using System.Threading.Tasks;

namespace Clients.HisClient
{
    public class HisClient : ApiGWRequest
    {
        #region Get Ekip From OR
        public static async Task<JToken> GetEkipFromOr(string pId, string visitcode)
        {
            string url_postfix = string.Format("/EMRVinmecCom/1.0.0/getThongTinEkipMo?PID={0}&VisitCode={1}", pId, visitcode);
            return await GetAsync(url_postfix, "Services", "Service");
        }
        #endregion
        public static async Task<JToken> GetViHCByPID(string pid)
        {
            string url_postfix = string.Format("/EMRVinmecCom/1.0.0/getViHCByPID?Pid={0}", pid);
            return await GetAsync(url_postfix, "Entries", "Entry", 20);
        }
        public static async Task<JToken> SyncAreas(string from, string to)
        {
            string url_postfix = string.Format("/EMRVinmecCom/1.0.0/sync_Areas?from={0}&to={1}", from, to);
            return await GetAsync(url_postfix, "Entries", "Entry");
        }
        public static async Task<JToken> SearchPatientByPID(string pid)
        {
            string url_postfix = string.Format("/EMRVinmecCom/1.0.0/searchPatientByPID?PID={0}", pid);
            return await GetAsync(url_postfix, "Entries", "Entry");
        }
        public static async Task<JToken> GetUpdateChargeByChargeDetailIdAsync(string ChargeIds)
        {
            string url_postfix = string.Format("/EMRVinmecCom/1.0.0/getChargeDetailIdStatus?ChargeIds={0}", ChargeIds);
            return await GetAsync(url_postfix, "Entries", "Entry", 20);
        }
        public static async Task<List<HISChargeDetailModel>> getUpdateChargeAsync(string ChargeIds, string LabChargeIds, string RadChargeIds, string visit_code, string visit_type)
        {
            string url_postfix = string.Format(
                "/EMRVinmecCom/1.0.0/getChargeStatus?ChargeIds={0}&LabChargeIds={1}&RadChargeIds={2}&VisitCode={3}&VisitType={4}",
                ChargeIds,
                LabChargeIds,
                RadChargeIds,
                visit_code,
                visit_type
            );
            var response = await GetAsync(url_postfix, "Entries", "Entry", 20);
            if (response != null)
                return response.Select(e => new HISChargeDetailModel
                {
                    ChargeDetailId = e["ChargeDetailId"].ToString() != "" ? new Guid(e["ChargeDetailId"].ToString()) : (Guid?)null,
                    NewChargeId = e["NewChargeId"].ToString() != "" ? new Guid(e["NewChargeId"].ToString()) : (Guid?)null,
                    FillerOrderNumber = e["FillerOrderNumber"].ToString() != "" ? e["FillerOrderNumber"].ToString() : null,
                    PlacerOrderNumber = e["PlacerOrderNumber"]?.ToString(),
                    PaymentStatus = e["PaymentStatus"]?.ToString(),
                    RadiologyScheduledStatus = e["RadiologyScheduledStatus"]?.ToString(),
                    PlacerOrderStatus = e["PlacerOrderStatus"]?.ToString(),
                    SpecimenStatus = e["SpecimenStatus"]?.ToString(),
                    ChargeDeletedDate = e["ChargeDeletedDate"]?.ToString() != "" ? DateTime.Parse(e["ChargeDeletedDate"]?.ToString()) : (DateTime?)null,
                }).ToList();
            return new List<HISChargeDetailModel>();
        }

        public static async Task<List<OpenVisitInfo>> GetVisitDetailsV2Async(Guid patientVisitId)
        {
            string url_postfix = string.Format("/EMRVinmecCom/1.0.0/getVisitDetails?PatientVisitId={0}", patientVisitId);
            var response = await GetAsync(url_postfix, "VisitSyncs", "VisitSync");
            if (response != null)
            {
                return BuildVisitDetailsResult(response);
            }
            return new List<OpenVisitInfo>();
        }
        private static List<OpenVisitInfo> BuildVisitDetailsResult(JToken visit_data)
        {
            List<OpenVisitInfo> result = new List<OpenVisitInfo>();

            foreach (JToken data in visit_data)
            {
                result.Add(new OpenVisitInfo
                {
                    PatientId = data.Value<string>("PatientId"),
                    PatientVisitId = data.Value<string>("PatientVisitId"),
                    VisitCode = data.Value<string>("VisitCode"),
                    VisitType = data.Value<string>("VisitType"),
                    ActualVisitDate = data.Value<string>("ActualVisitDate"),
                    ClosureDate = data.Value<string>("ClosureDate")
                });
            }
            return result.ToList();
        }
        public static async Task<List<OpenVisitResult>> GetOpenVisitCodeAsync(string pid)
        {
            string url_postfix = string.Format("/EMRVinmecCom/1.0.0/getVisitByPID?PID={0}&pagenum=1&pagesize=100", pid);
            var response = await GetAsync(url_postfix, "VisitSyncs", "VisitSync");
            if (response != null)
            {
                return BuildOpenVisitResult(response);
            }
            return new List<OpenVisitResult>();
        }
        private static List<OpenVisitResult> BuildOpenVisitResult(JToken visit_data)
        {
            List<OpenVisitResult> result = new List<OpenVisitResult>();

            foreach (JToken data in visit_data)
            {
                var visit_code = data.Value<string>("VisitCode");
                var ClosureDate = data.Value<string>("ClosureDate");
                var ActualVisitDate = data.Value<string>("ActualVisitDate");

                var VisitType = data.Value<string>("VisitType");
                var visit_type = FormatVisitTypeCodeOH(VisitType);

                if (string.IsNullOrEmpty(visit_code))
                    continue;
                if (string.IsNullOrEmpty(ClosureDate) || ClosureDate == "null")
                {
                    result.Add(new OpenVisitResult
                    {
                        PatientLocationId = StringHelper.StringToGuid(data.Value<string>("PatientLocationId")),
                        PatientVisitId = StringHelper.StringToGuid(data.Value<string>("PatientVisitId")),
                        VisitCode = visit_code,
                        VisitTypeName = visit_type,
                        ActualVisitDate = DatetimeHelper.FormatDateVisitCode(ActualVisitDate),
                        AreaName = data.Value<string>("AreaName"),
                        VisitType = data.Value<string>("VisitType"),
                        HospitalCode = data.Value<string>("HospitalCode"),
                        DoctorAD = data.Value<string>("DoctorAD"),
                        VisitGroupType = data.Value<string>("VisitGroupType"),
                        PatientLocationCode = data.Value<string>("PatientLocationCode"),
                        CostCentreId = StringHelper.StringToGuid(data.Value<string>("CostCentreId"))
                    });
                }
            }
            return result.ToList();
        }
        protected static string FormatVisitTypeCodeOH(string rawData)
        {
            var result = "";
            if (!string.IsNullOrEmpty(rawData))
            {
                rawData = rawData.Trim();
                switch (rawData)
                {
                    case "VMED":
                        result = "Cấp cứu";
                        break;
                    case "VMEDO":
                        result = "Cấu cứu lưu";
                        break;
                    case "VMOPD":
                        result = "Ngoại trú";
                        break;
                    case "VMPK":
                        result = "Ngoại trú (Gói)";
                        break;
                    case "KBTX1":
                        result = "Ngoại trú (Telehealth)";
                        break;
                    case "VMIPD":
                        result = "Nội trú";
                        break;
                    default:
                        break;
                }
            }
            return result;
        }

        public static async Task<JToken> GetServiceDepartmentId(Guid patientLocationId, Guid HISId)
        {
            string url_postfix = string.Format("/EMRVinmecCom/1.0.0/getServiceDepartment?PatientLocationId={0}&ServiceId={1}", patientLocationId, HISId.ToString());
            return await GetAsync(url_postfix, "Entries", "Entry");
        }
    }
}
