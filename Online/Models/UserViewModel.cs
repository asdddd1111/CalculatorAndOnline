using System.ComponentModel;

namespace Online.Models
{
    public class UserViewModel
    {
        [DisplayName("Логин")]
        public string Login { get; set; }
        [DisplayName("Пароль")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
