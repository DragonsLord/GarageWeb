using GarageWeb.Models.ViewModel;
using GarageWeb.Models.ViewModel.AdminPanel;
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
            return View(new AdminPanelViewModel());
        }
        
        // POST: /Account/Login
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