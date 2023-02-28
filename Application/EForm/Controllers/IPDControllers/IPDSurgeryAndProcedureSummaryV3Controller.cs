using EForm.Authentication;
using EForm.Controllers.EIOControllers;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.ServiceModel.Channels;
using System.Web.Http;

namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class IPDSurgeryAndProcedureSummaryV3Controller : SurgeryAndProcedureSummaryV3Controller
    {
        private readonly string vistiType = "IPD";
        [HttpGet]
        [Route("api/IPD/SurgeryAndProcedureSummaryV3/GetListItemsByVisitId/{visitId}")]
        [Permission(Code = "IPDSAPSV3GL")]
        public IHttpActionResult IPDGetListItemsByVisitId(Guid visitId)
        {
            return GetListItemsByVisitId(visitId, vistiType);
        }

        [HttpGet]
        [Route("api/IPD/SurgeryAndProcedureSummaryV3/GetDetail/{visitId}/{formId}")]
        [Permission(Code = "IPDSAPSV3GD")]
        public IHttpActionResult IPDGetDetail(Guid visitId, Guid formId)
        {
            return GetDetail(visitId, formId, vistiType);
        }
        [HttpPost]
        [Route("api/IPD/SurgeryAndProcedureSummaryV3/Create/{visitId}")]
        [Permission(Code = "IPDSAPSV3C")]
        public IHttpActionResult IPDCreateProcedureSummaryV2(Guid visitId)
        {
            return CreateSurgeryAndProcedureSummaryV3(visitId, vistiType);
        }

        [HttpPost]
        [Route("api/IPD/SurgeryAndProcedureSummaryV3/Update/{visitId}/{formId}")]
        [Permission(Code = "IPDSAPSV3U")]
        public IHttpActionResult IPDUpdate(Guid visitId, Guid formId, [FromBody] JObject request)
        {
            return Update(visitId,formId ,vistiType, request);
        }

        [HttpPost]
        [Route("api/IPD/SurgeryAndProcedureSummaryV3/Confirm/{visitId}/{formId}")]
        [Permission(Code = "IPDSAPSV3CF")]
        public IHttpActionResult IPDConfirmAPI(Guid visitId, Guid formId, [FromBody] JObject request)
        {
            return ConfirmAPI(visitId, formId, vistiType, request);
        }        
    }
}