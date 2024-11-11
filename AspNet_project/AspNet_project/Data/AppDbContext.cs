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
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Accessory> Accessories { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<AccessoryCategory> AccessoryCategories { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> option) : base(option) { }

    }
}
