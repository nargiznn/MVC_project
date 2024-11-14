using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNet_project.Data;
using AspNet_project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspNet_project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Category> categories = await _context.Categories.OrderByDescending(m => m.Id).ToListAsync();
            return View(categories);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            Category category = await _context.Categories
                                              .Include(c => c.Products) 
                                              .FirstOrDefaultAsync(m => m.Id == id);

            if (category is null) return NotFound();

            return View(category);
          
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            bool hasCategory = await _context.Categories.AnyAsync(m => m.Name.Trim() == category.Name.Trim());
            if (hasCategory)
            {
                ModelState.AddModelError("Name", "Category already exist");
                return View();
            }

            await _context.Categories.AddAsync(new Category { Name = category.Name });
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            var productsToUpdate = await _context.Products
                                                  .Where(p => p.CategoryId == id)
                                                  .ToListAsync();

            foreach (var product in productsToUpdate)
            {
                product.CategoryId = null; 
            }

            try
            {
                _context.Products.UpdateRange(productsToUpdate);
                await _context.SaveChangesAsync();
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while deleting the category.";
                return RedirectToAction(nameof(Index));
            }
        }




        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            Category category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);

            if (category is null) return NotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Category category)
        {
            if (id is null) return BadRequest();

            Category existCategory = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);

            if (existCategory is null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View();
            }

            bool hasCategory = await _context.Categories.AnyAsync(m => m.Name.Trim() == category.Name.Trim() && m.Id != id);

            if (hasCategory)
            {
                ModelState.AddModelError("Name", "Category already exist");
                return View();
            }

            existCategory.Name = category.Name;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}

