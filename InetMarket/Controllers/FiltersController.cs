using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InetMarket.Models;

namespace InetMarket.Controllers
{
    public class FiltersController : Controller
    {
        private readonly MarketContext _context;

        public FiltersController(MarketContext context)
        {
            _context = context;
        }

        // GET: Filters
        public async Task<IActionResult> Index()
        {
            var filterCateg = _context.Filters.Include(p => p.Category);
            return View(await _context.Filters.ToListAsync());
        }

        // GET: Filters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filter = await _context.Filters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (filter == null)
            {
                return NotFound();
            }

            return View(filter);
        }

        // GET: Filters/Create
        public IActionResult Create()
        {
            SelectList categories = new SelectList(_context.Categories, "Id", "Title");
            ViewBag.Categories = categories;

            return View();
        }

        // POST: Filters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,CategoryId")] Filter filter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(filter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(filter);
        }

        // GET: Filters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            SelectList categories = new SelectList(_context.Categories, "Id", "Title");
            ViewBag.Categories = categories;

            if (id == null)
            {
                return NotFound();
            }

            var filter = await _context.Filters.FindAsync(id);
            if (filter == null)
            {
                return NotFound();
            }
            return View(filter);
        }

        // POST: Filters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,CategoryId")] Filter filter)
        {
            if (id != filter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(filter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilterExists(filter.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(filter);
        }

        // GET: Filters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filter = await _context.Filters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (filter == null)
            {
                return NotFound();
            }

            return View(filter);
        }

        // POST: Filters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var filter = await _context.Filters.FindAsync(id);
            _context.Filters.Remove(filter);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilterExists(int id)
        {
            return _context.Filters.Any(e => e.Id == id);
        }
    }
}
