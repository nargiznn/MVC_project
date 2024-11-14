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
    public class SettingController : Controller
    {
        private readonly AppDbContext _context;

        public SettingController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Setting> settings = await _context.Settings.OrderByDescending(m => m.Id).ToListAsync();
            return View(settings);
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            Setting setting = await _context.Settings.FirstOrDefaultAsync(m => m.Id == id);

            if (setting is null) return NotFound();

            return View(setting);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Setting setting)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            bool hasSetting = await _context.Settings.AnyAsync(m => m.Key.Trim() == setting.Key.Trim());
            if (hasSetting)
            {
                ModelState.AddModelError("Key", "Setting with this key already exists");
                return View();
            }

            await _context.Settings.AddAsync(new Setting { Key = setting.Key, Value = setting.Value });
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var setting = await _context.Settings.FirstOrDefaultAsync(m => m.Id == id);
            if (setting == null)
            {
                return NotFound();
            }

            try
            {
                _context.Settings.Remove(setting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while deleting the setting.";
                return RedirectToAction(nameof(Index));
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            Setting setting = await _context.Settings.FirstOrDefaultAsync(m => m.Id == id);

            if (setting is null) return NotFound();

            return View(setting);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Setting setting)
        {
            if (id is null) return BadRequest();

            Setting existSetting = await _context.Settings.FirstOrDefaultAsync(m => m.Id == id);

            if (existSetting is null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View();
            }

            existSetting.Value = setting.Value;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}

