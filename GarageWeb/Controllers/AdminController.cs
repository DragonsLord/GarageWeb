using GarageWeb.Models.ViewModel;
using GarageWeb.Models.ViewModel.AdminPanel;
using System.Web.Mvc; 
using GarageWeb.Infrastructure;
using GarageWeb.Models.Interfaces;
using GarageWeb.Models;
using System.Linq;

namespace GarageWeb.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IAuthHelper authHelper;
        IRepository<Order> _orders;
        public AdminController(IAuthHelper helper, IRepository<Order> o)
        {
            authHelper = helper;
            _orders = o;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(new AdminPanelViewModel());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Order()
        {
            return View(_orders.Data.ToList());
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int id)
        {
            return View(_orders.Data.First(t=>t.Id==id));
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (HttpContext.User.IsInRole("Admin"))
                return RedirectToAction("Index", "Admin");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
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

        [HttpPost]
        public PartialViewResult ChangeLoginInfo(ChangeLoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(model);
            }
            if (authHelper.SetNewPassword(model.OldPassword, model.NewPassword) && authHelper.SetNewLogin(model.Login))
            {
                model.Message = "Дані успішно змінено";
                return PartialView(model);
            }
            ModelState.AddModelError("", "Не правильний старий пароль");
            return PartialView(model); ;
        }
        [HttpPost]
        public PartialViewResult ChangeMainPageSettings(MainPageSettingsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(model);
            }
            model.SaveChanges();
            model.Message = "Дані успішно збережено";
            return PartialView(model);
        }
        [HttpPost]
        public PartialViewResult ChangeOrderingSettings(OrderingSettingsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(model);
            }
            model.SaveChanges();
            model.Message = "Дані успішно збережено";
            return PartialView(model);
        }
    }
}