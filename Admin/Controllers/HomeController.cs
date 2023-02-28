using Admin.CustomAuthen;
using System.Web.Mvc;

namespace Admin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}