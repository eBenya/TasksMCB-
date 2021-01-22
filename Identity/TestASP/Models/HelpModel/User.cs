using Microsoft.AspNetCore.Identity;

namespace TestASP.Models.HelpModel
{
    public class User : IdentityUser
    {
        public int Year { get; set; }
    }

    public class CreateUserViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int Year { get; set; }
    }

    public class EditUserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public int Year { get; set; }
    }
}
