using Clients.HisClient;
using EMRModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bussiness.HisService
{
    public class OHAPIService
    {
        public static async Task<List<EkipFromOrModel>> SyncGetEkipFromOr(string pId, string visitcode)
        {
            var response = await HisClient.GetEkipFromOr(pId, visitcode);
            if (response != null)
                return JsonConvert.DeserializeObject<List<EkipFromOrModel>>(response.ToString());
            return new List<EkipFromOrModel>();
        }
        public static async Task<List<SyncAreasModel>> SyncOHAreaAsync(string from, string to)
        {
            var response = await HisClient.SyncAreas(from, to);
            if (response != null)
                return JsonConvert.DeserializeObject<List<SyncAreasModel>>(response.ToString());
            return new List<SyncAreasModel>();
        }
        public static async Task<List<HISChargeDetailModel>> GetUpdateChargeByChargeDetailIdAsync(string ChargeIds)
        {
            var response = await HisClient.GetUpdateChargeByChargeDetailIdAsync(ChargeIds);
            if (response != null)
                return JsonConvert.DeserializeObject<List<HISChargeDetailModel>>(response.ToString());
            return new List<HISChargeDetailModel>();
        }
        public static async Task<List<GetViHCByPID>> GetViHCByPIDAsync(string pid)
        {
            var response = await HisClient.GetViHCByPID(pid);
            if (response != null)
                return JsonConvert.DeserializeObject<List<GetViHCByPID>>(response.ToString());
            return new List<GetViHCByPID>();
        }
        public static async Task<List<GetServiceDepartment>> getServiceDepartmentIdAsync(Guid patientLocationId, Guid HISId)
        {
            var response = await HisClient.GetServiceDepartmentId(patientLocationId, HISId);
            if (response != null)
                return JsonConvert.DeserializeObject<List<GetServiceDepartment>>(response.ToString());
            return new List<GetServiceDepartment>();
        }
        public static async Task<JToken> GetMohFinalReportAsync()
        {
            return await ApiRequest.PostAsync("http://10.115.40.179/api/ApiReport/MohFinalReport", new {
                pId = "200527540",
                visitCode = "294159",
                siteCode = "HHP",
                lang = "vn"
            }, "f130551d-52ca-4e6c-9dfe-526d8e6d7334");
        }
    }
}
