using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNet_project.Helpers.Enums;
using AspNet_project.Models;
using AspNet_project.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspNet_project.Areas.Admin.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class AccountController : MainController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<AppUser> userManager,
                                 SignInManager<AppUser> signInManager,
                                 RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();

            List<UserVM> usersVM = new List<UserVM>();

            foreach (var user in users)
            {

                usersVM.Add(new UserVM
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                });
            }

            return View(usersVM);
        }

        [HttpGet]
        public async Task<IActionResult> AddRoleToUser()
        {
            ViewBag.users = await GetUsersAsync();
            ViewBag.roles = await GetRolesAsync();

            return View();
        }

        private async Task<SelectList> GetRolesAsync()
        {
            List<IdentityRole> roles = await _roleManager.Roles.ToListAsync();

            return new SelectList(roles, "Id", "Name");
        }

        private async Task<SelectList> GetUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();

            return new SelectList(users, "Id", "UserName");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRoleToUser(UserRoleVM request)
        {
            AppUser user = await _userManager.FindByIdAsync(request.UserId);
            IdentityRole role = await _roleManager.FindByIdAsync(request.RoleId);

            await _userManager.AddToRoleAsync(user, role.Name);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> RemoveRoleFromUser()
        {
            ViewBag.roles = await GetRolesAsync();
            ViewBag.users = await GetUsersAsync();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveRoleFromUser(UserRoleVM request)
        {
            AppUser user = await _userManager.FindByIdAsync(request.UserId);
            IdentityRole role = await _roleManager.FindByIdAsync(request.RoleId);

            await _userManager.RemoveFromRoleAsync(user, role.Name);
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home", new { Area = string.Empty });
        }
    }
}

