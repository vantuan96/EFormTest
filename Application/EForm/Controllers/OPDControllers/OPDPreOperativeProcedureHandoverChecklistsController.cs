using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers.BaseEIOControllers;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.OPDControllers
{
    [SessionAuthorize]
    public class OPDPreOperativeProcedureHandoverChecklistsController : EIOPreOperativeProcedureHandoverChecklistsController
    {
        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/PreOperativeProcedureHandoverChecklists/Create/{id}")]
        [Permission(Code = "OPOPH3")]
        public IHttpActionResult CreatePreOperativeProcedureHandoverChecklistAPI(Guid id)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var user = GetUser();
            if (IsBlockAfter24h(opd.CreatedAt) && !HasUnlockPermission(opd.Id, "OPOPH", user.Username))
                return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);

            var phc = GetPreOperativeProcedureHandoverChecklist(opd.Id, "OPD");
            if (phc != null)
                return Content(HttpStatusCode.NotFound, Message.EIO_PHC_EXIST);

            CreatePreOperativeProcedureHandoverChecklist(opd, "OPD");

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpGet]
        [Route("api/OPD/PreOperativeProcedureHandoverChecklists/{id}")]
        [Permission(Code = "OPOPH1")]
        public IHttpActionResult GetPreOperativeProcedureHandoverChecklists(Guid id)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var phc = GetPreOperativeProcedureHandoverChecklist(opd.Id, "OPD");
            var user = GetUser();
            bool IsLocked = Is24hLocked(opd.CreatedAt, opd.Id, "OPOPH", user.Username, phc?.Id);

            if (phc == null)
                return Content(HttpStatusCode.NotFound, new
                {
                    ViMessage = "Bản Kiểm bàn giao người bệnh trước mổ không tồn tại",
                    EnMessage = "Pre-Operative/Procedure handover checklist is not found",
                    IsLocked
                });
            
            return Content(HttpStatusCode.OK, BuildPreOperativeProcedureHandoverChecklist(phc, opd.Customer, app_version: opd.Version, IsLocked));
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/PreOperativeProcedureHandoverChecklists/{id}")]
        [Permission(Code = "OPOPH2")]
        public IHttpActionResult UpdateDischargeInformationAPI(Guid id, [FromBody]JObject request)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);


            var phc = GetPreOperativeProcedureHandoverChecklist(opd.Id, "OPD");
            var user = GetUser();
            if (IsBlockAfter24h(opd.CreatedAt, phc?.Id) && !HasUnlockPermission(opd.Id, "OPOPH", user.Username, phc?.Id))
                return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);

            if (phc == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_PHC_NOT_FOUND);

            HandleUpdateSpongeSharpsAndInstrumentsCountsSheet(phc, request);
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }
    }
}