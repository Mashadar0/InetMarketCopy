using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InetMarket.Models
{
    public class Provider
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Product { get; set; }
        public int Count { get; set; }
        public string Price { get; set; }
        public DateTime DateTime { get; set; }
    }
}
