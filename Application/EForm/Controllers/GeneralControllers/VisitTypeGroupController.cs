using EForm.Authentication;
using EForm.BaseControllers;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.GeneralControllers
{
    [SessionAuthorize]
    public class VisitTypeGroupController : BaseApiController
    {
        [HttpGet]
        [Route("api/VisitTypeGroup")]
        [Permission(Code = "GVITG1")]
        public IHttpActionResult GetVisitTypeGroupAPI()
        {
            var results = unitOfWork.VisitTypeGroupRepository.Find(e=> !e.IsDeleted).Select(s => new { s.Id, s.ViName, s.EnName, s.Code });
            return Content(HttpStatusCode.OK, results);
        }
    }
}
