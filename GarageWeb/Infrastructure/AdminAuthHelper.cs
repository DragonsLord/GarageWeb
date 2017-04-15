using Microsoft.Owin.Security;
using System.Web;
using System.Linq;
using System.Web.Configuration;
using System.Security.Claims;
using System.Security;
using System.Threading;

namespace GarageWeb.Infrastructure
{
    public class AdminAuthHelper : IAuthHelper
    {
        private static class AdminInfo
        {
            private static readonly object mutex = new object();
            private const string salt = "don`t uSe This as your password";
            public static bool CheckLogInData(string login, string password)
            {
                lock(mutex){
                    if (login != WebConfigurationManager.AppSettings["AdminLogin"])
                        return false;
                    if (Encrypt.EncryptString(password, salt) != WebConfigurationManager.AppSettings["AdminPassword"])
                        return false;
                    return true;
                }
            }

            public static void SetNewLogin(string new_login)
            {
                Monitor.Enter(mutex);
                WebConfigurationManager.AppSettings["AdminLogin"] = new_login;
                Monitor.Exit(mutex);
            }

            public static bool SetNewPassword(string old_password, string new_password)
            {
                lock (mutex) {
                    if (Encrypt.EncryptString(old_password, salt) == WebConfigurationManager.AppSettings["AdminPassword"])
                    {
                        WebConfigurationManager.AppSettings["AdminPassword"] = Encrypt.EncryptString(new_password, salt);
                        return true;
                    }
                }
                return false;
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Authentication;
            }
        }

        public bool SignIn(string login, string password)
        {
            if (AdminInfo.CheckLogInData(login, password))
            {

                ClaimsIdentity claim = new ClaimsIdentity("ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                claim.AddClaim(new Claim(ClaimTypes.NameIdentifier, "admin", ClaimValueTypes.String));
                claim.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, "Administrator", ClaimValueTypes.String));
                claim.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",
                    "OWIN Provider", ClaimValueTypes.String));
                claim.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, "Admin", ClaimValueTypes.String));
                claim.AddClaim(new Claim("Avatar", "/Images/AdminAvatar.jpg"));

                AuthenticationManager.SignOut();
                AuthenticationManager.SignIn(new AuthenticationProperties
                {
                    IsPersistent = true
                }, claim);
                return true;
            }
            else return false;
        }

        public void LogOut()
        {
            AuthenticationManager.SignOut();
        }

        public bool SetNewLogin(string new_login)
        {
            if (AuthenticationManager.User.Identity.IsAuthenticated)
            {
                AdminInfo.SetNewLogin(new_login);
                return true;
            }
            else return false;
        }

        public bool SetNewPassword(string old_password, string new_password)
        {
            if (AuthenticationManager.User.Identity.IsAuthenticated)
                return AdminInfo.SetNewPassword(old_password, new_password);
            else return false;
        }
    }
}