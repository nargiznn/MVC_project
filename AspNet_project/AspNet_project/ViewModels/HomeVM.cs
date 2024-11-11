using System;
using AspNet_project.Models;

namespace AspNet_project.ViewModels
{
	public class HomeVM
	{
        public List<Slider> Sliders { get; set; }
        public SliderWords SliderWords { get; set; }
        public List<Advert> Adverts { get; set; }
        public List<Brand> Brands { get; set; }
        public List<News> News { get; set; }
        public List<Testimonial> Testimonials { get; set; }
        public List<Accessory> Accessories { get; set; }
        public List<AccessoryCategory> AccessoryCategories { get; set; }
    }
}

