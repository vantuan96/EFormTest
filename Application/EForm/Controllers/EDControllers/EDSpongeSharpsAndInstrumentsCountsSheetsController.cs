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
    public class EDSpongeSharpsAndInstrumentsCountsSheetsController : EIOSpongeSharpsAndInstrumentsCountsSheetController
    {
        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/SpongeSharpsAndInstrumentsCountsSheets/Create/{id}")]
        [Permission(Code = "ESSIC3")]
        public IHttpActionResult CreateSpongeSharpsAndInstrumentsCountsSheetAPI(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var ssaic = GetSpongeSharpsAndInstrumentsCountsSheet(ed.Id, "ED");
            if (ssaic != null)
                return Content(HttpStatusCode.NotFound, Message.EIO_SSA_EXIST);

            CreateSpongeSharpsAndInstrumentsCountsSheet(ed.Id, "ED");

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpGet]
        [Route("api/ED/SpongeSharpsAndInstrumentsCountsSheets/{id}")]
        [Permission(Code = "ESSIC1")]
        public IHttpActionResult GetSpongeSharpsAndInstrumentsCountsSheetAPI(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var ssaic = GetSpongeSharpsAndInstrumentsCountsSheet(ed.Id, "ED");
            if (ssaic == null || ssaic.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.EIO_SSA_NOT_FOUND);

            return Content(HttpStatusCode.OK, BuildSpongeSharpsAndInstrumentsCountsSheet(ssaic, app_version: ed.Version));
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/SpongeSharpsAndInstrumentsCountsSheets/{id}")]
        [Permission(Code = "ESSIC2")]
        public IHttpActionResult UpdateSpongeSharpsAndInstrumentsCountsSheetAPI(Guid id, [FromBody] JObject request)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var ssaic = GetSpongeSharpsAndInstrumentsCountsSheet(ed.Id, "ED");
            if (ssaic == null || ssaic.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.EIO_SSA_NOT_FOUND);

            HandleUpdateSpongeSharpsAndInstrumentsCountsSheet(ssaic, request);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }
    }
}