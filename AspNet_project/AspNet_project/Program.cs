using AspNet_project.Data;
using AspNet_project.Models;
using AspNet_project.Services;
using AspNet_project.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
//builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddDbContext<AppDbContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddIdentity<AppUser,IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options =>
 {
     options.Password.RequireDigit = true;
     options.Password.RequireLowercase = true;
     options.Password.RequireNonAlphanumeric = true;
     options.Password.RequireUppercase = true;
     options.Password.RequiredLength = 5;

     options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
     options.Lockout.MaxFailedAccessAttempts = 5;
     options.Lockout.AllowedForNewUsers = true;
     options.User.RequireUniqueEmail = true;
 });

builder.Services.AddScoped<ILayoutService, LayoutService>();
var app = builder.Build();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();
app.MapControllerRoute(
    name: "Admin",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
