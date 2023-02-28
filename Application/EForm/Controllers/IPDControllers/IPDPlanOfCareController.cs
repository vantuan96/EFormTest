using DataAccess.Models.IPDModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers;
using EForm.Models.IPDModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class IPDPlanOfCareController : BaseIPDApiController
    {
        [HttpGet]
        [Route("api/IPD/PlanOfCare/{id}")]
        [Permission(Code = "IPLOC1")]
        public IHttpActionResult GetIPDPlanOfCareAPI(Guid id)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);

            var user = GetUser();
            if (user == null)
                return Content(HttpStatusCode.NotFound, "User không tồn tại trong hệ thống");

            var plan_of_care = unitOfWork.IPDPlanOfCareRepository.Find(
                e => !e.IsDeleted &&
                e.IPDId != null &&
                e.IPDId == id
            ).OrderBy(e => e.Time).Select(e => new
            {
                e.Id,
                Time = e.Time?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                e.Diagnosis,
                e.FollowUpCarePlan,
                e.ParaClinicalTestsPlan,
                e.SpecialRequests,
                e.EducationPlan,
                e.ExpectedNumber,
                e.Prognosis,
                e.ExpectedOutcome,
                e.Note,
                ConfirmTime = e.ConfirmTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                e.CreatedBy,
                e.CreatedAt,
                e.UpdatedAt,
                e.UpdatedBy,
                IsDoctor = IsDoctor(e.CreatedBy),
                AllowDelete = e.CreatedBy == user.Username ? true: false
            });
             var last_plan_of_care = plan_of_care.OrderByDescending(x => x.UpdatedAt).FirstOrDefault();
            return Content(HttpStatusCode.OK, new
            {
                ipd.Id,
                Datas = plan_of_care,
                IsLocked = IPDIsBlock(ipd, Constant.IPDFormCode.KeHoachDieuTriChamSoc),
                last_plan_of_care?.CreatedAt,
                last_plan_of_care?.CreatedBy,
                last_plan_of_care?.UpdatedAt,
                last_plan_of_care?.UpdatedBy
            });
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/IPD/PlanOfCare/{id}")]
        [Permission(Code = "IPLOC2")]
        public IHttpActionResult UpdateIPDPlanOfCareAPI(Guid id, [FromBody]JObject request)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);

            var plan_of_care = unitOfWork.IPDPlanOfCareRepository.Find(
                e => !e.IsDeleted &&
                e.IPDId != null &&
                e.IPDId == id
            ).ToList();

            var user = GetUser();

            foreach (var item in request["Datas"])
            {
                var str_id = item["Id"]?.ToString();
                var data = GetOrCreatePlanOfCare(ipd.Id, str_id, plan_of_care);

                if (data == null) continue;

                UpdatePlanOfCare(data, item);
            }
            unitOfWork.Commit();

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpGet]
        [Route("api/IPD/PlanOfCare/SyncSpecialRequest/{id}")]
        [Permission(Code = "IPLOC3")]
        public IHttpActionResult GetIPDSyncSpecialRequest(Guid id)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);

            var results = new List<IPDIASpecialRequestModel>();

            var spec_req = unitOfWork.IPDInitialAssessmentSpecialRequestRepository.Find(
                e => !e.IsDeleted &&
                e.IPDId != null &&
                e.IPDId == ipd.Id
            ).OrderBy(e => e.Order);

            foreach(IPDInitialAssessmentSpecialRequest spec in spec_req)
            {
                var data = results.FirstOrDefault(e => e.Group == spec.Group);
                if (data == null)
                {

                    results.Add(new IPDIASpecialRequestModel
                    {
                        Group = spec.Group,
                        ViName = spec.ViName,
                        EnName = spec.EnName,
                        ViValue = spec.ViValue,
                        EnValue = spec.EnValue,
                        IsShow = spec.IsKey
                    });
                    continue;
                }

                string symbol = ", ";
                if (spec.DataType != null && spec.DataType.ToLower().Equals("text") && spec.ViValue != spec.EnValue)
                    symbol = ": ";
                data.IsShow = data.IsShow || spec.IsKey;
                if(data.EnValue == "Yes")
                {
                    data.ViValue = spec.ViValue;
                    data.EnValue = spec.EnValue;
                }
                else
                {
                    data.ViValue = $"{data.ViValue}{symbol}{spec.ViValue}";
                    data.EnValue = $"{data.EnValue}{symbol}{spec.EnValue}";
                }
            }

            return Content(HttpStatusCode.OK, results.Where(e => e.IsShow));
        }

        private IPDPlanOfCare GetOrCreatePlanOfCare(Guid ipd_id, string str_id, List<IPDPlanOfCare> plan_of_care)
        {
            if (string.IsNullOrEmpty(str_id))
            {
                IPDPlanOfCare data = new IPDPlanOfCare
                {
                    IPDId = ipd_id
                };
                unitOfWork.IPDPlanOfCareRepository.Add(data);
                return data;
            }

            var id = new Guid(str_id);
            return plan_of_care.FirstOrDefault(e => e.Id == id);
        }
        private void UpdatePlanOfCare(IPDPlanOfCare data, JToken item)
        {
            var time = item["Time"]?.ToString();
            if (string.IsNullOrEmpty(time))
                data.Time = null;
            else
                data.Time = DateTime.ParseExact(time, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);

            data.Diagnosis = item["Diagnosis"]?.ToString();
            data.FollowUpCarePlan = item["FollowUpCarePlan"]?.ToString();
            data.ParaClinicalTestsPlan = item["ParaClinicalTestsPlan"]?.ToString();
            data.SpecialRequests = item["SpecialRequests"]?.ToString();
            data.EducationPlan = item["EducationPlan"]?.ToString();
            data.ExpectedNumber = item["ExpectedNumber"]?.ToString();
            data.Prognosis = item["Prognosis"]?.ToString();
            data.ExpectedOutcome = item["ExpectedOutcome"]?.ToString();
            try
            {
                data.IsDeleted = string.IsNullOrEmpty(item["IsDeleted"].ToString()) ? false : Convert.ToBoolean(item["IsDeleted"].ToString());
            }
            catch(Exception)
            {
                data.IsDeleted = false;
            }
            string note = item["Note"]?.ToString();
            if (string.IsNullOrEmpty(note) || !"true,false".Contains(note.ToLower()))
                data.ConfirmTime = null;
            else if(note != data.Note)
                data.ConfirmTime = DateTime.Now;
            
            data.Note = note;
            unitOfWork.IPDPlanOfCareRepository.Update(data);
        }
    }
}
