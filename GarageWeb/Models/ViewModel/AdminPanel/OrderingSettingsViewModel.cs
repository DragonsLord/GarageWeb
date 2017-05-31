using System;
using System.ComponentModel.DataAnnotations;
using GarageWeb.Infrastructure;

namespace GarageWeb.Models.ViewModel.AdminPanel
{
    public class OrderingSettingsViewModel
    {
        public const string Name = "orderingSettingsBlock";
        public string Message { get; set; }
        [Display(Name = "Онлайн замовлення активовано")]
        public bool IsEnabled { get; set; } = Settings.IsOrderingEnabled;
        [Display(Name = "Доставку активовано")]
        public bool IsShippingEnabled { get; set; } = Settings.IsShippingEnabled;
        [Display(Name = "Час видалення виконаних замовлень")]
        public TimeSpan OrdersDeleteTime { get; set; } = Settings.OrdersDeleteTime;
        [Display(Name = "Інтервал видалення замовлень (у днях)")]
        public short OrdersDeleteDaysInterval { get; set; } = Settings.OrdersDeleteDaysInterval;
        [Display(Name = "Відсоток від суми для авансу")]
        [Range(0, 100, ErrorMessage = "Значення повинно бути в межах від 0 до 100")]
        public short PrepaynmentPercent { get; set; } = Settings.PrepaynmentPercent;
        [Display(Name = "Ціна доставки")]
        public double DeliveryPrice { get; set; } = Settings.DeliveryPrice;
        public void SaveChanges()
        {
            Settings.IsOrderingEnabled = IsEnabled;
            Settings.IsShippingEnabled = IsShippingEnabled;
            Settings.OrdersDeleteTime = OrdersDeleteTime;
            Settings.OrdersDeleteDaysInterval = OrdersDeleteDaysInterval;
            Settings.PrepaynmentPercent = PrepaynmentPercent;
            Settings.DeliveryPrice = DeliveryPrice;
            Settings.SaveCanges();
        }
    }
}