using System.Web.Mvc;
using GarageWeb.Models.Interfaces;
using GarageWeb.Models;
using System.Linq;

namespace GarageWeb.Controllers
{
    public class HomeController : Controller
    {
        private IRepository<Dish> _dishes;
        public HomeController(IRepository<Dish> d)
        {
            _dishes = d;
        }
        public ActionResult Index()
        {
            ViewBag.PopularDish = _dishes.Data.ToList().OrderByDescending(t => t.CurrentRating).ThenBy(t => t.Name).Take(5).ToList();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public HomeController()
        {

        }
    }
}