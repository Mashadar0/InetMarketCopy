using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InetMarket.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace InetMarket.Controllers.UserControllers
{
    public class UserProductController : Controller
    {
        private readonly MarketContext _context;
        public UserProductController(MarketContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? categoryId)
        {
            IQueryable<Product> productsCateg = _context.Products.Include(p => p.Category);
            if (categoryId != null && categoryId != 0)
            {
                productsCateg = productsCateg.Where(p => p.CategoryId == categoryId);
            }
            ProductListView plv = new ProductListView
            {
                Products = productsCateg.ToList(),
            };
            return View(plv);
        }
    }
}