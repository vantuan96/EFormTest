using DataAccess.Models.IPDModel;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Common;
using EForm.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class IPDFallRiskAssessmentForPedatricController : BaseApiController
    {
        [HttpGet]
        [Route("api/IPD/FallRiskAssessment/Pediatric/All")]
        [Permission(Code = "IPDXEM")]
        public IHttpActionResult GetIPDFallRiskAssessmentForAdultAPI([FromUri] IPDFallRiskAssessmentForPedatricParam request)
        {
            Guid visitId = request.VisitId;
            var ipd = GetIPD(visitId);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var fallRicks = FillterSearch(request, ipd);
            int number_fallRicks = fallRicks.Count;
            int numberPage = number_fallRicks % request.PageSize == 0 ? number_fallRicks / request.PageSize : number_fallRicks / request.PageSize + 1;

            fallRicks = fallRicks.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList();

            return Content(HttpStatusCode.OK, new
            {
                fallRicks,
                Paging = new { numberPage, request.PageNumber },
                IsLocked = IPDIsBlock(ipd, Constant.IPDFormCode.DanhGiaNguyCoNgaNBNhi),
            });
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/IPD/FallRiskAssessment/Pediatric/Create/{id}")]
        [Permission(Code = "IPDDDCS")]
        public IHttpActionResult CreateDetailIPDFallRiskAssessmentForAdultAPI(Guid id, [FromBody] JObject request)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            if (IPDIsBlock(ipd, Constant.IPDFormCode.DanhGiaNguyCoNgaNBNhi))
                return Content(HttpStatusCode.BadRequest, Message.FORM_IS_LOCKED);

            var fall_risk = new IPDFallRiskAssessmentForAdult
            {
                IPDId = ipd.Id,
                Total = 0,
                FormType = "A02_047_301220_VE",
            };
            unitOfWork.IPDFallRiskAssessmentForAdultRepository.Add(fall_risk);

            HandleUpdateOrCreateFallRiskForAdult(fall_risk, request);
            CreateOrUpdateFormForSetupOfAdmin(ipd.Id, fall_risk.Id, Constant.IPDFormCode.DanhGiaNguyCoNgaNBNhi);
            return Content(HttpStatusCode.OK, new { fall_risk.Id });
        }

        [HttpGet]
        [Route("api/IPD/FallRiskAssessment/Pediatric/Detail/{id}")]
        [Permission(Code = "IPDXEM")]
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
                Gender = customer.Gender,
                fall_risk.Id,
                fall_risk.Total,
                fall_risk.Level,
                fall_risk.Intervention,
                Datas = datas,
                IsLocked = IPDIsBlock(fall_risk.IPD, Constant.IPDFormCode.DanhGiaNguyCoNgaNBNhi),
                fall_risk.CreatedBy,
                TimeAssessment = datas?.FirstOrDefault(d => d.Code == "TIMEASSESSMENT") == null ? null : datas.FirstOrDefault(d => d.Code == "TIMEASSESSMENT").Value
            });
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/IPD/FallRiskAssessment/Pediatric/Update/{id}")]
        [Permission(Code = "IPDDDCS")]
        public IHttpActionResult UpdateDetailIPDFallRiskAssessmentForAdultAPI(Guid id, [FromBody] JObject request)
        {
            var fall_risk = unitOfWork.IPDFallRiskAssessmentForAdultRepository.GetById(id);
            if (fall_risk == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_MMFS_NOT_FOUND);

            if (IPDIsBlock(fall_risk.IPD, Constant.IPDFormCode.DanhGiaNguyCoNgaNBNhi))
                return Content(HttpStatusCode.BadRequest, Message.FORM_IS_LOCKED);

            var user = GetUser();
            if (user.Username != fall_risk.CreatedBy)
                return Content(HttpStatusCode.BadRequest, Message.OWNER_FORBIDDEN);

            HandleUpdateOrCreateFallRiskForAdult(fall_risk, request);
            CreateOrUpdateFormForSetupOfAdmin(fall_risk.IPD.Id, fall_risk.Id, Constant.IPDFormCode.DanhGiaNguyCoNgaNBNhi);
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpGet]
        [Route("api/IPD/FallRiskAssessment/Pediatric/Infor/{visitId}")]
        [Permission(Code = "IPDXEM")]
        public IHttpActionResult GetInfor(Guid visitId)
        {
            IPD ipd = GetIPD(visitId);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var custumer = ipd.Customer;
            return Content(HttpStatusCode.OK, new { DateOfBirth = custumer?.DateOfBirth?.ToString(Constant.DATE_FORMAT), custumer?.Gender });
        }

        private void HandleUpdateOrCreateFallRiskForAdult(IPDFallRiskAssessmentForAdult fall_risk, JObject request)
        {
            DateTime time;
            bool success = DateTime.TryParseExact(request["TimeAssessment"].ToString(), "HH:mm dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out time);
            if (!success)
                time = DateTime.Now;

            fall_risk.CreatedAt = time;
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

            var checkObjTimeAssessment = unitOfWork.IPDFallRiskAssessmentForAdultDataRepository
                                        .FirstOrDefault(t => !t.IsDeleted && t.IPDFallRiskAssessmentForAdultId == fall_risk.Id && t.Code == "TIMEASSESSMENT");
            if (checkObjTimeAssessment == null)
            {
                var dataTimeInitAssessment = new IPDFallRiskAssessmentForAdultData()
                {
                    IPDFallRiskAssessmentForAdultId = fall_risk.Id,
                    Code = "TIMEASSESSMENT",
                    Value = time.ToString("HH:mm dd/MM/yyyy")
                };
                unitOfWork.IPDFallRiskAssessmentForAdultDataRepository.Add(dataTimeInitAssessment);
            }
            else
            {
                checkObjTimeAssessment.Value = time.ToString("HH:mm dd/MM/yyyy");
                unitOfWork.IPDFallRiskAssessmentForAdultDataRepository.Update(checkObjTimeAssessment);
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

        private List<dynamic> FillterSearch(IPDFallRiskAssessmentForPedatricParam reques, IPD ipd)
        {

            var fallRisks = unitOfWork.IPDFallRiskAssessmentForAdultRepository.AsQueryable()
                            .Where(
                                e => !e.IsDeleted &&
                                e.IPDId == ipd.Id &&
                                e.FormType == "A02_047_301220_VE"
                              ).ToList()
                              .Select(e => new
                              {
                                  e.Id,
                                  CreatedAt = e.CreatedAt,
                                  e.Total,
                                  e.Level,
                                  e.Intervention,
                                  e.CreatedBy,
                                  TimeAssessment = e.IPDFallRiskAssessmentForAdultDatas?.FirstOrDefault(d => d.Code == "TIMEASSESSMENT") == null ? "00:00 01/01/0001" : e.IPDFallRiskAssessmentForAdultDatas?.FirstOrDefault(d => d.Code == "TIMEASSESSMENT").Value
                              }).ToList();

            var list_fallRisks = (from f in fallRisks
                                  select new
                                  {
                                      Id = f.Id,
                                      CreatedAt = f.CreatedAt,
                                      Total = f.Total,
                                      Level = f.Level,
                                      Intervention = f.Intervention,
                                      CreatedBy = f.CreatedBy,
                                      TimeAssessment = DateTime.ParseExact(f.TimeAssessment, "HH:mm dd/MM/yyyy", new CultureInfo("en-US"))
                                  }).ToList();


            if (list_fallRisks == null || list_fallRisks.Count == 0)
                return new List<dynamic>();

            DateTime? fromDate = null;
            DateTime? toDate = null;
            string assessor = reques.Assessor;

            if (reques.FromDate != null)
            {
                fromDate = DateTime.ParseExact(reques.FromDate.ToString(), "HH:mm dd/MM/yyyy", new CultureInfo("en-US"));
            }

            if (reques.ToDate != null)
            {
                toDate = DateTime.ParseExact(reques.ToDate.ToString(), "HH:mm dd/MM/yyyy", new CultureInfo("en-US"));
            }

            if (fromDate != null)
                list_fallRisks = list_fallRisks.Where(f => f.TimeAssessment >= fromDate).ToList();

            if (toDate != null)
                list_fallRisks = list_fallRisks.Where(f => f.TimeAssessment <= toDate).ToList();

            if (assessor != null)
                list_fallRisks = list_fallRisks.Where(f => f.CreatedBy.ToUpper() == assessor.ToUpper()).ToList();

            var customer = ipd.Customer;
            var list = (from l in list_fallRisks
                        orderby l.CreatedAt descending
                        select new
                        {
                            DateOfBirth = customer.DateOfBirth?.ToString(Constant.DATE_FORMAT),
                            Id = l.Id,
                            CreatedAt = l.CreatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                            CreatedBy = l.CreatedBy,
                            Total = l.Total,
                            Level = l.Level,
                            Intervention = l.Intervention,
                            TimeAssessment = l.TimeAssessment.ToString("HH:mm dd/MM/yyyy")
                        }).ToList();

            var list_result = new List<dynamic>(list);
            return list_result;
        }

        public class IPDFallRiskAssessmentForPedatricParam : PagingParameterModel
        {
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public string Assessor { get; set; }
            public Guid VisitId { get; set; }
        }
    }
}
