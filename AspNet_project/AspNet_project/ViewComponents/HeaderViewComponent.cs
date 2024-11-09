using System;
using AspNet_project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AspNet_project.ViewComponents
{
	public class HeaderViewComponent:ViewComponent
	{
		private readonly ILayoutService _layoutService;
		public HeaderViewComponent(ILayoutService layoutService)
		{
			_layoutService = layoutService;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			return await Task.FromResult(View(await _layoutService.GetAllSettingAsync()));
		}
	}
}

