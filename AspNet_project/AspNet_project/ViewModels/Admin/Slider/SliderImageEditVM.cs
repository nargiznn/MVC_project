using System;
namespace AspNet_project.ViewModels.Admin.Slider
{
	public class SliderImageEditVM
	{
        public int Id { get; set; }
        public IFormFile Photo { get; set; }
        public string Image { get; set; }
    }
}

