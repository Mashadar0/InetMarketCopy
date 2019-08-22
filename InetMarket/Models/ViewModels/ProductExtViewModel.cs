using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InetMarket.Models
{
    public class ProductExtViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<FilterAddititon> FilterAddititons { get; set; }
    }
}
