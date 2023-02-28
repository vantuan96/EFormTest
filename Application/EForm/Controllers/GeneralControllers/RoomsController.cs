using System.Data;
using System.Linq;
using System.Net;
using System.Web.Http;
using EForm.Authentication;
using EForm.BaseControllers;

namespace EForm.Controllers.GeneralControllers
{
    [SessionAuthorize]
    public class RoomsController : BaseApiController
    {
        [HttpGet]
        [Route("api/Rooms")]
        [Permission(Code = "GROOM1")]
        public IHttpActionResult GetRooms()
        {
            var site_code = GetSiteId();
            var rooms = unitOfWork.RoomRepository.Find(r => !r.IsDeleted && r.SiteId == site_code)
                .OrderBy(r => r.Floor)
                .Select(r => new { r.Id, r.ViName, r.EnName, r.Floor })
                .ToList();
            return Content(HttpStatusCode.OK, rooms);
        }
    }
}