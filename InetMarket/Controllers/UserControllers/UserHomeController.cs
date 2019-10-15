using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InetMarket.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InetMarket.Controllers.UserControllers
{
    public class UserHomeController : Controller
    {
        private readonly MarketContext _context;
        public UserHomeController(MarketContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(_context.Products.ToList());
        }
    }
}