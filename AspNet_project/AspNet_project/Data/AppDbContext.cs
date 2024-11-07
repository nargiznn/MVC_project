using AspNet_project.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspNet_project.Data
{
    public class AppDbContext:DbContext
    {
        //IdentityDbContext<AppUser>
        //public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }
      
    }
}
