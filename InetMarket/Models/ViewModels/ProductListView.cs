using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InetMarket.Models
{
    public class ProductListView
    {
        public IEnumerable<Product> Products { get; set; }
        public SelectList Categories { get; set; }
        public SelectList Filters { get; set; }
    }
}
