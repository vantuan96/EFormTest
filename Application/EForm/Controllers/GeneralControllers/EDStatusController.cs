using System.Data;
using System.Linq;
using System.Net;
using System.Web.Http;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Models;
using EForm.Utils;

namespace EForm.Controllers.EDControllers.GeneralControllers
{
    [SessionAuthorize]
    public class EDStatusController : BaseApiController
    {
        [HttpGet]
        [Route("api/Status")]
        [Permission(Code = "GSTAT1")]
        public IHttpActionResult GetEDStatus([FromUri]string visit_type_group = "")
        {
            if (string.IsNullOrEmpty(visit_type_group))
            {
                var visit_status = new VisitStatus(unitOfWork, "OPD");
                var no_examination = visit_status.GetNoExaminationStatus();
                var results_1 = unitOfWork.EDStatusRepository.Find(
                        e => !e.IsDeleted && 
                        e.VisitTypeGroupId != null &&
                        e.Id != no_examination.Id
                    )
                    .Select(e => new StatusViewModel {
                        Id = e.Id,
                        EnName = e.EnName,
                        ViName = e.ViName,
                        Code = e.VisitTypeGroup.Code,
                        CreatedAt = e.CreatedAt,
                        StatusCode = e.Code
                    })
                    .OrderBy(e => e.Code)
                    .OrderBy(e => e.CreatedAt)
                    .ToList();
                return Content(HttpStatusCode.OK, results_1);
            }
            var resutls = unitOfWork.EDStatusRepository.Find(
                    e => !e.IsDeleted && 
                    e.VisitTypeGroupId != null &&
                    e.VisitTypeGroup.Code == visit_type_group
                ).OrderBy(e => e.CreatedAt)
                .Select(e => new { e.Id, e.EnName, e.ViName, e.VisitTypeGroup.Code, StatusCode = e.Code})
                .ToList();
            return Content(HttpStatusCode.OK, resutls);
        }
    }
}