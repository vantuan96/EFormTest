using EForm.Authentication;
using EForm.Controllers.EIOControllers;
using Newtonsoft.Json.Linq;
using System;
using System.Web.Http;

namespace EForm.Controllers.EOCControllers
{
    [SessionAuthorize]
    public class EOCProcedureSummaryV2Controller : ProcedureSummaryV2Controller
    {
        private readonly string vistiType = "EOC";
        [HttpGet]
        [Route("api/EOC/ProcedureSummaryV2/GetListItemsByVisitId/{visitId}")]
        [Permission(Code = "EOCPSV21A01084")]
        public IHttpActionResult EOCGetListItemsByVisitId(Guid visitId)
        {
            return GetListItemsByVisitId(visitId, vistiType);
        }

        [HttpGet]
        [Route("api/EOC/ProcedureSummaryV2/GetDetail/{visitId}/{formId}")]
        [Permission(Code = "EOCPSV21A01084")]
        public IHttpActionResult EOCGetDetail(Guid visitId, Guid formId)
        {
            return GetDetail(visitId, formId, vistiType);
        }

        [Route("api/EOC/ProcedureSummaryV2/Create/{visitId}")]
        [Permission(Code = "EOCPSV22A01084")]
        public IHttpActionResult EOCCreateProcedureSummaryV2(Guid visitId)
        {
            return CreateProcedureSummaryV2(visitId, vistiType);
        }

        [HttpPost]
        [Route("api/EOC/ProcedureSummaryV2/Update/{visitId}/{formId}")]
        [Permission(Code = "EOCPSV23A01084")]
        public IHttpActionResult EOCUpdate(Guid visitId, Guid formId, [FromBody] JObject request)
        {
            return Update(visitId,formId ,vistiType, request);
        }

        [HttpPost]
        [Route("api/EOC/ProcedureSummaryV2/Confirm/{visitId}/{formId}")]
        [Permission(Code = "EOCPSV24A01084")]
        public IHttpActionResult EOCConfirmAPI(Guid visitId, Guid formId, [FromBody] JObject request)
        {
            return ConfirmAPI(visitId, formId, vistiType, request);
        }
    }
}