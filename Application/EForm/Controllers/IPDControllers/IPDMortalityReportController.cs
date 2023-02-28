using DataAccess.Models;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers.BaseEIOControllers;
using EForm.Models.PrescriptionModels;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;

namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class IPDMortalityReportController : EIOMortalityReportController
    {
        [HttpGet]
        [Route("api/IPD/MortalityReport/{id}")]
        // [Permission(Code = "IMORE1")]
        public IHttpActionResult GetMortalityReportAPI(Guid id)
        {
            var visit = GetIPD(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);

            var mortality = GetEIOMortalityReport(visit.Id, "IPD");
            var IsLocked = IPDIsBlock(visit, Constant.IPDFormCode.BienBanKiemThaoTuVong);
            if (mortality == null)
                return Content(HttpStatusCode.NotFound, new
                {
                    Message = Message.FORM_NOT_FOUND,
                    IsLocked = IsLocked,
                });                  
            
            var mortalityReport = GetMortalityReportResult(mortality, visit, "IPD");            
            return Content(HttpStatusCode.OK, mortalityReport);
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/IPD/MortalityReport/Create/{id}")]
        // [Permission(Code = "IMORE2")]
        public IHttpActionResult CreateMortalityReportAPI(Guid id)
        {
            var visit = GetIPD(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);

            var mortality = GetEIOMortalityReport(visit.Id, "IPD");
            if (mortality != null)
                return Content(HttpStatusCode.OK, Message.FORM_EXIST);

            CreateEIOMortalityReport(visit.Id, "IPD");
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/IPD/MortalityReport/{id}")]
        // [Permission(Code = "IMORE3")]
        public IHttpActionResult UpdateMortalityReportAPI(Guid id, [FromBody] JObject request)
        {
            var visit = GetIPD(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);

            var mortality = GetEIOMortalityReport(visit.Id, "IPD");
            if (mortality == null)
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);

            var members = mortality.EDMortalityReportMembers.Where(e => !e.IsDeleted);//chưa check null           
           
            if (mortality.ChairmanTime != null || mortality.SecretaryTime != null || members.FirstOrDefault(e => e.ConfirmTime != null) != null)
                return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);

            UpdateMortalityReport(mortality, members, request, app_version: visit.Version);
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/IPD/MortalityReport/Sync/{id}")]
        // [Permission(Code = "IMORE4")]
        public IHttpActionResult SyncMortalityReportAPI(Guid id, [FromBody] JObject request)
        {
            var visit = GetIPD(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);

            var req_method = request["Method"]?.ToString();
            if (string.IsNullOrEmpty(req_method))
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            Type thisType = this.GetType();
            MethodInfo method_info = thisType.GetMethod(req_method, BindingFlags.Instance | BindingFlags.NonPublic);
            if (method_info == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            object[] parameters = new object[] { visit };
            var result = method_info.Invoke(this, parameters);
            return Content(HttpStatusCode.OK, result);
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/IPD/MortalityReport/Confirm/{id}")]
        // [Permission(Code = "IMORE5")]
        public IHttpActionResult ConfirmMortalityReportAPI(Guid id, [FromBody] JObject request)
        {
            var visit = GetIPD(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);

            var mortality = GetEIOMortalityReport(visit.Id, "IPD");
            if (mortality == null)
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);          

            var error = ConfirmMortalityReport(mortality, request);
            if (error != null)
                return Content(HttpStatusCode.NotFound, error);
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }
    }
}

