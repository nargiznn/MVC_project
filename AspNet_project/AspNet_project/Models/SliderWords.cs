using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNet_project.Models
{
	public class SliderWords:BaseEntity
	{
        public string Title { get; set; }
        public string MainText { get; set; }
        public string Subtitle { get; set; }
        public bool IsMain { get; set; } = false;

    }
}

