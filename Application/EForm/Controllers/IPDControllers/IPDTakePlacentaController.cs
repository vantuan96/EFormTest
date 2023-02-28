using DataAccess.Models;
using DataAccess.Models.IPDModel;
using EForm.Authentication;
using EForm.Client;
using EForm.Common;
using EForm.Controllers.EIOControllers;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class IPDTakePlacentaController : EMRFormController
    {   
        [HttpGet]
        [Route("api/TakePlacenta/SynInforCustomer/{visitId}")]
        //[Permission(Code = "APIGDTIPDA01_159_050919_VE")]
        public IHttpActionResult SynInforCustomer(Guid visitId)
        {
            IPD visit = GetIPD(visitId);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            Customer custormer = visit.Customer;

            return Content(HttpStatusCode.OK, new
            {
                CustomerName = custormer?.Fullname,
                CustomerAddress = custormer?.Address,
                CustomerPassportNumber = custormer?.IdentificationCard,
                CustomerPhone = custormer?.Phone,
                CustomerRelationship = GetRelationShipResultFromOH(custormer?.PID, "HU"),
            });
        }
        private dynamic GetRelationShipResultFromOH(string pId, string relationshipCode)
        {
            var relationships = OHClient.GetRelationshipOfCustormerByPid(pId);
            if (relationships.Count == 0 || relationships == null)
                return null;

            var relationship = (from r in relationships
                                   where r.RelationshipCode == relationshipCode
                                   orderby r.LastUpdated descending
                                   select r).FirstOrDefault();
            return relationship;
        }
    }
}
