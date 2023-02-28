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
    public class EDCardiacArrestRecordController : EIOCardiacArrestRecordController
    {
        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/CardiacArrestRecord/Create/{id}")]
        [Permission(Code = "ECAAR1")]
        public IHttpActionResult CreateEDCardiacArrestRecordAPI(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var car = GetCardiacArrestRecord(ed.Id, "ED");
            if (car != null)
                return Content(HttpStatusCode.NotFound, Message.EIO_CAARRE_EXIST);

            car = CreateCardiacArrestRecord(ed.Id, "ED", 2);

            return Content(HttpStatusCode.OK, new { car.Id });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/CardiacArrestRecord/Confirm/{id}")]
        [Permission(Code = "ECAAR2")]
        public IHttpActionResult ConfirmEDCardiacArrestRecordAPI(Guid id, [FromBody]JObject request)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var car = GetCardiacArrestRecord(ed.Id, "ED");
            if (car == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_CAARRE_NOT_FOUND);

            var err = ConfirmCardiacArrestRecord(car, request);
            if(err != null)
                return Content(HttpStatusCode.BadRequest, err);
            
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpGet]
        [Route("api/ED/CardiacArrestRecord/Info/{id}")]
        [Permission(Code = "ECAAR3")]
        public IHttpActionResult GetEDCardiacArrestRecordInfoAPI(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var car = GetCardiacArrestRecord(ed.Id, "ED");
            if (car == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_CAARRE_NOT_FOUND);

            var info = GetCardiacArrestRecordInfo(car, ed.DischargeInformationId);

            return Content(HttpStatusCode.OK, info);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/CardiacArrestRecord/Info/{id}")]
        [Permission(Code = "ECAAR4")]
        public IHttpActionResult UpdateEDCardiacArrestRecordInfoAPI(Guid id, [FromBody]JObject request)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var car = GetCardiacArrestRecord(ed.Id, "ED");
            if (car == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_CAARRE_NOT_FOUND);
            if (car.DoctorId != null)
                return Content(HttpStatusCode.NotFound, Message.OWNER_FORBIDDEN);

            HandleUpdateOrCreateCardiacArrestRecordInfoData(car, request);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/CardiacArrestRecord/Table/Create/{id}")]
        [Permission(Code = "ECAAR5")]
        public IHttpActionResult CreateEDCardiacArrestRecordTableAPI(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var car = GetCardiacArrestRecord(ed.Id, "ED");
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
        [Route("api/ED/CardiacArrestRecord/Table/{id}")]
        [Permission(Code = "ECAAR6")]
        public IHttpActionResult GetEDCardiacArrestRecordTableAPI(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var car = GetCardiacArrestRecord(ed.Id, "ED");
            if (car == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_CAARRE_NOT_FOUND);

            return Content(HttpStatusCode.OK, GetEIOCardiacArrestRecordTable(car));
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/CardiacArrestRecord/Table/{id}")]
        [Permission(Code = "ECAAR7")]
        public IHttpActionResult UpdateEDCardiacArrestRecordTableAPI(Guid id, [FromBody]JObject request)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var car = GetCardiacArrestRecord(ed.Id, "ED");
            if (car == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_CAARRE_NOT_FOUND);
            if (car.DoctorId != null)
                return Content(HttpStatusCode.NotFound, Message.OWNER_FORBIDDEN);

            HandleUpdateOrCreateCardiacArrestRecordTableData(car, request);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }
    }
}
