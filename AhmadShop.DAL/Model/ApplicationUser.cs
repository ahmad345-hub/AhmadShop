using Microsoft.AspNetCore.Identity;

namespace AhmadShop.DAL.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}