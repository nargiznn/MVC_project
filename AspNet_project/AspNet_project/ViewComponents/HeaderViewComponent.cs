using System;
using AspNet_project.Models;
using AspNet_project.Services.Interfaces;
using AspNet_project.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNet_project.ViewComponents
{
	public class HeaderViewComponent:ViewComponent
	{
        private readonly ILayoutService _layoutService;
        private readonly UserManager<AppUser> _userManager;

        public HeaderViewComponent(ILayoutService layoutService, UserManager<AppUser> userManager)
        {
            _layoutService = layoutService;
            _userManager = userManager;
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
            var headerVM = new HeaderVM
            {
                Settings = settings,
                FullName = fullName
            };
            return View(headerVM);
        }

    }
}

