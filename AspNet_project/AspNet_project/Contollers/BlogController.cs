using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNet_project.Data;
using AspNet_project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspNet_project.Contollers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        public BlogController(AppDbContext context)
        {
            _context = context;

        }
        public async Task<IActionResult> Index()
        {
            return View(new BlogVM
            {
                Categories = await _context.Categories.ToListAsync(),
                Articles = await _context.Articles.OrderByDescending(ar => ar.CreateDate).Take(3).ToListAsync(),
                News =await _context.News.OrderByDescending(or=>or.Id).Take(3).ToListAsync(),
            });
        }
    }
}

