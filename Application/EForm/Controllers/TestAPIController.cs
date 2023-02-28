using EForm.BaseControllers;
using EForm.Services;
using System.Net;
using System.Web.Http;
namespace EForm.Controllers
{
    public class TestAPIController : BaseApiController
    {
        [HttpGet]
        [Route("Test/APU")]
        public IHttpActionResult index()
        {
            var t = new ChargeLog();
            var x = t.test();
            return Content(HttpStatusCode.OK, new
            {
                Message = "Đã nhận thông tin"
            });
        }
    }
}