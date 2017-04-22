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
        
        [HttpPost, Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
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

        [HttpPost, Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Exclude = "CurrentRating")] Dish dish, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    byte[] array;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                        array = ms.GetBuffer();
                    }
                    dish.Image = array;
                }
                await _dishes.EditAsync(dish);
                return RedirectToAction("Index");
            }
            return View(dish);
        }

        [Authorize(Roles = "Admin")]
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
        [HttpPost, ActionName("Delete"), Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _dishes.RemoveAsync(id);
            return RedirectToAction("Index");
        }

        [HttpPost, Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateRating(int value, int dishID)
        {
            await SetRatingAsync(dishID, value);
            return new RedirectResult($"/Menu");
        }
        [HttpPost, Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ReviewDish(int dishID, string content)
        {
            await SetReviewAsync(dishID, content);
            return new RedirectResult($"/Menu/Details/{dishID}");
        }
        [HttpPost, Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AnswerToReview(int dishID, int reviewID, string content)
        {
            await SetReviewAnswerAsync(dishID, reviewID, content);
            return new RedirectResult($"/Menu/Details/{dishID}");
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
        private Task SetRatingAsync(int dish_id, int value)
        {
            return Task.Run(() =>
            {
                var dish = _dishes.Data.FirstOrDefault(d => d.Id == dish_id);
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
            });
        }
        [NonAction]
        private Task SetReviewAsync(int dish_id, string text)
        {
            return Task.Run(() =>
            {
                var dish = _dishes.Data.FirstOrDefault(d => d.Id == dish_id);
                string userToken = ((ClaimsIdentity)HttpContext.User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value,
                userProvider = ((ClaimsIdentity)HttpContext.User.Identity).FindFirst("Provider").Value;
                var r = new Review()
                {
                    UserToken = userToken,
                    UserProvider = userProvider,
                    DishId = dish_id,
                    Content = text,
                    Time = DateTime.Now
                };
                dish.Reviews.Add(r);
                _dishes.Save();
            });
        }
        [NonAction]
        private Task SetReviewAnswerAsync(int dish_id, int review_id, string text)
        {
            return Task.Run(() =>
            {
                var review = _dishes.Data.FirstOrDefault(d => d.Id == dish_id).Reviews.FirstOrDefault(r => r.Id == review_id);
                review.AdminAnswer = text;
                _dishes.Save();
            });
        }
    }
    
}