using System.ComponentModel.DataAnnotations;
using GarageWeb.Infrastructure;
namespace GarageWeb.Models.ViewModel.AdminPanel
{
    public class ChangeLoginViewModel
    {
        public const string Name = "changeLoginBlock";
        public string Message { get; set; }
        public bool IsChange { get; set; } = false;

        [Required(ErrorMessage = "Логін обов'язковий для введення!")]
        [Display(Name = "Новий Логін")]
        public string Login { get; set; } = Settings.AdminLogin;

        [Required(ErrorMessage = "Пароль обов'язковий для введення!")]
        [DataType(DataType.Password)]
        [Display(Name = "Новий Пароль")]
        [MinLength(5,ErrorMessage ="Пароль повинен бути не менше 5 символів!")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Старий пароль обов'язковий для введення!")]
        [DataType(DataType.Password)]
        [Display(Name = "Старий Пароль")]
        public string OldPassword { get; set; }
    }
}