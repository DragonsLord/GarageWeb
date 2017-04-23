using GarageWeb.Models;
using System.Linq;
using System.Web.Mvc;
using GarageWeb.Models.Interfaces;

namespace GarageWeb.Controllers
{
    public class BusketController : Controller
    {
        private IRepository<Dish> _dishes;
        public BusketController(IRepository<Dish> d)
        {
            _dishes = d;
        }
        public ActionResult Index(Busket bucket)
        {
            return View(bucket);
        }

        [HttpPost]
        public void AddToBucket(Busket bucket, int id)
        {
            Dish orderDish = _dishes.Data.FirstOrDefault(t => t.Id == id);
            if (orderDish!=null)
            {
                bucket.AddDish(orderDish);
            }
        }
        [HttpPost]
        public void RemoveFromBucket(Busket bucket, int id)
        {
            Dish orderDish = _dishes.Data.FirstOrDefault(t => t.Id == id);
            if (orderDish == null)
            {
                bucket.RemoveDish(orderDish);
            }
        }
        [HttpPost]
        public void ClearBucket(Busket bucket, int id)
        {
            bucket.Clear();
        }
        
        [HttpPost]
        public ActionResult GetCount(Busket bucket)
        {
            return Json(bucket.Count);
        }
    }
}