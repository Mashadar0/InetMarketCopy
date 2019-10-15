using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InetMarket.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InetMarket.Controllers.UserControllers
{
    public class UserCategoryController : Controller
    {
        private readonly MarketContext _context;

        public UserCategoryController(MarketContext context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }
    }
}