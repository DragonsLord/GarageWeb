using GarageWeb.Models;
using System.Linq;
using System.Web.Mvc;
using GarageWeb.Models.Interfaces;
using GarageWeb.Models.ViewModel;
using System.Net.Http;
using System;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using GarageWeb.Infrastructure;

namespace GarageWeb.Controllers
{
    public class BasketController : Controller
    {
        IRepository<Dish> _dishes;
        IRepository<Order> _orders;

        public BasketController(IRepository<Dish> d, IRepository<Order> o)
        {
            _dishes = d;
            _orders = o;
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

        [HttpPost]
        public async Task<ActionResult> ProcessOrder(Basket basket, OrderViewModel order)
        {
            LiqPay lp = new LiqPay("","");
            var result = await lp.PayTest(order.Phone, order.ToPay, order.OrderId, order.Card, order.CardExpMonth, order.CardExpYear, order.CardCVV, order.GetUserIP());
            System.Diagnostics.Debug.WriteLine(result);

            //TODO Check result

            var day = DateTime.Now;
            if (day.Hour > order.TargetHour)
                day = day.AddDays(1);
            Order o = new Order()
            {
                Name = order.Name,
                DeliveryAddress = order.DeliveryAddress,
                Phone = order.Phone,
                ToPay = order.ToPay,
                Time = new DateTime(day.Year, day.Month, day.Day, order.TargetHour, order.TargetMinute, 0)
            };

            foreach (var d in basket.Orders)
            {
                o.DishOrder.Add(new DishOrder()
                {
                    OrderId = o.Id,
                    DishId = d.Dish.Id,
                    Count = d.Count
                });
            }
            await _orders.AddAsync(o);
            return RedirectToAction("Index","Main");
        }
    }
}