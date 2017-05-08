using System.ComponentModel.DataAnnotations;
using GarageWeb.Infrastructure;

namespace GarageWeb.Models.ViewModel.AdminPanel
{
    public class MainPageSettingsViewModel
    {
        public const string Name = "mainPageSettingsBlock";
        public string Message { get; set; }
        [Required(ErrorMessage = "Поле не може бити пустим")]
        [Display(Name = "К-сть новин на головній сторінці")]
        public int NewsNumber { get; set; } = Settings.NewsOnMain;
        [Required(ErrorMessage = "Поле не може бити пустим!")]
        [Display(Name = "К-сть популярних страв на головній сторінці")]
        public int DishesNumber { get; set; } = Settings.DishesOnMain;
        [Required(ErrorMessage = "Поле не може бити пустим")]
        [Display(Name = "Затримка зміни страви (с)", Description = "Період прокрутки страв на головні сторінці")]
        public int DishChangeDelay { get; set; } = Settings.DishChangeDelayOnMain;

        public void SaveChanges()
        {
            Settings.NewsOnMain = NewsNumber;
            Settings.DishesOnMain = DishesNumber;
            Settings.DishChangeDelayOnMain = DishChangeDelay;
        }
    }
}