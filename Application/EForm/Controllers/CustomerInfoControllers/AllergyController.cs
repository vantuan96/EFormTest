using DataAccess.Models;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Common;
using System;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.CustomerInfoControllers
{
    [SessionAuthorize]
    public class AllergyController : BaseApiController
    {
        [HttpGet]
        [CSRFCheck]
        [Route("api/Customer/Allergy/{id}")]
        [Permission(Code = "CALLE1")]
        public IHttpActionResult GetAllergyAPI(Guid id)
        {
            var customer = GetCustomerByVisitId(id);
            if (customer == null)
                return Content(HttpStatusCode.BadRequest, Message.SYNCHRONIZED_ERROR);

            return Content(HttpStatusCode.OK, GetNewestAllergy(customer));
        }

        private Customer GetCustomerByVisitId(Guid id)
        {
            var ed = unitOfWork.EDRepository.GetById(id);
            if (ed != null) return ed.Customer;

            var opd = unitOfWork.OPDRepository.GetById(id);
            if (opd != null) return opd.Customer;
            var eoc = unitOfWork.EOCRepository.GetById(id);
            if (eoc != null) return eoc.Customer;
            var ipd = unitOfWork.IPDRepository.GetById(id);
            if (ipd != null) return ipd.Customer;
            return null;
        }
        private dynamic GetNewestAllergy(Customer customer)
        {
            if (!customer.IsAllergy)
            {
                if(!string.IsNullOrEmpty(customer.Allergy) && customer.Allergy == "Không xác định")
                    return new { Yes = "false", No = "false", Na = "true", Kind = "", Ans = "" };

                return new { Yes = "false", No = "true", Na = "false", Kind = "", Ans = "" };
            }

            return new
            {
                Yes = "true",
                No = "false",
                Na = "false",
                Kind = customer.KindOfAllergy,
                Ans = customer.Allergy
            };
        }
    }
}
