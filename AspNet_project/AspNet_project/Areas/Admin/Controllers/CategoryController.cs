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

            Category category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);

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
        public async Task<IActionResult> Create(Article article)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            bool hasArticle = await _context.Articles.AnyAsync(m => m.Desc.Trim() == article.Desc.Trim());

            if (hasArticle)
            {
                ModelState.AddModelError("Desc", "Desc already exist");
                return View();
            }
            article.CreateDate = DateTime.Now;

            await _context.Articles.AddAsync(article);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            Category existProduct = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
            _context.Categories.Remove(existProduct);
            await _context.SaveChangesAsync();
            return Ok(id);
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
        public async Task<IActionResult> Edit(int? id, Article article)
        {
            if (id is null) return BadRequest();

            Article existArticle = await _context.Articles.FirstOrDefaultAsync(m => m.Id == id);

            if (existArticle is null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View();
            }

            bool hasArticle = await _context.Articles.AnyAsync(m => m.Desc.Trim() == article.Desc.Trim() && m.Id != id);

            if (hasArticle)
            {
                ModelState.AddModelError("Desc", "Desc already exists");
                return View();
            }
            existArticle.Desc = article.Desc;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}

