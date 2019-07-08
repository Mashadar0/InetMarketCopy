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
    public class ProductsController : Controller
    {
        private readonly MarketContext _context;

        public ProductsController(MarketContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? categoryId, int? filterId)
        {
            var prodsCateg = _context.Products.Include(p => p.Category);
            var prodsBrand = _context.Products.Include(p => p.Brand);
            var prodsProvid = _context.Products.Include(p => p.Provider);
            IQueryable<Product> productsCateg = _context.Products.Include(p => p.Category);
            if (categoryId != null && categoryId != 0)
            {
                productsCateg = productsCateg.Where(p => p.CategoryId == categoryId);
            }
            List<Category> categories = _context.Categories.ToList();
            List<Brand> brands = _context.Brands.ToList();
            List<Provider> providers = _context.Providers.ToList();
            List<Filter> filters = _context.Filters.ToList();
            // устанавливаем начальный элемент, который позволит выбрать всех
            categories.Insert(0, new Category { Title = "All", Id = 0 });
            filters.Insert(0, new Filter { Title = "All", Id = 0 });
            ProductListView plv = new ProductListView
            {
                Products = productsCateg.ToList(),
                Categories = new SelectList(categories, "Id", "Title"),
                Filters = new SelectList(filters, "Id", "Title"),
            };
            return View(plv);
        }

        [HttpPost]
        public ActionResult CategorySearch(string title)
        {
            var allProducts = _context.Products.Where(a => a.Category.Title == title).ToList();
            return PartialView(allProducts);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var prodsCateg = _context.Products.Include(p => p.Category);
            var prodsBrand = _context.Products.Include(p => p.Brand);
            var prodsProvid = _context.Products.Include(p => p.Provider);
            List<Category> categories = _context.Categories.ToList();
            List<Brand> brands = _context.Brands.ToList();
            List<Provider> providers = _context.Providers.ToList();
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            SelectList categories = new SelectList(_context.Categories, "Id", "Title");
            ViewBag.Categories = categories;

            SelectList brands = new SelectList(_context.Brands, "Id", "Title");
            ViewBag.Brands = brands;

            SelectList providers = new SelectList(_context.Providers, "Id", "Title");
            ViewBag.Providers = providers;

            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,MainImage,IsDiscount,IsMane,CategoryId,Price,BrandId,ProviderId")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            SelectList categories = new SelectList(_context.Categories, "Id", "Title");
            ViewBag.Categories = categories;

            SelectList brands = new SelectList(_context.Brands, "Id", "Title");
            ViewBag.Brands = brands;

            SelectList providers = new SelectList(_context.Providers, "Id", "Title");
            ViewBag.Providers = providers;

            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,MainImage,IsDiscount,IsMane,CategoryId,Price,BrandId,ProviderId")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
