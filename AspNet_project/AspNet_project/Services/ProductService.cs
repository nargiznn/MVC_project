using System;
using AspNet_project.Data;
using AspNet_project.Services.Interfaces;
using AspNet_project.ViewModels.Admin.Product;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AspNet_project.Services
{
	public class ProductService:IProductService
	{
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductService(AppDbContext context,
                           IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductVM>> GetAllAsync()
        {
            var works = await _context.Products.Include(m => m.Category)
                                            .Include(m => m.ProductImages)
                                            .ToListAsync();

            return _mapper.Map<IEnumerable<ProductVM>>(works);
        }

        public async Task<SingleProductVM> GetByIdAsync(int id)
        {
            var work = await _context.Products
                                      .Include(m => m.Category)
                                      .Include(m => m.ProductImages)
                                      .Include(m => m.ProductTypes)
                                      .FirstOrDefaultAsync(p => p.Id == id);

            return _mapper.Map<SingleProductVM>(work);
        }


    }
}

