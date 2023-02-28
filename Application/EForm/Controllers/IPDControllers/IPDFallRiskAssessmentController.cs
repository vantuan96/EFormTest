using DataAccess.Models.IPDModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers;
using EForm.Models.IPDModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class IPDFallRiskAssessmentController : BaseIPDApiController
    {
        [HttpGet]
        [Route("api/IPD/FallRiskAssessment/Info/{id}")]
        [Permission(Code = "IMMFS1")]
        public IHttpActionResult GetIPDFallRiskAssessmentInfoAPI(Guid id)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            return Content(HttpStatusCode.OK, new { IsLocked = IPDIsBlock(ipd, Constant.IPDFormCode.DanhGiaNguyCoNga) });
        }
        [HttpGet]
        [Route("api/IPD/FallRiskAssessment/Adult/{id}")]
        [Permission(Code = "IMMFS1")]
        public IHttpActionResult GetIPDFallRiskAssessmentForAdultAPI(Guid id)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var customer = ipd.Customer;

            var fall_risk = unitOfWork.IPDFallRiskAssessmentForAdultRepository.Find(
                e => !e.IsDeleted &&
                e.IPDId != null &&
                e.IPDId == ipd.Id &&
                e.FormType == "A02_048_301220_VE"
            ).OrderBy(e => e.CreatedAt).Select(e => new
            {
                DateOfBirth = customer.DateOfBirth?.ToString(Constant.DATE_FORMAT),
                e.Id,
                CreatedAt = e.CreatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                e.Total,
                e.Level,
                e.Intervention,
                e.CreatedBy,
                IsLocked = IPDIsBlock(ipd, Constant.IPDFormCode.DanhGiaNguyCoNga)
            });

            return Content(HttpStatusCode.OK, fall_risk);
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/IPD/FallRiskAssessment/Adult/Create/{id}")]
        [Permission(Code = "IMMFS2")]
        public IHttpActionResult CreateDetailIPDFallRiskAssessmentForAdultAPI(Guid id, [FromBody] JObject request)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var fall_risk = new IPDFallRiskAssessmentForAdult
            {
                IPDId = ipd.Id,
                Total = 0,
                FormType = "A02_048_301220_VE"
            };
            unitOfWork.IPDFallRiskAssessmentForAdultRepository.Add(fall_risk);

            HandleUpdateOrCreateFallRiskForAdult(fall_risk, request);

            return Content(HttpStatusCode.OK, new { fall_risk.Id });
        }

        [HttpGet]
        [Route("api/IPD/FallRiskAssessment/Adult/Detail/{id}")]
        [Permission(Code = "IMMFS3")]
        public IHttpActionResult GetDetailIPDFallRiskAssessmentForAdultAPI(Guid id)
        {
            var fall_risk = unitOfWork.IPDFallRiskAssessmentForAdultRepository.GetById(id);
            if (fall_risk == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_MMFS_NOT_FOUND);

            var ipd = fall_risk.IPD;
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var customer = ipd.Customer;

            var datas = fall_risk.IPDFallRiskAssessmentForAdultDatas
                .Where(e => !e.IsDeleted)
                .Select(e => new { e.Id, e.Code, e.Value, e.CreatedBy });

            return Content(HttpStatusCode.OK, new
            {
                DateOfBirth = customer.DateOfBirth?.ToString(Constant.DATE_FORMAT),
                fall_risk.Id,
                fall_risk.Total,
                fall_risk.Level,
                fall_risk.Intervention,
                Datas = datas
            });
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/IPD/FallRiskAssessment/Adult/Detail/{id}")]
        [Permission(Code = "IMMFS4")]
        public IHttpActionResult UpdateDetailIPDFallRiskAssessmentForAdultAPI(Guid id, [FromBody] JObject request)
        {
            var fall_risk = unitOfWork.IPDFallRiskAssessmentForAdultRepository.GetById(id);
            if (fall_risk == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_MMFS_NOT_FOUND);

            var user = GetUser();
            if (user.Username != fall_risk.CreatedBy)
                return Content(HttpStatusCode.BadRequest, Message.OWNER_FORBIDDEN);

            HandleUpdateOrCreateFallRiskForAdult(fall_risk, request);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpGet]
        [Route("api/IPD/FallRiskAssessment/Obstetric/{id}")]
        [Permission(Code = "IMOFR1")]
        public IHttpActionResult GetIPDFallRiskAssessmentForObstetricAPI(Guid id, [FromUri] FallRiskAssessmentParameter parames)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var customer = ipd.Customer;

            var fall_risk_query = unitOfWork.IPDFallRiskAssessmentForObstetricRepository.Find(
                e => !e.IsDeleted &&
                e.IPDId != null &&
                e.IPDId == ipd.Id
            ).Select(e => new {
                DateOfBirth = customer.DateOfBirth?.ToString(Constant.DATE_FORMAT),
                e.Id,
                CreatedAt = e.CreatedAt,
                e.Total,
                e.Level,
                e.Intervention,
                e.CreatedBy,
                IsLocked = IPDIsBlock(ipd, Constant.IPDFormCode.DanhGiaNguyCoNga),
                e.UpdatedAt,
                e.UpdatedBy
            });

            var lastForm = fall_risk_query.OrderByDescending(o => o.UpdatedAt).FirstOrDefault();
            var lastUpdate = new
            {
                UpdatedBy = lastForm?.UpdatedBy,
                UpdatedAt = lastForm?.UpdatedAt
            };

            if (!string.IsNullOrEmpty(parames.FromDate))
            {
                DateTime start_date = DateTime.ParseExact(parames.FromDate, "HH:mm dd/MM/yyyy", new CultureInfo("en-US"));
                fall_risk_query = fall_risk_query.Where(e => e.CreatedAt >= start_date);
            }

            if (!string.IsNullOrEmpty(parames.ToDate))
            {
                DateTime end_date = DateTime.ParseExact(parames.ToDate, "HH:mm dd/MM/yyyy", new CultureInfo("en-US"));
                fall_risk_query = fall_risk_query.Where(e => e.CreatedAt <= end_date.AddSeconds(59));
            }

            if (!string.IsNullOrEmpty(parames.Assessor))
                fall_risk_query = fall_risk_query.Where(e => e.CreatedBy == parames.Assessor);

            fall_risk_query = fall_risk_query.OrderByDescending(e => e.CreatedAt);
            var fall_risk = fall_risk_query.Skip((parames.PageNumber - 1) * parames.PageSize).Take(parames.PageSize).ToList();

            var Paging = new
            {
                Index = parames.PageNumber,
                TotalPage = fall_risk_query.Count() % parames.PageSize == 0 ? fall_risk_query.Count() / parames.PageSize : (fall_risk_query.Count() / parames.PageSize) + 1,
                Count = fall_risk_query.Count()
            };
      

            return Content(HttpStatusCode.OK, new { fall_risk, Paging, LastUpdated = lastUpdate});
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/IPD/FallRiskAssessment/Obstetric/Create/{id}")]
        [Permission(Code = "IMOFR2")]
        public IHttpActionResult CreateDetailIPDFallRiskAssessmentForObstetricAPI(Guid id, [FromBody] JObject request)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var fall_risk = new IPDFallRiskAssessmentForObstetric
            {
                IPDId = ipd.Id,
                Total = 0,
            };
            unitOfWork.IPDFallRiskAssessmentForObstetricRepository.Add(fall_risk);

            HandleUpdateOrCreateFallRiskForObstetric(fall_risk, request);

            return Content(HttpStatusCode.OK, new { fall_risk.Id });
        }

        [HttpGet]
        [Route("api/IPD/FallRiskAssessment/Obstetric/Detail/{id}")]
        [Permission(Code = "IMOFR3")]
        public IHttpActionResult GetDetailIPDFallRiskAssessmentForObstetricAPI(Guid id)
        {
            var fall_risk = unitOfWork.IPDFallRiskAssessmentForObstetricRepository.GetById(id);
            if (fall_risk == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_MOFR_NOT_FOUND);

            var ipd = fall_risk.IPD;
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var customer = ipd.Customer;

            var datas = fall_risk.IPDFallRiskAssessmentForObstetricDatas
                .Where(e => !e.IsDeleted)
                .Select(e => new { e.Id, e.Code, e.Value, e.CreatedBy });

            return Content(HttpStatusCode.OK, new
            {
                DateOfBirth = customer.DateOfBirth?.ToString(Constant.DATE_FORMAT),
                fall_risk.Id,
                fall_risk.Total,
                fall_risk.Level,
                fall_risk.Intervention,
                Datas = datas,
                fall_risk.CreatedBy,
                CreatedAt = fall_risk.CreatedAt?.ToString("HH:mm dd/MM/yyyy")
            });
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/IPD/FallRiskAssessment/Obstetric/Detail/{id}")]
        [Permission(Code = "IMOFR4")]
        public IHttpActionResult UpdateDetailIPDFallRiskAssessmentForObstetricAPI(Guid id, [FromBody] JObject request)
        {
            var fall_risk = unitOfWork.IPDFallRiskAssessmentForObstetricRepository.GetById(id);
            if (fall_risk == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_MOFR_NOT_FOUND);

            var user = GetUser();
            if (user.Username != fall_risk.CreatedBy)
                return Content(HttpStatusCode.BadRequest, Message.OWNER_FORBIDDEN);

            HandleUpdateOrCreateFallRiskForObstetric(fall_risk, request);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }


        private void HandleUpdateOrCreateFallRiskForAdult(IPDFallRiskAssessmentForAdult fall_risk, JObject request)
        {
            var total = request["Total"]?.ToString();
            if (string.IsNullOrEmpty(total))
                fall_risk.Total = 0;
            else
                fall_risk.Total = int.Parse(total);

            fall_risk.Level = request["Level"]?.ToString();
            fall_risk.Intervention = request["Intervention"]?.ToString();

            var datas = fall_risk.IPDFallRiskAssessmentForAdultDatas.Where(e => !e.IsDeleted).ToList();

            foreach (var item in request["Datas"])
            {
                var code = item["Code"]?.ToString();
                if (string.IsNullOrEmpty(code)) continue;

                var data = GetOrCreateFallRiskAssementForAdultData(code, fall_risk.Id, datas);
                if (data == null) continue;

                UpdateFallRiskAssementForAdultData(item["Value"]?.ToString(), data);
            }
            unitOfWork.IPDFallRiskAssessmentForAdultRepository.Update(fall_risk);
            unitOfWork.Commit();
        }
        private IPDFallRiskAssessmentForAdultData GetOrCreateFallRiskAssementForAdultData(string code, Guid form_id, List<IPDFallRiskAssessmentForAdultData> datas)
        {
            var data = datas.FirstOrDefault(e => e.Code == code);
            if (data != null) return data;

            data = new IPDFallRiskAssessmentForAdultData
            {
                IPDFallRiskAssessmentForAdultId = form_id,
                Code = code
            };
            unitOfWork.IPDFallRiskAssessmentForAdultDataRepository.Add(data);
            return data;
        }
        private void UpdateFallRiskAssementForAdultData(string value, IPDFallRiskAssessmentForAdultData data)
        {
            data.Value = value;
            unitOfWork.IPDFallRiskAssessmentForAdultDataRepository.Update(data);
        }


        private void HandleUpdateOrCreateFallRiskForObstetric(IPDFallRiskAssessmentForObstetric fall_risk, JObject request)
        {
            var total = request["Total"]?.ToString();
            if (string.IsNullOrEmpty(total))
                fall_risk.Total = 0;
            else
                fall_risk.Total = int.Parse(total);

            fall_risk.Level = request["Level"]?.ToString();
            fall_risk.Intervention = request["Intervention"]?.ToString();

            var datas = fall_risk.IPDFallRiskAssessmentForObstetricDatas.Where(e => !e.IsDeleted).ToList();

            foreach (var item in request["Datas"])
            {
                var code = item["Code"]?.ToString();
                if (string.IsNullOrEmpty(code)) continue;

                var data = GetOrCreateFallRiskAssementForObstetricData(code, fall_risk.Id, datas);
                if (data == null) continue;

                UpdateFallRiskAssementForObstetricData(item["Value"]?.ToString(), data);
            }
            unitOfWork.IPDFallRiskAssessmentForObstetricRepository.Update(fall_risk);
            unitOfWork.Commit();
        }
        private IPDFallRiskAssessmentForObstetricData GetOrCreateFallRiskAssementForObstetricData(string code, Guid form_id, List<IPDFallRiskAssessmentForObstetricData> datas)
        {
            var data = datas.FirstOrDefault(e => e.Code == code);
            if (data != null) return data;

            data = new IPDFallRiskAssessmentForObstetricData
            {
                IPDFallRiskAssessmentForObstetricId = form_id,
                Code = code
            };
            unitOfWork.IPDFallRiskAssessmentForObstetricDataRepository.Add(data);
            return data;
        }
        private void UpdateFallRiskAssementForObstetricData(string value, IPDFallRiskAssessmentForObstetricData data)
        {
            data.Value = value;
            unitOfWork.IPDFallRiskAssessmentForObstetricDataRepository.Update(data);
        }

        public class FallRiskAssessmentParameter : IPDThrombosisRiskFactorAssessmentParams
        {
        }
    }
}
