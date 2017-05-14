using System;
using System.Linq;
using System.Data.Entity;
using System.Timers;
using GarageWeb.Infrastructure;

namespace GarageWeb.Models
{
    public class CoffeDBContext : DbContext
    {
        private static Timer _ordersTimer;
        private static Timer _startTimer;
        private static void InitializeOrdersTimer(TimeSpan time)
        {
            _ordersTimer.Stop();
            _startTimer = new Timer();
            var interval = time.TotalMilliseconds - DateTime.Now.TimeOfDay.TotalMilliseconds;
            if (interval < 0) interval += TimeSpan.FromDays(1).TotalMilliseconds;
            _startTimer.Interval = interval;
            _startTimer.AutoReset = false;
            _startTimer.Elapsed += (sender, e) => _ordersTimer.Start();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            _ordersTimer = new Timer(TimeSpan.FromDays(Settings.OrdersDeleteDaysInterval).TotalMilliseconds);
            _ordersTimer.Elapsed += (sender, e) =>
            {
                Orders.RemoveRange(Orders.Where(o => o.Time < DateTime.Now));
                SaveChanges();
            };
            InitializeOrdersTimer(Settings.OrdersDeleteTime);
            Settings.OnOrdersDeleteDayIntervalChange += (new_interval) => _ordersTimer.Interval = TimeSpan.FromDays(new_interval).TotalMilliseconds;
            Settings.OnOrdersDeleteTimeChange += InitializeOrdersTimer;
        }

        public DbSet<Dish> Dishes { get; set; }
        public DbSet<NewsEntry> News { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}