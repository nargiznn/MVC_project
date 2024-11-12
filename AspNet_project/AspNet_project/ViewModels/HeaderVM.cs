using System;
using AspNet_project.Models;

namespace AspNet_project.ViewModels
{
	public class HeaderVM
	{
        public string FullName { get; set; }
        public List<Category> Categories { get; set; }
        public Dictionary<string, string> Settings { get; set; }
    }
}

