using EForm.Authentication;
using EForm.BaseControllers;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.GeneralControllers
{
    [SessionAuthorize]
    public class StandingOrderMasterDataController : BaseApiController
    {
        [HttpGet]
        [Route("api/StandingOrderMasterDatas")]
        [Permission(Code = "GTOMD1")]
        public IHttpActionResult GetStandingOrderMasterDataAPI([FromUri]string request = "")
        {
            var results = unitOfWork.StandingOrderMasterDataRepository.Find(
                i => !i.IsDeleted && 
                !string.IsNullOrEmpty(i.Name) && 
                i.Name.Contains(request)
            )
            .OrderBy(i => i.Name)
            .Select(i => new { i.Id, i.Name, i.Code, i.Drug, i.Dosage, i.Route })
            .ToList();
            return Content(HttpStatusCode.OK, results);
        }
    }
}
