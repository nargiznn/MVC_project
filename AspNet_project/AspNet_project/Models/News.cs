using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNet_project.Models
{
	public class News:BaseEntity
	{
        public string Image { get; set; }
        public string Title { get; set; }
        public string PersonName { get; set; }
        public DateTime CreateDate { get; set; }
        public string Desc { get; set; }

        [NotMapped]
        [Required]
        public IFormFile NewsPhoto { get; set; }
    }
}

