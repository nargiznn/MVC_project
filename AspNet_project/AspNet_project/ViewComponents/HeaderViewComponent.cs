using System;
using AspNet_project.Data;
using AspNet_project.Models;
using AspNet_project.Services.Interfaces;
using AspNet_project.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNet_project.ViewComponents
{
	public class HeaderViewComponent:ViewComponent
	{
        private readonly ILayoutService _layoutService;
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;
        public HeaderViewComponent(ILayoutService layoutService, UserManager<AppUser> userManager, AppDbContext context)
        {
            _layoutService = layoutService;
            _userManager = userManager;
            _context = context;
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
            };
            return View(headerVM);
        }

    }
}

