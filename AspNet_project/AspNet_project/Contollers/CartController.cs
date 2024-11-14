using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNet_project.Data;
using AspNet_project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspNet_project.Contollers
{
    public class CartController : Controller
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly AppDbContext _context;

        public CartController(IHttpContextAccessor httpContext,
                              AppDbContext context)
        {
            _httpContext = httpContext;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
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

            List<BasketDetailVM> basketDetails = new();

            foreach (var item in basket)
            {
                var product = await _context.Products.Include(m => m.ProductImages).FirstOrDefaultAsync(m => m.Id == item.ProductId);

                basketDetails.Add(new BasketDetailVM
                {
                    Id = product.Id,
                    ProductCount = item.ProductCount,
                    Title = product.Title,
                    Image = product.ProductImages.FirstOrDefault(m => m.IsMain).Image,
                    Total = (double)(item.ProductCount * product.Price),
                    Price=product.Price,
                    ProductId=product.Id

                });
            }

            return View(basketDetails);
        }
        [HttpPost]
        public IActionResult UpdateBasket(int productId, int quantity)
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
            var basketItem = basket.FirstOrDefault(x => x.ProductId == productId);
            if (basketItem != null)
            {
                if (quantity == 0) 
                {
                    basket.Remove(basketItem);
                }
                else
                {
                    basketItem.ProductCount = quantity;
                }
            }
            else if (quantity > 0) 
            {
                basket.Add(new BasketVM { ProductId = productId, ProductCount = quantity });
            }
            var serializedBasket = JsonConvert.SerializeObject(basket);
            _httpContext.HttpContext.Response.Cookies.Append("basket", serializedBasket, new CookieOptions { Expires = DateTimeOffset.Now.AddDays(30) });
            var basketCount = basket.Sum(x => x.ProductCount);
            var totalPrice = basket.Sum(x =>
            {
                var product = _context.Products.FirstOrDefault(p => p.Id == x.ProductId);
                return product != null ? x.ProductCount * product.Price : 0;
            });
            return Json(new { basketCount, totalPrice });
        }

    }
}

