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
        public static string AdminLogin
        {   
            get
            {
                return WebConfigurationManager.AppSettings["AdminLogin"];
            }
        }

        public static int NewsOnMain
        {
            get
            {
                Monitor.Enter(_mutexOnMain);
                var val = int.Parse(WebConfigurationManager.AppSettings["NewsOnMain"]);
                Monitor.Exit(_mutexOnMain);
                return val;
            }
            set
            {
                Monitor.Enter(_mutexOnMain);
                WebConfigurationManager.AppSettings["NewsOnMain"] = value.ToString();
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
                WebConfigurationManager.AppSettings["DishesOnMain"] = value.ToString();
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
                WebConfigurationManager.AppSettings["DishChangeDelayOnMain"] = value.ToString();
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
                WebConfigurationManager.AppSettings["IsOrderingEnabled"] = value.ToString();
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
                WebConfigurationManager.AppSettings["IsShippingEnabled"] = value.ToString();
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
                WebConfigurationManager.AppSettings["OrdersDeleteTime"] = value.ToString();
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
                WebConfigurationManager.AppSettings["OrdersDeleteDaysInterval"] = value.ToString();
                OnOrdersDeleteDayIntervalChange?.Invoke(value);
                Monitor.Exit(_mutexOnTimer);
            }
        }
    }
}