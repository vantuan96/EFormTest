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
    public class OPDPatientAndFamilyEducationController : EIOPatientAndFamilyEducationController
    {
        [HttpGet]
        [Route("api/OPD/PatientAndFamilyEducation/Info/{type}/{visitId}")]
        [Permission(Code = "OPFEF1")]
        public IHttpActionResult GetPatientAndFamilyEducationInfoAPI(Guid visitId, string type = "A03_045_290422_VE")
        {
            var visit = GetOPD(visitId);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);
            var user = GetUser();
            return Content(HttpStatusCode.OK, new { IsLocked24h = Is24hLocked(visit.CreatedAt, visitId, type, user.Username) });
        }

        [HttpGet]
        [Route("api/OPD/PatientAndFamilyEducation/{type}/{visitId}")]
        [Permission(Code = "OPFEF1")]
        public IHttpActionResult GetPatientAndFamilyEducationAPI(Guid visitId, string type = "A03_045_290422_VE")
        {
            var visit = GetOPD(visitId);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var result = GetListPatientAndFamilyEducation(visitId, "OPD");
            return Content(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("api/OPD/PatientAndFamilyEducation/{type}/{visitId}/{id}")]
        [Permission(Code = "OPFEF2")]
        public IHttpActionResult GetDetailPatientAndFamilyEducationAPI(Guid visitId,Guid id, string type = "A03_045_290422_VE")
        {
            var visit = GetOPD(visitId);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var result = BuildPatientAndFamilyEducationFormData(id,type,"OPD");
            return Content(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/PatientAndFamilyEducation/{type}/{visitId}")]
        [Permission(Code = "OPFEF3")]
        public IHttpActionResult UpdateDetailPatientAndFamilyEducationAPI(Guid visitId, [FromBody]JObject request,string type = "A03_045_290422_VE")
        {
            var visit = GetOPD(visitId);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var edu_form = GetOrCreatePatientAndFamilyEducationForm(visitId, "OPD", request["Id"]?.ToString(), app_version: visit.Version);

            HandleUpdateOrCreatePatientFamilyEducationInformations(edu_form, request["Informations"]);

            HandleUpdateOrCreatePatientFamilyEducationGroupContents(edu_form, request["GroupContents"]);

            return Content(HttpStatusCode.OK, new
            {
                edu_form.Id,
                edu_form.CreatedAt,
                edu_form.UpdatedAt,
                edu_form.CreatedBy,
                edu_form.UpdatedBy
            });
        }
    }
}
