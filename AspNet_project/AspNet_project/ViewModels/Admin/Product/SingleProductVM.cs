using System;
using AspNet_project.Models;

namespace AspNet_project.ViewModels.Admin.Product
{
	public class SingleProductVM
	{
        public int Id { get; set; }
        public string Title { get; set; }
        public string CategoryName { get; set; }
        public string Info { get; set; }
        public string MoreInfo { get; set; }
        public double Price { get; set; }
        public double DiscountPrice { get; set; }
        public int SalesCount { get; set; }
        public List<ProductImageVM> Images { get; set; }

    }
}

