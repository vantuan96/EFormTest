using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers.BaseEIOControllers;
using EForm.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.EDControllers
{
    [SessionAuthorize]
    public class EDStandingOrderForRetailServiceController : EIOStandingOrderForRetailServiceController
    {
        private InHospital in_hospital = new InHospital();

        [HttpGet]
        [Route("api/ED/StandingOrderForRetailService/{id}")]
        [Permission(Code = "ESORS1")]
        public IHttpActionResult GetEDStandingOrderForRetailServiceAPI(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var sofrs = GetRetailService(ed, "ED");
            if (sofrs == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_SOFRS_NOT_FOUND);

            return Content(HttpStatusCode.OK, GetDetailRetailService(ed, Constant.ED_RETAIL_SERVICE_ORDER, sofrs));
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/StandingOrderForRetailService/{id}")]
        [Permission(Code = "ESORS2")]
        public IHttpActionResult UpdateEDStandingOrderForRetailServiceAPI(Guid id, JObject request)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var sofrs = ed.EDStandingOrderForRetailService;
            if (sofrs == null || sofrs.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.EIO_SOFRS_NOT_FOUND);

            UpdateRetailService(sofrs, request);

            HandleUpdateOrCreateOrderData(ed.Id, Constant.ED_RETAIL_SERVICE_ORDER, request["Datas"]);

            var error_mes = HandleStatus(ed, "ED", request);
            if (error_mes != null)
                return Content(HttpStatusCode.BadRequest, error_mes);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/ED/StandingOrderForRetailService/Confirm/{id}")]
        [Permission(Code = "ESORS3")]
        public IHttpActionResult ConfirmStandingOrderForRetailServiceAPI(Guid id, [FromBody]JObject request)
        {
            var order = unitOfWork.OrderRepository.GetById(id);
            if (order == null)
                return Content(HttpStatusCode.NotFound, Message.ORDER_NOT_FOUND);

            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var kind = request["kind"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null || string.IsNullOrEmpty(kind))
                return Content(HttpStatusCode.NotFound, Message.INFO_INCORRECT);

            var positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName);
            if ((kind == "Doctor" && !positions.Contains("Doctor")) || (kind == "Nurse" && !positions.Contains("Nurse")))
                return Content(HttpStatusCode.NotFound, Message.FORBIDDEN);

            return Content(HttpStatusCode.OK, user.Fullname);
        }
    }
}
