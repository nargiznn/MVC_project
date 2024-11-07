using AspNet_project.Data;
using AspNet_project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNet_project.Contollers
{
    public class HomeController : Controller
    {
        //private readonly AppDbContext _context;

        //public HomeController(AppDbContext context)
        //{
        //    _context = context;

        //}
        public IActionResult Index()
        {
            return View();
        }
    }
}
