using EForm.Authentication;
using EForm.Controllers.EIOControllers;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Web.Http;


namespace EForm.Controllers.EDControllers
{
    [SessionAuthorize]
    public class EDSurgeryAndProcedureSummaryV3Controller : SurgeryAndProcedureSummaryV3Controller
    {
        private readonly string vistiType = "ED";
        [HttpGet]
        [Route("api/ED/SurgeryAndProcedureSummaryV3/GetListItemsByVisitId/{visitId}")]
        [Permission(Code = "EDSAPSV3GL")]
        public IHttpActionResult EDGetListItemsByVisitId(Guid visitId)
        {
            return GetListItemsByVisitId(visitId, vistiType);
        }

        [HttpGet]
        [Route("api/ED/SurgeryAndProcedureSummaryV3/GetDetail/{visitId}/{formId}")]
        [Permission(Code = "EDSAPSV3GD")]
        public IHttpActionResult EDGetDetail(Guid visitId, Guid formId)
        {
            return GetDetail(visitId, formId, vistiType);
        }
        [HttpPost]
        [Route("api/ED/SurgeryAndProcedureSummaryV3/Create/{visitId}")]
        [Permission(Code = "EDSAPSV3C")]
        public IHttpActionResult EDCreateProcedureSummaryV2(Guid visitId)
        {
            return CreateSurgeryAndProcedureSummaryV3(visitId, vistiType);
        }

        [HttpPost]
        [Route("api/ED/SurgeryAndProcedureSummaryV3/Update/{visitId}/{formId}")]
        [Permission(Code = "EDSAPSV3U")]
        public IHttpActionResult EDUpdate(Guid visitId, Guid formId, [FromBody] JObject request)
        {
            return Update(visitId, formId, vistiType, request);
        }
        [HttpPost]
        [Route("api/ED/SurgeryAndProcedureSummaryV3/Confirm/{visitId}/{formId}")]
        [Permission(Code = "EDSAPSV3CF")]
        public IHttpActionResult EDConfirmAPI(Guid visitId, Guid formId, [FromBody] JObject request)
        {
            return ConfirmAPI(visitId, formId, vistiType, request);
        }
        [HttpGet]
        [Route("api/ED/SurgeryAndProcedureSummaryV3/Infor/{visitId}")]
        public IHttpActionResult EDGetInfo(Guid visitId)
        {
            var visit = GetVisit(visitId, "ED");
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Common.Message.ED_NOT_FOUND);
            bool IsLocked = false;

            return Content(HttpStatusCode.OK, IsLocked);
        }
    }
}