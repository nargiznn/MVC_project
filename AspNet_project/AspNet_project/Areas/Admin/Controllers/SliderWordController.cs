using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNet_project.Data;
using AspNet_project.Models;
using AspNet_project.Services.Interfaces;
using AspNet_project.ViewModels.Admin.Slider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspNet_project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderWordController : Controller
    {
        private readonly AppDbContext _context;

        public SliderWordController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<SliderWords> sliderWords = await _context.SliderWords.OrderByDescending(m => m.Id).ToListAsync();
            return View(sliderWords);
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();

            SliderWords sliderWord = await _context.SliderWords.FirstOrDefaultAsync(m => m.Id == id);
            if (sliderWord == null) return NotFound();

            return View(sliderWord);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderWords sliderWord)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            bool hasSliderWord = await _context.SliderWords.AnyAsync(m => m.Title.Trim() == sliderWord.Title.Trim());
            if (hasSliderWord)
            {
                ModelState.AddModelError("Title", "A slider word with this title already exists.");
                return View();
            }

            await _context.SliderWords.AddAsync(new SliderWords
            {
                Title = sliderWord.Title,
                MainText = sliderWord.MainText,
                Subtitle = sliderWord.Subtitle
            });
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();

            SliderWords sliderWord = await _context.SliderWords.FirstOrDefaultAsync(m => m.Id == id);
            if (sliderWord == null) return NotFound();

            return View(sliderWord);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, SliderWords sliderWord)
        {
            if (id == null) return BadRequest();
            SliderWords existSliderWord = await _context.SliderWords.FirstOrDefaultAsync(m => m.Id == id);

            if (existSliderWord == null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(existSliderWord); 
            }

            bool hasSliderWord = await _context.SliderWords
                                                .AnyAsync(m => m.Title.Trim() == sliderWord.Title.Trim() && m.Id != id);

            if (hasSliderWord)
            {

                ModelState.AddModelError("Title", "A slider word with this title already exists.");
                return View(existSliderWord); 
            }

            existSliderWord.Title = sliderWord.Title;
            existSliderWord.MainText = sliderWord.MainText;
            existSliderWord.Subtitle = sliderWord.Subtitle;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));  
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var sliderWord = await _context.SliderWords.FirstOrDefaultAsync(m => m.Id == id);
            if (sliderWord == null)
            {
                return NotFound();
            }

            try
            {
                _context.SliderWords.Remove(sliderWord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["Error"] = "An error occurred while deleting the slider word.";
                return RedirectToAction(nameof(Index));
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeMainStatus(int id)
        {
            SliderWords sliderWords = await _context.SliderWords.FirstOrDefaultAsync(m => m.Id == id);
            sliderWords.IsMain = true;
            var updatedSliderImageStatus = await _context.SliderWords.FirstOrDefaultAsync(m => m.Id != id && m.IsMain);
            updatedSliderImageStatus.IsMain = false;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}

