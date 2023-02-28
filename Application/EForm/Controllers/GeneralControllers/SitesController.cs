using System.Linq;
using System.Net;
using System.Web.Http;
using EForm.Authentication;
using EForm.BaseControllers;

namespace EForm.Controllers.GeneralControllers
{
    [SessionAuthorize]
    public class SitesController : BaseApiController
    {
        [HttpGet]
        [Route("api/Sites")]
        [Permission(Code = "GSITE1")]
        public IHttpActionResult GetSites()
        {
            var sites = unitOfWork.SiteRepository.Find(s => !s.IsDeleted)
                .Select(s => new { s.Name, s.Code, s.ViName, s.EnName, s.Address, s.PhoneNumber, s.Emergency, s.Hotline, s.ApiCode});
            return Content(HttpStatusCode.OK, sites);
        }
    }
}