using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InetMarket.Models
{
    //таблица поставщиков продукции
    public class Provider
    {
        public int Id { get; set; }

        [Display(Name = "Название")]
        [Required(ErrorMessage = "Не указано название")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Недопустимая длина")]
        public string Title { get; set; }

        [Display(Name = "Продукция")]
        public string Product { get; set; }

        [Display(Name = "Количество")]
        public int Count { get; set; }

        [Display(Name = "Цена")]
        public string Price { get; set; }

        [Display(Name = "Дата")]
        public DateTime DateTime { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
