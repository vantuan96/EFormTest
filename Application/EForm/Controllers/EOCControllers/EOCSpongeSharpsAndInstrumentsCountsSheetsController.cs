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
    public class EOCSpongeSharpsAndInstrumentsCountsSheetsController : EIOSpongeSharpsAndInstrumentsCountsSheetController
    {
        private readonly string visit_type = "EOC";
        [HttpGet]
        [Route("api/EOC/SpongeSharpsAndInstrumentsCountsSheets/{id}")]
        [Permission(Code = "EOC043")]
        public IHttpActionResult GetSpongeSharpsAndInstrumentsCountsSheetAPI(Guid id)
        {
            var visit = GetEOC(id);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);

            var ssaic = GetSpongeSharpsAndInstrumentsCountsSheet(id, visit_type);
            if (ssaic == null || ssaic.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.EIO_SSA_NOT_FOUND);

            return Content(HttpStatusCode.OK, BuildSpongeSharpsAndInstrumentsCountsSheet(ssaic, app_version: visit.Version));
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/EOC/SpongeSharpsAndInstrumentsCountsSheets/Create/{id}")]
        [Permission(Code = "EOC044")]
        public IHttpActionResult CreateSpongeSharpsAndInstrumentsCountsSheetAPI(Guid id)
        {
            var visit = GetEOC(id);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);

            var ssaic = GetSpongeSharpsAndInstrumentsCountsSheet(id, visit_type);
            if (ssaic != null)
                return Content(HttpStatusCode.NotFound, Message.EIO_SSA_EXIST);

            CreateSpongeSharpsAndInstrumentsCountsSheet(id, visit_type);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

      
        [HttpPost]
        [CSRFCheck]
        [Route("api/EOC/SpongeSharpsAndInstrumentsCountsSheets/{id}")]
        [Permission(Code = "EOC045")]
        public IHttpActionResult UpdateSpongeSharpsAndInstrumentsCountsSheetAPI(Guid id, [FromBody]JObject request)
        {
            var visit = GetEOC(id);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);

            var ssaic = GetSpongeSharpsAndInstrumentsCountsSheet(id, visit_type);
            if (ssaic == null || ssaic.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.EIO_SSA_NOT_FOUND);

            HandleUpdateSpongeSharpsAndInstrumentsCountsSheet(ssaic, request);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }
    }
}