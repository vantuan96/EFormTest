using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers.BaseEIOControllers;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class IPDPatientAndFamilyEducationController : EIOPatientAndFamilyEducationController
    {
        [HttpGet]
        [Route("api/IPD/PatientAndFamilyEducation/Info/{type}/{visitId}")]
        [Permission(Code = "IPFEF1")]
        public IHttpActionResult GetPatientAndFamilyEducationInfoAPI(Guid visitId, string type = "A03_045_290422_VE")
        {
            var visit = GetIPD(visitId);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            return Content(HttpStatusCode.OK, new { IsLocked = IPDIsBlock(visit, Constant.IPDFormCode.GDSKchoNBvaThanNhan) });
        }

        [HttpGet]
        [Route("api/IPD/PatientAndFamilyEducation/{type}/{visitId}")]
        [Permission(Code = "IPFEF1")]
        public IHttpActionResult GetPatientAndFamilyEducationAPI(Guid visitId, string type = "A03_045_290422_VE")
        {
            var visit = GetIPD(visitId);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var result = GetListPatientAndFamilyEducation(visitId, "IPD");
            return Content(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("api/IPD/PatientAndFamilyEducation/{type}/{visitId}/{id}")]
        [Permission(Code = "IPFEF2")]
        public IHttpActionResult GetDetailPatientAndFamilyEducationAPI(Guid visitId, Guid id,string type = "A03_045_290422_VE")
        {
            var vissit = GetIPD(visitId);
            if (vissit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var result = BuildPatientAndFamilyEducationFormData(id,type,"IPD");
            return Content(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/PatientAndFamilyEducation/{type}/{visitId}")]
        [Permission(Code = "IPFEF3")]
        public IHttpActionResult UpdateDetailPatientAndFamilyEducationAPI(Guid visitId, [FromBody]JObject request, string type = "A03_045_290422_VE")
        {
            var visit = GetIPD(visitId);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var edu_form = GetOrCreatePatientAndFamilyEducationForm(visitId, "IPD", request["Id"]?.ToString(), app_version: visit.Version);

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
