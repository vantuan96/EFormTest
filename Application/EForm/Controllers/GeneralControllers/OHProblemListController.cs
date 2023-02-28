using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Common;
using EForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using EForm.Client;
namespace EForm.Controllers.GeneralControllers
{
    [SessionAuthorize]
    public class OHProblemController : BaseApiController
    {
        [HttpGet]
        [Route("api/OHPROBLEMLIST/{type}/{id}")]
        [Permission(Code = "IPOVIEWPROBLEMLIST")]
        public IHttpActionResult getOHProblemList(Guid id, string type)
        {

            dynamic visit = null;
            string doctorname = "";
            if (type == "ED")
                visit = GetED(id);

            if (type == "OPD") { 
                visit = GetOPD(id);
                doctorname = visit.PrimaryDoctor?.Username;
            }

            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);
            var customer = visit.Customer;
            if (string.IsNullOrEmpty(customer.PID))
            {
                return Content(HttpStatusCode.OK, new List<dynamic>());
            }

            return Content(HttpStatusCode.OK, OHClient.getProblemListOH(customer.PID, doctorname));
        }
    }
}
