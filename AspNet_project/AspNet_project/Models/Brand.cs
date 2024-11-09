using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNet_project.Models
{
	public class Brand:BaseEntity
	{
        public string Name { get; set; }
        public string Image { get; set; }
        [NotMapped]
        [Required]
        public IFormFile BrandPhoto { get; set; }
    }
}

