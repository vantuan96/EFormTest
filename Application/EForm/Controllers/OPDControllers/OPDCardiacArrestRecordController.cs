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
    public class OPDCardiacArrestRecordController : EIOCardiacArrestRecordController
    {
        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/CardiacArrestRecord/Create/{id}")]
        [Permission(Code = "OCAAR1")]
        public IHttpActionResult CreateOPDCardiacArrestRecordAPI(Guid id)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var car = GetCardiacArrestRecord(opd.Id, "OPD");
            if (car != null)
                return Content(HttpStatusCode.NotFound, Message.EIO_CAARRE_EXIST);

            var user = GetUser();
            if (IsBlockAfter24h(opd.CreatedAt) && !HasUnlockPermission(opd.Id, "OPDCAARRE", user.Username))
                return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);

            car = CreateCardiacArrestRecord(opd.Id, "OPD", 2);

            return Content(HttpStatusCode.OK, new { car.Id });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/CardiacArrestRecord/Confirm/{id}")]
        [Permission(Code = "OCAAR2")]
        public IHttpActionResult ConfirmOPDCardiacArrestRecordAPI(Guid id, [FromBody]JObject request)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var car = GetCardiacArrestRecord(opd.Id, "OPD");
            if (car == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_CAARRE_NOT_FOUND);            
            var err = ConfirmCardiacArrestRecord(car, request);
            if (err != null)
                return Content(HttpStatusCode.BadRequest, err);            
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpGet]
        [Route("api/OPD/CardiacArrestRecord/Info/{id}")]
        [Permission(Code = "OCAAR3")]
        public IHttpActionResult GetOPDCardiacArrestRecordInfoAPI(Guid id)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var car = GetCardiacArrestRecord(opd.Id, "OPD");
            var user = GetUser();
            bool IsLocked = Is24hLocked(opd.CreatedAt, opd.Id, "OPDCAARRE", user.Username, car?.Id);
            if (car == null)
                return Content(HttpStatusCode.NotFound,
                    new
                    {
                        ViMessage = "Bảng hồi sinh tim phổi không tồn tại",
                        EnMessage = "Cardiac arrest record is not found",
                        IsLocked 
                    });

            
            var info = GetCardiacArrestRecordInfo(car, opd.OPDOutpatientExaminationNoteId, IsLocked);
            return Content(HttpStatusCode.OK, info);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/CardiacArrestRecord/Info/{id}")]
        [Permission(Code = "OCAAR4")]
        public IHttpActionResult UpdateOPDCardiacArrestRecordInfoAPI(Guid id, [FromBody]JObject request)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);
            

            var car = GetCardiacArrestRecord(opd.Id, "OPD");
            if (car == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_CAARRE_NOT_FOUND);
            var user = GetUser();
            if (IsBlockAfter24h(opd.CreatedAt, car.Id) && !HasUnlockPermission(opd.Id, "OPDCAARRE", user.Username, car.Id))
                return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);
            if (car.DoctorId != null)
             return Content(HttpStatusCode.NotFound, Message.OWNER_FORBIDDEN);

            HandleUpdateOrCreateCardiacArrestRecordInfoData(car, request);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/CardiacArrestRecord/Table/Create/{id}")]
        [Permission(Code = "OCAAR5")]
        public IHttpActionResult CreateOPDCardiacArrestRecordTableAPI(Guid id)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var car = GetCardiacArrestRecord(opd.Id, "OPD");
            var user = GetUser();
            if (IsBlockAfter24h(opd.CreatedAt, car?.Id) && !HasUnlockPermission(opd.Id, "OPDCAARRE", user.Username, car?.Id))
                return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);
        
            if (car == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_CAARRE_NOT_FOUND);
            var cart = unitOfWork.EIOCardiacArrestRecordTableRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.EIOCardiacArrestRecordId != null &&
                e.EIOCardiacArrestRecordId == car.Id
            );
            if (cart != null)
                return Content(HttpStatusCode.NotFound, Message.EIO_CAARRE_EXIST);

            return Content(HttpStatusCode.OK, CreateFirstEIOCardiacArrestRecordTable(car.Id));
        }

        [HttpGet]
        [Route("api/OPD/CardiacArrestRecord/Table/{id}")]
        [Permission(Code = "OCAAR6")]
        public IHttpActionResult GetOPDCardiacArrestRecordTableAPI(Guid id)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var car = GetCardiacArrestRecord(opd.Id, "OPD");
            if (car == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_CAARRE_NOT_FOUND);            
            return Content(HttpStatusCode.OK, GetEIOCardiacArrestRecordTable(car));
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/CardiacArrestRecord/Table/{id}")]
        [Permission(Code = "OCAAR7")]
        public IHttpActionResult UpdateOPDCardiacArrestRecordTableAPI(Guid id, [FromBody]JObject request)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);           

            var car = GetCardiacArrestRecord(opd.Id, "OPD");
            if (car == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_CAARRE_NOT_FOUND);
            var user = GetUser();
            if (IsBlockAfter24h(opd.CreatedAt, car.Id) && !HasUnlockPermission(opd.Id, "OPDCAARRE", user.Username, car.Id))
                return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);
            if (car.DoctorId != null)
                return Content(HttpStatusCode.NotFound, Message.OWNER_FORBIDDEN);

            HandleUpdateOrCreateCardiacArrestRecordTableData(car, request);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }
    }
}
