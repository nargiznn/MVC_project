using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNet_project.Models
{
	public class ProductType:BaseEntity
	{
        public int TypeModelId { get; set; }
        public TypeModel TypeModel { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}

