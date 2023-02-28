using DataAccess.Models.IPDModel;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class IPDGuggingSwallowingScreenController : BaseApiController
    {
        [HttpGet]
        [Route("api/IPD/GuggingSwallowingScreen/{type}/{visitId}")]
        [Permission(Code = "IGUSS2")]
        public IHttpActionResult GetAllGuggingSwallowingScreenAPI(string type, Guid visitId)
        {
            var ipd = GetIPD(visitId);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var gussList = unitOfWork.IPDGuggingSwallowingScreenRepository.Find(e => !e.IsDeleted && e.IPDId == visitId).OrderBy(o => o.CreatedAt).ToList().Select(guss => new
            {
                guss.Id,
                guss.IPDId,
                guss.CreatedBy,
                CreatedAt = guss.CreatedAt,
                UpdatedAt = guss.UpdatedAt
            }).ToList();
            return Content(HttpStatusCode.OK, new
            {
                Datas = gussList,
                IsLocked = IPDIsBlock(ipd, Constant.IPDFormCode.ThangDiemGUSSRoiLoanNuot),
            });
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/IPD/GuggingSwallowingScreen/Create/{type}/{visitId}")]
        [Permission(Code = "IGUSS1")]
        public IHttpActionResult CreateGuggingSwallowingScreenAPI(string type,Guid visitId)
        {
            var ipd = GetIPD(visitId);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            //var guss = GetIPDGuggingSwallowingScreen(visitId);
            //if(guss!=null)
            //    return Content(HttpStatusCode.BadRequest, Message.IPD_GUSS_EXIST);

            var guss = new IPDGuggingSwallowingScreen
            {
                IPDId = visitId
            };
            unitOfWork.IPDGuggingSwallowingScreenRepository.Add(guss);
            unitOfWork.Commit();

            return Content(HttpStatusCode.OK, new
            {
                guss.Id,
                guss.IPDId,
                guss.CreatedBy,
                guss.CreatedAt
            });
        }
        [HttpGet]
        [Route("api/IPD/GuggingSwallowingScreen/CheckFormLocked/{type}/{visitId}/{id}")]
        [Permission(Code = "IGUSS2")]
        public IHttpActionResult CheckFormLockedAPI(string type,Guid visitId, Guid id)
        {
            var ipd = GetIPD(visitId);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);
            return Content(HttpStatusCode.OK, new
            {
                IsLocked = IPDIsBlock(ipd, Constant.IPDFormCode.ThangDiemGUSSRoiLoanNuot)
            });
        }
        [HttpGet]
        [Route("api/IPD/GuggingSwallowingScreen/{type}/{visitId}/{id}")]
        [Permission(Code = "IGUSS2")]
        public IHttpActionResult GetGuggingSwallowingScreenAPI(string type,Guid visitId, Guid id)
        {
            var ipd = GetIPD(visitId);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var guss = GetIPDGuggingSwallowingScreen(id);
            if (guss == null)
                return Content(HttpStatusCode.BadRequest, new
                {
                    ViMessage = "Đánh giá rối loạn nuốt không tồn tại",
                    EnMessage = "GUSS is not found",
                    IsLocked = IPDIsBlock(ipd, Constant.IPDFormCode.ThangDiemGUSSRoiLoanNuot)
                });

            var datas = guss.IPDGuggingSwallowingScreenDatas.Where(e => !e.IsDeleted).Select(e => new { e.Code, e.Value, e.Id });

            return Content(HttpStatusCode.OK, new {
                guss.Id,
                guss.UpdatedBy,
                UpdatedAt = guss.UpdatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                IsLocked = IPDIsBlock(ipd, Constant.IPDFormCode.ThangDiemGUSSRoiLoanNuot),
                Datas = datas
            });
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/IPD/GuggingSwallowingScreen/Update/{type}/{visitId}/{id}")]
        [Permission(Code = "IGUSS3")]
        public IHttpActionResult UpdateGuggingSwallowingScreenAPI(string type,Guid visitId,Guid id, [FromBody]JObject request)
        {
            var ipd = GetIPD(visitId);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var guss = GetIPDGuggingSwallowingScreen(id);
            if (guss == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_GUSS_NOT_FOUND);
            if (IPDIsBlock(ipd, Constant.IPDFormCode.ThangDiemGUSSRoiLoanNuot))
                return Content(HttpStatusCode.Forbidden, Message.FORM_IS_LOCKED);

            var user = GetUser();
            if (user.Username != guss.CreatedBy)
                return Content(HttpStatusCode.BadRequest, Message.OWNER_FORBIDDEN);

            HandleUpdateGuggingSwallowingScreenData(guss, user.Username, request["Datas"]);

            return Content(HttpStatusCode.OK, new
            {
                guss.Id,
                guss.IPDId,
                guss.UpdatedAt
            });
        }


        private IPDGuggingSwallowingScreen GetIPDGuggingSwallowingScreen(Guid id)
        {
            return unitOfWork.IPDGuggingSwallowingScreenRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.Id != null &&
                e.Id == id
            );
        }

        private void HandleUpdateGuggingSwallowingScreenData(IPDGuggingSwallowingScreen guss, string username, JToken request_data)
        {
            var datas = guss.IPDGuggingSwallowingScreenDatas.Where(e => !e.IsDeleted).ToList();

            foreach(var item in request_data)
            {
                var item_code = item.Value<string>("Code");
                if (string.IsNullOrEmpty(item_code)) continue;

                var data = GetOrCreateGuggingSwallowingScreenData(datas, guss.Id, item_code);
                if (data != null)
                    UpdateGuggingSwallowingScreenData(data, item.Value<string>("Value"));
            }
            guss.UpdatedBy = username;
            unitOfWork.IPDGuggingSwallowingScreenRepository.Update(guss);
            unitOfWork.Commit();
        }
        private IPDGuggingSwallowingScreenData GetOrCreateGuggingSwallowingScreenData(List<IPDGuggingSwallowingScreenData> list_datas, Guid form_id, string code)
        {
            var data = list_datas.FirstOrDefault(e => !string.IsNullOrEmpty(e.Code) && e.Code == code);
            if (data != null) return data;

            data = new IPDGuggingSwallowingScreenData()
            {
                IPDGuggingSwallowingScreenId = form_id,
                Code = code
            };
            unitOfWork.IPDGuggingSwallowingScreenDataRepository.Add(data);
            return data;
        }
        private void UpdateGuggingSwallowingScreenData(IPDGuggingSwallowingScreenData data, string value)
        {
            data.Value = value;
            unitOfWork.IPDGuggingSwallowingScreenDataRepository.Update(data);
        }
    }
}
