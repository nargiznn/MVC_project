using AspNet_project.Data;
using AspNet_project.Models;
using AspNet_project.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNet_project.Contollers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;

        }
        public async Task<IActionResult> Index()
        {
            return View(new HomeVM
            {
                Sliders = await _context.Sliders.ToListAsync(),
                SliderWords = await _context.SliderWords.OrderByDescending(sw => sw.Id).FirstOrDefaultAsync(),
                Adverts = await _context.Adverts.OrderByDescending(mn => mn.Id).Take(2).ToListAsync(),
                Brands = await _context.Brands.OrderByDescending(mn => mn.Id).ToListAsync(),
            });
        }
    }
}
