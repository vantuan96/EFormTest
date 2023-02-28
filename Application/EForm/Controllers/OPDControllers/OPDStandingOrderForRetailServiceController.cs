using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers.BaseEIOControllers;
using EForm.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.OPDControllers
{
    [SessionAuthorize]
    public class OPDStandingOrderForRetailServiceController : EIOStandingOrderForRetailServiceController
    {
        private InHospital in_hospital = new InHospital();

        [HttpGet]
        [Route("api/OPD/StandingOrderForRetailService/{id}")]
        [Permission(Code = "OSORS1")]
        public IHttpActionResult GetOPDStandingOrderForRetailServiceAPI(Guid id)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);
            
            var sofrs = GetRetailService(opd, "OPD");
            if (sofrs == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_SOFRS_NOT_FOUND);

            return Content(HttpStatusCode.OK, GetDetailRetailService(opd, Constant.OPD_RETAIL_SERVICE_ORDER, sofrs, CheckIsBlock(opd)));
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/StandingOrderForRetailService/{id}")]
        [Permission(Code = "OSORS2")]
        public IHttpActionResult UpdateOPDStandingOrderForRetailServiceAPI(Guid id, JObject request)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var sofrs = opd.EIOStandingOrderForRetailService;
            if (sofrs == null || sofrs.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.EIO_SOFRS_NOT_FOUND);

            if (CheckIsBlock(opd))
                return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);

            UpdateRetailService(sofrs, request);

            HandleUpdateOrCreateOrderData(opd.Id, Constant.OPD_RETAIL_SERVICE_ORDER, request["Datas"]);

            var error_mes = HandleStatus(opd, "OPD", request);
            if (error_mes != null)
                return Content(HttpStatusCode.BadRequest, error_mes);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/OPD/StandingOrderForRetailService/Confirm/{id}")]
        [Permission(Code = "OSORS3")]
        public IHttpActionResult ConfirmStandingOrderForRetailServiceAPI(Guid id, [FromBody]JObject request)
        {
            var order = unitOfWork.OrderRepository.GetById(id);
            if (order == null)
                return Content(HttpStatusCode.NotFound, Message.ORDER_NOT_FOUND);
            var opd = GetOPD((Guid)order.VisitId);

            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var kind = request["kind"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null || string.IsNullOrEmpty(kind))
                return Content(HttpStatusCode.NotFound, Message.INFO_INCORRECT);
            if (CheckIsBlock(opd))
                return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);

            var positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName);
            if ((kind == "Doctor" && !positions.Contains("Doctor")) || (kind == "Nurse" && !positions.Contains("Nurse")))
                return Content(HttpStatusCode.NotFound, Message.FORBIDDEN);

            return Content(HttpStatusCode.OK, user.Fullname);
        }

        private bool CheckIsBlock(dynamic visit, Guid? formId = null)
        {
            var user = GetUser();
            var is_block_after_24h = IsBlockAfter24h(visit.CreatedAt, formId);
            var has_unlock_permission = HasUnlockPermission(visit.Id, "OPDSORS", user.Username, formId);
            if (!has_unlock_permission && is_block_after_24h)
                return true;
            return false;
        }
    }
}
