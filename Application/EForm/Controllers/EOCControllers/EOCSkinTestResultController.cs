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
    public class EOCSkinTestResultController : EIOSkinTestResultController
    {
        
        [HttpGet]
        [Route("api/EOC/SkinTestResult/{id}")]
        [Permission(Code = "EOC050")]
        public IHttpActionResult GetSkinTestResultAPI(Guid id)
        {
            var visit = GetEOC(id);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);

            var skin_test_result = GetEIOSkinTestResult(visit.Id, "EOC");
            if (skin_test_result == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_STR_NOT_FOUND);

            return Content(HttpStatusCode.OK, GetSkinTestResultDetail(skin_test_result));
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/EOC/SkinTestResult/Create/{id}")]
        [Permission(Code = "EOC051")]
        public IHttpActionResult CreateSkinTestResultAPI(Guid id)
        {
            var visit = GetEOC(id);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);

            var skin_test_result = GetEIOSkinTestResult(visit.Id, "EOC");

            if (skin_test_result != null)
                return Content(HttpStatusCode.NotFound, Message.EIO_STR_EXIST);

            CreateEIOSkinTestResult(visit.Id, "EOC");

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/EOC/SkinTestResult/{id}")]
        [Permission(Code = "EOC052")]
        public IHttpActionResult UpdateSkinTestResultAPI(Guid id, [FromBody]JObject request)
        {
            var visit = GetEOC(id);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);

            var skin_test_result = GetEIOSkinTestResult(visit.Id, "EOC");
            if (skin_test_result == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_STR_NOT_FOUND);

            if (skin_test_result.ConfirmDoctorId != null)
                return Content(HttpStatusCode.OK, Message.OWNER_FORBIDDEN);

            UpdateEIOSkinTestResult(skin_test_result, request);
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/EOC/SkinTestResult/Accept/{id}")]
        [Permission(Code = "EOC053")]
        public IHttpActionResult AcceptSkinTestResultAPI(Guid id, [FromBody]JObject request)
        {
            var visit = GetEOC(id);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);

            var skin_test_result = GetEIOSkinTestResult(visit.Id, "EOC");
            if (skin_test_result == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_STR_NOT_FOUND);

            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);

            var positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName);
            if (!positions.Contains("Doctor"))
                return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);

            AcceptSkinTestResult(skin_test_result, user.Id);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/EOC/SkinTestResult/Delete/{id}")]
        [Permission(Code = "EOC054")]
        public IHttpActionResult DeleteSkinTestResultAPI(Guid id)
        {
            var visit = GetEOC(id);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);

            var skin_test_result = GetEIOSkinTestResult(visit.Id, "EOC");
            if (skin_test_result == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_STR_NOT_FOUND);
            if (skin_test_result.ConfirmDoctorId != null)
                return Content(HttpStatusCode.BadRequest, Message.OWNER_FORBIDDEN);

            DeleteSkinTestResult(skin_test_result);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }
    }
}
