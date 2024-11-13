using System;
using AspNet_project.Models;

namespace AspNet_project.ViewModels
{
	public class CollectionVM
	{
        public List<Product> Products { get; set; }
        public List<TypeModel> TypeModels { get; set; }
        public List<ProductImage> ProductImages { get; set; }

    }
}

