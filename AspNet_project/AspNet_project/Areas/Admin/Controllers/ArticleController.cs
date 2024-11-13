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
    public class ArticleController : Controller
    {
        private readonly AppDbContext _context;

        public ArticleController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Article> articles = await _context.Articles.OrderByDescending(m => m.Id).ToListAsync();
            return View(articles);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            Article article = await _context.Articles.FirstOrDefaultAsync(m => m.Id == id);

            if (article is null) return NotFound();

            return View(article);
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
                ModelState.AddModelError("Desc", "Description already exists.");
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
            Article existArticle = await _context.Articles.FirstOrDefaultAsync(m => m.Id == id);
            _context.Articles.Remove(existArticle);
            await _context.SaveChangesAsync();
            return Ok(id);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            Article article = await _context.Articles.FirstOrDefaultAsync(m => m.Id == id);

            if (article is null) return NotFound();

            return View(article);
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

            bool hasArticle = await _context.Categories.AnyAsync(m => m.Name.Trim() == article.Desc.Trim() && m.Id != id);

            if (hasArticle)
            {
                ModelState.AddModelError("Desc", "Desc already exist");
                return View();
            }

            existArticle.Desc = article.Desc;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

