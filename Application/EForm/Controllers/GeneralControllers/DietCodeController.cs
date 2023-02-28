using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace EForm.Controllers.GeneralControllers
{
    [SessionAuthorize]
    public class DietCodeController: BaseApiController
    {
        [HttpPost]
        [CSRFCheck]
        [Route("api/DietCode")]
        [Permission(Code = "DIETCODE")]
        public IHttpActionResult GetDietCode([FromUri] DietCodeParameterModel request)
        {
            var search = request.ConvertedSearch;
            var icd_10 = unitOfWork.DietCodeRepository.Find(i => !i.IsDeleted &&
            ((!string.IsNullOrEmpty(i.ViName) && i.ViName.Contains(search))
            || (!string.IsNullOrEmpty(i.EnName) && i.EnName.Contains(search))
            || (!string.IsNullOrEmpty(i.Code) && i.Code.Contains(search))))
            .OrderBy(i => i.ViName.Length)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(i => new { i.ViName, i.EnName, i.Code });
            return Content(HttpStatusCode.OK, icd_10);
        }
    }
}
