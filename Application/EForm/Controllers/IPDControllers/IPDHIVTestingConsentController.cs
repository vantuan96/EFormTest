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
    public class IPDHIVTestingConsentController : EIOAllFormController
    {
        private readonly string vistiType = "IPD";
        private readonly string formCode = "A01_014_050919_VE";

        [HttpGet]
        [Route("api/IPD/HIVTestingConsent/Get/{visitId}")]
        [Permission(Code = "IPDHIVTCG")]
        public IHttpActionResult IPDGetFormByVisitId(Guid visitId)
        {
            return GetFormAPI(visitId, vistiType, formCode);
        }
        [HttpPost]
        [Route("api/IPD/HIVTestingConsent/Create/{visitId}")]
        [Permission(Code = "IPDHIVTCC")]
        public IHttpActionResult CreateForm(Guid visitId)
        {
            return CreateAPI(visitId, vistiType, formCode);
        }

        [HttpPost]
        [Route("api/IPD/HIVTestingConsent/Update/{visitId}")]
        [Permission(Code = "IPDHIVTCU")]
        public IHttpActionResult IPDUpdate(Guid visitId ,[FromBody] JObject request)
        {
            return UpdateAPI(visitId , request, vistiType, formCode);
        }      

        [HttpGet]
        [Route("api/IPD/HIVTestingConsent/GetInfo/{visitId}")]
        public IHttpActionResult IPDGetGetInfo(Guid visitId)
        {
            var visit = GetVisit(visitId, "IPD");
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Common.Message.IPD_NOT_FOUND);
            var CheckFormLocked = IPDIsBlock(visit, formCode);
            return Content(HttpStatusCode.OK, CheckFormLocked);
        }
    }
}