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
    public class OPDConsentForOperationOrrProcedureController : EIOAllFormController
    {
        private readonly string vistiType = "OPD";
        private readonly string formCode = "A01_001_080721_V";
        [HttpGet]
        [Route("api/OPD/ConsentForOperationOrrProcedure/Get/{visitId}")]
        [Permission(Code = "OPDCFOOPG")]
        public IHttpActionResult IPDGetFormByVisitId(Guid visitId)
        {
            return GetFormAPI(visitId, vistiType, formCode);
        }
        [HttpPost]
        [Route("api/OPD/ConsentForOperationOrrProcedure/Create/{visitId}")]
        [Permission(Code = "OPDCFOOPC")]
        public IHttpActionResult CreateForm(Guid visitId)
        {
            return CreateAPI(visitId, vistiType, formCode);
        }

        [HttpPost]
        [Route("api/OPD/ConsentForOperationOrrProcedure/Update/{visitId}")]
        [Permission(Code = "OPDCFOOPU")]
        public IHttpActionResult IPDUpdate(Guid visitId, [FromBody] JObject request)
        {
            return UpdateAPI(visitId, request, vistiType, formCode);
        }
        [HttpGet]
        [Route("api/OPD/ConsentForOperationOrrProcedure/GetInfo/{visitId}")]
        public IHttpActionResult OPDGetInfo(Guid visitId)
        {
            var visit = GetVisit(visitId, "OPD");
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Common.Message.OPD_NOT_FOUND);
            var user = GetUser();
            bool IsLocked = Is24hLocked(visit.CreatedAt, visit.Id, formCode, user.Username);
            return Content(HttpStatusCode.OK, new { IsLocked = IsLocked });
        }     
        
    }
}