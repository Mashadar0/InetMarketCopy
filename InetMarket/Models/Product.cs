using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InetMarket.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string MainImage { get; set; }
        public bool IsDiscount { get; set; }
        public bool IsMane { get; set; }
        public int CategoryId { get; set; }
        public string Price { get; set; }
        public int BrandId { get; set; }
    }
}
