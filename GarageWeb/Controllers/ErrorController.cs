using System.Web.Mvc;

namespace GarageWeb.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View("NotFound");
        }
        public ActionResult NotFound()
        {
            return View();
        }
    }
}