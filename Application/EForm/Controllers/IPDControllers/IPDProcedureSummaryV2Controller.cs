using EForm.Authentication;
using EForm.Controllers.EIOControllers;
using Newtonsoft.Json.Linq;
using System;
using System.Web.Http;

namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class IPDProcedureSummaryV2Controller : ProcedureSummaryV2Controller
    {
        private readonly string vistiType = "IPD";
        [HttpGet]
        [Route("api/IPD/ProcedureSummaryV2/GetListItemsByVisitId/{visitId}")]
        [Permission(Code = "IPDPSV21A01084")]
        public IHttpActionResult IPDGetListItemsByVisitId(Guid visitId)
        {
            return GetListItemsByVisitId(visitId, vistiType);
        }

        [HttpGet]
        [Route("api/IPD/ProcedureSummaryV2/GetDetail/{visitId}/{formId}")]
        [Permission(Code = "IPDPSV21A01084")]
        public IHttpActionResult IPDGetDetail(Guid visitId, Guid formId)
        {
            return GetDetail(visitId, formId, vistiType);
        }

        [Route("api/IPD/ProcedureSummaryV2/Create/{visitId}")]
        [Permission(Code = "IPDPSV22A01084")]
        public IHttpActionResult IPDCreateProcedureSummaryV2(Guid visitId)
        {
            return CreateProcedureSummaryV2(visitId, vistiType);
        }

        [HttpPost]
        [Route("api/IPD/ProcedureSummaryV2/Update/{visitId}/{formId}")]
        [Permission(Code = "IPDPSV23A01084")]
        public IHttpActionResult IPDUpdate(Guid visitId, Guid formId, [FromBody] JObject request)
        {
            return Update(visitId,formId ,vistiType, request);
        }

        [HttpPost]
        [Route("api/IPD/ProcedureSummaryV2/Confirm/{visitId}/{formId}")]
        [Permission(Code = "IPDPSV24A01084")]
        public IHttpActionResult IPDConfirmAPI(Guid visitId, Guid formId, [FromBody] JObject request)
        {
            return ConfirmAPI(visitId, formId, vistiType, request);
        }
    }
}