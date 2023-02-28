using DataAccess.Models.EIOModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers.BaseEIOControllers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class IPDSurgicalProcedureSafetyChecklistController : EIOSurgicalProcedureSafetyChecklistController
    {
        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/SurgicalProcedureSafetyChecklist/Create/{id}")]
        [Permission(Code = "ISPSC1")]
        public IHttpActionResult CreateSurgicalProcedureSafetyChecklistAPI(Guid id)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var spsc = GetSurgicalProcedureSafetyChecklist(ipd.Id, "IPD");
            if (spsc != null)
                return Content(HttpStatusCode.NotFound, Message.EIO_SPSC_EXIST);
            if (IPDIsBlock(ipd, Constant.IPDFormCode.BangKiemAnToanPhauThuatThuThuat))
            {
                return Content(HttpStatusCode.BadRequest, Message.FORM_IS_LOCKED);
            }
            spsc = new EIOSurgicalProcedureSafetyChecklist()
            {
                VisitId = ipd.Id,
                VisitTypeGroupCode = "IPD",
            };
            unitOfWork.EIOSurgicalProcedureSafetyChecklistRepository.Add(spsc);
            unitOfWork.Commit();

            return Content(HttpStatusCode.OK, new { spsc.Id });
        }

        [HttpGet]
        [Route("api/IPD/SurgicalProcedureSafetyChecklist/{id}")]
        [Permission(Code = "ISPSC2")]
        public IHttpActionResult GetSurgicalProcedureSafetyChecklistAPI(Guid id)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var spsc = GetSurgicalProcedureSafetyChecklist(ipd.Id, "IPD");
            if (spsc == null)
                return Content(HttpStatusCode.NotFound, new
                {
                    ViMessage = "Biên bản hội chẩn không tồn tại",
                    EnMessage = "Joint consultation group minutes is not found",
                    IsLocked = IPDIsBlock(ipd, Constant.IPDFormCode.BangKiemAnToanPhauThuatThuThuat)
                });

            return Content(HttpStatusCode.OK, new
            {
                spsc.Id,
                SignIn = spsc.EIOSurgicalProcedureSafetyChecklistSignInId,
                TimeOut = spsc.EIOSurgicalProcedureSafetyChecklistTimeOutId,
                SignOut = spsc.EIOSurgicalProcedureSafetyChecklistSignOutId,
                IsLocked = IPDIsBlock(ipd, Constant.IPDFormCode.BangKiemAnToanPhauThuatThuThuat),
                ipd.Version
            });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/SurgicalProcedureSafetyChecklist/SignIn/Create/{id}")]
        [Permission(Code = "ISPSC3")]
        public IHttpActionResult CreateSurgicalProcedureSafetyChecklistSignInAPI(Guid id)
        {
            var spsc = unitOfWork.EIOSurgicalProcedureSafetyChecklistRepository.GetById(id);
            if (spsc == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_SPSC_NOT_FOUND);
            if (spsc.EIOSurgicalProcedureSafetyChecklistSignInId != null)
                return Content(HttpStatusCode.NotFound, Message.EIO_SPSCSI_EXIST);
            var ipd = GetIPD((Guid)spsc.VisitId);
            if (IPDIsBlock(ipd, Constant.IPDFormCode.BangKiemAnToanPhauThuatThuThuat))
            {
                return Content(HttpStatusCode.BadRequest, Message.FORM_IS_LOCKED);
            }
            var spsc_si = new EIOSurgicalProcedureSafetyChecklistSignIn();
            unitOfWork.EIOSurgicalProcedureSafetyChecklistSignInRepository.Add(spsc_si);
            spsc.EIOSurgicalProcedureSafetyChecklistSignInId = spsc_si.Id;
            unitOfWork.EIOSurgicalProcedureSafetyChecklistRepository.Update(spsc);
            unitOfWork.Commit();

            return Content(HttpStatusCode.OK, new { spsc_si.Id });
        }

        [HttpGet]
        [Route("api/IPD/SurgicalProcedureSafetyChecklist/SignIn/{id}")]
        [Permission(Code = "ISPSC4")]
        public IHttpActionResult GetSurgicalProcedureSafetyChecklistSignInAPI(Guid id)
        {
            var spsc_si = unitOfWork.EIOSurgicalProcedureSafetyChecklistSignInRepository.GetById(id);
            if (spsc_si == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_SPSCSI_NOT_FOUND);
            var spsc = unitOfWork.EIOSurgicalProcedureSafetyChecklistRepository.Find(e => e.EIOSurgicalProcedureSafetyChecklistSignInId == id).FirstOrDefault();
            if (spsc == null)
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);

            var datas = GetSurgicalProcedureSafetyChecklistData(spsc_si.Id, "SignIn").Select(e => new { e.Code, e.Value, e.EnValue });
            
            var nurse = unitOfWork.UserRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                !string.IsNullOrEmpty(e.Username) &&
                e.Username == spsc_si.UpdatedBy
            );
            
            var ipd = GetIPD((Guid)spsc.VisitId);

            return Content(HttpStatusCode.OK, new
            {
                spsc_si.Id,
                CreatedAt = spsc_si.CreatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                Nurse = new { nurse?.Username, nurse?.Fullname, nurse?.DisplayName, nurse?.Title },
                Datas = datas,
                IsLocked = IPDIsBlock(ipd, Constant.IPDFormCode.BangKiemAnToanPhauThuatThuThuat)
            });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/SurgicalProcedureSafetyChecklist/SignIn/{id}")]
        [Permission(Code = "ISPSC5")]
        public IHttpActionResult UpdateSurgicalProcedureSafetyChecklistSignInAPI(Guid id, [FromBody] JObject request)
        {
            var spsc_si = unitOfWork.EIOSurgicalProcedureSafetyChecklistSignInRepository.GetById(id);
            if (spsc_si == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_SPSCSI_NOT_FOUND);

            var spsc = unitOfWork.EIOSurgicalProcedureSafetyChecklistRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.EIOSurgicalProcedureSafetyChecklistSignInId != null &&
                e.EIOSurgicalProcedureSafetyChecklistSignInId == spsc_si.Id
            );
            var ipd = GetIPD((Guid)spsc.VisitId);
            if (IPDIsBlock(ipd, Constant.IPDFormCode.BangKiemAnToanPhauThuatThuThuat))
            {
                return Content(HttpStatusCode.BadRequest, Message.FORM_IS_LOCKED);
            }
            var user = GetUser();
            if (spsc_si.CreatedBy != user.Username)
                return Content(HttpStatusCode.BadRequest, Message.OWNER_FORBIDDEN);

            HandleSurgicalProcedureSafetyChecklistData(spsc_si.Id, "SignIn", request["Datas"]);
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/SurgicalProcedureSafetyChecklist/TimeOut/Create/{id}")]
        [Permission(Code = "ISPSC6")]
        public IHttpActionResult CreateSurgicalProcedureSafetyChecklistTimeOutAPI(Guid id)
        {
            var spsc = unitOfWork.EIOSurgicalProcedureSafetyChecklistRepository.GetById(id);
            if (spsc == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_SPSC_NOT_FOUND);
            if (spsc.EIOSurgicalProcedureSafetyChecklistTimeOutId != null)
                return Content(HttpStatusCode.NotFound, Message.EIO_SPSCTO_EXIST);
            var ipd = GetIPD((Guid)spsc.VisitId);
            if (IPDIsBlock(ipd, Constant.IPDFormCode.BangKiemAnToanPhauThuatThuThuat))
            {
                return Content(HttpStatusCode.BadRequest, Message.FORM_IS_LOCKED);
            }

            if (HaveUncompletedSafetyChecklistData(spsc.EIOSurgicalProcedureSafetyChecklistSignInId, "SignIn"))
                return Content(HttpStatusCode.BadRequest, Message.EIO_SPSCTO_CANT_CREATE);

            var spsc_to = new EIOSurgicalProcedureSafetyChecklistTimeOut();
            unitOfWork.EIOSurgicalProcedureSafetyChecklistTimeOutRepository.Add(spsc_to);
            spsc.EIOSurgicalProcedureSafetyChecklistTimeOutId = spsc_to.Id;
            unitOfWork.EIOSurgicalProcedureSafetyChecklistRepository.Update(spsc);
            unitOfWork.Commit();

            return Content(HttpStatusCode.OK, new { spsc_to.Id });
        }

        [HttpGet]
        [Route("api/IPD/SurgicalProcedureSafetyChecklist/TimeOut/{id}")]
        [Permission(Code = "ISPSC7")]
        public IHttpActionResult GetSurgicalProcedureSafetyChecklistTimeOutAPI(Guid id)
        {
            var spsc_to = unitOfWork.EIOSurgicalProcedureSafetyChecklistTimeOutRepository.GetById(id);
            if (spsc_to == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_SPSCTO_NOT_FOUND);

            var datas = GetSurgicalProcedureSafetyChecklistData(spsc_to.Id, "TimeOut").Select(e => new { e.Code, e.Value, e.EnValue });
            var spsc = unitOfWork.EIOSurgicalProcedureSafetyChecklistRepository.Find(e => e.EIOSurgicalProcedureSafetyChecklistTimeOutId == spsc_to.Id).FirstOrDefault();
            var nurse = unitOfWork.UserRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                !string.IsNullOrEmpty(e.Username) &&
                e.Username == spsc_to.UpdatedBy
            );
            var ipd = GetIPD((Guid)spsc.VisitId);
            
            return Content(HttpStatusCode.OK, new
            {
                spsc_to.Id,
                CreatedAt = spsc_to.CreatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                Nurse = new { nurse?.Username, nurse?.Fullname, nurse?.DisplayName, nurse?.Title },
                Datas = datas,
                IsLocked = IPDIsBlock(ipd, Constant.IPDFormCode.BangKiemAnToanPhauThuatThuThuat)
            });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/SurgicalProcedureSafetyChecklist/TimeOut/{id}")]
        [Permission(Code = "ISPSC8")]
        public IHttpActionResult UpdateSurgicalProcedureSafetyChecklistTimeOutAPI(Guid id, [FromBody] JObject request)
        {
            var spsc_to = unitOfWork.EIOSurgicalProcedureSafetyChecklistTimeOutRepository.GetById(id);
            if (spsc_to == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_SPSCTO_NOT_FOUND);

            var spsc = unitOfWork.EIOSurgicalProcedureSafetyChecklistRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.EIOSurgicalProcedureSafetyChecklistTimeOutId != null &&
                e.EIOSurgicalProcedureSafetyChecklistTimeOutId == spsc_to.Id
            );

            var ipd = GetIPD((Guid)spsc.VisitId);
            if (IPDIsBlock(ipd, Constant.IPDFormCode.BangKiemAnToanPhauThuatThuThuat))
            {
                return Content(HttpStatusCode.BadRequest, Message.FORM_IS_LOCKED);
            }

            var user = GetUser();
            if (spsc_to.CreatedBy != user.Username)
                return Content(HttpStatusCode.BadRequest, Message.OWNER_FORBIDDEN);

            HandleSurgicalProcedureSafetyChecklistData(spsc_to.Id, "TimeOut", request["Datas"]);
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/SurgicalProcedureSafetyChecklist/SignOut/Create/{id}")]
        [Permission(Code = "ISPSC9")]
        public IHttpActionResult CreateSurgicalProcedureSafetyChecklistSignOutAPI(Guid id)
        {
            var spsc = unitOfWork.EIOSurgicalProcedureSafetyChecklistRepository.GetById(id);
            if (spsc == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_SPSC_NOT_FOUND);
            if (spsc.EIOSurgicalProcedureSafetyChecklistSignOutId != null)
                return Content(HttpStatusCode.NotFound, Message.EIO_SPSCSI_EXIST);

            var ipd = GetIPD((Guid)spsc.VisitId);
            if (IPDIsBlock(ipd, Constant.IPDFormCode.BangKiemAnToanPhauThuatThuThuat))
            {
                return Content(HttpStatusCode.BadRequest, Message.FORM_IS_LOCKED);
            }

            if (HaveUncompletedSafetyChecklistData(spsc.EIOSurgicalProcedureSafetyChecklistTimeOutId, "TimeOut"))
                return Content(HttpStatusCode.BadRequest, Message.EIO_SPSCSO_CANT_CREATE);

            var spsc_si = new EIOSurgicalProcedureSafetyChecklistSignOut();
            unitOfWork.EIOSurgicalProcedureSafetyChecklistSignOutRepository.Add(spsc_si);
            spsc.EIOSurgicalProcedureSafetyChecklistSignOutId = spsc_si.Id;
            unitOfWork.EIOSurgicalProcedureSafetyChecklistRepository.Update(spsc);
            unitOfWork.Commit();

            return Content(HttpStatusCode.OK, new { spsc_si.Id });
        }

        [HttpGet]
        [Route("api/IPD/SurgicalProcedureSafetyChecklist/SignOut/{id}")]
        [Permission(Code = "ISPSC10")]
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
            var spsc = unitOfWork.EIOSurgicalProcedureSafetyChecklistRepository.Find(e => e.EIOSurgicalProcedureSafetyChecklistSignOutId == spsc_so.Id).FirstOrDefault();
            var ipd = GetIPD((Guid)spsc.VisitId);
            
            return Content(HttpStatusCode.OK, new
            {
                spsc_so.Id,
                CreatedAt = spsc_so.CreatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                Nurse = new { nurse?.Username, nurse?.Fullname, nurse?.DisplayName, nurse?.Title },
                Datas = datas,
                IsLocked = IPDIsBlock(ipd, Constant.IPDFormCode.BangKiemAnToanPhauThuatThuThuat)
            });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/SurgicalProcedureSafetyChecklist/SignOut/{id}")]
        [Permission(Code = "ISPSC11")]
        public IHttpActionResult UpdateSurgicalProcedureSafetyChecklistSignOutAPI(Guid id, [FromBody] JObject request)
        {
            var spsc_so = unitOfWork.EIOSurgicalProcedureSafetyChecklistSignOutRepository.GetById(id);
            if (spsc_so == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_SPSCSO_NOT_FOUND);

            var spsc = unitOfWork.EIOSurgicalProcedureSafetyChecklistRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.EIOSurgicalProcedureSafetyChecklistSignOutId != null &&
                e.EIOSurgicalProcedureSafetyChecklistSignOutId == spsc_so.Id
            );
            var ipd = GetIPD((Guid)spsc.VisitId);
            if (IPDIsBlock(ipd, Constant.IPDFormCode.BangKiemAnToanPhauThuatThuThuat))
            {
                return Content(HttpStatusCode.BadRequest, Message.FORM_IS_LOCKED);
            }
            var user = GetUser();
            if (spsc_so.CreatedBy != user.Username)
                return Content(HttpStatusCode.BadRequest, Message.OWNER_FORBIDDEN);

            HandleSurgicalProcedureSafetyChecklistData(spsc_so.Id, "SignOut", request["Datas"]);
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

    }
}
