using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Models;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.GeneralControllers
{
    [SessionAuthorize]
    public class MedicationMasterdataController : BaseApiController
    {
        [HttpGet]
        [Route("api/MedicationMasterdata")]
        [Permission(Code = "GMEMD1")]
        public IHttpActionResult GetMedicationMasterdataAPI([FromUri]MedicationMasterdataModel request)
        {
            var results = unitOfWork.MedicationMasterdataRepository.Find(
                i => !i.IsDeleted &&
                (!string.IsNullOrEmpty(i.Manufactory) && i.Manufactory.ToLower().Contains(request.ConvertedSearch)) ||
                (!string.IsNullOrEmpty(i.Name) && i.Name.ToLower().Contains(request.ConvertedSearch)) ||
                (!string.IsNullOrEmpty(i.AutoComplete) && i.AutoComplete.ToLower().Contains(request.ConvertedSearch)) ||
                (!string.IsNullOrEmpty(i.ActiveMaterial) && i.ActiveMaterial.ToLower().Contains(request.ConvertedSearch))
             )
            .OrderBy(i => i.Name)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(i => new { i.Id, i.Name, i.Manufactory, i.ActiveMaterial});
            return Content(HttpStatusCode.OK, results);
        }
    }
}
