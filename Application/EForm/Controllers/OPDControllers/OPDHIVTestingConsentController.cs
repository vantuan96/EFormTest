using DataAccess.Models.EIOModel;
using EForm.Authentication;
using EForm.Controllers.EIOControllers;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.OPDControllers
{
    [SessionAuthorize]
    public class OPDHIVTestingConsentController : EIOAllFormController
    {
        private readonly string vistiType = "OPD";
        private readonly string formCode = "A01_014_050919_VE";
        [HttpGet]
        [Route("api/OPD/HIVTestingConsent/Get/{visitId}")]
        [Permission(Code = "OPDHIVTCG")]
        public IHttpActionResult IPDGetFormByVisitId(Guid visitId)
        {
            return GetFormAPI(visitId, vistiType, formCode);
        }
        [HttpPost]
        [Route("api/OPD/HIVTestingConsent/Create/{visitId}")]
        [Permission(Code = "OPDHIVTCC")]
        public IHttpActionResult CreateForm(Guid visitId)
        {
            return CreateAPI(visitId, vistiType, formCode);
        }

        [HttpPost]
        [Route("api/OPD/HIVTestingConsent/Update/{visitId}")]
        [Permission(Code = "OPDHIVTCU")]
        public IHttpActionResult IPDUpdate(Guid visitId, [FromBody] JObject request)
        {
            return UpdateAPI(visitId, request, vistiType, formCode);
        }
        [HttpGet]
        [Route("api/OPD/HIVTestingConsent/GetInfo/{visitId}")]
        public IHttpActionResult OPDGetInfo(Guid visitId)
        {
            var visit = GetVisit(visitId, "OPD");
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Common.Message.OPD_NOT_FOUND);
            var user = GetUser();
            bool IsLocked = Is24hLocked(visit.CreatedAt, visit.Id, formCode, user.Username);
            return Content(HttpStatusCode.OK, IsLocked);
        }     
        
    }
}