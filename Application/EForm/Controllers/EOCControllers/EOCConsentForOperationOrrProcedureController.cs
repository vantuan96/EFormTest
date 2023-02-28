using EForm.Authentication;
using EForm.Controllers.EIOControllers;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.EOCControllers
{
    [SessionAuthorize]
    public class EOCConsentForOperationOrrProcedureController : EIOAllFormController
    {
        private readonly string vistiType = "EOC";
        private readonly string formCode = "A01_001_080721_V";

        [HttpGet]
        [Route("api/EOC/ConsentForOperationOrrProcedure/Get/{visitId}")]
        [Permission(Code = "EOCCFOOPG")]
        public IHttpActionResult IPDGetFormByVisitId(Guid visitId)
        {
            return GetFormAPI(visitId, vistiType, formCode);
        }
        [HttpPost]
        [Route("api/EOC/ConsentForOperationOrrProcedure/Create/{visitId}")]
        [Permission(Code = "EOCCFOOPC")]
        public IHttpActionResult CreateForm(Guid visitId)
        {
            return CreateAPI(visitId, vistiType, formCode);
        }

        [HttpPost]
        [Route("api/EOC/ConsentForOperationOrrProcedure/Update/{visitId}")]
        [Permission(Code = "EOCCFOOPU")]
        public IHttpActionResult IPDUpdate(Guid visitId, [FromBody] JObject request)
        {
            return UpdateAPI(visitId, request, vistiType, formCode);
        }        
    }
}