using EForm.Authentication;
using EForm.Controllers.EIOControllers;
using Newtonsoft.Json.Linq;
using System;
using System.Web.Http;


namespace EForm.Controllers.EDControllers
{
    [SessionAuthorize]
    public class EDProcedureSummaryV2Controller : ProcedureSummaryV2Controller
    {
        private readonly string vistiType = "ED";
        [HttpGet]
        [Route("api/ED/ProcedureSummaryV2/GetListItemsByVisitId/{visitId}")]
        [Permission(Code = "EDPSV21A01084")]
        public IHttpActionResult EDGetListItemsByVisitId(Guid visitId)
        {
            return GetListItemsByVisitId(visitId, vistiType);
        }

        [HttpGet]
        [Route("api/ED/ProcedureSummaryV2/GetDetail/{visitId}/{formId}")]
        [Permission(Code = "EDPSV21A01084")]
        public IHttpActionResult EDGetDetail(Guid visitId, Guid formId)
        {
            return GetDetail(visitId, formId, vistiType);
        }

        [Route("api/ED/ProcedureSummaryV2/Create/{visitId}")]
        [Permission(Code = "EDPSV22A01084")]
        public IHttpActionResult EDCreateProcedureSummaryV2(Guid visitId)
        {
            return CreateProcedureSummaryV2(visitId, vistiType);
        }

        [HttpPost]
        [Route("api/ED/ProcedureSummaryV2/Update/{visitId}/{formId}")]
        [Permission(Code = "EDPSV23A01084")]
        public IHttpActionResult EDUpdate(Guid visitId, Guid formId, [FromBody] JObject request)
        {
            return Update(visitId, formId, vistiType, request);
        }
        [HttpPost]
        [Route("api/ED/ProcedureSummaryV2/Confirm/{visitId}/{formId}")]
        [Permission(Code = "EDPSV24A01084")]
        public IHttpActionResult EDConfirmAPI(Guid visitId, Guid formId, [FromBody] JObject request)
        {
            return ConfirmAPI(visitId, formId, vistiType, request);
        }
    }
}