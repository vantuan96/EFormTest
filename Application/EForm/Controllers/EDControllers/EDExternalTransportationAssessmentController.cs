using DataAccess.Models.EIOModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers.BaseEIOControllers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.EDControllers
{
    [SessionAuthorize]
    public class EDExternalTransportationAssessmentController : EIOExternalTransportationAssessmentController
    {
        [CSRFCheck]
        [HttpPost]
        [Route("api/ED/ExternalTransportationAssessment/Create/{visitId}")]
        [Permission(Code = "EEXTA1")]
        public IHttpActionResult CreateExternalTransportationAssessmentAPI(Guid visitId)
        {
            var ed = GetED(visitId);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            //var ex_assess = GetEIOExternalTransportationAssessment(ed.Id, "ED");
            //if (ex_assess != null)
            //    return Content(HttpStatusCode.NotFound, Message.ED_EXTA_EXIST);

            EIOExternalTransportationAssessment form = CreateEIOExternalTransportationAssessment(ed.Id, "ED");
            return Content(HttpStatusCode.OK, new
            {
                Message.SUCCESS,
                ItemId = form.Id
            });
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/ED/ExternalTransportationAssessment/Confirm/{visitId}/{itemId}")]
        [Permission(Code = "EEXTA2")]
        public IHttpActionResult ConfirmExternalTransportationAssessmentAPI(Guid visitId, Guid itemId,  [FromBody]JObject request)
        {
            var ed = GetED(visitId);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var ex_assess = GetEIOExternalTransportationAssessment(ed.Id, itemId, "ED");
            if (ex_assess == null)
                return Content(HttpStatusCode.NotFound, Message.ED_EXTA_NOT_FOUND);

            var error = ConfirmExternalTransportationAssessment(ex_assess, request);
            if (error != null)
            {
                return Content(HttpStatusCode.Forbidden, error);
            }    

            var user = GetUser();
            if (user == null) return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);

            var positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName).ToList();

            HandleUpdateOrCreateExternalTransportationAssessment(ex_assess, positions, request, ed);
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpGet]
        [Route("api/ED/ExternalTransportationAssessment/{visitId}/{itemId}")]
        [Permission(Code = "EEXTA3")]
        public IHttpActionResult GetExternalTransportationAssessmentAPI(Guid visitId, Guid itemId)
        {
            var ed = GetED(visitId);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var ex_assess = GetEIOExternalTransportationAssessment(ed.Id, itemId, "ED");
            if (ex_assess == null)
                return Content(HttpStatusCode.NotFound, Message.ED_EXTA_NOT_FOUND);

            return Content(HttpStatusCode.OK, GetExternalTransportationAssessmentResult(ex_assess));
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/ED/ExternalTransportationAssessment/{visitId}/{itemId}")]
        [Permission(Code = "EEXTA4")]
        public IHttpActionResult UpdateExternalTransportationAssessmentAPI(Guid visitId, Guid itemId, [FromBody]JObject request)
        {
            var ed = GetED(visitId);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var ex_assess = GetEIOExternalTransportationAssessment(ed.Id,itemId, "ED");
            if (ex_assess == null)
                return Content(HttpStatusCode.NotFound, Message.ED_EXTA_NOT_FOUND);

            if (ex_assess.DoctorId != null || ex_assess.NurseId != null)
                return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);

            ex_assess.UpdatedAt = DateTime.Now;
            var user = GetUser();
            if (user == null) return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);
            ex_assess.UpdatedBy = user.Username;
            var positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName).ToList();

            HandleUpdateOrCreateExternalTransportationAssessment(ex_assess, positions, request, ed);
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpGet]
        [Route("api/ED/ExternalTransportationAssessment/List/{visitId}")]
        [Permission(Code = "EEXTA5")]
        public IHttpActionResult GetListExternalTransportationAssessmentAPI(Guid visitId)
        {
            var visit = GetED(visitId);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);

            List<EIOExternalTransportationAssessment> listEIOExternalTransportationAssessment = GetListEIOExternalTransportationAssessment(visit.Id, "ED");

            return Content(HttpStatusCode.OK, new
            {
                EIOExternalTransportationAssessments = listEIOExternalTransportationAssessment,
            });
        }

    }
}
