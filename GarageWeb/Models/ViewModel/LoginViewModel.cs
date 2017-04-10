using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GarageWeb.Models.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Логін обов'язковий для введення!")]
        [Display(Name = "Логін")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Пароль обов'язковий для введення!")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}