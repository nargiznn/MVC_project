using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNet_project.Models
{
	public class Slider:BaseEntity
	{
        public string Image { get; set; }
        [NotMapped]
        [Required]
        public List<IFormFile> Photos { get; set; }
    }
}

