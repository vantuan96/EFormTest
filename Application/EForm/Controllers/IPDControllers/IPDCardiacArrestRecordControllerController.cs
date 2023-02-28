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
    public class IPDCardiacArrestRecordController : EIOCardiacArrestRecordController
    {
        [HttpGet]
        [Route("api/IPD/CardiacArrestRecord/CheckFormLocked/{id}")]
        [Permission(Code = "ICAAR3")]
        public IHttpActionResult CheckFormLockedAPI(Guid id)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var car = GetCardiacArrestRecord(ipd.Id, "IPD");
            return Content(HttpStatusCode.OK, new
            {
                IsLocked = IPDIsBlock(ipd, Constant.IPDFormCode.BangHoiSinhTimPhoi, car?.Id)
            });
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/CardiacArrestRecord/Create/{id}")]
        [Permission(Code = "ICAAR1")]
        public IHttpActionResult CreateIPDCardiacArrestRecordAPI(Guid id)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var car = GetCardiacArrestRecord(ipd.Id, "IPD");
            if (car != null)
                return Content(HttpStatusCode.NotFound, Message.EIO_CAARRE_EXIST);

            car = CreateCardiacArrestRecord(ipd.Id, "IPD", 2);

            return Content(HttpStatusCode.OK, new { car.Id });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/CardiacArrestRecord/Confirm/{id}")]
        [Permission(Code = "ICAAR2")]
        public IHttpActionResult ConfirmIPDCardiacArrestRecordAPI(Guid id, [FromBody]JObject request)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);           
            var car = GetCardiacArrestRecord(ipd.Id, "IPD");
            if (car == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_CAARRE_NOT_FOUND);
            if (IPDIsBlock(ipd, Constant.IPDFormCode.BangHoiSinhTimPhoi, car.Id))
                return Content(HttpStatusCode.Forbidden, Message.FORM_IS_LOCKED);
            var err = ConfirmCardiacArrestRecord(car, request);
            if (err != null)
                return Content(HttpStatusCode.BadRequest, err);            
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpGet]
        [Route("api/IPD/CardiacArrestRecord/Info/{id}")]
        [Permission(Code = "ICAAR3")]
        public IHttpActionResult GetIPDCardiacArrestRecordInfoAPI(Guid id)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            var car = GetCardiacArrestRecord(ipd.Id, "IPD");
            if (car == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_CAARRE_NOT_FOUND);
            var is24hlock = IPDIsBlock(ipd, Constant.IPDFormCode.BangHoiSinhTimPhoi, car.Id);            
            var info = GetCardiacArrestRecordInfo(car, ipd.IPDMedicalRecordId, is24hlock);

            return Content(HttpStatusCode.OK, info);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/CardiacArrestRecord/Info/{id}")]
        [Permission(Code = "ICAAR4")]
        public IHttpActionResult UpdateIPDCardiacArrestRecordInfoAPI(Guid id, [FromBody]JObject request)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            //if (IPDIsBlock(ipd, Constant.IPDFormCode.BangHoiSinhTimPhoi))
            //    return Content(HttpStatusCode.Forbidden, Message.FORM_IS_LOCKED);
            var car = GetCardiacArrestRecord(ipd.Id, "IPD");
            if (car == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_CAARRE_NOT_FOUND);           
            if (IPDIsBlock(ipd, Constant.IPDFormCode.BangHoiSinhTimPhoi, car.Id))
                    return Content(HttpStatusCode.Forbidden, Message.FORM_IS_LOCKED);
                if (car.DoctorId != null)
                return Content(HttpStatusCode.NotFound, Message.OWNER_FORBIDDEN);
            HandleUpdateOrCreateCardiacArrestRecordInfoData(car, request);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/CardiacArrestRecord/Table/Create/{id}")]
        [Permission(Code = "ICAAR5")]
        public IHttpActionResult CreateIPDCardiacArrestRecordTableAPI(Guid id)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var car = GetCardiacArrestRecord(ipd.Id, "IPD");
            if (IPDIsBlock(ipd, Constant.IPDFormCode.BangHoiSinhTimPhoi, car?.Id))
                return Content(HttpStatusCode.Forbidden, Message.FORM_IS_LOCKED);

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
        [Route("api/IPD/CardiacArrestRecord/Table/{id}")]
        [Permission(Code = "ICAAR6")]
        public IHttpActionResult GetIPDCardiacArrestRecordTableAPI(Guid id)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);  
            var car = GetCardiacArrestRecord(ipd.Id, "IPD");
            if (car == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_CAARRE_NOT_FOUND);            
            if (IPDIsBlock(ipd, Constant.IPDFormCode.BangHoiSinhTimPhoi, car.Id))
                return Content(HttpStatusCode.Forbidden, Message.FORM_IS_LOCKED);
            return Content(HttpStatusCode.OK, GetEIOCardiacArrestRecordTable(car));
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/CardiacArrestRecord/Table/{id}")]
        [Permission(Code = "ICAAR7")]
        public IHttpActionResult UpdateIPDCardiacArrestRecordTableAPI(Guid id, [FromBody]JObject request)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            //if (IPDIsBlock(ipd, Constant.IPDFormCode.BangHoiSinhTimPhoi))
            //    return Content(HttpStatusCode.Forbidden, Message.FORM_IS_LOCKED);
            var car = GetCardiacArrestRecord(ipd.Id, "IPD");
            if (car == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_CAARRE_NOT_FOUND);         
            if (car.DoctorId != null)
                return Content(HttpStatusCode.NotFound, Message.OWNER_FORBIDDEN);
            if (IPDIsBlock(ipd, Constant.IPDFormCode.BangHoiSinhTimPhoi, car.Id))
                return Content(HttpStatusCode.Forbidden, Message.FORM_IS_LOCKED);
            HandleUpdateOrCreateCardiacArrestRecordTableData(car, request);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }
    }
}
