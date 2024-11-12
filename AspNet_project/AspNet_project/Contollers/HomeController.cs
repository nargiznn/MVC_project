using AspNet_project.Data;
using AspNet_project.Models;
using AspNet_project.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNet_project.Controllers
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
                Products =await _context.Products.Include(m=>m.ProductImages).ToListAsync(),
                ProductImages=await _context.ProductImages.ToListAsync(),
                Sliders = await _context.Sliders.ToListAsync(),
                SliderWords = await _context.SliderWords.OrderByDescending(sw => sw.Id).FirstOrDefaultAsync(),
                Adverts = await _context.Adverts.OrderByDescending(mn => mn.Id).ToListAsync(),
                News = await _context.News.ToListAsync(),
                Testimonials = await _context.Testimonials.ToListAsync(),
                Brands = await _context.Brands.OrderByDescending(mn => mn.Id).ToListAsync(),
                Accessories = await _context.Accessories
                    .Where(a => a.AccessoryCategories.Any(ac => ac.AccessoryId == a.Id)) 
                    .Include(a => a.AccessoryCategories)
                    .ToListAsync()
            });;
        }

    }
}
