using DataAccess.Models;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Client;
using EForm.Common;
using System;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.CustomerInfoControllers
{
    [SessionAuthorize]
    public class HeightAndWeightController : BaseApiController
    {
        [HttpPost]
        [CSRFCheck]
        [Route("api/Customer/HeightAndWeight/{id}")]
        [Permission(Code = "CHEWE1")]
        public IHttpActionResult GetHeightAndWeightAPI(Guid id)
        {

            var customer = GetCustometByVisitId(id);
            if(customer == null)
                return Content(HttpStatusCode.BadRequest, Message.SYNCHRONIZED_ERROR);
            var site_code = GetSiteCode();
            if (string.IsNullOrEmpty(site_code))
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);
            // dynamic HeightWeight;

            //if (site_code == "times_city")
            //{
            //    HeightWeight = EHosClient.GetGetHeightAndWeight(customer.PID);
            //}
            //else
            //{
            //    HeightWeight = OHClient.getGetHeightAndWeight(customer.PID);
            //}

            return Content(HttpStatusCode.OK, new {
                customer.Height,
                LastUpdateHeight = customer.LastUpdateHeight?.ToString(Constant.DATE_FORMAT),
                customer.Weight,
                LastUpdateWeight = customer.LastUpdateWeight?.ToString(Constant.DATE_FORMAT)
                // HeightWeightFromHis = HeightWeight
            });
        }

        private Customer GetCustometByVisitId(Guid id)
        {
            var ed = unitOfWork.EDRepository.GetById(id);
            if (ed != null) return ed.Customer;

            var opd = unitOfWork.OPDRepository.GetById(id);
            if (opd != null) return opd.Customer;

            var ipd = unitOfWork.IPDRepository.GetById(id);
            if (ipd != null) return ipd.Customer;

            var eoc = unitOfWork.EOCRepository.GetById(id);
            if (eoc != null) return eoc.Customer;
            return null;
        }
    }
}
