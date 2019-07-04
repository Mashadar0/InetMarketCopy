using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InetMarket.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Display(Name = "Название")]
        [Required(ErrorMessage = "Не указано название")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Недопустимая длина")]
        public string Title { get; set; }

        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Не указано имя")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Недопустимая длина")]
        public string Name { get; set; }

        [Display(Name = "Электронная почта")]
        [Required(ErrorMessage = "Не указана почта")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Телефон")]
        [Required(ErrorMessage = "Не указан номер телефона")]
        [Phone]
        public string Phone { get; set; }

        [Display (Name = "Комментарий")]
        public string Comment { get; set; }

        [Display (Name = "Дата")]
        public DateTime DateTime { get; set; }

        [Display(Name = "Клиент")]
        public int ClientId { get; set; }
    }
}
