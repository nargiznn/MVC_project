using System;
using AspNet_project.ViewModels.Admin.Product;

namespace AspNet_project.Services.Interfaces
{
	public interface IProductService
	{
        Task<IEnumerable<ProductVM>> GetAllAsync();
        Task<SingleProductVM> GetByIdAsync(int id);
    }
}

