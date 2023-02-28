using EForm.Authentication;
using EForm.Controllers.EIOControllers;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.EOCControllers
{
    [SessionAuthorize]
    public class EOCHIVTestingConsentController : EIOAllFormController
    {
        private readonly string vistiType = "EOC";
        private readonly string formCode = "A01_014_050919_VE";

        [HttpGet]
        [Route("api/EOC/HIVTestingConsent/Get/{visitId}")]
        [Permission(Code = "EOCHIVTCG")]
        public IHttpActionResult IPDGetFormByVisitId(Guid visitId)
        {
            return GetFormAPI(visitId, vistiType, formCode);
        }
        [HttpPost]
        [Route("api/EOC/HIVTestingConsent/Create/{visitId}")]
        [Permission(Code = "EOCHIVTCC")]
        public IHttpActionResult CreateForm(Guid visitId)
        {
            return CreateAPI(visitId, vistiType, formCode);
        }

        [HttpPost]
        [Route("api/EOC/HIVTestingConsent/Update/{visitId}")]
        [Permission(Code = "EOCHIVTCU")]
        public IHttpActionResult IPDUpdate(Guid visitId, [FromBody] JObject request)
        {
            return UpdateAPI(visitId, request, vistiType, formCode);
        }        
    }
}