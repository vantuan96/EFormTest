using EForm.Authentication;
using EForm.BaseControllers;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.GeneralControllers
{
    [SessionAuthorize]
    public class ClinicController : BaseApiController
    {
        [HttpGet]
        [Route("api/Clinics")]
        [Permission(Code = "GCLIN1")]
        public IHttpActionResult GetSpecialties([FromUri]Guid? specicaltyId = null)
        {
            var query = unitOfWork.ClinicRepository.Find(e => !e.IsDeleted && e.SpecialtyId != null);
            if (specicaltyId != null)
                query = query.Where(e => e.SpecialtyId == specicaltyId);

            var results = query.OrderBy(e => e.SpecialtyId).ThenBy(e => e.Code)
                .Select(s => new { 
                    s.Id, s.ViName, s.EnName, s.Code, 
                    SpecialtyViName = s.Specialty.ViName, SpecialtyEnName = s.Specialty.EnName 
                });
            return Content(HttpStatusCode.OK, results);
        }
    }
}
