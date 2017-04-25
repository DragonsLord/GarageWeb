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

        public void SaveChanges()
        {
            Settings.IsOrderingEnabled = IsEnabled;
            Settings.IsShippingEnabled = IsShippingEnabled;
        }
    }
}