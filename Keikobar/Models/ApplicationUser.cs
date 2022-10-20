using Microsoft.AspNetCore.Identity;

namespace Keikobar.Models;

public class ApplicationUser : IdentityUser
{
    public string Login { get; set; }
}