using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GarageWeb.Infrastructure
{
    internal class ChallengeResult : HttpUnauthorizedResult
    {
        public string LoginProvider { get; set; }
        public string RedirectUri { get; set; }

        public ChallengeResult(string provider, string redirectUri)            
        {
            LoginProvider = provider;
            RedirectUri = redirectUri;
        }
        
        public override void ExecuteResult(ControllerContext context)
        {

            var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
            context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
        }
    }
}