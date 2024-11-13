using System;
using AspNet_project.Models;

namespace AspNet_project.ViewModels
{
    public class CollectionVM
    {
        public PaginatedList<Product> Products { get; set; }
        public List<TypeModel> TypeModels { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public string PriceRange { get; set; }


      
        
    }
}

