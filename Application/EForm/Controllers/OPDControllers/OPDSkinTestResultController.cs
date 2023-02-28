using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers.BaseEIOControllers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace EForm.Controllers.OPDControllers
{
    [SessionAuthorize]
    public class OPDSkinTestResultController : EIOSkinTestResultController
    {
        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/SkinTestResult/Create/{id}")]
        [Permission(Code = "OSKTR1")]
        public IHttpActionResult CreateSkinTestResultAPI(Guid id)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var user = GetUser();
            //if (IsBlockAfter24h(opd.CreatedAt) && !HasUnlockPermission(opd.Id, "OSKTR", user.Username))
               // return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);

            var skin_test_result = GetEIOSkinTestResult(opd.Id, "OPD");

            if (skin_test_result != null)
                return Content(HttpStatusCode.NotFound, Message.EIO_STR_EXIST);

            CreateEIOSkinTestResult(opd.Id, "OPD");

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpGet]
        [Route("api/OPD/SkinTestResult/{id}")]
        [Permission(Code = "OSKTR2")]
        public IHttpActionResult GetSkinTestResultAPI(Guid id)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var skin_test_result = GetEIOSkinTestResult(opd.Id, "OPD");
            var user = GetUser();
            bool IsLocked = false;
            if (skin_test_result == null)
                return Content(HttpStatusCode.NotFound, new
                {
                    ViMessage = "Kết quả test da không tồn tại",
                    EnMessage = "Skin test result is not found",
                    IsLocked = Is24hLocked(opd.CreatedAt, id, "OSKTR", user.Username)
                }); ;
            IsLocked = Is24hLocked(opd.CreatedAt, id, "OSKTR", user.Username, skin_test_result.Id);
            var result = GetSkinTestResultDetail(skin_test_result);            
            return Content(HttpStatusCode.OK, new { result, IsLocked });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/SkinTestResult/{id}")]
        [Permission(Code = "OSKTR3")]
        public IHttpActionResult UpdateSkinTestResultAPI(Guid id, [FromBody]JObject request)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            //var user = GetUser();
            //if (IsBlockAfter24h(opd.CreatedAt) && !HasUnlockPermission(opd.Id, "OSKTR", user.Username))
            //    return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);

            var skin_test_result = GetEIOSkinTestResult(opd.Id, "OPD");
            if (skin_test_result == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_STR_NOT_FOUND);            
            if (skin_test_result.ConfirmDoctorId != null)
                return Content(HttpStatusCode.OK, Message.OWNER_FORBIDDEN);

            UpdateEIOSkinTestResult(skin_test_result, request);
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/SkinTestResult/Accept/{id}")]
        [Permission(Code = "OSKTR4")]
        public IHttpActionResult AcceptSkinTestResultAPI(Guid id, [FromBody]JObject request)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var skin_test_result = GetEIOSkinTestResult(opd.Id, "OPD");
            if (skin_test_result == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_STR_NOT_FOUND);            
            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);
            if (IsBlockAfter24h(opd.CreatedAt, skin_test_result.Id) && !HasUnlockPermission(opd.Id, "OSKTR", user.Username, skin_test_result.Id))
                return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);

            var positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName);
            if (!positions.Contains("Doctor"))
                return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);

            AcceptSkinTestResult(skin_test_result, user.Id);
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/OPD/SkinTestResult/Delete/{id}")]
        [Permission(Code = "OSKTR5")]
        public IHttpActionResult DeleteSkinTestResultAPI(Guid id)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var skin_test_result = GetEIOSkinTestResult(opd.Id, "OPD");
            if (skin_test_result == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_STR_NOT_FOUND);
            if (skin_test_result.ConfirmDoctorId != null)
                return Content(HttpStatusCode.BadRequest, Message.OWNER_FORBIDDEN);

            DeleteSkinTestResult(skin_test_result);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }
    }
}