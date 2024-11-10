using System;
namespace AspNet_project.Models
{
	public class Accessory:BaseEntity
	{
		public string Name { get; set; }
        public string Image { get; set; }
		public ICollection<AccessoryCategory> AccessoryCategories { get; set; }

    }
}

