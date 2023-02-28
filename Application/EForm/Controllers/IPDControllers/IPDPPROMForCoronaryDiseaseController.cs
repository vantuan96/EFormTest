using EForm.Authentication;
using EForm.Controllers.EIOControllers;
using EForm.Controllers.GeneralControllers;
using Newtonsoft.Json.Linq;
using System;
using System.Web.Http;

namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class IPDPPROMForCoronaryDiseaseController : PROMForCoronaryDiseaseController
    {
        private readonly string vistiType = "IPD";
        [HttpGet]
        [Route("api/IPD/PROMForCoronaryDisease/Info/{visitId}")]
        public IHttpActionResult IPDGetInfoForm(Guid visitId)
        {
            return GetInfoForm(visitId, vistiType);
        }

        [HttpGet]
        [Route("api/IPD/PROMForCoronaryDisease/GetFormByVisitId/{visitId}")]
        [Permission(Code = "IPDPROMFCD1")]
        public IHttpActionResult IPDGetFormByVisitId(Guid visitId)
        {
            return GetFormByVisitId(visitId, vistiType);
        }

        [Route("api/IPD/PROMForCoronaryDisease/Create/{visitId}")]
        [Permission(Code = "IPDPROMFCD2")]
        public IHttpActionResult IPDCreateForm(Guid visitId)
        {
            return CreateForm(visitId, vistiType);
        }

        [HttpPost]
        [Route("api/IPD/PROMForCoronaryDisease/Update/{visitId}")]
        [Permission(Code = "IPDPROMFCD3")]
        public IHttpActionResult IPDUpdateForm(Guid visitId, [FromBody] JObject request)
        {
            return UpdateForm(visitId,vistiType, request);
        }

        [HttpPost]
        [Route("api/IPD/PROMForCoronaryDisease/Confirm/{visitId}")]
        [Permission(Code = "IPDPROMFCD4")]
        public IHttpActionResult IPDConfirmAPI(Guid visitId, [FromBody] JObject request)
        {
            return ConfirmAPI(visitId, vistiType, request);
        }
    }
}