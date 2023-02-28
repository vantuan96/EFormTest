using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers;
using EForm.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.EDControllers
{
    [SessionAuthorize]
    public class EDSyncVisitHistoryController : BaseEDApiController
    {
        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/SyncVisitHistory/")]
        [Permission(Code = "ESYVH1")]
        public IHttpActionResult SyncEDVisitHistoryAPI([FromBody]JObject request)
        {
            var id = new Guid(request["Id"]?.ToString());
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var customer = ed.Customer;
            if (customer == null || customer.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);
            else if (customer.PID == null)
                return Content(HttpStatusCode.NotFound, Message.PID_IS_MISSING);

            var emer_record = ed.EmergencyRecord;
            if (emer_record == null)
                return Content(HttpStatusCode.NotFound, Message.ED_ER0_NOT_FOUND);

            var site_code = GetSiteCode();
            if (string.IsNullOrEmpty(site_code))
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);

            VisitHistory visit_history = VisitHistoryFactory.GetVisit("ED", ed, site_code);
            var visit_history_list = visit_history.GetHistory();

            return Content(HttpStatusCode.OK, new
            {
                PastMedicalHistory = visit_history_list.Where(e => !string.IsNullOrEmpty(e.PastMedicalHistory)).Select(e => formatPastMedicalHistory(e)),
                HistoryOfPresentIllness = visit_history_list.Where(e => !string.IsNullOrEmpty(e.HistoryOfPresentIllness)).Select(e => new {
                    ExaminationTime = e.ExaminationTime.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                    e.ViName,
                    e.EnName,
                    e.Fullname,
                    e.Username,
                    e.HistoryOfPresentIllness,
                    e.Type
                }),
            });
        }
    }
}
