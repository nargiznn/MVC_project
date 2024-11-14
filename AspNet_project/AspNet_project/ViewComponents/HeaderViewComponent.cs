using System;
using AspNet_project.Data;
using AspNet_project.Models;
using AspNet_project.Services.Interfaces;
using AspNet_project.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AspNet_project.ViewComponents
{
	public class HeaderViewComponent:ViewComponent
	{
        private readonly ILayoutService _layoutService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContext;
        private readonly AppDbContext _context;
        public HeaderViewComponent(ILayoutService layoutService,
                                   UserManager<AppUser> userManager,
                                   AppDbContext context,
                                   IHttpContextAccessor httpContext)
        {
            _layoutService = layoutService;
            _userManager = userManager;
            _context = context;
            _httpContext = httpContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var settings = await _layoutService.GetAllSettingAsync();
            string fullName = string.Empty;

            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (user != null)
                {
                    fullName = user.FullName;
                }
            }
            var categories = await _context.Categories.ToListAsync();
            if (categories == null || !categories.Any())
            {
                Console.WriteLine("No categories found in the database.");
            }
            var headerVM = new HeaderVM
            {
                Settings = settings,
                FullName = fullName,
                Categories = categories,
                BasketCount = GetBasketCount()
            };
            return View(headerVM);
        }
        private int GetBasketCount()
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

            return basket.Sum(m => m.ProductCount);
        }

    }
}

