using Microsoft.AspNetCore.Identity;

namespace SiceCoreApi.Models.Security
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
    }
}
