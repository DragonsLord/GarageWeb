using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Google;
using System.Security.Claims;
using System.Threading.Tasks;
using Duke.Owin.VkontakteMiddleware;
using Duke.Owin.VkontakteMiddleware.Provider;
using Owin.Security.Providers.GitHub;
using System.Web.Configuration;

[assembly: OwinStartup(typeof(GarageWeb.Startup))]
namespace GarageWeb
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        { 
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Admin/Login"),
            });
            app.UseExternalSignInCookie("ExternalCookie");
            app.SetDefaultSignInAsAuthenticationType("ExternalCookie");
            app.UseGitHubAuthentication(new GitHubAuthenticationOptions()
            {
                ClientId = "037d0dfd9476aa09bf70",
                ClientSecret = WebConfigurationManager.AppSettings["GitHubSecret"],
                Provider = new GitHubAuthenticationProvider
                {
                    OnAuthenticated = context =>
                    {
                        dynamic stuff1 = Newtonsoft.Json.JsonConvert.DeserializeObject(context.User.ToString());
                        string avatar = stuff1.avatar_url;
                        context.Identity.AddClaim(new Claim("Provider", "GitHub"));
                        context.Identity.AddClaim(new Claim("Avatar", avatar));

                        return Task.FromResult(true);
                    }
                }

            });

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "373441158521-on3q879mcd6q0iqfmb4l6n6pd6bjj2gb.apps.googleusercontent.com",
                ClientSecret = WebConfigurationManager.AppSettings["GoogleSecret"],
                Provider = new GoogleOAuth2AuthenticationProvider
                {
                    OnAuthenticated = context =>
                    {
                        dynamic stuff1 = Newtonsoft.Json.JsonConvert.DeserializeObject(context.User.ToString());
                        string avatar = stuff1.image.url;
                        context.Identity.AddClaim(new Claim("Provider", "Google"));
                        context.Identity.AddClaim(new Claim("Avatar", avatar));

                        return Task.FromResult(true);
                    }
                }
            });
            app.UseFacebookAuthentication(new Microsoft.Owin.Security.Facebook.FacebookAuthenticationOptions()
            {
                AppId = "800402306775205",
                AppSecret = WebConfigurationManager.AppSettings["FacebookSecret"],
                Provider = new Microsoft.Owin.Security.Facebook.FacebookAuthenticationProvider()
                {
                    OnAuthenticated = context =>
                    {
                        context.Identity.AddClaim(new Claim("Provider", "Facebook"));
                        context.Identity.AddClaim(new Claim("Avatar", $"http://graph.facebook.com/{context.Id}/picture?type=square"));
                        return Task.FromResult(true);
                    }
                }
            });
            app.UseVkontakteAuthentication(new VkAuthenticationOptions()
            {
                AppId = "5467899",
                AppSecret = WebConfigurationManager.AppSettings["VKSecret"],
                Scope = "email,photos",
                Provider = new VkAuthenticationProvider()
                {
                    OnAuthenticated = context => {
                        context.Identity.RemoveClaim(context.Identity.FindFirst(ClaimsIdentity.DefaultNameClaimType));
                        context.Identity.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, context.FullName));
                        context.Identity.AddClaim(new Claim("Provider", "VK"));

                        context.Identity.AddClaim(new Claim("Avatar", context.Link));

                        return Task.FromResult(true);
                    }
                }
            });
        }
    }
}