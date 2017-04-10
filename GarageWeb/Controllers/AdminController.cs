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
        private IAuthHelper authHelper;

        public AdminController(IAuthHelper helper)
        {
            authHelper = helper;
        }

        // GET: Admin
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (HttpContext.User.IsInRole("Admin"))
                return RedirectToAction("Index", "Admin");
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }
        
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
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
            return RedirectToAction("Index", "Admin");
            
        }

        public ActionResult LogOut()
        {
            authHelper.LogOut();
            return RedirectToAction("Index", "Home");
        }
    }
}