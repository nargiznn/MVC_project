using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNet_project.Data;
using AspNet_project.Models;
using AspNet_project.ViewModels.Admin.Slider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspNet_project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sliders.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (!id.HasValue) return BadRequest();

            Slider sliderImage = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id.Value);

            if (sliderImage == null) return NotFound();

            return View(sliderImage);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            foreach (var item in request.Photos)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + item.FileName;

                string path = Path.Combine(_env.WebRootPath, "assets/img", fileName);

                using (FileStream stream = new(path, FileMode.Create))
                {
                    await item.CopyToAsync(stream);
                }

                await _context.Sliders.AddAsync(new Slider { Image = fileName });

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var sliderImage = await _context.Sliders.FindAsync(id);

            string existPath = Path.Combine(_env.WebRootPath, "assets/img", sliderImage.Image);

            DeleteFile(existPath);

            _context.Sliders.Remove(sliderImage);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            Slider sliderImage = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);

            if (sliderImage is null) return NotFound();

            return View(new SliderImageEditVM { Image = sliderImage.Image, Id = sliderImage.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, SliderImageEditVM request)
        {
            if (id is null) return BadRequest();

            Slider sliderImage = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);

            if (sliderImage is null) return NotFound();

            if (request.Photo != null)
            {
                string existPath = Path.Combine(_env.WebRootPath, "assets/img", sliderImage.Image);
                DeleteFile(existPath);

                string newFileName = Guid.NewGuid().ToString() + "_" + request.Photo.FileName;
                string newPath = Path.Combine(_env.WebRootPath, "assets/img", newFileName);

                using (FileStream stream = new(newPath, FileMode.Create))
                {
                    await request.Photo.CopyToAsync(stream);
                }

                sliderImage.Image = newFileName;

                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private void DeleteFile(string path)
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }

    }
}

