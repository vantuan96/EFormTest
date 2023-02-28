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
    public class OPDSpongeSharpsAndInstrumentsCountsSheetsController : EIOSpongeSharpsAndInstrumentsCountsSheetController
    {
        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/SpongeSharpsAndInstrumentsCountsSheets/Create/{id}")]
        [Permission(Code = "OSSIC3")]
        public IHttpActionResult CreateSpongeSharpsAndInstrumentsCountsSheetAPI(Guid id)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var user = GetUser();
            if (IsBlockAfter24h(opd.CreatedAt) && !HasUnlockPermission(opd.Id, "OPOPH", user.Username))
                return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);

            var ssaic = GetSpongeSharpsAndInstrumentsCountsSheet(opd.Id, "OPD");
            if (ssaic != null)
                return Content(HttpStatusCode.NotFound, Message.EIO_SSA_EXIST);

            CreateSpongeSharpsAndInstrumentsCountsSheet(opd.Id, "OPD");

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpGet]
        [Route("api/OPD/SpongeSharpsAndInstrumentsCountsSheets/{id}")]
        [Permission(Code = "OSSIC1")]
        public IHttpActionResult GetSpongeSharpsAndInstrumentsCountsSheetAPI(Guid id)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);
            var user = GetUser();        
            var ssaic = GetSpongeSharpsAndInstrumentsCountsSheet(opd.Id, "OPD");
            bool IsLocked = Is24hLocked(opd.CreatedAt, opd.Id, "OPOPH", user.Username, ssaic?.Id);
            if (ssaic == null || ssaic.IsDeleted)
                return Content(HttpStatusCode.NotFound, new
                {
                    ViMessage = "Phiếu kiểm gạc và dụng cụ phẫu thuật không tồn tại",
                    EnMessage = "Sponge, shards and instruments counts sheet is not found",
                    IsLocked
                });

            return Content(HttpStatusCode.OK, BuildSpongeSharpsAndInstrumentsCountsSheet(ssaic, app_version: opd.Version, IsLocked));
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/SpongeSharpsAndInstrumentsCountsSheets/{id}")]
        [Permission(Code = "OSSIC2")]
        public IHttpActionResult UpdateSpongeSharpsAndInstrumentsCountsSheetAPI(Guid id, [FromBody]JObject request)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var ssaic = GetSpongeSharpsAndInstrumentsCountsSheet(opd.Id, "OPD");
            var user = GetUser();
            if (IsBlockAfter24h(opd.CreatedAt, ssaic?.Id) && !HasUnlockPermission(opd.Id, "OPOPH", user.Username, ssaic?.Id))
                return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);

            if (ssaic == null || ssaic.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.EIO_SSA_NOT_FOUND);

            HandleUpdateSpongeSharpsAndInstrumentsCountsSheet(ssaic, request);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }
    }
}