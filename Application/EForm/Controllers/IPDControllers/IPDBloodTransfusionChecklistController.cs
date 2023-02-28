using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers.BaseEIOControllers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class IPDBloodTransfusionChecklistController : EIOBloodTransfusionChecklistController
    {
        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/BloodTransfusionChecklist/Create/{id}")]
        [Permission(Code = "IBLTC1")]
        public IHttpActionResult CreateBloodTransfusionChecklistAPI(Guid id)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            if (IPDIsBlock(ipd, Constant.IPDFormCode.PhieuTruyenMau))
            {
                return Content(HttpStatusCode.BadRequest, Message.FORM_IS_LOCKED);
            }
            var brsac = GetBloodRequestSupplyAndConfirmation(ipd.Id, "IPD");
            if (brsac == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_BRSAC_NOT_FOUND);

            var btc = CreateEIOBloodTransfusionChecklist(ipd.Id, "IPD", ipd.SpecialtyId);
            var detail = GetEIOBloodTransfusionChecklistDetail(btc, null, ipd.IPDMedicalRecordId);

            return Content(HttpStatusCode.OK, detail);
        }

        [HttpGet]
        [Route("api/IPD/BloodTransfusionChecklist/{id}")]
        [Permission(Code = "IBLTC2")]
        public IHttpActionResult GetBloodTransfusionChecklistAPI(Guid id)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var brsac = GetBloodRequestSupplyAndConfirmation(ipd.Id, "IPD");
            bool check_isExistBrsac = true;
            if (brsac == null)
                check_isExistBrsac = false;

            var IsLocked = IPDIsBlock(ipd, Constant.IPDFormCode.PhieuTruyenMau);            
            var btc_id = unitOfWork.EIOBloodTransfusionChecklistRepository.Find(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == ipd.Id &&
                !string.IsNullOrEmpty(e.VisitTypeGroupCode) &&
                e.VisitTypeGroupCode == "IPD"
            ).OrderBy(e => e.CreatedAt).Select(e => new { e.Id, e.CreatedAt, e.CreatedBy });
            if (btc_id.Count() == 0 && IPDIsBlock(ipd, Constant.IPDFormCode.PhieuTruyenMau))
            {
                return Content(HttpStatusCode.BadRequest, new { IsLocked });
            }
        
            return Content(HttpStatusCode.OK, new { Datas = btc_id, IsExistBrsac = check_isExistBrsac });
        }

        [HttpGet]
        [Route("api/IPD/BloodTransfusionChecklist/Detail/{id}")]
        [Permission(Code = "IBLTC2")]
        public IHttpActionResult GetBloodTransfusionChecklistDetailAPI(Guid id)
        {
            var btc = unitOfWork.EIOBloodTransfusionChecklistRepository.GetById(id);
            if (btc == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_BTC_NOT_FOUND);

            var ipd = GetIPD((Guid)btc.VisitId);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);            
            bool IsLocked = IPDIsBlock(ipd, Constant.IPDFormCode.PhieuTruyenMau, id);
            var detail = GetEIOBloodTransfusionChecklistDetail(btc, null, ipd.IPDMedicalRecordId, IsLocked);

            return Content(HttpStatusCode.OK, detail);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/BloodTransfusionChecklist/Detail/{id}")]
        [Permission(Code = "IBLTC4")]
        public IHttpActionResult UpdateBloodTransfusionChecklistDetailAPI(Guid id, [FromBody] JObject request)
        {
            var btc = unitOfWork.EIOBloodTransfusionChecklistRepository.GetById(id);
            if (btc == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_BTC_NOT_FOUND);
            var ipd = GetIPD((Guid)btc.VisitId);
            if (IPDIsBlock(ipd, Constant.IPDFormCode.PhieuTruyenMau, id))            
                return Content(HttpStatusCode.BadRequest, Message.FORM_IS_LOCKED);
            var error = UpdateEIOBloodTransfusionChecklist(btc, request);
            if (error != null)
                return Content(HttpStatusCode.OK, Message.SUCCESS);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/BloodTransfusionChecklist/Confirm/{id}")]
        [Permission(Code = "IBLTC5")]
        public IHttpActionResult ConfirmBloodTransfusionChecklistDetailAPI(Guid id, [FromBody] JObject request)
        {
            var btc = unitOfWork.EIOBloodTransfusionChecklistRepository.GetById(id);
            if (btc == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_BTC_NOT_FOUND);
            var ipd = GetIPD((Guid)btc.VisitId);
            if (IPDIsBlock(ipd, Constant.IPDFormCode.PhieuTruyenMau, id))            
                return Content(HttpStatusCode.BadRequest, Message.FORM_IS_LOCKED);            
            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var kind = request["kind"]?.ToString();
            if (string.IsNullOrEmpty(kind))
                return Content(HttpStatusCode.BadRequest, Message.OWNER_FORBIDDEN);

            var user = GetAcceptUser(username, password);
            if (user == null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);                     
            var positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName).ToList();
            var success = ConfirmEIOBloodTransfusionChecklist(btc, user.Id, positions, kind); 
            if (success)
                return Content(HttpStatusCode.OK, Message.SUCCESS);
           
            return Content(HttpStatusCode.BadRequest, Message.OWNER_FORBIDDEN);
        }
    }
}
