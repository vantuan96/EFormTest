using DataAccess.Models;
using DataAccess.Models.GeneralModel;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Client;
using EForm.Common;
using EForm.Models;
using EForm.Models.DiagnosticReporting;
using EForm.Services;
using EMRModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.ESignControllers
{
    public class ESignController : BaseApiController
    {
        // GET: ESign
        [HttpPost]
        [CSRFCheck]
        [Route("api/esign-confirm")]
        [Permission(Code = "DRS000xxx7")]
        public IHttpActionResult Index([FromBody] EsignRequestModel request)
        {
            return Content(HttpStatusCode.OK, new {
            });
        }
    }
}