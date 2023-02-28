using EForm.Authentication;
using EForm.Controllers.EIOControllers;
using EForm.Controllers.GeneralControllers;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.OPDControllers
{
    [SessionAuthorize]
    public class OPDPROMForheartFailureController : PROMForheartFailureController
    {
        private readonly string vistiType = "OPD";

        [HttpGet]
        [Route("api/OPD/PROMForheartFailure/GetDetail/{visitId}")]
        [Permission(Code = "OPDPFFGMM")]
        public IHttpActionResult IPDGetFormByVisitId(Guid visitId)
        {
            return GetFormByVisitId(visitId, vistiType);
        }
        [HttpPost]
        [Route("api/OPD/PROMForheartFailure/Create/{visitId}")]
        [Permission(Code = "OPDPFFCMM")]
        public IHttpActionResult CreateForm(Guid visitId)
        {
            return CreateForm(visitId, vistiType);
        }

        [HttpPost]
        [Route("api/OPD/PROMForheartFailure/Update/{visitId}")]
        [Permission(Code = "OPDPFFUMM")]
        public IHttpActionResult IPDUpdate(Guid visitId, [FromBody] JObject request)
        {
            return UpdateForm(visitId, vistiType, request);
        }

        [HttpPost]
        [Route("api/OPD/PROMForheartFailure/Confirm/{visitId}")]
        [Permission(Code = "OPDPFFCMMF")]
        public IHttpActionResult IPDConfirmAPI(Guid visitId, [FromBody] JObject request)
        {
            return ConfirmAPI(visitId, vistiType, request);
        }

        [HttpGet]
        [Route("api/OPD/PROMForheartFailure/GetInfo/{visitId}")]
        public IHttpActionResult OPDGetGetInfo(Guid visitId)
        {
            var visit = GetVisit(visitId, "OPD");
            var user = GetUser();
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Common.Message.OPD_NOT_FOUND);
            var is_block_after_24h = Is24hLocked(visit.CreatedAt, visitId, "PROMFHF", user.Username);
            return Content(HttpStatusCode.OK, is_block_after_24h);
        }
    }
}