using AspNet_project.Data;
using AspNet_project.Models;
using Newtonsoft.Json;
using AspNet_project.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNet_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContext;

        public HomeController(AppDbContext context,
            IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }

        public async Task<IActionResult> Index()
        {

            return View(new HomeVM
            {

                Products =await _context.Products.Include(m=>m.ProductImages).ToListAsync(),
                ProductImages=await _context.ProductImages.ToListAsync(),
                Sliders = await _context.Sliders.OrderByDescending(m=>m.Id).ToListAsync(),
                SliderWords = await _context.SliderWords.FirstOrDefaultAsync(m=>m.IsMain),
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

        [HttpPost]
        public async Task<IActionResult> AddProductToBasket(int id)
        {
            List<BasketVM> basket;

            if (_httpContext.HttpContext.Request.Cookies["basket"] != null)
            {
                basket = JsonConvert.DeserializeObject<List<BasketVM>>(_httpContext.HttpContext.Request.Cookies["basket"]);
            }
            else
            {
                basket = new List<BasketVM>();
            }

            var existBasketData = basket.FirstOrDefault(m => m.ProductId == id);

            if (existBasketData is null)
            {
                basket.Add(new BasketVM
                {
                    ProductId = id,
                    ProductCount = 1
                });
            }
            else
            {
                existBasketData.ProductCount++;
            }

            _httpContext.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));

            return Ok(basket.Sum(m => m.ProductCount));
        }

    }
}
