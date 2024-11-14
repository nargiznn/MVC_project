using System;
using AspNet_project.Models;

namespace AspNet_project.ViewModels.Admin.Product
{
	public class ProductVM:BaseEntity
	{
        public int Id { get; set; }
        public string Title { get; set; }
        public string Info { get; set; }
        public string MoreInfo { get; set; }
        public double Price { get; set; }
        public int SalesCount { get; set; }
        public double DiscountPrice { get; set; }
        public string CategoryName { get; set; }
        public string MainImage { get; set; }
    }
}

