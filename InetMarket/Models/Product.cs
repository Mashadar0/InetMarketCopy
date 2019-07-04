using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InetMarket.Models
{
    public class Product
    {
        
        public int Id { get; set; }

        [Display (Name ="Название")]
        [Required(ErrorMessage = "Не указано название")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Недопустимая длина")]
        public string Title { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Изображение")]
        public string MainImage { get; set; }

        [Display(Name = "Скидка")]
        public bool IsDiscount { get; set; }

        [Display(Name = "На главной")]
        public bool IsMane { get; set; }

        [Display(Name = "Категория")]
        public int CategoryId { get; set; }

        [Display(Name = "Цена")]
        public string Price { get; set; }

        [Display(Name = "Марка авто")]
        public int BrandId { get; set; }

        [Display(Name = "Поставщик")]
        public int ProviderId { get; set; }

        public Category Category { get; set; }
        public Brand Brand { get; set; }
        public Provider Provider { get; set; }
    }
}
