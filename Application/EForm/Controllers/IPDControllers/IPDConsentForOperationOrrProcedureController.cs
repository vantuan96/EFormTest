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
    public class IPDConsentForOperationOrrProcedureController : EIOAllFormController
    {
        private readonly string vistiType = "IPD";
        private readonly string formCode = "A01_001_080721_V";

        [HttpGet]
        [Route("api/IPD/ConsentForOperationOrrProcedure/Get/{visitId}")]
        [Permission(Code = "IPDCFOOPG")]
        public IHttpActionResult IPDGetFormByVisitId(Guid visitId)
        {
            return GetFormAPI(visitId, vistiType, formCode);
        }
        [HttpPost]
        [Route("api/IPD/ConsentForOperationOrrProcedure/Create/{visitId}")]
        [Permission(Code = "IPDCFOOPC")]
        public IHttpActionResult CreateForm(Guid visitId)
        {
            return CreateAPI(visitId, vistiType, formCode);
        }

        [HttpPost]
        [Route("api/IPD/ConsentForOperationOrrProcedure/Update/{visitId}")]
        [Permission(Code = "IPDCFOOPU")]
        public IHttpActionResult IPDUpdate(Guid visitId ,[FromBody] JObject request)
        {
            return UpdateAPI(visitId , request, vistiType, formCode);
        }      

        [HttpGet]
        [Route("api/IPD/ConsentForOperationOrrProcedure/GetInfo/{visitId}")]
        public IHttpActionResult IPDGetGetInfo(Guid visitId)
        {
            var visit = GetVisit(visitId, "IPD");
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Common.Message.IPD_NOT_FOUND);
            var CheckFormLocked = IPDIsBlock(visit, "A01_001_080721_V");
            return Content(HttpStatusCode.OK, new { IsLocked = CheckFormLocked } );
        }
    }
}