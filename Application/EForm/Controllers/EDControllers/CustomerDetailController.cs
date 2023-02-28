using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.EDControllers
{
    [SessionAuthorize]
    public class CustomerDetailController : BaseEDApiController
    {
        [HttpGet]
        [Route("api/ED/CustomerDetail/{id}")]
        [Permission(Code = "ECUDE1")]
        public IHttpActionResult GetCustomerDetailAPI(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);
            var emer_record = ed.EmergencyRecord;
            if (emer_record == null || emer_record.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.ED_ER0_NOT_FOUND);

            return Content(HttpStatusCode.OK, new
            {
                emer_record.Id,
                EDId = ed.Id,
                ed.EmergencyTriageRecordId,
                Datas = emer_record.EmergencyRecordDatas.Where(e => !e.IsDeleted).Select(etrd => new { Id = emer_record.Id, Code = etrd.Code, Value = etrd.Value }).ToList(),
            });
        }
    }
}
