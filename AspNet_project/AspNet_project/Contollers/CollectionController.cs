using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNet_project.Data;
using AspNet_project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspNet_project.Contollers
{
    public class CollectionController : Controller
    {
        // GET: /<controller>/
        private readonly AppDbContext _context;
        public CollectionController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string size, string priceRange)
        {
            var productsQuery = _context.Products
                .Include(m => m.ProductImages)
                .Include(m => m.ProductTypes)
                .Where(p => p.DiscountPrice > 0) 
                .AsQueryable();
            if (!string.IsNullOrEmpty(priceRange))
            {
                switch (priceRange.ToLower())
                {
                    case "under100":
                        productsQuery = productsQuery.Where(p => (p.Price < 100 || p.DiscountPrice < 100));
                        break;
                    case "100to300":
                        productsQuery = productsQuery.Where(p => (p.Price >= 100 && p.Price <= 300) || (p.DiscountPrice >= 100 && p.DiscountPrice <= 300));
                        break;
                    case "300to500":
                        productsQuery = productsQuery.Where(p => (p.Price >= 300 && p.Price <= 500) || (p.DiscountPrice >= 300 && p.DiscountPrice <= 500));
                        break;
                    case "500to1000":
                        productsQuery = productsQuery.Where(p => (p.Price >= 500 && p.Price <= 1000) || (p.DiscountPrice >= 500 && p.DiscountPrice <= 1000));
                        break;
                    case "above1000":
                        productsQuery = productsQuery.Where(p => p.Price > 1000 || p.DiscountPrice > 1000);
                        break;
                    default:
                        break;
                }
            }
            var products = await productsQuery.ToListAsync();


            return View(new CollectionVM
            {
                Products = products,
                ProductImages = await _context.ProductImages.ToListAsync(),
                TypeModels = await _context.TypeModels.ToListAsync(),
            });
        }



    }
}

