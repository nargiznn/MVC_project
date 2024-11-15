using AspNet_project.Data;
using AspNet_project.Models;
using AspNet_project.Services.Interfaces;
using AspNet_project.ViewModels.Admin.Product;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet_project.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public ProductService(AppDbContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        public async Task<IEnumerable<ProductVM>> GetAllAsync()
        {
            var products = await _context.Products
                                          .Include(p => p.Category)
                                          .Include(p => p.ProductImages)
                                          .ToListAsync();

            return _mapper.Map<IEnumerable<ProductVM>>(products);
        }

        public async Task<SingleProductVM> GetByIdAsync(int id)
        {
            var product = await _context.Products
                                         .Include(p => p.ProductImages)
                                         .Include(p => p.Category)
                                         .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return null;

            return _mapper.Map<SingleProductVM>(product);
        }

        public async Task CreateAsync(ProductVM model, List<IFormFile> productPhotos)
        {
            var product = _mapper.Map<Product>(model);
            var productImages = new List<ProductImage>();

            if (productPhotos != null && productPhotos.Any())
            {
                foreach (var photo in productPhotos)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
                    string path = Path.Combine(_env.WebRootPath, "images", "products", fileName);
                    var directoryPath = Path.Combine(_env.WebRootPath, "images", "products");
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await photo.CopyToAsync(stream);
                    }
                    var productImage = new ProductImage
                    {
                        Image = fileName,
                        IsMain = false, 
                        Product = product
                    };

                    productImages.Add(productImage);
                }
                product.ProductImages = productImages;
            }
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductVM model, List<IFormFile> productPhotos)
        {
            var existingProduct = await _context.Products
                                                 .Include(p => p.ProductImages)
                                                 .FirstOrDefaultAsync(p => p.Id == model.Id);

            if (existingProduct == null)
                throw new InvalidOperationException("Product not found");

            _mapper.Map(model, existingProduct);

            if (productPhotos != null && productPhotos.Count > 0)
            {
                foreach (var photo in productPhotos)
                {
                    var imagePath = await SaveProductImage(photo);

                    var productImage = new ProductImage
                    {
                        Image = imagePath,
                        IsMain = false,
                        Product = existingProduct
                    };

                    existingProduct.ProductImages.Add(productImage);
                }
            }

            _context.Products.Update(existingProduct);
            await _context.SaveChangesAsync();
        }

        private async Task<string> SaveProductImage(IFormFile imageFile)
        {
            var filePath = Path.Combine("wwwroot", "img", "products", Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName));

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return filePath;
        }

        public async Task<IEnumerable<CategoryVM>> GetCategoriesAsync()
        {
            var categories = await _context.Categories
                                           .Select(c => new CategoryVM { Id = c.Id, Name = c.Name })
                                           .ToListAsync();

            return categories;
        }
    }
}
