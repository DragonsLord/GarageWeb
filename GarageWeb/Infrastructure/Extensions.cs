using GarageWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace GarageWeb.Infrastructure
{
    public static class Extensions
    {
        public static bool IsRated(this ClaimsIdentity identity, Dish dish)
        {
            var token = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (token == "admin") return true;
            var provider = identity.FindFirst("Provider").Value;

            return dish.Ratings.Any(r => r.UserToken == token && r.UserProvider == provider);
        }

        public static Expression<Func<Dish, double>> Test(this HtmlHelper helper)
        {
            return (d) => d.CurrentRating;
        }
    }
}