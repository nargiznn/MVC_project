using AspNet_project.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspNet_project.Data
{
    public class AppDbContext: DbContext
    {
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<SliderWords> SliderWords { get; set; }
        public DbSet<Advert> Adverts { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> option) : base(option) { }

    }
}
