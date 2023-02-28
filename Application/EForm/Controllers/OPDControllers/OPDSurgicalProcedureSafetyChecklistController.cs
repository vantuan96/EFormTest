using DataAccess.Models.EIOModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers.BaseEIOControllers;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.OPDControllers
{
    [SessionAuthorize]
    public class OPDSurgicalProcedureSafetyChecklistController : EIOSurgicalProcedureSafetyChecklistController
    {
        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/SurgicalProcedureSafetyChecklist/Create/{id}")]
        [Permission(Code = "OSPSC1")]
        public IHttpActionResult CreateSurgicalProcedureSafetyChecklistAPI(Guid id)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var user = GetUser();
            if (IsBlockAfter24h(opd.CreatedAt) && !HasUnlockPermission(opd.Id, "OPDSPSC", user.Username))
                return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);

            var spsc = GetSurgicalProcedureSafetyChecklist(opd.Id, "OPD");

            if (spsc != null)
                return Content(HttpStatusCode.NotFound, Message.EIO_SPSC_EXIST);

            spsc = new EIOSurgicalProcedureSafetyChecklist()
            {
                VisitId = opd.Id,
                VisitTypeGroupCode = "OPD",
            };
            unitOfWork.EIOSurgicalProcedureSafetyChecklistRepository.Add(spsc);
            unitOfWork.Commit();

            return Content(HttpStatusCode.OK, new { spsc.Id });
        }

        [HttpGet]
        [Route("api/OPD/SurgicalProcedureSafetyChecklist/{id}")]
        [Permission(Code = "OSPSC2")]
        public IHttpActionResult GetSurgicalProcedureSafetyChecklistAPI(Guid id)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var user = GetUser();
            bool IsLocked = Is24hLocked(opd.CreatedAt, opd.Id, "OPDSPSC", user.Username);

            var spsc = GetSurgicalProcedureSafetyChecklist(opd.Id, "OPD");

            if (spsc == null)
                return Content(HttpStatusCode.NotFound, new
                {
                    ViMessage = "Biên bản hội chẩn không tồn tại",
                    EnMessage = "Joint consultation group minutes is not found",
                    IsLocked
                });

            return Content(HttpStatusCode.OK, new
            {
                spsc.Id,
                SignIn = spsc.EIOSurgicalProcedureSafetyChecklistSignInId,
                TimeOut = spsc.EIOSurgicalProcedureSafetyChecklistTimeOutId,
                SignOut = spsc.EIOSurgicalProcedureSafetyChecklistSignOutId,
                IsLocked = IsLocked,
                opd.Version
            });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/SurgicalProcedureSafetyChecklist/SignIn/Create/{id}")]
        [Permission(Code = "OSPSC3")]
        public IHttpActionResult CreateSurgicalProcedureSafetyChecklistSignInAPI(Guid id)
        {
            var spsc = unitOfWork.EIOSurgicalProcedureSafetyChecklistRepository.GetById(id);
            if (spsc == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_SPSC_NOT_FOUND);
            if (spsc.EIOSurgicalProcedureSafetyChecklistSignInId != null)
                return Content(HttpStatusCode.NotFound, Message.EIO_SPSCSI_EXIST);

            var opd = GetOPD((Guid)spsc.VisitId);

            var user = GetUser();
            if (IsBlockAfter24h(opd.CreatedAt) && !HasUnlockPermission(opd.Id, "OPDSPSC", user.Username))
                return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);

            var spsc_si = new EIOSurgicalProcedureSafetyChecklistSignIn();
            unitOfWork.EIOSurgicalProcedureSafetyChecklistSignInRepository.Add(spsc_si);
            spsc.EIOSurgicalProcedureSafetyChecklistSignInId = spsc_si.Id;
            unitOfWork.EIOSurgicalProcedureSafetyChecklistRepository.Update(spsc);
            unitOfWork.Commit();

            return Content(HttpStatusCode.OK, new { spsc_si.Id });
        }

        [HttpGet]
        [Route("api/OPD/SurgicalProcedureSafetyChecklist/SignIn/{id}")]
        [Permission(Code = "OSPSC4")]
        public IHttpActionResult GetSurgicalProcedureSafetyChecklistSignInAPI(Guid id)
        {
            var spsc_si = unitOfWork.EIOSurgicalProcedureSafetyChecklistSignInRepository.GetById(id);
            if (spsc_si == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_SPSCSI_NOT_FOUND);

            var datas = GetSurgicalProcedureSafetyChecklistData(spsc_si.Id, "SignIn").Select(e=> new { e.Code, e.Value, e.EnValue });

            var nurse = unitOfWork.UserRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                !string.IsNullOrEmpty(e.Username) &&
                e.Username == spsc_si.UpdatedBy
            );

            return Content(HttpStatusCode.OK, new
            {
                spsc_si.Id,
                CreatedAt = spsc_si.CreatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                Nurse = new {nurse?.Username, nurse?.Fullname, nurse?.DisplayName, nurse?.Title },
                Datas = datas,
            });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/SurgicalProcedureSafetyChecklist/SignIn/{id}")]
        [Permission(Code = "OSPSC5")]
        public IHttpActionResult UpdateSurgicalProcedureSafetyChecklistSignInAPI(Guid id, [FromBody]JObject request)
        {
            var spsc_si = unitOfWork.EIOSurgicalProcedureSafetyChecklistSignInRepository.GetById(id);
            if (spsc_si == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_SPSCSI_NOT_FOUND);

            var spsc = unitOfWork.EIOSurgicalProcedureSafetyChecklistRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.EIOSurgicalProcedureSafetyChecklistSignInId != null &&
                e.EIOSurgicalProcedureSafetyChecklistSignInId == spsc_si.Id
            );

            var opd = GetOPD((Guid)spsc.VisitId);

            var user = GetUser();
            if (IsBlockAfter24h(opd.CreatedAt) && !HasUnlockPermission(opd.Id, "OPDSPSC", user.Username))
                return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);

            if (spsc_si.CreatedBy != user.Username)
                return Content(HttpStatusCode.BadRequest, Message.OWNER_FORBIDDEN);

            HandleSurgicalProcedureSafetyChecklistData(spsc_si.Id, "SignIn", request["Datas"]);
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/SurgicalProcedureSafetyChecklist/TimeOut/Create/{id}")]
        [Permission(Code = "OSPSC6")]
        public IHttpActionResult CreateSurgicalProcedureSafetyChecklistTimeOutAPI(Guid id)
        {
            var spsc = unitOfWork.EIOSurgicalProcedureSafetyChecklistRepository.GetById(id);
            if (spsc == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_SPSC_NOT_FOUND);
            if (spsc.EIOSurgicalProcedureSafetyChecklistTimeOutId != null)
                return Content(HttpStatusCode.NotFound, Message.EIO_SPSCTO_EXIST);

            if (HaveUncompletedSafetyChecklistData(spsc.EIOSurgicalProcedureSafetyChecklistSignInId, "SignIn"))
                return Content(HttpStatusCode.BadRequest, Message.EIO_SPSCTO_CANT_CREATE);

            var opd = GetOPD((Guid)spsc.VisitId);

            var user = GetUser();
            if (IsBlockAfter24h(opd.CreatedAt) && !HasUnlockPermission(opd.Id, "OPDSPSC", user.Username))
                return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);

            var spsc_to = new EIOSurgicalProcedureSafetyChecklistTimeOut();
            unitOfWork.EIOSurgicalProcedureSafetyChecklistTimeOutRepository.Add(spsc_to);
            spsc.EIOSurgicalProcedureSafetyChecklistTimeOutId = spsc_to.Id;
            unitOfWork.EIOSurgicalProcedureSafetyChecklistRepository.Update(spsc);
            unitOfWork.Commit();

            return Content(HttpStatusCode.OK, new { spsc_to.Id });
        }

        [HttpGet]
        [Route("api/OPD/SurgicalProcedureSafetyChecklist/TimeOut/{id}")]
        [Permission(Code = "OSPSC7")]
        public IHttpActionResult GetSurgicalProcedureSafetyChecklistTimeOutAPI(Guid id)
        {
            var spsc_to = unitOfWork.EIOSurgicalProcedureSafetyChecklistTimeOutRepository.GetById(id);
            if (spsc_to == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_SPSCTO_NOT_FOUND);

            var datas = GetSurgicalProcedureSafetyChecklistData(spsc_to.Id, "TimeOut").Select(e => new { e.Code, e.Value, e.EnValue });

            var nurse = unitOfWork.UserRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                !string.IsNullOrEmpty(e.Username) &&
                e.Username == spsc_to.UpdatedBy
            );

            return Content(HttpStatusCode.OK, new
            {
                spsc_to.Id,
                CreatedAt = spsc_to.CreatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                Nurse = new { nurse?.Username, nurse?.Fullname, nurse?.DisplayName, nurse?.Title },
                Datas = datas,
            });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/SurgicalProcedureSafetyChecklist/TimeOut/{id}")]
        [Permission(Code = "OSPSC8")]
        public IHttpActionResult UpdateSurgicalProcedureSafetyChecklistTimeOutAPI(Guid id, [FromBody]JObject request)
        {
            var spsc_to = unitOfWork.EIOSurgicalProcedureSafetyChecklistTimeOutRepository.GetById(id);
            if (spsc_to == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_SPSCTO_NOT_FOUND);

            var spsc = unitOfWork.EIOSurgicalProcedureSafetyChecklistRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.EIOSurgicalProcedureSafetyChecklistTimeOutId != null &&
                e.EIOSurgicalProcedureSafetyChecklistTimeOutId == spsc_to.Id
            );

            var opd = GetOPD((Guid)spsc.VisitId);

            var user = GetUser();
            if (IsBlockAfter24h(opd.CreatedAt) && !HasUnlockPermission(opd.Id, "OPDSPSC", user.Username))
                return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);

            if (spsc_to.CreatedBy != user.Username)
                return Content(HttpStatusCode.BadRequest, Message.OWNER_FORBIDDEN);

            HandleSurgicalProcedureSafetyChecklistData(spsc_to.Id, "TimeOut", request["Datas"]);
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/SurgicalProcedureSafetyChecklist/SignOut/Create/{id}")]
        [Permission(Code = "OSPSC9")]
        public IHttpActionResult CreateSurgicalProcedureSafetyChecklistSignOutAPI(Guid id)
        {
            var spsc = unitOfWork.EIOSurgicalProcedureSafetyChecklistRepository.GetById(id);
            if (spsc == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_SPSC_NOT_FOUND);
            if (spsc.EIOSurgicalProcedureSafetyChecklistSignOutId != null)
                return Content(HttpStatusCode.NotFound, Message.EIO_SPSCSI_EXIST);

            if (HaveUncompletedSafetyChecklistData(spsc.EIOSurgicalProcedureSafetyChecklistTimeOutId, "TimeOut"))
                return Content(HttpStatusCode.BadRequest, Message.EIO_SPSCSO_CANT_CREATE);

            var opd = GetOPD((Guid)spsc.VisitId);

            var user = GetUser();
            if (IsBlockAfter24h(opd.CreatedAt) && !HasUnlockPermission(opd.Id, "OPDSPSC", user.Username))
                return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);

            var spsc_si = new EIOSurgicalProcedureSafetyChecklistSignOut();
            unitOfWork.EIOSurgicalProcedureSafetyChecklistSignOutRepository.Add(spsc_si);
            spsc.EIOSurgicalProcedureSafetyChecklistSignOutId = spsc_si.Id;
            unitOfWork.EIOSurgicalProcedureSafetyChecklistRepository.Update(spsc);
            unitOfWork.Commit();

            return Content(HttpStatusCode.OK, new { spsc_si.Id });
        }

        [HttpGet]
        [Route("api/OPD/SurgicalProcedureSafetyChecklist/SignOut/{id}")]
        [Permission(Code = "OSPSC10")]
        public IHttpActionResult GetSurgicalProcedureSafetyChecklistSignOutAPI(Guid id)
        {
            var spsc_so = unitOfWork.EIOSurgicalProcedureSafetyChecklistSignOutRepository.GetById(id);
            if (spsc_so == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_SPSCTO_NOT_FOUND);

            var datas = GetSurgicalProcedureSafetyChecklistData(spsc_so.Id, "SignOut").Select(e => new { e.Code, e.Value, e.EnValue });

            var nurse = unitOfWork.UserRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                !string.IsNullOrEmpty(e.Username) &&
                e.Username == spsc_so.UpdatedBy
            );

            return Content(HttpStatusCode.OK, new
            {
                spsc_so.Id,
                CreatedAt = spsc_so.CreatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                Nurse = new { nurse?.Username, nurse?.Fullname, nurse?.DisplayName, nurse?.Title },
                Datas = datas,
            });
        }    

        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/SurgicalProcedureSafetyChecklist/SignOut/{id}")]
        [Permission(Code = "OSPSC11")]
        public IHttpActionResult UpdateSurgicalProcedureSafetyChecklistSignOutAPI(Guid id, [FromBody]JObject request)
        {
            var spsc_so = unitOfWork.EIOSurgicalProcedureSafetyChecklistSignOutRepository.GetById(id);
            if (spsc_so == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_SPSCSO_NOT_FOUND);

            var spsc = unitOfWork.EIOSurgicalProcedureSafetyChecklistRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.EIOSurgicalProcedureSafetyChecklistSignOutId != null &&
                e.EIOSurgicalProcedureSafetyChecklistSignOutId == spsc_so.Id
            );

            var opd = GetOPD((Guid)spsc.VisitId);

            var user = GetUser();
            if (IsBlockAfter24h(opd.CreatedAt) && !HasUnlockPermission(opd.Id, "OPDSPSC", user.Username))
                return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);

            if (spsc_so.CreatedBy != user.Username)
                return Content(HttpStatusCode.BadRequest, Message.OWNER_FORBIDDEN);

            HandleSurgicalProcedureSafetyChecklistData(spsc_so.Id, "SignOut", request["Datas"]);
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

    }
}
