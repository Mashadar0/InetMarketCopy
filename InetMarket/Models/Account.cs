using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InetMarket.Models
{
    public class Account
    {
        [Display (Name = "Логин")]
        [Required(ErrorMessage = "Не указан логин")]
        public string Name { get; set; }

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Не указан пароль")]
        public string Password { get; set; }
    }
}
