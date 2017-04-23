using System.Web.Mvc;
using GarageWeb.Models.Interfaces;
using GarageWeb.Models;
using System.Linq;
using GarageWeb.Models.ViewModel;

namespace GarageWeb.Controllers
{
    public class HomeController : Controller
    {
        private IRepository<Dish> _dishes;
        private IRepository<NewsEntry> _news;
        public HomeController(IRepository<Dish> d, IRepository<NewsEntry> n)
        {
            _dishes = d;
            _news = n;
        }
        public ActionResult Index()
        {
            HomeViewModel main = new HomeViewModel();
            main.Dishes= _dishes.Data.ToList().OrderByDescending(t => t.CurrentRating).ThenBy(t => t.Name).Take(5).ToList();
            main.News = _news.Data.OrderBy(t => t.DateTime).Take(10).ToList();
            return View(main);
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