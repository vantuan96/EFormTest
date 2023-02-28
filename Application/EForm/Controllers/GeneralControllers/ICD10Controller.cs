using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Models;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.GeneralControllers
{
    [SessionAuthorize]
    public class ICD10Controller : BaseApiController
    {
        [HttpPost]
        [CSRFCheck]
        [Route("api/ICD10")]
        [Permission(Code = "GICDT1")]
        public IHttpActionResult GetICD10([FromUri]ICD10ParameterModel request)
        {
            using (var txn = GetNewReadUncommittedScope())
            {
                var search = request.ConvertedSearch;
                var icd_10 = unitOfWork.ICD10Repository.Find(i => !i.IsDeleted &&
                ((!string.IsNullOrEmpty(i.ViName) && i.ViName.Contains(search))
                || (!string.IsNullOrEmpty(i.EnName) && i.EnName.Contains(search))
                || (!string.IsNullOrEmpty(i.ViNameWithoutSign) && i.ViNameWithoutSign.Contains(search))
                || (!string.IsNullOrEmpty(i.Code) && i.Code.Contains(search))))
                .OrderBy(i => i.ViName.Length)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(i => new { i.ViName, i.EnName, i.Code });
                return Content(HttpStatusCode.OK, icd_10);
            }
        }
    }
}
