using EForm.Authentication;
using EForm.Controllers.EIOControllers;
using EForm.Controllers.GeneralControllers;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.ServiceModel.Channels;
using System.Web.Http;
using EForm.Common;

namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class IPDPROMForheartFailureController : PROMForheartFailureController
    {
        private readonly string vistiType = "IPD";       

        [HttpGet]
        [Route("api/IPD/PROMForheartFailure/GetDetail/{visitId}")]
        [Permission(Code = "IPDPFFGMM")]
        public IHttpActionResult IPDGetFormByVisitId(Guid visitId)
        {
            return GetFormByVisitId(visitId, vistiType);
        }
        [HttpPost]
        [Route("api/IPD/PROMForheartFailure/Create/{visitId}")]
        [Permission(Code = "IPDPFFCMM")]
        public IHttpActionResult CreateForm(Guid visitId)
        {
            return CreateForm(visitId, vistiType);
        }

        [HttpPost]
        [Route("api/IPD/PROMForheartFailure/Update/{visitId}")]
        [Permission(Code = "IPDPFFUMM")]
        public IHttpActionResult IPDUpdate(Guid visitId ,[FromBody] JObject request)
        {
            return UpdateForm(visitId ,vistiType, request);
        }

        [HttpPost]
        [Route("api/IPD/PROMForheartFailure/Confirm/{visitId}")]
        [Permission(Code = "IPDPFFCMMF")]
        public IHttpActionResult IPDConfirmAPI(Guid visitId,[FromBody] JObject request)
        {
            return ConfirmAPI(visitId, vistiType, request);
        }

        [HttpGet]
        [Route("api/IPD/PROMForheartFailure/GetInfo/{visitId}")]
        public IHttpActionResult IPDGetGetInfo(Guid visitId)
        {
            var visit = GetVisit(visitId, "IPD");
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Common.Message.IPD_NOT_FOUND);
            var CheckFormLocked = IPDIsBlock(visit, "PROMFHF");
            return Content(HttpStatusCode.OK, CheckFormLocked);
        }
    }
}