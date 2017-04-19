using System.Web.Mvc;
using GarageWeb.Models;

namespace GarageWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (CoffeDBContext db = new CoffeDBContext())
            {
                //db.Database.ExecuteSqlCommand("DROP TABLE [Ratings]");
                //db.Database.ExecuteSqlCommand("DROP TABLE [Users]");
                //db.Database.ExecuteSqlCommand("DROP TABLE [Dishes]");
                Dish d = new Dish()
                {
                    Name = "Test",
                    Weight = 100,
                    Price = 20
                };
                db.Dishes.Add(d);
                User u = new Models.User()
                {
                    Name = "TestUser",
                    Token = "Test",
                    Provider = "Test"
                };
                db.Users.Add(u);
                Rating r = new Rating()
                {
                    Dish = d,
                    User = u,
                    Value = 5
                };
                db.Ratings.Add(r);
                db.SaveChanges();
            }

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