using DataAccess.Models.EOCModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers.BaseEIOControllers;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.EOCControllers
{
    [SessionAuthorize]
    public class EOCCardiacArrestRecordController : EIOCardiacArrestRecordController
    {
        [HttpPost]
        [CSRFCheck]
        [Route("api/EOC/CardiacArrestRecord/Create/{id}")]
        [Permission(Code = "EOC002")]
        public IHttpActionResult CreateEDCardiacArrestRecordAPI(Guid id)
        {
            var visit = GetEOC(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);

            var car = GetCardiacArrestRecord(visit.Id, "EOC");
            if (car != null)
                return Content(HttpStatusCode.NotFound, Message.EIO_CAARRE_EXIST);

            car = CreateCardiacArrestRecord(visit.Id, "EOC", 2);

            return Content(HttpStatusCode.OK, new { car.Id });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/EOC/CardiacArrestRecord/Confirm/{id}")]
        [Permission(Code = "EOC004")]
        public IHttpActionResult ConfirmEDCardiacArrestRecordAPI(Guid id, [FromBody]JObject request)
        {
            var visit = GetEOC(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);

            var car = GetCardiacArrestRecord(visit.Id, "EOC");
            if (car == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_CAARRE_NOT_FOUND);

            var err = ConfirmCardiacArrestRecord(car, request);
            if(err != null)
                return Content(HttpStatusCode.BadRequest, err);
            
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpGet]
        [Route("api/EOC/CardiacArrestRecord/Info/{id}")]
        [Permission(Code = "EOC001")]
        public IHttpActionResult GetEDCardiacArrestRecordInfoAPI(Guid id)
        {
            var visit = GetEOC(id);
            if (visit == null)
                return Content(HttpStatusCode.BadGateway, Message.ED_NOT_FOUND);

            var car = GetCardiacArrestRecord(visit.Id, "EOC");
            if (car == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_CAARRE_NOT_FOUND);

            var info = GetCardiacArrestRecordInfo(car, null);

            return Content(HttpStatusCode.OK, info);
        }
        protected EOCOutpatientExaminationNote GetOutpatientExaminationNote(Guid VisitId)
        {
            var form = unitOfWork.EOCOutpatientExaminationNoteRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == VisitId);
            return form;
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/EOC/CardiacArrestRecord/Info/{id}")]
        [Permission(Code = "EOC003")]
        public IHttpActionResult UpdateEDCardiacArrestRecordInfoAPI(Guid id, [FromBody]JObject request)
        {
            var visit = GetEOC(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var car = GetCardiacArrestRecord(visit.Id, "EOC");
            if (car == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_CAARRE_NOT_FOUND);
            if (car.DoctorId != null)
                return Content(HttpStatusCode.NotFound, Message.OWNER_FORBIDDEN);

            HandleUpdateOrCreateCardiacArrestRecordInfoData(car, request);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/EOC/CardiacArrestRecord/Table/Create/{id}")]
        [Permission(Code = "EOC002")]
        public IHttpActionResult CreateEDCardiacArrestRecordTableAPI(Guid id)
        {
            var visit = GetEOC(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var car = GetCardiacArrestRecord(visit.Id, "EOC");
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
        [Route("api/EOC/CardiacArrestRecord/Table/{id}")]
        [Permission(Code = "EOC001")]
        public IHttpActionResult GetEDCardiacArrestRecordTableAPI(Guid id)
        {
            var visit = GetEOC(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var car = GetCardiacArrestRecord(visit.Id, "EOC");
            if (car == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_CAARRE_NOT_FOUND);

            return Content(HttpStatusCode.OK, GetEIOCardiacArrestRecordTable(car));
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/EOC/CardiacArrestRecord/Table/{id}")]
        [Permission(Code = "EOC003")]
        public IHttpActionResult UpdateEDCardiacArrestRecordTableAPI(Guid id, [FromBody]JObject request)
        {
            var visit = GetEOC(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var car = GetCardiacArrestRecord(visit.Id, "EOC");
            if (car == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_CAARRE_NOT_FOUND);
            if (car.DoctorId != null)
                return Content(HttpStatusCode.NotFound, Message.OWNER_FORBIDDEN);

            HandleUpdateOrCreateCardiacArrestRecordTableData(car, request);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }
    }
}
