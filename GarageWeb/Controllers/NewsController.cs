using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;
using GarageWeb.Models;
using GarageWeb.Models.Interfaces;
using System.IO;
using System.Web;
using System.Net;

namespace GarageWeb.Controllers
{
    public class NewsController : Controller
    {
        private IRepository<NewsEntry> _news;

        public NewsController(IRepository<NewsEntry> n)
        {
            _news = n;
        }
        
        public async Task<ActionResult> Index()
        {
            return View(await _news.Data.ToListAsync());
        }
        
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsEntry newsEntry = await _news.Data.FirstAsync(t => t.Id == id.Value);
            if (newsEntry == null)
            {
                return HttpNotFound();
            }
            return View(newsEntry);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Titile,Description,Image")] NewsEntry news, HttpPostedFileBase file)
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
                    news.Image = array;
                }
                await _news.AddAsync(news);
                return RedirectToAction("Index");
            }
            return View(news);
        }


        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsEntry newsEntry = await _news.Data.FirstAsync(t => t.Id == id.Value);
            if (newsEntry == null)
            {
                return HttpNotFound();
            }
            return View(newsEntry);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Titile,Description,Image")] NewsEntry news, HttpPostedFileBase file)
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
                    news.Image = array;
                }
                await _news.AddAsync(news);
                return RedirectToAction("Index");
            }
            return View(news);
        }
        
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsEntry newsEntry =  await _news.Data.FirstAsync(t=>t.Id== id.Value);
            if (newsEntry == null)
            {
                return HttpNotFound();
            }
            return View(newsEntry);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _news.RemoveAsync(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _news.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
