using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Configuration;
using System.Threading;

namespace GarageWeb.Models.ViewModel
{
    public sealed partial interface IGssd
    {

    }
    public class ChangeLoginViewModel
    {
        public bool IsSelected { get; set; }
        public bool IsChange { get; set; } = false;

        [Required(ErrorMessage = "Логін обов'язковий для введення!")]
        [Display(Name = "Новий Логін")]
        public string Login { get; set; }

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