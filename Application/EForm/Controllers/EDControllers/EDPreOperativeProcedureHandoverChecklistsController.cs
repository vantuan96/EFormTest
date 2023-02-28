using System;
using System.Net;
using System.Web.Http;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers.BaseEIOControllers;
using Newtonsoft.Json.Linq;

namespace EForm.Controllers.EDControllers
{
    [SessionAuthorize]
    public class EDPreOperativeProcedureHandoverChecklistsController : EIOPreOperativeProcedureHandoverChecklistsController
    {
        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/PreOperativeProcedureHandoverChecklists/Create/{id}")]
        [Permission(Code = "EPOPH3")]
        public IHttpActionResult CreatePreOperativeProcedureHandoverChecklistAPI(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var phc = GetPreOperativeProcedureHandoverChecklist(ed.Id, "ED");
            if (phc != null)
                return Content(HttpStatusCode.NotFound, Message.EIO_PHC_EXIST);

            CreatePreOperativeProcedureHandoverChecklist(ed, "ED");

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpGet]
        [Route("api/ED/PreOperativeProcedureHandoverChecklists/{id}")]
        [Permission(Code = "EPOPH1")]
        public IHttpActionResult GetPreOperativeProcedureHandoverChecklists(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var phc = GetPreOperativeProcedureHandoverChecklist(ed.Id, "ED");
            if (phc == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_PHC_NOT_FOUND);

            return Content(HttpStatusCode.OK, BuildPreOperativeProcedureHandoverChecklist(phc, ed.Customer, app_version: ed.Version));
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/PreOperativeProcedureHandoverChecklists/{id}")]
        [Permission(Code = "EPOPH2")]
        public IHttpActionResult UpdateDischargeInformationAPI(Guid id, [FromBody]JObject request)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var phc = GetPreOperativeProcedureHandoverChecklist(ed.Id, "ED");
            if (phc == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_PHC_NOT_FOUND);

            HandleUpdateSpongeSharpsAndInstrumentsCountsSheet(phc, request);
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }
    }
}