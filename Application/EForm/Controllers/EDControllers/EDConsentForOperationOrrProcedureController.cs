using EForm.Authentication;
using EForm.Controllers.EIOControllers;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Web.Http;


namespace EForm.Controllers.EDControllers
{
    [SessionAuthorize]
    public class EDConsentForOperationOrrProcedureController : EIOAllFormController
    {
        private readonly string vistiType = "ED";
        private readonly string formCode = "A01_001_080721_V";
        [HttpGet]
        [Route("api/ED/ConsentForOperationOrrProcedure/Get/{visitId}")]
        [Permission(Code = "EDCFOOPG")]
        public IHttpActionResult IPDGetFormByVisitId(Guid visitId)
        {
            return GetFormAPI(visitId, vistiType, formCode);
        }
        [HttpPost]
        [Route("api/ED/ConsentForOperationOrrProcedure/Create/{visitId}")]
        [Permission(Code = "EDCFOOPC")]
        public IHttpActionResult CreateForm(Guid visitId)
        {
            return CreateAPI(visitId, vistiType, formCode);
        }

        [HttpPost]
        [Route("api/ED/ConsentForOperationOrrProcedure/Update/{visitId}")]
        [Permission(Code = "EDCFOOPU")]
        public IHttpActionResult IPDUpdate(Guid visitId, [FromBody] JObject request)
        {
            return UpdateAPI(visitId, request, vistiType, formCode);
        }
    }
}