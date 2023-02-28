using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EForm.Controllers.GeneralControllers
{
    [SessionAuthorize]
    public class TestCovidController : BaseApiController
    {
        [HttpGet]
        [CSRFCheck]
        [Route("api/{type}/TestCovidConfirmation/{id}")]
        //[Permission(Code = "GETTCOVID2")]
        public IHttpActionResult getTestCovidResult(Guid id, string type = "ED")
        {
            dynamic visit = null;
            if (type == "ED")
                visit = GetED(id);

            if (type == "OPD")
                visit = GetOPD(id);

            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);
            var customer = visit.Customer;
            var afrsp = visit.EIOTestCovid2Confirmation;
            if (afrsp == null || afrsp.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.EIO_TESTCOVID_NOT_FOUND);

            return Content(HttpStatusCode.OK, new {
                Time = afrsp.Time?.ToString(Constant.DATE_FORMAT),
                afrsp.MethodTest,
                afrsp.Result,
                afrsp.Id,
                customer.Fullname,
                DateOfBirth = customer.DateOfBirth?.ToString(Constant.DATE_FORMAT),
                customer.PID,
                customer.Nationality,
                customer.NationalityEn,
                customer.AddressEn,
                customer.Address,
                customer.Gender,
                customer.IdentificationCard,
                PIDOH = customer.PIDOH
            });
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/{type}/TestCovidConfirmation/{id}")]
        //[Permission(Code = "PUTTCOVID2")]
        public IHttpActionResult UpdateTestCovidResult(Guid id, string type, [FromBody]JObject request)
        {
            dynamic visit = null;
            if (type == "ED")
                visit = GetED(id);

            if (type == "OPD")
                visit = GetOPD(id);

            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);
            var data = visit.EIOTestCovid2Confirmation;
            if (request["Time"]?.ToString() != "" && request["Time"]?.ToString() != null) {
                data.Time = DateTime.ParseExact(request["Time"]?.ToString(), Constant.DATE_FORMAT, null);
            }
            
            data.MethodTest = "1";
            data.Result = request["Result"]?.ToString();

            unitOfWork.EIOTestCovid2ConfirmationRepository.Update(data);
            unitOfWork.Commit();
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }
    }
}
