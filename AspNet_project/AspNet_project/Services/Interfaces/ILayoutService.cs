using System;
namespace AspNet_project.Services.Interfaces
{
	public interface ILayoutService
	{
		Task<Dictionary<string, string>> GetAllSettingAsync();
	}
}

