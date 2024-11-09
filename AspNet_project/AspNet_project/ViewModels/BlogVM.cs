using System;
using AspNet_project.Models;

namespace AspNet_project.ViewModels
{
	public class BlogVM
	{
        public List<Category> Categories { get; set; }
        public List<Article> Articles { get; set; }
        public List<News> News { get; set; }
    }
}

