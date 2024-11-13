using System;
namespace AspNet_project.Models
{
	public class Product:BaseEntity
	{
        public string Title { get; set; }
        public string Info { get; set; }
        public string MoreInfo { get; set; }
        public double Price { get; set; }
        public int SalesCount { get; set; }
        public double DiscountPrice { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public List<ProductType> ProductTypes { get; set; }

    }
}

