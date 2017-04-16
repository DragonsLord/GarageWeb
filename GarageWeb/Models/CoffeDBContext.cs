using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace GarageWeb.Models
{
    public class CoffeDBContext : DbContext
    {
        public CoffeDBContext() : base()
        { }

        public DbSet<Dish> Dishes { get; set; }
        public DbSet<NewsEntry> News { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}