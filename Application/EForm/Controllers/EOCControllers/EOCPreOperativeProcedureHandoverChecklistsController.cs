using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers.BaseEIOControllers;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.EOCControllers
{
    [SessionAuthorize]
    public class EOCPreOperativeProcedureHandoverChecklistsController : EIOPreOperativeProcedureHandoverChecklistsController
    {
        private readonly string visit_type = "EOC";
        
        [HttpGet]
        [Route("api/EOC/PreOperativeProcedureHandoverChecklists/{id}")]
        [Permission(Code = "EOC043")]
        public IHttpActionResult GetPreOperativeProcedureHandoverChecklists(Guid id)
        {
            var visit = GetEOC(id);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);

            var phc = GetPreOperativeProcedureHandoverChecklist(id, visit_type);
            if (phc == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_PHC_NOT_FOUND);

            return Content(HttpStatusCode.OK, BuildPreOperativeProcedureHandoverChecklist(phc, visit.Customer, app_version: visit.Version));
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/EOC/PreOperativeProcedureHandoverChecklists/Create/{id}")]
        [Permission(Code = "EOC044")]
        public IHttpActionResult CreatePreOperativeProcedureHandoverChecklistAPI(Guid id)
        {
            var visit = GetEOC(id);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);

            var phc = GetPreOperativeProcedureHandoverChecklist(id, visit_type);
            if (phc != null)
                return Content(HttpStatusCode.NotFound, Message.EIO_PHC_EXIST);

            CreatePreOperativeProcedureHandoverChecklist(visit, visit_type);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/EOC/PreOperativeProcedureHandoverChecklists/{id}")]
        [Permission(Code = "EOC045")]
        public IHttpActionResult UpdateDischargeInformationAPI(Guid id, [FromBody]JObject request)
        {
            var visit = GetEOC(id);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);

            var phc = GetPreOperativeProcedureHandoverChecklist(id, visit_type);
            if (phc == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_PHC_NOT_FOUND);

            HandleUpdateSpongeSharpsAndInstrumentsCountsSheet(phc, request);
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }
    }
}