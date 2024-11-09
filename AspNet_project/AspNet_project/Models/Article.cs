using System;
namespace AspNet_project.Models
{
	public class Article:BaseEntity
	{
		public string Desc { get; set; }
		public DateTime CreateDate { get; set; }
	}
}

