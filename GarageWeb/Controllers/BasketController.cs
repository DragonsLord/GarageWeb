using GarageWeb.Models;
using System.Linq;
using System.Web.Mvc;
using GarageWeb.Models.Interfaces;

namespace GarageWeb.Controllers
{
    public class BasketController : Controller
    {
        IRepository<Dish> _dishes;

        public BasketController(IRepository<Dish> d)
        {
            _dishes = d;
        }
        public ActionResult Index(Basket bucket, string returnUrl)
        {
            ViewBag.Url = returnUrl;
            return View(bucket);
        }

        [HttpPost]
        public void AddToBasket(Basket bucket, int id)
        {
            Dish orderDish = _dishes.Data.FirstOrDefault(t => t.Id == id);
            if (orderDish!=null)
            {
                bucket.AddDish(orderDish);
            }
        }
        [HttpPost]
        public void RemoveOneFromBasket(Basket bucket, int id)
        {
            Dish orderDish = _dishes.Data.FirstOrDefault(t => t.Id == id);
            if (orderDish != null)
            {
                bucket.RemoveOnePortion(orderDish);
            }
        }
        [HttpPost]
        public void RemoveFromBasket(Basket bucket, int id)
        {
            Dish orderDish = _dishes.Data.FirstOrDefault(t => t.Id == id);
            if (orderDish != null)
            {
                bucket.RemoveDish(orderDish);
            }
        }
        [HttpPost]
        public void ClearBasket(Basket basket)
        {
            basket.Clear();
        }
        public ActionResult Order(Basket basket)
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetCount(Basket basket)
        { 
            return Json(new { Count=basket.Count,Price = basket.Price + " грн"},"",System.Text.Encoding.UTF8);
        }
        [HttpPost]
        public ActionResult GetPriceById(Basket basket,int id)
        {
            var dish = basket.Orders.Find(t => t.Dish.Id == id);
            return Json(new { Price = dish.Dish.Price*dish.Count + " грн" }, "", System.Text.Encoding.UTF8);
        }
    }
}