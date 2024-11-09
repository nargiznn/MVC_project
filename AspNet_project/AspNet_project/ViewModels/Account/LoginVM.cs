using System;
using System.ComponentModel.DataAnnotations;

namespace AspNet_project.ViewModels.Account
{
	public class LoginVM
	{
        [Required]
        public string UsernameOrEmail { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

