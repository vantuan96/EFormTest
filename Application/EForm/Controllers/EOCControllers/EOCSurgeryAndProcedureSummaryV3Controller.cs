using EForm.Authentication;
using EForm.Controllers.EIOControllers;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.EOCControllers
{
    public class EOCSurgeryAndProcedureSummaryV3Controller : SurgeryAndProcedureSummaryV3Controller
    {
        private readonly string vistiType = "EOC";
        [HttpGet]
        [Route("api/EOC/SurgeryAndProcedureSummaryV3/GetListItemsByVisitId/{visitId}")]
        [Permission(Code = "EOCSAPSV3GL")]
        public IHttpActionResult EOCGetListItemsByVisitId(Guid visitId)
        {
            return GetListItemsByVisitId(visitId, vistiType);
        }

        [HttpGet]
        [Route("api/EOC/SurgeryAndProcedureSummaryV3/GetDetail/{visitId}/{formId}")]
        [Permission(Code = "EOCSAPSV3GD")]
        public IHttpActionResult EOCGetDetail(Guid visitId, Guid formId)
        {
            return GetDetail(visitId, formId, vistiType);
        }
        [HttpPost]
        [Route("api/EOC/SurgeryAndProcedureSummaryV3/Create/{visitId}")]
        [Permission(Code = "EOCSAPSV3C")]
        public IHttpActionResult EOCCreateProcedureSummaryV2(Guid visitId)
        {
            return CreateSurgeryAndProcedureSummaryV3(visitId, vistiType);
        }

        [HttpPost]
        [Route("api/EOC/SurgeryAndProcedureSummaryV3/Update/{visitId}/{formId}")]
        [Permission(Code = "EOCSAPSV3U")]
        public IHttpActionResult EOCUpdate(Guid visitId, Guid formId, [FromBody] JObject request)
        {
            return Update(visitId,formId ,vistiType, request);
        }

        [HttpPost]
        [Route("api/EOC/SurgeryAndProcedureSummaryV3/Confirm/{visitId}/{formId}")]
        [Permission(Code = "EOCSAPSV3CF")]
        public IHttpActionResult EOCConfirmAPI(Guid visitId, Guid formId, [FromBody] JObject request)
        {
            return ConfirmAPI(visitId, formId, vistiType, request);
        }        
    }
}