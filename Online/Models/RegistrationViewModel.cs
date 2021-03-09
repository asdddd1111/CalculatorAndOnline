using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Online.Models
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "введите логин")]
        [DisplayName("Логин")]
        [Remote(action: "CheckLogin", controller: "Account", HttpMethod = "POST")]
        public string Login { get; set; }
        [Required(ErrorMessage = "введите пароль")]
        [DisplayName("Пароль")]
        public string Password { get; set; }
        [Compare("Password")]
        public string RepeatPassword { get; set; }
    }
}
