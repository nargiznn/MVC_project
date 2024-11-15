using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNet_project.Services.Interfaces;
using AspNet_project.ViewModels.Admin.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using AspNet_project.Helpers.Enums;
using Microsoft.AspNetCore.Authorization;

namespace AspNet_project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
                return BadRequest();

            var product = await _productService.GetByIdAsync((int)id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new ProductVM();
            ViewBag.Categories = await _productService.GetCategoriesAsync();

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductVM model, List<IFormFile> productPhotos)
        {
            if (ModelState.IsValid)
            {
                if (model.CategoryId == 0)
                {
                    ModelState.AddModelError("CategoryId", "Category is required");
                    ViewBag.Categories = await _productService.GetCategoriesAsync();
                    return View(model);
                }
                await _productService.CreateAsync(model, productPhotos);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = await _productService.GetCategoriesAsync();
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            ViewBag.Categories = await _productService.GetCategoriesAsync();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductVM model, List<IFormFile> productPhotos)
        {
            if (id != model.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                await _productService.UpdateAsync(model, productPhotos);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = await _productService.GetCategoriesAsync();
            return View(model);
        }

    }
}
