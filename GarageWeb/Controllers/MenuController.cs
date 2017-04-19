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
        public ActionResult Create([Bind(Exclude = "CurrentRating")] Dish dish)
        {
            
            if (ModelState.IsValid)
            {
                _dishes.Add(dish);
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
        public ActionResult Edit(/*[Bind(Exclude = "CurrentRating")]*/ Dish dish)
        {
            if (ModelState.IsValid)
            {
                _dishes.Edit();
                return RedirectToAction("Index");
            }
            return View(dish);
        }

        //// GET: Dishes/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Dish dish = await db.Dishes.FindAsync(id);
        //    if (dish == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(dish);
        //}

        //// POST: Dishes/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    Dish dish = await db.Dishes.FindAsync(id);
        //    db.Dishes.Remove(dish);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
    
}
