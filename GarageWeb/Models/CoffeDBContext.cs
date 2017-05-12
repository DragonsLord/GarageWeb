using System.Data.Entity;

namespace GarageWeb.Models
{
    public class CoffeDBContext : DbContext
    {
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<NewsEntry> News { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}