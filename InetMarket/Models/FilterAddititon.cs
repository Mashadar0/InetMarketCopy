using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InetMarket.Models
{
    public class FilterAddititon
    {
        public int Id { get; set; }

        [Display(Name = "Название")]
        [Required(ErrorMessage = "Не указано название")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Недопустимая длина")]
        public string Title { get; set; }

        [Display(Name = "Фильтр")]
        public int FilterId { get; set; }

        public Filter Filter { get; set; }

        [NotMapped]
        public bool IsChecked { get; set; }
    }
}
