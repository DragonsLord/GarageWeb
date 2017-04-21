using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GarageWeb.Models;
using GarageWeb.Models.Interfaces;
using System.Security.Claims;
using System.IO;

namespace GarageWeb.Controllers
{
    public class MenuController : Controller
    {
        private IRepository<Dish> _dishes;

        public MenuController(IRepository<Dish> d)
        {
            _dishes = d;
        }
        public async Task<ActionResult> Index()
        {
            return View(await _dishes.Data.ToListAsync());
        }
        
        public async Task<ActionResult> Details(int? id = 0)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dish dish = await _dishes.Data.FirstOrDefaultAsync(d => d.Id == id);
            if (dish == null)
            {
                return HttpNotFound();
            }
            return View(dish);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Exclude = "CurrentRating")] Dish dish, HttpPostedFileBase file)
        {
            
            if (ModelState.IsValid)
            {
                if (file !=null)
                {
                    byte[] array;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                        array = ms.GetBuffer();
                    }
                    dish.Image = array;
                }
                await _dishes.AddAsync(dish);
                return RedirectToAction("Index");
            }

            return View(dish);
        }


        public async Task<ActionResult> Edit(int? id=0)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dish dish = await _dishes.Data.FirstOrDefaultAsync(t=>t.Id==id);
            if (dish == null)
            {
                return HttpNotFound();
            }
            return View(dish);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Exclude = "CurrentRating")] Dish dish)
        {
            if (ModelState.IsValid)
            {
                await _dishes.EditAsync(dish);
                return RedirectToAction("Index");
            }
            return View(dish);
        }

        // GET: Dishes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dish dish = _dishes.Data.FirstOrDefault(d => d.Id == id);
            if (dish == null)
            {
                return HttpNotFound();
            }
            return View(dish);
        }

        // POST: Dishes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _dishes.RemoveAsync(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dishes.Dispose();
            }
            base.Dispose(disposing);
        }
        [NonAction]
        public async void SetRatingAsync(int dish_id, int value)
        {
            var dish = await _dishes.Data.FirstOrDefaultAsync(d => d.Id == dish_id);
            string userToken = ((ClaimsIdentity)HttpContext.User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value,
                userProvider = ((ClaimsIdentity)HttpContext.User.Identity).FindFirst("Provider").Value;
            if (!dish.Ratings.Any(r => r.UserToken == userToken && r.UserProvider == userProvider))
            {
                var r = new Rating()
                {
                    UserToken = userToken,
                    UserProvider = userProvider,
                    DishId = dish_id,
                    Value = value
                };
                dish.Ratings.Add(r);
                _dishes.Save();
            }
        }
        [NonAction]
        public async void SetReviewAsync(int dish_id, string text)
        {
            var dish = await _dishes.Data.FirstOrDefaultAsync(d => d.Id == dish_id);
            string userToken = ((ClaimsIdentity)HttpContext.User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value,
                userProvider = ((ClaimsIdentity)HttpContext.User.Identity).FindFirst("Provider").Value;
            if (!dish.Reviews.Any(r => r.UserToken == userToken && r.UserProvider == userProvider))
            {
                var r = new Review()
                {
                    UserToken = userToken,
                    UserProvider = userProvider,
                    DishId = dish_id,
                    Content = text
                };
                dish.Reviews.Add(r);
                _dishes.Save();
            }
        }
    }
    
}