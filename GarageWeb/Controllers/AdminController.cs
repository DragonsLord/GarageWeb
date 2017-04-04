using GarageWeb.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using GarageWeb.Infrastructure;

namespace GarageWeb.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        AdminAuthHelper authHelper = new AdminAuthHelper();

        // GET: Admin
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Panel()
        {
            return View();
        }
        
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        //public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (!authHelper.SignIn(model.Login, model.Password))
            {
                ModelState.AddModelError("", "Не правильний логін або пароль");
                return View(model);
            }
            return RedirectToAction("Panel");
            
        }
    }
}