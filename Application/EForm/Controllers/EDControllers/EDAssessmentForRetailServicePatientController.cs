using DataAccess.Models.EIOModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers.BaseEIOControllers;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.EDControllers
{
    [SessionAuthorize]
    public class EDAssessmentForRetailServicePatientController : EIOAssessmentForRetailServicePatientController
    {
        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/EDAssessmentForRetailServicePatient/Confirm/{id}")]
        [Permission(Code = "EAFRS1")]
        public IHttpActionResult CreateEDAssessmentForRetailServicePatientAPI(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var afrsp = ed.EDAssessmentForRetailServicePatient;
            if (afrsp != null && !afrsp.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.EIO_AFRS_EXIST);

            CreateAssessmentForRetailServicePatient(ed, "ED");

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpGet]
        [Route("api/ED/EDAssessmentForRetailServicePatient/{id}")]
        [Permission(Code = "EAFRS2")]
        public IHttpActionResult GetEDAssessmentForRetailServicePatientAPI(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var afrsp = ed.EDAssessmentForRetailServicePatient;
            if (afrsp == null || afrsp.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.EIO_AFRS_NOT_FOUND);

            return Content(HttpStatusCode.OK, GetDetailAssessmentForRetailServicePatient(ed, afrsp));
        }

        [HttpGet]
        [Route("api/ED/EDAssessmentForRetailServicePatient/Sync/{id}")]
        [Permission(Code = "EAFRS3")]
        public IHttpActionResult SyncEmergencyTriageRecordAPI(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);
            if(ed.AdmittedDate == null)
                return Content(HttpStatusCode.NotFound, Message.SYNC_24H_NOT_FOUND);

            var lastest_ed_in_24h = GetLastestRetailVisit24H("ED", ed.CustomerId, ed.Id, ed.AdmittedDate);
            if (lastest_ed_in_24h == null)
                return Content(HttpStatusCode.NotFound, Message.SYNC_24H_NOT_FOUND);

            EIOAssessmentForRetailServicePatient afrsp = lastest_ed_in_24h.EDAssessmentForRetailServicePatient;
            if (afrsp == null || afrsp.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.SYNC_24H_NOT_FOUND);

            return Content(HttpStatusCode.OK, new
            {
                afrsp.Version,
                Datas = afrsp.EIOAssessmentForRetailServicePatientDatas.Where(e => !e.IsDeleted)
                .Select(e => new { e.Id, e.Code, e.Value })
                .ToList(),
            });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/EDAssessmentForRetailServicePatient/{id}")]
        [Permission(Code = "EAFRS4")]
        public IHttpActionResult UpdateEmergencyTriageRecordAPI(Guid id, [FromBody]JObject request)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var afrs = ed.EDAssessmentForRetailServicePatient;
            if (afrs == null || afrs.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.EIO_AFRS_NOT_FOUND);

            //var user = GetUser();
            //if (IsBlockAfter24h(ed.CreatedAt) && !HasUnlockPermission(ed.Id, "EDETR", user.Username))
            //    return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);

            UpdateVisit("ED", ed, afrs, request);

            HandleUpdateOrCreateAssessmentForRetailServicePatientData(ed, "ED", afrs, request["Datas"]);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }
    }
}
