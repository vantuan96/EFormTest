using DataAccess.Models;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.GeneralControllers
{
    [SessionAuthorize]
    public class SpecialtyController : BaseApiController
    {
        [HttpGet]
        [Route("api/Specialties")]
        [Permission(Code = "GSPEC1")]
        public IHttpActionResult GetSpecialties([FromUri]SpecialtyParameterModel request)
        {
            var query = from spec_sql in unitOfWork.SpecialtyRepository.AsQueryable().Where(e => !e.IsDeleted)
                        join site_sql in unitOfWork.SiteRepository.AsQueryable().Where(e => !e.IsDeleted) 
                        on spec_sql.SiteId equals site_sql.Id
                        join visit_type_sql in unitOfWork.VisitTypeGroupRepository.AsQueryable().Where(e => !e.IsDeleted) 
                        on spec_sql.VisitTypeGroupId equals visit_type_sql.Id
                        select new
                        {
                            SiteCode = site_sql.Code,
                            SiteName = site_sql.Name,
                            VisitTypeGroup = visit_type_sql.Code,
                            spec_sql.Id,
                            spec_sql.ViName,
                            spec_sql.EnName,
                        };

            if (request != null && !string.IsNullOrEmpty(request.SiteCode))
                query = query.Where(e => request.SiteCode.Contains(e.SiteCode));

            if (request != null && !string.IsNullOrEmpty(request.VisitTypeGroupCode))
                query = query.Where(e => request.VisitTypeGroupCode.Contains(e.VisitTypeGroup));

            var results = query.OrderBy(e => e.SiteCode).ThenBy(e => e.VisitTypeGroup).Select(s => new { 
                s.Id, s.ViName, s.EnName, s.VisitTypeGroup, s.SiteName, s.SiteCode
            });
            return Content(HttpStatusCode.OK, results);
        }
    }
}
