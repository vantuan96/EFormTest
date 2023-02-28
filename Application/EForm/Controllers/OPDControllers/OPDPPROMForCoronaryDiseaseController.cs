using EForm.Authentication;
using EForm.Controllers.EIOControllers;
using EForm.Controllers.GeneralControllers;
using Newtonsoft.Json.Linq;
using System;
using System.Web.Http;

namespace EForm.Controllers.OPDControllers
{
    [SessionAuthorize]
    public class OPDPPROMForCoronaryDiseaseController : PROMForCoronaryDiseaseController
    {
        private readonly string vistiType = "OPD";
        [HttpGet]
        [Route("api/OPD/PROMForCoronaryDisease/Info/{visitId}")]
        public IHttpActionResult OPDGetInfoForm(Guid visitId)
        {
            return GetInfoForm(visitId, vistiType);
        }

        [HttpGet]
        [Route("api/OPD/PROMForCoronaryDisease/GetFormByVisitId/{visitId}")]
        [Permission(Code = "OPDPROMFCD1")]
        public IHttpActionResult OPDGetFormByVisitId(Guid visitId)
        {
            return GetFormByVisitId(visitId, vistiType);
        }

        [Route("api/OPD/PROMForCoronaryDisease/Create/{visitId}")]
        [Permission(Code = "OPDPROMFCD2")]
        public IHttpActionResult OPDCreateForm(Guid visitId)
        {
            return CreateForm(visitId, vistiType);
        }

        [HttpPost]
        [Route("api/OPD/PROMForCoronaryDisease/Update/{visitId}")]
        [Permission(Code = "OPDPROMFCD3")]
        public IHttpActionResult OPDUpdateForm(Guid visitId, [FromBody] JObject request)
        {
            return UpdateForm(visitId,vistiType, request);
        }

        [HttpPost]
        [Route("api/OPD/PROMForCoronaryDisease/Confirm/{visitId}")]
        [Permission(Code = "OPDPROMFCD4")]
        public IHttpActionResult OPDConfirmAPI(Guid visitId, [FromBody] JObject request)
        {
            return ConfirmAPI(visitId, vistiType, request);
        }
    }
}