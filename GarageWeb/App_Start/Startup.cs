using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Google;
using GarageWeb.Infrastructure;
using Microsoft.AspNet.Identity;

[assembly: OwinStartup(typeof(GarageWeb.Startup))]
namespace GarageWeb
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            app.SetDefaultSignInAsAuthenticationType("ExternalCookie");
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Admin/Login"),
            });
            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    AuthenticationType = "ExternalCookie",
            //    LoginPath = new PathString("/Admin/Login"),
            //});
            app.UseExternalSignInCookie("ExternalCookie");
            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "373441158521-on3q879mcd6q0iqfmb4l6n6pd6bjj2gb.apps.googleusercontent.com",
                ClientSecret = "9dQtQs34jGqQlPRxgCtEgOfS",
                Provider = new GoogleOAuth2AuthenticationProvider
                {
                    OnAuthenticated = context =>
                    {
                        context.Identity.AddClaim(new System.Security.Claims.Claim("urn:google:profile", context.AccessToken));
                        return System.Threading.Tasks.Task.FromResult(true);
                    }
                }
            });
        }
    }
}