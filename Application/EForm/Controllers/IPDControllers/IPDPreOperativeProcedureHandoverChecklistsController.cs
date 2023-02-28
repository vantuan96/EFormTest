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
    public class IPDPreOperativeProcedureHandoverChecklistsController : EIOPreOperativeProcedureHandoverChecklistsController
    {
        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/PreOperativeProcedureHandoverChecklists/Create/{id}")]
        [Permission(Code = "IPOPH3")]
        public IHttpActionResult CreatePreOperativeProcedureHandoverChecklistAPI(Guid id)
        {
            var visit = GetIPD(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);
            if (IPDIsBlock(visit, Constant.IPDFormCode.BangKiemChuanBiBanGiaoTruocPhauThuat))
            {
                return Content(HttpStatusCode.BadRequest, Message.FORM_IS_LOCKED);
            }
            var phc = GetPreOperativeProcedureHandoverChecklist(visit.Id, "IPD");
            if (phc != null)
                return Content(HttpStatusCode.NotFound, Message.EIO_PHC_EXIST);

            CreatePreOperativeProcedureHandoverChecklist(visit, "IPD");

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpGet]
        [Route("api/IPD/PreOperativeProcedureHandoverChecklists/{id}")]
        [Permission(Code = "IPOPH1")]
        public IHttpActionResult GetPreOperativeProcedureHandoverChecklists(Guid id)
        {
            var visit = GetIPD(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);
            
            var phc = GetPreOperativeProcedureHandoverChecklist(visit.Id, "IPD");
            bool formIsLocked = IPDIsBlock(visit, Constant.IPDFormCode.BangKiemChuanBiBanGiaoTruocPhauThuat, phc?.Id);
            if (phc == null)
                return Content(HttpStatusCode.NotFound, new
                {
                    FormIsLocked = formIsLocked,
                    Message.EIO_PHC_NOT_FOUND
                });
            
            return Content(HttpStatusCode.OK, BuildPreOperativeProcedureHandoverChecklist(phc, visit.Customer, app_version: visit.Version, formIsLocked));
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/PreOperativeProcedureHandoverChecklists/{id}")]
        [Permission(Code = "IPOPH2")]
        public IHttpActionResult UpdateDischargeInformationAPI(Guid id, [FromBody]JObject request)
        {
            var visit = GetIPD(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);

            var phc = GetPreOperativeProcedureHandoverChecklist(visit.Id, "IPD");
            if (IPDIsBlock(visit, Constant.IPDFormCode.BangKiemChuanBiBanGiaoTruocPhauThuat, phc?.Id))
            {
                return Content(HttpStatusCode.BadRequest, Message.FORM_IS_LOCKED);
            }
            
            if (phc == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_PHC_NOT_FOUND);

            HandleUpdateSpongeSharpsAndInstrumentsCountsSheet(phc, request);
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }
    }
}