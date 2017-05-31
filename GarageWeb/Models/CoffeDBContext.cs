using System;
using System.Linq;
using System.Data.Entity;
using System.Timers;
using System.Data.SqlServerCe;
using System.IO;
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
            try
            {
                SqlCeEngine ce = new SqlCeEngine(Database.Connection.ConnectionString);
                ce.Compact(Database.Connection.ConnectionString);
            }
            catch { }
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Order>().Property<int>(o => o.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            _ordersTimer = new Timer(TimeSpan.FromDays(Settings.OrdersDeleteDaysInterval).TotalMilliseconds);
            _ordersTimer.Elapsed += (sender, e) =>
            {
                var to_remove = Orders.Where(o => o.Time < DateTime.Now);
                DirectoryInfo dir = new DirectoryInfo($"{System.AppDomain.CurrentDomain.BaseDirectory}/Checks");
                var files = dir.GetFiles();
                foreach (var file in files)  ///Check
                {
                    if (to_remove.Any(o => o.Id.ToString() == file.Name))
                        file.Delete();
                }
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