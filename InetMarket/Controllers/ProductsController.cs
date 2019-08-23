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
    public class ProductsController : Controller
    {
        private readonly MarketContext _context;

        public ProductsController(MarketContext context)
        {
            _context = context;
        }
        [AllowAnonymous]
        public IActionResult Index(int? categoryId)
        {
            IQueryable<Product> productsCateg = _context.Products.Include(p => p.Category);
            if (categoryId != null && categoryId != 0)
            {
                productsCateg = productsCateg.Where(p => p.CategoryId == categoryId);
            }
            List<Category> categories = _context.Categories.ToList();
            List<Brand> brands = _context.Brands.ToList();
            List<Provider> providers = _context.Providers.ToList();
            // устанавливаем начальный элемент, который позволит выбрать всех
            categories.Insert(0, new Category { Title = "All", Id = 0 });
            ProductListView plv = new ProductListView
            {
                Categories = new SelectList(categories, "Id", "Title"),
                Products = productsCateg.ToList(),
            };
            return View(plv);
        }

        [HttpGet]
        public PartialViewResult CategorySearch(int? categoryId)
        {
            IQueryable<Product> productsCateg = _context.Products.Include(p => p.Category);
            if (categoryId != null && categoryId != 0)
            {
                productsCateg = productsCateg.Where(p => p.CategoryId == categoryId);
            }
            List<Category> categories = _context.Categories.ToList();
            List<Brand> brands = _context.Brands.ToList();
            List<Provider> providers = _context.Providers.ToList();
            ProductListView plv = new ProductListView
            {
                Products = productsCateg.ToList(),
            };
            return PartialView(plv);
        }

        // GET: Products/Details/5
        public PartialViewResult Details(int? id)
        {
            List<Category> categories = _context.Categories.ToList();
            List<Brand> brands = _context.Brands.ToList();
            List<Provider> providers = _context.Providers.ToList();
            Product product = _context.Products.Find(id);
            return PartialView(product);
        }

        // GET: Products/Create
        public PartialViewResult Create()
        {
            SelectList categories = new SelectList(_context.Categories, "Id", "Title");
            ViewBag.Categories = categories;

            SelectList brands = new SelectList(_context.Brands, "Id", "Title");
            ViewBag.Brands = brands;

            SelectList providers = new SelectList(_context.Providers, "Id", "Title");
            ViewBag.Providers = providers;

            return PartialView();
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
        public PartialViewResult Edit(int? id)
        {
            SelectList categories = new SelectList(_context.Categories, "Id", "Title");
            ViewBag.Categories = categories;

            SelectList brands = new SelectList(_context.Brands, "Id", "Title");
            ViewBag.Brands = brands;

            SelectList providers = new SelectList(_context.Providers, "Id", "Title");
            ViewBag.Providers = providers;

            Product product = _context.Products.Find(id);
            return PartialView(product);
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
        public PartialViewResult Delete(int? id)
        {
            Product product = _context.Products.Find(id);
            return PartialView(product);
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

        public async Task<IActionResult> EditFilters(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productsModel = await _context.Products.FindAsync(id);
            List<Filter> filters = await _context.Filters.ToListAsync();
            List<FilterAddititon> filterAddititons = await _context.FilterAddititons.ToListAsync();
            List<Value> filterValues = await _context.Values.Where(p => p.ProductId == id).ToListAsync();
            if (filterValues.Count != 0)
            {
                foreach (FilterAddititon fa in filterAddititons)
                {
                    if (filterValues.FirstOrDefault(fv => fv.FilterAddId == fa.Id) != null)
                    {
                        fa.IsChecked = true;
                    }
                    fa.Filter = filters.Find(f => f.Id == fa.FilterId);
                }
            }
            filterAddititons = filterAddititons.OrderBy(f => f.FilterId).ToList();
            ProductExtViewModel tour = new ProductExtViewModel
            {
                Id = productsModel.Id,
                Title = productsModel.Title,
                FilterAddititons = filterAddititons
            };

            if (productsModel == null)
            {
                return NotFound();
            }
            return View(tour);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFilters(int id, [Bind("Id, FilterAddititons")] ProductExtViewModel product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            List<Value> oldFilterValues = await _context.Values.Where(p => p.ProductId == id).ToListAsync();
            if (oldFilterValues.Count != 0)
            {
                foreach (var item in product.FilterAddititons)
                {
                    if (item.IsChecked)
                    {
                        if (oldFilterValues.FirstOrDefault(fv => fv.FilterAddId == item.Id && fv.ProductId == product.Id) == null)
                            _context.Update(new Value { FilterAddId = item.Id, ProductId = product.Id });
                    }
                    else
                    {
                        if (oldFilterValues.FirstOrDefault(fv => fv.FilterAddId == item.Id && fv.ProductId == product.Id) != null)
                            _context.Remove(oldFilterValues.First(fv => fv.FilterAddId == item.Id && fv.ProductId == product.Id));
                    }
                }
            }
            else
            {
                foreach (var item in product.FilterAddititons)
                    if (item.IsChecked)
                        _context.Update(new Value { FilterAddId = item.Id, ProductId = product.Id });
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
