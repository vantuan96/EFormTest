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
    public class EOCPatientAndFamilyEducationController : EIOPatientAndFamilyEducationController
    {
        [HttpGet]
        [Route("api/EOC/PatientAndFamilyEducation/Info/{type}/{visitId}")]
        [Permission(Code = "EOPFEF1")]
        public IHttpActionResult GetPatientAndFamilyEducationInfoAPI(Guid visitId, string type = "A03_045_290422_VE")
        {
            var visit = GetEOC(visitId);
            if (visit == null) return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);

            return Content(HttpStatusCode.OK,Message.SUCCESS);
        }

        [HttpGet]
        [Route("api/EOC/PatientAndFamilyEducation/{type}/{visitId}")]
        [Permission(Code = "EOPFEF1")]
        public IHttpActionResult GetPatientAndFamilyEducationAPI(Guid visitId, string type = "A03_045_290422_VE")
        {
            var visit = GetEOC(visitId);
            if (visit == null) return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);

            var result = GetListPatientAndFamilyEducation(visitId, "EOC");
            return Content(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("api/EOC/PatientAndFamilyEducation/{type}/{visitId}/{id}")]
        [Permission(Code = "EOPFEF2")]
        public IHttpActionResult GetDetailPatientAndFamilyEducationAPI(Guid visitId,Guid id, string type = "A03_045_290422_VE")
        {
            var visit = GetEOC(visitId);
            if (visit == null) return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);

            var result = BuildPatientAndFamilyEducationFormData(id,type, "EOC");
            return Content(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/EOC/PatientAndFamilyEducation/{type}/{visitId}")]
        [Permission(Code = "EOPFEF3")]
        public IHttpActionResult UpdateDetailPatientAndFamilyEducationAPI(Guid visitId, [FromBody]JObject request,string type = "A03_045_290422_VE")
        {
            var visit = GetEOC(visitId);
            if (visit == null) return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);

            var edu_form = GetOrCreatePatientAndFamilyEducationForm(visitId, "EOC", request["Id"]?.ToString(), app_version: visit.Version);

            HandleUpdateOrCreatePatientFamilyEducationInformations(edu_form, request["Informations"]);

            HandleUpdateOrCreatePatientFamilyEducationGroupContents(edu_form, request["GroupContents"]);

            return Content(HttpStatusCode.OK, new
            {
                edu_form.Id,
                edu_form.CreatedAt,
                edu_form.UpdatedAt,
                edu_form.CreatedBy,
                edu_form.UpdatedBy,
                edu_form.Version
            });
        }
    }
}
