using System;
namespace AspNet_project.Models
{
	public class AccessoryCategory:BaseEntity
	{
		public string Name { get; set; }
		public int? AccessoryId { get; set; }
		public Accessory Accessory { get; set; }

    }
}

