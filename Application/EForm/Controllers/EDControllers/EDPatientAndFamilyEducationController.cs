using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers.BaseEIOControllers;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.EDControllers
{
    [SessionAuthorize]
    public class EDPatientAndFamilyEducationController : EIOPatientAndFamilyEducationController
    {
        [HttpGet]
        [Route("api/ED/PatientAndFamilyEducation/Info/{type}/{visitId}")]
        [Permission(Code = "EPFEF1")]
        public IHttpActionResult GetPatientAndFamilyEducationInfoAPI(Guid visitId, string type = "A03_045_290422_VE")
        {
            var visit = GetED(visitId);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpGet]
        [Route("api/ED/PatientAndFamilyEducation/{type}/{visitId}")]
        [Permission(Code = "EPFEF1")]
        public IHttpActionResult GetPatientAndFamilyEducationAPI(Guid visitId,string type = "A03_045_290422_VE")
        {
            var visit = GetED(visitId);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var result = GetListPatientAndFamilyEducation(visitId, "ED");
            return Content(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("api/ED/PatientAndFamilyEducation/{type}/{visitId}/{id}")]
        [Permission(Code = "EPFEF2")]
        public IHttpActionResult GetDetailPatientAndFamilyEducationAPI(Guid visitId, Guid id, string type = "A03_045_290422_VE")
        {
            var visit = GetED(visitId);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);
            var result = BuildPatientAndFamilyEducationFormData(id,type,"ED");
            return Content(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/PatientAndFamilyEducation/{type}/{visitId}")]
        [Permission(Code = "EPFEF3")]
        public IHttpActionResult UpdateDetailPatientAndFamilyEducationAPI(Guid visitId, [FromBody]JObject request,string type = "A03_045_290422_VE")
        {
            var visit = GetED(visitId);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);


            //var user = GetUser();
            //if (IsBlockAfter24h(ed.CreatedAt) && !HasUnlockPermission(ed.Id, "EDPFEF", user.Username))
            //    return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);

            var edu_form = GetOrCreatePatientAndFamilyEducationForm(visitId, "ED", request["Id"]?.ToString(), app_version: visit.Version);

            HandleUpdateOrCreatePatientFamilyEducationInformations(edu_form, request["Informations"]);

            HandleUpdateOrCreatePatientFamilyEducationGroupContents(edu_form, request["GroupContents"]);

            return Content(HttpStatusCode.OK, new { 
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
