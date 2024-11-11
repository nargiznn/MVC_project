using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNet_project.Models
{
	public class Testimonial:BaseEntity
	{
        public string Title { get; set; }
        public string Person { get; set; }
        public string Job { get; set; }
        public string Image { get; set; }
        [NotMapped]
        [Required]
        public IFormFile TestPhoto { get; set; }
    }
}

