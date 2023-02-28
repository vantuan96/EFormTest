using System;
using System.Net;
using System.Web.Http;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers.BaseEIOControllers;
using Newtonsoft.Json.Linq;

namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class IPDSpongeSharpsAndInstrumentsCountsSheetsController : EIOSpongeSharpsAndInstrumentsCountsSheetController
    {
        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/SpongeSharpsAndInstrumentsCountsSheets/Create/{id}")]
        [Permission(Code = "ISSIC3")]
        public IHttpActionResult CreateSpongeSharpsAndInstrumentsCountsSheetAPI(Guid id)
        {
            var visit = GetIPD(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);

            if (IPDIsBlock(visit, Constant.IPDFormCode.BangKiemChuanBiBanGiaoTruocPhauThuat))
            {
                return Content(HttpStatusCode.BadRequest, Message.FORM_IS_LOCKED);
            }

            var ssaic = GetSpongeSharpsAndInstrumentsCountsSheet(visit.Id, "IPD");
            if (ssaic != null)
                return Content(HttpStatusCode.NotFound, Message.EIO_SSA_EXIST);

            CreateSpongeSharpsAndInstrumentsCountsSheet(visit.Id, "IPD");

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpGet]
        [Route("api/IPD/SpongeSharpsAndInstrumentsCountsSheets/{id}")]
        [Permission(Code = "ISSIC1")]
        public IHttpActionResult GetSpongeSharpsAndInstrumentsCountsSheetAPI(Guid id)
        {
            var visit = GetIPD(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);

            var ssaic = GetSpongeSharpsAndInstrumentsCountsSheet(visit.Id, "IPD");
            bool formIsLocked = IPDIsBlock(visit, Constant.IPDFormCode.BangKiemChuanBiBanGiaoTruocPhauThuat, ssaic?.Id);
            if (ssaic == null || ssaic.IsDeleted)
                return Content(HttpStatusCode.NotFound, new
                {
                    FormIsLocked = formIsLocked,
                    Message.EIO_SSA_NOT_FOUND
                });

            return Content(HttpStatusCode.OK, BuildSpongeSharpsAndInstrumentsCountsSheet(ssaic, app_version: visit.Version));
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/SpongeSharpsAndInstrumentsCountsSheets/{id}")]
        [Permission(Code = "ISSIC2")]
        public IHttpActionResult UpdateSpongeSharpsAndInstrumentsCountsSheetAPI(Guid id, [FromBody]JObject request)
        {
            var visit = GetIPD(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);

            var ssaic = GetSpongeSharpsAndInstrumentsCountsSheet(visit.Id, "IPD");
            if (IPDIsBlock(visit, Constant.IPDFormCode.BangKiemChuanBiBanGiaoTruocPhauThuat, ssaic?.Id))
            {
                return Content(HttpStatusCode.BadRequest, Message.FORM_IS_LOCKED);
            }
         
            if (ssaic == null || ssaic.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.EIO_SSA_NOT_FOUND);

            HandleUpdateSpongeSharpsAndInstrumentsCountsSheet(ssaic, request);
            
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }
    }
}