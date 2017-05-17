using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Configuration;

namespace GarageWeb.Infrastructure
{
    public static class Settings
    {
        private static object _mutexOnMain = new object();
        private static object _mutexOnOrdering = new object();
        private static object _mutexOnTimer = new object();

        private static System.Configuration.Configuration _webConfig;
        public static System.Configuration.Configuration WebConfig
        {
            get
            {
                if (_webConfig == null)
                    _webConfig = WebConfigurationManager.OpenWebConfiguration("~/");
                return _webConfig;
            }
        }
        public static string AdminLogin
        {   
            get
            {
                return WebConfigurationManager.AppSettings["AdminLogin"];
            }
        }

        public static int TotalDishesOnMain
        {
            get
            {
                Monitor.Enter(_mutexOnMain);
                var val = int.Parse(WebConfigurationManager.AppSettings["TotalDishesOnMain"]);
                Monitor.Exit(_mutexOnMain);
                return val;
            }
            set
            {
                Monitor.Enter(_mutexOnMain);
                WebConfig.AppSettings.Settings["TotalDishesOnMain"].Value = value.ToString();
                Monitor.Exit(_mutexOnMain);
            }
        }
        public static int DishesOnMain
        {
            get
            {
                Monitor.Enter(_mutexOnMain);
                var val = int.Parse(WebConfigurationManager.AppSettings["DishesOnMain"]);
                Monitor.Exit(_mutexOnMain);
                return val;
            }
            set
            {
                Monitor.Enter(_mutexOnMain);
                WebConfig.AppSettings.Settings["DishesOnMain"].Value = value.ToString();
                Monitor.Exit(_mutexOnMain);
            }
        }
        public static int DishChangeDelayOnMain
        {
            get
            {
                Monitor.Enter(_mutexOnMain);
                var val = int.Parse(WebConfigurationManager.AppSettings["DishChangeDelayOnMain"]);
                Monitor.Exit(_mutexOnMain);
                return val;
            }
            set
            {
                Monitor.Enter(_mutexOnMain);
                WebConfig.AppSettings.Settings["DishChangeDelayOnMain"].Value = value.ToString();
                Monitor.Exit(_mutexOnMain);
            }
        }

        public static bool IsOrderingEnabled
        {
            get
            {
                Monitor.Enter(_mutexOnOrdering);
                var val = bool.Parse(WebConfigurationManager.AppSettings["IsOrderingEnabled"]);
                Monitor.Exit(_mutexOnOrdering);
                return val;
            }
            set
            {
                Monitor.Enter(_mutexOnOrdering);
                WebConfig.AppSettings.Settings["IsOrderingEnabled"].Value = value.ToString();
                Monitor.Exit(_mutexOnOrdering);
            }
        }
        public static bool IsShippingEnabled
        {
            get
            {
                Monitor.Enter(_mutexOnOrdering);
                var val = bool.Parse(WebConfigurationManager.AppSettings["IsShippingEnabled"]);
                Monitor.Exit(_mutexOnOrdering);
                return val;
            }
            set
            {
                Monitor.Enter(_mutexOnOrdering);
                WebConfig.AppSettings.Settings["IsShippingEnabled"].Value = value.ToString();
                Monitor.Exit(_mutexOnOrdering);
            }
        }

        public static event Action<TimeSpan> OnOrdersDeleteTimeChange;
        public static event Action<short> OnOrdersDeleteDayIntervalChange;
        public static TimeSpan OrdersDeleteTime
        {
            get
            {
                Monitor.Enter(_mutexOnTimer);
                var val = TimeSpan.Parse(WebConfigurationManager.AppSettings["OrdersDeleteTime"]);
                Monitor.Exit(_mutexOnTimer);
                return val;
            }
            set
            {
                Monitor.Enter(_mutexOnTimer);
                WebConfig.AppSettings.Settings["OrdersDeleteTime"].Value = value.ToString();
                OnOrdersDeleteTimeChange?.Invoke(value);
                Monitor.Exit(_mutexOnTimer);
            }
        }
        public static short OrdersDeleteDaysInterval
        {
            get
            {
                Monitor.Enter(_mutexOnTimer);
                var val = short.Parse(WebConfigurationManager.AppSettings["OrdersDeleteDaysInterval"]);
                Monitor.Exit(_mutexOnTimer);
                return val;
            }
            set
            {
                Monitor.Enter(_mutexOnTimer);
                WebConfig.AppSettings.Settings["OrdersDeleteDaysInterval"].Value = value.ToString();
                OnOrdersDeleteDayIntervalChange?.Invoke(value);
                Monitor.Exit(_mutexOnTimer);
            }
        }

        public static void SaveCanges()
        {
            WebConfig.Save();
            _webConfig = null;
        }
    }
}