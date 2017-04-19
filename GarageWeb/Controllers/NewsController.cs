using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;
using GarageWeb.Models;
using GarageWeb.Models.Interfaces;

namespace GarageWeb.Controllers
{
    public class NewsController : Controller
    {
        private IRepository<NewsEntry> _news;

        public NewsController(IRepository<NewsEntry> n)
        {
            _news = n;
        }

        // GET: News
        public async Task<ActionResult> Index()
        {
            return View(await _news.Data.ToListAsync());
        }

        // GET: News/Details/5
        //public async Task<ActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    NewsEntry newsEntry = await db.News.FindAsync(id);
        //    if (newsEntry == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(newsEntry);
        //}

        //// GET: News/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: News/Create
        //// Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        //// сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include = "Id,Titile,Description,Image")] NewsEntry newsEntry)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.News.Add(newsEntry);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    return View(newsEntry);
        //}

        //// GET: News/Edit/5
        //public async Task<ActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    NewsEntry newsEntry = await db.News.FindAsync(id);
        //    if (newsEntry == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(newsEntry);
        //}

        //// POST: News/Edit/5
        //// Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        //// сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "Id,Titile,Description,Image")] NewsEntry newsEntry)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(newsEntry).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    return View(newsEntry);
        //}

        //// GET: News/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    NewsEntry newsEntry = await db.News.FindAsync(id);
        //    if (newsEntry == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(newsEntry);
        //}

        //// POST: News/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    NewsEntry newsEntry = await db.News.FindAsync(id);
        //    db.News.Remove(newsEntry);
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
