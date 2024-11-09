using Microsoft.AspNetCore.Identity;

namespace AspNet_project.Models
{
    public class AppUser: IdentityUser
    {
        public string FullName { get; set; }
    }
}
