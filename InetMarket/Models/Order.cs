using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InetMarket.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Comment { get; set; }
        public DateTime DateTime { get; set; }
        public int ClientId { get; set; }
    }
}
