using System;
namespace AspNet_project.Models
{
	public class Category:BaseEntity
	{
		public string Name { get; set; }
		public string Icon { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}

