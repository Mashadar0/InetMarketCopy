using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InetMarket.Models;
using Microsoft.AspNetCore.Authorization;

namespace InetMarket.Controllers
{
    [Authorize(Roles = "admin")]
    public class FiltersController : Controller
    {
        private readonly MarketContext _context;

        public FiltersController(MarketContext context)
        {
            _context = context;
        }

        // GET: Filters
        public IActionResult Index(int? categoryId)
        {
            IQueryable<Filter> filtersCateg = _context.Filters.Include(p => p.Category);
            if (categoryId != null && categoryId != 0)
            {
                filtersCateg = filtersCateg.Where(p => p.CategoryId == categoryId);
            }
            List<Category> categories = _context.Categories.ToList();
            // устанавливаем начальный элемент, который позволит выбрать всех
            categories.Insert(0, new Category { Title = "All", Id = 0 });
            FilterListView flv = new FilterListView
            {
                Categories = new SelectList(categories, "Id", "Title"),
                Filters = filtersCateg.ToList(),
            };
            return View(flv);
        }

        [HttpGet]
        public PartialViewResult CategorySearch(int? categoryId)
        {
            IQueryable<Filter> filtersCateg = _context.Filters.Include(p => p.Category);
            if (categoryId != null && categoryId != 0)
            {
                filtersCateg = filtersCateg.Where(p => p.CategoryId == categoryId);
            }
            FilterListView flv = new FilterListView
            {
                Filters = filtersCateg.ToList(),
            };
            return PartialView(flv);
        }

        [HttpGet]
        public ViewResult FilterSearch(int? filterId)
        {
            IQueryable<FilterAddititon> filterAddititonsFilter = _context.FilterAddititons.Include(p => p.Filter);
            if (filterId != null && filterId != 0)
            {
                filterAddititonsFilter = filterAddititonsFilter.Where(p => p.FilterId == filterId);
            }
            FilterAddititonListView falv = new FilterAddititonListView
            {
                FilterAddititons = filterAddititonsFilter.ToList(),
            };
            return View(falv);
        }

        // GET: Filters/Details/5
        public PartialViewResult Details(int? id)
        {
            Filter filter = _context.Filters.Find(id);
            return PartialView(filter);
        }

        // GET: Filters/Create
        public PartialViewResult Create()
        {
            SelectList categories = new SelectList(_context.Categories, "Id", "Title");
            ViewBag.Categories = categories;

            return PartialView();
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
        public PartialViewResult Edit(int? id)
        {
            SelectList categories = new SelectList(_context.Categories, "Id", "Title");
            ViewBag.Categories = categories;

            Filter filter = _context.Filters.Find(id);
            return PartialView(filter);
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
        public PartialViewResult Delete(int? id)
        {
            Filter filter = _context.Filters.Find(id);
            return PartialView(filter);
        }

        // POST: Filters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //удаление доп фильтров, привязаннх к фильтру
            List<FilterAddititon> filterAddititons = _context.FilterAddititons.ToList();
            foreach (var item in filterAddititons)
            {
                if (item.FilterId == id)
                {
                    var filterAddititon = await _context.FilterAddititons.FindAsync(item.Id);
                    _context.FilterAddititons.Remove(filterAddititon);
                    await _context.SaveChangesAsync();
                }
            }

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
