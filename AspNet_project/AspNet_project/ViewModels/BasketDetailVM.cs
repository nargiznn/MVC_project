using System;
using AspNet_project.Models;

namespace AspNet_project.ViewModels
{
	public class BasketDetailVM:BaseEntity
	{
        public string Title { get; set; }
        public string Image { get; set; }
        public int ProductCount { get; set; }
        public decimal Total { get; set; }
    }
}

