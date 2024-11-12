using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNet_project.Models
{
	public class ProductImage:BaseEntity
	{
        public string Image { get; set; }
        public bool IsMain { get; set; } = false;
        [NotMapped]
        [Required]
        public List<IFormFile> ProductPhotos { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}

