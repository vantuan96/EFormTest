using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Common;
using EForm.Models;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.GeneralControllers
{
    [SessionAuthorize]
    public class NotificationController : BaseApiController
    {
        [HttpPost]
        [CSRFCheck]
        [Route("api/Notification")]
        [Permission(Code = "GNOTI1")]
        public IHttpActionResult GetNotificationAPI()
        {
            var user = GetUser();
            var time = DateTime.Now.AddHours(-12);
            var notification = unitOfWork.NotificationRepository
                .Find(
                    e => !e.IsDeleted &&
                    !string.IsNullOrEmpty(e.ToUser) &&
                    e.ToUser.Equals(user.Username) &&
                    (!e.Seen || (e.Seen && e.TimeSeen >= time))
                )
                .OrderBy(e => e.CreatedAt)
                .Select(e => new NotificationModel {
                    Id = e.Id,
                    Seen = e.Seen,
                    Form = e.Form,
                    SpecialtyId = e.SpecialtyId,
                    VisitId = e.VisitId,
                    VisitTypeGroupCode = e.VisitTypeGroupCode,
                    EnMessage = e.EnMessage,
                    ViMessage = e.ViMessage,
                    CreatedAt = e.CreatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND)
                });
            return Content(HttpStatusCode.OK, notification);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/Notification/{id}")]
        [Permission(Code = "GNOTI2")]
        public IHttpActionResult SeenNotificationAPI(Guid id)
        {
            var sid = id.ToString();
            var user = GetUser();
            var notification = unitOfWork.NotificationRepository.FirstOrDefault(e => (e.Id == id || e.Form == sid) && e.ToUser == user.Username);
            if (notification == null)
                return Content(HttpStatusCode.NotFound, Message.NOTI_NOT_FOUND);
            notification.Seen = true;
            notification.TimeSeen = DateTime.Now;
            unitOfWork.NotificationRepository.Update(notification);
            unitOfWork.Commit();
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }
    }
}