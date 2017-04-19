using Microsoft.Owin.Security;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GarageWeb.Infrastructure;
using System.Security.Claims;
using GarageWeb.Models;
using System.Linq;

namespace GarageWeb.Controllers
{
    public partial class UserController : Controller
    {
        private CoffeDBContext _db;
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "User"));
        }

        public async Task<ActionResult> ExternalLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ClaimsIdentity claim = new ClaimsIdentity("ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            claim.AddClaims(loginInfo.ExternalIdentity.Claims);

            AuthenticationManager.SignOut();
            AuthenticationManager.SignIn(new AuthenticationProperties
            {
                IsPersistent = true
            }, claim);
            using (_db = new CoffeDBContext())
            {
                var user = new Models.User(claim);
                if (!_db.Users.Any(u => u.Token == user.Token && u.Provider == user.Provider))
                {
                    _db.Users.Add(user);
                    await _db.SaveChangesAsync();
                }
            }
            return RedirectToAction("Index", "Home");
        }
        
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
    }
}