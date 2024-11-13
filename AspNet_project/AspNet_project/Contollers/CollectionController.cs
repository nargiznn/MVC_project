using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNet_project.Data;
using AspNet_project.Models;
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

        public async Task<IActionResult> Index(string size, string priceRange, string sortBy, int? productTypeId, int page = 1, int pageSize = 6)
        {
            var productsQuery = _context.Products
                .Include(m => m.ProductImages)
                .Include(m => m.ProductTypes)
                .AsQueryable();
            if (!string.IsNullOrEmpty(priceRange))
            {
                switch (priceRange.ToLower())
                {
                    case "under100":
                        productsQuery = productsQuery.Where(p => ((p.Price < 100 || p.DiscountPrice < 100 )) && (p.DiscountPrice !=0));
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
                if (productTypeId.HasValue)
    {
        productsQuery = productsQuery.Where(p => p.ProductTypes.Any(t => t.Id == productTypeId.Value));
    }
            switch (sortBy)
            {
                case "alphabetically-az":
                    productsQuery = productsQuery.OrderBy(p => p.Title);
                    break;
                case "alphabetically-za":
                    productsQuery = productsQuery.OrderByDescending(p => p.Title);
                    break;
                case "price-low-to-high":
                    productsQuery = productsQuery.OrderBy(p => p.Price);
                    break;
                case "price-high-to-low":
                    productsQuery = productsQuery.OrderByDescending(p => p.Price);
                    break;
                case "best-selling":
                    productsQuery = productsQuery.OrderByDescending(p => p.SalesCount);
                    break;
                default:
                    productsQuery = productsQuery.OrderBy(p => p.Title); 
                    break;
            }

            var paginatedProducts = await PaginatedList<Product>.CreateAsync(productsQuery, page, pageSize);


            return View(new CollectionVM
            {
                Products = paginatedProducts,
                ProductImages = await _context.ProductImages.ToListAsync(),
                TypeModels = await _context.TypeModels.ToListAsync(),
                PriceRange = priceRange,
            });
        }



    }
}

