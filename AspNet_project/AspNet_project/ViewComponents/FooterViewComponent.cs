using System;
using AspNet_project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AspNet_project.ViewComponents
{
	public class FooterViewComponent: ViewComponent
    {
        private readonly ILayoutService _layoutService;
        public FooterViewComponent(ILayoutService layoutService)
        {
            _layoutService = layoutService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View(await _layoutService.GetAllSettingAsync()));
        }
    }
}

