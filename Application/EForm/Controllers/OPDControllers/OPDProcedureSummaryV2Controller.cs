using EForm.Authentication;
using EForm.Controllers.EIOControllers;
using Newtonsoft.Json.Linq;
using System;
using System.Web.Http;

namespace EForm.Controllers.OPDControllers
{
    [SessionAuthorize]
    public class OPDProcedureSummaryV2Controller : ProcedureSummaryV2Controller
    {
        private readonly string vistiType = "OPD";
        [HttpGet]
        [Route("api/OPD/ProcedureSummaryV2/GetListItemsByVisitId/{visitId}")]
        [Permission(Code = "OPDPSV21A01084")]
        public IHttpActionResult OPDGetListItemsByVisitId(Guid visitId)
        {
            return GetListItemsByVisitId(visitId, vistiType);
        }

        [HttpGet]
        [Route("api/OPD/ProcedureSummaryV2/GetDetail/{visitId}/{formId}")]
        [Permission(Code = "OPDPSV21A01084")]
        public IHttpActionResult OPDGetDetail(Guid visitId, Guid formId)
        {
            return GetDetail(visitId, formId, vistiType);
        }

        [Route("api/OPD/ProcedureSummaryV2/Create/{visitId}")]
        [Permission(Code = "OPDPSV22A01084")]
        public IHttpActionResult OPDCreateProcedureSummaryV2(Guid visitId)
        {
            return CreateProcedureSummaryV2(visitId, vistiType);
        }

        [HttpPost]
        [Route("api/OPD/ProcedureSummaryV2/Update/{visitId}/{formId}")]
        [Permission(Code = "OPDPSV23A01084")]
        public IHttpActionResult OPDUpdate(Guid visitId, Guid formId, [FromBody] JObject request)
        {
            return Update(visitId,formId, vistiType, request);
        }

        [HttpPost]
        [Route("api/OPD/ProcedureSummaryV2/Confirm/{visitId}/{formId}")]
        [Permission(Code = "OPDPSV24A01084")]
        public IHttpActionResult OPDConfirmAPI(Guid visitId, Guid formId, [FromBody] JObject request)
        {
            return ConfirmAPI(visitId, formId, vistiType, request);
        }
    }
}