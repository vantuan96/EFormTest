using EForm.Authentication;
using EForm.Controllers.EIOControllers;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Web.Http;


namespace EForm.Controllers.EDControllers
{
    [SessionAuthorize]
    public class EDHIVTestingConsentController : EIOAllFormController
    {
        private readonly string vistiType = "ED";
        private readonly string formCode = "A01_014_050919_VE";
        [HttpGet]
        [Route("api/ED/HIVTestingConsent/Get/{visitId}")]
        [Permission(Code = "EDHIVTCG")]
        public IHttpActionResult IPDGetFormByVisitId(Guid visitId)
        {
            return GetFormAPI(visitId, vistiType, formCode);
        }
        [HttpPost]
        [Route("api/ED/HIVTestingConsent/Create/{visitId}")]
        [Permission(Code = "EDHIVTCC")]
        public IHttpActionResult CreateForm(Guid visitId)
        {
            return CreateAPI(visitId, vistiType, formCode);
        }

        [HttpPost]
        [Route("api/ED/HIVTestingConsent/Update/{visitId}")]
        [Permission(Code = "EDHIVTCU")]
        public IHttpActionResult IPDUpdate(Guid visitId, [FromBody] JObject request)
        {
            return UpdateAPI(visitId, request, vistiType, formCode);
        }
    }
}