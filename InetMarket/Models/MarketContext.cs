using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InetMarket.Models
{
    public class MarketContext:DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Filter> Filters { get; set; }
        public DbSet<FilterAddititon> FilterAddititons { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ValueOrder> ValueOrders { get; set; }



        public MarketContext(DbContextOptions<MarketContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
