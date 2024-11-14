using System;
using AspNet_project.Models;

namespace AspNet_project.ViewModels
{
	public class BasketDetailVM:BaseEntity
	{
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public int ProductCount { get; set; }
        public double Total { get; set; }
        public double Price { get; set; }
    }
}

